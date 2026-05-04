using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class ProcessBusiness: IProcessBusiness
    {
        public ResponseBase<IList<Process>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Process>> businessResponse;
            businessResponse = new ProcessRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
