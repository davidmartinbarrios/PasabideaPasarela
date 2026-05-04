using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface ICw_Data_UsageBusiness
    {
        ResponseBase<IList<Cw_Data_Usage>> ObtenerTodos();
    }
}
