using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IDiagramBusiness
    {
        ResponseBase<IList<Diagram>> ObtenerTodos();
        ResponseBase<Diagram> ObtenerPorId(string nombre, int id);
        ResponseBase<IList<Diagram>> GetByDIModelName(string ModelName, int id);
        ResponseBase<int> InsertDiagramList(IList<Diagram> data);
        ResponseBase<int> InsertDiagram1(Diagram business);
        ResponseBase<int> InsertDiagram2(Diagram business);
        ResponseBase<int> InsertDiagram3(Diagram business);
        ResponseBase<int> InsertDiagram4(Diagram business);
        ResponseBase<int> InsertDiagram5(Diagram business);
        ResponseBase<int> InsertDiagram6(Diagram business);
        ResponseBase<int> InsertDiagram7(Diagram business);
        ResponseBase<int> InsertDiagram8(Diagram business);
        ResponseBase<int> InsertDiagram9(Diagram business);
        ResponseBase<int> InsertDiagram10(Diagram business);
        ResponseBase<int> InsertDiagram11(Diagram business);
        ResponseBase<int> InsertDiagram12(Diagram business);
        ResponseBase<int> InsertDiagram13(Diagram business);
        ResponseBase<int> InsertDiagram14(Diagram business);
        ResponseBase<int> InsertDiagram15(Diagram business);
        ResponseBase<int> InsertDiagram16(Diagram business);
        ResponseBase<int> InsertDiagram17(Diagram business);
        ResponseBase<int> InsertDiagram18(Diagram business);
        ResponseBase<int> InsertDiagram19(Diagram business);
        ResponseBase<int> InsertDiagram20(Diagram business);
        ResponseBase<int> InsertDiagram21(Diagram business);
        ResponseBase<int> InsertDiagram22(Diagram business);
        ResponseBase<int> InsertDiagram23(Diagram business);
        ResponseBase<int> InsertDiagram24(Diagram business);
        ResponseBase<int> InsertDiagram25(Diagram business);
        ResponseBase<int> Update(Diagram business);
    }
}
