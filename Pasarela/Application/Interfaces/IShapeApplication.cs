using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IShapeApplication
    {
        ResponseBaseDTO<IList<ShapeDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID);
    }
}
