using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class ErwinDiagramApplication : IErwinDiagramApplication
    {
        private ErwinDiagramDTO FillDiagramDTO(ErwinDiagram diagram)
        {
            ErwinDiagramDTO retDiagram;
            retDiagram = new ErwinDiagramDTO
            {
                DI_ID = diagram.DI_ID,
                DI_NAME = diagram.DI_NAME,
                DI_TYPE = diagram.DI_TYPE,
                ANO_ID = diagram.ANO_ID,
                ANO_TABNR = diagram.ANO_TABNR
            };
            return retDiagram;
        }

        public ResponseBaseDTO<IList<ErwinDiagramDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<ErwinDiagramDTO>> responseDTO = new ResponseBaseDTO<IList<ErwinDiagramDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<ErwinDiagram>> response = new ErwinDiagramBusiness().ObtenerPorModelo(ModelName);

            IList<ErwinDiagramDTO> retList = new List<ErwinDiagramDTO>();
            ErwinDiagramDTO retDiagram;

            foreach (ErwinDiagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                retList.Add(retDiagram);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

        public object ObtenerModelosARTEZ()
        {
            throw new NotImplementedException();
        }
    }
}
