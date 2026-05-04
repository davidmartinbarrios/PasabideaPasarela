Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosGeneralesTramite
        <XmlElement("Nombre")>
        Public Property Nombre As String

        <XmlElement("BloqueTramitacionComun", IsNullable:=True)>
        Public Property BloqueTramitacionComun As String

        <XmlElement("Tipo")>
        Public Property TipoTramite As TipoTramite

        <XmlElement("NombreEnFlujo")>
        Public Property NombreEnFlujo As String

        <XmlElement("VariableTramitePlanificado", IsNullable:=True)>
        Public Property VariableTramitePlanificado As String

        <XmlIgnore()>
        Friend Property OrdenTramite As Integer

        <XmlIgnore()>
        Friend Property PathSalidaBloqueTramitacionComun() As String

#Region "Métodos para la Obtención de los PATH ascociados al trámite"
        Friend Function EsBloqueTramitacionComun() As Boolean
            Return If(Not IsNothing(BloqueTramitacionComun) AndAlso BloqueTramitacionComun.Trim.ToUpper.Equals(Constantes.S), True, False)
        End Function

        Friend Function ObtenerNombrePathTramite() As String
            ' Inicializamos el Nombre del Path por defecto
            Dim nombrePathTramite As String = String.Format("{0}_{1:000}", NombreEnFlujo, OrdenTramite)

            ' Comprobamo si superamos el máximo número de caracteres para un PATH
            If SuperaNumeroMaximoCaracteresPATH(nombrePathTramite) Then
                nombrePathTramite = String.Format("{0}_{1:000}", NombreEnFlujo.Substring(0, NombreEnFlujo.Length - NumeroCaracteresExtraPATH(nombrePathTramite)), OrdenTramite)
            End If

            Return nombrePathTramite
        End Function

        Friend Function ObtenerNombrePathBloqueTramitacionComun() As String
            Dim bloqueTramitacionComun As String = String.Empty

            Select Case TipoTramite
                Case TipoTramite.Notificacion
                    bloqueTramitacionComun = "BLOQUE_TRAM_NOTIF"

                Case TipoTramite.Firma
                    bloqueTramitacionComun = "BLOQUE_TRAM_FIRMA"

                Case TipoTramite.BKON
                    bloqueTramitacionComun = "BLOQUE_TRAM_BKON"

                Case TipoTramite.PuestaManifiesto
                    bloqueTramitacionComun = "BLOQUE_TRAM_PUESTA_M"
            End Select

            Return String.Format("{0}_{1:000}", bloqueTramitacionComun, OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionObtenerPlazo() As String
            Return String.Format("{0}_{1:000}", Constantes.ACCION_GESTION_OBTENER_PLAZO, OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathEvaluacionCaducidadAlertaPlazo() As String
            Return String.Format("IF_ALERTA_CADUCADA_T_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathALERT() As String
            Return String.Format("ALERTA_T_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionCaducarTarea() As String
            Return String.Format("{0}_{1:0000}", Constantes.ACCION_GESTION_CADUCAR_TAREA, OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionCrearTareaAutomatica() As String
            Return String.Format("CREAR_TAREA_AUTOMA_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionCrearTareaManual() As String
            Return String.Format("{0}_{1:000}", Constantes.ACCION_GESTION_CREAR_TAREA_MANUAL, OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionCrearTareaComprobacionRequisitos() As String
            Return String.Format("CREAR_TAREA_COMP_RE_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionCrearTareaRecepcionAcuseRecibo() As String
            Return String.Format("CREAR_TAREA_ACUSE_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionEjecutarTareaAutomatica() As String
            Return String.Format("EJECUTAR_TAREA_AUT_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathComprobacionNotificacionAcusada() As String
            Return String.Format("COMP_NOTI_ACUSADA_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathFROM() As String
            Return String.Format("FORM TAREA {0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathAccionGestionFinalizarTarea() As String
            Return String.Format("{0}_{1:000}", Constantes.ACCION_GESTION_FINALIZAR_TAREA, OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathCANCEL_ALERT() As String
            Return String.Format("CANCEL_ALERT_T_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathCANCEL_FORM() As String
            Return String.Format("CANCEL_FORM_T_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathComprobacionEjecucionTareaPlanificada() As String
            Return String.Format("COMP_EJEC_TAREA_PLAN_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathComprobacionRespuestaFormularioTareaPlanificada() As String
            Return String.Format("COMP_REAC_TAREA_PLAN_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerLiteralLecturaValorVariableTareaPlanificada() As String
            Return String.Format("@INICIO!{0}", VariableTramitePlanificado)
        End Function

        Friend Function ObtenerNombreVariableTareaPlanificadaInicializacion() As String
            Return String.Format("@{0}", VariableTramitePlanificado)
        End Function

        Friend Function ObtenerLiteralLectutaValorVariableRespuestaFormulario() As String
            Return String.Format("@{0}!RESPUESTA", ObtenerNombrePathFROM)
        End Function

        Friend Function ObtenerLiteralLectutaValorVariableDocumentoGestorDocumentalFormulario() As String
            Return String.Format("@{0}!DOCGESTORDOCUMENTAL", ObtenerNombrePathFROM)
        End Function

        Friend Function ObtenerLiteralLectutaValorVariableDocumentoFirmadoFueraARTEZFormulario() As String
            Return String.Format("@{0}!DOC_FIRMADO_FUERA_ARTEZ", ObtenerNombrePathFROM)
        End Function

        Friend Function ObtenerLiteralLectutaValorVariablePeticionSolicitudFirma(valorParametroHabilitarPestanaFirma As String, valorParametroTipoFirmaPorDefecto As String) As String
            Dim valor As String = String.Empty

            If valorParametroHabilitarPestanaFirma.StartsWith("@") Then
                ' El valor es una Variable, así que tenemos que realizar un IF para devolver el valor del Tipo de Firma
                valor = String.Format("=$IIF([{0}=true];[];[@{1}!TIPOSOLICITUDFIRMAN8];[{2}])/$", valorParametroHabilitarPestanaFirma, ObtenerNombrePathFROM, valorParametroTipoFirmaPorDefecto)

            Else
                ' El valor es TRUE, así que hay que recuperar el Tipo de Firma seleccionado en el Formualrio
                valor = String.Format("@{0}!TIPOSOLICITUDFIRMAN8", ObtenerNombrePathFROM)
            End If

            Return valor
        End Function

        Friend Function ObtenerLiteralLectutaValorVariableFechaFirma() As String
            Return String.Format("@{0}!FECHAFIRMA", ObtenerNombrePathFROM)
        End Function

        Friend Function ObtenerLiteralLectutaValorVariablePeticionSolicitudFirmaN8() As String
            Return String.Format("@{0}!PETSOLICITUDFIRMAN8", ObtenerNombrePathFROM)
        End Function

        Friend Function ObtenerNombrePathInicializacionVariableTareaPlanificada() As String
            Return String.Format("LET INI VAR_T_PLANIF_{0:000}", OrdenTramite)
        End Function

        Friend Function ObtenerNombrePathInicializacionVariableTareaManualElaboracionDocumento() As String
            Return String.Format("INI VAR_ELAB_DOC_{0:000}", OrdenTramite)
        End Function
#End Region
    End Class
End Namespace