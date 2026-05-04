using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_LookupApplication
    {
        ResponseBaseDTO<IList<Cw_LookupDTO>> ObtenerTodos();

        ResponseBaseDTO<IList<Cw_LookupDTO>> ObtenerPorModelo(string ModelName);
    }
}
