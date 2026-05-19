Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosTramitacionOrigen
        <XmlElement("Idnt_Procedimiento")>
        Public Property Idnt_Procedimiento As String

        <XmlElement("Descriptor_Procedimiento")>
        Public Property DescriptorProcedimiento As String

        <XmlElement("Instancia")>
        Public Property Instancia As String

        <XmlElement("Idnt_Elemento")>
        Public Property Idnt_Elemento As String
    End Class
End Namespace

