Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class Paso
        'Private _sentenciasSQLCreacion As List(Of SentenciaSQL)
        Private _lineasDetalle As List(Of LineaDetalle)
        Private _definicionAccion As DefinicionAccion
        Private _parametrosEspecificos As Dictionary(Of String, String)

        <XmlIgnore()>
        Friend Property Numero As Integer

        <XmlIgnore()>
        Friend Property EsUltimoPaso As Boolean = False

        <XmlElement("Tipo")>
        Public Property Tipo As TipoPaso

        <XmlElement("NombrePaso")>
        Public Property Nombre As String

        <XmlElement("Diferido", IsNullable:=True)>
        Public Property Diferido As String

        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        ' PARÁMETROS PARA EL TIPO DE PASO ACCIÓN
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        <XmlElement("NombreAccion", IsNullable:=True)>
        Public Property NombreAccion As String

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosEntrada As List(Of Parametro)

        <XmlArrayItem("Parametro", IsNullable:=True)>
        Public Property ParametrosSalida As List(Of Parametro)

        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        ' PARÁMTROS PARA EL TIPO DE PASO INICIALIZACION_VARIABLES
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        <XmlElement("VariablesGenerales", IsNullable:=True)>
        Public Property VariablesGenerales As String

        <XmlArrayItem("InicializacionVariable", IsNullable:=True)>
        Public Property InicializacionesVariables As List(Of InicializacionVariable)

        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        ' PARÁMETROS PARA EL TIPO DE PASO EVALUACIÓN
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        <XmlElement("Condicion", IsNullable:=True)>
        Public Property Condicion As Condicion

        <XmlElement("DescripcionPasoVerdadero", IsNullable:=True)>
        Public Property DescripcionPasoTRUE As String

        <XmlElement("PasoVerdadero", IsNullable:=True)>
        Public Property PasoTRUE As String

        <XmlElement("PasoFalso", IsNullable:=True)>
        Public Property PasoFALSO As String

        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        ' PARÁMETROS PARA EL TIPO DE PASO SALTO
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        <XmlElement("PasoDestino", IsNullable:=True)>
        Public Property PasoDestino As String

        '<XmlIgnore()>
        'Friend Property NumeroPasoInicializacion As Integer?

        '<XmlIgnore()>
        'Friend Property NumeroPasoSalto As Integer?

        <XmlIgnore()>
        Friend ReadOnly Property InicializacionVariablesGenerales As Boolean
            Get
                Return If(VariablesGenerales.Equals(Constantes.S), True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property LineasDetalle As List(Of LineaDetalle)
            Get
                Return _lineasDetalle
            End Get
        End Property

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

        Friend Function ObtenerNombrePaso(ordenTramite As Integer) As String
            Return ObtenerNombrePaso(ordenTramite, Nothing)
        End Function

        Friend Function ObtenerNombrePaso(ordenTramite As Integer, ByRef catalogoAcciones As CatalogoAcciones) As String
            Dim nombrePaso As String = String.Empty

            Select Case Tipo
                Case TipoPaso.Accion
                    ' Obtenemos la Definición de la Acción
                    ObtenerDefinicionAccion(catalogoAcciones)
                    ' Obtenemos el Nombre del Paso por defecto en el flujo
                    nombrePaso = ObtenerNombrePasoTipoAccion(ordenTramite)

                Case TipoPaso.InicializacionVariables
                    nombrePaso = ObtenerNombrePasoTipoInicializacion(ordenTramite)

                Case TipoPaso.Evaluacion
                    nombrePaso = ObtenerNombrePasoTipoEvaluacion(ordenTramite)

                Case TipoPaso.Salto
                    nombrePaso = ObtenerNombrePasoTipoSalto(ordenTramite)

                Case TipoPaso.Parada
                    nombrePaso = ObtenerNombrePasoTipoParada(ordenTramite)
            End Select

            Return nombrePaso
        End Function

        Friend Sub ObtenerLineasDetalleTramite(ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite, ByRef definicionTramiteARTEZ As DefinicionTramiteARTEZ, baseDatosFlujoPrincipal As String,
                                               identificadorFlujo As String, flowOrder As Integer, aplicacion As String, contadorReferencias As Integer)
            ObtenerLineasDetalle(TipoTratamientoPasos.TramiteARTEZ, Nothing, catalogoAcciones, catalogoPasos, datosGeneralesTramite, definicionTramiteARTEZ, baseDatosFlujoPrincipal, identificadorFlujo, flowOrder, aplicacion, contadorReferencias)
        End Sub

        Friend Sub ObtenerLineasDetalletramiteFicticio(ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), pathSiguientePaso As String, ByRef datosGeneralesTramite As DatosGeneralesTramite, baseDatosFlujoPrincipal As String, identificadorFlujo As String,
                                                       flowOrder As Integer, aplicacion As String, contadorReferencias As Integer)
            ObtenerLineasDetalle(TipoTratamientoPasos.TramiteFicticio, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite, Nothing, baseDatosFlujoPrincipal, identificadorFlujo, flowOrder, aplicacion, contadorReferencias)
        End Sub

        Friend Sub ObtenerLineasDetalleIniciarTramitacion(ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), pathSiguientePaso As String, baseDatosFlujoPrincipal As String, identificadorFlujo As String, flowOrder As Integer, aplicacion As String,
                                                          contadorReferencias As Integer)
            ObtenerLineasDetalle(TipoTratamientoPasos.InicioTramitacion, pathSiguientePaso, catalogoAcciones, catalogoPasos, Nothing, Nothing, baseDatosFlujoPrincipal, identificadorFlujo, flowOrder, aplicacion, contadorReferencias)
        End Sub

        Friend Function EsPasoConReferencia() As Boolean
            Dim pasoConReferencia As Boolean = False

            Select Case Tipo
                Case TipoPaso.Accion
                    pasoConReferencia = True

                Case TipoPaso.InicializacionVariables
                    pasoConReferencia = False

                Case TipoPaso.Evaluacion
                    pasoConReferencia = False

                Case TipoPaso.Salto
                    pasoConReferencia = False
            End Select

            Return pasoConReferencia
        End Function

        'Friend Function EsPasoConJUMP() As Boolean
        '    Dim pasoConJUMP As Boolean = False

        '    Select Case Tipo
        '        Case TipoPaso.Accion
        '            pasoConJUMP = False

        '        Case TipoPaso.InicializacionVariables
        '            pasoConJUMP = False

        '        Case TipoPaso.Evaluacion
        '            pasoConJUMP = False
        '    End Select

        '    Return pasoConJUMP
        'End Function

        Friend Function EsPasoInicializacionVariables() As Boolean
            Return If(Tipo.Equals(TipoPaso.InicializacionVariables), True, False)
        End Function

        Friend Function ObtenerVariablesOrigenValorPasoForm() As List(Of String)
            Dim variablesOrigenValorPasoForm As List(Of String) = Nothing

            For Each variable As InicializacionVariable In InicializacionesVariables
                If variable.OrigenValorFormularioTramite AndAlso Not variable.VariableValor.Equals("RESPUESTA") AndAlso Not variable.VariableValor.Equals("@RESPUESTA") Then
                    If IsNothing(variablesOrigenValorPasoForm) Then
                        variablesOrigenValorPasoForm = New List(Of String)
                    End If
                    variablesOrigenValorPasoForm.Add(variable.VariableValor)
                End If
            Next

            Return variablesOrigenValorPasoForm
        End Function

