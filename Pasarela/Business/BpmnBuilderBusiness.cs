using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Lantik.Pasarela.Entities.POCOs.Bpmn;

namespace Lantik.Pasarela.Business.Bpmn
{
    public sealed class BpmnBuilderOptions
    {
        public double ScaleDivisor { get; set; } = 1024.0; // Ajustar si se quiere otra escala
        public double Margin { get; set; } = 50.0;
        public string TargetNamespace { get; set; } = "http://lantik/pasarela/bpmn";
    }

    internal sealed class Bound
    {
        public double X;
        public double Y;
        public double W;
        public double H;

        public Bound() { }
        public Bound(double x, double y, double w, double h) { X = x; Y = y; W = w; H = h; }
    }

    public static class BpmnXmlBuilder
    {
        public static string Build(BpmnExtract data, BpmnBuilderOptions options = null)
        {
            if (options == null) options = new BpmnBuilderOptions();

            // --- Filtrado de nodos/edges relevantes ---
            List<BpmnNode> laneShapes = data.Nodes
                .Where(n => string.Equals(n.SuggestedBpmnType, "laneOrPool", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var allowedFlowTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "userTask","serviceTask","gateway","event"
            };

            List<BpmnNode> flowNodes = data.Nodes
                .Where(n =>
                {
                    var t = n.SuggestedBpmnType ?? "";
                    if (string.Equals(t, "laneOrPool", StringComparison.OrdinalIgnoreCase)) return false;
                    if (allowedFlowTypes.Contains(t)) return true;
                    if (n.PR_ID.HasValue) return true;
                    if (n.EV_ID.HasValue) return true;
                    return false;
                })
                .ToList();

            HashSet<string> flowNodeIds = new HashSet<string>(flowNodes.Select(n => n.NodeId));

            List<BpmnEdge> edges = data.Edges
                .Where(e => flowNodeIds.Contains(e.FromNodeId) && flowNodeIds.Contains(e.ToNodeId))
                .ToList();

            // --- Escala / normalización de coordenadas ---
            Func<decimal, double> S = v => (double)v / options.ScaleDivisor;

            List<double> xs = new List<double>();
            List<double> ys = new List<double>();

            foreach (var n in flowNodes)
            {
                xs.Add(S(n.X_BPMN));
                ys.Add(S(n.Y_BPMN));
                xs.Add(S(n.X_BPMN) + S(n.W_CM));
                ys.Add(S(n.Y_BPMN) + S(n.H_CM));
            }
            foreach (var l in laneShapes)
            {
                xs.Add(S(l.X_BPMN));
                ys.Add(S(l.Y_BPMN));
                xs.Add(S(l.X_BPMN) + S(l.W_CM));
                ys.Add(S(l.Y_BPMN) + S(l.H_CM));
            }
            foreach (var e in edges)
            {
                xs.Add(S(e.Waypoint1_X)); ys.Add(S(e.Waypoint1_Y));
                xs.Add(S(e.Waypoint2_X)); ys.Add(S(e.Waypoint2_Y));
            }

            double minX = xs.Count > 0 ? xs.Min() : 0;
            double minY = ys.Count > 0 ? ys.Min() : 0;

            double offX = (minX < 0 ? -minX : 0) + options.Margin;
            double offY = (minY < 0 ? -minY : 0) + options.Margin;

            Func<decimal, double> X = v => S(v) + offX;
            Func<decimal, double> Y = v => S(v) + offY;

            // --- Namespaces BPMN ---
            XNamespace bpmn = "http://www.omg.org/spec/BPMN/20100524/MODEL";
            XNamespace bpmndi = "http://www.omg.org/spec/BPMN/20100524/DI";
            XNamespace dc = "http://www.omg.org/spec/DD/20100524/DC";
            XNamespace di = "http://www.omg.org/spec/DD/20100524/DI";

            string defsId = "Defs_" + data.Header.DI_ID;
            string processId = "Process_" + data.Header.DI_ID;
            string collabId = "Collab_" + data.Header.DI_ID;
            string participantId = "Participant_" + data.Header.DI_ID;
            string planeId = "Plane_" + data.Header.DI_ID;
            string diagramId = "BPMNDiagram_" + data.Header.DI_ID;

            var definitions = new XElement(bpmn + "definitions",
                new XAttribute("id", defsId),
                new XAttribute("targetNamespace", options.TargetNamespace),
                new XAttribute(XNamespace.Xmlns + "bpmn", bpmn),
                new XAttribute(XNamespace.Xmlns + "bpmndi", bpmndi),
                new XAttribute(XNamespace.Xmlns + "dc", dc),
                new XAttribute(XNamespace.Xmlns + "di", di)
            );

            // collaboration + participant
            var collaboration = new XElement(bpmn + "collaboration", new XAttribute("id", collabId));

            string diagName = string.IsNullOrEmpty(data.Header.DI_NAME)
                ? ("DI_" + data.Header.DI_ID)
                : data.Header.DI_NAME;

            var participant = new XElement(bpmn + "participant",
                new XAttribute("id", participantId),
                new XAttribute("name", diagName),
                new XAttribute("processRef", processId));

            collaboration.Add(participant);

            // process
            var process = new XElement(bpmn + "process",
                new XAttribute("id", processId),
                new XAttribute("name", diagName),
                new XAttribute("isExecutable", "false"));

            // --- Lanes (si existen shapes de organización) ---
            var lanesByOu = laneShapes
                .Where(l => l.OU_ID.HasValue)
                .GroupBy(l => l.OU_ID.Value)
                .ToDictionary(g => g.Key, g => g.First());

            if (lanesByOu.Count > 0)
            {
                var laneSet = new XElement(bpmn + "laneSet",
                    new XAttribute("id", "LaneSet_" + data.Header.DI_ID));

                foreach (var kv in lanesByOu)
                {
                    int ouId = kv.Key;
                    BpmnNode laneShape = kv.Value;

                    string laneId = "Lane_" + ouId;
                    string laneName = !string.IsNullOrEmpty(laneShape.OU_NAME)
                        ? laneShape.OU_NAME
                        : (!string.IsNullOrEmpty(laneShape.NodeName) ? laneShape.NodeName : laneId);

                    var lane = new XElement(bpmn + "lane",
                        new XAttribute("id", laneId),
                        new XAttribute("name", laneName));

                    foreach (var n in flowNodes.Where(n => n.Lane_OU_ID.HasValue && n.Lane_OU_ID.Value == ouId))
                        lane.Add(new XElement(bpmn + "flowNodeRef", n.NodeId));

                    laneSet.Add(lane);
                }

                process.Add(laneSet);
            }

            // --- Flow nodes ---
            foreach (var n in flowNodes)
                process.Add(CreateFlowNodeElement(bpmn, n));

            // --- Sequence flows ---
            foreach (var e in edges)
            {
                string flowId = "Flow_" + e.EdgeId;
                process.Add(new XElement(bpmn + "sequenceFlow",
                    new XAttribute("id", flowId),
                    new XAttribute("sourceRef", e.FromNodeId),
                    new XAttribute("targetRef", e.ToNodeId)
                ));
            }

            definitions.Add(collaboration);
            definitions.Add(process);

            // --- BPMN-DI ---
            var bpmnDiagram = new XElement(bpmndi + "BPMNDiagram", new XAttribute("id", diagramId));
            var plane = new XElement(bpmndi + "BPMNPlane",
                new XAttribute("id", planeId),
                new XAttribute("bpmnElement", collabId));

            // Bounds globales para el participant
            var allBounds = new List<Bound>();

            foreach (var n in flowNodes)
                allBounds.Add(new Bound(X(n.X_BPMN), Y(n.Y_BPMN), S(n.W_CM), S(n.H_CM)));

            foreach (var l in laneShapes)
                allBounds.Add(new Bound(X(l.X_BPMN), Y(l.Y_BPMN), S(l.W_CM), S(l.H_CM)));

            Bound participantBBox = ComputeBBox(allBounds, options.Margin);

            plane.Add(new XElement(bpmndi + "BPMNShape",
                new XAttribute("id", "Shape_" + participantId),
                new XAttribute("bpmnElement", participantId),
                new XElement(dc + "Bounds",
                    new XAttribute("x", F(participantBBox.X)),
                    new XAttribute("y", F(participantBBox.Y)),
                    new XAttribute("width", F(participantBBox.W)),
                    new XAttribute("height", F(participantBBox.H))
                )
            ));

            // Lane shapes
            foreach (var kv in lanesByOu)
            {
                int ouId = kv.Key;
                BpmnNode l = kv.Value;
                string laneId = "Lane_" + ouId;

                plane.Add(new XElement(bpmndi + "BPMNShape",
                    new XAttribute("id", "Shape_" + laneId),
                    new XAttribute("bpmnElement", laneId),
                    new XElement(dc + "Bounds",
                        new XAttribute("x", F(X(l.X_BPMN))),
                        new XAttribute("y", F(Y(l.Y_BPMN))),
                        new XAttribute("width", F(S(l.W_CM))),
                        new XAttribute("height", F(S(l.H_CM)))
                    )
                ));
            }

            // Node shapes
            foreach (var n in flowNodes)
            {
                plane.Add(new XElement(bpmndi + "BPMNShape",
                    new XAttribute("id", "Shape_" + n.NodeId),
                    new XAttribute("bpmnElement", n.NodeId),
                    new XElement(dc + "Bounds",
                        new XAttribute("x", F(X(n.X_BPMN))),
                        new XAttribute("y", F(Y(n.Y_BPMN))),
                        new XAttribute("width", F(S(n.W_CM))),
                        new XAttribute("height", F(S(n.H_CM)))
                    )
                ));
            }

            // Edges DI
            foreach (var e in edges)
            {
                string flowId = "Flow_" + e.EdgeId;

                plane.Add(new XElement(bpmndi + "BPMNEdge",
                    new XAttribute("id", "Edge_" + e.EdgeId),
                    new XAttribute("bpmnElement", flowId),
                    new XElement(di + "waypoint",
                        new XAttribute("x", F(X(e.Waypoint1_X))),
                        new XAttribute("y", F(Y(e.Waypoint1_Y)))),
                    new XElement(di + "waypoint",
                        new XAttribute("x", F(X(e.Waypoint2_X))),
                        new XAttribute("y", F(Y(e.Waypoint2_Y))))
                ));
            }

            bpmnDiagram.Add(plane);
            definitions.Add(bpmnDiagram);

            var doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), definitions);
            return doc.ToString(SaveOptions.DisableFormatting);
        }

