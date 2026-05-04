Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class InicializacionVariable
        <XmlElement("VariableInicializar")>
        Public Property VariableInicializar As String

        <XmlElement("ValorFijo", IsNullable:=True)>
        Public Property ValorFijo As String

        <XmlElement("OrigenVariableValor", IsNullable:=True)>
        Public Property OrigenVariableValor As Origen_VariableValor?

        <XmlElement("NombrePaso", IsNullable:=True)>
        Public Property NombrePaso As String

        <XmlElement("VariableValor", IsNullable:=True)>
        Public Property VariableValor As String

        <XmlIgnore()>
        Friend ReadOnly Property InicializacionConValorFijo() As Boolean
            Get
                Return If(IsNothing(OrigenVariableValor) OrElse OrigenVariableValor.Equals(Origen_VariableValor.None), True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property OrigenValorFormularioTramite() As Boolean
            Get
                Return If(OrigenVariableValor.Equals(Origen_VariableValor.FormularioTramite), True, False)
            End Get
        End Property

        Friend Function ObtenerValor(ByRef datosGeneralesTramite As DatosGeneralesTramite) As String
            Dim valor As String = String.Empty

            If InicializacionConValorFijo Then
                ' Se va a realizar la Inicialización con un Valor Fijo
                valor = ValorFijo

            Else
                ' Se va a realizar la Inicialización con el Valor de una Variable de Flujo
                ' Determinamos el origen de la Variable
                Dim origen As String = String.Empty
                Select Case OrigenVariableValor
                    Case Origen_VariableValor.FormularioTramite
                        origen = datosGeneralesTramite.ObtenerNombrePathFROM

                    Case Origen_VariableValor.General
                        origen = "INICIO"
                End Select
                valor = String.Format("@{0}!{1}", origen, VariableValor)
            End If

            Return valor
        End Function
    End Class
End Namespace