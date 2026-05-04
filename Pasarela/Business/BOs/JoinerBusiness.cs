using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class JoinerBusiness: IJoinerBusiness
    {
        public ResponseBase<IList<Joiner>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Joiner>> businessResponse;
            businessResponse = new JoinerRepository().GetByModelNameAndID(ModelName, DI_ID);
            return businessResponse;
        }
    }
}
