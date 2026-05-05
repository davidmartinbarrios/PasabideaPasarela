using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Artez.AdReader
{
    public sealed class AdConfigReader
    {
        private readonly AdReaderOptions _opt;

        public AdConfigReader(AdReaderOptions opt)
        {
            _opt = opt ?? throw new ArgumentNullException(nameof(opt));
            if (string.IsNullOrWhiteSpace(_opt.ConnectionString))
                throw new ArgumentException("ConnectionString vacio", nameof(opt));
        }

        public async Task<AdConfigSnapshot> LoadAsync(CancellationToken ct = default(CancellationToken))
        {
            if (_opt.Tables.Count == 0)
                throw new InvalidOperationException("No hay tablas configuradas en AdReader:Tables.");

            var results = new ConcurrentDictionary<string, IReadOnlyList<IReadOnlyDictionary<string, object>>>();
            var maxDegreeOfParallelism = Math.Min(4, _opt.Tables.Count);

            using (var semaphore = new SemaphoreSlim(maxDegreeOfParallelism))
            {
                var tasks = _opt.Tables.Select(async table =>
                {
                    await semaphore.WaitAsync(ct).ConfigureAwait(false);
                    try
                    {
                        var rows = await LoadTableAsync(table, ct).ConfigureAwait(false);
                        results[table.Name] = rows;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }

            return new AdConfigSnapshot { Tables = results };
        }

        private async Task<IReadOnlyList<IReadOnlyDictionary<string, object>>> LoadTableAsync(AdTableSpec table, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(table.Name))
                throw new ArgumentException("TableSpec.Name vacio");

            var sql = string.IsNullOrWhiteSpace(table.Sql) ? $"SELECT * FROM {table.Name}" : table.Sql;

            using (var con = new SqlConnection(_opt.ConnectionString))
            {
                await con.OpenAsync(ct).ConfigureAwait(false);

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = _opt.CommandTimeoutSeconds;

                    using (var rdr = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                    {
                        var list = new List<IReadOnlyDictionary<string, object>>(capacity: 1024);
                        while (await rdr.ReadAsync(ct).ConfigureAwait(false))
                        {
                            var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                            for (var i = 0; i < rdr.FieldCount; i++)
                            {
                                var col = rdr.GetName(i);
                                var val = await rdr.IsDBNullAsync(i, ct).ConfigureAwait(false) ? null : rdr.GetValue(i);
                                row[col] = val;
                            }

                            list.Add(row);
                        }

                        return list;
                    }
                }
            }
        }
    }
}
