using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface ICw_Prop_TypeApplication
    {
        ResponseBaseDTO<IList<Cw_Prop_TypeDTO>> ObtenerPorModelo(string ModelName);
    }
}
