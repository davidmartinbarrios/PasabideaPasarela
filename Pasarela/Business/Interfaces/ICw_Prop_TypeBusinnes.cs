using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_Prop_TypeBusinnes
    {
        ResponseBase<IList<Cw_Prop_Type>> ObtenerPorModelo(string ModelName);
    }
}
