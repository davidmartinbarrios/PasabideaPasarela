Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class NotificacionTelePapel
        <XmlElement("EsTelePapel", IsNullable:=True)>
        Public Property EsTelePapelXML As String

        <XmlElement("CodigoPlazoNotificacionTelematica", IsNullable:=True)>
        Public Property CodigoPlazoNotificacionTelematicaXML As String

        <XmlElement("TramiteDestinoCaducidadNotificacionTelematica", IsNullable:=True)>
        Public Property TramiteDestinoCaducidadPlazoXML As String

        <XmlIgnore>
        Friend ReadOnly Property EsNotificacionTelePapel As Boolean
            Get
                Return If(String.IsNullOrEmpty(EsTelePapelXML) OrElse EsTelePapelXML.Equals(Constantes.N), False, True)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property EsTelePapel As String
            Get
                Return ObtenerValorParaParametro(EsTelePapelXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property CodigoPlazoNotificacionTelematica As String
            Get
                Return ObtenerValorParaParametro(CodigoPlazoNotificacionTelematicaXML)
            End Get
        End Property

#Region "Métodos Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Comprobamos si se trata de una Notificación en Tele-Papel, porque en ese caso, el Código de Plazo y el Trámite Destino para la Caducidad de la Notificación Telemática son obligatorios
            If EsNotificacionTelePapel Then
                If String.IsNullOrEmpty(CodigoPlazoNotificacionTelematicaXML) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Código de Plazo para la Notificación Telemática del Grupo 'DatosNotificacionTelePapel'.", nombreBloque))
                End If

                If String.IsNullOrEmpty(TramiteDestinoCaducidadPlazoXML) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Trámite Destino para la Caducidad de la Notificación Telemática del Grupo 'DatosNotificacionTelePapel'.", nombreBloque))
                End If
            End If
        End Sub

        Friend Function TramiteDestinoCaducidadPlazo(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As String
            Return ObtenerPathTramite(resumenTramitesNombreEnFlujo, TramiteDestinoCaducidadPlazoXML)
        End Function
#End Region

    End Class
End Namespace