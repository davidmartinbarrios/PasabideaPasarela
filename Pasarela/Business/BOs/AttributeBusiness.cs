using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class AttributeBusiness: IAttributeBusiness
    {
        public ResponseBase<IList<Attribute>> ObtenerTodos()
        {
            ResponseBase<IList<Attribute>> businessResponse;
            businessResponse = new AttributeRepository().GetAll();
            return businessResponse;
        }
    }
}
