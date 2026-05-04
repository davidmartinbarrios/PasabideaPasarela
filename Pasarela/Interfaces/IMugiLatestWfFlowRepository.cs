using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IMugiLatestWfFlowRepository
    {
        ResponseBase<IList<MugiLatestWfFlow>> GetLatestVersionsDBN8();
    }
}
