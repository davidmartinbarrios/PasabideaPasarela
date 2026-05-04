using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface ICw_Data_UsageRepository
    {
        ResponseBase<IList<Cw_Data_Usage>> GetAll();
    }
}
