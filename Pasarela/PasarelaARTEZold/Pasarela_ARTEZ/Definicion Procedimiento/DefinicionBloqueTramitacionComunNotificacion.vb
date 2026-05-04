Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionBloqueTramitacionComunNotificacion
        <XmlElement("SoloEsperaAcuseRecibo", IsNullable:=True)>
        Public Property SoloEsperaAcuseReciboXML As String

        <XmlElement("SolicitudNotificacionARTEZ", IsNullable:=True)>
        Public Property SolicitudNotificacionARTEZ As String

        <XmlElement("IdentificadorNotificacionNT", IsNullable:=True)>
        Public Property IdentificadorNotificacionNT As String

        <XmlElement("DocumentoANotificar", IsNullable:=True)>
        Public Property DocumentoANotificar As String

        <XmlElement("CodigoActuacion", IsNullable:=True)>
        Public Property CodigoActuacion As String

        <XmlElement("ParametrosGneracionPeticonNotificacion", IsNullable:=True)>
        Public Property ParametrosGeneracionPeticionNotificacion As ParametrosGeneracionPeticionNotificacion

        <XmlElement("Tramites", IsNullable:=True)>
        Public Property Tramites As TramitesBloqueTramitacionComunNotificacion

        <XmlElement("DatosNotificacionTelePapel", IsNullable:=True)>
        Public Property DatosNotificacionTelePapel As NotificacionTelePapel

        <XmlElement("VariableFechaNotificacion")>
        Public Property VariableFechaNotificacion As String

        '******************************************************************************************************************************
        '******************************************************************************************************************************
        ' Parámetros Antiguos que deberían desaparecer
        '******************************************************************************************************************************
        '******************************************************************************************************************************
        <XmlElement("TipoNotificacion", IsNullable:=True)>
        Public Property TipoNotificacion As String

        <XmlElement("Idnt_TramiteDarOrdenNotificacion")>
        Public Property Idnt_TramiteDarOrdenNotificacion As String

        <XmlElement("Idnt_TramiteProcesarNotificacion")>
        Public Property Idnt_TramiteProcesarNotificacion As String

        <XmlElement("Idnt_TramiteDarOrdenNotificacionManual")>
        Public Property Idnt_TramiteDarOrdenNotificacionManual As String

        <XmlElement("Idnt_TramiteCorreccionErroresNotificacion")>
        Public Property Idnt_TramiteCorreccionErroresNotificacion As String

        <XmlElement("DatosReponsableTramiteDarOrdenNotificacionManual")>
        Public Property DatosReponsableTramiteDarOrdenNotificacionManual As DatosResponsableTarea

        <XmlElement("DatosReponsableTramiteCorreccionErroresNotificacion")>
        Public Property DatosReponsableTramiteCorreccionErroresNotificacion As DatosResponsableTarea

        <XmlElement("ModoCreacionTramitesManuales")>
        Public Property ModoCreacionTramitesManuales As String

        <XmlElement("ObservacionesTramitesManuales")>
        Public Property ObservacionesTramitesManuales As String

        <XmlIgnore>
        Private ReadOnly Property SoloEsperaAcuseRecibo As Boolean
            Get
                Return If(Not IsNothing(SoloEsperaAcuseReciboXML), If(SoloEsperaAcuseReciboXML.Equals(Constantes.S), True, False), False)
            End Get
        End Property


