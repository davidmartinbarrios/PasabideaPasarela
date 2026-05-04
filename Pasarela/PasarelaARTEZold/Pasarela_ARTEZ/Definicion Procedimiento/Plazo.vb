Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class Plazo

#Region "Propiedades del XML"
        <XmlElement("TipoAlerta")>
        Public Property TipoAlertaXML As TipoAlerta

        <XmlElement("FechaReferencia")>
        Public Property FechaReferenciaXML As String

        <XmlElement("CodigoPlazo")>
        Public Property CodigoPlazoXML As String

        <XmlElement("FechaFinExacta", IsNullable:=True)>
        Public Property FechaFinExactaXML As String

        <XmlElement("TramiteDestino")>
        Public Property TramiteDestino As String

        <XmlElement("VariableFechaFinPlazo", IsNullable:=True)>
        Public Property VariableFechaFinPlazo As String
#End Region

#Region "Propiedades para la Generación del Flujo"
        <XmlIgnore()>
        Friend ReadOnly Property FechaReferencia As String
            Get
                Return If(String.IsNullOrEmpty(FechaReferenciaXML), "%DATE%", ObtenerValorParaParametro(FechaReferenciaXML))
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property CodigoPlazo As String
            Get
                Return If(String.IsNullOrEmpty(CodigoPlazoXML), String.Empty, ObtenerValorParaParametro(CodigoPlazoXML))
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property Tipo_Alerta As String
            Get
                Dim tipo As String = String.Empty

                Select Case TipoAlertaXML
                    Case TipoAlerta.Tramite
                        tipo = "1"

                    Case TipoAlerta.Resolucion
                        tipo = "2"
                End Select

                Return tipo
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property HayVariableFechaFinPlazo As Boolean
            Get
                Return If(String.IsNullOrEmpty(VariableFechaFinPlazo), False, True)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property NombreVariableFechaFinPlazo() As String
            Get
                Return If(Not VariableFechaFinPlazo.StartsWith("@"), String.Format("@{0}", VariableFechaFinPlazo), VariableFechaFinPlazo)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property FechaFinExacta As String
            Get
                Return If(String.IsNullOrEmpty(FechaFinExactaXML), String.Empty, ObtenerValorParaParametro(FechaFinExactaXML))
            End Get
        End Property
#End Region

#Region "Constructores"
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace