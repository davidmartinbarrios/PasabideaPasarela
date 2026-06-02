#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
vb_llamadas_jerarquia.py

Lee un fichero VB.NET/VB6 migrado, por defecto frmProceso.vb, y genera llamadas.txt
con la jerarquía de llamadas.

Pensado para PasarelaT0NET / frmProceso.vb.

Uso básico:
    python vb_llamadas_jerarquia.py

Uso indicando fichero:
    python vb_llamadas_jerarquia.py frmProceso.vb

Uso indicando salida:
    python vb_llamadas_jerarquia.py frmProceso.vb --output llamadas.txt

Qué hace:
  1. Detecta Subs/Functions.
  2. Detecta llamadas entre funciones del mismo fichero.
  3. Detecta entradas WinForms:
       - frmProceso_Load
       - frmProceso_Activated
       - timers: *_Tick / Handles xxx.Tick
  4. Genera llamadas.txt empezando por frmProceso_Load si existe.
  5. Si hay Timer, lo cuelga lógicamente de frmProceso_Load para ver el flujo real:
       frmProceso_Load
          -> tmrProg_Tick
             -> msPasarProcedimientos
  6. Añade al final funciones sueltas/no alcanzadas con su jerarquía.

No es un compilador VB.NET. Es análisis estático por regex, suficiente para orientarse en código legacy.
"""

import argparse
import re
from collections import defaultdict
from pathlib import Path


VB_KEYWORDS = {
    "If", "Then", "Else", "ElseIf", "End", "For", "Each", "Next", "While", "Wend",
    "Do", "Loop", "Select", "Case", "Try", "Catch", "Finally", "Return", "Exit",
    "GoTo", "Resume", "On", "Error", "New", "Set", "Let", "Get", "Call",
    "CInt", "CStr", "CDbl", "CShort", "CLng", "Val", "Trim", "Mid", "Left", "Right",
    "Len", "InStr", "Replace", "UCase", "LCase", "Format", "MsgBox", "IIf",
    "IsNothing", "Nothing", "True", "False", "String", "Integer", "Long", "Short",
    "Boolean", "Object", "Date", "Now", "Time", "Timer", "ADODB", "System",
    "Microsoft", "VB6", "Application", "Me", "MyBase", "My",
}


def remove_comments(line: str) -> str:
    """Quita comentarios que empiezan por ', respetando strings."""
    in_string = False
    out = []

    i = 0
    while i < len(line):
        ch = line[i]

        if ch == '"':
            in_string = not in_string
            out.append(ch)
        elif ch == "'" and not in_string:
            break
        else:
            out.append(ch)

        i += 1

    return "".join(out)


def normalize_code(text: str) -> list[tuple[int, str]]:
    """
    Devuelve lista de (num_linea_original, linea_limpia), uniendo continuaciones con _.
    """
    result = []
    buffer = ""
    buffer_start_line = None

    for n, raw in enumerate(text.splitlines(), start=1):
        line = remove_comments(raw).rstrip()

        if not line.strip():
            continue

        if buffer_start_line is None:
            buffer_start_line = n

        if line.rstrip().endswith("_"):
            buffer += line.rstrip()[:-1] + " "
        else:
            buffer += line
            result.append((buffer_start_line, buffer))
            buffer = ""
            buffer_start_line = None

    if buffer:
        result.append((buffer_start_line or 0, buffer))

    return result


def find_procedures(lines: list[tuple[int, str]]) -> dict[str, dict]:
    """
    Detecta Sub/Function y captura sus líneas.
    """
    proc_start = re.compile(
        r"^\s*(Public|Private|Friend|Protected|Static)?\s*"
        r"(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(",
        re.IGNORECASE,
    )
    proc_end = re.compile(r"^\s*End\s+(Sub|Function)\b", re.IGNORECASE)

    procedures = {}
    current = None
    current_lines = []
    start_line = None
    signature = ""

    for line_no, line in lines:
        m = proc_start.search(line)

        if m:
            current = m.group(3)
            start_line = line_no
            signature = line.strip()
            current_lines = [(line_no, line)]
            continue

        if current:
            current_lines.append((line_no, line))

            if proc_end.search(line):
                procedures[current] = {
                    "name": current,
                    "start": start_line,
                    "end": line_no,
                    "signature": signature,
                    "lines": current_lines,
                }
                current = None
                current_lines = []
                start_line = None
                signature = ""

    return procedures


