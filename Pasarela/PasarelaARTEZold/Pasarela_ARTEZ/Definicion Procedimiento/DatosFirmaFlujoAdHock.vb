Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosFirmaFlujoAdHock
        <XmlElement("Idnt_TramiteSeleccionFirmantes", IsNullable:=True)>
        Public Property Idnt_TramiteSeleccionFirmantes As String

        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("Idnt_TramiteRevisionRechazoFirma")>
        Public Property Idnt_TramiteRevisionRechazoFirma As String
    End Class
End Namespace