Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosTramiteAutomatico
        <XmlElement("Idnt_Elemento")>
        Public Property Idnt_Elemento As String

        <XmlIgnore>
        Friend ReadOnly Property DatosBasicosInformados As Boolean
            Get
                Return If(Not String.IsNullOrEmpty(Idnt_Elemento), True, False)
            End Get
        End Property
    End Class
End Namespace