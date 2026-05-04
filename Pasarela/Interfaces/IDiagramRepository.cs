using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IDiagramRepository
    {
        ResponseBase<IList<Diagram>> GetAll();
        ResponseBase<Diagram> GetByDIID(string ModelName, int id);
        ResponseBase<IList<Diagram>> GetByDIModelName(string ModelName, int id);
        ResponseBase<int> InsertDiagram1(Diagram obj);
        ResponseBase<int> InsertDiagram2(Diagram obj);
        ResponseBase<int> InsertDiagram3(Diagram obj);
        ResponseBase<int> InsertDiagram4(Diagram obj);
        ResponseBase<int> InsertDiagram5(Diagram obj);
        ResponseBase<int> InsertDiagram6(Diagram obj);
        ResponseBase<int> InsertDiagram7(Diagram obj);
        ResponseBase<int> InsertDiagram8(Diagram obj);
        ResponseBase<int> InsertDiagram9(Diagram obj);
        ResponseBase<int> InsertDiagram10(Diagram obj);
        ResponseBase<int> InsertDiagram11(Diagram obj);
        ResponseBase<int> InsertDiagram12(Diagram obj);
        ResponseBase<int> InsertDiagram13(Diagram obj);
        ResponseBase<int> InsertDiagram14(Diagram obj);
        ResponseBase<int> InsertDiagram15(Diagram obj);
        ResponseBase<int> InsertDiagram16(Diagram obj);
        ResponseBase<int> InsertDiagram17(Diagram obj);
        ResponseBase<int> InsertDiagram18(Diagram obj);
        ResponseBase<int> InsertDiagram19(Diagram obj);
        ResponseBase<int> InsertDiagram20(Diagram obj);
        ResponseBase<int> InsertDiagram21(Diagram obj);
        ResponseBase<int> InsertDiagram22(Diagram obj);
        ResponseBase<int> InsertDiagram23(Diagram obj);
        ResponseBase<int> InsertDiagram24(Diagram obj);
        ResponseBase<int> InsertDiagram25(Diagram obj);
        ResponseBase<int> InsertDiagram26(Diagram obj);
        ResponseBase<int> Update(Diagram obj);
    }
}
