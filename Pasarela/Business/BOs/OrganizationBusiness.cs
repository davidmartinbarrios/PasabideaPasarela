using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class OrganizationBusiness: IOrganizationBusiness
    {
        public ResponseBase<IList<Organization>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Organization>> businessResponse;
            businessResponse = new OrganizationRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
