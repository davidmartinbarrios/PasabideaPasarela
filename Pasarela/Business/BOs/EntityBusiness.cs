using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class EntityBusiness: IEntityBusiness
    {
        public ResponseBase<IList<Entity>> ObtenerTodos()
        {
            ResponseBase<IList<Entity>> businessResponse;
            businessResponse = new EntityRepository().GetAll();
            return businessResponse;
        }
    }
}
