using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_Object_TypeBusiness: ICw_Object_TypeBusiness
    {
        public ResponseBase<IList<Cw_Object_Type>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Cw_Object_Type>> businessResponse;
            businessResponse = new Cw_Object_TypeRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
