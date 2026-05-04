using Microsoft.Data.SqlClient;
using System.Collections.Concurrent;

namespace Artez.AdReader;

public sealed class AdConfigReader
{
    private readonly AdReaderOptions _opt;

    public AdConfigReader(AdReaderOptions opt)
    {
        _opt = opt ?? throw new ArgumentNullException(nameof(opt));
        if (string.IsNullOrWhiteSpace(_opt.ConnectionString))
            throw new ArgumentException("ConnectionString vacío", nameof(opt));
    }

    public async Task<AdConfigSnapshot> LoadAsync(CancellationToken ct = default)
    {
        if (_opt.Tables.Count == 0)
            throw new InvalidOperationException("No hay tablas configuradas en AdReader:Tables.");

        var results = new ConcurrentDictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object?>>>();

        // Si quieres más concurrencia, cambia MaxDegreeOfParallelism (ojo con el SQL).
        await Parallel.ForEachAsync(_opt.Tables, new ParallelOptions
        {
            MaxDegreeOfParallelism = Math.Min(4, _opt.Tables.Count),
            CancellationToken = ct
        },
        async (table, token) =>
        {
            var rows = await LoadTableAsync(table, token);
            results[table.Name] = rows;
        });

        return new AdConfigSnapshot { Tables = results };
    }

    private async Task<IReadOnlyList<IReadOnlyDictionary<string, object?>>> LoadTableAsync(AdTableSpec table, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(table.Name)) throw new ArgumentException("TableSpec.Name vacío");
        var sql = string.IsNullOrWhiteSpace(table.Sql) ? $"SELECT * FROM {table.Name}" : table.Sql;

        await using var con = new SqlConnection(_opt.ConnectionString);
        await con.OpenAsync(ct);

        await using var cmd = con.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandTimeout = _opt.CommandTimeoutSeconds;

        await using var rdr = await cmd.ExecuteReaderAsync(ct);

        var list = new List<IReadOnlyDictionary<string, object?>>(capacity: 1024);
        while (await rdr.ReadAsync(ct))
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (var i = 0; i < rdr.FieldCount; i++)
            {
                var col = rdr.GetName(i);
                var val = await rdr.IsDBNullAsync(i, ct) ? null : rdr.GetValue(i);
                row[col] = val;
            }
            list.Add(row);
        }

        return list;
    }
}
