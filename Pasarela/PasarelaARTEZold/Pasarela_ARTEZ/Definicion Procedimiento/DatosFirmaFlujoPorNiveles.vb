Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosFirmaFlujoPorNiveles
        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("Idnt_TramiteRevisionRechazoFirma")>
        Public Property Idnt_TramiteRevisionRechazoFirma As String

        <XmlElement("FlujoFK", IsNullable:=True)>
        Public Property FlujoFK As String

        <XmlElement("Organico", IsNullable:=True)>
        Public Property Organico As String

        <XmlElement("TipoSello", IsNullable:=True)>
        Public Property TipoSello As String
    End Class
End Namespace