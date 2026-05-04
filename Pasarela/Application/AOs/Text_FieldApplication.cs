using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class Text_FieldApplication : IText_FieldApplication
    {
        private Text_FieldDTO FillProcessDTO(Text_Field text)
        {
            Text_FieldDTO rettext;
            rettext = new Text_FieldDTO
            {
                ANO_TABNR = text.ANO_TABNR,
                ANO_ID = text.ANO_ID,
                TT_ATTRIBUTE = text.TT_ATTRIBUTE,
                VALUE = text.VALUE,

            };
            return rettext;
        }

        public ResponseBaseDTO<IList<Text_FieldDTO>> ObtenerPorAtributoeID(string AnoTabnr, int ANO_ID, int attribute)
        {
            ResponseBaseDTO<IList<Text_FieldDTO>> responseDTO = new ResponseBaseDTO<IList<Text_FieldDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorAtributoeID con los valores AnoTabnr: " + AnoTabnr + ", ANO_ID " + ANO_ID + " y attribute " + attribute);
            ResponseBase<IList<Text_Field>> response = new Text_FieldBusiness().GetByAttributeAndID(AnoTabnr, ANO_ID, attribute);

            IList<Text_FieldDTO> retList = new List<Text_FieldDTO>();
            Text_FieldDTO retProcess;

            foreach (Text_Field businessProcess in response.Data)
            {
                retProcess = FillProcessDTO(businessProcess);
                retList.Add(retProcess);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
