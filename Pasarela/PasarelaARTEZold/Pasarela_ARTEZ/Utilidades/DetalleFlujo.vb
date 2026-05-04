Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml

Namespace Bizkaia.Pasarela.Utilidades
    <Serializable()>
    <XmlRoot("DetalleFlujo")>
    Public NotInheritable Class DetalleFlujo
        <XmlArrayItem("LineaDetalle")>
        Public Property LineasDetalle() As List(Of LineaDetalle)

        Public Sub AddLineasDetalle(nuevasLineasDetalle As List(Of LineaDetalle))
            If IsNothing(LineasDetalle) Then
                LineasDetalle = New List(Of LineaDetalle)
            End If

            LineasDetalle.AddRange(nuevasLineasDetalle)
        End Sub

        Public Function ToXML() As String
            Dim xml As String

            ' Definimos las propiedades y Objetos para quitar la declaración y los Namespace
            Dim propiedades As New XmlWriterSettings With {
                .Indent = True,
                .OmitXmlDeclaration = True
            }
            'Dim noNamespace As New XmlSerializerNamespaces(New XmlQualifiedName)

            Using stream As New StringWriter()
                Using writer As XmlWriter = XmlWriter.Create(stream, propiedades)
                    Dim serialitzador As New XmlSerializer(GetType(DetalleFlujo))
                    serialitzador.Serialize(writer, Me)
                    xml = stream.ToString()
                End Using
            End Using

            Return xml
        End Function
    End Class
End Namespace

