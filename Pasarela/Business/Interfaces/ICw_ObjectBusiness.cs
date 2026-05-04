using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_ObjectBusiness
    {
        ResponseBase<IList<Cw_Object>> ObtenerPorModelo(string ModelName);
    }
}
