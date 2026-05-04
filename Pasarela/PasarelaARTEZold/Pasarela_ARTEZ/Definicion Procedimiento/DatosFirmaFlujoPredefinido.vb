Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosFirmaFlujoPredefinido
        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("Idnt_TramiteRevisionRechazoFirma")>
        Public Property Idnt_TramiteRevisionRechazoFirma As String

        <XmlElement("FlujoFK", IsNullable:=True)>
        Public Property FlujoFK As String
    End Class
End Namespace