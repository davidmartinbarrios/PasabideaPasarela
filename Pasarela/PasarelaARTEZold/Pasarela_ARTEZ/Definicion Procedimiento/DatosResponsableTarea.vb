Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosResponsableTarea
        <XmlElement("SistemaFuncional")>
        Public Property SistemaFuncionalXML As String

        <XmlElement("GrupoUsuarios")>
        Public Property GrupoUsuariosXML As String

        <XmlElement("Usuario")>
        Public Property UsuarioXML As String

        <XmlIgnore>
        Friend ReadOnly Property DatosBasicosInformados As Boolean
            Get
                Return If((Not String.IsNullOrEmpty(SistemaFuncionalXML) AndAlso Not String.IsNullOrEmpty(GrupoUsuariosXML)) OrElse (String.IsNullOrEmpty(SistemaFuncionalXML) AndAlso String.IsNullOrEmpty(GrupoUsuariosXML) AndAlso Not String.IsNullOrEmpty(UsuarioXML)), True, False)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property SistemaFuncional() As String
            Get
                Return ObtenerValorParaParametro(SistemaFuncionalXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property GrupoUsuarios() As String
            Get
                Return ObtenerValorParaParametro(GrupoUsuariosXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property Usuario() As String
            Get
                Return ObtenerValorParaParametro(UsuarioXML)
            End Get
        End Property
    End Class
End Namespace