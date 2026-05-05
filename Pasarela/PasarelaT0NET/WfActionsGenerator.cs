using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Application.Services
{
    /// <summary>
    /// Generador mínimo y limpio de wfFlows/wfFlowActions desde las tablas intermedias de Pasarela.
    ///
    /// Objetivo práctico:
    ///   Procedimiento origen ya descargado desde Erwin/Corporate Modeler a tablas PASARELA
    ///   DIAGRAMA / ACCIONES_DI / PARAM_ACC / TBPFIN01_PA / TBDCELTA_PA
    ///   => generación de wfFlows y wfFlowActions en la BBDD destino MUGI/HidraNet.
    ///
    /// No intenta reimplementar toda la frmProceso.vb. Reproduce el corazón de msCrearFlujo:
    ///   1) localizar CODPROCE/CODEXTPR
    ///   2) crear/actualizar versión de flujo
    ///   3) insertar BEGIN
    ///   4) recorrer ACCIONES_DI ordenado
    ///   5) expandir cada acción a una o varias filas wfFlowActions
    ///   6) insertar END
    ///
    /// Uso recomendado desde Pasabidea:
    ///   var gen = new WfActionsGenerator();
    ///   var result = await gen.GenerarWfActionsAsync(new WfActionsGenerationRequest { ... });
    /// </summary>
    public sealed class WfActionsGenerator
    {
        public async Task<WfActionsGenerationResult> GenerarWfActionsAsync(
            WfActionsGenerationRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            request.Validate();

            var result = new WfActionsGenerationResult
            {
                Procedimiento = request.Procedimiento,
                StartedAt = DateTime.Now
            };

            using (var cnPasarela = new SqlConnection(request.PasarelaConnectionString))
            using (var cnDestino = new SqlConnection(request.DestinoConnectionString))
            using (var cnInfra = new SqlConnection(string.IsNullOrWhiteSpace(request.InfraConnectionString)
                       ? request.PasarelaConnectionString
                       : request.InfraConnectionString))
            {
                await cnPasarela.OpenAsync(cancellationToken).ConfigureAwait(false);
                await cnDestino.OpenAsync(cancellationToken).ConfigureAwait(false);
                await cnInfra.OpenAsync(cancellationToken).ConfigureAwait(false);

                using (var txDestino = cnDestino.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var proc = await LeerProcedimientoAsync(cnPasarela, request, cancellationToken).ConfigureAwait(false);
                        result.Flow = proc.Flow;
                        result.CodProce = proc.CodProce;

                        var version = request.VersionDestino > 0
                            ? request.VersionDestino
                            : await CalcularVersionDestinoAsync(cnDestino, txDestino, proc.Flow, request.NuevaVersion, cancellationToken).ConfigureAwait(false);

                        result.Version = version;

                        await ActualizarParametrosBaseAsync(cnPasarela, request, proc.Flow, version, cancellationToken).ConfigureAwait(false);

                        if (request.SobrescribirVersion)
                        {
                            await ExecuteAsync(cnDestino, txDestino,
                                @"DELETE FROM dbo.wfFlowActions WHERE Flow = @Flow AND Version = @Version;
                                  DELETE FROM dbo.wfFlows       WHERE Flow = @Flow AND Version = @Version;",
                                cancellationToken,
                                P("@Flow", proc.Flow), P("@Version", version)).ConfigureAwait(false);
                        }

                        await InsertarWfFlowAsync(cnDestino, txDestino, proc.Flow, version, proc.Comments, request.FechaActivacion, cancellationToken).ConfigureAwait(false);

                        var flowOrder = 0;
                        await InsertActionAsync(cnDestino, txDestino, proc.Flow, version, flowOrder, 0,
                            "BEGIN", "INICIO", "STATE", "INICIO", "Pasabidea / Pasarela .NET", cancellationToken).ConfigureAwait(false);
                        result.RowsInserted++;

                        var acciones = await LeerAccionesAsync(cnPasarela, request.Procedimiento, cancellationToken).ConfigureAwait(false);

                        var referenceCounter = 0;
                        foreach (var accion in acciones)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            flowOrder += 100;

                            var parametros = await LeerParametrosAccionAsync(cnPasarela, request.Procedimiento, accion, cancellationToken).ConfigureAwait(false);
                            var rows = await InsertarAccionExpandidaAsync(
                                cnDestino,
                                txDestino,
                                cnInfra,
                                cnPasarela,
                                request,
                                proc.Flow,
                                version,
                                flowOrder,
                                accion,
                                parametros,
                                ++referenceCounter,
                                cancellationToken).ConfigureAwait(false);

                            result.ActionsRead++;
                            result.RowsInserted += rows;
                        }

                        flowOrder += 100;
                        await InsertActionAsync(cnDestino, txDestino, proc.Flow, version, flowOrder, 0,
                            "END", "FIN " + proc.Flow, "FLUSHALL", "0", null, cancellationToken).ConfigureAwait(false);
                        await InsertActionAsync(cnDestino, txDestino, proc.Flow, version, flowOrder, 1,
                            "", "", "LEVEL", "1", null, cancellationToken).ConfigureAwait(false);
                        result.RowsInserted += 2;

                        if (!request.DryRun)
                        {
                            txDestino.Commit();
                        }
                        else
                        {
                            txDestino.Rollback();
                            result.DryRun = true;
                        }

                        result.Succeeded = true;
                    }
                    catch
                    {
                        TryRollback(txDestino);
                        throw;
                    }
                }
            }

            result.FinishedAt = DateTime.Now;
            return result;
        }

        private static async Task<ProcedimientoInfo> LeerProcedimientoAsync(SqlConnection cnPasarela, WfActionsGenerationRequest request, CancellationToken ct)
        {
            const string sql = @"
SELECT TOP (1)
       p.CODPROCE,
       LTRIM(RTRIM(p.CODEXTPR)) AS Flow,
       LTRIM(RTRIM(ISNULL(c.ELTA_DESC40, p.CODEXTPR))) AS Comments
FROM dbo.TBPFIN01_PA p
LEFT JOIN dbo.TBDCELTA_PA c
       ON c.PROCEDIMIENTO = p.PROCEDIMIENTO
      AND c.ELTA_CODTAB = 'P3'
      AND c.ELTA_IDIOMA = 'C'
WHERE p.PROCEDIMIENTO = @Procedimiento;";

            using (var cmd = new SqlCommand(sql, cnPasarela))
            {
                cmd.Parameters.AddWithValue("@Procedimiento", request.Procedimiento);
                using (var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    if (!await rd.ReadAsync(ct).ConfigureAwait(false))
                        throw new InvalidOperationException("No existe TBPFIN01_PA para el procedimiento " + request.Procedimiento);

                    return new ProcedimientoInfo
                    {
                        CodProce = ToInt(rd["CODPROCE"]),
                        Flow = ToStringTrim(rd["Flow"]),
                        Comments = ToStringTrim(rd["Comments"])
                    };
                }
            }
        }

        private static async Task<int> CalcularVersionDestinoAsync(SqlConnection cn, SqlTransaction tx, string flow, bool nuevaVersion, CancellationToken ct)
        {
            const string sql = "SELECT ISNULL(MAX(CONVERT(int, Version)), 0) FROM dbo.wfFlows WHERE Flow = @Flow;";
            var current = ToInt(await ScalarAsync(cn, tx, sql, ct, P("@Flow", flow)).ConfigureAwait(false));
            if (current <= 0) return 1;
            return nuevaVersion ? current + 1 : current;
        }

        private static async Task ActualizarParametrosBaseAsync(SqlConnection cnPasarela, WfActionsGenerationRequest request, string flow, int version, CancellationToken ct)
        {
            await ExecuteAsync(cnPasarela, null,
                @"UPDATE dbo.TBPFIN03_PA SET VERSIONP = @Version WHERE PROCEDIMIENTO = @Procedimiento;
                  UPDATE dbo.TBPFIN04_PA SET VERSIONP = @Version WHERE PROCEDIMIENTO = @Procedimiento;
                  UPDATE dbo.PARAM_ACC   SET VALOR = @VersionText WHERE PROCEDIMIENTO = @Procedimiento AND PARAMETRO = '@VERSIONP';
                  UPDATE dbo.PARAM_ACC   SET VALOR = @Flow        WHERE PROCEDIMIENTO = @Procedimiento AND PARAMETRO = '@FLUJO';
                  UPDATE dbo.PARAM_ACC   SET VALOR = @Project     WHERE PROCEDIMIENTO = @Procedimiento AND PARAMETRO = '@PROJECT';",
                ct,
                P("@Procedimiento", request.Procedimiento),
                P("@Version", version),
                P("@VersionText", version.ToString(CultureInfo.InvariantCulture)),
                P("@Flow", flow),
                P("@Project", request.ProjectDatabaseName ?? string.Empty)).ConfigureAwait(false);
        }

        private static async Task InsertarWfFlowAsync(SqlConnection cn, SqlTransaction tx, string flow, int version, string comments, DateTime fechaActivacion, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO dbo.wfFlows (Flow, Version, Active, FlowName, Comments, Running, Start, StopOlderVersions)
VALUES (@Flow, @Version, '1', @Flow, @Comments, '1', @Start, '0');";

            await ExecuteAsync(cn, tx, sql, ct,
                P("@Flow", flow),
                P("@Version", version),
                P("@Comments", comments ?? string.Empty),
                P("@Start", fechaActivacion.ToString("yyyyMMdd", CultureInfo.InvariantCulture))).ConfigureAwait(false);
        }

        private static async Task<List<AccionDi>> LeerAccionesAsync(SqlConnection cnPasarela, string procedimiento, CancellationToken ct)
        {
            const string sql = @"
SELECT A.PROCEDIMIENTO,
       A.ORDEN_N1, A.ORDEN_N2, A.ORDEN_N3, A.ORDEN_N4, A.ORDEN_N5,
       A.ORDEN_ACC,
       A.NOM_ACCION,
       A.NUM_ACCION,
       A.TIPO_ACCION,
       A.PATH_HIDRA,
       A.ID_ACCION,
       D.NOMBRE AS NOMBRE_DIAGRAMA
FROM dbo.ACCIONES_DI A
LEFT JOIN dbo.DIAGRAMA D
       ON D.PROCEDIMIENTO = A.PROCEDIMIENTO
      AND D.ORDEN_N1 = A.ORDEN_N1
      AND D.ORDEN_N2 = A.ORDEN_N2
      AND D.ORDEN_N3 = A.ORDEN_N3
      AND D.ORDEN_N4 = A.ORDEN_N4
      AND D.ORDEN_N5 = A.ORDEN_N5
WHERE A.PROCEDIMIENTO = @Procedimiento
  AND ISNULL(A.ORDEN_ACC, 0) <> 999
ORDER BY A.ORDEN_N1, A.ORDEN_N2, A.ORDEN_N3, A.ORDEN_N4, A.ORDEN_N5, A.ORDEN_ACC;";

            var list = new List<AccionDi>();
            using (var cmd = new SqlCommand(sql, cnPasarela))
            {
                cmd.Parameters.AddWithValue("@Procedimiento", procedimiento);
                using (var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    while (await rd.ReadAsync(ct).ConfigureAwait(false))
                    {
                        list.Add(new AccionDi
                        {
                            OrdenN1 = ToInt(rd["ORDEN_N1"]),
                            OrdenN2 = ToInt(rd["ORDEN_N2"]),
                            OrdenN3 = ToInt(rd["ORDEN_N3"]),
                            OrdenN4 = ToInt(rd["ORDEN_N4"]),
                            OrdenN5 = ToInt(rd["ORDEN_N5"]),
                            OrdenAcc = ToInt(rd["ORDEN_ACC"]),
                            NomAccion = ToStringTrim(rd["NOM_ACCION"]),
                            NumAccion = ToInt(rd["NUM_ACCION"]),
                            TipoAccion = ToStringTrim(rd["TIPO_ACCION"]),
                            PathHidra = ToStringTrim(rd["PATH_HIDRA"]),
                            IdAccion = rd["ID_ACCION"] == DBNull.Value ? (int?)null : ToInt(rd["ID_ACCION"]),
                            NombreDiagrama = ToStringTrim(rd["NOMBRE_DIAGRAMA"])
                        });
                    }
                }
            }
            return list;
        }

        private static async Task<List<ParametroAccion>> LeerParametrosAccionAsync(SqlConnection cnPasarela, string procedimiento, AccionDi accion, CancellationToken ct)
        {
            var sql = new StringBuilder(@"
SELECT ID_ACCION, PARAMETRO, VALOR, ORDEN_PA
FROM dbo.PARAM_ACC
WHERE PROCEDIMIENTO = @Procedimiento
  AND ORDEN_N1 = @N1 AND ORDEN_N2 = @N2 AND ORDEN_N3 = @N3 AND ORDEN_N4 = @N4 AND ORDEN_N5 = @N5
  AND ORDEN_ACC = @OrdenAcc");

            if (accion.IdAccion.HasValue)
                sql.Append(" AND ID_ACCION = @IdAccion");
            else
                sql.Append(" AND ID_ACCION IS NULL");

            sql.Append(" ORDER BY ORDEN_PA;");

            var list = new List<ParametroAccion>();
            using (var cmd = new SqlCommand(sql.ToString(), cnPasarela))
            {
                cmd.Parameters.AddWithValue("@Procedimiento", procedimiento);
                cmd.Parameters.AddWithValue("@N1", accion.OrdenN1);
                cmd.Parameters.AddWithValue("@N2", accion.OrdenN2);
                cmd.Parameters.AddWithValue("@N3", accion.OrdenN3);
                cmd.Parameters.AddWithValue("@N4", accion.OrdenN4);
                cmd.Parameters.AddWithValue("@N5", accion.OrdenN5);
                cmd.Parameters.AddWithValue("@OrdenAcc", accion.OrdenAcc);
                if (accion.IdAccion.HasValue) cmd.Parameters.AddWithValue("@IdAccion", accion.IdAccion.Value);

                using (var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    while (await rd.ReadAsync(ct).ConfigureAwait(false))
                    {
                        list.Add(new ParametroAccion
                        {
                            IdAccion = rd["ID_ACCION"] == DBNull.Value ? (int?)null : ToInt(rd["ID_ACCION"]),
                            Parametro = ToStringTrim(rd["PARAMETRO"]),
                            Valor = ToStringTrim(rd["VALOR"]),
                            Orden = ToInt(rd["ORDEN_PA"])
                        });
                    }
                }
            }
            return list;
        }

        private static async Task<int> InsertarAccionExpandidaAsync(
            SqlConnection cnDestino,
            SqlTransaction txDestino,
            SqlConnection cnInfra,
            SqlConnection cnPasarela,
            WfActionsGenerationRequest request,
            string flow,
            int version,
            int flowOrder,
            AccionDi accion,
            List<ParametroAccion> parametros,
            int referenceCounter,
            CancellationToken ct)
        {
            var path = NormalizarPath(accion.PathHidra);
            var nom = (accion.NomAccion ?? string.Empty).Trim();
            var prefix = Prefix(path);
            var rows = 0;
            var id = 0;
            var level = string.Equals(accion.NombreDiagrama, "ISFLY", StringComparison.OrdinalIgnoreCase) ? "3" : "2";

            // LABEL / carpeta de proceso. En VB aparece para PRO_ y carpetas FLY/ISFLY.
            if (prefix == "PRO" || nom.Equals("PROCESS", StringComparison.OrdinalIgnoreCase))
            {
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "LABEL", path, "STATE", path, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "SUBMIT", accion.NombreDiagrama == "FLY" ? "FLY" : "0", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 3;
            }

            if (prefix == "EDP" || nom.Equals("ENDPROC", StringComparison.OrdinalIgnoreCase))
            {
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "ENDPROC", path, "COMENTARIO", "", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 2;
            }

            if (prefix == "LET" || nom.StartsWith("LET", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var p in parametros)
                {
                    await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "LETVAR", path, p.Parametro, p.Valor, null, ct).ConfigureAwait(false);
                    rows++;
                }
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id + 1, "", "", "INICIO", "1", null, ct).ConfigureAwait(false);
                return rows + 2;
            }

            if (nom.Equals("JUMP", StringComparison.OrdinalIgnoreCase) || prefix == "JUM")
            {
                var destino = Valor(parametros, "PATH") ?? Valor(parametros, "@PATH") ?? Valor(parametros, "@PATHRETORNO") ?? path;
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "JUMP", path, "PATH", destino, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 2;
            }

            if (nom.Equals("WAITALL", StringComparison.OrdinalIgnoreCase))
            {
                var destino = Valor(parametros, "PATH") ?? Valor(parametros, "@PATHRETORNO") ?? path;
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "WAITALL", path, "PATH", destino, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "REQUIRED", "1", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 3;
            }

            if (nom.Equals("CANCELWAIT", StringComparison.OrdinalIgnoreCase))
            {
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "CANCELWAIT", path, "LEVEL", level, null, ct).ConfigureAwait(false);
                return 1;
            }

            if (nom.Equals("ALERT", StringComparison.OrdinalIgnoreCase) || prefix == "ALE")
            {
                var date = Valor(parametros, "DATE") ?? Valor(parametros, "@DATE") ?? Valor(parametros, "@FECHA") ?? string.Empty;
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "ALERT", path, "DATE", date, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "MATURATION", Valor(parametros, "MATURATION") ?? "1", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "INTERVAL", Valor(parametros, "INTERVAL") ?? "D", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "ALERTPATH", Valor(parametros, "ALERTPATH") ?? Valor(parametros, "@PATHRETORNO") ?? string.Empty, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "DAYSOFWEEK", Valor(parametros, "DAYSOFWEEK") ?? "LMXJVSD", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 6;
            }

            if (nom.Equals("FORMFICTICIO", StringComparison.OrdinalIgnoreCase) || nom.Equals("FORM", StringComparison.OrdinalIgnoreCase) || prefix == "FOR")
            {
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "LETVAR", path, "@USUARIO", "@INICIO!CODUSUAR", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "FORM", path, "FORM", Valor(parametros, "FORM") ?? "FORMULARIO FICTICIO", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "FILTER", Valor(parametros, "FILTER") ?? string.Empty, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "GROUP", Valor(parametros, "GROUP") ?? string.Empty, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "USER", Valor(parametros, "USER") ?? "USUARIOFICTICIO", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "WAIT", Valor(parametros, "WAIT") ?? "1", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "TO", Valor(parametros, "TO") ?? "ALL", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "ICON", Valor(parametros, "ICON") ?? string.Empty, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "@TITTLE", Valor(parametros, "@TITTLE") ?? "FORMULARIO FICTICIO", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                return 10;
            }

            // Caso principal de Pasarela: llamada a acción externa APE_* mediante APIINIENDEXT/APIINIEXT.
            // La BBDD destino se decide por ACCIONES.INDTIPOA igual que en frmProceso.vb.
            if (prefix == "APE" || !string.IsNullOrWhiteSpace(nom))
            {
                var project = await ResolverProyectoAccionAsync(cnInfra, request, accion, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "APIINIENDEXT", CortarPath(path), "PROJECT", project, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "FLOW", nom, null, ct).ConfigureAwait(false);

                var reference = "=$CONCAT([%REFERENCE%];[" + (request.ReferencePrefix ?? string.Empty) + referenceCounter.ToString(CultureInfo.InvariantCulture) + "])/$";
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "REFERENCE", reference, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "INIT", "1", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "LEVEL", level, null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "DEFERRED", "0", null, ct).ConfigureAwait(false);
                await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", "@CODIGOAPLICA", request.CodigoAplicacion ?? string.Empty, null, ct).ConfigureAwait(false);
                rows = 7;

                foreach (var p in parametros)
                {
                    var value = p.Parametro.Equals("@REFERENCIA", StringComparison.OrdinalIgnoreCase)
                        ? "%REFERENCE%"
                        : p.Parametro.Equals("REFERENCE", StringComparison.OrdinalIgnoreCase)
                            ? reference
                            : p.Valor;

                    await InsertActionAsync(cnDestino, txDestino, flow, version, flowOrder, id++, "", "", p.Parametro, value, null, ct).ConfigureAwait(false);
                    rows++;
                }

                return rows;
            }

            throw new NotSupportedException("Acción no soportada: " + accion.NomAccion + " / " + accion.PathHidra);
        }

        private static async Task<string> ResolverProyectoAccionAsync(SqlConnection cnInfra, WfActionsGenerationRequest request, AccionDi accion, CancellationToken ct)
        {
            const string sqlById = @"SELECT TOP (1) INDTIPOA FROM dbo.ACCIONES WHERE IDACCION = @IdAccion AND DESACCHN = @NomAccion;";
            const string sqlByName = @"SELECT TOP (1) INDTIPOA FROM dbo.ACCIONES WHERE DESACCHN = @NomAccion;";

            object raw;
            if (accion.IdAccion.HasValue && accion.IdAccion.Value != 0)
            {
                raw = await ScalarAsync(cnInfra, null, sqlById, ct, P("@IdAccion", accion.IdAccion.Value), P("@NomAccion", accion.NomAccion)).ConfigureAwait(false);
            }
            else
            {
                raw = await ScalarAsync(cnInfra, null, sqlByName, ct, P("@NomAccion", accion.NomAccion)).ConfigureAwait(false);
            }

            var tipo = ToStringTrim(raw);
            switch (tipo)
            {
                case "G": return request.DbGenerales;
                case "F": return request.DbGestion;
                case "E": return request.DbEspecificas;
                case "W": return request.DbWord;
                case "D": return request.DbDocumentos;
                case "A": return request.DbArranque;
                case "M": return request.DbMensajes;
                case "O": return request.DbInfra;
                default:  return request.ProjectDatabaseName ?? string.Empty;
            }
        }

        private static async Task InsertActionAsync(SqlConnection cn, SqlTransaction tx, string flow, int version, int flowOrder, int id,
            string action, string path, string param, string value, string comments, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO dbo.wfFlowActions (Flow, Version, FlowOrder, Id, Action, Path, Param, Value, Comments)
VALUES (@Flow, @Version, @FlowOrder, @Id, @Action, @Path, @Param, @Value, @Comments);";

            await ExecuteAsync(cn, tx, sql, ct,
                P("@Flow", flow),
                P("@Version", version),
                P("@FlowOrder", flowOrder),
                P("@Id", id),
                P("@Action", action ?? string.Empty),
                P("@Path", path ?? string.Empty),
                P("@Param", param ?? string.Empty),
                P("@Value", value ?? string.Empty),
                P("@Comments", comments ?? string.Empty)).ConfigureAwait(false);
        }

        private static async Task<int> ExecuteAsync(SqlConnection cn, SqlTransaction tx, string sql, CancellationToken ct, params SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                cmd.CommandTimeout = 0;
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                return await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }
        }

        private static async Task<object> ScalarAsync(SqlConnection cn, SqlTransaction tx, string sql, CancellationToken ct, params SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                cmd.CommandTimeout = 0;
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                return await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
            }
        }

        private static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        private static string Valor(IEnumerable<ParametroAccion> parametros, string nombre)
        {
            return parametros
                .Where(p => p.Parametro.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                .Select(p => p.Valor)
                .FirstOrDefault();
        }

        private static string Prefix(string path)
        {
            path = (path ?? string.Empty).Trim();
            return path.Length < 3 ? path.ToUpperInvariant() : path.Substring(0, 3).ToUpperInvariant();
        }

        private static string NormalizarPath(string path)
        {
            return (path ?? string.Empty).Trim();
        }

        private static string CortarPath(string path)
        {
            path = NormalizarPath(path);
            if (path.Length <= 25) return path;
            return path.Substring(0, 22) + path.Substring(23, Math.Min(3, path.Length - 23));
        }

        private static int ToInt(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            int parsed;
            return int.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out parsed)
                ? parsed
                : 0;
        }

        private static string ToStringTrim(object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.InvariantCulture).Trim();
        }

        private static void TryRollback(SqlTransaction tx)
        {
            try { tx.Rollback(); } catch { /* No ocultar la excepción original. */ }
        }

        private sealed class ProcedimientoInfo
        {
            public int CodProce { get; set; }
            public string Flow { get; set; }
            public string Comments { get; set; }
        }

        private sealed class AccionDi
        {
            public int OrdenN1 { get; set; }
            public int OrdenN2 { get; set; }
            public int OrdenN3 { get; set; }
            public int OrdenN4 { get; set; }
            public int OrdenN5 { get; set; }
            public int OrdenAcc { get; set; }
            public string NomAccion { get; set; }
            public int NumAccion { get; set; }
            public string TipoAccion { get; set; }
            public string PathHidra { get; set; }
            public int? IdAccion { get; set; }
            public string NombreDiagrama { get; set; }
        }

        private sealed class ParametroAccion
        {
            public int? IdAccion { get; set; }
            public string Parametro { get; set; }
            public string Valor { get; set; }
            public int Orden { get; set; }
        }
    }

    public sealed class WfActionsGenerationRequest
    {
        public string Procedimiento { get; set; }
        public string PasarelaConnectionString { get; set; }
        public string DestinoConnectionString { get; set; }
        public string InfraConnectionString { get; set; }

        public int VersionDestino { get; set; }
        public bool NuevaVersion { get; set; }
        public bool SobrescribirVersion { get; set; } = true;
        public bool DryRun { get; set; }
        public DateTime FechaActivacion { get; set; } = DateTime.Today;

        public string ProjectDatabaseName { get; set; }
        public string CodigoAplicacion { get; set; }
        public string ReferencePrefix { get; set; } = "";

        public string DbGenerales { get; set; }
        public string DbGestion { get; set; }
        public string DbEspecificas { get; set; }
        public string DbWord { get; set; }
        public string DbDocumentos { get; set; }
        public string DbArranque { get; set; }
        public string DbMensajes { get; set; }
        public string DbInfra { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Procedimiento)) throw new ArgumentException("Procedimiento obligatorio.");
            if (string.IsNullOrWhiteSpace(PasarelaConnectionString)) throw new ArgumentException("PasarelaConnectionString obligatoria.");
            if (string.IsNullOrWhiteSpace(DestinoConnectionString)) throw new ArgumentException("DestinoConnectionString obligatoria.");
        }
    }

    public sealed class WfActionsGenerationResult
    {
        public bool Succeeded { get; set; }
        public bool DryRun { get; set; }
        public string Procedimiento { get; set; }
        public string Flow { get; set; }
        public int CodProce { get; set; }
        public int Version { get; set; }
        public int ActionsRead { get; set; }
        public int RowsInserted { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
