using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IJoinerApplication
    {
        ResponseBaseDTO<IList<JoinerDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID);
    }
}