#Region "Métodos Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Comprobamos el valor del Parámetro SoloEsperaAcuseReciboXML 
            If Not String.IsNullOrEmpty(SoloEsperaAcuseReciboXML) AndAlso Not (SoloEsperaAcuseReciboXML.Equals(Constantes.S) OrElse SoloEsperaAcuseReciboXML.Equals(Constantes.N)) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' tiene informado el Parámetro 'SoloEsperaAcuseRecibo', pero el valor tiene que ser S o N.", nombreBloque))
            End If

            ' Determinamos la Verificación a Realizar
            If SoloEsperaAcuseRecibo Then
                ' Solo se va a Tratar la Redepción del Acuse de Recibo, así que la Definición tiene que ser nueva
                VerificarDefinicionNueva(nombreBloque)

            ElseIf EsDefinicionAntigua() Then
                ' Se está tratando una Definición Antigua
                VerificarDefinicionAntigua(nombreBloque)

            Else
                ' Se está tratando una Definición Nueva
                VerificarDefinicionNueva(nombreBloque)
            End If

            ' Comporbamos si tenemos el Grupo para la Notificación en Tele-Papel
            If Not IsNothing(DatosNotificacionTelePapel) Then
                DatosNotificacionTelePapel.Verificar(nombreBloque)
            End If
        End Sub

        Friend Function ObtenerParametrosEspecificos(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String)) As Dictionary(Of String, String)
            Dim parametrosBloqueNotificacion As New Dictionary(Of String, String) From {
                {"@SOLICITUD_NOTI_ARTEZ_IN", ObtenerValorParaParametro(SolicitudNotificacionARTEZ)},
                {"@IDNT_NOTIFICACION_NT_IN", ObtenerValorParaParametro(IdentificadorNotificacionNT)},
                {"@DOCUMENTO_NOTIFICAR_IN", ObtenerValorParaParametro(DocumentoANotificar)},
                {"@CODIGO_ACTUACION_IN", ObtenerValorParaParametro(CodigoActuacion)},
                {"@MODO_EMISION_IN", ObtenerValorParametroModoEmision()},
                {"@TIPO_EMISION_IN", ObtenerValorParametroTipoEmision()},
                {"@TIENE_ACUSE_IN", ObtenerValorParametroTieneAcuse()},
                {"@EMISOR_IN", ObtenerValorParametroEmisor()},
                {"@ASUNTO_IN", ObtenerValorParametroAsunto()},
                {"@INDICADOR_BOLETIN_IN", ObtenerValorParametroIndicadorBoletin()},
                {"@TIPO_DESTINATARIO_IN", ObtenerValorParametroTipoDestinatario()},
                {"@TIPO_NOTIFICACION_IN", ObtenerValorParametroTipoNotificacion()},
                {"@TRAM_OBT_DATOS_NOTI_IN", ObtenerValorParametroTramiteObtenerDatosNotificacion()},
                {"@TRAM_ESPERA_EMISION_IN", ObtenerValorParametroTramiteEsperaEmisionNotificacion()},
                {"@TRAM_ESPERA_ACUSE_IN", ObtenerValorParametroTramiteEsperaAcuseRecibo()},
                {"@TRAM_INF_DATOS_NOTI_IN", ObtenerValorParametroTramiteInformarDatosNotificacion()},
                {"@SF_INF_DATOS_NOTI_IN", ObtenerValorParametroSistemaFuncionalTramiteInformarDatosNotificacion()},
                {"@GU_INF_DATOS_NOTI_IN", ObtenerValorParametroGrupoUsuariosTramiteInformarDatosNotificacion()},
                {"@US_INF_DATOS_NOTI_IN", ObtenerValorParametroUsuarioTramiteInformarDatosNotificacion()},
                {"@MODO_CREA_INF_DAT_NOT_IN", ObtenerValorParametroModoCreacionTramiteInformarDatosNotificacion()},
                {"@OBSERV_INF_DAT_NOT_IN", ObtenerValorParametroObservacionesTramiteInformarDatosNotificacion()},
                {"@TRAM_REVI_ERRORES_NOT_IN", ObtenerValorParametroTramiteRevisionErroresNotificacion()},
                {"@SF_REVI_ERRORES_NOT_IN", ObtenerValorParametroSistemaFuncionalTramiteRevisionErroresNotificacion()},
                {"@GU_REVI_ERRORES_NOT_IN", ObtenerValorParametroGrupoUsuariosTramiteRevisionErroresNotificacion()},
                {"@US_REVI_ERRORES_NOT_IN", ObtenerValorParametroUsuarioTramiteRevisionErroresNotificacion()},
                {"@MODO_CREA_REV_ERR_NOT_IN", ObtenerValorParametroModoCreacionTramiteRevisionErroresNotificacion()},
                {"@OBSERV_REV_ERR_NOT_IN", ObtenerValorParametroObservacionesTramiteRevisionErroresNotificacion()}
            }

            ' Comprobamos si podemos estar tratando una Notificación de Tipo Tele-Papel
            If Not IsNothing(DatosNotificacionTelePapel) AndAlso DatosNotificacionTelePapel.EsNotificacionTelePapel Then
                parametrosBloqueNotificacion.Add("@ES_TELE_PAPEL_IN", DatosNotificacionTelePapel.EsTelePapel)
                parametrosBloqueNotificacion.Add("@COD_PLAZO_NOTI_TELE_IN", DatosNotificacionTelePapel.CodigoPlazoNotificacionTelematica)
                parametrosBloqueNotificacion.Add("@TRAM_DESTINO_FIN_TELE_IN", DatosNotificacionTelePapel.TramiteDestinoCaducidadPlazo(resumenTramitesNombreEnFlujo))
            Else
                parametrosBloqueNotificacion.Add("@ES_TELE_PAPEL_IN", Constantes.N)
                parametrosBloqueNotificacion.Add("@COD_PLAZO_NOTI_TELE_IN", String.Empty)
                parametrosBloqueNotificacion.Add("@TRAM_DESTINO_FIN_TELE_IN", String.Empty)
            End If

            ' Incluimos el Parámetro de Salida
            parametrosBloqueNotificacion.Add("@FECHA_NOTIFICACION_OUT", VariableFechaNotificacion)

            ' Devolvemos los Parámetros del Bloque de Notificación
            Return parametrosBloqueNotificacion
        End Function
