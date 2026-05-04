using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_Prop_TypeApplication : ICw_Prop_TypeApplication
    {

        private Cw_Prop_TypeDTO FillCw_Prop_TypeDTO(Cw_Prop_Type _type)
        {
            Cw_Prop_TypeDTO rettype;
            rettype = new Cw_Prop_TypeDTO
            {
                PPT_UUID = _type.PPT_UUID,
                PPT_NAME = _type.PPT_NAME,
                PPT_DATA_TYPE = _type.PPT_DATA_TYPE
            };
            return rettype;
        }

        public ResponseBaseDTO<IList<Cw_Prop_TypeDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<Cw_Prop_TypeDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_Prop_TypeDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Cw_Prop_Type>> response = new Cw_Prop_TypeBusinnes().ObtenerPorModelo(ModelName);

            IList<Cw_Prop_TypeDTO> retList = new List<Cw_Prop_TypeDTO>();
            Cw_Prop_TypeDTO retType;

            foreach (Cw_Prop_Type businessType in response.Data)
            {
                retType = FillCw_Prop_TypeDTO(businessType);
                retList.Add(retType);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
