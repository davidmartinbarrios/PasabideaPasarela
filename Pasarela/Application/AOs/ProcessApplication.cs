using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class ProcessApplication : IProcessApplication
    {

        private ProcessDTO FillProcessDTO(Process process)
        {
            ProcessDTO retprocess;
            retprocess = new ProcessDTO
            {
                PR_ID = process.PR_ID,
                PR_TYPE = process.PR_TYPE,
                PR_NAME = process.PR_NAME,
                TIPO = process.TIPO

            };
            return retprocess;
        }

        public ResponseBaseDTO<IList<ProcessDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<ProcessDTO>> responseDTO = new ResponseBaseDTO<IList<ProcessDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Process>> response = new ProcessBusiness().ObtenerPorModelo(ModelName);

            IList<ProcessDTO> retList = new List<ProcessDTO>();
            ProcessDTO retProcess;

            foreach (Process businessProcess in response.Data)
            {
                retProcess = FillProcessDTO(businessProcess);
                retList.Add(retProcess);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
