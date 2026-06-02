import re
import sys
from pathlib import Path
from collections import defaultdict


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
    """
    Quita comentarios VB empezando por ', respetando comillas simples dentro de strings.
    """
    in_string = False
    result = []

    i = 0
    while i < len(line):
        ch = line[i]

        if ch == '"':
            in_string = not in_string
            result.append(ch)
        elif ch == "'" and not in_string:
            break
        else:
            result.append(ch)

        i += 1

    return "".join(result)


def normalize_code(text: str) -> list[str]:
    """
    Limpia comentarios y une líneas continuadas con _
    """
    lines = text.splitlines()
    cleaned = []

    buffer = ""

    for raw in lines:
        line = remove_comments(raw).rstrip()

        if not line.strip():
            continue

        if line.rstrip().endswith("_"):
            buffer += line.rstrip()[:-1] + " "
        else:
            buffer += line
            cleaned.append(buffer)
            buffer = ""

    if buffer:
        cleaned.append(buffer)

    return cleaned


def find_procedures(lines: list[str]) -> dict[str, dict]:
    """
    Detecta bloques Sub/Function/Property.
    """
    proc_start = re.compile(
        r"^\s*(Public|Private|Friend|Protected)?\s*"
        r"(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(",
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


def extract_called_names(proc_lines: list[str], known_proc_names: set[str]) -> set[str]:
    """
    Extrae llamadas aproximadas:
      - nombre(...)
      - Call nombre(...)
      - nombre arg1, arg2
    Filtra por funciones conocidas en el mismo fichero.
    """
    calls = set()

    joined_lines = proc_lines

    for line in joined_lines:
        stripped = line.strip()

        if not stripped:
            continue

        # Evitar la propia declaración de Sub/Function
        if re.match(r"^(Public|Private|Friend|Protected)?\s*(Sub|Function)\b", stripped, re.IGNORECASE):
            continue

        # Caso: Call funcion(...)
        for m in re.finditer(r"\bCall\s+([A-Za-z_][A-Za-z0-9_]*)\b", stripped, re.IGNORECASE):
            name = m.group(1)
            if name in known_proc_names:
                calls.add(name)

        # Caso: funcion(...)
        for m in re.finditer(r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\(", stripped):
            name = m.group(1)

            if name in known_proc_names:
                calls.add(name)

        # Caso VB clásico: funcion arg1, arg2
        # Ejemplo: msProceso j, total, i, max
        first_token = re.match(r"^([A-Za-z_][A-Za-z0-9_]*)\s+.+", stripped)
        if first_token:
            name = first_token.group(1)

            if name in known_proc_names:
                calls.add(name)

    return calls


def build_call_graph(procedures: dict[str, dict]) -> dict[str, set[str]]:
    known_proc_names = set(procedures.keys())
    graph = {}

    for name, data in procedures.items():
        graph[name] = extract_called_names(data["lines"], known_proc_names)

    return graph


def print_tree(graph: dict[str, set[str]], root: str, indent: int = 0, visited=None):
    if visited is None:
        visited = set()

    prefix = "   " * indent
    print(f"{prefix}{root}()")

    if root in visited:
        print(f"{prefix}   [ya visitada]")
        return

    visited.add(root)

    for child in sorted(graph.get(root, [])):
        print_tree(graph, child, indent + 1, visited.copy())


def print_all_roots(graph: dict[str, set[str]]):
    all_funcs = set(graph.keys())
    called = set()

    for calls in graph.values():
        called.update(calls)

    roots = sorted(all_funcs - called)

    print("=== POSIBLES RAÍCES ===")
    for r in roots:
        print(f"- {r}")

    print()
    print("=== ÁRBOLES DE LLAMADAS ===")

    for r in roots:
        print()
        print_tree(graph, r)


def write_mermaid(graph: dict[str, set[str]], output_path: Path):
    lines = ["flowchart TD"]

    for caller, callees in graph.items():
        if not callees:
            lines.append(f'    {caller}["{caller}()"]')
        else:
            for callee in sorted(callees):
                lines.append(f'    {caller}["{caller}()"] --> {callee}["{callee}()"]')

    output_path.write_text("\n".join(lines), encoding="utf-8")


def main():
    if len(sys.argv) < 2:
        print("Uso:")
        print("  python analizar_llamadas_vb.py frmProceso.vb")
        sys.exit(1)

    input_path = Path(sys.argv[1])

    if not input_path.exists():
        print(f"No existe el fichero: {input_path}")
        sys.exit(1)

    text = input_path.read_text(encoding="utf-8", errors="ignore")
    lines = normalize_code(text)

    procedures = find_procedures(lines)

    print(f"Funciones/Subs detectadas: {len(procedures)}")
    print()

    graph = build_call_graph(procedures)

    print_all_roots(graph)

    mermaid_path = input_path.with_suffix(".callgraph.mmd")
    write_mermaid(graph, mermaid_path)

    print()
    print(f"Mermaid generado en: {mermaid_path}")


if __name__ == "__main__":
    main()