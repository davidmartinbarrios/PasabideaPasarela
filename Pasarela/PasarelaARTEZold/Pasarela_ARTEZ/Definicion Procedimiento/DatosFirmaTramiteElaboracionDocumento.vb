Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosFirmaTramiteElaboracionDocumento
        <XmlElement("TipoFirmaPorDefecto", IsNullable:=True)>
        Public Property TipoFirmaPorDefectoXML As String

        <XmlElement("VariableTipoFirma")>
        Public Property VariableTipoFirmaXML As String

        <XmlElement("VariableSolicitudFirma")>
        Public Property VariableSolicitudFirmaXML As String

        <XmlElement("VariableFechaFirma", IsNullable:=True)>
        Public Property VariableFechaFirmaXML As String

        <XmlIgnore>
        Friend ReadOnly Property VariableTipoFirma As String
            Get
                Return If(Not VariableTipoFirmaXML.StartsWith("@"), String.Format("@{0}", VariableTipoFirmaXML), VariableTipoFirmaXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property VariableSolicitudFirma As String
            Get
                Return If(Not VariableSolicitudFirmaXML.StartsWith("@"), String.Format("@{0}", VariableSolicitudFirmaXML), VariableSolicitudFirmaXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property VariableFechaFirma As String
            Get
                Return If(Not VariableFechaFirmaXML.StartsWith("@"), String.Format("@{0}", VariableFechaFirmaXML), VariableFechaFirmaXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property TipoFirmaPorDefecto As String
            Get
                Return ObtenerValorParaParametro(TipoFirmaPorDefectoXML)
            End Get
        End Property

        Friend Function DatosInformados(valorParametroHabilitarPestanaFirma As String) As Boolean
            If valorParametroHabilitarPestanaFirma.StartsWith("@") Then
                ' El valor del Parámetro Habilitar Pestaña Firma se recoge de una variables, así que es necesario recibir el parámetro 'TipoFirmaPorDefecto'
                Return If(Not String.IsNullOrEmpty(VariableTipoFirmaXML) AndAlso Not String.IsNullOrEmpty(VariableSolicitudFirmaXML) AndAlso Not String.IsNullOrEmpty(VariableFechaFirmaXML) AndAlso Not String.IsNullOrEmpty(TipoFirmaPorDefectoXML), True, False)

            Else
                ' El valor del Parámetro Habilitar Pestaña Firma es una constante
                Return If(Not String.IsNullOrEmpty(VariableTipoFirmaXML) AndAlso Not String.IsNullOrEmpty(VariableSolicitudFirmaXML) AndAlso Not String.IsNullOrEmpty(VariableFechaFirmaXML), True, False)
            End If

        End Function
    End Class
End Namespace