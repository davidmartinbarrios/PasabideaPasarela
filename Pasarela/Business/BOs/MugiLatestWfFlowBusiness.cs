using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class MugiLatestWfFlowBusiness : IMugiLatestWfFlowBusiness
    {
        public ResponseBase<IList<MugiLatestWfFlow>> ObtenerUltimasVersionesDBN8()
        {
            ResponseBase<IList<MugiLatestWfFlow>> businessResponse;
            businessResponse = new MugiLatestWfFlowRepository().GetLatestVersionsDBN8();
            return businessResponse;
        }
    }
}
