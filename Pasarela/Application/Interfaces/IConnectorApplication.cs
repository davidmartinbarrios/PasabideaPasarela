using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IConnectorApplication
    {
        ResponseBaseDTO<IList<ConnectorDTO>> ObtenerPorModelo(string ModelName);
    }
}
