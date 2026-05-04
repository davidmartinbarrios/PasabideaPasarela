using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_Inter_ObjectBusiness: ICw_Inter_ObjectBusiness
    {
        public ResponseBase<IList<Cw_Inter_Object>> ObtenerTodos()
        {
            ResponseBase<IList<Cw_Inter_Object>> businessResponse;
            businessResponse = new Cw_Inter_ObjectRepository().GetAll();
            return businessResponse;
        }
    }
}
