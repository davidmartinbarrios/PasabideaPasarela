using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_Object_TypeBusiness
    {
        ResponseBase<IList<Cw_Object_Type>> ObtenerPorModelo(string ModelName);
    }
}
