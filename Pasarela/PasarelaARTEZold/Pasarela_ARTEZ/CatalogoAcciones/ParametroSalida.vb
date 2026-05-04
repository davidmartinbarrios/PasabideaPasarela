Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class ParametroSalida
        Private _nombre As String
        Private _valor As String

        <XmlText()>
        Public Property Nombre As String
            Get
                Return If(Not _nombre.StartsWith("@"), String.Format("@{0}", _nombre), _nombre)
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        <XmlIgnore>
        Friend Property Valor As String
            Get
                Return If(String.IsNullOrEmpty(_valor), _nombre.Replace("@", String.Empty), _valor.Replace("@", String.Empty))
            End Get
            Set(value As String)
                _valor = value
            End Set
        End Property
    End Class
End Namespace