using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class DiagramBusiness: IDiagramBusiness
    {        public ResponseBase<IList<Diagram>> GetByDIId(int diId)
        {
            return new DiagramRepository().GetByDIId(diId);
        }

        public ResponseBase<IList<Diagram>> ObtenerTodos()
        {
            ResponseBase<IList<Diagram>> businessResponse;
            businessResponse = new DiagramRepository().GetAll();
            return businessResponse;
        }

        public ResponseBase<Diagram> ObtenerPorId(string nombre, int id)
        {
            ResponseBase<Diagram> businessResponse;
            businessResponse = new DiagramRepository().GetByDIID(nombre, id);
            return businessResponse;
        }

        public ResponseBase<IList<Diagram>> GetByDIModelName(string ModelName, int id)
        {
            ResponseBase<IList<Diagram>> businessResponse;
            businessResponse = new DiagramRepository().GetByDIModelName(ModelName, id);
            return businessResponse;
        }
        public ResponseBase<int> DeleteByProcedimiento(string Procedimiento)
        {
            return new DiagramRepository().DeleteByProcedimiento(Procedimiento);
        }
        public ResponseBase<int> InsertDiagramList(IList<Diagram> data)
        {
            return new DiagramRepository().InsertDiagramList(data);
        }

        public ResponseBase<int> InsertDiagram1(Diagram business)
        {
            return new DiagramRepository().InsertDiagram1(business);
        }
        public ResponseBase<int> InsertDiagram2(Diagram business)
        {
            return new DiagramRepository().InsertDiagram2(business);
        }
        public ResponseBase<int> InsertDiagram3(Diagram business)
        {
            return new DiagramRepository().InsertDiagram3(business);
        }
        public ResponseBase<int> InsertDiagram4(Diagram business)
        {
            return new DiagramRepository().InsertDiagram4(business);
        }
        public ResponseBase<int> InsertDiagram5(Diagram business)
        {
            return new DiagramRepository().InsertDiagram5(business);
        }
        public ResponseBase<int> InsertDiagram6(Diagram business)
        {
            return new DiagramRepository().InsertDiagram6(business);
        }
        public ResponseBase<int> InsertDiagram7(Diagram business)
        {
            return new DiagramRepository().InsertDiagram7(business);
        }
        public ResponseBase<int> InsertDiagram8(Diagram business)
        {
            return new DiagramRepository().InsertDiagram8(business);
        }
        public ResponseBase<int> InsertDiagram9(Diagram business)
        {
            return new DiagramRepository().InsertDiagram9(business);
        }
        public ResponseBase<int> InsertDiagram10(Diagram business)
        {
            return new DiagramRepository().InsertDiagram10(business);
        }
        public ResponseBase<int> InsertDiagram11(Diagram business)
        {
            return new DiagramRepository().InsertDiagram11(business);
        }
        public ResponseBase<int> InsertDiagram12(Diagram business)
        {
            return new DiagramRepository().InsertDiagram12(business);
        }
        public ResponseBase<int> InsertDiagram13(Diagram business)
        {
            return new DiagramRepository().InsertDiagram13(business);
        }
        public ResponseBase<int> InsertDiagram14(Diagram business)
        {
            return new DiagramRepository().InsertDiagram14(business);
        }
        public ResponseBase<int> InsertDiagram15(Diagram business)
        {
            return new DiagramRepository().InsertDiagram15(business);
        }
        public ResponseBase<int> InsertDiagram16(Diagram business)
        {
            return new DiagramRepository().InsertDiagram16(business);
        }
        public ResponseBase<int> InsertDiagram17(Diagram business)
        {
            return new DiagramRepository().InsertDiagram17(business);
        }
        public ResponseBase<int> InsertDiagram18(Diagram business)
        {
            return new DiagramRepository().InsertDiagram18(business);
        }
        public ResponseBase<int> InsertDiagram19(Diagram business)
        {
            return new DiagramRepository().InsertDiagram19(business);
        }
        public ResponseBase<int> InsertDiagram20(Diagram business)
        {
            return new DiagramRepository().InsertDiagram20(business);
        }
        public ResponseBase<int> InsertDiagram21(Diagram business)
        {
            return new DiagramRepository().InsertDiagram21(business);
        }

        //public ResponseBase<int> ProcessDiagramDTOList(IList<DiagramDTO> retList)
        //{
        //    throw new NotImplementedException();
        //}

        public ResponseBase<int> InsertDiagram22(Diagram business)
        {
            return new DiagramRepository().InsertDiagram22(business);
        }

        public ResponseBase<int> InsertDiagram23(Diagram business)
        {
            return new DiagramRepository().InsertDiagram23(business);
        }

        public ResponseBase<int> InsertDiagram24(Diagram business)
        {
            return new DiagramRepository().InsertDiagram24(business);
        }
        public ResponseBase<int> InsertDiagram25(Diagram business)
        {
            return new DiagramRepository().InsertDiagram25(business);
        }
        public ResponseBase<int> Update(Diagram business)
        {
            return new DiagramRepository().Update(business);
        }


        /*
        public ResponseBase<IList<Diagram>> GetByDIName(string diName)
        {
            return new DiagramRepository().GetByDIName(diName);
        }*/
    }
}
