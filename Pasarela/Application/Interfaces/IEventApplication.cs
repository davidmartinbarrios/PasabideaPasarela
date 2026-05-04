using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IEventApplication
    {
        ResponseBaseDTO<IList<EventDTO>> ObtenerPorModelo(string ModelName);
    }
}
