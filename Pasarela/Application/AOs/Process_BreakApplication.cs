using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Process_BreakApplication: IProcess_BreakApplication
    {
        private Process_BreakDTO FillProcessDTO(Process_Break process)
        {
            Process_BreakDTO retprocess;
            retprocess = new Process_BreakDTO
            {
                PR_ID = process.PR_ID,
                PR_NAME = process.PR_NAME,

            };
            return retprocess;
        }

        public ResponseBaseDTO<IList<Process_BreakDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<Process_BreakDTO>> responseDTO = new ResponseBaseDTO<IList<Process_BreakDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Process_Break>> response = new Process_BreakBusiness().ObtenerPorModelo(ModelName);

            IList<Process_BreakDTO> retList = new List<Process_BreakDTO>();
            Process_BreakDTO retProcess;

            foreach (Process_Break businessProcess in response.Data)
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
