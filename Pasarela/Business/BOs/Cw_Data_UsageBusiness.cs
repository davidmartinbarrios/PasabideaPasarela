using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_Data_UsageBusiness: ICw_Data_UsageBusiness
    {
        public ResponseBase<IList<Cw_Data_Usage>> ObtenerTodos()
        {
            ResponseBase<IList<Cw_Data_Usage>> businessResponse;
            businessResponse = new Cw_Data_UsageRepository().GetAll();
            return businessResponse;
        }
    }
}
