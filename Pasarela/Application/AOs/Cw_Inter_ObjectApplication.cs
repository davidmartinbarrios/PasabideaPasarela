using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Cw_Inter_ObjectApplication : ICw_Inter_ObjectApplication
    {

        private Cw_Inter_ObjectDTO FillCw_Data_UsageDTO(Cw_Inter_Object _object)
        {
            Cw_Inter_ObjectDTO retobject;
            retobject = new Cw_Inter_ObjectDTO
            {
                ANO_ID_BELOW = _object.ANO_ID_BELOW,
                GO_NAME = _object.GO_NAME,
            };
            return retobject;
        }

        public ResponseBaseDTO<IList<Cw_Inter_ObjectDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<Cw_Inter_ObjectDTO>> responseDTO = new ResponseBaseDTO<IList<Cw_Inter_ObjectDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerTodos");
            ResponseBase<IList<Cw_Inter_Object>> response = new Cw_Inter_ObjectBusiness().ObtenerTodos();

            IList<Cw_Inter_ObjectDTO> retList = new List<Cw_Inter_ObjectDTO>();
            Cw_Inter_ObjectDTO retObject;

            foreach (Cw_Inter_Object businessObject in response.Data)
            {
                retObject = FillCw_Data_UsageDTO(businessObject);
                retList.Add(retObject);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
