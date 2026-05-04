using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_LookupApplication : ICw_LookupApplication
    {

        private Cw_LookupDTO FillCw_LookupApplicationDTO(Cw_Lookup lookup)
        {
            Cw_LookupDTO retlookup;
            retlookup = new Cw_LookupDTO
            {
                LU_ID = lookup.LU_ID,
                LU_NAME = lookup.LU_NAME,
                LU_ABBREVIATION = lookup.LU_ABBREVIATION,
            };
            return retlookup;
        }

        public ResponseBaseDTO<IList<Cw_LookupDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<Cw_LookupDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_LookupDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerTodos");
            ResponseBase<IList<Cw_Lookup>> response = new Cw_LookupBusiness().ObtenerTodos();

            IList<Cw_LookupDTO> retList = new List<Cw_LookupDTO>();
            Cw_LookupDTO retLookup;

            foreach (Cw_Lookup businesslookup in response.Data)
            {
                retLookup = FillCw_LookupApplicationDTO(businesslookup);
                retList.Add(retLookup);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

        public ResponseBaseDTO<IList<Cw_LookupDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<Cw_LookupDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_LookupDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Cw_Lookup>> response = new Cw_LookupBusiness().ObtenerPorModelo(ModelName);

            IList<Cw_LookupDTO> retList = new List<Cw_LookupDTO>();
            Cw_LookupDTO retLookup;

            foreach (Cw_Lookup businesslookup in response.Data)
            {
                retLookup = FillCw_LookupApplicationDTO(businesslookup);
                retList.Add(retLookup);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
