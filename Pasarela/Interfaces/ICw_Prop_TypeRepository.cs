using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_Prop_TypeRepository
    {
        ResponseBase<IList<Cw_Prop_Type>> GetByModelName(string ModelName);
    }
}
