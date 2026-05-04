using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Entities.POCOs.Bpmn;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.sqlRepository;
using System;
using System.Diagnostics;

namespace Lantik.Pasarela.Business.BOs
{
    public class BpmnExportBusiness
    {
        public ResponseBase<string> ExportarBpmnXml(string modelName, int diId)
        {
            ResponseBase<string> resp = new ResponseBase<string>();

            ResponseBase<BpmnExtract> extractResp = new BpmnRepository().GetExtract(modelName, diId);

            // Si Query_Result es asignable, esto propaga el estado SQL tal cual:
            // (si no compila porque es read-only, comenta esta línea y usa el bloque de copia de abajo)
            resp.Query_Result = extractResp.Query_Result;

            if (extractResp.Query_Result != null && extractResp.Query_Result.Query_HasError())
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    extractResp.Query_Result.SQLMessage);

                // --- Si resp.Query_Result NO es asignable, copia aquí lo mínimo:
                // resp.Query_Result.SQLMessage = extractResp.Query_Result.SQLMessage;

                return resp;
            }

            resp.Data = Lantik.Pasarela.Business.Bpmn.BpmnXmlBuilder.Build(extractResp.Data);
            return resp;
        }
    }
}