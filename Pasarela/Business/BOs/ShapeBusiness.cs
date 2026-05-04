using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class ShapeBusiness: IShapeBusiness
    {
        public ResponseBase<IList<Shape>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Shape>> businessResponse;
            businessResponse = new ShapeRepository().GetByModelNameAndID(ModelName, DI_ID);
            return businessResponse;
        }
    }
}
