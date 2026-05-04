using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IConnectorBusiness
    {
        ResponseBase<IList<Connector>> ObtenerPorModelo(string ModelName);
    }
}
