using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_Data_UsageApplication
    {
        ResponseBaseDTO<IList<Cw_Data_UsageDTO>> ObtenerTodos();
    }
}
