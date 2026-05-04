using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_Object_TypeApplication : ICw_Object_TypeApplication
    {

        private Cw_Object_TypeDTO FillCw_Object_TypeDTO(Cw_Object_Type _type)
        {
            Cw_Object_TypeDTO rettype;
            rettype = new Cw_Object_TypeDTO
            {
                OT_ID = _type.OT_ID,
                OT_NAME = _type.OT_NAME
            };
            return rettype;
        }

        public ResponseBaseDTO<IList<Cw_Object_TypeDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<Cw_Object_TypeDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_Object_TypeDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Cw_Object_Type>> response = new Cw_Object_TypeBusiness().ObtenerPorModelo(ModelName);

            IList<Cw_Object_TypeDTO> retList = new List<Cw_Object_TypeDTO>();
            Cw_Object_TypeDTO retType;

            foreach (Cw_Object_Type businessType in response.Data)
            {
                retType = FillCw_Object_TypeDTO(businessType);
                retList.Add(retType);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
