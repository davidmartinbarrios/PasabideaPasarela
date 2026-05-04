Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DatosTramiteManual
        <XmlElement("Idnt_Elemento")>
        Public Property Idnt_Elemento As String

        <XmlElement("ModoCreacion", IsNullable:=True)>
        Public Property ModoCreacionXML As String

        <XmlElement("Observaciones", IsNullable:=True)>
        Public Property ObservacionesXML As String

        <XmlElement("DatosReponsableTramite", IsNullable:=True)>
        Public Property DatosReponsableTramite As DatosResponsableTarea

        <XmlIgnore>
        Friend ReadOnly Property DatosBasicosInformados As Boolean
            Get
                Dim informdados As Boolean = True

                If String.IsNullOrEmpty(Idnt_Elemento) Then
                    ' No tenemos Identificador de Trámite, así que no estám los Datos Básicos Informados
                    informdados = False

                Else
                    ' Tenemos Identificador de Trámite, así que comprobamos si tenemos objeto 'DatosReponsableTramite', ya que en caso afirmativo, deben estar los Datos Básicos informados
                    If Not IsNothing(DatosReponsableTramite) AndAlso Not DatosReponsableTramite.DatosBasicosInformados Then
                        ' El Objeto 'DatosReponsableTramite', así que tiene que tener los Datos Básicos Informados, así qie mp están los Datos Básicos Informados
                        informdados = False
                    End If
                End If

                Return informdados
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property ModoCreacion() As String
            Get
                Return ObtenerValorParaParametro(ModoCreacionXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property Observaciones() As String
            Get
                Return ObtenerValorParaParametro(ObservacionesXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property SistemaFuncional() As String
            Get
                Return DatosReponsableTramite.SistemaFuncional
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property GrupoUsuarios() As String
            Get
                Return DatosReponsableTramite.GrupoUsuarios
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property Usuario() As String
            Get
                Return DatosReponsableTramite.Usuario
            End Get
        End Property
    End Class
End Namespace