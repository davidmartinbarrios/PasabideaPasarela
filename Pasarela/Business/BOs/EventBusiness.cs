using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class EventBusiness: IEventBusiness
    {
        public ResponseBase<IList<Event>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Event>> businessResponse;
            businessResponse = new EventRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
