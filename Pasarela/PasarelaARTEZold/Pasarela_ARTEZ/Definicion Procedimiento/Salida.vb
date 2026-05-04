Imports System.Text
Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class Salida
        <XmlElement("Descripcion")>
        Public Property Descripcion As String

        <XmlElement("TramiteDestino")>
        Public Property TramiteDestino As String

        <XmlArrayItem("Condicion", IsNullable:=True)>
        Public Property Condiciones As List(Of Condicion)

#Region "Constructores"
        Public Sub New()

        End Sub
#End Region
        Friend Function Condicionada() As Boolean
            ' Determinar si es una salida Condicionada, así que comprobamos si tenemos condiciones asociadas a la salida
            Return If(Not IsNothing(Condiciones) AndAlso Condiciones.Count > Constantes.NUMERO_0, True, False)
        End Function

        Friend Function ObtenerLineasDetalleSalidaUnica(ByRef flowOrder As Integer, ByRef contadorJUMPs As Integer, ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle)

            flowOrder += 100
            contadorJUMPs += 1
            lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(flowOrder, path:=String.Format("JUMP_{0:0000}", contadorJUMPs), puntoSalto:=ObtenerPATHSalotSalidaNoExcluyente(resumenTramitesNombreEnFlujo), level:=2,
                                                                                                         comentario:=Descripcion))

            Return lineasDetalle
        End Function

        'Friend Function ObtenerLineasDetalleEvaluacionSalidaUnica(ByRef flowOrder As Integer, ByRef datosGenerales As DatosGeneralesTramite, ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As List(Of LineaDetalle)
        '    Dim lineasDetalle As New List(Of LineaDetalle)

        '    ' Nos recorremos las Condiciones Asociadas a la Salida pra obtener las sentencias de las Evaluaciones
        '    Dim numeroCondicion As Integer = 1
        '    For Each condicion As Condicion In Condiciones
        '        flowOrder += 100
        '        lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_IF(flowOrder, path:=String.Format("EVALUAR_SALIDA_T_{0:000}_{1}", datosGenerales.OrdenTramite, numeroCondicion),
        '                                                                                                   valorParametroVAR1:=condicion.ObtenerValorOperando1(datosGenerales, datosGenerales.OrdenTramite), valorParametroVAR2:=condicion.ObtenerValorOperando2,
        '                                                                                                   condicion:=condicion.ObtenerOperador, tipoComparacion:="STRING",
        '                                                                                                   saltoTRUE:=ObtenerSaltoTRUEEvaluacionSalidaUnica(numeroCondicion, datosGenerales.OrdenTramite, resumenTramitesNombreEnFlujo),
        '                                                                                                   saltoFALSE:=Constantes.ACCION_GESTION_FIN_TRAMITACION,
        '                                                                                                   level:=2, comentario:=Descripcion))

        '        ' Avanzamos a la Siguiente Condición
        '        numeroCondicion += 1
        '    Next

        '    Return lineasDetalle
        'End Function

        Friend Function ObtenerLineasDetalleSalidaNoExcluyente(ByRef flowOrder As Integer, ByRef datosGenerales As DatosGeneralesTramite, contadorSalida As Integer, esUltimaSalidaTramite As Boolean, ByRef contadorJUMPs As Integer,
                                                               ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle)

            With datosGenerales
                ' Comprobamos si la Salida está Condicionada
                If Condicionada() Then
                    ' La Salida tiene Condiciones, así que creamos la Rama Paralela Condicionada con el Salto al siguiente Trámite
                    flowOrder += 100
                    lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(flowOrder, path:=String.Format("SALIDA_T_{0:000}_{1}", .OrdenTramite, contadorSalida), tieneParametro_STATE:=False, valorParametro_STATE:=String.Empty,
                                                                                                                  valorParametro_SUBMIT:=ObtenerParametroSUBMITSalidaCondicionada(datosGenerales), tipoAgrupacion:=String.Empty, level:=2,
                                                                                                                  comentario:=Descripcion))

                    flowOrder += 100
                    contadorJUMPs += 1
                    lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(flowOrder, path:=String.Format("JUMP_{0:0000}", contadorJUMPs), puntoSalto:=ObtenerPATHSalotSalidaNoExcluyente(resumenTramitesNombreEnFlujo), level:=3,
                                                                                                                 comentario:=Descripcion))

                Else
                    ' La Salida no tiene Condiciones, así que creamos la Rama Paralela con el Salto al siguiente Trámite
                    flowOrder += 100
                    lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(flowOrder, path:=String.Format("SALIDA_T_{0:000}_{1}", .OrdenTramite, contadorSalida), tieneParametro_STATE:=False, valorParametro_STATE:=String.Empty,
                                                                                                                  valorParametro_SUBMIT:="FLY", tipoAgrupacion:=String.Empty, level:=2, comentario:=Descripcion))

                    flowOrder += 100
                    contadorJUMPs += 1
                    lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(flowOrder, path:=String.Format("JUMP_{0:0000}", contadorJUMPs), puntoSalto:=ObtenerPATHSalotSalidaNoExcluyente(resumenTramitesNombreEnFlujo), level:=3,
                                                                                                                 comentario:=Descripcion))
                End If

                ' Comprobamos si estamos tratando la Última Salida del Trámite
                If esUltimaSalidaTramite Then
                    ' Estamos tratando la última salida de un Trámite, así que incluimos un paso de parada para no seguir con la secuencia de ejecución
                    flowOrder += 100
                    lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_ENDPROC(flowOrder, path:=String.Format("FIN_SALIDAS_T_{0:000}", .OrdenTramite), level:=2,
                                                                                                                    comentario:=String.Format("Fin del tratamiento de las salidas del Trámite {0}", .OrdenTramite)))
                End If
            End With

            Return lineasDetalle
        End Function

        Friend Function ObtenerValorOperando1PrimeraCondicion(ByRef datosGenerales As DatosGeneralesTramite) As String
            Return Condiciones.First.ObtenerValorOperando1(datosGenerales, datosGenerales.OrdenTramite)
        End Function

        Friend Function ObtenerValorOperando2SalidaExcluyente() As String
            Return Condiciones.First.ObtenerValorOperando2
        End Function

        Friend Function ObtenerValorOperadorPrimeraCondicion() As String
            Return Condiciones.First.ObtenerOperador
        End Function

        Private Function ObtenerPATHSalotSalidaNoExcluyente(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As String
            Return If(String.IsNullOrEmpty(TramiteDestino), Constantes.ACCION_GESTION_FIN_TRAMITACION, Utilidades.Utilidades.ObtenerPathTramite(resumenTramitesNombreEnFlujo, TramiteDestino))
        End Function

        Private Function ObtenerParametroSUBMITSalidaCondicionada(ByRef datosGenerales As DatosGeneralesTramite) As String
            Dim parametroSUBMIT As New StringBuilder()

            ' Contruimos la verificación para poder determinar si se tiene que crear una Rama Paralela
            parametroSUBMIT.Append("=$IIF([")
            ' Nos recorremos las condiciones de la Salida
            Dim contadorCondicion As Integer = 1
            For Each condicion As Condicion In Condiciones
                If contadorCondicion > Integer.Parse(Constantes.NUMERO_1) Then
                    parametroSUBMIT.Append("_")
                End If
                parametroSUBMIT.AppendFormat("{0}{1}{2}", condicion.ObtenerValorOperando1(datosGenerales, datosGenerales.OrdenTramite), condicion.ObtenerOperador, condicion.ObtenerValorOperando2)

                contadorCondicion += 1
            Next
            parametroSUBMIT.Append("];[];[FLY];[1])/$")

            Return parametroSUBMIT.ToString
        End Function

        'Private Function ObtenerSaltoTRUEEvaluacionSalidaUnica(numeroCondicion As Integer, ordenTramite As Integer, ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As String
        '    Dim saltoTRUE As String = String.Empty

        '    ' Comprobamos si estamos tratando la última condición
        '    If numeroCondicion = Condiciones.Count Then
        '        ' Es la última salida, así que el salto TRUE sería el trámite destino de la Salida o la Acción de Gestión FIN_TRAMITACION si no se ha especificado un trámite destino
        '        saltoTRUE = If(String.IsNullOrEmpty(TramiteDestino), Constantes.ACCION_GESTION_FIN_TRAMITACION, Utilidades.Utilidades.ObtenerPathTramite(resumenTramitesNombreEnFlujo, TramiteDestino))

        '    Else
        '        ' No es la última salida, así que el salto TRUE será el enlace con la siguiente Evaluación
        '        saltoTRUE = String.Format("EVALUAR_SALIDA_T_{0:000}_{1}", ordenTramite, numeroCondicion + 1)
        '    End If

        '    Return saltoTRUE
        'End Function
    End Class
End Namespace