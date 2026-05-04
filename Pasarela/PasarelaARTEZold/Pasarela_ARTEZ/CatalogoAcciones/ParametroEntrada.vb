Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class ParametroEntrada
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
                'Return If(String.IsNullOrEmpty(_valor), String.Empty, If(Not _valor.StartsWith("@"), String.Format("@INICIO!{0}", _valor), _valor))
                Return If(String.IsNullOrEmpty(_valor), String.Empty, If(_valor.StartsWith("@"), String.Format("@INICIO!{0}", _valor.Replace("@", String.Empty)), _valor))
            End Get
            Set(value As String)
                _valor = value
            End Set
        End Property

        'Friend Function ObtenerNombre() As strning
        'Return If(Not _nombre.StartsWith("@"), String.Format("@{0}", _nombre), _nombre)
        'End Function
    End Class
End Namespace