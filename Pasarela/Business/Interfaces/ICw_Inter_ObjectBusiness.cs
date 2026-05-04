using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_Inter_ObjectBusiness
    {
        ResponseBase<IList<Cw_Inter_Object>> ObtenerTodos();
    }
}
