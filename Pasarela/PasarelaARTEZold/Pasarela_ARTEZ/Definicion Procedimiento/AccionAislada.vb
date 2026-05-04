Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class AccionAislada
        Private _definicionAccion As DefinicionAccion
        Private _parametrosEspecificos As Dictionary(Of String, String)

        <XmlElement("NombreAccion", IsNullable:=True)>
        Public Property NombreAccion As String

        <XmlElement("IncluirParametrosPlazo", IsNullable:=True)>
        Public Property IncluirParametrosPlazoXML As String

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosEntrada As List(Of Parametro)

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosSalida As List(Of Parametro)

        <XmlElement("Diferido", IsNullable:=True)>
        Public Property Diferido As String

        <XmlIgnore>
        Friend Property Orden As Integer

        <XmlIgnore()>
        Friend ReadOnly Property EsDiferido As Boolean
            Get
                Dim llamadaDiferida As Boolean = False

                ' Comprobamos si el Paso está marcado como Diferido en la Definición del Flujo
                If String.IsNullOrEmpty(Diferido) Then
                    ' El Paso no tiene informado si se debe Ejecutar de Manera Diferida o no, así que comprobamos la Definición de la Acción
                    llamadaDiferida = _definicionAccion.EjecucionDiferida

                Else
                    ' El Paso tiene informado si se debe Ejecutar de Manera Diferida o no, así que nos quedamos con el valor indicado en la Definición del Flujo
                    Select Case Diferido.ToUpper
                        Case Constantes.S
                            llamadaDiferida = True

                        Case Constantes.N
                            llamadaDiferida = False

                        Case Else
                            llamadaDiferida = False
                    End Select
                End If

                Return llamadaDiferida
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property IncluirParametrosPlazo As Boolean
            Get
                Dim incluirParametros As Boolean = False

                ' Comprobamos si tenemos informado el parámetro IncluirParametrosPlazoXML
                If Not String.IsNullOrEmpty(IncluirParametrosPlazoXML) Then
                    Select Case IncluirParametrosPlazoXML.ToUpper
                        Case Constantes.S
                            incluirParametros = True

                        Case Constantes.N
                            incluirParametros = False

                        Case Else
                            incluirParametros = False
                    End Select
                End If

                Return incluirParametros
            End Get
        End Property

#Region "Métodos Friend"
        Friend Function ObtenerLineasDetalle(tipoTratamientoPasos As TipoTratamientoPasos, identificadorFlujo As String, ByRef flowOrder As Integer, ByRef contadorReferencias As Integer, aplicacion As String, ByRef datosGeneralesTramite As DatosGeneralesTramite,
                                             ByRef definicionTramiteARTEZ As DefinicionTramiteARTEZ, ByRef catalogoAcciones As CatalogoAcciones, plazo As Plazo) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle)

            ' Creamos la Rama Paralela para llamar a la Acción Aislada
            lineasDetalle.AddRange(ObtenerLineasDetalleCreacionRamaParalela(flowOrder, datosGeneralesTramite))

            ' Creamos la llamada a la Acción Aislada
            lineasDetalle.AddRange(ObtenerLineasDetalleLlamadaAccionAislada(tipoTratamientoPasos, identificadorFlujo, flowOrder, contadorReferencias, aplicacion, datosGeneralesTramite, definicionTramiteARTEZ, catalogoAcciones, plazo))

            Return lineasDetalle
        End Function
#End Region

