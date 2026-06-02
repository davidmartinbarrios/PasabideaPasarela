using Lantik.Pasabidea.Core.Models.Mugi;
using System;
using System.Collections.Generic;

namespace Lantik.Pasabidea.Core.Mugi
{
    /// <summary>
    /// Conversión optimizada de la utilidad VB UtilidadesCreacionLineasDetalleFlujo.
    /// Genera filas de wfFlowActions usando MugiWfFlowAction.
    ///
    /// Nota: cada método recibe flow y version porque LineaDetalle VB no los llevaba,
    /// pero MugiWfFlowAction sí los necesita para insertar en wfFlowActions.
    /// </summary>
    internal static class UtilidadesCreacionMugiWfFlowActions
    {
        private const string Empty = "";
        private const string Zero = "0";
        private const string One = "1";
        private const string All = "ALL";
        private const string Aplicacion = "ARTEZ";

        private const int ApiIniEndExtNumeroParametrosDefecto = 42;
        private const int BloqueTramitacionComunNumeroParametrosDefecto = 38;
        private const int FormNumeroParametrosDefecto = 8;

        #region Métodos principales

        public static List<MugiWfFlowAction> ObtenerLineasDetalleInicioFlujo(string flow, int version)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, 0, 0, "BEGIN", "INICIO", "STATE", "INICIO", "Definición Pasarela")
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleFinFlujo(string flow, int version, string identificadorFlujo)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, 999999999, 0, "END", $"FIN {identificadorFlujo}", "FLUSHALL", Zero),
                Row(flow, version, 999999999, 1, Empty, Empty, "LEVEL", One)
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleFinalizacionTramitacion(
            string flow,
            int version,
            string identificadorFlujo,
            string baseDatosAccionesInfraestructura,
            string baseDatosDestinoFlujo)
        {
            var rows = new List<MugiWfFlowAction>();

            rows.AddRange(ObtenerLineasDetalleAccionLabel(
                flow,
                version,
                flowOrder: 999999900,
                path: "FIN_TRAMITACION",
                tieneParametroState: true,
                valorParametroState: "FIN_TRAMITACION",
                valorParametroSubmit: Zero,
                tipoAgrupacion: Empty,
                level: 1,
                comentario: Empty));

            rows.AddRange(ObtenerLineasDetalleAccionApiIniEndExt(
                flow,
                version,
                identificadorFlujo,
                flowOrder: 999999950,
                path: "FINALIZAR_TRAMITACION",
                baseDatosAccion: baseDatosAccionesInfraestructura,
                accion: "FINALIZAR_TRAMITACION",
                aplicacion: Aplicacion,
                contadorReferencia: 9999,
                level: 2,
                diferido: false,
                idntElemento: Empty,
                sistemaFuncional: Empty,
                grupoUsuarios: Empty,
                usuario: Empty,
                baseDatosRetorno: baseDatosDestinoFlujo,
                pathRetorno: $"FIN {identificadorFlujo}",
                comentario: Empty,
                parametrosEspecificos: null,
                habilitado: true));

            return rows;
        }

        #endregion

        #region Acciones simples

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionLabel(
            string flow,
            int version,
            int flowOrder,
            string path,
            bool tieneParametroState,
            string valorParametroState,
            string valorParametroSubmit,
            string tipoAgrupacion,
            int level,
            string comentario)
        {
            var rows = new List<MugiWfFlowAction>
            {
                Row(
                    flow,
                    version,
                    flowOrder,
                    0,
                    "LABEL",
                    path,
                    tieneParametroState ? "STATE" : Empty,
                    tieneParametroState ? valorParametroState : Empty,
                    tieneParametroState ? comentario : Empty),

                Row(
                    flow,
                    version,
                    flowOrder,
                    1,
                    Empty,
                    Empty,
                    "SUBMIT",
                    valorParametroSubmit,
                    !tieneParametroState ? comentario : Empty)
            };

            var nextId = 2;
            if (!string.IsNullOrEmpty(tipoAgrupacion))
            {
                rows.Add(Row(flow, version, flowOrder, nextId++, Empty, Empty, "TIPO_AGRUPACION", tipoAgrupacion));
            }

            rows.Add(Row(flow, version, flowOrder, nextId, Empty, Empty, "LEVEL", level.ToString()));
            return rows;
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionEndProc(
            string flow,
            int version,
            int flowOrder,
            string path,
            int level,
            string comentario)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "ENDPROC", path, "COMENTARIO", Empty, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "LEVEL", level.ToString())
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionJump(
            string flow,
            int version,
            int flowOrder,
            string path,
            string puntoSalto,
            int level,
            string comentario)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "JUMP", path, "PATH", puntoSalto, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "LEVEL", level.ToString())
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionUnion(
            string flow,
            int version,
            int flowOrder,
            string path,
            int numeroRamasParalelas,
            int level,
            string comentario)
        {
            var rows = new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "UNION", path, "PATH1", Empty, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "CONDITION1", Empty)
            };

            var id = 2;
            for (var numeroRama = 2; numeroRama <= numeroRamasParalelas; numeroRama++)
            {
                rows.Add(Row(flow, version, flowOrder, id++, Empty, Empty, $"PATH{numeroRama}", Empty));
                rows.Add(Row(flow, version, flowOrder, id++, Empty, Empty, $"CONDITION{numeroRama}", Empty));
            }

            rows.Add(Row(flow, version, flowOrder, id, Empty, Empty, "LEVEL", level.ToString()));
            return rows;
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionIf(
            string flow,
            int version,
            int flowOrder,
            string path,
            string valorParametroVar1,
            string valorParametroVar2,
            string condicion,
            string tipoComparacion,
            string saltoTrue,
            string saltoFalse,
            int level,
            string comentario)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "IF", path, "VAR1", valorParametroVar1),
                Row(flow, version, flowOrder, 1, Empty, Empty, "VAR2", valorParametroVar2, comentario),
                Row(flow, version, flowOrder, 2, Empty, Empty, "CONDITION", condicion),
                Row(flow, version, flowOrder, 3, Empty, Empty, "PATH", saltoTrue),
                Row(flow, version, flowOrder, 4, Empty, Empty, "ELSEPATH", saltoFalse),
                Row(flow, version, flowOrder, 5, Empty, Empty, "TYPE", tipoComparacion),
                Row(flow, version, flowOrder, 6, Empty, Empty, "LEVEL", level.ToString())
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionAlert(
            string flow,
            int version,
            int flowOrder,
            string path,
            string fechaReferencia,
            string maturation,
            string interval,
            string pathAlerta,
            int level,
            string comentario)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "ALERT", path, "DATE", fechaReferencia, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "MATURATION", maturation),
                Row(flow, version, flowOrder, 2, Empty, Empty, "INTERVAL", interval),
                Row(flow, version, flowOrder, 3, Empty, Empty, "ALERTPATH", pathAlerta),
                Row(flow, version, flowOrder, 4, Empty, Empty, "DAYSOFWEEK", "LMXJVSD"),
                Row(flow, version, flowOrder, 5, Empty, Empty, "EXACTTIME", One),
                Row(flow, version, flowOrder, 6, Empty, Empty, "LEVEL", level.ToString())
            };
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionCancel(
            string flow,
            int version,
            int flowOrder,
            string path,
            string valorPasoCancelar,
            int level)
        {
            return new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "CANCEL", path, "PATH", valorPasoCancelar),
                Row(flow, version, flowOrder, 1, Empty, Empty, "ALL", Zero),
                Row(flow, version, flowOrder, 2, Empty, Empty, "LEVEL", level.ToString())
            };
        }

        #endregion

        #region Acciones FORM / LETVAR

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionForm(
            string flow,
            int version,
            int flowOrder,
            string path,
            int level,
            string idntElemento,
            string tipoEjecucion,
            IDictionary<string, string> parametrosEspecificos)
        {
            return ObtenerLineasDetalleAccionForm(
                flow,
                version,
                flowOrder,
                path,
                level,
                esTramite: true,
                idntTramite: idntElemento,
                tipoEjecucion: tipoEjecucion,
                parametrosEspecificos: parametrosEspecificos);
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionForm(
            string flow,
            int version,
            int flowOrder,
            string path,
            int level,
            bool esTramite,
            string idntTramite,
            string tipoEjecucion,
            IDictionary<string, string> parametrosEspecificos)
        {
            var rows = new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "FORM", path, "FORM", "FORM"),
                Row(flow, version, flowOrder, 1, Empty, Empty, "FILTER", Empty),
                Row(flow, version, flowOrder, 2, Empty, Empty, "GROUP", Empty),
                Row(flow, version, flowOrder, 3, Empty, Empty, "USER", Empty),
                Row(flow, version, flowOrder, 4, Empty, Empty, "WAIT", One),
                Row(flow, version, flowOrder, 5, Empty, Empty, "TO", All),
                Row(flow, version, flowOrder, 6, Empty, Empty, "ICON", Empty),
                Row(flow, version, flowOrder, 7, Empty, Empty, "TIPOEJECUCION", tipoEjecucion)
            };

            var id = FormNumeroParametrosDefecto;
            if (esTramite)
            {
                rows.Add(Row(flow, version, flowOrder, id++, Empty, Empty, "IDNT_ELEMENTO", idntTramite));
            }

            AddSpecificParameters(rows, flow, version, flowOrder, ref id, parametrosEspecificos);
            rows.Add(Row(flow, version, flowOrder, id, Empty, Empty, "LEVEL", level.ToString()));
            return rows;
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionLetVar(
            string flow,
            int version,
            int flowOrder,
            string path,
            int level,
            IDictionary<string, string> variables,
            bool deInicio)
        {
            var rows = new List<MugiWfFlowAction>();
            var id = 0;

            foreach (var variable in variables)
            {
                rows.Add(Row(
                    flow,
                    version,
                    flowOrder,
                    id,
                    id == 0 ? "LETVAR" : Empty,
                    id == 0 ? path : Empty,
                    variable.Key,
                    variable.Value));
                id++;
            }

            if (deInicio)
            {
                rows.Add(Row(flow, version, flowOrder, id++, Empty, Empty, "INICIO", One));
            }

            rows.Add(Row(flow, version, flowOrder, id, Empty, Empty, "LEVEL", level.ToString()));
            return rows;
        }

        #endregion

        #region APIINIENDEXT / bloques comunes

        public static List<MugiWfFlowAction> ObtenerLineasDetalleLlamadaBloqueTramitacionComun(
            string flow,
            int version,
            string identificadorFlujo,
            int flowOrder,
            string path,
            string baseDatosBloqueTramitacionComun,
            string bloqueTramitacionComun,
            string aplicacion,
            int contadorReferencia,
            int level,
            bool diferido,
            string baseDatosRetorno,
            string pathRetorno,
            string comentario,
            IDictionary<string, string> parametrosEspecificos,
            bool habilitado)
        {
            var rows = new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "APIINIENDEXT", path, "PROJECT", baseDatosBloqueTramitacionComun, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "FLOW", bloqueTramitacionComun),
                Row(flow, version, flowOrder, 2, Empty, Empty, "REFERENCE", ReferenceExpression(aplicacion, contadorReferencia)),
                Row(flow, version, flowOrder, 3, Empty, Empty, "INIT", One),
                Row(flow, version, flowOrder, 4, Empty, Empty, "LEVEL", level.ToString()),
                Row(flow, version, flowOrder, 5, Empty, Empty, "DEFERRED", diferido ? One : Zero),
                Row(flow, version, flowOrder, 6, Empty, Empty, "CODIGO_APLICACION", aplicacion),
                Row(flow, version, flowOrder, 7, Empty, Empty, "IDNT_DOMINIO", "[%IDNT_DOMINIO%]"),
                Row(flow, version, flowOrder, 8, Empty, Empty, "IDNT_PROCEDIMIENTO", "[%IDNT_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 9, Empty, Empty, "DESCRIPTOR_PROCEDIMIENTO", "[%DESCRIPTOR_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 10, Empty, Empty, "FEC_INI_SISTEMA", "[%FEC_INI_SISTEMA%]"),
                Row(flow, version, flowOrder, 11, Empty, Empty, "FEC_INI_VIGENCIA", "[%FEC_INI_VIGENCIA%]"),
                Row(flow, version, flowOrder, 12, Empty, Empty, "INSTANCIA_N8", "[%INSTANCIA_N8%]"),
                Row(flow, version, flowOrder, 13, Empty, Empty, "IDNT_INSTANCIA_PROCEDIMIENTO", "[%IDNT_INSTANCIA_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 14, Empty, Empty, "TIPO_ALTA", "[%TIPO_ALTA%]"),
                Row(flow, version, flowOrder, 15, Empty, Empty, "CENTRO_FORAL", "[%CENTRO_FORAL%]"),
                Row(flow, version, flowOrder, 16, Empty, Empty, "ORGANICO_TRAMITADOR", "[%ORGANICO_TRAMITADOR%]"),
                Row(flow, version, flowOrder, 17, Empty, Empty, "PROCEDIMIENTO_JX", "[%PROCEDIMIENTO_JX%]"),
                Row(flow, version, flowOrder, 18, Empty, Empty, "FORMULARIO_JX", "[%FORMULARIO_JX%]"),
                Row(flow, version, flowOrder, 19, Empty, Empty, "IDNT_INTERESADO", "[%IDNT_INTERESADO%]"),
                Row(flow, version, flowOrder, 20, Empty, Empty, "NIF_INTERESADO", "[%NIF_INTERESADO%]"),
                Row(flow, version, flowOrder, 21, Empty, Empty, "IDNT_REPRESENTANTE", "[%IDNT_REPRESENTANTE%]"),
                Row(flow, version, flowOrder, 22, Empty, Empty, "NIF_REPRESENTANTE", "[%NIF_REPRESENTANTE%]"),
                Row(flow, version, flowOrder, 23, Empty, Empty, "IDNT_TRAMITACION_CARPETA", "[%IDNT_TRAMITACION_CARPETA%]"),
                Row(flow, version, flowOrder, 24, Empty, Empty, "IDNT_EXPEDIENTE_CARPETA", "[%IDNT_EXPEDIENTE_CARPETA%]"),
                Row(flow, version, flowOrder, 25, Empty, Empty, "IDNT_EXPEDIENTE", "[%IDNT_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 26, Empty, Empty, "NUMERO_EXPEDIENTE", "[%NUMERO_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 27, Empty, Empty, "DESCRIPTOR_TIPO_EXPEDIENTE", "[%DESCRIPTOR_TIPO_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 28, Empty, Empty, "CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES", "[%CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES%]"),
                Row(flow, version, flowOrder, 29, Empty, Empty, "IDNT_SUBEXPEDIENTE", "[%IDNT_SUBEXPEDIENTE%]"),
                Row(flow, version, flowOrder, 30, Empty, Empty, "IDNT_ENTRADA", "[%IDNT_ENTRADA%]"),
                Row(flow, version, flowOrder, 31, Empty, Empty, "NUMERO_REGISTRO_ENTRADA", "[%NUMERO_REGISTRO_ENTRADA%]"),
                Row(flow, version, flowOrder, 32, Empty, Empty, "FECHA_REGISTRO_ENTRADA", "[%FECHA_REGISTRO_ENTRADA%]"),
                Row(flow, version, flowOrder, 33, Empty, Empty, "NUMERO_EJECUCION", "[%NUMERO_EJECUCION%]"),
                Row(flow, version, flowOrder, 34, Empty, Empty, "BBDD_RETORNO", baseDatosRetorno),
                Row(flow, version, flowOrder, 35, Empty, Empty, "FLUJO_RETORNO", identificadorFlujo),
                Row(flow, version, flowOrder, 36, Empty, Empty, "PATH_RETORNO", pathRetorno),
                Row(flow, version, flowOrder, 37, Empty, Empty, "REFERENCIA_RETORNO", "[%REFERENCE%]")
            };

            var id = BloqueTramitacionComunNumeroParametrosDefecto;
            AddOptionalSpecificAndEnable(rows, flow, version, flowOrder, ref id, parametrosEspecificos, habilitado);
            return rows;
        }

        public static List<MugiWfFlowAction> ObtenerLineasDetalleAccionApiIniEndExt(
            string flow,
            int version,
            string identificadorFlujo,
            int flowOrder,
            string path,
            string baseDatosAccion,
            string accion,
            string aplicacion,
            int contadorReferencia,
            int level,
            bool diferido,
            string idntElemento,
            string sistemaFuncional,
            string grupoUsuarios,
            string usuario,
            string baseDatosRetorno,
            string pathRetorno,
            string comentario,
            IDictionary<string, string> parametrosEspecificos,
            bool habilitado)
        {
            var tieneRetorno = !string.IsNullOrEmpty(baseDatosRetorno);

            var rows = new List<MugiWfFlowAction>
            {
                Row(flow, version, flowOrder, 0, "APIINIENDEXT", path, "PROJECT", baseDatosAccion, comentario),
                Row(flow, version, flowOrder, 1, Empty, Empty, "FLOW", accion),
                Row(flow, version, flowOrder, 2, Empty, Empty, "REFERENCE", ReferenceExpression(aplicacion, contadorReferencia)),
                Row(flow, version, flowOrder, 3, Empty, Empty, "INIT", One),
                Row(flow, version, flowOrder, 4, Empty, Empty, "LEVEL", level.ToString()),
                Row(flow, version, flowOrder, 5, Empty, Empty, "DEFERRED", diferido ? One : Zero),
                Row(flow, version, flowOrder, 6, Empty, Empty, "CODIGO_APLICACION", aplicacion),
                Row(flow, version, flowOrder, 7, Empty, Empty, "IDNT_DOMINIO", "[%IDNT_DOMINIO%]"),
                Row(flow, version, flowOrder, 8, Empty, Empty, "IDNT_PROCEDIMIENTO", "[%IDNT_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 9, Empty, Empty, "DESCRIPTOR_PROCEDIMIENTO", "[%DESCRIPTOR_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 10, Empty, Empty, "FEC_INI_SISTEMA", "[%FEC_INI_SISTEMA%]"),
                Row(flow, version, flowOrder, 11, Empty, Empty, "FEC_INI_VIGENCIA", "[%FEC_INI_VIGENCIA%]"),
                Row(flow, version, flowOrder, 12, Empty, Empty, "INSTANCIA_N8", "[%INSTANCIA_N8%]"),
                Row(flow, version, flowOrder, 13, Empty, Empty, "IDNT_INSTANCIA_PROCEDIMIENTO", "[%IDNT_INSTANCIA_PROCEDIMIENTO%]"),
                Row(flow, version, flowOrder, 14, Empty, Empty, "TIPO_ALTA", "[%TIPO_ALTA%]"),
                Row(flow, version, flowOrder, 15, Empty, Empty, "CENTRO_FORAL", "[%CENTRO_FORAL%]"),
                Row(flow, version, flowOrder, 16, Empty, Empty, "ORGANICO_TRAMITADOR", "[%ORGANICO_TRAMITADOR%]"),
                Row(flow, version, flowOrder, 17, Empty, Empty, "PROCEDIMIENTO_JX", "[%PROCEDIMIENTO_JX%]"),
                Row(flow, version, flowOrder, 18, Empty, Empty, "FORMULARIO_JX", "[%FORMULARIO_JX%]"),
                Row(flow, version, flowOrder, 19, Empty, Empty, "IDNT_INTERESADO", "[%IDNT_INTERESADO%]"),
                Row(flow, version, flowOrder, 20, Empty, Empty, "NIF_INTERESADO", "[%NIF_INTERESADO%]"),
                Row(flow, version, flowOrder, 21, Empty, Empty, "IDNT_REPRESENTANTE", "[%IDNT_REPRESENTANTE%]"),
                Row(flow, version, flowOrder, 22, Empty, Empty, "NIF_REPRESENTANTE", "[%NIF_REPRESENTANTE%]"),
                Row(flow, version, flowOrder, 23, Empty, Empty, "IDNT_TRAMITACION_CARPETA", "[%IDNT_TRAMITACION_CARPETA%]"),
                Row(flow, version, flowOrder, 24, Empty, Empty, "IDNT_EXPEDIENTE_CARPETA", "[%IDNT_EXPEDIENTE_CARPETA%]"),
                Row(flow, version, flowOrder, 25, Empty, Empty, "IDNT_EXPEDIENTE", "[%IDNT_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 26, Empty, Empty, "NUMERO_EXPEDIENTE", "[%NUMERO_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 27, Empty, Empty, "DESCRIPTOR_TIPO_EXPEDIENTE", "[%DESCRIPTOR_TIPO_EXPEDIENTE%]"),
                Row(flow, version, flowOrder, 28, Empty, Empty, "CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES", "[%CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES%]"),
                Row(flow, version, flowOrder, 29, Empty, Empty, "IDNT_SUBEXPEDIENTE", "[%IDNT_SUBEXPEDIENTE%]"),
                Row(flow, version, flowOrder, 30, Empty, Empty, "IDNT_ENTRADA", "[%IDNT_ENTRADA%]"),
                Row(flow, version, flowOrder, 31, Empty, Empty, "NUMERO_REGISTRO_ENTRADA", "[%NUMERO_REGISTRO_ENTRADA%]"),
                Row(flow, version, flowOrder, 32, Empty, Empty, "FECHA_REGISTRO_ENTRADA", "[%FECHA_REGISTRO_ENTRADA%]"),
                Row(flow, version, flowOrder, 33, Empty, Empty, "NUMERO_EJECUCION", "[%NUMERO_EJECUCION%]"),
                Row(flow, version, flowOrder, 34, Empty, Empty, "IDNT_ELEMENTO", idntElemento),
                Row(flow, version, flowOrder, 35, Empty, Empty, "SISTEMA_FUNCIONAL", sistemaFuncional),
                Row(flow, version, flowOrder, 36, Empty, Empty, "GRUPO_USUARIOS", grupoUsuarios),
                Row(flow, version, flowOrder, 37, Empty, Empty, "USUARIO", usuario),
                Row(flow, version, flowOrder, 38, Empty, Empty, "BBDD_RETORNO", baseDatosRetorno),
                Row(flow, version, flowOrder, 39, Empty, Empty, "FLUJO_RETORNO", tieneRetorno ? identificadorFlujo : Empty),
                Row(flow, version, flowOrder, 40, Empty, Empty, "PATH_RETORNO", pathRetorno),
                Row(flow, version, flowOrder, 41, Empty, Empty, "REFERENCIA_RETORNO", tieneRetorno ? "[%REFERENCE%]" : Empty)
            };

            var id = ApiIniEndExtNumeroParametrosDefecto;
            AddOptionalSpecificAndEnable(rows, flow, version, flowOrder, ref id, parametrosEspecificos, habilitado);
            return rows;
        }

        #endregion

        #region Helpers

        private static MugiWfFlowAction Row(
            string flow,
            int version,
            int flowOrder,
            int id,
            string action,
            string path,
            string param,
            string value,
            string comments = Empty)
        {
            return new MugiWfFlowAction
            {
                Flow = flow,
                Version = version,
                FlowOrder = flowOrder,
                Id = id,
                Action = action ?? Empty,
                Path = path ?? Empty,
                Param = param ?? Empty,
                Value = value ?? Empty,
                Comments = comments ?? Empty
            };
        }

        private static void AddSpecificParameters(
            ICollection<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            ref int id,
            IDictionary<string, string> parametrosEspecificos)
        {
            if (parametrosEspecificos == null)
                return;

            foreach (var parametro in parametrosEspecificos)
            {
                rows.Add(Row(flow, version, flowOrder, id++, Empty, Empty, parametro.Key, parametro.Value));
            }
        }

        private static void AddOptionalSpecificAndEnable(
            ICollection<MugiWfFlowAction> rows,
            string flow,
            int version,
            int flowOrder,
            ref int id,
            IDictionary<string, string> parametrosEspecificos,
            bool habilitado)
        {
            AddSpecificParameters(rows, flow, version, flowOrder, ref id, parametrosEspecificos);

            if (!habilitado)
            {
                rows.Add(Row(flow, version, flowOrder, id, Empty, Empty, "ENABLE", Zero));
            }
        }

        private static string ReferenceExpression(string aplicacion, int contadorReferencia)
        {
            return $"=$CONCAT([%REFERENCE%];[_{aplicacion}_{contadorReferencia:0000}])/$";
        }

        #endregion
    }
}
