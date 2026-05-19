Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosFirmaCircuitoARTEZ
        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("Idnt_TramiteRevisionRechazoFirma")>
        Public Property Idnt_TramiteRevisionRechazoFirma As String

        <XmlElement("CircuitoARTEZ", IsNullable:=True)>
        Public Property CircuitoARTEZ As String
    End Class
End Namespace