using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_Inter_ObjectApplication
    {
        ResponseBaseDTO<IList<Cw_Inter_ObjectDTO>> ObtenerTodos();
    }
}
