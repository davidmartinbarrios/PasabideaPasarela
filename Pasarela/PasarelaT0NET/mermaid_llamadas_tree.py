#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
mermaid_llamadas_tree.py

Lee un .txt/.mmd con un Mermaid flowchart de llamadas y genera llamadas.txt
con una jerarquía indentada.

Ejemplos:

  python mermaid_llamadas_tree.py "Pegado text.txt" --root frmProceso_Load

  # Para reflejar que frmProceso_Load activa el timer y de ahí empieza el proceso:
  python mermaid_llamadas_tree.py "Pegado text.txt" --root frmProceso_Load --bridge frmProceso_Load:tmrProg_Tick

  # Para partir directamente del núcleo:
  python mermaid_llamadas_tree.py "Pegado text.txt" --root msPasarProcedimientos
"""

import argparse
import re
from collections import defaultdict, OrderedDict
from pathlib import Path


EDGE_PATTERNS = [
    # A["A()"] --> B["B()"]
    re.compile(
        r'^\s*(?P<src>[A-Za-z_][A-Za-z0-9_]*)\s*\["[^"]*"\]\s*-->\s*'
        r'(?P<dst>[A-Za-z_][A-Za-z0-9_]*)\s*\["[^"]*"\]'
    ),
    # A --> B
    re.compile(
        r'^\s*(?P<src>[A-Za-z_][A-Za-z0-9_]*)\s*-->\s*'
        r'(?P<dst>[A-Za-z_][A-Za-z0-9_]*)'
    ),
]


def parse_edges(text: str):
    """
    Devuelve:
      graph: dict[str, list[str]]
      nodes: set[str]
    Mantiene el orden de aparición y evita duplicados por función.
    """
    graph = defaultdict(list)
    nodes = set()

    for line in text.splitlines():
        line = line.strip()
        if not line or line.startswith("flowchart") or line.startswith("graph"):
            continue

        matched = False
        for pat in EDGE_PATTERNS:
            m = pat.search(line)
            if m:
                src = m.group("src")
                dst = m.group("dst")

                nodes.add(src)
                nodes.add(dst)

                if dst not in graph[src]:
                    graph[src].append(dst)

                matched = True
                break

        # Nodo aislado tipo: msProceso["msProceso()"]
        if not matched:
            node_match = re.match(r'^\s*(?P<node>[A-Za-z_][A-Za-z0-9_]*)\s*\["[^"]*"\]', line)
            if node_match:
                node = node_match.group("node")
                nodes.add(node)
                graph.setdefault(node, [])

    for n in nodes:
        graph.setdefault(n, [])

    return dict(graph), nodes


def add_bridge(graph, bridge_arg: str):
    """
    Añade una llamada artificial origen:destino.
    Útil para reflejar eventos WinForms no presentes como llamada directa:
      frmProceso_Load:tmrProg_Tick
    """
    if not bridge_arg:
        return

    pairs = [p.strip() for p in bridge_arg.split(",") if p.strip()]
    for pair in pairs:
        if ":" not in pair:
            raise ValueError(f"Bridge inválido: {pair}. Usa origen:destino")
        src, dst = [x.strip() for x in pair.split(":", 1)]

        graph.setdefault(src, [])
        graph.setdefault(dst, [])

        if dst not in graph[src]:
            # Lo metemos al principio para que aparezca como continuación principal
            graph[src].insert(0, dst)


def build_tree_lines(graph, root, max_depth=99, include_repeated=False):
    lines = []

    def walk(node, depth, stack):
        indent = "  " * depth
        lines.append(f"{indent}-> {node}" if depth > 0 else f"{node}")

        if depth >= max_depth:
            lines.append(f"{indent}  ... [profundidad máxima alcanzada]")
            return

        if node in stack:
            lines.append(f"{indent}  ... [recursiva / ciclo: {node}]")
            return

        new_stack = stack + [node]
        children = graph.get(node, [])

        for child in children:
            if not include_repeated and child in new_stack:
                child_indent = "  " * (depth + 1)
                lines.append(f"{child_indent}-> {child}  [recursiva / ya en la rama]")
                continue

            walk(child, depth + 1, new_stack)

    walk(root, 0, [])
    return lines


def find_roots(graph):
    all_nodes = set(graph.keys())
    called = set()
    for children in graph.values():
        called.update(children)
    return sorted(all_nodes - called)


def main():
    parser = argparse.ArgumentParser(
        description="Genera llamadas.txt con jerarquía de llamadas desde un Mermaid flowchart."
    )
    parser.add_argument("input", help="Fichero .txt/.mmd con flowchart Mermaid")
    parser.add_argument("--root", default="frmProceso_Load", help="Función raíz. Por defecto: frmProceso_Load")
    parser.add_argument("--output", default="llamadas.txt", help="Fichero de salida. Por defecto: llamadas.txt")
    parser.add_argument("--max-depth", type=int, default=99, help="Profundidad máxima")
    parser.add_argument(
        "--bridge",
        default="",
        help="Llamadas artificiales origen:destino separadas por coma. Ej: frmProceso_Load:tmrProg_Tick"
    )
    parser.add_argument(
        "--include-repeated",
        action="store_true",
        help="Permite repetir llamadas aunque ya estén en la rama actual."
    )
    parser.add_argument(
        "--show-roots",
        action="store_true",
        help="Muestra posibles raíces detectadas."
    )

    args = parser.parse_args()

    input_path = Path(args.input)
    if not input_path.exists():
        raise FileNotFoundError(f"No existe el fichero: {input_path}")

    text = input_path.read_text(encoding="utf-8", errors="ignore")
    graph, nodes = parse_edges(text)

    add_bridge(graph, args.bridge)

    if args.show_roots:
        print("Posibles raíces:")
        for r in find_roots(graph):
            print(f" - {r}")
        print()

    if args.root not in graph:
        available = "\n".join(f" - {n}" for n in sorted(nodes))
        raise ValueError(f"No existe la raíz '{args.root}'. Funciones detectadas:\n{available}")

    lines = build_tree_lines(
        graph,
        args.root,
        max_depth=args.max_depth,
        include_repeated=args.include_repeated,
    )

    output_path = Path(args.output)
    output_path.write_text("\n".join(lines), encoding="utf-8")

    print(f"Generado: {output_path.resolve()}")
    print()
    print("\n".join(lines[:120]))
    if len(lines) > 120:
        print("\n... salida truncada en consola. Revisa el fichero completo.")


if __name__ == "__main__":
    main()
