Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class Tramite
        Private _lineasDetalle As List(Of LineaDetalle)

        <XmlElement("DatosGenerales")>
        Public Property DatosGenerales As DatosGeneralesTramite

        <XmlElement("DatosARTEZ", IsNullable:=True)>
        Public Property DatosARTEZ As DefinicionTramiteARTEZ

        <XmlArrayItem("AccionAislada", IsNullable:=True)>
        Public Property AccionesAisladas() As List(Of AccionAislada)

        <XmlElement("DatosARTEZBloqueTramitacionComunNotificacion", IsNullable:=True)>
        Public Property DatosARTEZBloqueTramitacionComunNotificacion As DefinicionBloqueTramitacionComunNotificacion

        <XmlElement("DatosARTEZBloqueTramitacionComunFirma", IsNullable:=True)>
        Public Property DatosARTEZBloqueTramitacionComunFirma As DefinicionBloqueTramitacionComunFirma

        <XmlElement("DatosARTEZBloqueTramitacionComunBKON", IsNullable:=True)>
        Public Property DatosARTEZBloqueTramitacionComunBKON As DefinicionBloqueTramitacionComunIntegracionBKON

        <XmlElement("DatosARTEZBloqueTramitacionComunPuestaManifiesto", IsNullable:=True)>
        Public Property DatosARTEZBloqueTramitacionComunPuestaManifiesto As DefinicionBloqueTramitacionComunPuestaManifiesto

        <XmlElement("DatosTramiteRecepcionAcuseRecibo", IsNullable:=True)>
        Public Property DatosTramiteRecepcionAcuseRecibo As DatosTramiteRecepcionAcuseRecibo

        <XmlElement("Plazo", IsNullable:=True)>
        Public Property Plazo As Plazo

        <XmlArrayItem("Paso")>
        Public Property Pasos() As List(Of Paso)

        <XmlArrayItem("Salida")>
        Public Property Salidas() As List(Of Salida)

        <XmlIgnore()>
        Friend Property TipoTratamientoTramite As TipoTratamientoTramite

        <XmlIgnore()>
        Friend Property ParametrosEspecificosCreacionTarea As Dictionary(Of String, String)

        <XmlIgnore()>
        Friend ReadOnly Property EsTramiteARTEZ As Boolean
            Get
                Return If(Not DatosGenerales.TipoTramite.Equals(TipoTramite.UnionRamas) AndAlso Not DatosGenerales.TipoTramite.Equals(TipoTramite.Ficticio), True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property TieneParametrosEspecificosCreacionTarea As Boolean
            Get
                Return If(Not IsNothing(DatosARTEZ.ParametrosEspecificosCreacionTarea) AndAlso DatosARTEZ.ParametrosEspecificosCreacionTarea.Count > Constantes.NUMERO_0, True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property TienePlazo() As Boolean
            Get
                Return If(Not IsNothing(Plazo), True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property TienePasos() As Boolean
            Get
                Return If(Not IsNothing(Pasos) AndAlso Pasos.Count > Constantes.NUMERO_0, True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property TienePasosInicializacionVariables() As Boolean
            Get
                Return If(Not IsNothing(Pasos) AndAlso Pasos.Count > Constantes.NUMERO_0 AndAlso Pasos.Exists(Function(x) x.EsPasoInicializacionVariables), True, False)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property NumeroSalidas() As Integer
            Get
                Return If(IsNothing(Salidas) OrElse Salidas.Count = Constantes.NUMERO_0, Constantes.NUMERO_0, Salidas.Count)
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property PrimeraSalida() As Salida
            Get
                Dim salida As Salida = Nothing

                If Not NumeroSalidas.Equals(Constantes.NUMERO_0) Then
                    salida = Salidas.First
                End If

                Return salida
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property UltimaSalida() As Salida
            Get
                Dim salida As Salida = Nothing

                If Not NumeroSalidas.Equals(Constantes.NUMERO_0) Then
                    salida = Salidas.Last
                End If

                Return salida
            End Get
        End Property

        <XmlIgnore()>
        Friend ReadOnly Property LineasDetalle() As List(Of LineaDetalle)
            Get
                Return _lineasDetalle
            End Get
        End Property

        <XmlIgnore()>
        Private Property IdentificadorFlujo As String

        <XmlIgnore()>
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
        Private Property BaseDatosDestinoFlujo As String

        <XmlIgnore()>
        Private Property BaseDatosInfraestructura As String

#Region "Constructores"
        Public Sub New()

        End Sub
#End Region

#Region "Métodos Friend"
        Friend Sub VerificarObjetosNecesariosInformados()
            If DatosGenerales.EsBloqueTramitacionComun Then
                ' Estamos tratando un Bloque de Tramitación Común, así que tenemos que verificar si está informado el objeto correspondiente
                Select Case DatosGenerales.TipoTramite
                    Case TipoTramite.Notificacion
                        If IsNothing(DatosARTEZBloqueTramitacionComunNotificacion) Then
                            Throw New Exception(String.Format("El Bloque de Tramitación Común '{0}' de tipo '{1}' no tiene informados los datos de ARTEZ necesarios para poder realizar la invocación", DatosGenerales.Nombre, ObtenerDescripcionTipoTramite()))
                        End If

                        ' Verificamos si los Objetos para el Bloque de Notificación están informados
                        DatosARTEZBloqueTramitacionComunNotificacion.Verificar(DatosGenerales.Nombre)

                    Case TipoTramite.Firma
                        If IsNothing(DatosARTEZBloqueTramitacionComunFirma) Then
                            Throw New Exception(String.Format("El Bloque de Tramitación Común '{0}' de tipo '{1}' no tiene informados los datos de ARTEZ necesarios para poder realizar la invocación", DatosGenerales.Nombre, ObtenerDescripcionTipoTramite()))
                        End If

                        ' Verificamos si los Objetos para el Bloque de Firma están informados
                        DatosARTEZBloqueTramitacionComunFirma.Verificar(DatosGenerales.Nombre)

                    Case TipoTramite.BKON
                        If IsNothing(DatosARTEZBloqueTramitacionComunBKON) Then
                            Throw New Exception(String.Format("El Bloque de Tramitación Común '{0}' de tipo '{1}' no tiene informados los datos de ARTEZ necesarios para poder realizar la invocación", DatosGenerales.Nombre, ObtenerDescripcionTipoTramite()))
                        End If

                        ' Verificamos si los Objetos para el Bloque de BKON están informados
                        DatosARTEZBloqueTramitacionComunBKON.Verificar(DatosGenerales.Nombre)

                    Case TipoTramite.PuestaManifiesto
                        If IsNothing(DatosARTEZBloqueTramitacionComunPuestaManifiesto) Then
                            Throw New Exception(String.Format("El Bloque de Tramitación Común '{0}' de tipo '{1}' no tiene informados los datos de ARTEZ necesarios para poder realizar la invocación", DatosGenerales.Nombre, ObtenerDescripcionTipoTramite()))
                        End If

                        ' Verificamos si los Objetos para el Bloque de Puesta de Manifiesto están informados
                        DatosARTEZBloqueTramitacionComunPuestaManifiesto.Verificar(DatosGenerales.Nombre)

                    Case Else
                        Throw New Exception(String.Format("El Bloque de Tramitación Común '{0}' de tipo '{1}' no está contemplado", DatosGenerales.Nombre, ObtenerDescripcionTipoTramite()))
                End Select

            Else
                ' Estamos tratando un Trámite ARTEZ, así que tenemos que verificar si está informado el objeto DatosArtez
                If IsNothing(DatosARTEZ) Then
                    Throw New Exception(String.Format("El Trámite ARTEZ '{0}' no tiene informados los datos de ARTEZ", DatosGenerales.Nombre))
                End If

                ' Realizamos las verificaciones específicas por el Tipo de Trámite ARTES
                Select Case DatosGenerales.TipoTramite
                    Case TipoTramite.Manual
                        If IsNothing(DatosARTEZ.DatosReponsableTramite) Then
                            Throw New Exception(String.Format("El Trámite ARTEZ '{0}' Manual, no tiene informados los datos necesarios para Crear la Tarea Manual", DatosGenerales.Nombre))
                        End If

                        ' Comprobamos si estamos tratando un Trámite ARTEZ de Elaboración de Documentos
                        If Not IsNothing(DatosARTEZ.DatosElaboracionDocumento) Then
                            ' Comproabmos si tenemos informados los Datos para la Elaboración de Documentos
                            If Not DatosARTEZ.DatosElaboracionDocumento.DatosInformados Then
                                Throw New Exception(String.Format("El Trámite ARTEZ '{0}' de Elaboración de Documentos, no tiene informados los datos necesarios para la Elaboración", DatosGenerales.Nombre))
                            End If

                            ' Comprobamos si tenemos Activada la Pestaña de Firma
                            If DatosARTEZ.DatosElaboracionDocumento.HabilitarPestanaFirma Then
                                ' Comprobamos que el valor del Parámetro Habilitar Pestaña Firma tiene un valor válido
                                If Not DatosARTEZ.DatosElaboracionDocumento.ValorValidoHabilitarPestanaFirma Then
                                    Throw New Exception(String.Format("El Trámite ARTEZ '{0}' de Elaboración de Documentos con Pestaña de Firma Habilitada, no tiene informado un Valor Válido en el Parámetro 'HabilitarPestanaFirma' (true o S)", DatosGenerales.Nombre))
                                End If

                                ' Comprobamos que todos los Parámetros para el Tratamiento de Firmas están informados
                                If Not DatosARTEZ.DatosElaboracionDocumento.DatosFirma.DatosInformados(DatosARTEZ.DatosElaboracionDocumento.ValorHabilitarPestanaFirma) Then
                                    Throw New Exception(String.Format("El Trámite ARTEZ '{0}' de Elaboración de Documentos con Pestaña de Firma, no tiene informados los datos necesarios para el Tratamiento de la Devolución de los Datos de Firma", DatosGenerales.Nombre))
                                End If
                            End If
                        End If

                    Case TipoTramite.RecepcionAcuseRecibo
                        If IsNothing(DatosTramiteRecepcionAcuseRecibo) Then
                            Throw New Exception(String.Format("El Trámite ARTEZ '{0}' de Recepción de Acuse de Recibo, no tiene informados los datos necesarios para la Creación de este tipo de Tareas", DatosGenerales.Nombre))
                        End If

                        ' Verificamos si están informados los datos básicos para este tipo de trámites
                        DatosTramiteRecepcionAcuseRecibo.Verificar(DatosGenerales.Nombre)
                End Select
            End If
        End Sub

        Friend Sub VerificarObjetosTramiteFicticio()
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.Ficticio
                    ' Si estamos en un trámite ficiticio, verificamos si tenemos saltos, y en caso afirmativo comprobamos que solo pueda haber un paso de Parada
                    If TienePasos Then
                        'If Pasos.Count > Constantes.NUMERO_1 Then
                        '    Throw New Exception(String.Format("El Trámite Ficticio '{0}' tiene más de un paso asociado, lo que no está permitido para este tipo de 'Trámites'", DatosGenerales.Nombre))
                        'End If

                        'If Not Pasos.First.Tipo.Equals(TipoPaso.Parada) Then
                        '    Throw New Exception(String.Format("El Trámite Ficticio '{0}' tiene un paso que no es de tipo 'PARADA', y no está permitido para este tipo de 'Trámites'", DatosGenerales.Nombre))
                        'End If
                    End If
            End Select
        End Sub

        Friend Sub AsignarOrden(ordenTramite As Integer)
            DatosGenerales.OrdenTramite = ordenTramite
        End Sub

        Friend Sub AsignarDatosBasicosGenerarLineasDetalleCreacionTramite(identificadorFlujo As String, flowOrder As Integer)
            Me.IdentificadorFlujo = identificadorFlujo
            Me.FlowOrder = flowOrder
        End Sub

        Friend Sub AsignarDatosGenearrLineasDetalleCreacionTramiteARTEZ(contadorReferencias As Integer, contadorJUMPs As Integer, contadorUNIONs As Integer, contadorProcesosFirmaPortaFirmas As Integer, contadorProcesosNotificacionesNT As Integer,
                                                                        contadorPasosInicializacion As Integer, baseDatosInfraestructura As String, baseDatosDestinoFlujo As String)
            ' Asignamos los Contadores
            Me.ContadorReferencias = contadorReferencias
            Me.ContadorJUMPs = contadorJUMPs
            Me.ContadorUNIONs = contadorUNIONs
            Me.ContadorProcesosFirmaPortaFirmas = contadorProcesosFirmaPortaFirmas
            Me.ContadorProcesosNotificacionesNT = contadorProcesosNotificacionesNT
            Me.ContadorPasosInicializacion = contadorPasosInicializacion

            ' Asignamos las Base de Datos
            Me.BaseDatosInfraestructura = baseDatosInfraestructura
            Me.BaseDatosDestinoFlujo = baseDatosDestinoFlujo
        End Sub

        Friend Sub ObtenerUltimosValoresContadores(ByRef contadorReferencias As Integer, ByRef contadorJUMPs As Integer, ByRef contadorProcesosFirmaPortaFirmas As Integer, ByRef contadorProcesosNotificacionesNT As Integer, ByRef contadorPasosInicializacion As Integer)
            contadorReferencias = Me.ContadorReferencias
            contadorJUMPs = Me.ContadorJUMPs
            contadorProcesosFirmaPortaFirmas = Me.ContadorProcesosFirmaPortaFirmas
            contadorProcesosNotificacionesNT = Me.ContadorProcesosNotificacionesNT
            contadorPasosInicializacion = Me.ContadorPasosInicializacion
        End Sub

        Friend Function ObtenerUltimoValorContadorUNIONs() As Integer
            Return ContadorUNIONs
        End Function

        Friend Function ObtenerUltimoValorFlowOrder() As Integer
            Return FlowOrder
        End Function

        Friend Function ObtenerUltimoValorJUMPs() As Integer
            Return ContadorJUMPs
        End Function

        Friend Sub GenerarLineasDetalleCreacionTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones)
            ' Insertamos en las Sentencias la Creación de la Etiqueta para Agrupar los Pasos del Trámite
            AddLineasDetalleEtiquetaTramite()

            ' Insertamos en las Sentencias la Creación de las llamadas a las Acciones Aisladas
            GenerarLineasDetalleEjecucionAccionesAisladas(TipoTratamientoPasos.TramiteARTEZ, catalogoAcciones)

            ' Comprobamos si estamos tratando un Bloque de Tramitación Común
            If DatosGenerales.EsBloqueTramitacionComun Then
                ' Estamos tratando un Bloque de Tramitación Común
                GenerarLineasDetalleCreacionBloqueTramitacionComunARTEZ(resumenTramitesNombreEnFlujo, catalogoAcciones)

            Else
                ' Estamos tratando un Trámite ARTEZ
                GenerarLineasDetalleCreacionTramiteARTEZ(resumenTramitesNombreEnFlujo, catalogoAcciones)
            End If
        End Sub

        Friend Sub GenerarLineasDetalleEjecucionAccionesAisladas(tipoTratamientoPasos As TipoTratamientoPasos, ByRef catalogoAcciones As CatalogoAcciones)
            ' Comprobamos si tenemos Acciones Aisladas
            If Not IsNothing(AccionesAisladas) AndAlso AccionesAisladas.Count > Constantes.NUMERO_0 Then
                ' Tenemos Acciones Aisladas (se ejecutan como ramas paralelas)
                Dim contador As Integer = 0
                For Each accionAislada As AccionAislada In AccionesAisladas
                    contador += 1
                    accionAislada.Orden = contador
                    _lineasDetalle.AddRange(accionAislada.ObtenerLineasDetalle(tipoTratamientoPasos, IdentificadorFlujo, FlowOrder, ContadorReferencias, Constantes.APLICACION, DatosGenerales, DatosARTEZ, catalogoAcciones, Plazo))
                Next
            End If
        End Sub

        Friend Sub GenerarLineasDetalleCreacionTramiteTecnico(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones)
            ' Insertamos en las Sentencias la Creación de la Etiqueta para Agrupar los Pasos del Trámite
            AddLineasDetalleEtiquetaTramite()

            ' Insertamos en las Sentencias la Creación de las llamadas a las Acciones Aisladas
            GenerarLineasDetalleEjecucionAccionesAisladas(TipoTratamientoPasos.TramiteFicticio, catalogoAcciones)

            ' Comprobamos si tenemos Pasos para Obtener el Nombre del primer Path de Tratamiento de Salida
            Dim nombrePathTratamientoSalida As String = String.Empty
            Dim saltoDirecto As Boolean
            If TienePasos Then
                ObtenerDatosSaltoSiguienteTramite(resumenTramitesNombreEnFlujo, saltoDirecto, nombrePathTratamientoSalida)
            End If

            ' Generamos las Líneas de Detalle correspondientes al Tipo de Trámite Técnico
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.UnionRamas
                    GenerarLineasDetalleCreacionTramiteTecnicoUnionRamas(resumenTramitesNombreEnFlujo, catalogoAcciones, nombrePathTratamientoSalida)

                Case TipoTramite.Ficticio
                    GenerarLineasDetalleCreacionTramiteTecnicoFicticio(resumenTramitesNombreEnFlujo, catalogoAcciones, nombrePathTratamientoSalida)
            End Select
        End Sub

        Friend Sub InicializarNumeroPaso_IndicadorUltimoPaso()
            If TienePasos Then
                ' Determinamos el Número de Paso que le corresponde a cada Paso del listado, y marcamos el último paso
                Dim numeroPaso As Integer = 0
                For Each paso As Paso In Pasos
                    numeroPaso += 1
                    paso.Numero = numeroPaso
                    ' Comprobamos si es el último paso
                    If paso.Equals(Pasos.Last) Then
                        paso.EsUltimoPaso = True
                    End If
                Next
            End If
        End Sub
#End Region

#Region "Métodos Privados"
        Private Sub GenerarLineasDetalleCreacionBloqueTramitacionComunARTEZ(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones)
            ' Variable para obtener el nombre del Path al que debe ir tras la Finalización del Bloque de Tramitación Común
            Dim pathRetornoBloqueTramitacionComun As String = String.Empty

            ' Obtenemos el Path correspondiente a la Salida del Bloque de Tramitación Común
            Dim saltoDirecto As Boolean = False
            Dim pathSalidaBloqueTramitacionComun As String = String.Empty
            ObtenerDatosSaltoSiguienteTramite(resumenTramitesNombreEnFlujo, saltoDirecto, pathSalidaBloqueTramitacionComun)

            ' Comprobamos si tras la llamada al Bloque de Tramitación Común tenemos Pasos
            If TienePasos Then
                ' El Bloque de Tramitación Común tiene pasos, así que de la llamada al Bloque de Tramitación Común pasamos al Primer Paso
                pathRetornoBloqueTramitacionComun = ObtenerNombrePrimerPaso(catalogoAcciones)

            Else
                ' El Bloque de Tramitación Común no tiene pasos, así que de la llamada al Bloque de Tramitación Común pasamos a la Salida del Trámite
                pathRetornoBloqueTramitacionComun = pathSalidaBloqueTramitacionComun
            End If

            ' Obtenemos la líneas de Detalle de la Llamada al Bloque de Tramitación Común
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.Notificacion
                    AddLineasDetalleBloqueTramitacionComunNotificacion(resumenTramitesNombreEnFlujo, pathRetornoBloqueTramitacionComun)

                Case TipoTramite.Firma
                    AddLineasDetalleBloqueTramitacionComunFirma(resumenTramitesNombreEnFlujo, pathRetornoBloqueTramitacionComun)

                Case TipoTramite.BKON
                    AddLineasDetalleBloqueTramitacionComunIntegracionBKON(resumenTramitesNombreEnFlujo, pathRetornoBloqueTramitacionComun)

                Case TipoTramite.PuestaManifiesto
                    AddLineasDetalleBloqueTramitacionComunPuestaManifiesto(resumenTramitesNombreEnFlujo, pathRetornoBloqueTramitacionComun)
            End Select

            ' Comprobamos si tenemos Pasos tras la llamada al Bloque de Tramitación Común
            If TienePasos Then
                DatosGenerales.PathSalidaBloqueTramitacionComun = pathSalidaBloqueTramitacionComun
                AddLineasDetallePasosTramite(catalogoAcciones)
            End If

            ' Insertamos en las Sentencias la Creación del Tratamiento de la/s Salida/s del Trámite (si es necesario)
            AddLineasDetalleTratamientoSalidasTramite(resumenTramitesNombreEnFlujo)
        End Sub

        Private Sub GenerarLineasDetalleCreacionTramiteARTEZ(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones)
            ' Inicializamos el contador de Firmas vía Porta Firmas o Notificaciones vía NT en función del tipo de trámite
            InicializarContadoresFirmas_Notificaciones()

            ' Insertamos en las Sentencias la Creación del Tratamiento de Plazos (si tiene)
            AddLineasDetalleTratamientoPlazoTramite()

            ' Insertamos en las Sentencias la Creación de la Creación y Ejecución de la Tarea (si es necesario)
            AddLineasDetalleEjecucionTareaTramite(catalogoAcciones)

            ' Insertamos en las Sentencias la Creación del Formulario de Parada (si es necesario)
            AddLineasDetalleParadaTramite(catalogoAcciones)

            ' Insertamos en las Sentencias la Creación de la Finalización de la Tarea
            AddLineasDetalleFinalizacionTareaTramite(resumenTramitesNombreEnFlujo)

            ' Insertamos en las Sentencias la Creación de la Cancelación de la Alerta (si es necesario)
            AddLineasDetalleCancelarAlertaTramite(resumenTramitesNombreEnFlujo)

            ' Insertamos en las Sentencias la Creación del Tratamiento de la/s Salida/s del Trámite (si es necesario)
            AddLineasDetalleTratamientoSalidasTramite(resumenTramitesNombreEnFlujo)

            ' Insertamos en las Sentencias la Creación de la Acción para Caducar la Tarea y el Salto al Trámite correspondiente (si es necesario)
            AddLineasDetalleCaducarTramite(resumenTramitesNombreEnFlujo)
        End Sub

        Private Sub GenerarLineasDetalleCreacionTramiteTecnicoUnionRamas(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones, nombrePathTratamientoSalida As String)
            ' Insertamos en las Sentencias la Creación de la Instrucción UNION
            AddLineasDetalleUnionRamasParalelas()

            ' Comprobamos si el Trámite Ficiticio tiene pasos, y en caso afirmativo lo insertamos
            If TienePasos Then
                AddLineasDetallePasosTramite(catalogoAcciones, nombrePathTratamientoSalida)
            End If

            ' Insertamos en las Sentencias la Creación del Tratamiento de la/s Salida/s del Trámite (si es necesario)
            AddLineasDetalleTratamientoSalidasTramite(resumenTramitesNombreEnFlujo, esTramiteFicticio:=True)
        End Sub

        Private Sub GenerarLineasDetalleCreacionTramiteTecnicoFicticio(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef catalogoAcciones As CatalogoAcciones, nombrePathTratamientoSalida As String)
            ' Comprobamos si el Trámite Ficiticio tiene pasos, y en caso afirmativo lo insertamos
            If TienePasos Then
                AddLineasDetallePasosTramite(catalogoAcciones, nombrePathTratamientoSalida)
            End If

            ' Insertamos en las Sentencias la Creación del Tratamiento de la/s Salida/s del Trámite (si es necesario)
            AddLineasDetalleTratamientoSalidasTramite(resumenTramitesNombreEnFlujo, esTramiteFicticio:=True)
        End Sub

        Private Sub InicializarContadoresFirmas_Notificaciones()
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.PortaFirmas
                    ContadorProcesosFirmaPortaFirmas += 1

                Case TipoTramite.OrdenNotificacion
                    ContadorProcesosNotificacionesNT += 1
            End Select
        End Sub

        Private Function ObtenerDescripcionTipoTramite() As String
            Dim descripcionTipoTramite As String = String.Empty

            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.Automatico
                    descripcionTipoTramite = Constantes.TRAMITE_AUTOMATICO

                Case TipoTramite.Manual
                    descripcionTipoTramite = Constantes.TRAMITE_MANUAL

                Case TipoTramite.RecepcionAcuseRecibo
                    descripcionTipoTramite = Constantes.TRAMITE_RECEPCION_ACUSE_RECIBO

                Case TipoTramite.ComprobacionRequisitos
                    descripcionTipoTramite = Constantes.TRAMITE_COMPROBACION_REQUISITOS

                Case TipoTramite.FirmaSello
                    descripcionTipoTramite = Constantes.TRAMITE_FIRMA_SELLO

                Case TipoTramite.Planificado
                    descripcionTipoTramite = Constantes.TRAMITE_PLANIFICADO

                Case TipoTramite.ProcesoPlanificado
                    descripcionTipoTramite = Constantes.TRAMITE_PROCESO_PLANIFICADO

                Case TipoTramite.Notificacion
                    descripcionTipoTramite = Constantes.TRAMITE_NOTIFICACION

                Case TipoTramite.Firma
                    descripcionTipoTramite = Constantes.TRAMITE_FIRMA

                Case TipoTramite.BKON
                    descripcionTipoTramite = Constantes.TRAMITE_BKON

                Case TipoTramite.PuestaManifiesto
                    descripcionTipoTramite = Constantes.TRAMITE_PUESTA_MANIFIESTO

                Case TipoTramite.PortaFirmas
                    descripcionTipoTramite = Constantes.TRAMITE_PORTA_FIRMAS

                Case TipoTramite.RechazoFirmas
                    descripcionTipoTramite = Constantes.TRAMITE_RECHAZO_FIRMAS

                Case TipoTramite.OrdenNotificacion
                    descripcionTipoTramite = Constantes.TRAMITE_ORDEN_NOTIFICACION

                Case TipoTramite.ProcesarNotificacion
                    descripcionTipoTramite = Constantes.TRAMITE_PROCESAR_NOTIFICACION

                Case TipoTramite.OrdenNotificacionManual
                    descripcionTipoTramite = Constantes.TRAMITE_ORDEN_NOTIFICACION_MANUAL

                Case TipoTramite.PortaFirmasControlM
                    descripcionTipoTramite = Constantes.TRAMITE_PORTA_FIRMAS_CONTROL_M

                Case TipoTramite.AutomaticoControlM
                    descripcionTipoTramite = Constantes.TRAMITE_AUTOMATICO_CONTROL_M
            End Select

            Return descripcionTipoTramite
        End Function

        Private Sub AddLineasDetalleEtiquetaTramite()
            FlowOrder += 100

            _lineasDetalle = New List(Of LineaDetalle)
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LABEL(FlowOrder, DatosGenerales.ObtenerNombrePathTramite, tieneParametro_STATE:=True, valorParametro_STATE:=DatosGenerales.NombreEnFlujo,
                                                                                                           valorParametro_SUBMIT:=Constantes.NUMERO_0, tipoAgrupacion:=If(DatosGenerales.EsBloqueTramitacionComun, Constantes.TIPO_AGRUPACION_BLOQUE_TRAMITACION_COMUN, String.Empty),
                                                                                                           level:=1, comentario:=DatosGenerales.Nombre))
        End Sub

        Private Sub AddLineasDetalleBloqueTramitacionComunNotificacion(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), pathRetorno As String)
            FlowOrder += 100
            ContadorReferencias += 1

            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Llamada_BloqueTramitacionComun(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathBloqueTramitacionComun(),
                                                                                                                             baseDatosBloqueTramitacionComun:=Constantes.BASE_DATOS_BLOQUES_TRAMITACION_COMUN,
                                                                                                                             bloqueTramitacionComun:=Constantes.BLOQUE_TRAMITACION_COMUN_NOTIFICACION, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias,
                                                                                                                             level:=2, diferido:=False, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=pathRetorno, comentario:=String.Empty,
                                                                                                                             parametrosEspecificos:=DatosARTEZBloqueTramitacionComunNotificacion.ObtenerParametrosEspecificos(resumenTramitesNombreEnFlujo), habilitado:=True))
        End Sub

        Private Sub AddLineasDetalleBloqueTramitacionComunFirma(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), pathRetorno As String)
            FlowOrder += 100
            ContadorReferencias += 1

            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Llamada_BloqueTramitacionComun(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathBloqueTramitacionComun(),
                                                                                                                             baseDatosBloqueTramitacionComun:=Constantes.BASE_DATOS_BLOQUES_TRAMITACION_COMUN,
                                                                                                                             bloqueTramitacionComun:=Constantes.BLOQUE_TRAMITACION_COMUN_FIRMA, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias,
                                                                                                                             level:=2, diferido:=False, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=pathRetorno, comentario:=String.Empty,
                                                                                                                             parametrosEspecificos:=DatosARTEZBloqueTramitacionComunFirma.ObtenerParametrosEspecificos(), habilitado:=True))
        End Sub

        Private Sub AddLineasDetalleBloqueTramitacionComunIntegracionBKON(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), pathRetorno As String)
            FlowOrder += 100
            ContadorReferencias += 1

            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Llamada_BloqueTramitacionComun(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathBloqueTramitacionComun(),
                                                                                                                             baseDatosBloqueTramitacionComun:=Constantes.BASE_DATOS_BLOQUES_TRAMITACION_COMUN,
                                                                                                                             bloqueTramitacionComun:=Constantes.BLOQUE_TRAMITACION_COMUN_INTEGRACION_BKON, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias,
                                                                                                                             level:=2, diferido:=False, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=pathRetorno, comentario:=String.Empty,
                                                                                                                             parametrosEspecificos:=DatosARTEZBloqueTramitacionComunBKON.ObtenerParametrosEspecificos(), habilitado:=True))
        End Sub

        Private Sub AddLineasDetalleBloqueTramitacionComunPuestaManifiesto(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), pathRetorno As String)
            FlowOrder += 100
            ContadorReferencias += 1

            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Llamada_BloqueTramitacionComun(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathBloqueTramitacionComun(),
                                                                                                                             baseDatosBloqueTramitacionComun:=Constantes.BASE_DATOS_BLOQUES_TRAMITACION_COMUN,
                                                                                                                             bloqueTramitacionComun:=Constantes.BLOQUE_TRAMITACION_COMUN_PUESTA_MANIFIESTO, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias,
                                                                                                                             level:=2, diferido:=False, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=pathRetorno, comentario:=String.Empty,
                                                                                                                             parametrosEspecificos:=DatosARTEZBloqueTramitacionComunPuestaManifiesto.ObtenerParametrosEspecificos(), habilitado:=True))
        End Sub

        Private Sub AddLineasDetalleTratamientoPlazoTramite()
            If TienePlazo Then
                ' Llamada a la Acción de Gestión para Obtener el Plazo
                FlowOrder += 100
                ContadorReferencias += 1
                Dim parametrosEspecificos As New Dictionary(Of String, String) From {
                    {"@FECHA_INICIO_IN", Plazo.FechaReferencia},
                    {"@TIPO_PLAZO_IN", Plazo.CodigoPlazo},
                    {"@TIPO_ALERTA_IN", Plazo.Tipo_Alerta},
                    {"@FECHA_EXACTA_IN", Plazo.FechaFinExacta},
                    {"@IND_PLAZO_CADUCADO_OUT", String.Format("IND_PLAZO_CADUCADO_T_{0}", DatosGenerales.OrdenTramite)},
                    {"@FECHA_FIN_PLAZO_OUT", String.Format("FECHA_FIN_PLAZO_T_{0}", DatosGenerales.OrdenTramite)},
                    {"@INTERVAL_OUT", String.Format("INTERVAL_T_{0}", DatosGenerales.OrdenTramite)},
                    {"@MATURATION_OUT", String.Format("MATURATION_T_{0}", DatosGenerales.OrdenTramite)}
                }
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathAccionGestionObtenerPlazo(), baseDatosAccion:=BaseDatosInfraestructura,
                                                                                                                      accion:=Constantes.ACCION_GESTION_OBTENER_PLAZO, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2, diferido:=False,
                                                                                                                      idnt_Elemento:=DatosARTEZ.Idnt_Elemento, sistemaFuncional:=DatosARTEZ.ObtenerSistemaFuncional, grupoUsuarios:=DatosARTEZ.ObtenerGrupoUsuarios,
                                                                                                                      usuario:=DatosARTEZ.ObtenerUsuario, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=DatosGenerales.ObtenerNombrePathEvaluacionCaducidadAlertaPlazo(),
                                                                                                                      comentario:=String.Empty, parametrosEspecificos:=parametrosEspecificos, habilitado:=True))

                ' Comprobamos si en el Plazo se ha indicado una variable de flujo funcional para la Fecha Fin Plazo
                If Plazo.HayVariableFechaFinPlazo Then
                    ' Inicializamos la variable con el valor recuperado de la acción Obtener Plazo
                    FlowOrder += 100

                    ' Determinamos las Variables a Inicializar
                    Dim listadoVariables As New Dictionary(Of String, String) From {
                        {Plazo.NombreVariableFechaFinPlazo, String.Format("@INICIO!FECHA_FIN_PLAZO_T_{0}", DatosGenerales.OrdenTramite)}
                    }
                    _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LETVAR(FlowOrder, path:=String.Format("INI FECHA_FIN_PLAZO_{0:000}", DatosGenerales.OrdenTramite), level:=2, listadoVariables:=listadoVariables, deInicio:=True))
                End If

                ' Verificación del estado de la Fecha de Alerta
                FlowOrder += 100
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_IF(FlowOrder, path:=DatosGenerales.ObtenerNombrePathEvaluacionCaducidadAlertaPlazo(),
                                                                                                            valorParametroVAR1:=String.Format("@INICIO!IND_PLAZO_CADUCADO_T_{0}", DatosGenerales.OrdenTramite), valorParametroVAR2:=Constantes.S, condicion:=Constantes.IGUAL,
                                                                                                            tipoComparacion:="STRING", saltoTRUE:=DatosGenerales.ObtenerNombrePathAccionGestionCaducarTarea(), saltoFALSE:=DatosGenerales.ObtenerNombrePathALERT(), level:=2,
                                                                                                            comentario:="Alerta Caducada"))

                ' Creación de la Alerta de Motor
                FlowOrder += 100

                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_ALERT(FlowOrder, path:=DatosGenerales.ObtenerNombrePathALERT(), fechaReferencia:=String.Format("@INICIO!FECHA_FIN_PLAZO_T_{0}", DatosGenerales.OrdenTramite),
                                                                                                               maturation:=String.Format("@INICIO!MATURATION_T_{0}", DatosGenerales.OrdenTramite), interval:=String.Format("@INICIO!INTERVAL_T_{0}", DatosGenerales.OrdenTramite),
                                                                                                               pathAlerta:=DatosGenerales.ObtenerNombrePathAccionGestionCaducarTarea(), level:=2, comentario:=String.Empty))
            End If
        End Sub

        Private Sub AddLineasDetalleEjecucionTareaTramite(ByRef catalogoAcciones As CatalogoAcciones)
            If Not DatosGenerales.TipoTramite.Equals(TipoTramite.AutomaticoControlM) AndAlso Not DatosGenerales.TipoTramite.Equals(TipoTramite.PortaFirmasControlM) Then
                ' Llamada a la Acción de Gestión que Crea la Tarea
                FlowOrder += 100
                ContadorReferencias += 1

                ' Determinamos la Acción de Gestión a Invocar
                Dim nombrePasoLlamadaAccionGestion As String = String.Empty
                Dim nombreFlujoAccionGestion As String = String.Empty
                Select Case DatosGenerales.TipoTramite
                    Case TipoTramite.RecepcionAcuseRecibo

                        ' Inicializamos las variables para realizar la Creación de una Tarea de Recepción de Acuse de ARTEZ
                        nombrePasoLlamadaAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionCrearTareaRecepcionAcuseRecibo()
                        nombreFlujoAccionGestion = Constantes.ACCION_GESTION_CREAR_TAREA_RECEPCION_ACUSE_RECIBO
                        TipoTratamientoTramite = TipoTratamientoTramite.Automatico

                    Case TipoTramite.ComprobacionRequisitos

                        ' Inicializamos las variables para realizar la Creación de una Tarea de Comprobación de Requisitos
                        nombrePasoLlamadaAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionCrearTareaComprobacionRequisitos()
                        nombreFlujoAccionGestion = Constantes.ACCION_GESTION_CREAR_TAREA_COMPROBACION_REQUISITOS
                        TipoTratamientoTramite = TipoTratamientoTramite.Manual

                    Case TipoTramite.Automatico,
                         TipoTramite.FirmaSello,
                         TipoTramite.Planificado,
                         TipoTramite.PortaFirmas,
                         TipoTramite.OrdenNotificacion,
                         TipoTramite.ProcesarNotificacion,
                         TipoTramite.ProcesoPlanificado

                        ' Inicializamos las variables para realizar la Creación de una Tarea Automática de ARTEZ
                        nombrePasoLlamadaAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionCrearTareaAutomatica()
                        nombreFlujoAccionGestion = Constantes.ACCION_GESTION_CREAR_TAREA_AUTOMATICA
                        TipoTratamientoTramite = TipoTratamientoTramite.Automatico

                    Case TipoTramite.Manual,
                         TipoTramite.RechazoFirmas,
                         TipoTramite.OrdenNotificacionManual

                        ' Inicializamos las variables para realizar la Creación de una Tarea Manual de ARTEZ
                        nombrePasoLlamadaAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionCrearTareaManual()
                        nombreFlujoAccionGestion = Constantes.ACCION_GESTION_CREAR_TAREA_MANUAL
                        TipoTratamientoTramite = TipoTratamientoTramite.Manual
                End Select

                ' Determinamos el Punto de Retorno de la Acción de Gestión
                Dim nombrePuntoRetornoAccionGestion As String = String.Empty
                Select Case DatosGenerales.TipoTramite
                    Case TipoTramite.RecepcionAcuseRecibo
                        ' El Path de Retorno de la Acción de Gestión será la Comprobación del Estado de la Notificación, ya que si está Acusada no hay que parar la Tramitación
                        nombrePuntoRetornoAccionGestion = DatosGenerales.ObtenerNombrePathComprobacionNotificacionAcusada()

                    Case TipoTramite.Automatico

                        ' Comprobamos si estamos tratando un Trámite con Plazo, ya que en caso afirmativo hay que saltar al paso FORM
                        If TienePlazo Then
                            nombrePuntoRetornoAccionGestion = DatosGenerales.ObtenerNombrePathFROM

                        Else
                            ' Comprobamos si tenemos Pasos en el Trámite
                            If TienePasos Then
                                ' El trámite tiene pasos, así que de la Creación de la Tarea pasamos al Primer Paso
                                nombrePuntoRetornoAccionGestion = ObtenerNombrePrimerPaso(catalogoAcciones)

                            Else
                                ' El trámite no tiene pasos, así que de la Creación de la Tarea pasamos a la Finalización del Trámite
                                nombrePuntoRetornoAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionFinalizarTarea
                            End If
                        End If

                    Case TipoTramite.FirmaSello,
                         TipoTramite.PortaFirmas,
                         TipoTramite.OrdenNotificacion,
                         TipoTramite.ProcesarNotificacion,
                         TipoTramite.Planificado

                        ' Se indica que el siguiente paso a ejecutar es la Acción Ejecutar Tarea Automática
                        nombrePuntoRetornoAccionGestion = DatosGenerales.ObtenerNombrePathAccionGestionEjecutarTareaAutomatica

                    Case TipoTramite.Manual,
                         TipoTramite.RechazoFirmas,
                         TipoTramite.OrdenNotificacionManual,
                         TipoTramite.ProcesoPlanificado,
                         TipoTramite.ComprobacionRequisitos

                        ' Se indica que el siguiente paso a ejecutar es el paso FORM de parada
                        nombrePuntoRetornoAccionGestion = DatosGenerales.ObtenerNombrePathFROM
                End Select

                ' Determinamos los Parámetros Específicos a pasar a la Acción de Gestión (Entrada y Salida)
                Dim parametrosEspecificosEntrada As Dictionary(Of String, String) = Nothing
                Dim parametrosEspecificosSalida As Dictionary(Of String, String) = Nothing
                Dim numeroParametrosEspecificosEntradaTratados As Integer = 0

                ' Determinamos los Parámetros Específicos de Entrada Obligatorios por Tipo de Trámite
                Select Case DatosGenerales.TipoTramite
                    Case TipoTramite.RecepcionAcuseRecibo
                        ' Obtenemos los Parámetros de Entrada y Salida necesarios para poder generar un Trámite de Recepción de Acuse de Recibo
                        parametrosEspecificosEntrada = DatosTramiteRecepcionAcuseRecibo.ObtenerParametrosEntrada()

                    Case TipoTramite.Manual,
                         TipoTramite.ComprobacionRequisitos

                        ' Incluimos los Parámetros para el Modo de Creación y las Observaciones
                        parametrosEspecificosEntrada = New Dictionary(Of String, String) From {
                            {"@MODO_CREACION_IN", DatosARTEZ.ModoCreacion},
                            {"@OBSERVACIONES_IN", Utilidades.Utilidades.ObtenerValorParaParametro(DatosARTEZ.Observaciones)}
                        }

                        ' Comprobamos si estamos tratando un Trámite de Elaboración de Documentos
                        If Not IsNothing(DatosARTEZ.DatosElaboracionDocumento) Then
                            ' Pasamoe el Parámetro con el Código de Documento a Elboarar en el Trámite Manual
                            parametrosEspecificosEntrada.Add("@NOMBRE_PARAM_1", "TipoDocumentoN8")
                            parametrosEspecificosEntrada.Add("@VALOR_PARAM_1", DatosARTEZ.DatosElaboracionDocumento.CodigoDocumento)

                            ' Inicializamos el número de Parámetros Específicos Tratados
                            numeroParametrosEspecificosEntradaTratados = 1

                            ' Comprobamos si se va a Habilitar la Pestaña de Firma
                            If DatosARTEZ.DatosElaboracionDocumento.HabilitarPestanaFirma Then
                                parametrosEspecificosEntrada.Add("@NOMBRE_PARAM_2", "pestaniaFirma")
                                parametrosEspecificosEntrada.Add("@VALOR_PARAM_2", DatosARTEZ.DatosElaboracionDocumento.ValorHabilitarPestanaFirma)

                                ' Inicializamos el número de Parámetros Específicos Tratados
                                numeroParametrosEspecificosEntradaTratados = 2
                            End If
                        End If

                    Case TipoTramite.ProcesarNotificacion
                        ' Pasamos los parámetros de la Notificación a Integrar con NT
                        parametrosEspecificosEntrada = New Dictionary(Of String, String) From {
                            {"@NOMBRE_PARAM_1", "DocumentoEmision"},
                            {"@VALOR_PARAM_1", String.Format("@INICIO!DOC_EMISION_{0}", ContadorProcesosNotificacionesNT)},
                            {"@NOMBRE_PARAM_2", "NumeroIdentificacionNotificacion"},
                            {"@VALOR_PARAM_2", String.Format("@INICIO!IDNT_NT_{0}", ContadorProcesosNotificacionesNT)}
                        }

                        ' Inicializamos el número de Parámetros Específicos Tratados
                        numeroParametrosEspecificosEntradaTratados = 2

                    Case TipoTramite.RechazoFirmas
                        ' Pasamos el Parámetro de la Firma Rechazada
                        parametrosEspecificosEntrada = New Dictionary(Of String, String) From {
                            {"@NOMBRE_PARAM_1", "IdntPeticionSolicitudFirma"},
                            {"@VALOR_PARAM_1", String.Format("@INICIO!IDNT_PETICION_FIRMA_{0}", ContadorProcesosFirmaPortaFirmas)}
                        }

                        ' Inicializamos el número de Parámetros Específicos Tratados
                        numeroParametrosEspecificosEntradaTratados = 1
                End Select

                ' Comprobamos si tenemos Parámetros Específicos para el Trámite
                If TieneParametrosEspecificosCreacionTarea Then
                    If IsNothing(parametrosEspecificosEntrada) Then
                        parametrosEspecificosEntrada = New Dictionary(Of String, String)
                    End If

                    ' Incluimos los Parámetros Específicos para la Creación de la Tarea
                    For Each parametroEspecifico As ParametroAccion In DatosARTEZ.ParametrosEspecificosCreacionTarea
                        ' Obtenemos el Número de Parámetros Tratados
                        numeroParametrosEspecificosEntradaTratados += 1

                        ' Incluimos el Parámetro
                        parametrosEspecificosEntrada.Add(String.Format("@NOMBRE_PARAM_{0}", numeroParametrosEspecificosEntradaTratados), Utilidades.Utilidades.ObtenerValorParaParametro(parametroEspecifico.Nombre))
                        parametrosEspecificosEntrada.Add(String.Format("@VALOR_PARAM_{0}", numeroParametrosEspecificosEntradaTratados), Utilidades.Utilidades.ObtenerValorParaParametro(parametroEspecifico.Valor))
                    Next
                End If

                '' Determinamos los Parámetros de Salida
                'If TipoTratamientoTramite.Equals(TipoTratamientoTramite.Automatico) Then
                '    If IsNothing(parametrosEspecificosSalida) Then
                '        parametrosEspecificosSalida = New Dictionary(Of String, String)
                '    End If

                '    parametrosEspecificosSalida.Add("@IDNT_TAREA_AUTOMATICA", String.Format("IDNT_TAREA_AUT_T_{0}", DatosGenerales.OrdenTramite))
                'End If

                ' Obtenemos los Parámetros Específicos
                ParametrosEspecificosCreacionTarea = ObtenerParametrosEspecificos(parametrosEspecificosEntrada, parametrosEspecificosSalida)

                ' Obtenemos las líneas de detalle de Creación de la Acción de Gestión
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(IdentificadorFlujo, FlowOrder, path:=nombrePasoLlamadaAccionGestion, baseDatosAccion:=BaseDatosInfraestructura, accion:=nombreFlujoAccionGestion,
                                                                                                                      aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2, diferido:=False, idnt_Elemento:=DatosARTEZ.Idnt_Elemento,
                                                                                                                      sistemaFuncional:=DatosARTEZ.ObtenerSistemaFuncional, grupoUsuarios:=DatosARTEZ.ObtenerGrupoUsuarios, usuario:=DatosARTEZ.ObtenerUsuario,
                                                                                                                      baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=nombrePuntoRetornoAccionGestion, comentario:=String.Empty, parametrosEspecificos:=ParametrosEspecificosCreacionTarea,
                                                                                                                      habilitado:=True))

                ' Incluimos los Pasos y la llamada a la Acción de Gestión que Ejecuta las Tareas Automáticas
                If DatosGenerales.TipoTramite.Equals(TipoTramite.Automatico) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.FirmaSello) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.PortaFirmas) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.OrdenNotificacion) OrElse
                       DatosGenerales.TipoTramite.Equals(TipoTramite.ProcesarNotificacion) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.Planificado) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.ProcesoPlanificado) OrElse
                       DatosGenerales.TipoTramite.Equals(TipoTramite.RecepcionAcuseRecibo) Then
                    ' Si estamos en un Trámite Automático y tenemos Plazo, hay que obtener las Sentencias para incluir el paso FORM
                    If DatosGenerales.TipoTramite.Equals(TipoTramite.Automatico) AndAlso TienePlazo Then
                        FlowOrder += 100
                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_FORM(FlowOrder, path:=DatosGenerales.ObtenerNombrePathFROM, level:=2, idnt_Elemento:=DatosARTEZ.Idnt_Elemento, tipoEjecucion:="M",
                                                                                                                          listadoParamtrosEspecificos:=ObtenerParametrosEspecificosParadaTramite()))
                    End If

                    ' Si estamos en un Trámite Automático y tenemos Pasos, hay que obtener las Sentencias para incluir los Pasos
                    If DatosGenerales.TipoTramite.Equals(TipoTramite.Automatico) AndAlso TienePasos Then
                        AddLineasDetallePasosTramite(catalogoAcciones)
                    End If

                    ' Comprobamos si estamos en un Trámite que requiere un Paso FORM antes de llamar a la Acción de Gestión EEJECUTAR_TAREA_AUTMATCA
                    If DatosGenerales.TipoTramite.Equals(TipoTramite.ProcesoPlanificado) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.RecepcionAcuseRecibo) Then
                        ' Incluimos el paso FORM de espera
                        FlowOrder += 100
                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_FORM(FlowOrder, path:=DatosGenerales.ObtenerNombrePathFROM, level:=2, idnt_Elemento:=DatosARTEZ.Idnt_Elemento, tipoEjecucion:="P",
                                                                                                                          listadoParamtrosEspecificos:=ObtenerParametrosEspecificosParadaTramite()))

                        ' Comprobamos si tenemos Pasos, y en caso afirmativo los añadimos
                        If TienePasos Then
                            AddLineasDetallePasosTramite(catalogoAcciones)
                        End If
                    End If

                    ' Solo se Inserta la Acción de Gestión Ejecutar Tarea Automática cuando el Tipo de Trámite NO es AUTOMATICO, PROCESO_PLANIFICADO o RECEPCION_ACUSE_RECIBO
                    If Not DatosGenerales.TipoTramite.Equals(TipoTramite.Automatico) AndAlso Not DatosGenerales.TipoTramite.Equals(TipoTramite.ProcesoPlanificado) AndAlso Not DatosGenerales.TipoTramite.Equals(TipoTramite.RecepcionAcuseRecibo) Then
                        ' Obtenemos las Sentencias para insertar la Acción de Gestión que Ejecuta la Tarea Automática
                        FlowOrder += 100
                        ContadorReferencias += 1

                        ' Determinamos el Punto de Retorno de la Acción de Gestión Ejecutar Tarea Automática
                        Dim nombrePuntoRetornoEjecucionTarea As String = String.Empty
                        Select Case DatosGenerales.TipoTramite
                            Case TipoTramite.FirmaSello,
                                     TipoTramite.OrdenNotificacion

                                'El Punto de Retonro será la Acción de Gestión Finalizar Tarea
                                nombrePuntoRetornoEjecucionTarea = DatosGenerales.ObtenerNombrePathAccionGestionFinalizarTarea

                            Case TipoTramite.PortaFirmas,
                                     TipoTramite.ProcesarNotificacion

                                'El Punto de Retonro será el paso FORM
                                nombrePuntoRetornoEjecucionTarea = DatosGenerales.ObtenerNombrePathFROM

                            Case TipoTramite.Planificado

                                'El Punto de Retonro será la Acción de la Tarea Planificada
                                nombrePuntoRetornoEjecucionTarea = DatosGenerales.ObtenerNombrePathComprobacionEjecucionTareaPlanificada
                        End Select

                        ' Determinamos los parámetros específicos
                        Dim parametrosEspecificosEjecucionTarea As New Dictionary(Of String, String) From {
                                {"@TIPO_TAREA_AUTOMATICA", ObtenerDescripcionTipoTramite()},
                                {"@IDNT_TAREA_AUTOMATICA", String.Format("@INICIO!IDNT_TAREA_AUT_T_{0}", DatosGenerales.OrdenTramite)}
                            }
                        If DatosGenerales.TipoTramite.Equals(TipoTramite.FirmaSello) OrElse DatosGenerales.TipoTramite.Equals(TipoTramite.OrdenNotificacion) Then
                            parametrosEspecificosEjecucionTarea.Add("@DECISION", String.Format("DECISION_T_{0}", DatosGenerales.OrdenTramite))
                            If DatosGenerales.TipoTramite.Equals(TipoTramite.OrdenNotificacion) Then
                                parametrosEspecificosEjecucionTarea.Add("@DOC_EMISION", String.Format("DOC_EMISION_{0}", ContadorProcesosNotificacionesNT))
                                parametrosEspecificosEjecucionTarea.Add("@IDNT_NT", String.Format("IDNT_NT_{0}", ContadorProcesosNotificacionesNT))
                            End If

                        ElseIf DatosGenerales.TipoTramite.Equals(TipoTramite.Planificado) Then
                            ' Comprobamos si se ha informado la Variable del Trámite Planificado
                            If String.IsNullOrEmpty(DatosGenerales.VariableTramitePlanificado) Then
                                Throw New Exception(String.Format("El Trámite '{0}' de Tipo 'PLANIFICADO' no tiene informada la Variable para realizar la Comprobación", DatosGenerales.Nombre))
                            End If
                            parametrosEspecificosEjecucionTarea.Add("@DECISION", DatosGenerales.VariableTramitePlanificado)

                        ElseIf DatosGenerales.TipoTramite.Equals(TipoTramite.PortaFirmas) Then
                            ' Asociamos el ID de la Petición de firma a la variable IDNT_PETICION_FIRMA correspondiente a la firma que se está tratando
                            parametrosEspecificosEjecucionTarea.Add("@IDNT_PETICION_FIRMA", String.Format("IDNT_PETICION_FIRMA_{0}", ContadorProcesosFirmaPortaFirmas))
                        End If
                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathAccionGestionEjecutarTareaAutomatica, baseDatosAccion:=BaseDatosInfraestructura,
                                                                                                                              accion:=Constantes.ACCION_GESTION_EJECUTAR_TAREA_AUTOMATICA, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2,
                                                                                                                              diferido:=False, idnt_Elemento:=DatosARTEZ.Idnt_Elemento, sistemaFuncional:=DatosARTEZ.ObtenerSistemaFuncional,
                                                                                                                              grupoUsuarios:=DatosARTEZ.ObtenerGrupoUsuarios, usuario:=DatosARTEZ.ObtenerUsuario, baseDatosRetorno:=BaseDatosDestinoFlujo,
                                                                                                                              pathRetorno:=nombrePuntoRetornoEjecucionTarea, comentario:=String.Empty, parametrosEspecificos:=parametrosEspecificosEjecucionTarea, habilitado:=True))
                    End If
                End If
            End If
        End Sub

        Private Function ObtenerParametrosEspecificos(parametrosEspecificosEntrada As Dictionary(Of String, String), parametrosEspecificosSalida As Dictionary(Of String, String))
            Dim parametrosEspecificos As Dictionary(Of String, String) = Nothing

            If Not IsNothing(parametrosEspecificosEntrada) OrElse Not IsNothing(parametrosEspecificosSalida) Then
                parametrosEspecificos = New Dictionary(Of String, String)

                If Not IsNothing(parametrosEspecificosEntrada) Then
                    parametrosEspecificos = parametrosEspecificos.Concat(parametrosEspecificosEntrada).ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)
                End If

                If Not IsNothing(parametrosEspecificosSalida) Then
                    parametrosEspecificos = parametrosEspecificos.Concat(parametrosEspecificosSalida).ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)
                End If
            End If

            Return parametrosEspecificos
        End Function

        Private Sub AddLineasDetallePasosTramite(ByRef catalogoAcciones As CatalogoAcciones)
            AddLineasDetallePasosTramite(catalogoAcciones, String.Empty)
        End Sub

        Private Sub AddLineasDetallePasosTramite(ByRef catalogoAcciones As CatalogoAcciones, nombrePathTratamientoSalida As String)
            ' Nos recorremos los pasos
            For Each paso As Paso In Pasos
                ' Aumentamos el FlowOrder
                FlowOrder += 100

                ' Determinamos si se debe aumentar el contador de Referencias
                If paso.EsPasoConReferencia() Then
                    ContadorReferencias += 1
                End If

                ' Llamamos a la Función para Obtener los Pasos en Función del Tipo de Trámite
                If EsTramiteARTEZ Then
                    ' Estamos Tratando un Trámite ARTEZ o Bloque de Tramitación Comúun
                    paso.ObtenerLineasDetalleTramite(catalogoAcciones, Pasos, DatosGenerales, DatosARTEZ, BaseDatosDestinoFlujo, IdentificadorFlujo, FlowOrder, Constantes.APLICACION, ContadorReferencias)

                Else
                    ' Estamos Tatando un Támite Ficticio
                    paso.ObtenerLineasDetalletramiteFicticio(catalogoAcciones, Pasos, nombrePathTratamientoSalida, DatosGenerales, BaseDatosDestinoFlujo, IdentificadorFlujo, FlowOrder, Constantes.APLICACION, ContadorReferencias)
                End If

                ' Añadimos las Líneas de Detalle del Paso al Detalle del Trámite
                _lineasDetalle.AddRange(paso.LineasDetalle)
            Next
        End Sub

        Private Sub AddLineasDetalleParadaTramite(ByRef catalogoAcciones As CatalogoAcciones)
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.Manual,
                     TipoTramite.Planificado,
                     TipoTramite.ComprobacionRequisitos,
                     TipoTramite.PortaFirmas,
                     TipoTramite.RechazoFirmas,
                     TipoTramite.ProcesarNotificacion,
                     TipoTramite.OrdenNotificacionManual,
                     TipoTramite.PortaFirmasControlM,
                     TipoTramite.AutomaticoControlM

                    ' Comprobamos si estamos tratando un Trámite Planificado
                    If DatosGenerales.TipoTramite.Equals(TipoTramite.Planificado) Then
                        ' Insertamos la Evaluación del Resultado de la Ejecución de la Tarea Planificada
                        FlowOrder += 100
                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_IF(FlowOrder, path:=DatosGenerales.ObtenerNombrePathComprobacionEjecucionTareaPlanificada,
                                                                                                                    valorParametroVAR1:=DatosGenerales.ObtenerLiteralLecturaValorVariableTareaPlanificada, valorParametroVAR2:=String.Empty, condicion:=Constantes.IGUAL,
                                                                                                                    tipoComparacion:="STRING", saltoTRUE:=DatosGenerales.ObtenerNombrePathFROM, saltoFALSE:=DatosGenerales.ObtenerNombrePathAccionGestionFinalizarTarea,
                                                                                                                    level:=2, comentario:="Ejecución Tarea Planificada sin Resultado, así que hay que esperar a detectar/obtener el resultado"))
                    End If

                    ' Formulario de Parada en la Tarea
                    FlowOrder += 100
                    ' Obtenemos las Líneas de Detalle del Formulario de Parada
                    _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_FORM(FlowOrder, path:=DatosGenerales.ObtenerNombrePathFROM, level:=2, idnt_Elemento:=DatosARTEZ.Idnt_Elemento, tipoEjecucion:="M",
                                                                                                                  listadoParamtrosEspecificos:=ObtenerParametrosEspecificosParadaTramite()))

                    ' Comprobamos si estamos tratando un Trámite Planificado
                    If DatosGenerales.TipoTramite.Equals(TipoTramite.Planificado) Then
                        ' Insertamos la Evaluación de la Respuesta del Formulario de Espera
                        FlowOrder += 100
                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_IF(FlowOrder, path:=DatosGenerales.ObtenerNombrePathComprobacionRespuestaFormularioTareaPlanificada,
                                                                                                                    valorParametroVAR1:=DatosGenerales.ObtenerLiteralLectutaValorVariableRespuestaFormulario, valorParametroVAR2:=String.Empty, condicion:=Constantes.IGUAL,
                                                                                                                    tipoComparacion:="STRING", saltoTRUE:=DatosGenerales.ObtenerNombrePathFROM, saltoFALSE:=DatosGenerales.ObtenerNombrePathInicializacionVariableTareaPlanificada,
                                                                                                                    level:=2, comentario:="Ejecución Tarea Planificada sin Resultado, así que hay que esperar a detectar/obtener el resultado"))
                    End If

                    ' Insertamos las Inicializaciones de Variables tras el Formulario de Parada (si es necesario)
                    AddLineasDetalleInicializacionVariableTrasFormularioParada()

                    ' Insertamos los Pasos del Trámite (si tiene Pasos)
                    If TienePasos Then
                        AddLineasDetallePasosTramite(catalogoAcciones)
                    End If
            End Select
        End Sub

        Private Function ObtenerParametrosEspecificosParadaTramite() As Dictionary(Of String, String)
            ' Determinamos los Parámetros Generales a Asociar al paso FORM del Trámite
            Dim parametros As New Dictionary(Of String, String) From {
                {"@RESPUESTA", String.Empty}
            }

            ' Determinamos los Parámetros Específicos a Asociar al paso FORM del Trámite en función del Tipo de Trámite
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.OrdenNotificacionManual
                    parametros.Add("@DOCUMENTO_EMISION", String.Empty)

                Case TipoTramite.Manual
                    If Not IsNothing(DatosARTEZ.DatosElaboracionDocumento) Then
                        parametros.Add("@DOCGESTORDOCUMENTAL", String.Empty)
                        parametros.Add("@DOC_FIRMADO_FUERA_ARTEZ", String.Empty)

                        ' Comprobamos si se habilitar la Pestaña de Firma
                        If DatosARTEZ.DatosElaboracionDocumento.HabilitarPestanaFirma Then
                            ' Hay que incluir los parámetros para el tratamiento de Firma
                            parametros.Add("@TIPOSOLICITUDFIRMAN8", String.Empty)
                            parametros.Add("@PETSOLICITUDFIRMAN8", String.Empty)
                            parametros.Add("@FECHAFIRMA", String.Empty)
                        End If
                    End If
            End Select

            ' Completamos los Parámetros Específicos si el trámite tiene pasos de Inicialización de Variables donde se vayan recuperar los valores del paso FORM
            Dim parametrosVariablesOrigenValorFORM = ObtenerParametrosInicializacionVariablesOrigenValorFORM()
            If Not IsNothing(parametrosVariablesOrigenValorFORM) Then
                For Each parametro As String In parametrosVariablesOrigenValorFORM
                    parametros.Add(String.Format("{0}{1}", If(Not parametro.StartsWith("@"), "@", String.Empty), parametro), String.Empty)
                Next
            End If

            Return parametros
        End Function

        Private Function ObtenerParametrosInicializacionVariablesOrigenValorFORM() As List(Of String)
            Dim parametrosVariablesOrigenValorFORM As List(Of String) = Nothing

            If TienePasosInicializacionVariables Then
                ' Nos recorremos los pasos de Inicialización de Variables
                For Each paso As Paso In Pasos.FindAll(Function(x) x.Tipo.Equals(TipoPaso.InicializacionVariables))
                    Dim variables As List(Of String) = paso.ObtenerVariablesOrigenValorPasoForm
                    If Not IsNothing(variables) Then
                        If IsNothing(parametrosVariablesOrigenValorFORM) Then
                            parametrosVariablesOrigenValorFORM = New List(Of String)
                        End If
                        parametrosVariablesOrigenValorFORM.AddRange(variables)
                    End If
                Next
            End If

            Return parametrosVariablesOrigenValorFORM
        End Function

        Private Sub AddLineasDetalleInicializacionVariableTrasFormularioParada()
            Select Case DatosGenerales.TipoTramite
                Case TipoTramite.Manual
                    FlowOrder += 100
                    ' Determinamos las Variables a Inicializar si estamos tratando un Trámite de Elaboración de Documento
                    If Not IsNothing(DatosARTEZ.DatosElaboracionDocumento) Then
                        Dim listadoVariables As New Dictionary(Of String, String) From {
                            {DatosARTEZ.DatosElaboracionDocumento.VariableDocumentoElaborado, DatosGenerales.ObtenerLiteralLectutaValorVariableDocumentoGestorDocumentalFormulario},
                            {DatosARTEZ.DatosElaboracionDocumento.VariableDocumentoFirmadoFueraARTEZ, DatosGenerales.ObtenerLiteralLectutaValorVariableDocumentoFirmadoFueraARTEZFormulario}
                        }

                        ' Comprobamos si tenemos la Pestaña de Firma Habilitada
                        If DatosARTEZ.DatosElaboracionDocumento.HabilitarPestanaFirma Then
                            ' Hay que incluir el Tratamiento para los Datos de Firma
                            listadoVariables.Add(DatosARTEZ.DatosElaboracionDocumento.DatosFirma.VariableTipoFirma, ObtenerLiteralLectutaValorVariablePeticionSolicitudFirma)
                            listadoVariables.Add(DatosARTEZ.DatosElaboracionDocumento.DatosFirma.VariableSolicitudFirma, DatosGenerales.ObtenerLiteralLectutaValorVariablePeticionSolicitudFirmaN8)
                            listadoVariables.Add(DatosARTEZ.DatosElaboracionDocumento.DatosFirma.VariableFechaFirma, DatosGenerales.ObtenerLiteralLectutaValorVariableFechaFirma)
                        End If

                        _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LETVAR(FlowOrder, path:=DatosGenerales.ObtenerNombrePathInicializacionVariableTareaManualElaboracionDocumento, level:=2, listadoVariables:=listadoVariables,
                                                                                                                        deInicio:=True))
                    End If

                Case TipoTramite.OrdenNotificacionManual
                    FlowOrder += 100
                    ' Determinamos las Variables a Inicializar
                    Dim listadoVariables As New Dictionary(Of String, String) From {
                        {String.Format("@DOC_EMISION_{0}", ContadorProcesosNotificacionesNT), String.Format("@{0}!DOCUMENTO_EMISION", DatosGenerales.ObtenerNombrePathFROM)}
                    }
                    _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LETVAR(FlowOrder, path:=String.Format("LET INI DOC_EMISION_{0:000}", DatosGenerales.OrdenTramite), level:=2, listadoVariables:=listadoVariables, deInicio:=True))

                Case TipoTramite.Planificado
                    FlowOrder += 100
                    ' Determinamos las Variables a Inicializar
                    Dim listadoVariables As New Dictionary(Of String, String) From {
                        {DatosGenerales.ObtenerNombreVariableTareaPlanificadaInicializacion, DatosGenerales.ObtenerLiteralLectutaValorVariableRespuestaFormulario}
                    }
                    _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_LETVAR(FlowOrder, path:=DatosGenerales.ObtenerNombrePathInicializacionVariableTareaPlanificada, level:=2, listadoVariables:=listadoVariables, deInicio:=True))
            End Select
        End Sub

        Private Function ObtenerLiteralLectutaValorVariablePeticionSolicitudFirma() As String
            Return DatosGenerales.ObtenerLiteralLectutaValorVariablePeticionSolicitudFirma(DatosARTEZ.DatosElaboracionDocumento.ValorHabilitarPestanaFirma, DatosARTEZ.DatosElaboracionDocumento.DatosFirma.TipoFirmaPorDefecto)
        End Function

        Private Sub AddLineasDetalleFinalizacionTareaTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            If Not DatosGenerales.TipoTramite.Equals(TipoTramite.AutomaticoControlM) AndAlso Not DatosGenerales.TipoTramite.Equals(TipoTramite.PortaFirmasControlM) Then
                ' Llamada a la Acción de Gestión que Finaliza la Tarea
                FlowOrder += 100
                ContadorReferencias += 1

                ' Obtenemos los Datos del Path de Retorno para la Acción de Gestión Finalizar Tarea, y el indicador para determinar si se debe incluir un JUMP si el Componente para Finalizar la Tarea no existe
                Dim puntoSalida As String = String.Empty
                Dim saltoDirecto As Boolean = False
                ObtenerDatosPathRetornoFinalizacionTarea(resumenTramitesNombreEnFlujo, puntoSalida, saltoDirecto)

                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathAccionGestionFinalizarTarea, baseDatosAccion:=BaseDatosInfraestructura,
                                                                                                                      accion:=Constantes.ACCION_GESTION_FINALIZAR_TAREA, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2, diferido:=False,
                                                                                                                      idnt_Elemento:=DatosARTEZ.Idnt_Elemento, sistemaFuncional:=DatosARTEZ.ObtenerSistemaFuncional, grupoUsuarios:=DatosARTEZ.ObtenerGrupoUsuarios,
                                                                                                                      usuario:=DatosARTEZ.ObtenerUsuario, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=puntoSalida, comentario:=String.Empty,
                                                                                                                      parametrosEspecificos:=Nothing, habilitado:=True))
            End If
        End Sub

        Private Function ObtenerTipoTramite() As String
            Return If(TipoTratamientoTramite.Equals(TipoTratamientoTramite.Manual), Constantes.TIPO_TAREA_MANUAL, Constantes.TIPO_TAREA_AUTOMATICA)
        End Function

        Private Sub ObtenerDatosPathRetornoFinalizacionTarea(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef puntoSalidaFinalizacionTarea As String, ByRef saltoDirecto As Boolean)
            If TienePlazo Then
                ' El trámite tiene un Plazo asociado, así que el punto de salida para la Acción de Gestión Finalizar Tarea será el tratamiento para cancelar la Alerta, y marcamos que es una salida directa
                puntoSalidaFinalizacionTarea = DatosGenerales.ObtenerNombrePathCANCEL_ALERT
                saltoDirecto = False

            Else
                ' Obtenemos los datos de la manera en la que se va a saltar al siguiente trámite
                ObtenerDatosSaltoSiguienteTramite(resumenTramitesNombreEnFlujo, saltoDirecto, puntoSalidaFinalizacionTarea)
            End If
        End Sub

        Private Sub ObtenerDatosSaltoSiguienteTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), ByRef saltoDirecto As Boolean, ByRef siguienteTramite As String)
            Select Case NumeroSalidas
                Case Constantes.NUMERO_0
                    ' El trámite no tiene ninguna salida parametrizada, así que el siguiente "trámite" es FIN_TRAMITACION y no hay que evaluar ninguna condición
                    siguienteTramite = Constantes.ACCION_GESTION_FIN_TRAMITACION
                    saltoDirecto = True

                Case Constantes.NUMERO_1
                    ' El trámite SOLO tiene una salida parametrizada, así que tenemos que determinar si tiene alguna condición de salida o no:
                    '   - Si la salida tiene una condición, el punto de salida será la evaluación correspondiente
                    '   - Si la salida no tiene una condición, el punto de salida será el trámite indicado
                    If PrimeraSalida.Condicionada() Then
                        ' La Salida está condicionada, así que el puntio de salida será la Evaluación de la salida
                        siguienteTramite = String.Format("SALIDA_T_{0:000}_1", DatosGenerales.OrdenTramite)
                        saltoDirecto = False

                    Else
                        ' La Salida no está condicionada, así que el puntio de salida será el Trámite indicado en la Salida
                        siguienteTramite = If(String.IsNullOrEmpty(PrimeraSalida.TramiteDestino), Constantes.ACCION_GESTION_FIN_TRAMITACION, Utilidades.Utilidades.ObtenerPathTramite(resumenTramitesNombreEnFlujo, PrimeraSalida.TramiteDestino))
                        saltoDirecto = True
                    End If

                Case Else
                    ' El trámite tiene más de una salida parametrizada, así que el/los siguiente/s trámite/s se deben evaluar
                    siguienteTramite = String.Format("SALIDA_T_{0:000}_1", DatosGenerales.OrdenTramite)
                    saltoDirecto = False
            End Select
        End Sub

        Private Sub AddLineasDetalleCancelarAlertaTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            If TienePlazo Then
                ' Creación del Paso de Motor CANCEL para eliminar la Alerta que pudiera existir
                FlowOrder += 100
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_CANCEL(FlowOrder, path:=DatosGenerales.ObtenerNombrePathCANCEL_ALERT, valorPasoCancelar:=DatosGenerales.ObtenerNombrePathALERT, level:=2))

                ' Insertamos el salto al siguiente Trámite cuando estemos tratando un salto directo
                Dim siguienteTramite As String = String.Empty
                Dim saltoDirecto As Boolean = False
                ObtenerDatosSaltoSiguienteTramite(resumenTramitesNombreEnFlujo, saltoDirecto, siguienteTramite)
                If saltoDirecto Then
                    FlowOrder += 100
                    ContadorJUMPs += 1
                    _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(FlowOrder, path:=String.Format("JUMP_{0:0000}", ContadorJUMPs), puntoSalto:=siguienteTramite, level:=2, comentario:=String.Empty))
                End If
            End If
        End Sub

        Private Sub AddLineasDetalleUnionRamasParalelas()
            ' De momento se crea el paso UNION para unir dos ramas Paralelas, pero sin lógica
            Dim numeroRamasUnir As Integer = 2

            ' Creación del Paso de Motor UNION para unir las Ramas Paralelas
            FlowOrder += 100
            ContadorUNIONs += 1
            _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_UNION(FlowOrder, path:=String.Format("UNION_{0:0000}", ContadorUNIONs), numeroRamasParalelas:=numeroRamasUnir, level:=2, comentario:=String.Empty))
        End Sub

        Private Sub AddLineasDetalleTratamientoSalidasTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            AddLineasDetalleTratamientoSalidasTramite(resumenTramitesNombreEnFlujo, esTramiteFicticio:=False)
        End Sub

        Private Sub AddLineasDetalleTratamientoSalidasTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), esTramiteFicticio As Boolean)
            If NumeroSalidas > Constantes.NUMERO_0 Then
                Select Case NumeroSalidas
                    Case Constantes.NUMERO_1
                        ' El trámite solo tiene un Salida
                        If esTramiteFicticio Then
                            AddLineasDetalleTramientoSalidaUnicaTramiteFicticio(resumenTramitesNombreEnFlujo)

                        Else
                            AddLineasDetalleTratamientoSalidaUnica(resumenTramitesNombreEnFlujo)
                        End If

                    Case Else
                        ' El trámite tiene más de una Salida                        
                        AddLineasDetalleTratamientoSalidasNoExcluyentesTramite(resumenTramitesNombreEnFlujo)
                End Select
            End If
        End Sub

        Private Sub AddLineasDetalleTramientoSalidaUnicaTramiteFicticio(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            ' Comprobamos si la única Salida está condicionada
            If PrimeraSalida.Condicionada() Then
                ' La Salida está Condicionada
                _lineasDetalle.AddRange(PrimeraSalida.ObtenerLineasDetalleSalidaNoExcluyente(FlowOrder, datosGenerales:=DatosGenerales, contadorSalida:=1, esUltimaSalidaTramite:=If(NumeroSalidas.Equals(Constantes.NUMERO_1), True, False), contadorJUMPs:=ContadorJUMPs,
                                                                                             resumenTramitesNombreEnFlujo:=resumenTramitesNombreEnFlujo))

            Else
                ' La salida no está Condicionada
                ' Si el Trámite no tiene pasos o tiene pasos y el último es de tipo Inicialización de Variables, hay que incluir un JUMP
                If Not TienePasos OrElse (TienePasos AndAlso Pasos.Last.Tipo.Equals(TipoPaso.InicializacionVariables)) Then
                    _lineasDetalle.AddRange(PrimeraSalida.ObtenerLineasDetalleSalidaUnica(FlowOrder, ContadorJUMPs, resumenTramitesNombreEnFlujo))
                End If
            End If
        End Sub

        Private Sub AddLineasDetalleTratamientoSalidaUnica(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            ' Comprobamos si la única Salida está condicionada, ya que en caso contrario ya ha sido tratada
            If PrimeraSalida.Condicionada() Then
                _lineasDetalle.AddRange(PrimeraSalida.ObtenerLineasDetalleSalidaNoExcluyente(FlowOrder, datosGenerales:=DatosGenerales, contadorSalida:=1, esUltimaSalidaTramite:=If(NumeroSalidas.Equals(Constantes.NUMERO_1), True, False), contadorJUMPs:=ContadorJUMPs,
                                                                                             resumenTramitesNombreEnFlujo:=resumenTramitesNombreEnFlujo))
            End If
        End Sub

        Private Sub AddLineasDetalleTratamientoSalidasNoExcluyentesTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            ' Nos recorremos las Salidas
            Dim contadorSalida As Integer = 1
            For Each salida As Salida In Salidas
                ' Obtenemos el valor del Parámeotr PATH
                _lineasDetalle.AddRange(salida.ObtenerLineasDetalleSalidaNoExcluyente(FlowOrder, datosGenerales:=DatosGenerales, contadorSalida:=contadorSalida, esUltimaSalidaTramite:=If(contadorSalida.Equals(NumeroSalidas), True, False), contadorJUMPs:=ContadorJUMPs,
                                                                                      resumenTramitesNombreEnFlujo:=resumenTramitesNombreEnFlujo))

                ' Avanzamos el Contador de Salidas
                contadorSalida += 1
            Next
        End Sub

        Private Sub AddLineasDetalleCaducarTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String))
            If TienePlazo Then
                ' Llamada a la Acción de Gestión para Caducar la Tarea
                FlowOrder += 100
                ContadorReferencias += 1

                ' Obtenemos los Parámetros Específicos para la Acción Caducar Trámite
                Dim parametrosEspecificos As New Dictionary(Of String, String) From {
                    {"@PLAZO_CADUCADO_IN", String.Format("@INICIO!IND_PLAZO_CADUCADO_T_{0}", DatosGenerales.OrdenTramite)},
                    {"@TIPO_TAREA_IN", ObtenerTipoTramite()}
                }

                If Not IsNothing(ParametrosEspecificosCreacionTarea) Then
                    parametrosEspecificos = parametrosEspecificos.Concat(ParametrosEspecificosCreacionTarea).ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)
                End If

                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_APIINIENDEXT(IdentificadorFlujo, FlowOrder, path:=DatosGenerales.ObtenerNombrePathAccionGestionCaducarTarea, baseDatosAccion:=BaseDatosInfraestructura,
                                                                                                                      accion:=Constantes.ACCION_GESTION_CADUCAR_TAREA, aplicacion:=Constantes.APLICACION, contadorReferencia:=ContadorReferencias, level:=2, diferido:=False,
                                                                                                                      idnt_Elemento:=DatosARTEZ.Idnt_Elemento, sistemaFuncional:=DatosARTEZ.ObtenerSistemaFuncional, grupoUsuarios:=DatosARTEZ.ObtenerGrupoUsuarios,
                                                                                                                      usuario:=DatosARTEZ.ObtenerUsuario, baseDatosRetorno:=BaseDatosDestinoFlujo, pathRetorno:=DatosGenerales.ObtenerNombrePathCANCEL_FORM, comentario:=String.Empty,
                                                                                                                      parametrosEspecificos:=parametrosEspecificos, habilitado:=True))

                ' Insertamos el paso para Cancelar el Formulario de la Tarea
                FlowOrder += 100
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_CANCEL(FlowOrder, path:=DatosGenerales.ObtenerNombrePathCANCEL_FORM, valorPasoCancelar:=DatosGenerales.ObtenerNombrePathFROM, level:=2))

                ' Insertamoe el Salto a la Siguiente tarea
                FlowOrder += 100
                ContadorJUMPs += 1
                _lineasDetalle.AddRange(UtilidadesCreacionLineasDetalleFlujo.ObtenerLineasDetalle_Accion_JUMP(FlowOrder, path:=String.Format("JUMP_{0:0000}", ContadorJUMPs), puntoSalto:=If(String.IsNullOrEmpty(Plazo.TramiteDestino), Constantes.ACCION_GESTION_FIN_TRAMITACION,
                                                                                                              Utilidades.Utilidades.ObtenerPathTramite(resumenTramitesNombreEnFlujo, Plazo.TramiteDestino)), level:=2, comentario:=String.Empty))
            End If
        End Sub

        Private Function ObtenerNombrePrimerPaso(ByRef catalogoAcciones As CatalogoAcciones) As String
            Return Pasos.First.ObtenerNombrePaso(DatosGenerales.OrdenTramite, catalogoAcciones)
        End Function
#End Region
    End Class
End Namespace