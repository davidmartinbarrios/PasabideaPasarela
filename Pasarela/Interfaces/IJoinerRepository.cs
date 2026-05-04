using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IJoinerRepository
    {
        ResponseBase<IList<Joiner>> GetByModelNameAndID(string ModelName, int DI_ID);
    }
}
