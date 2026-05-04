using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_ObjectRepository
    {
        ResponseBase<IList<Cw_Object>> GetByModelName(string ModelName);
    }
}
