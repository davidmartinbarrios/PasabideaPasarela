using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Lantik.Pasarela.Application.AOs
{
    public class Conectores_DIApplication : IConectores_DIApplication
    {
        private Conectores_DIDTO FillConectores_DIDTO(Conectores_DI conector)
        {
            Conectores_DIDTO ret;
            ret = new Conectores_DIDTO
            {
                PROCEDIMIENTO = conector.PROCEDIMIENTO,
                ID_CONECTOR = conector.ID_CONECTOR,
                DIAGRAMA = conector.DIAGRAMA,
                NUM = conector.NUM,
                NUM_SEC_DESDE = conector.NUM_SEC_DESDE,
                NUM_SEC_HASTA = conector.NUM_SEC_HASTA,
                CAT_CONECTOR = conector.CAT_CONECTOR,
                CAT_CONECTOR2 = conector.CAT_CONECTOR2,
                DI_ID = conector.DI_ID,
                ORDEN_N1 = conector.ORDEN_N1,
                ORDEN_N2 = conector.ORDEN_N2,
                ORDEN_N3 = conector.ORDEN_N3,
                ORDEN_N4 = conector.ORDEN_N4,
                TIPO_CONECTOR = conector.TIPO_CONECTOR,
                SALIDA = conector.SALIDA
            };
            return ret;
        }

        public ResponseBaseDTO<int> Insertar(Conectores_DIDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<Conectores_DIDTO>> responseDTO = new ResponseBaseDTO<IList<Conectores_DIDTO>>();

            Logger.Debug("LLamamos al metodo Insertar");
            ResponseBase<IList<Conectores_DI>> response = new Conectores_DIBusiness().ObtenerPorIDyModelo("",1);

            IList<Conectores_DIDTO> retList = new List<Conectores_DIDTO>();
            Conectores_DIDTO retconectores;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Conectores_DI business in response.Data)
            {

                retconectores = FillConectores_DIDTO(business);
                Logger.Debug("LLamamos al metodo Insertar");
                responseI = new Conectores_DIBusiness().Insertar(business);
                retList.Add(retconectores);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }

        public ResponseBaseDTO<IList<Conectores_DIDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBaseDTO<IList<Conectores_DIDTO>> responseDTO = new ResponseBaseDTO<IList<Conectores_DIDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorIDyModelo con valores de ModelName: " + ModelName + " DI_ID: " + DI_ID);
            ResponseBase<IList<Conectores_DI>> response = new Conectores_DIBusiness().ObtenerPorIDyModelo(ModelName, DI_ID);

            IList<Conectores_DIDTO> retList = new List<Conectores_DIDTO>();
            Conectores_DIDTO retConectores;

            foreach (Conectores_DI business in response.Data)
            {
                retConectores = FillConectores_DIDTO(business);
                retList.Add(retConectores);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

        public ResponseBaseDTO<bool> Borrar(int IdDiagram)
        {
            ResponseBaseDTO<bool> responseDTO = new ResponseBaseDTO<bool>();

            ResponseBase<bool> response = new Conectores_DIBusiness().Borrar(IdDiagram);
            responseDTO.Data = response.Data;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

    }
}
