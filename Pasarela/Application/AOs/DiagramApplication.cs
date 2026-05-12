using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Lantik.Pasarela.Application.AOs
{
    public class DiagramApplication : IDiagramApplication
    {
        public ResponseBaseDTO<IList<DiagramDTO>> GetByDIId(int diId)
        {
            var responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            var business = new DiagramBusiness();

            ResponseBase<IList<Diagram>> response = business.GetByDIId(diId);

            var retList = new List<DiagramDTO>();

            if (response.Data == null || response.Data.Count == 0)
            {
                responseDTO.Data = retList;
                responseDTO.Query_Result.ParseResponse(response.Query_Result);
                return responseDTO;
            }

            business.DeleteByProcedimiento(response.Data[0].PROCEDIMIENTO);

            foreach (Diagram diagram in response.Data)
            {
                business.InsertDiagram1(diagram);
                retList.Add(FillDiagramDTO(diagram));
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);

            return responseDTO;
        }

        private DiagramDTO FillDiagramDTO(Diagram diagram)
        {
            DiagramDTO retdiagram;
            retdiagram = new DiagramDTO
            {
                PROCEDIMIENTO = diagram.PROCEDIMIENTO,
                ORDEN_N1 = diagram.ORDEN_N1,
                ORDEN_N2 = diagram.ORDEN_N2,
                ORDEN_N3 = diagram.ORDEN_N3,
                ORDEN_N4 = diagram.ORDEN_N4,
                ORDEN_N5 = diagram.ORDEN_N5,
                CAT_DIAGRAMA = diagram.CAT_DIAGRAMA,
                NOMBRE = diagram.NOMBRE,
                USERDEFINED = diagram.USERDEFINED,
                NIVEL = diagram.NIVEL,
                ARBOL = diagram.ARBOL,
                PLAZOTIPO1 = diagram.PLAZOTIPO1,
                PLAZOTIPO2 = diagram.PLAZOTIPO2,
                NIV_TRAMIT = diagram.NIV_TRAMIT,
                BLOQUEO_EXP = diagram.BLOQUEO_EXP,
                UNION_RAMAS = diagram.UNION_RAMAS,
                TRAMIT_SIMUL = diagram.TRAMIT_SIMUL,
                TRAM_OCULTO = diagram.TRAM_OCULTO,
                IND_VALORVAR = diagram.IND_VALORVAR,
                VUELTA_ATRAS = diagram.VUELTA_ATRAS,
                NOMBRE_TRAM = diagram.NOMBRE_TRAM
            };
            return retdiagram;
        }

        private Diagram Parse_DTO_To_POCO(DiagramDTO diagram)
        {
            Diagram businessAplicacion = new Diagram
            {
                PROCEDIMIENTO = diagram.PROCEDIMIENTO,
                ORDEN_N1 = diagram.ORDEN_N1,
                ORDEN_N2 = diagram.ORDEN_N2,
                ORDEN_N3 = diagram.ORDEN_N3,
                ORDEN_N4 = diagram.ORDEN_N4,
                ORDEN_N5 = diagram.ORDEN_N5,
                CAT_DIAGRAMA = diagram.CAT_DIAGRAMA,
                NOMBRE = diagram.NOMBRE,
                USERDEFINED = diagram.USERDEFINED,
                NIVEL = diagram.NIVEL,
                ARBOL = diagram.ARBOL,
                PLAZOTIPO1 = diagram.PLAZOTIPO1,
                PLAZOTIPO2 = diagram.PLAZOTIPO2,
                NIV_TRAMIT = diagram.NIV_TRAMIT,
                BLOQUEO_EXP = diagram.BLOQUEO_EXP,
                UNION_RAMAS = diagram.UNION_RAMAS,
                TRAMIT_SIMUL = diagram.TRAMIT_SIMUL,
                TRAM_OCULTO = diagram.TRAM_OCULTO,
                IND_VALORVAR = diagram.IND_VALORVAR,
                VUELTA_ATRAS = diagram.VUELTA_ATRAS,
                NOMBRE_TRAM = diagram.NOMBRE_TRAM
            };
            return businessAplicacion;
        }

        public ResponseBaseDTO<IList<DiagramDTO>> ObtenerTodos()
        {
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo Obtenertodos");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().ObtenerTodos();

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                retList.Add(retDiagram);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

        public ResponseBaseDTO<IList<DiagramDTO>> GetByDIModelName(string ModelName, int id)
        {
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            Logger.Debug("LLamamos al metodo Obtener ModelName con los valores: ModelName " + ModelName + " e id: " + id);
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName(ModelName, id);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                retList.Add(retDiagram);
            }
            Logger.Debug("Recuperamos " + retList + " registros");

            //foreach (Diagram businessDiagram in response.Data)
            //{
            //    retDiagram = FillDiagramDTO(businessDiagram);
            //    Logger.Debug("LLamamos al metodo InsertarDiagramaEspejo");
            //    responseI = new DiagramBusiness().InsertDiagram1(businessDiagram);
            //    retList.Add(retDiagram);
            //    responseDTOI.Data = responseI.Data;
            //    responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            //}
            var borrado = new DiagramBusiness().DeleteByProcedimiento(response.Data[0].PROCEDIMIENTO);
            var result = new DiagramBusiness().InsertDiagramList(response.Data);

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
        
        public ResponseBaseDTO<DiagramDTO> ObtenerPorId(string nombre, int id)
        {
            Logger.Debug("LLamamos al metodo ObtenerPorId con valores de nombre: " + nombre + "e ID: " + id);
            ResponseBase<Diagram> response = new DiagramBusiness().ObtenerPorId(nombre, id);

            ResponseBaseDTO<DiagramDTO> responseDTO = new ResponseBaseDTO<DiagramDTO>();
            responseDTO.Data = FillDiagramDTO(response.Data);
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }

        public ResponseBaseDTO<int> InsertDiagram1(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0); // TODO: Porqué está vacío??? usamos de base ARTEZELI

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram1 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram1(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram2(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram2 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram2(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram3(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram3 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram3(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram4(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram4 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram4(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram5(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram5 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram5(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram6(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram6 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram6(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram7(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram7 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram7(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram8(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram8 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram8(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram9(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram9 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram9(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram10(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram10 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram10(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram11(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram11 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram11(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram12(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram12 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram12(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram13(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram13 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram13(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram14(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram14 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram14(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram15(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram15 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram15(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram16(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram16 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram16(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram17(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram17 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram17(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram18(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram18 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram18(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram19(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram19 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram19(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram20(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram20 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram20(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram21(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram21 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram21(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram22(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram22 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram22(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram23(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram23 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram23(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram24(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram24 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram24(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> InsertDiagram25(DiagramDTO obj)
        {
            ResponseBase<int> responseI;
            ResponseBaseDTO<int> responseDTOI = new ResponseBaseDTO<int>();
            ResponseBaseDTO<IList<DiagramDTO>> responseDTO = new ResponseBaseDTO<IList<DiagramDTO>>();

            Logger.Debug("LLamamos al metodo GetByDIModelName");
            ResponseBase<IList<Diagram>> response = new DiagramBusiness().GetByDIModelName("", 0);

            IList<DiagramDTO> retList = new List<DiagramDTO>();
            DiagramDTO retDiagram;

            Logger.Debug("Recuperamos " + response.Data.Count + " registros");
            foreach (Diagram businessDiagram in response.Data)
            {
                retDiagram = FillDiagramDTO(businessDiagram);
                Logger.Debug("LLamamos al metodo InsertDiagram25 con los valores: " + businessDiagram.NIVEL + "," + businessDiagram.NIVEL);
                responseI = new DiagramBusiness().InsertDiagram25(businessDiagram);
                retList.Add(retDiagram);
                responseDTOI.Data = responseI.Data;
                responseDTOI.Query_Result.ParseResponse(response.Query_Result);
            }
            return responseDTOI;
        }
        public ResponseBaseDTO<int> Update(DiagramDTO obj)
        {
            ResponseBaseDTO<int> responseDTO = new ResponseBaseDTO<int>();

            Diagram business = Parse_DTO_To_POCO(obj);
            ResponseBase<int> response = new DiagramBusiness().Update(business);
            responseDTO.Data = response.Data;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
