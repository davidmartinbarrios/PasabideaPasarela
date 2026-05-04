using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IProcessRepository
    {
        ResponseBase<IList<Process>> GetByModelName(string ModelName);
    }
}
