Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class TramitesBloqueTramitacionComunNotificacion
        <XmlElement("TramiteObtenerDatosNotificacion")>
        Public Property TramiteObtenerDatosNotificacion As DatosTramiteAutomatico

        <XmlElement("TramiteEsperaEmisionNotificacion")>
        Public Property TramiteEsperaEmisionNotificacion As DatosTramiteAutomatico

        <XmlElement("TramiteEsperaAcuseRecibo", IsNullable:=True)>
        Public Property TramiteEsperaAcuseRecibo As DatosTramiteAutomatico

        <XmlElement("TramiteInformarDatosNotificacion")>
        Public Property TramiteInformarDatosNotificacion As DatosTramiteManual

        <XmlElement("TramiteRevisionErroresNotificacion")>
        Public Property TramiteRevisionErroresNotificacion As DatosTramiteManual
    End Class
End Namespace