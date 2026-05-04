Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosTramiteRecepcionAcuseRecibo
        <XmlElement("NumeroNegocio", IsNullable:=True)>
        Public Property NumeroNegocio As String

        <XmlElement("NumeroDocumentoEmision")>
        Public Property NumeroDocumentoEmision As String

        <XmlElement("NumeroNotificacionNT")>
        Public Property NumeroNotificacionNT As String

#Region "Métodos Publicos y Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Verificamos que se ha informado el Número de Documento de la Emisión
            If String.IsNullOrEmpty(NumeroDocumentoEmision) Then
                Throw New Exception(String.Format("El Trámite de Recepción de Acuse de Recibo '{0}' no tiene informado el Número de Documento de Emisión.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Número de Notificación de NT
            If String.IsNullOrEmpty(NumeroNotificacionNT) Then
                Throw New Exception(String.Format("El Trámite de Recepción de Acuse de Recibo '{0}' no tiene informado el Número de Notificación de NT.", nombreBloque))
            End If
        End Sub

        Friend Function ObtenerParametrosEntrada() As Dictionary(Of String, String)
            Return New Dictionary(Of String, String) From {
                {"@NUM_NEGOCIO_IN", ObtenerValorParaParametro(NumeroNegocio)},
                {"@NUM_DOCUMENTO_NOTIF_IN", ObtenerValorParaParametro(NumeroDocumentoEmision)},
                {"@IDNT_NOTIFICACION_NT_IN", ObtenerValorParaParametro(NumeroNotificacionNT)}
            }
        End Function
#End Region

#Region "Métodos Privados"
        Private Function ObtenerNombreVariableNotificacionAcusada(ordenTramite As Integer) As String
            Return String.Format("NOTI_ACUSADA_T_{0:000}", ordenTramite)
        End Function
#End Region
    End Class
End Namespace