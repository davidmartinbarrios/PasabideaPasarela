using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IConnectorRepository
    {
        ResponseBase<IList<Connector>> GetByModelName(string ModelName);
    }
}
