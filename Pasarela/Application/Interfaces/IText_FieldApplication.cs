using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IText_FieldApplication
    {
        ResponseBaseDTO<IList<Text_FieldDTO>> ObtenerPorAtributoeID(string AnoTabnr, int ANO_ID, int attribute);
    }
}
