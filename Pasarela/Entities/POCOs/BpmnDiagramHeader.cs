namespace Lantik.Pasarela.Entities.POCOs.Bpmn
{
    public sealed class BpmnDiagramHeader
    {
        public int DI_ID { get; set; }
        public string DI_NAME { get; set; } // puede venir null -> controlar al mapear
        public int DI_ANO_ID { get; set; }   // si en BD puede ser null, usa int? pero C# 7.3 lo soporta
        public int DI_ANO_TABNR { get; set; }
        public int DI_TYPE { get; set; }
    }

    public sealed class BpmnNode
    {
        public string NodeId { get; set; }
        public int DI_ID { get; set; }
        public int SH_SEQ { get; set; }

        public string OT_NAME { get; set; }       // puede ser null
        public string NodeName { get; set; }      // recomendado no-null

        public int? PR_ID { get; set; }           // nullable value type OK en C# 7.3
        public string PR_TYPE_NAME { get; set; }  // puede ser null

        public int? EV_ID { get; set; }
        public int? EV_INTERNAL { get; set; }

        public int? OU_ID { get; set; }
        public string OU_NAME { get; set; }       // puede ser null

        public int? Lane_OU_ID { get; set; }
        public string Lane_OU_NAME { get; set; }  // puede ser null

        public decimal X_BPMN { get; set; }
        public decimal Y_BPMN { get; set; }
        public decimal W_CM { get; set; }
        public decimal H_CM { get; set; }

        public decimal CenterX_BPMN { get; set; }
        public decimal CenterY_BPMN { get; set; }

        public string SuggestedBpmnType { get; set; } // "userTask"/"serviceTask"/"gateway"/"event"/"laneOrPool"
    }

    public sealed class BpmnEdge
    {
        public string EdgeId { get; set; }
        public int DI_ID { get; set; }
        public int JO_SEQ { get; set; }

        public string FromNodeId { get; set; }
        public string ToNodeId { get; set; }

        public decimal Waypoint1_X { get; set; }
        public decimal Waypoint1_Y { get; set; }
        public decimal Waypoint2_X { get; set; }
        public decimal Waypoint2_Y { get; set; }
    }

    public sealed class BpmnExtract
    {
        public BpmnDiagramHeader Header { get; set; }
        public System.Collections.Generic.IList<BpmnNode> Nodes { get; set; }
        public System.Collections.Generic.IList<BpmnEdge> Edges { get; set; }

        public BpmnExtract()
        {
            Header = new BpmnDiagramHeader();
            Nodes = new System.Collections.Generic.List<BpmnNode>();
            Edges = new System.Collections.Generic.List<BpmnEdge>();
        }
    }
}