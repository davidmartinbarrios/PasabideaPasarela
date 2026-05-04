using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IProcess_BreakRepository
    {
        ResponseBase<IList<Process_Break>> GetByModelName(string ModelName);
    }
}
