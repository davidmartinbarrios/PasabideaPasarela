using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_LookupBusiness: ICw_LookupBusiness
    {
        public ResponseBase<IList<Cw_Lookup>> ObtenerTodos()
        {
            ResponseBase<IList<Cw_Lookup>> businessResponse;
            businessResponse = new Cw_LookupRepository().GetAll();
            return businessResponse;
        }

        public ResponseBase<IList<Cw_Lookup>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Cw_Lookup>> businessResponse;
            businessResponse = new Cw_LookupRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
