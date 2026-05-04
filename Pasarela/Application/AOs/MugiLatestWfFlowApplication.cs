using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class MugiLatestWfFlowApplication : IMugiLatestWfFlowApplication
    {
        private MugiLatestWfFlowDTO FillDTO(MugiLatestWfFlow item)
        {
            MugiLatestWfFlowDTO ret;
            ret = new MugiLatestWfFlowDTO
            {
                BaseDatos = item.BaseDatos,
                Flow = item.Flow,
                Version = item.Version,
                Comments = item.Comments
            };
            return ret;
        }

        public ResponseBaseDTO<IList<MugiLatestWfFlowDTO>> ObtenerUltimasVersionesDBN8()
        {
            ResponseBaseDTO<IList<MugiLatestWfFlowDTO>> responseDTO = new ResponseBaseDTO<IList<MugiLatestWfFlowDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerUltimasVersionesDBN8");
            ResponseBase<IList<MugiLatestWfFlow>> response = new MugiLatestWfFlowBusiness().ObtenerUltimasVersionesDBN8();

            IList<MugiLatestWfFlowDTO> retList = new List<MugiLatestWfFlowDTO>();
            MugiLatestWfFlowDTO retItem;

            if (response != null && response.Data != null)
            {
                foreach (MugiLatestWfFlow businessItem in response.Data)
                {
                    retItem = FillDTO(businessItem);
                    retList.Add(retItem);
                }
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
