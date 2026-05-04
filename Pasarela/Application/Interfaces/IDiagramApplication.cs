using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IDiagramApplication
    {
        ResponseBaseDTO<IList<DiagramDTO>> ObtenerTodos();
        ResponseBaseDTO<DiagramDTO> ObtenerPorId(string nombre, int id);
        ResponseBaseDTO<IList<DiagramDTO>> GetByDIModelName(string ModelName, int id);
        ResponseBaseDTO<int> InsertDiagram1(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram2(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram3(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram4(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram5(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram6(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram7(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram8(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram9(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram10(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram11(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram12(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram13(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram14(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram15(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram16(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram17(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram18(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram19(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram21(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram22(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram23(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram24(DiagramDTO obj);
        ResponseBaseDTO<int> InsertDiagram25(DiagramDTO obj);
        ResponseBaseDTO<int> Update(DiagramDTO obj);

    }
}
