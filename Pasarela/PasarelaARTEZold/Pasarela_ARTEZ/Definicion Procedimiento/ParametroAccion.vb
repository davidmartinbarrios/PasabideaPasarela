Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class ParametroAccion
        <XmlElement("Nombre")>
        Public Property Nombre As String

        <XmlElement("Valor")>
        Public Property Valor As String
    End Class
End Namespace