#End Region

#Region "Métodos Privados"
        Private Function EsDefinicionAntigua() As Boolean
            ' Con tener informado uno de los Parámetros antiguos sería suficiente para determinar que es una Definición antigua
            Return If(Not String.IsNullOrEmpty(Idnt_TramiteDarOrdenNotificacion) OrElse Not String.IsNullOrEmpty(Idnt_TramiteProcesarNotificacion) OrElse Not String.IsNullOrEmpty(Idnt_TramiteDarOrdenNotificacionManual) OrElse Not String.IsNullOrEmpty(Idnt_TramiteCorreccionErroresNotificacion),
                        True,
                        False)
        End Function

        Private Sub VerificarDefinicionNueva(nombreBloque As String)
            ' Comprobamos si tenemos creado el Parámetro Tramites
            If IsNothing(Tramites) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Grupo de Trámites.", nombreBloque))
            End If

            With Tramites
                ' Comprobamos si solo se va Tratar la Recepción del Acuse de Recibo
                If Not SoloEsperaAcuseRecibo Then
                    If IsNothing(.TramiteObtenerDatosNotificacion) OrElse Not .TramiteObtenerDatosNotificacion.DatosBasicosInformados Then
                        Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado correctamente el Trámite 'Obtención Datos de Notificación'.", nombreBloque))
                    End If

                    If IsNothing(.TramiteEsperaEmisionNotificacion) OrElse Not .TramiteEsperaEmisionNotificacion.DatosBasicosInformados Then
                        Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado correctamente el Trámite 'Espera Emisión de Notificación'.", nombreBloque))
                    End If

                    If IsNothing(.TramiteRevisionErroresNotificacion) OrElse Not .TramiteRevisionErroresNotificacion.DatosBasicosInformados Then
                        Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado correctamente el Trámite 'Revisión Errores Emisión/Recepción de Notificación'.", nombreBloque))
                    End If

                    If String.IsNullOrEmpty(VariableFechaNotificacion) Then
                        Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Nombre de la Variable para la Fecha de Acuse/Notificación.", nombreBloque))
                    End If
                End If

                If IsNothing(.TramiteEsperaAcuseRecibo) OrElse Not .TramiteEsperaAcuseRecibo.DatosBasicosInformados Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado correctamente el Trámite 'Espera Acuse Recibo'.", nombreBloque))
                End If

                ' Comprobamos si tenemos creado el Parámetro IdentificadorNotificacionNT
                If Not String.IsNullOrEmpty(IdentificadorNotificacionNT) And (IsNothing(.TramiteEsperaAcuseRecibo) OrElse Not .TramiteEsperaAcuseRecibo.DatosBasicosInformados) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' tiene informado el Parámetro 'IdentificadorNotificacionNT', pero no tiene informado correctamente el Trámite 'Espera Recepción Acuse Recibo'.", nombreBloque))
                End If
            End With
        End Sub

        Private Sub VerificarDefinicionAntigua(nombreBloque As String)
            ' Es una Definición Antigua
            If String.IsNullOrEmpty(Idnt_TramiteDarOrdenNotificacion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Identificador del Trámite 'Obtención Datos de Notificación'.", nombreBloque))
            End If

            If String.IsNullOrEmpty(Idnt_TramiteProcesarNotificacion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Identificador del Trámite 'Espera Emisión de Notificación'.", nombreBloque))
            End If

            If String.IsNullOrEmpty(Idnt_TramiteDarOrdenNotificacionManual) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Identificador del Trámite 'Informar Datos de Notificación'.", nombreBloque))
            End If

            If String.IsNullOrEmpty(Idnt_TramiteCorreccionErroresNotificacion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informado el Identificador del Trámite 'Revisión Errores Emisión/Recepción de Notificación'.", nombreBloque))
            End If

            If Not DatosReponsableTramiteDarOrdenNotificacionManual.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informados los Datos de Responsable para el Trámite 'Informar Datos de Notificación'.", nombreBloque))
            End If

            If Not DatosReponsableTramiteCorreccionErroresNotificacion.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Notificación '{0}' no tiene informados los Datos de Responsable para el Trámite 'Revisión Errores Emisión/Recepción de Notificación'.", nombreBloque))
            End If
        End Sub

        Private Function ObtenerValorParametroModoEmision() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.ModoEmision))
        End Function

        Private Function ObtenerValorParametroTipoEmision() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.TipoEmision))
        End Function

        Private Function ObtenerValorParametroTieneAcuse() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.TieneAcuseRecibo))
        End Function

        Private Function ObtenerValorParametroEmisor() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.Emisor))
        End Function

        Private Function ObtenerValorParametroAsunto() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.Asunto))
        End Function

        Private Function ObtenerValorParametroIndicadorBoletin() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.IndicadorBoletin))
        End Function

        Private Function ObtenerValorParametroTipoDestinatario() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.TipoDestinatario))
        End Function

        Private Function ObtenerValorParametroTipoNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(TipoNotificacion), ObtenerValorParaParametro(ParametrosGeneracionPeticionNotificacion.TipoNotificacion))
        End Function

        Private Function ObtenerValorParametroTramiteObtenerDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(Idnt_TramiteDarOrdenNotificacion), ObtenerIdentificadorTramiteAutomatico(Tramites.TramiteObtenerDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroTramiteEsperaEmisionNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(Idnt_TramiteProcesarNotificacion), ObtenerIdentificadorTramiteAutomatico(Tramites.TramiteEsperaEmisionNotificacion))
        End Function

        Private Function ObtenerValorParametroTramiteEsperaAcuseRecibo() As String
            Return If(EsDefinicionAntigua(), String.Empty, ObtenerIdentificadorTramiteAutomatico(Tramites.TramiteEsperaAcuseRecibo))
        End Function

        Private Function ObtenerValorParametroTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(Idnt_TramiteDarOrdenNotificacionManual), ObtenerIdentificadorTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroSistemaFuncionalTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteDarOrdenNotificacionManual.SistemaFuncional, ObtenerSistemaFuncionalTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroGrupoUsuariosTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteDarOrdenNotificacionManual.GrupoUsuarios, ObtenerGrupoUsuariosTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroUsuarioTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteDarOrdenNotificacionManual.Usuario, ObtenerUsuarioTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroModoCreacionTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(ModoCreacionTramitesManuales), ObtenerModoCreacionTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroObservacionesTramiteInformarDatosNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(ObservacionesTramitesManuales), ObtenerObservacionesTramiteManual(Tramites.TramiteInformarDatosNotificacion))
        End Function

        Private Function ObtenerValorParametroTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(Idnt_TramiteCorreccionErroresNotificacion), ObtenerIdentificadorTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function

        Private Function ObtenerValorParametroSistemaFuncionalTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteCorreccionErroresNotificacion.SistemaFuncional, ObtenerSistemaFuncionalTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function

        Private Function ObtenerValorParametroGrupoUsuariosTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteCorreccionErroresNotificacion.GrupoUsuarios, ObtenerGrupoUsuariosTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function

        Private Function ObtenerValorParametroUsuarioTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), DatosReponsableTramiteCorreccionErroresNotificacion.Usuario, ObtenerUsuarioTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function

        Private Function ObtenerValorParametroModoCreacionTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(ModoCreacionTramitesManuales), ObtenerModoCreacionTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function

        Private Function ObtenerValorParametroObservacionesTramiteRevisionErroresNotificacion() As String
            Return If(EsDefinicionAntigua(), ObtenerValorParaParametro(ObservacionesTramitesManuales), ObtenerObservacionesTramiteManual(Tramites.TramiteRevisionErroresNotificacion))
        End Function
#End Region
    End Class
End Namespace