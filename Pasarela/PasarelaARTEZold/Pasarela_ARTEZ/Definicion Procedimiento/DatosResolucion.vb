Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosResolucion
        <XmlElement("FechaAcuerdo")>
        Public Property FechaAcuerdo As String

        <XmlElement("AnioAcuerdo")>
        Public Property AnioAcuerdo As String

        <XmlElement("NumeroAcuerdo")>
        Public Property NumeroAcuerdo As String
    End Class
End Namespace