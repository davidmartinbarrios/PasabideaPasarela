using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class ConnectorBusiness: IConnectorBusiness
    {
        public ResponseBase<IList<Connector>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Connector>> businessResponse;
            businessResponse = new ConnectorRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