#Region "Métodos Privados"
        Private Function ObtenerLineasDetalleCreacionRamaParalela(ByRef flowOrder As Integer, ByRef datosGenerales As DatosGeneralesTramite) As List(Of LineaDetalle)
            flowOrder += 100

            Return UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(flowOrder, path:=ObtenerNombreCreacionRamaParalela(datosGenerales), tieneParametro_STATE:=False, valorParametro_STATE:=String.Empty, valorParametro_SUBMIT:="FLY", tipoAgrupacion:=String.Empty,
                                                                                          level:=2, comentario:=String.Empty)
        End Function

        Private Function ObtenerLineasDetalleLlamadaAccionAislada(tipoTratamientoPasos As TipoTratamientoPasos, identificadorFlujo As String, ByRef flowOrder As Integer, ByRef contadorReferencias As Integer, aplicacion As String, ByRef datosGeneralesTramite As DatosGeneralesTramite,
                                                                  ByRef definicionTramiteARTEZ As DefinicionTramiteARTEZ, ByRef catalogoAcciones As CatalogoAcciones, plazo As Plazo) As List(Of LineaDetalle)
            ' Obtenemos la Definición de la Acción
            ObtenerDefinicionAccion(catalogoAcciones)

            ' Cargamos los Parámetros Específicos del Paso
            ObtenerParametrosEspecificos(plazo)

            ' Comprobamos si estamos tratando un Inicio de Tramitación, un Bloque de Tramitación Común, un Trámite ARTEZ o un Trámite Ficticio para determinar los valores de los datos de ARTEZ
            Dim identificadorTramite As String = String.Empty
            Dim sistemaFuncional As String = String.Empty
            Dim grupoUsuarios As String = String.Empty
            Dim usuario As String = String.Empty
            Dim ordenTramite As Integer
            Select Case tipoTratamientoPasos
                Case TipoTratamientoPasos.InicioTramitacion
                    ' Estamos tratanod un Inicio de Tramitación
                    ordenTramite = 0

                Case TipoTratamientoPasos.TramiteARTEZ
                    ' Estamos tratando un Bloque de Tramitación común o un Trámite ARTEZ
                    ordenTramite = datosGeneralesTramite.OrdenTramite
                    If Not datosGeneralesTramite.EsBloqueTramitacionComun Then
                        identificadorTramite = definicionTramiteARTEZ.Idnt_Elemento
                        sistemaFuncional = definicionTramiteARTEZ.ObtenerSistemaFuncional
                        grupoUsuarios = definicionTramiteARTEZ.ObtenerGrupoUsuarios
                        usuario = definicionTramiteARTEZ.ObtenerUsuario
                    End If

                Case TipoTratamientoPasos.TramiteFicticio
                    ' Estamos tratando un Tramitación Ficticio
                    ordenTramite = datosGeneralesTramite.OrdenTramite
            End Select

            ' Inciializamos las Líneas de Detalle para la Creación de la Llamada a la Acción Aislada
            flowOrder += 100
            contadorReferencias += 1
            Return UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(identificadorFlujo, flowOrder, path:=ObtenerNombreLlamadaAccionAislada(ordenTramite), baseDatosAccion:=_definicionAccion.BaseDatos,
                                                                                                 accion:=_definicionAccion.NombreFlujo, aplicacion:=aplicacion, contadorReferencia:=contadorReferencias, level:=3, diferido:=EsDiferido,
                                                                                                 idnt_Elemento:=identificadorTramite, sistemaFuncional:=sistemaFuncional, grupoUsuarios:=grupoUsuarios, usuario:=usuario, baseDatosRetorno:=String.Empty,
                                                                                                 pathRetorno:=String.Empty, comentario:=String.Empty, parametrosEspecificos:=_parametrosEspecificos, habilitado:=True)
        End Function

        Private Function ObtenerNombreCreacionRamaParalela(ByRef datosGenerales As DatosGeneralesTramite) As String
            Return String.Format("ACCION_AISLADA_T_{0:000}_{1}", datosGenerales.OrdenTramite, Orden)
        End Function

        Private Sub ObtenerDefinicionAccion(ByRef catalogoAcciones As CatalogoAcciones)
            _definicionAccion = catalogoAcciones.DefinicionesAcciones.Find(Function(x) x.Nombre.Equals(NombreAccion))

            ' Comprobamos si hemos obtenido la definición de la Acción
            If IsNothing(_definicionAccion) Then
                Throw New Exception(String.Format("No se ha recuperado la Definición de la Acción Aislada '{0}'", NombreAccion))
            End If
        End Sub

        Private Sub ObtenerParametrosEspecificos(plazo As Plazo)
            ' Comprobamos si el trámite tiene Plazo
            If Not IsNothing(plazo) AndAlso IncluirParametrosPlazo Then
                ' El trámite tiene Plazo, así que insertamos los Parámetros del Plazo por Defecto
                ' Fecha de Referencia para el Cálculo del Plazo
                AddParametroEspecifico("@FECHA_INICIO_IN", plazo.FechaReferencia)
                ' Código de Plazo a Aplicar
                AddParametroEspecifico("@TIPO_PLAZO_IN", plazo.CodigoPlazo)
            End If

            ' Parámetros de Entrada
            If Not IsNothing(_definicionAccion.ParametrosEntrada) AndAlso _definicionAccion.ParametrosEntrada.Count > 0 Then
                ' Nos recorremos los Parámetros de Entrada de la Definición para casarlos con los Parámetros de Entrada del Paso
                Dim numeroParametro As Integer = 1
                For Each parametro As ParametroEntrada In _definicionAccion.ParametrosEntrada
                    ' Obtenemos el valor para el Parámetro
                    parametro.Valor = String.Empty
                    If Not IsNothing(ParametrosEntrada) OrElse ParametrosEntrada.Count > 0 Then
                        ' Comprobamos si el Parámetro que se está tratando tiene un valor
                        If numeroParametro <= ParametrosEntrada.Count Then
                            parametro.Valor = ParametrosEntrada.Item(numeroParametro - 1).Valor
                        End If
                    End If

                    ' Añadimos el Parámetro Específico
                    AddParametroEspecifico(parametro.Nombre, parametro.Valor)

                    ' Aumentamos el Número de Parámetro tratado
                    numeroParametro += 1
                Next
            End If

            ' Parámetros de Salida
            If Not IsNothing(_definicionAccion.ParametrosSalida) AndAlso _definicionAccion.ParametrosSalida.Count > 0 Then
                ' Nos recorremos los Parámetros de Entrada de la Definición para casarlos con los Parámetros de Entrada del Paso
                Dim numeroParametro As Integer = 1
                For Each parametro As ParametroSalida In _definicionAccion.ParametrosSalida
                    Dim valorParametro As String = String.Empty
                    ' Obtenemos el valor para el Parámetro
                    parametro.Valor = String.Empty
                    If Not IsNothing(ParametrosSalida) OrElse ParametrosSalida.Count > 0 Then
                        ' Comprobamos si el Parámetro que se está tratando tiene un valor
                        If numeroParametro <= ParametrosSalida.Count Then
                            parametro.Valor = ParametrosSalida.Item(numeroParametro - 1).Valor
                        End If
                    End If

                    ' Añadimos el Parámetro Específico
                    AddParametroEspecifico(parametro.Nombre, parametro.Valor)

                    ' Aumentamos el Número de Parámetro tratado
                    numeroParametro += 1
                Next
            End If
        End Sub

        Private Sub AddParametroEspecifico(nombreParametro As String, valorParametro As String)
            If IsNothing(_parametrosEspecificos) Then
                _parametrosEspecificos = New Dictionary(Of String, String)
            End If
            _parametrosEspecificos.Add(nombreParametro, valorParametro)
        End Sub

        Private Function ObtenerNombreLlamadaAccionAislada(ordenTramite As Integer?) As String
            ' Obtenemos el Nombre del Paso por defecto en el Trámite
            Dim nombrePasoTipoAccion As String = String.Format("{0}_{1}_{2}", _definicionAccion.NombreFlujo, ordenTramite, Orden)

            ' Comprobamos si supera el número máximo de caracteres permitidos para un PATH
            If Utilidades.Utilidades.SuperaNumeroMaximoCaracteresPATH(nombrePasoTipoAccion) Then
                nombrePasoTipoAccion = String.Format("{0}_{1}_{2}", _definicionAccion.NombreFlujo.Substring(0, _definicionAccion.NombreFlujo.Length - Utilidades.Utilidades.NumeroCaracteresExtraPATH(nombrePasoTipoAccion)), ordenTramite, Orden)
            End If

            Return nombrePasoTipoAccion
        End Function
#End Region
    End Class



    'flowOrder += 100
    '                lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(flowOrder, path:=String.Format("SALIDA_T_{0:000}_{1}", .OrdenTramite, contadorSalida), tieneParametro_STATE:=False, valorParametro_STATE:=String.Empty,
    '                                                                                                              valorParametro_SUBMIT:="FLY", tipoAgrupacion:=String.Empty, level:=2, comentario:=Descripcion))



End Namespace