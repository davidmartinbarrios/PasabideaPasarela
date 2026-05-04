using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IOrganizationBusiness
    {
        ResponseBase<IList<Organization>> ObtenerPorModelo(string ModelName);
    }
}
