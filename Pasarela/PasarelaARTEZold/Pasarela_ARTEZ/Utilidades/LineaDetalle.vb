Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela.Utilidades
    <Serializable()>
    <XmlType>
    Public NotInheritable Class LineaDetalle
        <XmlAttribute("FlowOrder")>
        Public Property FlowOrder As Integer

        <XmlAttribute("Id")>
        Public Property Id As Integer

        <XmlAttribute("Action")>
        Public Property Action As String

        <XmlAttribute("Path")>
        Public Property Path As String

        <XmlAttribute("Param")>
        Public Property Param As String

        <XmlAttribute("Value")>
        Public Property Value As String

        <XmlAttribute("Comments")>
        Public Property Comentario As String

        Public Sub New()

        End Sub

        Public Sub New(flowOrder As Integer, id As Integer, action As String, path As String, param As String, value As String, comentario As String)
            Me.FlowOrder = flowOrder
            Me.Id = id
            Me.Action = action
            Me.Path = path
            Me.Param = param
            Me.Value = value
            Me.Comentario = comentario
        End Sub
    End Class
End Namespace