#Region "Métodos Privados"
        Private Function ObtenerNombrePasoTipoAccion(ordenTramite As Integer?) As String
            ' Obtenemos el Nombre del Paso por defecto en el Trámite
            Dim nombrePasoTipoAccion As String = String.Format("{0}_{1}_{2}", _definicionAccion.NombreFlujo, ordenTramite, Numero)

            ' Comprobamos si supera el número máximo de caracteres permitidos para un PATH
            If Utilidades.Utilidades.SuperaNumeroMaximoCaracteresPATH(nombrePasoTipoAccion) Then
                nombrePasoTipoAccion = String.Format("{0}_{1}_{2}", _definicionAccion.NombreFlujo.Substring(0, _definicionAccion.NombreFlujo.Length - Utilidades.Utilidades.NumeroCaracteresExtraPATH(nombrePasoTipoAccion)), ordenTramite, Numero)
            End If

            Return nombrePasoTipoAccion
        End Function

        Private Function ObtenerNombrePasoTipoInicializacion(ordenTramite As Integer?) As String
            'Return String.Format("LET INICIALIZACION_{0:0000}", NumeroPasoInicializacion)
            Return String.Format("INICIALIZACION_{0}_{1}", ordenTramite, Numero)
        End Function

        Private Function ObtenerNombrePasoTipoEvaluacion(ordenTramite As Integer?) As String
            Return String.Format("EVALUAR_{0}_{1}", ordenTramite, Numero)
        End Function

        Private Function ObtenerNombrePasoTipoSalto(ordenTramite As Integer?) As String
            Return String.Format("JUMP_{0}_{1}", ordenTramite, Numero)
        End Function

        Private Function ObtenerNombrePasoTipoParada(ordenTramite As Integer?) As String
            Return String.Format("FRM_PARADA_{0}_{1}", ordenTramite, Numero)
        End Function

        Private Sub ObtenerDefinicionAccion(ByRef catalogoAcciones As CatalogoAcciones)
            ' Comprobamos si ya hemos obtenido la Definición de la Acción
            If IsNothing(_definicionAccion) Then
                _definicionAccion = catalogoAcciones.DefinicionesAcciones.Find(Function(x) x.Nombre.Equals(NombreAccion))

                ' Comprobamos si hemos obtenido la definición de la Acción
                If IsNothing(_definicionAccion) Then
                    Throw New Exception(String.Format("No se ha recuperado la Definición de la Acción '{0}'", NombreAccion))
                End If
            End If
        End Sub

        Private Sub ObtenerLineasDetalle(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite,
                                         ByRef definicionTramiteARTEZ As DefinicionTramiteARTEZ, baseDatosFlujoPrincipal As String, identificadorFlujo As String, flowOrder As Integer, aplicacion As String, contadorReferencias As Integer)
            Select Case Tipo
                Case TipoPaso.Accion
                    ' Obtenemos la Definición de la Acción
                    ObtenerDefinicionAccion(catalogoAcciones)
                    ' Obtenemos las Sentencias SQL para crear el paso
                    ObtenerLineasDetallePasoAccion(tipoTratamientoPasos, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite, definicionTramiteARTEZ, baseDatosFlujoPrincipal, identificadorFlujo, flowOrder, aplicacion, contadorReferencias)

                Case TipoPaso.InicializacionVariables
                    ' Obtenemos las Sentencias SQL para crear el paso
                    ObtenerLineasDetallePasoInicializacionVariables(tipoTratamientoPasos, datosGeneralesTramite, flowOrder)

                Case TipoPaso.Evaluacion
                    ' Obtenemos las Sentencias SQL para crear el paso
                    ObtenerLineasDetallePasoEvaluacion(tipoTratamientoPasos, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite, flowOrder)

                Case TipoPaso.Salto
                    ' Obtenemos las Sentencias SQL para crear el paso
                    ObtenerLineasDetallePasoSalto(tipoTratamientoPasos, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite, flowOrder)

                Case TipoPaso.Parada
                    ' Obtenemos las Sentencias SQL para crear el paso
                    ObtenerLineasDetallePasoParada(tipoTratamientoPasos, datosGeneralesTramite, flowOrder)
            End Select
        End Sub

        Private Sub ObtenerLineasDetallePasoAccion(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite,
                                                   ByRef definicionTramiteARTEZ As DefinicionTramiteARTEZ, baseDatosFlujoPrincipal As String, identificadorFlujo As String, flowOrder As Integer, aplicacion As String, contadorReferencias As Integer)
            ' Cargamos los Parámetros Específicos del Paso
            ObtenerParametrosEspecificos()

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

            ' Obtenemos el Path de Retorno para el paso, diferenciando si estamos tratando el último paso o no
            Dim pathRetorno As String = String.Empty
            If EsUltimoPaso Then
                ' Estamos tratando el último paso
                Select Case tipoTratamientoPasos
                    Case TipoTratamientoPasos.InicioTramitacion
                        ' Estamos tratando el último paso de la Inicialización de la Tramitación, así que el Path de Retorno será el primer trámite del Flujo
                        pathRetorno = pathSiguientePaso

                    Case TipoTratamientoPasos.TramiteARTEZ
                        ' Estamos tratando el último paso de un Trámite o Bloque de Tramitación Común, así que hay que ver el tipo de trámite que estamos tratando para que el retorno vaya a la acción correspondiente
                        pathRetorno = If(datosGeneralesTramite.EsBloqueTramitacionComun, datosGeneralesTramite.PathSalidaBloqueTramitacionComun, datosGeneralesTramite.ObtenerNombrePathAccionGestionFinalizarTarea)

                    Case TipoTratamientoPasos.TramiteFicticio
                        ' Estamos tratanod el último paso de un Trámite Ficticio, así que el Path de Retonrno será el tratamiento de Salida del Trámite
                        pathRetorno = pathSiguientePaso
                End Select

            Else
                ' No estamos tratando el último paso, así que el retorno es al siguinte Paso
                pathRetorno = catalogoPasos.Item(catalogoPasos.IndexOf(Me) + 1).ObtenerNombrePaso(ordenTramite, catalogoAcciones)
            End If

            ' Inciializamos las Líneas de Detalle para la Creación del Paso
            _lineasDetalle = New List(Of LineaDetalle)
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(identificadorFlujo, flowOrder, path:=ObtenerNombrePaso(ordenTramite, catalogoAcciones), baseDatosAccion:=_definicionAccion.BaseDatos,
                                                                                                                  accion:=_definicionAccion.NombreFlujo, aplicacion:=aplicacion, contadorReferencia:=contadorReferencias, level:=2, diferido:=EsDiferido,
                                                                                                                  idnt_Elemento:=identificadorTramite, sistemaFuncional:=sistemaFuncional, grupoUsuarios:=grupoUsuarios, usuario:=usuario, baseDatosRetorno:=baseDatosFlujoPrincipal,
                                                                                                                  pathRetorno:=pathRetorno, comentario:=String.Empty, parametrosEspecificos:=_parametrosEspecificos, habilitado:=True))
        End Sub

        Private Sub ObtenerLineasDetallePasoInicializacionVariables(tipoTratamientoPasos As TipoTratamientoPasos, ByRef datosGeneralesTramite As DatosGeneralesTramite, flowOrder As Integer)
            ' Obtenemos el Listado de Variables a Inicializar si nos han informado
            If Not IsNothing(InicializacionesVariables) AndAlso InicializacionesVariables.Count > 0 Then
                ' Comprobamos si estamos en la Inicialización de la Tramitación, ya que en estos casos el Origen de la Variable no podría ser un Formulario de Trámite
                If tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion) OrElse tipoTratamientoPasos.Equals(TipoTratamientoPasos.TramiteFicticio) Then
                    For Each inicializacionVariable As InicializacionVariable In InicializacionesVariables
                        If inicializacionVariable.OrigenVariableValor.Equals(Origen_VariableValor.FormularioTramite) Then
                            Throw New Exception(String.Format("El Paso '{0}' es de Tipo 'Inicialización de Variables', y tiene una Inicialización cuyo origen es Formulario Trámite, en un Trámite que no tiene paso FORM.", Nombre))
                        End If
                    Next
                End If

                Dim listadoVariables As New Dictionary(Of String, String)

                For Each inicializacionVariable As InicializacionVariable In InicializacionesVariables
                    listadoVariables.Add(String.Format("{0}{1}", If(Not inicializacionVariable.VariableInicializar.StartsWith("@"), "@", String.Empty), inicializacionVariable.VariableInicializar), inicializacionVariable.ObtenerValor(datosGeneralesTramite))
                Next

                ' Inicializamos las Líneas de Detalle para la Cración del Paso
                _lineasDetalle = New List(Of LineaDetalle)
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LETVAR(flowOrder, path:=ObtenerNombrePaso(If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite)),
                                                                                                                level:=2, listadoVariables:=listadoVariables, deInicio:=InicializacionVariablesGenerales))
            End If
        End Sub

        Private Sub ObtenerLineasDetallePasoSalto(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite, flowOrder As Integer)
            ' Obtenemos el Ornde de Trámite con el que trabajar
            Dim ordenTramite As Integer = If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite)

            ' Comprobamos si tenemos un Paso Destino Asociado al paso de Tipo Salto
            Dim pathSalto As String = String.Empty
            If Not String.IsNullOrEmpty(PasoDestino) Then
                ' El paso de Tipo Salto tiene un Paso Destino informado, así que saltamos a ese paso
                pathSalto = catalogoPasos.Find(Function(x) Not String.IsNullOrEmpty(x.Nombre) AndAlso x.Nombre.Equals(PasoDestino)).ObtenerNombrePaso(ordenTramite, catalogoAcciones)

            Else
                ' El paso de Tipo Salto no tiene un Paso Destino infomrado, así que tenemos que deducirlo
                Select Case tipoTratamientoPasos
                    Case TipoTratamientoPasos.InicioTramitacion
                        ' Saltamos al Primer Trámite
                        pathSalto = pathSiguientePaso

                    Case TipoTratamientoPasos.TramiteARTEZ
                        ' Hay que ver el tipo de trámite que estamos tratando para que la redirección vaya a la acción correspondiente
                        pathSalto = If(datosGeneralesTramite.EsBloqueTramitacionComun, datosGeneralesTramite.PathSalidaBloqueTramitacionComun, datosGeneralesTramite.ObtenerNombrePathAccionGestionFinalizarTarea)

                    Case TipoTratamientoPasos.TramiteFicticio
                        ' Saltamos al Tratamiento de Salida del Trámite Ficticio
                        pathSalto = pathSiguientePaso
                End Select
            End If

            ' Inciializamos las Líneas de Detalle para la Creación del Paso
            _lineasDetalle = New List(Of LineaDetalle)
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(flowOrder, path:=ObtenerNombrePaso(ordenTramite), puntoSalto:=pathSalto, level:=2, comentario:=String.Empty))
        End Sub

        Private Sub ObtenerLineasDetallePasoEvaluacion(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite, flowOrder As Integer)
            ' Comprobamos si el Paso de Evaluación es Correcto
            If Not EsPasoEvaluacionCorrecto(tipoTratamientoPasos) Then
                Throw New Exception(String.Format("El paso de tipo Evaluación '{0}' no es Correcto, porque no pueden estar vacíos los dos saltos.", Nombre))
            End If

            ' Obtenemos el Ornde de Trámite con el que trabajar
            Dim ordenTramite As Integer = If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite)

            ' Inicializamos las Líneas de Detalle para la Cración del Paso
            _lineasDetalle = New List(Of LineaDetalle)
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_IF(flowOrder, path:=ObtenerNombrePaso(ordenTramite, Nothing),
                                                                                                        valorParametroVAR1:=Condicion.ObtenerValorOperando1(datosGeneralesTramite, ordenTramite),
                                                                                                        valorParametroVAR2:=Condicion.ObtenerValorOperando2, condicion:=Condicion.ObtenerOperador, tipoComparacion:="STRING",
                                                                                                        saltoTRUE:=ObtenerNombrePasoTRUE(tipoTratamientoPasos, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite),
                                                                                                        saltoFALSE:=ObtenerNombrePasoFALSE(tipoTratamientoPasos, pathSiguientePaso, catalogoAcciones, catalogoPasos, datosGeneralesTramite),
                                                                                                        level:=2, comentario:=DescripcionPasoTRUE))
        End Sub

        Private Sub ObtenerLineasDetallePasoParada(tipoTratamientoPasos As TipoTratamientoPasos, ByRef datosGeneralesTramite As DatosGeneralesTramite, flowOrder As Integer)
            ' Obtenemos el Ornde de Trámite con el que trabajar
            Dim ordenTramite As Integer = If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite)

            ' Inicializamos las Líneas de Detalle para la Cración del Paso
            _lineasDetalle = New List(Of LineaDetalle)
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_FORM(flowOrder, path:=ObtenerNombrePaso(ordenTramite, Nothing), level:=2, esTramite:=False, idnt_Tramite:=String.Empty, tipoEjecucion:="M",
                                                                                                          listadoParamtrosEspecificos:=Nothing))
        End Sub

        Private Function EsPasoEvaluacionCorrecto(tipoTratamientoPasos As TipoTratamientoPasos) As Boolean
            Return If((tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion) OrElse tipoTratamientoPasos.Equals(TipoTratamientoPasos.TramiteFicticio)) AndAlso String.IsNullOrEmpty(Condicion.Operando1),
                        False,
                        If(String.IsNullOrEmpty(PasoTRUE) AndAlso String.IsNullOrEmpty(PasoFALSO), False, True))
        End Function

        Private Function ObtenerNombrePasoTRUE(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite) As String
            Dim nombrePasoTRUE As String = String.Empty

            ' Comprobamos si tenemos informado el Paso al que tenemos que saltar
            If Not String.IsNullOrEmpty(PasoTRUE) Then
                ' Obtenemos el nombre del paso al que tenemos que saltar
                nombrePasoTRUE = catalogoPasos.Find(Function(x) Not String.IsNullOrEmpty(x.Nombre) AndAlso x.Nombre.ToUpper.Equals(PasoTRUE.ToUpper)).ObtenerNombrePaso(If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite), catalogoAcciones)

            Else
                ' No tenemos el Paso al que saltar informado
                Select Case tipoTratamientoPasos
                    Case TipoTratamientoPasos.InicioTramitacion
                        ' Saltamos al Primer Trámite
                        nombrePasoTRUE = pathSiguientePaso

                    Case TipoTratamientoPasos.TramiteARTEZ
                        ' Hay que ver el tipo de trámite que estamos tratando para que la redirección vaya a la acción correspondiente
                        nombrePasoTRUE = If(datosGeneralesTramite.EsBloqueTramitacionComun, datosGeneralesTramite.PathSalidaBloqueTramitacionComun, datosGeneralesTramite.ObtenerNombrePathAccionGestionFinalizarTarea)

                    Case TipoTratamientoPasos.TramiteFicticio
                        ' Saltamos al Tratamiento de Salida del Trámite Ficticio
                        nombrePasoTRUE = pathSiguientePaso
                End Select
            End If

            Return nombrePasoTRUE
        End Function

        Private Function ObtenerNombrePasoFALSE(tipoTratamientoPasos As TipoTratamientoPasos, pathSiguientePaso As String, ByRef catalogoAcciones As CatalogoAcciones, catalogoPasos As List(Of Paso), ByRef datosGeneralesTramite As DatosGeneralesTramite) As String
            Dim nombrePasoFALSE As String = String.Empty

            ' Comprobamos si tenemos informado el Paso al que tenemos que saltar
            If Not String.IsNullOrEmpty(PasoFALSO) Then
                ' Obtenemos el nombre del paso al que tenemos que saltar
                nombrePasoFALSE = catalogoPasos.Find(Function(x) Not String.IsNullOrEmpty(x.Nombre) AndAlso x.Nombre.ToUpper.Equals(PasoFALSO.ToUpper)).ObtenerNombrePaso(If(tipoTratamientoPasos.Equals(TipoTratamientoPasos.InicioTramitacion), Constantes.NUMERO_0, datosGeneralesTramite.OrdenTramite), catalogoAcciones)

            Else
                ' No tenemos el Paso al que saltar informado
                Select Case tipoTratamientoPasos
                    Case TipoTratamientoPasos.InicioTramitacion
                        ' Saltamos al Primer Trámite
                        nombrePasoFALSE = pathSiguientePaso

                    Case TipoTratamientoPasos.TramiteARTEZ
                        ' Hay que ver el tipo de trámite que estamos tratando para que la redirección vaya a la acción correspondiente
                        nombrePasoFALSE = If(datosGeneralesTramite.EsBloqueTramitacionComun, datosGeneralesTramite.PathSalidaBloqueTramitacionComun, datosGeneralesTramite.ObtenerNombrePathAccionGestionFinalizarTarea)

                    Case TipoTratamientoPasos.TramiteFicticio
                        ' Saltamos al Tratamiento de Salida del Trámite Ficticio
                        nombrePasoFALSE = pathSiguientePaso
                End Select
            End If

            Return nombrePasoFALSE
        End Function

        Private Sub ObtenerParametrosEspecificos()
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
#End Region
    End Class
End Namespace