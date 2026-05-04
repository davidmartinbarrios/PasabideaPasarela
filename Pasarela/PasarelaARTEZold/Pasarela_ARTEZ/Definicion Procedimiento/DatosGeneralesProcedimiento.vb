Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosgeneralesProcedimiento
        <XmlElement("IdentificadorFlujo", IsNullable:=False)>
        Public Property IdentificadorFlujo() As String

        <XmlElement("NombreFlujo", IsNullable:=False)>
        Public Property NombreFlujo() As String

        <XmlElement("Descripcion", IsNullable:=False)>
        Public Property Descripcion() As String

        <XmlIgnore()>
        Friend Property Version As Integer
    End Class
End Namespace