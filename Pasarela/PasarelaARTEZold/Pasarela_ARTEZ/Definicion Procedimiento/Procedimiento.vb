Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlRoot("Procedimiento")>
    Public NotInheritable Class Procedimiento
        Private _entornos As Entornos
        Private ReadOnly _listadoBaseDatosConAccionesDiferidas As List(Of String)
        Private _detalleFlujo As DetalleFlujo
        Private _resumenTramitesNombreEnFlujo As Dictionary(Of String, String)
        Private _catalogoAcciones As CatalogoAcciones

#Region "Propiedades del XML"
        <XmlElement("DatosGenerales")>
        Public Property DatosGenerales() As DatosgeneralesProcedimiento

        <XmlArrayItem("Paso", IsNullable:=True)>
        Public Property PasosIniciarTramitacion() As List(Of Paso)

        <XmlArrayItem("Tramite")>
        Public Property Tramites() As List(Of Tramite)

        <XmlIgnore()>
        Friend Property BaseDatosDestinoFlujo As String

        <XmlIgnore()>
        Friend Property BaseDatosInfraestructura As String

        Private Property FlowOrder As Integer

        <XmlIgnore()>
        Private Property ContadorReferencias As Integer

        <XmlIgnore()>
        Private Property ContadorJUMPs As Integer

        <XmlIgnore()>
        Private Property ContadorUNIONs As Integer

        <XmlIgnore()>
        Private Property ContadorProcesosFirmaPortaFirmas As Integer

        <XmlIgnore()>
        Private Property ContadorProcesosNotificacionesNT As Integer

        <XmlIgnore()>
        Private Property ContadorPasosInicializacion As Integer

        <XmlIgnore()>
        Private Property ContadorTamitesUnionRamasParalelas As Integer

        <XmlIgnore()>
        Private ReadOnly Property HayPasosIniciarTramitacion As Boolean
            Get
                Return If(Not IsNothing(PasosIniciarTramitacion) AndAlso PasosIniciarTramitacion.Count > Constantes.NUMERO_0, True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property HayUnionRamasParalelas As Boolean
            Get
                Return If(Not IsNothing(ContadorUNIONs) AndAlso ContadorUNIONs > Constantes.NUMERO_0, True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property NumeroUnionRamasParalelas As Integer
            Get
                Return If(Not IsNothing(ContadorUNIONs), ContadorUNIONs, Constantes.NUMERO_0)
            End Get
        End Property
#End Region

#Region "Constructores"
        Public Sub New()
            DatosGenerales = Nothing
            PasosIniciarTramitacion = Nothing
            Tramites = Nothing
            'Tramites = New List(Of Tramite)
        End Sub
#End Region

#Region "Métodos Friend"
        Friend Sub VerificarInicializarDefiniconCargada()
            ' Verficamos los Bloques Básicos de la Definición del Procedimiento
            VerificarBloquesBasicos()

            ' Verificamos el Bloque Pasos Iniciar Tramitación, ya que si hay pasos de tipo EVALUACION y/o SALTO y pasos sin el campo NombrePaso informado
            VerificarPasosBloquePasosIniciarTramitacion()

            ' Verificamos e Inicializamos los Trámites

        End Sub

        Friend Sub Crear(entornosAdicionalesSeleccionados As List(Of String))
            ' Obtenemos los Entornos sobre los que vamos a trabajar
            _entornos = Utilidades.Utilidades.ObtenerEntornos(entornosAdicionalesSeleccionados, BaseDatosDestinoFlujo)

            ' Verificamos si la Base de Datos Destino indicada para el Flujo es correcta para el Procedimiento
            VerificarRegistrarBaseDatosDestinoFlujo(_entornos.ObtenerCadenaConexionMotorEntornoPredeterminado, BaseDatosDestinoFlujo)

            ' Determinamos la Versión del Flujo a Crear
            DeterminarVersionFlujo(_entornos.ObtenerCadenaConexionInfraestructuraEntornoPredeterminado, BaseDatosDestinoFlujo)

            ' Determinamos el Nombre del Flujo
            DeterminarNombreFlujo()

            ' Obtenemos las Sentencias SQL para la Creación del Flujo HIDRA correspondiente al Procedimiento
            ObtenerDetalleCreacionFlujo()

            ' Damos de Alta el Flujo en el Motor para la Aplicación
            AddFlujoAplicacionMotor(BaseDatosDestinoFlujo)
        End Sub
#End Region

#Region "Métodos Privados"
        Private Sub VerificarBloquesBasicos()
            ' Verificamos el bloque Datos Generales
            VerificarBloqueDatosGenerales()

            ' Verificamos el bloque Trámites
            VerificarBloqueTramites()
        End Sub

        Private Sub VerificarBloqueDatosGenerales()
            If IsNothing(DatosGenerales) Then
                Throw New Exception("El XML de Definición del Procedimiento no tiene el bloque 'DatosGenerales'.")

            Else
                With DatosGenerales
                    If IsNothing(.IdentificadorFlujo) OrElse String.IsNullOrEmpty(.IdentificadorFlujo) Then
                        Throw New Exception("El bloque 'DatosGenerales' del XML de Definición del Procedimiento no tiene informado el campo 'IdentificadorFlujo'.")

                    ElseIf IsNothing(.NombreFlujo) OrElse String.IsNullOrEmpty(.NombreFlujo) Then
                        Throw New Exception("El bloque 'DatosGenerales' del XML de Definición del Procedimiento no tiene informado el campo 'NombreFlujo'.")

                    ElseIf IsNothing(.Descripcion) OrElse String.IsNullOrEmpty(.Descripcion) Then
                        Throw New Exception("El bloque 'DatosGenerales' del XML de Definición del Procedimiento no tiene informado el campo 'Descripcion'.")
                    End If
                End With
            End If
        End Sub

        Private Sub VerificarBloqueTramites()
            If IsNothing(Tramites) Then
                Throw New Exception("El XML de Definición del Procedimiento no tiene el bloque 'Tramites'.")

            ElseIf Tramites.Count = Constantes.NUMERO_0 Then
                Throw New Exception("El XML de Definición del Procedimiento tiene el bloque 'Tramites' vacío.")
            End If
        End Sub

        Private Sub VerificarPasosBloquePasosIniciarTramitacion()
            If Not IsNothing(PasosIniciarTramitacion) AndAlso PasosIniciarTramitacion.Count > Constantes.NUMERO_0 Then
                ' Comprobamos si tenemos algún paso de tipo Evaluación o Salto, ya que en caso afirmativo todos los pasos tiene que tener Nombre
                If PasosIniciarTramitacion.Exists(Function(x) x.Tipo.Equals(TipoPaso.Evaluacion) OrElse x.Tipo.Equals(TipoPaso.Salto)) AndAlso PasosIniciarTramitacion.Exists(Function(x) String.IsNullOrEmpty(x.Nombre)) Then
                    Throw New Exception("El bloque 'PasosIniciarTramitacion' del XML de Definición del Procedimiento tiene un paso 'EVALUACION' y/o 'SALTO', pero hay pasos que no tienen el campo 'NombrePaso' informado.")
                End If
            End If
        End Sub

        Private Sub VerificarInicializarTramites()

        End Sub

        Private Sub VerificarRegistrarBaseDatosDestinoFlujo(cadenaConexionInfraestructura As String, baseDatosDestinoFlujo As String)
            Dim baseDatosDestinoFlujoCorrecta As Boolean = False
            Dim baseDatosDestinoFlujoRegistrada As String = String.Empty

            AccesoDatos.VerificarRegistrarBaseDatosDestinoFlujo(cadenaConexionInfraestructura, Constantes.APLICACION, baseDatosDestinoFlujo, DatosGenerales.IdentificadorFlujo, baseDatosDestinoFlujoCorrecta, baseDatosDestinoFlujoRegistrada)

            If Not baseDatosDestinoFlujoCorrecta Then
                Throw New Exception(String.Format("La Base de Datos Destino Flujo seleccionada '{0}' no es válida porque el Procedimiento '{1}' ya existe para la Aplicación '{2}' en la Base de Datos {3}",
                                                  baseDatosDestinoFlujo,
                                                  DatosGenerales.IdentificadorFlujo,
                                                  Constantes.APLICACION,
                                                  baseDatosDestinoFlujoRegistrada))
            End If
        End Sub

        Private Sub DeterminarVersionFlujo(cadenaConexionInfraestructura As String, baseDatosDestinoFlujo As String)
            DatosGenerales.Version = AccesoDatos.ObtenerSiguienteVersionFlujo(cadenaConexionInfraestructura, Constantes.APLICACION, baseDatosDestinoFlujo, DatosGenerales.IdentificadorFlujo)
        End Sub

        Private Sub DeterminarNombreFlujo()
            If String.IsNullOrEmpty(DatosGenerales.NombreFlujo) Then
                ' Utilizamos como Nombre el Identificador, pero solo las priemras 25 posiciones, ya que el identificador es de 50 posiciones y el nombre de 25
                DatosGenerales.NombreFlujo = If(DatosGenerales.IdentificadorFlujo.Length > 25, DatosGenerales.IdentificadorFlujo.Substring(1, 25), DatosGenerales.IdentificadorFlujo)
            End If
        End Sub

        Private Sub ObtenerDetalleCreacionFlujo()
            ' Obtenemos el Resumen de trámites con su nombre en Flujo
            _resumenTramitesNombreEnFlujo = ObtenerResumenTramitesNombreEnFlujo()

            ' Obtenemos el Catálogo de Acciones
            _catalogoAcciones = Utilidades.Utilidades.CargarCatalogoAcciones()

            ' Inicializamos los Contadores
            InicializarContadores()

            ' Inicializamos el objeto del Detalle del Flujo
            _detalleFlujo = New DetalleFlujo

            ' Insertamos la línea de Detalle del Inicio del Flujo
            _detalleFlujo.AddLineasDetalle(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalleInicioFlujo())

            ' Insertamos las Líneas de Detalle de la Inicialización de la Tramitación
            TratamientoInicioTramitacion()

            ' Insertamos en las Sentencias la Creación de los Trámites
            For Each tramite As Tramite In Tramites
                With tramite
                    ' Asignamos los datos báscios para poder Obtener las líneas de Detalle del FLujo
                    .AsignarDatosBasicosGenerarLineasDetalleCreacionTramite(DatosGenerales.IdentificadorFlujo, FlowOrder)

                    ' Asignamos los últimos valores de Contadores, Bases de Datos y estado de la Infraestructura ARTEZ para poder obtener las líneas de Detalle del Flujo
                    .AsignarDatosGenearrLineasDetalleCreacionTramiteARTEZ(ContadorReferencias, ContadorJUMPs, ContadorUNIONs, ContadorProcesosFirmaPortaFirmas, ContadorProcesosNotificacionesNT, ContadorPasosInicializacion, BaseDatosInfraestructura, BaseDatosDestinoFlujo)

                    ' Inicializamos la ordenación de los pasos dentro del trámite y activamos el indicador de último paso para el último
                    .InicializarNumeroPaso_IndicadorUltimoPaso()

                    ' Comprobamos si estamos tratando un Trámite ARTEZ o un Trámite Técnico
                    If .EsTramiteARTEZ Then
                        ' Es un trámite ARTEZ
                        TratamientoTramite(tramite)

                    Else
                        ' Es un trámite técnico
                        TratamientoTramiteTecnico(tramite)
                    End If

                    ' Recuperamos el último valor del Contador FlowOrder oara los siguientes Trámites a tratar
                    FlowOrder = .ObtenerUltimoValorFlowOrder()

                    ' Añadimos las Líneas de Detalle del Trámite al Detalle del Flujo para poder crear el Procedimiento
                    _detalleFlujo.AddLineasDetalle(.LineasDetalle)
                End With
            Next

            ' Insertamos en el Detalle del Flujo la Finalización de la Tramitación
            _detalleFlujo.AddLineasDetalle(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalleFinalizacionTramitacion(DatosGenerales.IdentificadorFlujo, BaseDatosInfraestructura, BaseDatosDestinoFlujo))

            ' Insertamos en el Detalle del Flujo el Fin del Flujo
            _detalleFlujo.AddLineasDetalle(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalleFinFlujo(DatosGenerales.IdentificadorFlujo))
        End Sub

        Private Sub InicializarContadores()
            FlowOrder = 0
            ContadorReferencias = 0
            ContadorJUMPs = 0
            ContadorUNIONs = 0
            ContadorProcesosFirmaPortaFirmas = 0
            ContadorProcesosNotificacionesNT = 0
            ContadorPasosInicializacion = 0
            ContadorTamitesUnionRamasParalelas = 0
        End Sub

        Private Sub TratamientoInicioTramitacion()
            ' Insertamos la Carpeta para Marcar el Inicio de la Tramitación
            FlowOrder += 100
            _detalleFlujo.AddLineasDetalle(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(flowOrder:=FlowOrder, path:="INICIO_TRAMITACION", tieneParametro_STATE:=True, valorParametro_STATE:="INICIO_TRAMITACION", valorParametro_SUBMIT:="0",
                                                                                                                  tipoAgrupacion:=String.Empty, level:=1, comentario:=String.Empty))

            ' Obtenemos el Path del Primer Trámite
            Dim pathPrimerTramite As String = Utilidades.Utilidades.ObtenerPathTramite(_resumenTramitesNombreEnFlujo, Tramites.First.DatosGenerales.Nombre)

            ' Inicializamos el Número de los Pasos del Inicio de Tramitación si procede
            InicializarNumeroPasosInicioTramitacion()

            ' Obtenemos el Path Retorno de la Acción Inicializar Tramitación
            Dim pathRetornoAccionInicializarTramitacion As String = ObtenerPathRetornoAccionIniciarTramitacion(pathPrimerTramite)

            ' Insertamos la Llamada a la Acción que Inicializa la Tramitación
            FlowOrder += 100
            ContadorReferencias += 1
            _detalleFlujo.AddLineasDetalle(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(identificadorFlujo:=DatosGenerales.IdentificadorFlujo, flowOrder:=FlowOrder, path:="INICIAR_TRAMITACION", baseDatosAccion:=BaseDatosInfraestructura,
                                                                                                                         accion:="INICIAR_TRAMITACION", aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2, diferido:=False,
                                                                                                                         idnt_Elemento:=String.Empty, sistemaFuncional:=String.Empty, grupoUsuarios:=String.Empty, usuario:=String.Empty, baseDatosRetorno:=BaseDatosDestinoFlujo,
                                                                                                                         pathRetorno:=pathRetornoAccionInicializarTramitacion, comentario:=String.Empty, parametrosEspecificos:=Nothing, habilitado:=False))

            ' Comprobamos si tenemos Pasos en la Inicialización de la Trmaitación
            If HayPasosIniciarTramitacion Then
                ' Añadimos 
                AddLineasDetallePasosIniciarTramitacion(pathPrimerTramite)
            End If
        End Sub

        Private Sub InicializarNumeroPasosInicioTramitacion()
            ' Determinamos el Número de Paso que le corresponde a cada Paso del listado, y marcamos el último paso
            Dim numeroPaso As Integer = 0
            For Each paso As Paso In PasosIniciarTramitacion
                numeroPaso += 1
                paso.Numero = numeroPaso
                ' Comprobamos si es el último paso
                If paso.Equals(PasosIniciarTramitacion.Last) Then
                    paso.EsUltimoPaso = True
                End If
            Next
        End Sub

        Private Function ObtenerPathRetornoAccionIniciarTramitacion(pathPrimerTramite As String) As String
            Return If(HayPasosIniciarTramitacion, PasosIniciarTramitacion.First.ObtenerNombrePaso(Constantes.NUMERO_0, _catalogoAcciones), pathPrimerTramite)
        End Function

        Private Sub AddLineasDetallePasosIniciarTramitacion(pathPrimerTramite As String)
            ' Nos recorremos los pasos
            For Each paso As Paso In PasosIniciarTramitacion
                ' Aumentamos el FlowOrder
                FlowOrder += 100

                ' Determinamos si se debe aumentar el contador de Referencias
                If paso.EsPasoConReferencia() Then
                    ContadorReferencias += 1
                End If

                ' Estamos tratando un Trámite
                paso.ObtenerLineasDetalleIniciarTramitacion(_catalogoAcciones, PasosIniciarTramitacion, pathPrimerTramite, BaseDatosDestinoFlujo, DatosGenerales.IdentificadorFlujo, FlowOrder, Constantes.APLICACION, ContadorReferencias)

                ' Añadimos las Líneas de Detalle del Paso al Detalle del Trámite
                _detalleFlujo.AddLineasDetalle(paso.LineasDetalle)
            Next
        End Sub

        Private Sub TratamientoTramite(tramite As Tramite)
            With tramite
                ' Verificamos que el Trámite tenga los objetos necesarios informados
                .VerificarObjetosNecesariosInformados()

                ' Obtenemos las Líneas de Detalle de creación del Trámite ARTEZ
                .GenerarLineasDetalleCreacionTramite(_resumenTramitesNombreEnFlujo, _catalogoAcciones)

                ' Recuperamos los últimos valores de Contadores para los siguientes Trámites ARTEZ a tratar
                .ObtenerUltimosValoresContadores(ContadorReferencias, ContadorJUMPs, ContadorProcesosFirmaPortaFirmas, ContadorProcesosNotificacionesNT, ContadorPasosInicializacion)
            End With
        End Sub

        Private Sub TratamientoTramiteTecnico(tramite As Tramite)
            With tramite
                ' Verificamos que el Trámite Ficitio esté correctamente definido
                .VerificarObjetosTramiteFicticio()

                ' Obtenemos las Líneas de Detalle de creación del Trámite Ficticio
                .GenerarLineasDetalleCreacionTramiteTecnico(_resumenTramitesNombreEnFlujo, _catalogoAcciones)

                If .DatosGenerales.TipoTramite.Equals(TipoTramite.UnionRamas) Then
                    ' Recuperamos el valor del Contador de UNIONs para los siguientes
                    ContadorUNIONs = .ObtenerUltimoValorContadorUNIONs()
                End If

                ContadorJUMPs = .ObtenerUltimoValorJUMPs
            End With
        End Sub

        Private Sub AddFlujoAplicacionMotor(baseDatosDestinoFlujo As String)
            ' Añadimos el Flujo correspondiente a la Aplicación al Motor HIDRA para cada uno de los entornos
            _entornos.AddFlujoAplicacionMotor(Constantes.APLICACION, baseDatosDestinoFlujo, DatosGenerales.IdentificadorFlujo, DatosGenerales.Descripcion, DatosGenerales.Version, _detalleFlujo)
        End Sub

        Private Function ObtenerResumenTramitesNombreEnFlujo() As Dictionary(Of String, String)
            Dim resumenTramitesNombreEnFlujo As New Dictionary(Of String, String)

            Dim numeroTramite As Integer = 1
            For Each tramite As Tramite In Tramites
                tramite.AsignarOrden(numeroTramite)

                ' Comprobamos si ya existe un Trámite con el mismo Nombre en el Flujo (no podemos tener dos trámite con el mismo nombre)
                If Not resumenTramitesNombreEnFlujo.ContainsKey(tramite.DatosGenerales.Nombre.ToUpper) Then
                    ' No hemos tratado el trámite, así que lo adjuntamos al Diccionario
                    resumenTramitesNombreEnFlujo.Add(tramite.DatosGenerales.Nombre.ToUpper, tramite.DatosGenerales.ObtenerNombrePathTramite)

                Else
                    ' Hemos tratado el trámite, así que hay que elevar una Excepción
                    Throw New Exception(String.Format("El Trámite con nombre '{0}' está Duplicado en la Definición XML del Flujo", tramite.DatosGenerales.Nombre.ToUpper))
                End If

                numeroTramite += 1
            Next

            Return resumenTramitesNombreEnFlujo
        End Function
#End Region
    End Class
End Namespace

