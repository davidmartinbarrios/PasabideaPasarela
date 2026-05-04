Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosFirmaCSVOrganico
        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("NivelOrganico", IsNullable:=True)>
        Public Property NivelOrganico As String

        <XmlElement("Organico", IsNullable:=True)>
        Public Property Organico As String

        <XmlElement("Politica", IsNullable:=True)>
        Public Property Politica
    End Class
End Namespace