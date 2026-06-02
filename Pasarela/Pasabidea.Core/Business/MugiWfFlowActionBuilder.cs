using System.Collections.Generic;
using Lantik.Pasabidea.Core.Models.Mugi;

namespace Lantik.Pasabidea.Core.Business
{
    /// <summary>
    /// Construye en memoria las filas wfFlowActions.
    /// No conoce SQL Server ni DbContext.
    /// </summary>
    internal static class MugiWfFlowActionBuilder
    {
        public static List<MugiWfFlowAction> ConstruirAccionesBasicas(
            string flow,
            int version,
            List<MugiTramiteDTO> tramites)
        {
            var rows = new List<MugiWfFlowAction>();

            AddBegin(rows, flow, version);

            var flowOrder = 100;

            AddLabel(rows, flow, version, flowOrder, "INICIAL", "INICIAL", 1);

            foreach (var tramite in tramites)
            {
                flowOrder += 100;
                AddInicioTramite(rows, flow, version, flowOrder, tramite);

                if (!tramite.EsAutomatico)
                {
                    flowOrder += 10;
                    AddFormFicticio(rows, flow, version, flowOrder, tramite);
                }

                flowOrder += 10;
                AddAccionTramitePlaceholder(rows, flow, version, flowOrder, tramite);

                flowOrder += 10;
                AddFinTramite(rows, flow, version, flowOrder, tramite);
            }

            flowOrder += 100;
            AddLabel(rows, flow, version, flowOrder, "FINAL", "FINAL", 1);

            flowOrder += 100;
            AddEnd(rows, flow, version, flowOrder);

            return rows;
        }

        private static void AddBegin(List<MugiWfFlowAction> rows, string flow, int version)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = 0,
                Id = 0,
                Action = "BEGIN",
                Path = "INICIO",
                Param = "STATE",
                Value = "INICIO",
                Comments = "Generado por Pasabidea.Core"
            });
        }

        private static void AddEnd(List<MugiWfFlowAction> rows, string flow, int version, int flowOrder)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "END",
                Path = "FIN " + flow,
                Param = "FLUSHALL",
                Value = "0",
                Comments = "Fin técnico del flujo"
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = "1",
                Comments = string.Empty
            });
        }

        private static void AddLabel(
            List<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            string path,
            string caption,
            int level)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "LABEL",
                Path = path,
                Param = "CAPTION",
                Value = caption,
                Comments = string.Empty
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = level.ToString(),
                Comments = string.Empty
            });
        }

        private static void AddInicioTramite(
            List<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            MugiTramiteDTO tramite)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "LABEL",
                Path = tramite.Codigo,
                Param = "STATE",
                Value = tramite.Codigo,
                Comments = "Inicio lógico de trámite"
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = "2",
                Comments = string.Empty
            });
        }

        private static void AddFormFicticio(
            List<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            MugiTramiteDTO tramite)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "FORM",
                Path = "FORMFICTICIO_" + tramite.Codigo,
                Param = "FORM",
                Value = "FORMFICTICIO",
                Comments = "Parada de motor para trámite manual"
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = "2",
                Comments = string.Empty
            });
        }

        private static void AddAccionTramitePlaceholder(
            List<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            MugiTramiteDTO tramite)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "LABEL",
                Path = "APE_" + tramite.Codigo,
                Param = "CAPTION",
                Value = tramite.Descripcion ?? tramite.Codigo,
                Comments = "Placeholder de acción funcional del trámite"
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = "2",
                Comments = string.Empty
            });
        }

        private static void AddFinTramite(
            List<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            MugiTramiteDTO tramite)
        {
            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 0,
                Action = "ENDPROC",
                Path = "FIN_" + tramite.Codigo,
                Param = "COMENTARIO",
                Value = string.Empty,
                Comments = "Fin lógico de trámite"
            });

            rows.Add(new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = 1,
                Action = string.Empty,
                Path = string.Empty,
                Param = "LEVEL",
                Value = "2",
                Comments = string.Empty
            });
        }
    }
}
