using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IEventBusiness
    {
        ResponseBase<IList<Event>> ObtenerPorModelo(string ModelName);
    }
}
