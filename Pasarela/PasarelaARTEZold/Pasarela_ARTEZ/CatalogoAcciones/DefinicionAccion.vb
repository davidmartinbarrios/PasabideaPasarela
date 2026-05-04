Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionAccion
        <XmlElement("Nombre")>
        Public Property Nombre As String

        <XmlElement("BaseDatos")>
        Public Property BaseDatos As String

        <XmlElement("NombreFlujo")>
        Public Property NombreFlujo As String

        <XmlElement("EjecucionDiferida")>
        Public Property EjecucionDiferida_XML As String

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosEntrada As List(Of ParametroEntrada)

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosSalida As List(Of ParametroSalida)

        <XmlIgnore>
        Friend ReadOnly Property MarcadaComoDiferidaEnDefinicion As Boolean
            Get
                Dim marcadaComoDiferida As Boolean = False

                If Not String.IsNullOrEmpty(EjecucionDiferida_XML) Then
                    If EjecucionDiferida_XML.Equals(Constantes.S) Then
                        marcadaComoDiferida = True
                    End If
                End If

                Return marcadaComoDiferida
            End Get
        End Property

        <XmlIgnore>
        Friend Property EjecucionDiferida As Boolean
    End Class
End Namespace