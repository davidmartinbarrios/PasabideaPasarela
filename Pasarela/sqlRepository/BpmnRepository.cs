using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Entities.POCOs.Bpmn;
using Lantik.Pasarela.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Lantik.Pasarela.sqlRepository
{
    /// <summary>
    /// Repositorio BPMN (DP4 / erwin_evolve) siguiendo el patrón de DiagramRepository:
    /// - DBContext(Settings.BD_DP4)
    /// - Queries en struct Table
    /// - GetDataTable() + Parse_DataRow_To_POCO
    /// </summary>
    public class BpmnRepository
    {
        private readonly DBContext DB;

        public BpmnRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }

        public struct Table
        {
            // Header
            public const string DI_ID = "DI_ID";
            public const string DI_NAME = "DI_NAME";
            public const string DI_TYPE = "DI_TYPE";
            public const string DI_ANO_ID = "DI_ANO_ID";
            public const string DI_ANO_TABNR = "DI_ANO_TABNR";

            // Nodes
            public const string NodeId = "NodeId";
            public const string SH_SEQ = "SH_SEQ";
            public const string OT_NAME = "OT_NAME";
            public const string NodeName = "NodeName";
            public const string PR_ID = "PR_ID";
            public const string PR_TYPE_NAME = "PR_TYPE_NAME";
            public const string EV_ID = "EV_ID";
            public const string EV_INTERNAL = "EV_INTERNAL";
            public const string OU_ID = "OU_ID";
            public const string OU_NAME = "OU_NAME";
            public const string Lane_OU_ID = "Lane_OU_ID";
            public const string Lane_OU_NAME = "Lane_OU_NAME";
            public const string X_BPMN = "X_BPMN";
            public const string Y_BPMN = "Y_BPMN";
            public const string W_CM = "W_CM";
            public const string H_CM = "H_CM";
            public const string CenterX_BPMN = "CenterX_BPMN";
            public const string CenterY_BPMN = "CenterY_BPMN";
            public const string SuggestedBpmnType = "SuggestedBpmnType";

            // Edges
            public const string EdgeId = "EdgeId";
            public const string JO_SEQ = "JO_SEQ";
            public const string FromNodeId = "FromNodeId";
            public const string ToNodeId = "ToNodeId";
            public const string Waypoint1_X = "Waypoint1_X";
            public const string Waypoint1_Y = "Waypoint1_Y";
            public const string Waypoint2_X = "Waypoint2_X";
            public const string Waypoint2_Y = "Waypoint2_Y";

            // -----------------------
            // QUERIES
            // -----------------------
            public const string GetHeader =
                "SELECT DI_ID, DI_NAME, DI_TYPE, ANO_ID AS DI_ANO_ID, ANO_TABNR AS DI_ANO_TABNR " +
                "FROM dbo.DIAGRAM " +
                "WHERE MODEL_NAME = '{0}' AND DI_ID = {1};";

            /// <summary>
            /// Nodos (shapes) con bounds, lane contenedora y tipo sugerido.
            /// OJO: los ANO_TABNR están basados en tus datos de ejemplo:
            ///   12689 = Organización (lane)
            ///   9097  = Evento/Resultado
            ///   8953/20003 = Proceso/Procedimiento (PROCESS)
            /// </summary>
            public const string GetNodes = @"
DECLARE @ModelName sysname = N'{0}';
DECLARE @DiID int = {1};

;WITH MaxY AS (
    SELECT MaxY = MAX(CAST(S.SH_Y AS decimal(18,4)))
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @ModelName
      AND S.DI_ID      = @DiID
),
OrgShapes AS (
    SELECT
        S2.DI_ID,
        S2.SH_SEQ,
        X = CAST(S2.SH_X      AS decimal(18,4)),
        Y = CAST(S2.SH_Y      AS decimal(18,4)),
        W = CAST(S2.SH_WIDTH  AS decimal(18,4)),
        H = CAST(S2.SH_HEIGHT AS decimal(18,4)),
        OU.OU_ID,
        OU.OU_NAME,
        Area = CAST(S2.SH_WIDTH AS decimal(18,4)) * CAST(S2.SH_HEIGHT AS decimal(18,4))
    FROM dbo.SHAPE S2
    JOIN dbo.ORGANIZATION OU
      ON OU.MODEL_NAME = @ModelName
     AND OU.OU_ID      = S2.ANO_ID
     AND S2.ANO_TABNR  = 12689
    WHERE S2.MODEL_NAME = @ModelName
      AND S2.DI_ID      = @DiID
),
Base AS (
    SELECT
        S.DI_ID,
        S.SH_SEQ,
        S.ANO_TABNR,
        S.ANO_ID,
        X = CAST(S.SH_X      AS decimal(18,4)),
        Y = CAST(S.SH_Y      AS decimal(18,4)),
        W = CAST(S.SH_WIDTH  AS decimal(18,4)),
        H = CAST(S.SH_HEIGHT AS decimal(18,4)),
        OT.OT_NAME,

        P.PR_ID,
        P.PR_NAME,
        P.PR_TYPE,
        LU.LU_NAME AS PR_TYPE_NAME,

        EV.EV_ID,
        EV.EV_NAME,
        EV.EV_INTERNAL,

        OU.OU_ID,
        OU.OU_NAME
    FROM dbo.SHAPE S
    LEFT JOIN dbo.CW_OBJECT_TYPE OT
      ON OT.MODEL_NAME = @ModelName
     AND OT.OT_ID      = S.ANO_TABNR

    LEFT JOIN dbo.PROCESS P
      ON P.MODEL_NAME = @ModelName
     AND P.PR_ID      = S.ANO_ID
     AND S.ANO_TABNR IN (8953, 20003)

    LEFT JOIN dbo.CW_LOOKUP LU
      ON LU.MODEL_NAME = @ModelName
     AND LU.LU_ID      = P.PR_TYPE

    LEFT JOIN dbo.EVENT EV
      ON EV.MODEL_NAME = @ModelName
     AND EV.EV_ID      = S.ANO_ID
     AND S.ANO_TABNR   = 9097

    LEFT JOIN dbo.ORGANIZATION OU
      ON OU.MODEL_NAME = @ModelName
     AND OU.OU_ID      = S.ANO_ID
     AND S.ANO_TABNR   = 12689

    WHERE S.MODEL_NAME = @ModelName
      AND S.DI_ID      = @DiID
      AND S.SH_WIDTH  IS NOT NULL
      AND S.SH_HEIGHT IS NOT NULL
),
LanePick AS (
    SELECT
        b.DI_ID,
        b.SH_SEQ,
        os.OU_ID,
        os.OU_NAME,
        rn = ROW_NUMBER() OVER (
            PARTITION BY b.DI_ID, b.SH_SEQ
            ORDER BY os.Area ASC
        )
    FROM Base b
    JOIN OrgShapes os
      ON os.DI_ID = b.DI_ID
     AND b.X > os.X
     AND b.X < os.X + os.W
     AND b.Y < os.Y
     AND b.Y > os.Y - os.H
)
SELECT
    NodeId   = CONCAT('D', b.DI_ID, '_S', b.SH_SEQ),
    DI_ID    = b.DI_ID,
    SH_SEQ   = b.SH_SEQ,
    OT_NAME  = b.OT_NAME,
    NodeName = COALESCE(b.PR_NAME, b.EV_NAME, b.OU_NAME, CONCAT(N'(SH_', b.SH_SEQ, N')')),

    PR_ID        = b.PR_ID,
    PR_TYPE_NAME = b.PR_TYPE_NAME,

    EV_ID       = b.EV_ID,
    EV_INTERNAL = b.EV_INTERNAL,

    OU_ID   = b.OU_ID,
    OU_NAME = b.OU_NAME,

    Lane_OU_ID   = lp.OU_ID,
    Lane_OU_NAME = lp.OU_NAME,

    X_BPMN = b.X,
    Y_BPMN = (SELECT MaxY FROM MaxY) - b.Y,
    W_CM   = b.W,
    H_CM   = b.H,

    CenterX_BPMN = b.X + (b.W / 2),
    CenterY_BPMN = ((SELECT MaxY FROM MaxY) - b.Y) + (b.H / 2),

    SuggestedBpmnType =
        CASE
            WHEN b.ANO_TABNR = 12689 THEN 'laneOrPool'
            WHEN b.PR_TYPE_NAME LIKE N'%Ramificación%' THEN 'gateway'
            WHEN b.PR_TYPE_NAME LIKE N'%automát%'      THEN 'serviceTask'
            WHEN b.PR_ID IS NOT NULL                   THEN 'userTask'
            WHEN b.EV_ID IS NOT NULL                   THEN 'event'
            ELSE 'shape'
        END
FROM Base b
LEFT JOIN LanePick lp
  ON lp.DI_ID  = b.DI_ID
 AND lp.SH_SEQ = b.SH_SEQ
 AND lp.rn     = 1
ORDER BY b.Y DESC, b.X ASC, b.SH_SEQ;";

            public const string GetEdges = @"
DECLARE @ModelName sysname = N'{0}';
DECLARE @DiID int = {1};

;WITH MaxY AS (
    SELECT MaxY = MAX(CAST(S.SH_Y AS decimal(18,4)))
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @ModelName
      AND S.DI_ID      = @DiID
),
FromTo AS (
    SELECT
        J.DI_ID,
        J.JO_SEQ,
        SH_FROM = J.SH_SEQ_FROM,
        SH_TO   = J.SH_SEQ_TO
    FROM dbo.JOINER J
    WHERE J.MODEL_NAME = @ModelName
      AND J.DI_ID      = @DiID
)
SELECT
    EdgeId     = CONCAT('D', ft.DI_ID, '_J', ft.JO_SEQ),
    DI_ID      = ft.DI_ID,
    JO_SEQ     = ft.JO_SEQ,

    FromNodeId = CONCAT('D', ft.DI_ID, '_S', ft.SH_FROM),
    ToNodeId   = CONCAT('D', ft.DI_ID, '_S', ft.SH_TO),

    Waypoint1_X = SF.SH_X + (SF.SH_WIDTH / 2.0),
    Waypoint1_Y = ((SELECT MaxY FROM MaxY) - SF.SH_Y) + (SF.SH_HEIGHT / 2.0),

    Waypoint2_X = ST.SH_X + (ST.SH_WIDTH / 2.0),
    Waypoint2_Y = ((SELECT MaxY FROM MaxY) - ST.SH_Y) + (ST.SH_HEIGHT / 2.0)
FROM FromTo ft
JOIN dbo.SHAPE SF
  ON SF.MODEL_NAME = @ModelName
 AND SF.DI_ID      = ft.DI_ID
 AND SF.SH_SEQ     = ft.SH_FROM
JOIN dbo.SHAPE ST
  ON ST.MODEL_NAME = @ModelName
 AND ST.DI_ID      = ft.DI_ID
 AND ST.SH_SEQ     = ft.SH_TO
ORDER BY ft.JO_SEQ;";

            
        }



        public ResponseBase<BpmnExtract> GetExtract(string modelName, int diId)
        {
            ResponseBase<BpmnExtract> response = new ResponseBase<BpmnExtract>();
            BpmnExtract ret = new BpmnExtract();

            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            try
            {
                // 1) Header
                this.DB.SqlStatment = string.Format(Table.GetHeader, modelName, diId);
                Logger.Info("Ejecutamos la query HEADER: " + this.DB.SqlStatment);
                DataTable headerDT = this.DB.GetDataTable();

                if (headerDT.Rows.Count > 0)
                    ret.Header = Parse_Header_Row(headerDT.Rows[0]);

                // 2) Nodes
                this.DB.ClearParameters();
                this.DB.Type = CommandType.Text;
                this.DB.SqlStatment = string.Format(Table.GetNodes, modelName, diId);
                Logger.Info("Ejecutamos la query NODES: " + this.DB.SqlStatment);
                DataTable nodesDT = this.DB.GetDataTable();

                foreach (DataRow dr in nodesDT.Rows)
                    ret.Nodes.Add(Parse_Node_Row(dr));

                // 3) Edges
                this.DB.ClearParameters();
                this.DB.Type = CommandType.Text;
                this.DB.SqlStatment = string.Format(Table.GetEdges, modelName, diId);
                Logger.Info("Ejecutamos la query EDGES: " + this.DB.SqlStatment);
                DataTable edgesDT = this.DB.GetDataTable();

                foreach (DataRow dr in edgesDT.Rows)
                    ret.Edges.Add(Parse_Edge_Row(dr));

                response.Data = ret;

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }

        // ---------------------------
        // Parsers (DataRow -> POCO)
        // ---------------------------
        private static BpmnDiagramHeader Parse_Header_Row(DataRow dr)
        {
            BpmnDiagramHeader h = new BpmnDiagramHeader();
            h.DI_ID = ToInt(dr[Table.DI_ID]);
            h.DI_NAME = ToStr(dr[Table.DI_NAME]);
            h.DI_TYPE = ToInt(dr[Table.DI_TYPE]);
            h.DI_ANO_ID = ToInt(dr[Table.DI_ANO_ID]);
            h.DI_ANO_TABNR = ToInt(dr[Table.DI_ANO_TABNR]);
            return h;
        }

        private static BpmnNode Parse_Node_Row(DataRow dr)
        {
            BpmnNode n = new BpmnNode();

            n.NodeId = ToStr(dr[Table.NodeId]) ?? "";
            n.DI_ID = ToInt(dr[Table.DI_ID]);
            n.SH_SEQ = ToInt(dr[Table.SH_SEQ]);

            n.OT_NAME = ToStr(dr[Table.OT_NAME]);
            n.NodeName = ToStr(dr[Table.NodeName]) ?? "";

            n.PR_ID = ToNullableInt(dr[Table.PR_ID]);
            n.PR_TYPE_NAME = ToStr(dr[Table.PR_TYPE_NAME]);

            n.EV_ID = ToNullableInt(dr[Table.EV_ID]);
            n.EV_INTERNAL = ToNullableInt(dr[Table.EV_INTERNAL]);

            n.OU_ID = ToNullableInt(dr[Table.OU_ID]);
            n.OU_NAME = ToStr(dr[Table.OU_NAME]);

            n.Lane_OU_ID = ToNullableInt(dr[Table.Lane_OU_ID]);
            n.Lane_OU_NAME = ToStr(dr[Table.Lane_OU_NAME]);

            n.X_BPMN = ToDecimal(dr[Table.X_BPMN]);
            n.Y_BPMN = ToDecimal(dr[Table.Y_BPMN]);
            n.W_CM = ToDecimal(dr[Table.W_CM]);
            n.H_CM = ToDecimal(dr[Table.H_CM]);

            n.CenterX_BPMN = ToDecimal(dr[Table.CenterX_BPMN]);
            n.CenterY_BPMN = ToDecimal(dr[Table.CenterY_BPMN]);

            n.SuggestedBpmnType = ToStr(dr[Table.SuggestedBpmnType]) ?? "shape";

            if (string.IsNullOrEmpty(n.NodeId)) n.NodeId = "D" + n.DI_ID + "_S" + n.SH_SEQ;
            if (string.IsNullOrEmpty(n.NodeName)) n.NodeName = n.NodeId;

            return n;
        }

        private static BpmnEdge Parse_Edge_Row(DataRow dr)
        {
            BpmnEdge e = new BpmnEdge();

            e.EdgeId = ToStr(dr[Table.EdgeId]) ?? "";
            e.DI_ID = ToInt(dr[Table.DI_ID]);
            e.JO_SEQ = ToInt(dr[Table.JO_SEQ]);

            e.FromNodeId = ToStr(dr[Table.FromNodeId]) ?? "";
            e.ToNodeId = ToStr(dr[Table.ToNodeId]) ?? "";

            e.Waypoint1_X = ToDecimal(dr[Table.Waypoint1_X]);
            e.Waypoint1_Y = ToDecimal(dr[Table.Waypoint1_Y]);
            e.Waypoint2_X = ToDecimal(dr[Table.Waypoint2_X]);
            e.Waypoint2_Y = ToDecimal(dr[Table.Waypoint2_Y]);

            if (string.IsNullOrEmpty(e.EdgeId)) e.EdgeId = "D" + e.DI_ID + "_J" + e.JO_SEQ;

            return e;
        }

        // ---------------------------
        // Converters (robustos)
        // ---------------------------
        private static string ToStr(object o)
        {
            if (o == null || o == DBNull.Value) return null;
            string s = o.ToString();
            return string.IsNullOrEmpty(s) ? null : s;
        }

        private static int ToInt(object o)
        {
            if (o == null || o == DBNull.Value) return 0;
            int v;
            return int.TryParse(o.ToString(), out v) ? v : 0;
        }

        private static int? ToNullableInt(object o)
        {
            if (o == null || o == DBNull.Value) return null;
            int v;
            return int.TryParse(o.ToString(), out v) ? (int?)v : null;
        }

        private static decimal ToDecimal(object o)
        {
            if (o == null || o == DBNull.Value) return 0m;
            decimal v;
            return decimal.TryParse(o.ToString(), out v) ? v : 0m;
        }
    }
}