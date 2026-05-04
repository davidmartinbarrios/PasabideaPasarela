Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosFirmaDeducidaEjecucion
        <XmlElement("TipoFirma")>
        Public Property TipoFirma As String

        <XmlElement("Idnt_TramiteFirmaPropia", IsNullable:=True)>
        Public Property Idnt_TramiteFirmaPropia As String

        <XmlElement("Idnt_TramiteSeleccionFirmantes", IsNullable:=True)>
        Public Property Idnt_TramiteSeleccionFirmantes As String

        <XmlElement("Idnt_TramiteFirma")>
        Public Property Idnt_TramiteFirma As String

        <XmlElement("Idnt_TramiteRevisionRechazoFirma")>
        Public Property Idnt_TramiteRevisionRechazoFirma As String

        <XmlElement("NivelOrganico", IsNullable:=True)>
        Public Property NivelOrganico As String

        <XmlElement("FlujoFKNiveles", IsNullable:=True)>
        Public Property FlujoFK_Niveles As String

        <XmlElement("FlujoFKPredefinido", IsNullable:=True)>
        Public Property FlujoFK_Predefinido As String

        <XmlElement("Organico", IsNullable:=True)>
        Public Property Organico As String

        <XmlElement("TipoSello", IsNullable:=True)>
        Public Property TipoSello As String

        <XmlElement("Politica", IsNullable:=True)>
        Public Property Politica
    End Class
End Namespace