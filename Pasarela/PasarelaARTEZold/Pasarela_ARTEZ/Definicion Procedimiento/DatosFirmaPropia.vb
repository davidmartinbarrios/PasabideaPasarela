Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosFirmaPropia
        <XmlElement("Idnt_TramiteFirmaPropia", IsNullable:=True)>
        Public Property Idnt_TramiteFirmaPropia As String
    End Class
End Namespace