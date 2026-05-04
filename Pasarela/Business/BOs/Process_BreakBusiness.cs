using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Process_BreakBusiness
    {
        public ResponseBase<IList<Process_Break>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Process_Break>> businessResponse;
            businessResponse = new Process_BreakRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
