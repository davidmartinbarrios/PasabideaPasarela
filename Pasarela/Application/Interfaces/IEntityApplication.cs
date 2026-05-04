using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IEntityApplication
    {
        ResponseBaseDTO<IList<EntityDTO>> ObtenerTodos();
    }
}
