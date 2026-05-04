using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IOrganizationRepository
    {
        ResponseBase<IList<Organization>> GetByModelName(string ModelName);
    }
}
