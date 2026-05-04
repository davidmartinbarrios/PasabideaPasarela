using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lantik.Pasarela.Application.AOs
{
    public class ShapeApplication: IShapeApplication
    {

        private ShapeDTO FillShapeDTO(Shape shape)
        {
            ShapeDTO retshape;
            retshape = new ShapeDTO
            {
                NUM_SEQ = shape.NUM_SEQ,
                ANO_TABNR = shape.ANO_TABNR,
                ANO_ID = shape.ANO_ID,
                SH_Y = shape.SH_Y,
                SH_X = shape.SH_X,
                OT_NAME = shape.OT_NAME,
                DI_ID = shape.DI_ID
            };
            return retshape;
        }

        public ResponseBaseDTO<IList<ShapeDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBaseDTO<IList<ShapeDTO>> responseDTO = new ResponseBaseDTO<IList<ShapeDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorIDyModelo con valores de ModelName: " + ModelName + "y DI_ID: " + DI_ID);
            ResponseBase<IList<Shape>> response = new ShapeBusiness().ObtenerPorIDyModelo(ModelName, DI_ID);

            IList<ShapeDTO> retList = new List<ShapeDTO>();
            ShapeDTO retShape;

            foreach (Shape businessShape in response.Data)
            {
                retShape = FillShapeDTO(businessShape);
                retList.Add(retShape);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
