using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IErwinDiagramRepository
    {
        ResponseBase<IList<ErwinDiagram>> GetByModelName(string ModelName);
    }
}
