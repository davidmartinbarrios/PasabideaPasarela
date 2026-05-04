using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IProcessApplication
    {
        ResponseBaseDTO<IList<ProcessDTO>> ObtenerPorModelo(string ModelName);
    }
}
