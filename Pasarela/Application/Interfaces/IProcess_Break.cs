using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IProcess_BreakApplication
    {
        ResponseBaseDTO<IList<Process_BreakDTO>> ObtenerPorModelo(string ModelName);
    }
}
