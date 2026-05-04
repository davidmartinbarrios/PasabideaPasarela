using Lantik.Pasarela.Application.DTOs;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IBpmnExportApplication
    {
        ResponseBaseDTO<string> ExportarBpmnXml(string modelName, int diId);
    }
}