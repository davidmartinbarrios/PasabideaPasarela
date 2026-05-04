using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_ObjectBusiness: ICw_ObjectBusiness
    {
        public ResponseBase<IList<Cw_Object>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Cw_Object>> businessResponse;
            businessResponse = new Cw_ObjectRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
