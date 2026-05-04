Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionTramiteARTEZ
        <XmlElement("Idnt_Elemento")>
        Public Property Idnt_Elemento As String

        <XmlElement("ModoCreacion", IsNullable:=True)>
        Public Property ModoCreacionXML As String

        <XmlElement("Observaciones", IsNullable:=True)>
        Public Property Observaciones As String

        <XmlElement("DatosReponsableTramite", IsNullable:=True)>
        Public Property DatosReponsableTramite As DatosResponsableTarea

        <XmlElement("DatosElaboracionDocumento", IsNullable:=True)>
        Public Property DatosElaboracionDocumento As DatosElaboracionDocumento

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosEspecificosCreacionTarea() As List(Of ParametroAccion)

        ' **********************************************************************************************************************************************************************************************************
        ' **********************************************************************************************************************************************************************************************************
        ' ESTAS PROPIEDADES DEBERÍAN DESAPARECER PARA CENTRALIZAR TRATAMIENTOS, PERO IMPLICA ADAPTAR TODOS LOS XML DE CREACIÓN
        ' **********************************************************************************************************************************************************************************************************
        ' **********************************************************************************************************************************************************************************************************
        <XmlElement("SistemaFuncional", IsNullable:=True)>
        Public Property SistemaFuncional As String

        <XmlElement("GrupoUsuarios", IsNullable:=True)>
        Public Property GrupoUsuarios As String

        <XmlElement("Usuario", IsNullable:=True)>
        Public Property Usuario As String
        ' **********************************************************************************************************************************************************************************************************
        ' **********************************************************************************************************************************************************************************************************

        <XmlIgnore>
        Friend ReadOnly Property ModoCreacion() As String
            Get
                Return ObtenerValorParaParametro(ModoCreacionXML)
            End Get
        End Property

        Friend Function ObtenerSistemaFuncional() As String
            Return If(Not IsNothing(DatosReponsableTramite), DatosReponsableTramite.SistemaFuncional, If(Not String.IsNullOrEmpty(SistemaFuncional), ObtenerValorParaParametro(SistemaFuncional), String.Empty))
        End Function

        Friend Function ObtenerGrupoUsuarios() As String
            Return If(Not IsNothing(DatosReponsableTramite), DatosReponsableTramite.GrupoUsuarios, If(Not String.IsNullOrEmpty(GrupoUsuarios), ObtenerValorParaParametro(GrupoUsuarios), String.Empty))
        End Function

        Friend Function ObtenerUsuario() As String
            Return If(Not IsNothing(DatosReponsableTramite), DatosReponsableTramite.Usuario, If(Not String.IsNullOrEmpty(Usuario), ObtenerValorParaParametro(Usuario), String.Empty))
        End Function
    End Class
End Namespace