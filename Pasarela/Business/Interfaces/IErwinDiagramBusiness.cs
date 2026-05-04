using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IErwinDiagramBusiness
    {
        ResponseBase<IList<ErwinDiagram>> ObtenerPorModelo(string ModelName);
    }
}
