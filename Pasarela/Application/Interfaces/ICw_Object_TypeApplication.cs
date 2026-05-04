using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_Object_TypeApplication
    {
        ResponseBaseDTO<IList<Cw_Object_TypeDTO>> ObtenerPorModelo(string ModelName);
    }
}
