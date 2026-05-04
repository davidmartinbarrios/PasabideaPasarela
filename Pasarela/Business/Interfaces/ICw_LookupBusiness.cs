using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_LookupBusiness
    {
        ResponseBase<IList<Cw_Lookup>> ObtenerTodos();

        ResponseBase<IList<Cw_Lookup>> ObtenerPorModelo(string ModelName);
    }
}
