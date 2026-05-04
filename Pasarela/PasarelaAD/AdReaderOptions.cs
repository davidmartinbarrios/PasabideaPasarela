namespace Artez.AdReader;

public sealed class AdReaderOptions
{
    public string ConnectionString { get; init; } = "";
    public int CommandTimeoutSeconds { get; init; } = 60;
    public List<AdTableSpec> Tables { get; init; } = new();
}

public sealed class AdTableSpec
{
    public string Name { get; init; } = "";          // Ej: "dbo.AD_SERVICIOS"
    public string Sql { get; init; } = "";           // Ej: "SELECT * FROM dbo.AD_SERVICIOS WHERE ACTIVO=1"
}
