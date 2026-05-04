using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_Object_TypeRepository
    {
        ResponseBase<IList<Cw_Object_Type>> GetByModelName(string ModelName);
    }
}
