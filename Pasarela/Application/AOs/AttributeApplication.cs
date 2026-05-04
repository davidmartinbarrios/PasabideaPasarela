using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class AttributeApplication : IAttributeApplication
    {

        private AttributeDTO FillAttributeDTO(Attribute attribute)
        {
            AttributeDTO retattribute;
            retattribute = new AttributeDTO
            {
                AT_ID = attribute.AT_ID,
                AT_NAME = attribute.AT_NAME,
                
            };
            return retattribute;
        }

        public ResponseBaseDTO<IList<AttributeDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<AttributeDTO>> responseDTO = new ResponseBaseDTO<IList<AttributeDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerTodos");
            ResponseBase<IList<Attribute>> response = new AttributeBusiness().ObtenerTodos();

            IList<AttributeDTO> retList = new List<AttributeDTO>();
            AttributeDTO retAttribute;

            foreach (Attribute businessAttribute in response.Data)
            {
                retAttribute = FillAttributeDTO(businessAttribute);
                retList.Add(retAttribute);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
