using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IAttributeBusiness
    {
        ResponseBase<IList<Attribute>> ObtenerTodos();
    }
}
