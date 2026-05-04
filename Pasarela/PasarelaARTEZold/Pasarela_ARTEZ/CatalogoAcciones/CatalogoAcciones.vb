Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlRoot("CatalogoAcciones")>
    Public NotInheritable Class CatalogoAcciones
        <XmlArrayItem("DefinicionAccion")>
        Public Property DefinicionesAcciones() As List(Of DefinicionAccion)
    End Class
End Namespace

