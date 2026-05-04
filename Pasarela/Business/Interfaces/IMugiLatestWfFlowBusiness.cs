using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IMugiLatestWfFlowBusiness
    {
        ResponseBase<IList<MugiLatestWfFlow>> ObtenerUltimasVersionesDBN8();
    }
}
