Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class ParametrosGeneracionPeticionNotificacion
        <XmlElement("ModoEmision", IsNullable:=True)>
        Public Property ModoEmision As String

        <XmlElement("TipoEmision", IsNullable:=True)>
        Public Property TipoEmision As String

        <XmlElement("TieneAcuseRecibo", IsNullable:=True)>
        Public Property TieneAcuseRecibo As String

        <XmlElement("Emisor", IsNullable:=True)>
        Public Property Emisor As String

        <XmlElement("Asunto", IsNullable:=True)>
        Public Property Asunto As String

        <XmlElement("IndicadorBloetin", IsNullable:=True)>
        Public Property IndicadorBoletin As String

        <XmlElement("TipoDestinatario", IsNullable:=True)>
        Public Property TipoDestinatario As String

        <XmlElement("TipoNotificacion", IsNullable:=True)>
        Public Property TipoNotificacion As String
    End Class
End Namespace