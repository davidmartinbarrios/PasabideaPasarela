using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IEntityBusiness
    {
        ResponseBase<IList<Entity>> ObtenerTodos();
    }
}
