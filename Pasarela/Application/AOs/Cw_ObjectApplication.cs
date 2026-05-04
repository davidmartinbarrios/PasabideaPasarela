using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_ObjectApplication : ICw_ObjectApplication
    {

        private Cw_ObjectDTO FillCw_ObjectDTO(Cw_Object _object)
        {
            Cw_ObjectDTO retobject;
            retobject = new Cw_ObjectDTO
            {
                GO_ID = _object.GO_ID,
                USERDEFINED = _object.USERDEFINED,
            };
            return retobject;
        }

        public ResponseBaseDTO<IList<Cw_ObjectDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<Cw_ObjectDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_ObjectDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Cw_Object>> response = new Cw_ObjectBusiness().ObtenerPorModelo(ModelName);

            IList<Cw_ObjectDTO> retList = new List<Cw_ObjectDTO>();
            Cw_ObjectDTO retObject;

            foreach (Cw_Object businessObject in response.Data)
            {
                retObject = FillCw_ObjectDTO(businessObject);
                retList.Add(retObject);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
