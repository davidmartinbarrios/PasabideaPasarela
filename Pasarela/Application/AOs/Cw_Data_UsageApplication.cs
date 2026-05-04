using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_Data_UsageApplication : ICw_Data_UsageApplication
    {

        private Cw_Data_UsageDTO FillCw_Data_UsageDTO(Cw_Data_Usage usage)
        {
            Cw_Data_UsageDTO retusage;
            retusage = new Cw_Data_UsageDTO
            {
                DM_DELETES = usage.DM_DELETES,
                DM_INSERTS = usage.DM_INSERTS,
            };
            return retusage;
        }

        public ResponseBaseDTO<IList<Cw_Data_UsageDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<Cw_Data_UsageDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_Data_UsageDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerTodos");
            ResponseBase<IList<Cw_Data_Usage>> response = new Cw_Data_UsageBusiness().ObtenerTodos();

            IList<Cw_Data_UsageDTO> retList = new List<Cw_Data_UsageDTO>();
            Cw_Data_UsageDTO retCw_Data_Usage;

            foreach (Cw_Data_Usage businessretusage in response.Data)
            {
                retCw_Data_Usage = FillCw_Data_UsageDTO(businessretusage);
                retList.Add(retCw_Data_Usage);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
