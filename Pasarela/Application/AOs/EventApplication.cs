using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class EventApplication : IEventApplication
    {

        private EventDTO FillEventDTO(Event _event)
        {
            EventDTO retevent;
            retevent = new EventDTO
            {
                EV_ID = _event.EV_ID,
                EV_NAME = _event.EV_NAME
            };
            return retevent;
        }

        public ResponseBaseDTO<IList<EventDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<EventDTO>> responseDTO = new ResponseBaseDTO<IList<EventDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Event>> response = new EventBusiness().ObtenerPorModelo(ModelName);

            IList<EventDTO> retList = new List<EventDTO>();
            EventDTO retEvent;

            foreach (Event businessEvent in response.Data)
            {
                retEvent = FillEventDTO(businessEvent);
                retList.Add(retEvent);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
