using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Pasabidea.Business.Models;
using Pasabidea.Interfaces.Repositories;

namespace Pasabidea.Infrastructure.Repositories
{
    public sealed class FlujosRepository : IFlujosRepository
    {
        private readonly string _connectionString;

        public FlujosRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La cadena de conexion no puede estar vacia.", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public async Task<IReadOnlyList<FlujoResumen>> ObtenerUltimasVersionesAsync(
            string patronBaseDatos,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(patronBaseDatos))
            {
                patronBaseDatos = "DBN8%";
            }

            var resultado = new List<FlujoResumen>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                var sql = BuildSql();

                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 120;

                    command.Parameters.Add(new SqlParameter("@PatronBaseDatos", SqlDbType.NVarChar, 128)
                    {
                        Value = patronBaseDatos
                    });

                    using (var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                    {
                        var ordinalBaseDatos = reader.GetOrdinal("BaseDatos");
                        var ordinalFlow = reader.GetOrdinal("Flow");
                        var ordinalVersion = reader.GetOrdinal("Version");
                        var ordinalComments = reader.GetOrdinal("Comments");

                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            resultado.Add(new FlujoResumen
                            {
                                BaseDatos = reader.IsDBNull(ordinalBaseDatos) ? string.Empty : reader.GetString(ordinalBaseDatos),
                                Flow = reader.IsDBNull(ordinalFlow) ? string.Empty : Convert.ToString(reader.GetValue(ordinalFlow)) ?? string.Empty,
                                Version = reader.IsDBNull(ordinalVersion) ? string.Empty : Convert.ToString(reader.GetValue(ordinalVersion)) ?? string.Empty,
                                Comments = reader.IsDBNull(ordinalComments) ? null : Convert.ToString(reader.GetValue(ordinalComments))
                            });
                        }
                    }
                }
            }

            return resultado;
        }

        private static string BuildSql()
        {
            return @"
DECLARE @sql nvarchar(max) = N'';

SELECT @sql = @sql +
    CASE WHEN @sql = N'' THEN N'' ELSE CHAR(13) + N'UNION ALL' + CHAR(13) END +
    N'SELECT
          ' + QUOTENAME(d.name, '''') + N' AS BaseDatos,
          Flow,
          Version,
          Comments
      FROM ' + QUOTENAME(d.name) + N'.dbo.wfFlows'
FROM sys.databases d
WHERE d.name LIKE @PatronBaseDatos
  AND d.state_desc = 'ONLINE'
  AND HAS_DBACCESS(d.name) = 1
  AND OBJECT_ID(d.name + '.dbo.wfFlows') IS NOT NULL;

IF @sql IS NULL OR LEN(@sql) = 0
BEGIN
    SELECT
        CAST(NULL AS sysname) AS BaseDatos,
        CAST(NULL AS varchar(255)) AS Flow,
        CAST(NULL AS varchar(50)) AS Version,
        CAST(NULL AS varchar(max)) AS Comments
    WHERE 1 = 0;
    RETURN;
END;

SET @sql = N'
WITH Flujos AS (
' + @sql + N'
),
FlujosRanked AS (
    SELECT
        BaseDatos,
        Flow,
        Version,
        Comments,
        ROW_NUMBER() OVER (
            PARTITION BY BaseDatos, Flow
            ORDER BY TRY_CONVERT(int, Version) DESC, Version DESC
        ) AS rn
    FROM Flujos
    WHERE Flow IS NOT NULL
      AND LTRIM(RTRIM(Flow)) <> ''''
)
SELECT
    BaseDatos,
    Flow,
    Version,
    Comments
FROM FlujosRanked
WHERE rn = 1
ORDER BY BaseDatos, Flow;';

EXEC sp_executesql @sql;";
        }
    }
}
