using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class JoinerApplication : IJoinerApplication
    {

        private JoinerDTO FillJoinerDTO(Joiner joiner)
        {
            JoinerDTO retjoiner;
            retjoiner = new JoinerDTO
            {
                DI_ID = joiner.DI_ID,
                ID_CONECTOR = joiner.ID_CONECTOR,
                NUM_SEQ_DESDE = joiner.NUM_SEQ_DESDE,
                NUM_SEQ_HASTA = joiner.NUM_SEQ_HASTA
            };
            return retjoiner;
        }

        public ResponseBaseDTO<IList<JoinerDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBaseDTO<IList<JoinerDTO>> responseDTO = new ResponseBaseDTO<IList<JoinerDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorIDyModelo con valores de ModelName: " + ModelName + "y DI_ID: " + DI_ID);
            ResponseBase<IList<Joiner>> response = new JoinerBusiness().ObtenerPorIDyModelo(ModelName, DI_ID);

            IList<JoinerDTO> retList = new List<JoinerDTO>();
            JoinerDTO retJoiner;

            foreach (Joiner businessJoiner in response.Data)
            {
                retJoiner = FillJoinerDTO(businessJoiner);
                retList.Add(retJoiner);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