        private static XElement CreateFlowNodeElement(XNamespace bpmn, BpmnNode n)
        {
            string name = string.IsNullOrEmpty(n.NodeName) ? n.NodeId : n.NodeName;

            string prType = n.PR_TYPE_NAME ?? "";
            bool looksStart =
                name.IndexOf("Inicio", StringComparison.OrdinalIgnoreCase) >= 0 ||
                prType.IndexOf("Inicio", StringComparison.OrdinalIgnoreCase) >= 0;

            bool looksEnd =
                name.IndexOf("Fin", StringComparison.OrdinalIgnoreCase) >= 0 ||
                name.IndexOf("Resolución", StringComparison.OrdinalIgnoreCase) >= 0;

            string t = n.SuggestedBpmnType ?? "";

            if (string.Equals(t, "serviceTask", StringComparison.OrdinalIgnoreCase))
                return new XElement(bpmn + "serviceTask", new XAttribute("id", n.NodeId), new XAttribute("name", name));

            if (string.Equals(t, "gateway", StringComparison.OrdinalIgnoreCase))
                return new XElement(bpmn + "exclusiveGateway", new XAttribute("id", n.NodeId), new XAttribute("name", name));

            if (string.Equals(t, "event", StringComparison.OrdinalIgnoreCase))
            {
                if (looksStart)
                    return new XElement(bpmn + "startEvent", new XAttribute("id", n.NodeId), new XAttribute("name", name));
                if (looksEnd)
                    return new XElement(bpmn + "endEvent", new XAttribute("id", n.NodeId), new XAttribute("name", name));

                return new XElement(bpmn + "intermediateThrowEvent", new XAttribute("id", n.NodeId), new XAttribute("name", name));
            }

            // Default: userTask
            return new XElement(bpmn + "userTask", new XAttribute("id", n.NodeId), new XAttribute("name", name));
        }

        private static Bound ComputeBBox(List<Bound> bounds, double margin)
        {
            if (bounds == null || bounds.Count == 0) return new Bound(0, 0, 800, 600);

            double minX = bounds.Min(b => b.X);
            double minY = bounds.Min(b => b.Y);
            double maxX = bounds.Max(b => b.X + b.W);
            double maxY = bounds.Max(b => b.Y + b.H);

            return new Bound(minX - margin, minY - margin, (maxX - minX) + (2 * margin), (maxY - minY) + (2 * margin));
        }

        private static string F(double d)
        {
            return d.ToString("0.####", CultureInfo.InvariantCulture);
        }
    }
}