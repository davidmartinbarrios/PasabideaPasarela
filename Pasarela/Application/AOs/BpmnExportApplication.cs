using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Helpers;

namespace Lantik.Pasarela.Application.AOs
{
    public class BpmnExportApplication : IBpmnExportApplication
    {
        public ResponseBaseDTO<string> ExportarBpmnXml(string modelName, int diId)
        {
            var responseDTO = new ResponseBaseDTO<string>();
            Logger.Debug("ExportarBpmnXml");

            var resp = new BpmnExportBusiness().ExportarBpmnXml(modelName, diId);
            responseDTO.Data = resp.Data;
            responseDTO.Query_Result.ParseResponse(resp.Query_Result);
            return responseDTO;
        }
    }
}