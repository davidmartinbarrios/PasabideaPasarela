using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Lantik.Pasabidea.Core.Business
{
    /// <summary>
    /// Genera filas dbo.wfFlowActions a partir de las tablas intermedias PASARELA_ARTEZ:
    /// DIAGRAMA, PROPIEDADES_DI, ACCIONES_DI, CONECTOR_ACC y PARAM_ACC.
    ///
    /// Objetivo: poder probar ya la traducción ERWIN/PASARELA_ARTEZ -> MUGI sin depender todavía del XML completo.
    /// No inserta en BD. Devuelve las acciones para que la capa Data haga Insert/Delete transaccional.
    /// </summary>
    public sealed class PasarelaArtezWfFlowActionsBuilder
    {
        private const int VersionDefault = 3;
        private const int FlowOrderStep = 100;
        private const string AplicacionDefault = "ARTEZ";

        public IList<MugiWfAction> Generar(PasarelaArtezProcedure procedimiento, WfBuildOptions options)
        {
            if (procedimiento == null) throw new ArgumentNullException("procedimiento");
            if (options == null) throw new ArgumentNullException("options");
            if (string.IsNullOrWhiteSpace(options.Flow)) throw new ArgumentException("Flow obligatorio", "options.Flow");

            var ctx = new BuildContext(options.Flow, options.Version <= 0 ? VersionDefault : options.Version);
            var output = new List<MugiWfAction>();

            AddInicioFlujo(output, ctx);
            AddInicioTramitacion(output, ctx, options);

            foreach (var tramite in procedimiento.Diagramas
                         .OrderBy(x => x.OrdenN1).ThenBy(x => x.OrdenN2).ThenBy(x => x.OrdenN3)
                         .ThenBy(x => x.OrdenN4).ThenBy(x => x.OrdenN5))
            {
                var props = procedimiento.Propiedades.FirstOrDefault(p => p.MismaClaveJerarquica(tramite));
                var acciones = procedimiento.Acciones
                    .Where(a => a.MismaClaveJerarquica(tramite))
                    .OrderBy(a => a.OrdenAcc)
                    .ToList();

                AddLabelTramite(output, ctx, tramite, props);
                AddPreAccionesTramite(output, ctx, tramite, props, options);

                foreach (var accion in acciones)
                {
                    var parametros = procedimiento.Parametros
                        .Where(p => p.MismaClaveAccion(accion))
                        .OrderBy(p => p.OrdenPa)
                        .ToList();

                    AddAccionIntermedia(output, ctx, accion, parametros, options);
                }

                AddSalidasTramite(output, ctx, procedimiento, tramite);
                AddPostAccionesTramite(output, ctx, tramite, props, options);
            }

            AddFinalizacionTramitacion(output, ctx, options);
            AddFinFlujo(output, ctx);

            return output;
        }

        public PasarelaArtezProcedure FromDataTables(
            string procedimiento,
            DataTable diagrama,
            DataTable propiedadesDi,
            DataTable accionesDi,
            DataTable conectorAcc,
            DataTable paramAcc)
        {
            return new PasarelaArtezProcedure
            {
                Procedimiento = procedimiento,
                Diagramas = diagrama == null ? new List<DiagramaRow>() : diagrama.Rows.Cast<DataRow>().Select(DiagramaRow.From).ToList(),
                Propiedades = propiedadesDi == null ? new List<PropiedadesDiRow>() : propiedadesDi.Rows.Cast<DataRow>().Select(PropiedadesDiRow.From).ToList(),
                Acciones = accionesDi == null ? new List<AccionesDiRow>() : accionesDi.Rows.Cast<DataRow>().Select(AccionesDiRow.From).ToList(),
                Conectores = conectorAcc == null ? new List<ConectorAccRow>() : conectorAcc.Rows.Cast<DataRow>().Select(ConectorAccRow.From).ToList(),
                Parametros = paramAcc == null ? new List<ParamAccRow>() : paramAcc.Rows.Cast<DataRow>().Select(ParamAccRow.From).ToList()
            };
        }

        private static void AddInicioFlujo(ICollection<MugiWfAction> output, BuildContext ctx)
        {
            ctx.FlowOrder = 0;
            Add(output, ctx, 0, "BEGIN", "INICIO", "STATE", "INICIO", "Definición Pasarela");
        }

        private static void AddInicioTramitacion(ICollection<MugiWfAction> output, BuildContext ctx, WfBuildOptions options)
        {
            ctx.NextFlowOrder();
            AddActionWithParams(output, ctx, "LABEL", "INICIO_TRAMITACION", "", "", "",
                Params(
                    Pair("STATE", "INICIO_TRAMITACION"),
                    Pair("SUBMIT", "0"),
                    Pair("LEVEL", "1")));

            ctx.NextFlowOrder();
            ctx.ContadorReferencias++;
            AddApiIniEndExt(output, ctx, "INICIAR_TRAMITACION", options.BaseDatosInfraestructura,
                "INICIAR_TRAMITACION", options.BaseDatosDestino, options.PathRetornoInicio, false,
                new List<KeyValuePair<string, string>>());
        }

        private static void AddLabelTramite(ICollection<MugiWfAction> output, BuildContext ctx, DiagramaRow tramite, PropiedadesDiRow props)
        {
            ctx.NextFlowOrder();
            string nombre = FirstNotEmpty(props == null ? null : props.NomDiagrama, tramite.Nombre, tramite.NombreTram);
            string path = NormalizePath(nombre);

            AddActionWithParams(output, ctx, "LABEL", path, "", "", nombre,
                Params(
                    Pair("STATE", path),
                    Pair("SUBMIT", "0"),
                    Pair("LEVEL", "1")));
        }

        private static void AddPreAccionesTramite(ICollection<MugiWfAction> output, BuildContext ctx, DiagramaRow tramite, PropiedadesDiRow props, WfBuildOptions options)
        {
            if (EsUnionRamas(tramite, props))
            {
                ctx.NextFlowOrder();
                ctx.ContadorUnions++;
                AddActionWithParams(output, ctx, "WAITALL", "UNION_" + ctx.ContadorUnions.ToString("0000"), "", "", "Unión ramas paralelas",
                    Params(Pair("NUMBRANCHES", "2"), Pair("LEVEL", "2")));
                return;
            }

            if (TienePlazo(tramite, props))
            {
                ctx.NextFlowOrder();
                ctx.ContadorReferencias++;
                AddApiIniEndExt(output, ctx, "OBTENER_PLAZO_" + NormalizePath(tramite.Nombre), options.BaseDatosInfraestructura,
                    "OBTENER_PLAZO", options.BaseDatosDestino, "ALERT_" + NormalizePath(tramite.Nombre), false,
                    Params(Pair("@ORDENTRA", tramite.OrdenN1.ToString(CultureInfo.InvariantCulture))));

                ctx.NextFlowOrder();
                AddActionWithParams(output, ctx, "ALERT", "ALERT_" + NormalizePath(tramite.Nombre), "", "", "Alerta de plazo",
                    Params(Pair("LEVEL", "2")));
            }

            if (EsTramiteManual(props))
            {
                ctx.NextFlowOrder();
                AddActionWithParams(output, ctx, "FORM", "FORMFICTICIO_" + NormalizePath(tramite.Nombre), "", "", "Parada trámite manual",
                    Params(Pair("FORM", "FORMFICTICIO"), Pair("LEVEL", "2")));
            }
        }

        private static void AddAccionIntermedia(ICollection<MugiWfAction> output, BuildContext ctx, AccionesDiRow accion, IList<ParamAccRow> parametros, WfBuildOptions options)
        {
            ctx.NextFlowOrder();

            string nombre = FirstNotEmpty(accion.NomAccion, accion.Nombre, accion.PathHidra);
            string normalized = (nombre ?? string.Empty).Trim().ToUpperInvariant();
            string path = FirstNotEmpty(accion.PathHidra, NormalizePath(nombre));
            var paramsAccion = parametros.Select(p => Pair(p.Parametro, p.Valor)).ToList();

            if (normalized.Contains("LET") || normalized.Contains("INICIALIZACIONVAR"))
            {
                AddActionWithParams(output, ctx, "LETVAR", path, "", "", nombre, NormalizeParams(paramsAccion));
                return;
            }

            if (normalized == "JUMP" || normalized.StartsWith("JUMP "))
            {
                ctx.ContadorJumps++;
                AddActionWithParams(output, ctx, "JUMP", path, "", "", nombre, NormalizeParams(paramsAccion));
                return;
            }

            if (normalized == "IF" || normalized.StartsWith("IF_"))
            {
                AddActionWithParams(output, ctx, "IF", path, "", "", nombre, NormalizeParams(paramsAccion));
                return;
            }

            if (normalized == "FORM" || normalized.Contains("FORMFICTICIO"))
            {
                AddActionWithParams(output, ctx, "FORM", path, "", "", nombre, NormalizeParams(paramsAccion));
                return;
            }

            if (normalized == "ENDPROC" || normalized == "PROCESS" || normalized == "CANCEL" || normalized == "ALERT" || normalized == "WAITALL")
            {
                AddActionWithParams(output, ctx, normalized == "PROCESS" ? "ENDPROC" : normalized, path, "", "", nombre, NormalizeParams(paramsAccion));
                return;
            }

            ctx.ContadorReferencias++;
            AddApiIniEndExt(output, ctx, path, ResolveBaseDatosAccion(accion, options), nombre,
                options.BaseDatosDestino, ResolvePathRetorno(paramsAccion, options), false, paramsAccion);
        }

        private static void AddSalidasTramite(ICollection<MugiWfAction> output, BuildContext ctx, PasarelaArtezProcedure procedimiento, DiagramaRow tramite)
        {
            var salidas = procedimiento.Conectores
                .Where(c => c.IdDiagrama == tramite.IdDiagrama && c.NumSeqDesde == tramite.NumSeq && c.IndSalidaTram == "S")
                .OrderBy(c => c.NumConector)
                .ToList();

            if (salidas.Count == 0 && string.IsNullOrWhiteSpace(tramite.Salidas)) return;

            if (salidas.Count == 0)
            {
                ctx.NextFlowOrder();
                ctx.ContadorJumps++;
                AddActionWithParams(output, ctx, "JUMP", "JUMP_" + ctx.ContadorJumps.ToString("0000"), "PATH", NormalizePath(tramite.Salidas), "Salida trámite", new List<KeyValuePair<string, string>>());
                return;
            }

            foreach (var salida in salidas)
            {
                string pathDestino = FirstNotEmpty(salida.CatConector2, "SEQ_" + salida.NumSeqHasta.ToString(CultureInfo.InvariantCulture));
                ctx.NextFlowOrder();
                ctx.ContadorJumps++;
                AddActionWithParams(output, ctx, "JUMP", "JUMP_" + ctx.ContadorJumps.ToString("0000"), "PATH", NormalizePath(pathDestino), "Salida trámite", new List<KeyValuePair<string, string>>());
            }
        }

        private static void AddPostAccionesTramite(ICollection<MugiWfAction> output, BuildContext ctx, DiagramaRow tramite, PropiedadesDiRow props, WfBuildOptions options)
        {
            if (!TienePlazo(tramite, props)) return;

            ctx.NextFlowOrder();
            ctx.ContadorReferencias++;
            AddApiIniEndExt(output, ctx, "CADUCAR_TAREA_" + NormalizePath(tramite.Nombre), options.BaseDatosInfraestructura,
                "CADUCAR_TAREA", options.BaseDatosDestino, options.PathRetornoFin, false,
                Params(Pair("@ORDENTRA", tramite.OrdenN1.ToString(CultureInfo.InvariantCulture))));

            ctx.NextFlowOrder();
            AddActionWithParams(output, ctx, "CANCEL", "CANCEL_" + NormalizePath(tramite.Nombre), "", "", "Cancelación por caducidad", Params(Pair("LEVEL", "2")));
        }

        private static void AddFinalizacionTramitacion(ICollection<MugiWfAction> output, BuildContext ctx, WfBuildOptions options)
        {
            ctx.NextFlowOrder();
            ctx.ContadorReferencias++;
            AddApiIniEndExt(output, ctx, "FINALIZAR_TRAMITACION", options.BaseDatosInfraestructura,
                "FINALIZAR_TRAMITACION", options.BaseDatosDestino, options.PathRetornoFin, false,
                new List<KeyValuePair<string, string>>());
        }

        private static void AddFinFlujo(ICollection<MugiWfAction> output, BuildContext ctx)
        {
            ctx.NextFlowOrder();
            Add(output, ctx, 0, "END", "FIN", "STATE", "FIN", "Fin flujo");
        }

        private static void AddApiIniEndExt(ICollection<MugiWfAction> output, BuildContext ctx, string path, string baseDatosAccion, string accion,
            string baseDatosRetorno, string pathRetorno, bool diferido, IList<KeyValuePair<string, string>> parametrosEspecificos)
        {
            var parametros = Params(
                Pair("FLOW", accion),
                Pair("BBDD", baseDatosAccion),
                Pair("APPLICATION", AplicacionDefault),
                Pair("REFERENCE", ctx.ContadorReferencias.ToString(CultureInfo.InvariantCulture)),
                Pair("RETURNBBDD", baseDatosRetorno),
                Pair("RETURNPATH", pathRetorno),
                Pair("DEFERRED", diferido ? "1" : "0"),
                Pair("LEVEL", "2"));

            foreach (var p in parametrosEspecificos ?? new List<KeyValuePair<string, string>>())
            {
                if (!string.IsNullOrWhiteSpace(p.Key)) parametros.Add(p);
            }

            AddActionWithParams(output, ctx, "APIINIENDEXT", path, "", "", accion, NormalizeParams(parametros));
        }

        private static void AddActionWithParams(ICollection<MugiWfAction> output, BuildContext ctx, string action, string path, string param, string value, string comments, IList<KeyValuePair<string, string>> parametros)
        {
            Add(output, ctx, 0, action, path, param, value, comments);

            int id = 1;
            foreach (var p in parametros ?? new List<KeyValuePair<string, string>>())
            {
                if (string.IsNullOrWhiteSpace(p.Key)) continue;
                Add(output, ctx, id++, string.Empty, string.Empty, p.Key.Trim(), NullToEmpty(p.Value), comments);
            }
        }

        private static void Add(ICollection<MugiWfAction> output, BuildContext ctx, int id, string action, string path, string param, string value, string comments)
        {
            output.Add(new MugiWfAction
            {
                Flow = ctx.Flow,
                Version = ctx.Version,
                FlowOrder = ctx.FlowOrder,
                Id = id,
                Action = NullToEmpty(action),
                Path = NullToEmpty(path),
                Param = NullToEmpty(param),
                Value = NullToEmpty(value),
                Comments = NullToEmpty(comments)
            });
        }

        private static IList<KeyValuePair<string, string>> NormalizeParams(IList<KeyValuePair<string, string>> source)
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (var p in source ?? new List<KeyValuePair<string, string>>())
            {
                if (string.IsNullOrWhiteSpace(p.Key)) continue;
                result.Add(Pair(NormalizeParamName(p.Key), p.Value));
            }
            return result;
        }

        private static string NormalizeParamName(string value)
        {
            value = NullToEmpty(value).Trim();
            return value.StartsWith("@", StringComparison.Ordinal) ? value : value.ToUpperInvariant();
        }

        private static IList<KeyValuePair<string, string>> Params(params KeyValuePair<string, string>[] values)
        {
            return values == null ? new List<KeyValuePair<string, string>>() : values.ToList();
        }

        private static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(NullToEmpty(key), NullToEmpty(value));
        }

        private static string ResolveBaseDatosAccion(AccionesDiRow accion, WfBuildOptions options)
        {
            if (!string.IsNullOrWhiteSpace(accion.BaseDatosAccion)) return accion.BaseDatosAccion;
            if (string.Equals(accion.TipoAccion, "T", StringComparison.OrdinalIgnoreCase)) return options.BaseDatosInfraestructura;
            if (string.Equals(accion.TipoAccion, "G", StringComparison.OrdinalIgnoreCase)) return options.BaseDatosInfraestructura;
            return options.BaseDatosAccionesEspecificas;
        }

        private static string ResolvePathRetorno(IList<KeyValuePair<string, string>> parametros, WfBuildOptions options)
        {
            var retorno = parametros == null ? default(KeyValuePair<string, string>) : parametros.FirstOrDefault(p => string.Equals(p.Key, "@PATHRETORNO", StringComparison.OrdinalIgnoreCase));
            return string.IsNullOrWhiteSpace(retorno.Value) ? options.PathRetornoFin : retorno.Value;
        }

        private static bool EsUnionRamas(DiagramaRow d, PropiedadesDiRow p)
        {
            return string.Equals(d.UnionRamas, "S", StringComparison.OrdinalIgnoreCase)
                   || Contains(p == null ? null : p.TipoDiagrama, "UNION")
                   || Contains(d.Nombre, "UNION");
        }

        private static bool TienePlazo(DiagramaRow d, PropiedadesDiRow p)
        {
            return !string.IsNullOrWhiteSpace(d.PlazoTipo1)
                   || !string.IsNullOrWhiteSpace(d.PlazoTipo2)
                   || !string.IsNullOrWhiteSpace(p == null ? null : p.PlazTip1Di)
                   || !string.IsNullOrWhiteSpace(p == null ? null : p.PlazTip2Di);
        }

        private static bool EsTramiteManual(PropiedadesDiRow p)
        {
            return Contains(p == null ? null : p.TipoDiagrama, "MANUAL")
                   || string.Equals(p == null ? null : p.TipoDiagrama, "Trámite", StringComparison.OrdinalIgnoreCase);
        }

        private static bool Contains(string text, string value)
        {
            return !string.IsNullOrWhiteSpace(text) && text.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string NormalizePath(string value)
        {
            value = NullToEmpty(value).Trim();
            if (value.Length == 0) return string.Empty;
            var chars = value.ToUpperInvariant().Select(c => char.IsLetterOrDigit(c) ? c : '_').ToArray();
            var normalized = new string(chars);
            while (normalized.Contains("__")) normalized = normalized.Replace("__", "_");
            return normalized.Trim('_');
        }

        private static string FirstNotEmpty(params string[] values)
        {
            if (values == null) return string.Empty;
            foreach (var v in values)
                if (!string.IsNullOrWhiteSpace(v)) return v.Trim();
            return string.Empty;
        }

        private static string NullToEmpty(string value)
        {
            return value ?? string.Empty;
        }

        private sealed class BuildContext
        {
            public BuildContext(string flow, int version)
            {
                Flow = flow;
                Version = version;
            }

            public string Flow { get; private set; }
            public int Version { get; private set; }
            public int FlowOrder { get; set; }
            public int ContadorReferencias { get; set; }
            public int ContadorJumps { get; set; }
            public int ContadorUnions { get; set; }

            public void NextFlowOrder()
            {
                FlowOrder += FlowOrderStep;
            }
        }
    }

    public sealed class WfBuildOptions
    {
        public string Flow { get; set; }
        public int Version { get; set; }
        public string BaseDatosInfraestructura { get; set; }
        public string BaseDatosAccionesEspecificas { get; set; }
        public string BaseDatosDestino { get; set; }
        public string PathRetornoInicio { get; set; }
        public string PathRetornoFin { get; set; }
    }

    public sealed class MugiWfAction
    {
        public string Flow { get; set; }
        public int Version { get; set; }
        public int FlowOrder { get; set; }
        public int Id { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public string Param { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }
    }

    public sealed class PasarelaArtezProcedure
    {
        public string Procedimiento { get; set; }
        public IList<DiagramaRow> Diagramas { get; set; }
        public IList<PropiedadesDiRow> Propiedades { get; set; }
        public IList<AccionesDiRow> Acciones { get; set; }
        public IList<ConectorAccRow> Conectores { get; set; }
        public IList<ParamAccRow> Parametros { get; set; }
    }

    public abstract class JerarquiaRow : DataRowMappingBase
    {
        public string Procedimiento { get; set; }
        public int OrdenN1 { get; set; }
        public int OrdenN2 { get; set; }
        public int OrdenN3 { get; set; }
        public int OrdenN4 { get; set; }
        public int OrdenN5 { get; set; }

        public bool MismaClaveJerarquica(JerarquiaRow other)
        {
            return other != null
                   && string.Equals(Procedimiento, other.Procedimiento, StringComparison.OrdinalIgnoreCase)
                   && OrdenN1 == other.OrdenN1 && OrdenN2 == other.OrdenN2 && OrdenN3 == other.OrdenN3
                   && OrdenN4 == other.OrdenN4 && OrdenN5 == other.OrdenN5;
        }
    }

    public sealed class DiagramaRow : JerarquiaRow
    {
        public int IdDiagrama { get; set; }
        public int IdPadre { get; set; }
        public int NumSeq { get; set; }
        public string Nombre { get; set; }
        public string Salidas { get; set; }
        public string PlazoTipo1 { get; set; }
        public string PlazoTipo2 { get; set; }
        public string UnionRamas { get; set; }
        public string NombreTram { get; set; }

        public static DiagramaRow From(DataRow r)
        {
            return new DiagramaRow
            {
                Procedimiento = S(r, "PROCEDIMIENTO"), OrdenN1 = I(r, "ORDEN_N1"), OrdenN2 = I(r, "ORDEN_N2"), OrdenN3 = I(r, "ORDEN_N3"), OrdenN4 = I(r, "ORDEN_N4"), OrdenN5 = I(r, "ORDEN_N5"),
                IdDiagrama = I(r, "ID_DIAGRAMA"), IdPadre = I(r, "ID_PADRE"), NumSeq = I(r, "NUM_SEQ"), Nombre = S(r, "NOMBRE"), Salidas = S(r, "SALIDAS"),
                PlazoTipo1 = S(r, "PLAZOTIPO1"), PlazoTipo2 = S(r, "PLAZOTIPO2"), UnionRamas = S(r, "UNION_RAMAS"), NombreTram = S(r, "NOMBRE_TRAM")
            };
        }
    }

    public sealed class PropiedadesDiRow : JerarquiaRow
    {
        public int IdDiagrama { get; set; }
        public string NomDiagrama { get; set; }
        public string TipoDiagrama { get; set; }
        public string PlazTip1Di { get; set; }
        public string PlazTip2Di { get; set; }

        public static PropiedadesDiRow From(DataRow r)
        {
            return new PropiedadesDiRow
            {
                Procedimiento = S(r, "PROCEDIMIENTO"), OrdenN1 = I(r, "ORDEN_N1"), OrdenN2 = I(r, "ORDEN_N2"), OrdenN3 = I(r, "ORDEN_N3"), OrdenN4 = I(r, "ORDEN_N4"), OrdenN5 = I(r, "ORDEN_N5"),
                IdDiagrama = I(r, "ID_DIAGRAMA"), NomDiagrama = S(r, "NOM_DIAGRAMA"), TipoDiagrama = S(r, "TIPO_DIAGRAMA"), PlazTip1Di = S(r, "PLAZTIP1_DI"), PlazTip2Di = S(r, "PLAZTIP2_DI")
            };
        }
    }

    public sealed class AccionesDiRow : JerarquiaRow
    {
        public int OrdenAcc { get; set; }
        public int IdAccion { get; set; }
        public string NomAccion { get; set; }
        public string TipoAccion { get; set; }
        public string PathHidra { get; set; }
        public int NumSeq { get; set; }
        public int DiId { get; set; }
        public string Nombre { get; set; }
        public string BaseDatosAccion { get; set; }

        public static AccionesDiRow From(DataRow r)
        {
            return new AccionesDiRow
            {
                Procedimiento = S(r, "PROCEDIMIENTO"), OrdenN1 = I(r, "ORDEN_N1"), OrdenN2 = I(r, "ORDEN_N2"), OrdenN3 = I(r, "ORDEN_N3"), OrdenN4 = I(r, "ORDEN_N4"), OrdenN5 = I(r, "ORDEN_N5"),
                OrdenAcc = I(r, "ORDEN_ACC"), IdAccion = I(r, "ID_ACCION"), NomAccion = S(r, "NOM_ACCION"), TipoAccion = S(r, "TIPO_ACCION"), PathHidra = S(r, "PATH_HIDRA"), NumSeq = I(r, "NUM_SEQ"), DiId = I(r, "DI_ID"), Nombre = S(r, "NOMBRE"), BaseDatosAccion = S(r, "BASE_DATOS_ACCION")
            };
        }
    }

    public sealed class ConectorAccRow : DataRowMappingBase
    {
        public string Procedimiento { get; set; }
        public int IdConector { get; set; }
        public int IdDiagrama { get; set; }
        public int NumConector { get; set; }
        public int NumSeqDesde { get; set; }
        public int NumSeqHasta { get; set; }
        public string CatConector { get; set; }
        public string IndSalidaTram { get; set; }
        public int DiId { get; set; }
        public string CatConector2 { get; set; }

        public static ConectorAccRow From(DataRow r)
        {
            return new ConectorAccRow
            {
                Procedimiento = S(r, "PROCEDIMIENTO"), IdConector = I(r, "ID_CONECTOR"), IdDiagrama = I(r, "ID_DIAGRAMA"), NumConector = I(r, "NUM_CONECTOR"), NumSeqDesde = I(r, "NUM_SEQ_DESDE"), NumSeqHasta = I(r, "NUM_SEQ_HASTA"),
                CatConector = S(r, "CAT_CONECTOR"), IndSalidaTram = S(r, "IND_SALIDA_TRAM"), DiId = I(r, "DI_ID"), CatConector2 = S(r, "CAT_CONECTOR2")
            };
        }
    }

    public sealed class ParamAccRow : JerarquiaRow
    {
        public int OrdenAcc { get; set; }
        public int OrdenAcSub { get; set; }
        public int IdAccion { get; set; }
        public string Parametro { get; set; }
        public string Valor { get; set; }
        public int OrdenPa { get; set; }

        public bool MismaClaveAccion(AccionesDiRow accion)
        {
            return MismaClaveJerarquica(accion) && OrdenAcc == accion.OrdenAcc && IdAccion == accion.IdAccion;
        }

        public static ParamAccRow From(DataRow r)
        {
            return new ParamAccRow
            {
                Procedimiento = S(r, "PROCEDIMIENTO"), OrdenN1 = I(r, "ORDEN_N1"), OrdenN2 = I(r, "ORDEN_N2"), OrdenN3 = I(r, "ORDEN_N3"), OrdenN4 = I(r, "ORDEN_N4"), OrdenN5 = I(r, "ORDEN_N5"),
                OrdenAcc = I(r, "ORDEN_ACC"), OrdenAcSub = I(r, "ORDEN_AC_SUB"), IdAccion = I(r, "ID_ACCION"), Parametro = S(r, "PARAMETRO"), Valor = S(r, "VALOR"), OrdenPa = I(r, "ORDEN_PA")
            };
        }
    }

    internal static class DataRowSafe
    {
        public static string S(DataRow row, string column)
        {
            if (row == null || row.Table == null || !row.Table.Columns.Contains(column) || row[column] == DBNull.Value) return string.Empty;
            return Convert.ToString(row[column], CultureInfo.InvariantCulture).Trim();
        }

        public static int I(DataRow row, string column)
        {
            var value = S(row, column);
            int result;
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) ? result : 0;
        }
    }

    // Alias cortos para no ensuciar los From(DataRow).
    public abstract class DataRowMappingBase
    {
        protected static string S(DataRow row, string column) { return DataRowSafe.S(row, column); }
        protected static int I(DataRow row, string column) { return DataRowSafe.I(row, column); }
    }
}
