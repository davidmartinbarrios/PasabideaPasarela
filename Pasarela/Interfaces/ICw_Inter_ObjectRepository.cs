using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_Inter_ObjectRepository
    {
        ResponseBase<IList<Cw_Inter_Object>> GetAll();
    }
}
