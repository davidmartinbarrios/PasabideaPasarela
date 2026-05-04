using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;
namespace Lantik.Pasarela.Interfaces
{
    public interface IEventRepository
    {
        ResponseBase<IList<Event>> GetByModelName(string ModelName);
    }
}