def detect_entry_points(procedures: dict[str, dict]) -> dict[str, list[str]]:
    """
    Detecta puntos de entrada típicos de WinForms.
    """
    load = []
    activated = []
    timers = []
    other_events = []

    for name, data in procedures.items():
        sig = data["signature"]

        # Load
        if re.search(r"Handles\s+MyBase\.Load\b", sig, re.IGNORECASE) or name.lower().endswith("_load"):
            load.append(name)
            continue

        # Activated / Activate
        if (
            re.search(r"Handles\s+MyBase\.Activated\b", sig, re.IGNORECASE)
            or name.lower().endswith("_activated")
            or name.lower().endswith("_activate")
        ):
            activated.append(name)
            continue

        # Timer Tick
        if re.search(r"Handles\s+[A-Za-z_][A-Za-z0-9_]*\.Tick\b", sig, re.IGNORECASE) or name.lower().endswith("_tick"):
            timers.append(name)
            continue

        # Otros eventos Handles
        if re.search(r"\bHandles\b", sig, re.IGNORECASE):
            other_events.append(name)

    return {
        "load": sorted(load),
        "activated": sorted(activated),
        "timers": sorted(timers),
        "other_events": sorted(other_events),
    }


def extract_calls(proc_lines: list[tuple[int, str]], known_names: set[str]) -> list[str]:
    """
    Extrae llamadas en orden aproximado de aparición.
    Solo devuelve funciones definidas en el mismo fichero.
    """
    calls = []
    seen_on_proc = set()

    for _, line in proc_lines:
        stripped = line.strip()

        if not stripped:
            continue

        # Evitar declaración
        if re.match(r"^(Public|Private|Friend|Protected|Static)?\s*(Sub|Function)\b", stripped, re.IGNORECASE):
            continue

        # Call nombre(...)
        for m in re.finditer(r"\bCall\s+([A-Za-z_][A-Za-z0-9_]*)\b", stripped, re.IGNORECASE):
            name = m.group(1)
            if name in known_names and name not in seen_on_proc:
                calls.append(name)
                seen_on_proc.add(name)

        # nombre(...)
        for m in re.finditer(r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\(", stripped):
            name = m.group(1)
            if name in known_names and name not in seen_on_proc:
                calls.append(name)
                seen_on_proc.add(name)

        # VB clásico: nombre arg1, arg2
        # Evita asignaciones: x = funcion(...)
        m = re.match(r"^\s*([A-Za-z_][A-Za-z0-9_]*)\s+.+", stripped)
        if m:
            name = m.group(1)
            if name in known_names and name not in seen_on_proc and name not in VB_KEYWORDS:
                calls.append(name)
                seen_on_proc.add(name)

    return calls


def build_graph(procedures: dict[str, dict]) -> dict[str, list[str]]:
    known = set(procedures.keys())
    graph = {}

    for name, data in procedures.items():
        graph[name] = extract_calls(data["lines"], known)

    return graph


def add_logical_event_bridges(graph: dict[str, list[str]], entry_points: dict[str, list[str]]):
    """
    WinForms no llama explícitamente al Timer desde Load en el código,
    pero para entender el proceso interesa colgar Tick/Activated debajo de Load.
    """
    load_roots = entry_points["load"]
    activated = entry_points["activated"]
    timers = entry_points["timers"]

    if not load_roots:
        return

    main_load = load_roots[0]
    graph.setdefault(main_load, [])

    # Primero lo que ya llama de verdad.
    # Después eventos lógicos relevantes si no están.
    for event_func in activated + timers:
        if event_func != main_load and event_func not in graph[main_load]:
            graph[main_load].append(event_func)


def choose_main_roots(entry_points: dict[str, list[str]], graph: dict[str, list[str]]) -> list[str]:
    """
    Prioridad:
      1. Load
      2. Activated
      3. Timer Tick
      4. msPasarProcedimientos
      5. raíces detectadas
    """
    roots = []

    for group in ("load", "activated", "timers"):
        for r in entry_points[group]:
            if r not in roots:
                roots.append(r)

    if "msPasarProcedimientos" in graph and "msPasarProcedimientos" not in roots:
        roots.append("msPasarProcedimientos")

    if not roots:
        roots = find_roots(graph)

    return roots


def find_roots(graph: dict[str, list[str]]) -> list[str]:
    all_nodes = set(graph.keys())
    called = set()

    for children in graph.values():
        called.update(children)

    return sorted(all_nodes - called)


def render_tree(
    graph: dict[str, list[str]],
    root: str,
    global_seen: set[str] | None = None,
    max_depth: int = 99,
) -> tuple[list[str], set[str]]:
    lines = []
    visited_in_tree = set()

    def walk(node: str, depth: int, stack: list[str]):
        indent = "  " * depth
        prefix = "" if depth == 0 else "-> "
        lines.append(f"{indent}{prefix}{node}")

        visited_in_tree.add(node)

        if depth >= max_depth:
            lines.append(f"{indent}  ... [profundidad máxima alcanzada]")
            return

        if node in stack:
            lines.append(f"{indent}  ... [recursiva / ciclo]")
            return

        children = graph.get(node, [])

        for child in children:
            child_indent = "  " * (depth + 1)

            if child in stack:
                lines.append(f"{child_indent}-> {child}  [recursiva / ciclo]")
                visited_in_tree.add(child)
                continue

            walk(child, depth + 1, stack + [node])

    walk(root, 0, [])
    return lines, visited_in_tree


def generate_output(graph, entry_points, max_depth=99, include_orphans=True):
    output = []
    output.append("SECUENCIA DE LLAMADAS")
    output.append("====================")
    output.append("")

    if entry_points["load"]:
        output.append("Entradas detectadas:")
        output.append(f"  Load: {', '.join(entry_points['load'])}")
        if entry_points["activated"]:
            output.append(f"  Activated: {', '.join(entry_points['activated'])}")
        if entry_points["timers"]:
            output.append(f"  Timer/Tick: {', '.join(entry_points['timers'])}")
        if entry_points["other_events"]:
            output.append(f"  Otros eventos: {', '.join(entry_points['other_events'])}")
        output.append("")

    roots = choose_main_roots(entry_points, graph)

    # Si hay Load y hemos hecho bridge, basta sacar el primer Load como árbol principal.
    if entry_points["load"]:
        roots_to_render = [entry_points["load"][0]]
    else:
        roots_to_render = roots

    reached = set()

    output.append("FLUJO PRINCIPAL")
    output.append("---------------")
    output.append("")

    for idx, root in enumerate(roots_to_render):
        if idx > 0:
            output.append("")
        tree_lines, visited = render_tree(graph, root, max_depth=max_depth)
        output.extend(tree_lines)
        reached.update(visited)

    if include_orphans:
        remaining = sorted(set(graph.keys()) - reached)

        # Evitar listar eventos ya recogidos si quedan por cualquier motivo
        remaining = [r for r in remaining if r not in reached]

        if remaining:
            output.append("")
            output.append("FUNCIONES SUELTAS / NO ALCANZADAS DESDE EL FLUJO PRINCIPAL")
            output.append("----------------------------------------------------------")
            output.append("")

            # Agrupa primero eventos sueltos, luego funciones normales
            event_set = set(entry_points["other_events"])
            ordered_remaining = (
                [r for r in remaining if r in event_set]
                + [r for r in remaining if r not in event_set]
            )

            orphan_seen = set()
            for root in ordered_remaining:
                if root in orphan_seen:
                    continue

                tree_lines, visited = render_tree(graph, root, max_depth=max_depth)
                output.extend(tree_lines)
                output.append("")
                orphan_seen.update(visited)

    return "\n".join(output).rstrip() + "\n"


def main():
    parser = argparse.ArgumentParser(
        description="Genera llamadas.txt desde frmProceso.vb con jerarquía de llamadas."
    )
    parser.add_argument(
        "input",
        nargs="?",
        default="frmProceso.vb",
        help="Fichero de entrada VB. Por defecto: frmProceso.vb",
    )
    parser.add_argument(
        "--output",
        default="llamadas.txt",
        help="Fichero de salida. Por defecto: llamadas.txt",
    )
    parser.add_argument(
        "--max-depth",
        type=int,
        default=99,
        help="Profundidad máxima de expansión. Por defecto: 99",
    )
    parser.add_argument(
        "--no-event-bridge",
        action="store_true",
        help="No cuelga Activated/Timer debajo del Load.",
    )
    parser.add_argument(
        "--no-orphans",
        action="store_true",
        help="No añade funciones sueltas/no alcanzadas.",
    )

    args = parser.parse_args()

    input_path = Path(args.input)
    if not input_path.exists():
        raise FileNotFoundError(f"No existe el fichero de entrada: {input_path}")

    text = input_path.read_text(encoding="utf-8", errors="ignore")
    lines = normalize_code(text)
    procedures = find_procedures(lines)

    if not procedures:
        raise RuntimeError("No se han detectado Sub/Function en el fichero.")

    graph = build_graph(procedures)
    entry_points = detect_entry_points(procedures)

    if not args.no_event_bridge:
        add_logical_event_bridges(graph, entry_points)

    output = generate_output(
        graph,
        entry_points,
        max_depth=args.max_depth,
        include_orphans=not args.no_orphans,
    )

    output_path = Path(args.output)
    output_path.write_text(output, encoding="utf-8")

    print(f"Entrada: {input_path.resolve()}")
    print(f"Funciones detectadas: {len(procedures)}")
    print(f"Salida: {output_path.resolve()}")
    print("")
    print("Entradas detectadas:")
    print(f"  Load: {entry_points['load']}")
    print(f"  Activated: {entry_points['activated']}")
    print(f"  Timer/Tick: {entry_points['timers']}")
    print(f"  Otros eventos: {len(entry_points['other_events'])}")


if __name__ == "__main__":
    main()
