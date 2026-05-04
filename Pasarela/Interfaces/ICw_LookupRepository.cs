using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_LookupRepository
    {
        ResponseBase<IList<Cw_Lookup>> GetAll();

        ResponseBase<IList<Cw_Lookup>> GetByModelName(string ModelName);
    }
}
