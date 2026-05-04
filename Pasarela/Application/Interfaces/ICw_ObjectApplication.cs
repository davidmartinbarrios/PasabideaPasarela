using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_ObjectApplication
    {
        ResponseBaseDTO<IList<Cw_ObjectDTO>> ObtenerPorModelo(string ModelName);
    }
}
