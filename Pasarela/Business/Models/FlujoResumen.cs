namespace Pasabidea.Business.Models;

public sealed class FlujoResumen
{
    public string BaseDatos { get; set; } = string.Empty;
    public string Flow { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Comments { get; set; }
}
