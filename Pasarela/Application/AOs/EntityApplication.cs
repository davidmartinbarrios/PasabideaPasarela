using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lantik.Pasarela.Application.AOs
{
    public class EntityApplication : IEntityApplication
    {

        private EntityDTO FillEntityDTO(Entity _entity)
        {
            EntityDTO retentity;
            retentity = new EntityDTO
            {
                EN_ID = _entity.EN_ID,
                EN_NAME = _entity.EN_NAME
            };
            return retentity;
        }

        public ResponseBaseDTO<IList<EntityDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<EntityDTO>> responseDTO = new ResponseBaseDTO<IList<EntityDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerTodos");
            ResponseBase<IList<Entity>> response = new EntityBusiness().ObtenerTodos();

            IList<EntityDTO> retList = new List<EntityDTO>();
            EntityDTO retEntity;

            foreach (Entity businessEntity in response.Data)
            {
                retEntity = FillEntityDTO(businessEntity);
                retList.Add(retEntity);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
