import re
import sys
from pathlib import Path


VB_KEYWORDS = {
    "If", "Then", "Else", "ElseIf", "End", "For", "Each", "Next", "While", "Wend",
    "Do", "Loop", "Select", "Case", "Try", "Catch", "Finally", "Return", "Exit",
    "GoTo", "Resume", "On", "Error", "New", "Set", "Let", "Get", "Call",
    "CInt", "CStr", "CDbl", "CShort", "CLng", "Val", "Trim", "Mid", "Left", "Right",
    "Len", "InStr", "Replace", "UCase", "LCase", "Format", "MsgBox",
    "IIf", "IsNothing", "Nothing", "True", "False",
    "String", "Integer", "Long", "Short", "Boolean", "Object",
    "ADODB", "System", "Microsoft", "VB6", "Application", "Me",
}


def remove_comments(line: str) -> str:
    in_string = False
    result = []

    for ch in line:
        if ch == '"':
            in_string = not in_string
            result.append(ch)
        elif ch == "'" and not in_string:
            break
        else:
            result.append(ch)

    return "".join(result)


def normalize_code(text: str) -> list[str]:
    lines = text.splitlines()
    cleaned = []
    buffer = ""

    for raw in lines:
        line = remove_comments(raw).rstrip()

        if not line.strip():
            continue

        if line.endswith("_"):
            buffer += line[:-1] + " "
        else:
            buffer += line
            cleaned.append(buffer)
            buffer = ""

    if buffer:
        cleaned.append(buffer)

    return cleaned


def find_procedures(lines: list[str]) -> dict[str, dict]:
    proc_start = re.compile(
        r"^\s*(Public|Private|Friend|Protected)?\s*(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(",
        re.IGNORECASE,
    )
    proc_end = re.compile(r"^\s*End\s+(Sub|Function)\b", re.IGNORECASE)

    procedures = {}
    current_name = None
    current_lines = []
    start_line = None

    for idx, line in enumerate(lines, start=1):
        m = proc_start.search(line)
        if m:
            current_name = m.group(3)
            current_lines = [line]
            start_line = idx
            continue

        if current_name:
            current_lines.append(line)
            if proc_end.search(line):
                procedures[current_name] = {
                    "start": start_line,
                    "end": idx,
                    "lines": current_lines,
                }
                current_name = None
                current_lines = []
                start_line = None

    return procedures


def extract_called_names(proc_lines: list[str], known_proc_names: set[str]) -> list[str]:
    """
    Devuelve llamadas en orden aproximado de aparición.
    """
    calls = []
    seen = set()

    for line in proc_lines:
        stripped = line.strip()
        if not stripped:
            continue

        if re.match(r"^(Public|Private|Friend|Protected)?\s*(Sub|Function)\b", stripped, re.IGNORECASE):
            continue

        # Call nombre(...)
        for m in re.finditer(r"\bCall\s+([A-Za-z_][A-Za-z0-9_]*)\b", stripped, re.IGNORECASE):
            name = m.group(1)
            if name in known_proc_names and name not in seen:
                calls.append(name)
                seen.add(name)

        # nombre(...)
        for m in re.finditer(r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\(", stripped):
            name = m.group(1)
            if name in known_proc_names and name not in seen:
                calls.append(name)
                seen.add(name)

        # VB clásico: nombre arg1, arg2
        m = re.match(r"^\s*([A-Za-z_][A-Za-z0-9_]*)\s+.+", stripped)
        if m:
            name = m.group(1)
            if name in known_proc_names and name not in seen:
                calls.append(name)
                seen.add(name)

    return calls


def build_call_graph(procedures: dict[str, dict]) -> dict[str, list[str]]:
    known_proc_names = set(procedures.keys())
    graph = {}
    for name, data in procedures.items():
        graph[name] = extract_called_names(data["lines"], known_proc_names)
    return graph


def print_tree(graph: dict[str, list[str]], root: str, indent: int = 0, path=None):
    if path is None:
        path = set()

    prefix = "   " * indent
    print(f"{prefix}{root}()")

    if root in path:
        print(f"{prefix}   [recursiva / ya visitada en esta rama]")
        return

    new_path = set(path)
    new_path.add(root)

    for child in graph.get(root, []):
        print_tree(graph, child, indent + 1, new_path)


def collect_sequence_edges(graph: dict[str, list[str]], root: str, max_depth=8, path=None):
    """
    Genera una lista ordenada de mensajes caller -> callee
    haciendo un recorrido DFS desde root.
    """
    if path is None:
        path = []

    if len(path) > max_depth:
        return []

    edges = []
    current_path = path + [root]

    for callee in graph.get(root, []):
        edges.append((root, callee))

        # evitar ciclos infinitos
        if callee not in current_path:
            edges.extend(collect_sequence_edges(graph, callee, max_depth=max_depth, path=current_path))
        else:
            edges.append((callee, callee + " [recursiva]"))

    return edges


def unique_participants_from_edges(edges):
    participants = []
    seen = set()

    for a, b in edges:
        if a not in seen:
            participants.append(a)
            seen.add(a)
        if b not in seen:
            participants.append(b)
            seen.add(b)

    return participants


def write_mermaid_sequence(root: str, graph: dict[str, list[str]], output_path: Path, max_depth=8):
    edges = collect_sequence_edges(graph, root, max_depth=max_depth)

    participants = unique_participants_from_edges(edges)

    lines = ["sequenceDiagram"]
    lines.append("    autonumber")

    for p in participants:
        alias = sanitize_name(p)
        lines.append(f"    participant {alias} as {p}")

    for caller, callee in edges:
        caller_alias = sanitize_name(caller)
        callee_alias = sanitize_name(callee)
        lines.append(f"    {caller_alias}->>{callee_alias}: llama a {callee}")

    output_path.write_text("\n".join(lines), encoding="utf-8")


def sanitize_name(name: str) -> str:
    return re.sub(r"[^A-Za-z0-9_]", "_", name)


def main():
    if len(sys.argv) < 3:
        print("Uso:")
        print("  python analizar_secuencia_vb.py frmProceso.vb frmProceso_Load")
        sys.exit(1)

    input_path = Path(sys.argv[1])
    root = sys.argv[2]

    if not input_path.exists():
        print(f"No existe el fichero: {input_path}")
        sys.exit(1)

    text = input_path.read_text(encoding="utf-8", errors="ignore")
    lines = normalize_code(text)
    procedures = find_procedures(lines)
    graph = build_call_graph(procedures)

    if root not in graph:
        print(f"La función raíz '{root}' no existe en el fichero.")
        print("Funciones detectadas:")
        for k in sorted(graph.keys()):
            print(" -", k)
        sys.exit(1)

    print("=== SECUENCIA JERÁRQUICA ===")
    print_tree(graph, root)

    out_path = input_path.with_name(f"{input_path.stem}_{root}_sequence.mmd")
    write_mermaid_sequence(root, graph, out_path, max_depth=8)

    print()
    print(f"Mermaid sequence generado en: {out_path}")


if __name__ == "__main__":
    main()