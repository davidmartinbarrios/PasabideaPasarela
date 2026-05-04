using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class ConnectorApplication : IConnectorApplication
    {

        private ConnectorDTO FillConnectorDTO(Connector connector)
        {
            ConnectorDTO retconnector;
            retconnector = new ConnectorDTO
            {
                CO_ID = connector.CO_ID,
                CO_CONDITION = connector.CO_CONDITION,
                MODEL_NAME = connector.MODEL_NAME,
            };
            return retconnector;
        }

        public ResponseBaseDTO<IList<ConnectorDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<ConnectorDTO>> responseDTO = new ResponseBaseDTO<IList<ConnectorDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Connector>> response = new ConnectorBusiness().ObtenerPorModelo(ModelName);

            IList<ConnectorDTO> retList = new List<ConnectorDTO>();
            ConnectorDTO retConnector;

            foreach (Connector businessConnector in response.Data)
            {
                retConnector = FillConnectorDTO(businessConnector);
                retList.Add(retConnector);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
