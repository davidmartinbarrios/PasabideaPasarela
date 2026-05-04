using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class ErwinDiagramBusiness : IErwinDiagramBusiness
    {
        public ResponseBase<IList<ErwinDiagram>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<ErwinDiagram>> businessResponse;
            businessResponse = new ErwinDiagramRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
