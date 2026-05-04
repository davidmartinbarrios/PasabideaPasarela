namespace Artez.AdReader;

public sealed class AdConfigSnapshot
{
    // Tabla -> filas (cada fila = diccionario columna->valor)
    public required IReadOnlyDictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object?>>> Tables { get; init; }

    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Get(string tableName)
        => Tables.TryGetValue(tableName, out var rows) ? rows : Array.Empty<IReadOnlyDictionary<string, object?>>();
}
