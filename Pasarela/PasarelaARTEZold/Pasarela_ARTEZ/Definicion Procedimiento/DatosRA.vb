Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosRA
        <XmlElement("TipoAcuerdo")>
        Public Property TipoAcuerdo As String

        <XmlElement("CodigoAsunto")>
        Public Property CodigoAsunto As String
    End Class
End Namespace