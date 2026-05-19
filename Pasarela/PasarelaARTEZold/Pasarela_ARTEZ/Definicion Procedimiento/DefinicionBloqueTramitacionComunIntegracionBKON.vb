Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionBloqueTramitacionComunIntegracionBKON
        <XmlElement("Idnt_TramiteObtenerInformacion")>
        Public Property Idnt_TramiteObtenerInformacion As String

        <XmlElement("Idnt_TramiteValidacionCarga")>
        Public Property Idnt_TramiteValidacionCarga As String

        <XmlElement("Idnt_TramiteCargaExpediente")>
        Public Property Idnt_TramiteCargaExpediente As String

        <XmlElement("Idnt_TramiteCargaDocumentos")>
        Public Property Idnt_TramiteCargaDocumentos As String

        <XmlElement("Idnt_TramiteEsperaDatosResolucion")>
        Public Property Idnt_TramiteEsperaDatosResolucion As String

        <XmlElement("Idnt_TramiteRevisionValidacionCarga")>
        Public Property Idnt_TramiteRevisionValidacionCarga As String

        <XmlElement("Idnt_TramitePendiente")>
        Public Property Idnt_TramitePendiente As String

        <XmlElement("ModoCreacionTramitesManuales", IsNullable:=True)>
        Public Property ModoCreacionTramitesManuales As String

        <XmlElement("ObservacionesTramitesManuales", IsNullable:=True)>
        Public Property ObservacionesTramitesManuales As String

        <XmlElement("DatosResponsableTramitesManuales")>
        Public Property DatosResponsableTramitesManuales As DatosResponsableTarea

        <XmlElement("DescriptorServicio")>
        Public Property DescriptorServicio As String

        <XmlElement("TipoOperacion")>
        Public Property TipoOperacion As String

        <XmlElement("ARTEZEnviaDatosResolucion")>
        Public Property ARTEZEnviaDatosResolucion As String

        <XmlElement("DatosResolucion", IsNullable:=True)>
        Public Property DatosResolucion As DatosResolucion

        <XmlElement("OrganoGestor")>
        Public Property OrganoGestor As String

        <XmlElement("DocumentosCargar")>
        Public Property DocumentosCargar As String

        <XmlElement("DatosTramitacionOrigen", IsNullable:=True)>
        Public Property DatosTramitacionOrigen As DatosTramitacionOrigen

#Region "Métodos Publicos y Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Verificamos que se ha informado el Identificador del Trámite para Obtener la Información para BKON
            If String.IsNullOrEmpty(Idnt_TramiteObtenerInformacion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite para la Obtención de la Información para BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite para Validar la Carga en BKON
            If String.IsNullOrEmpty(Idnt_TramiteValidacionCarga) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite para la Validación de la Carga en BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite de Carga Expediente en BKON
            If String.IsNullOrEmpty(Idnt_TramiteCargaExpediente) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite de Carga de Expediente en BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite de Carga Documentos en BKON
            If String.IsNullOrEmpty(Idnt_TramiteCargaDocumentos) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite de Carga de Documentos en BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite para Revisar la Validación de la Carga en BKON
            If String.IsNullOrEmpty(Idnt_TramiteRevisionValidacionCarga) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite para la Revisión de la Validación de la Carga en BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite Pendiente
            If String.IsNullOrEmpty(Idnt_TramitePendiente) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Identificador del Trámite Pendiente.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite Pendiente
            If String.IsNullOrEmpty(DescriptorServicio) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Descriptor del Servicio para la Obtenión de la Información para BKON.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Identificador del Trámite Pendiente
            If String.IsNullOrEmpty(TipoOperacion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Tipo de Operación.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Órgano Gestor
            If String.IsNullOrEmpty(OrganoGestor) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Órgano Gestor.", nombreBloque))
            End If

            ' Verificamos que se ha informado el Listado de Documentos a Cargar
            If String.IsNullOrEmpty(DocumentosCargar) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Listado de Documentos a Cargar.", nombreBloque))
            End If

            ' Verificamos que se ha informado el objeto Datos Resolución
            ValidarDatosBasicosDatosResolucion(nombreBloque)

            ' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
            ValidarDatosBasicosResponsablesTramitesManuales(nombreBloque)
        End Sub

        Friend Function ObtenerParametrosEspecificos() As Dictionary(Of String, String)
            Return New Dictionary(Of String, String) From {
                {"@TRAM_OBT_INFO_BKON_IN", Idnt_TramiteObtenerInformacion},
                {"@TRAM_VAL_CARG_BKON_IN", Idnt_TramiteValidacionCarga},
                {"@TRAM_CARG_BKON_IN", Idnt_TramiteCargaExpediente},
                {"@TRAM_CARG_DOCS_BKON_IN", Idnt_TramiteCargaDocumentos},
                {"@TRAM_ESPERA_DAT_RESO_IN", Idnt_TramiteEsperaDatosResolucion},
                {"@TRAM_REVI_VALI_CARGA_IN", Idnt_TramiteRevisionValidacionCarga},
                {"@TRAM_PENDIENTE_IN", Idnt_TramitePendiente},
                {"@MODO_CREACION_TRAM_MA_IN", ObtenerValorParaParametro(ModoCreacionTramitesManuales)},
                {"@OBSERVACIONES_TRAM_MA_IN", ObtenerValorParaParametro(ObservacionesTramitesManuales)},
                {"@SF_TRAMITES_MANUALES_IN", DatosResponsableTramitesManuales.SistemaFuncional},
                {"@GU_TRAMITES_MANUALES_IN", DatosResponsableTramitesManuales.GrupoUsuarios},
                {"@US_TRAMITES_MANUALES_IN", DatosResponsableTramitesManuales.Usuario},
                {"@DESCRIPTOR_SERVICIO_IN", ObtenerValorParaParametro(DescriptorServicio)},
                {"@TIPO_OPERACION_IN", ObtenerValorParaParametro(TipoOperacion)},
                {"@ARTEZ_ENVIA_OF_IN", ObtenerValorParaParametro(ARTEZEnviaDatosResolucion)},
                {"@TIPO_RESOLUCION_IN", If(Not IsNothing(DatosResolucion), ObtenerValorParaParametro(DatosResolucion.TipoAcuerdo), String.Empty)},
                {"@FECHA_RESOLUCION_IN", If(Not IsNothing(DatosResolucion), ObtenerValorParaParametro(DatosResolucion.FechaAcuerdo), String.Empty)},
                {"@ANIO_RESOLUCION_IN", If(Not IsNothing(DatosResolucion), ObtenerValorParaParametro(DatosResolucion.AnioAcuerdo), String.Empty)},
                {"@NUMERO_RESOLUCION_IN", If(Not IsNothing(DatosResolucion), ObtenerValorParaParametro(DatosResolucion.NumeroAcuerdo), String.Empty)},
                {"@ORGANO_GESTOR_IN", ObtenerValorParaParametro(OrganoGestor)},
                {"@DOCUMENTOS_CARGAR_IN", ObtenerValorParaParametro(DocumentosCargar)},
                {"@IDNT_PROC_NEGOCIO_IN", If(Not IsNothing(DatosTramitacionOrigen), ObtenerValorParaParametro(DatosTramitacionOrigen.Idnt_Procedimiento), String.Empty)},
                {"@DESCRIP_PROC_NEGOCIO_IN", If(Not IsNothing(DatosTramitacionOrigen), ObtenerValorParaParametro(DatosTramitacionOrigen.DescriptorProcedimiento), String.Empty)},
                {"@INSTANCIA_NEGOCIO_IN", If(Not IsNothing(DatosTramitacionOrigen), ObtenerValorParaParametro(DatosTramitacionOrigen.Instancia), String.Empty)},
                {"@IDNT_ESP_CREAR_EXPTE_IN", If(Not IsNothing(DatosTramitacionOrigen), ObtenerValorParaParametro(DatosTramitacionOrigen.Idnt_Elemento), String.Empty)}
            }
        End Function
#End Region

#Region "Métodos Privados"
        Private Sub ValidarDatosBasicosDatosResolucion(nombreBloque As String)
            If Not IsNothing(DatosResolucion) Then
                ' Tenemos el Objeto Datos Resolución
                If Not String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informado el Parámetro 'FechaAcuerdo' del Bloque 'DatosResolucion', pero no tiene informados los Parámetros 'AnioAcuerdo' y 'NumeroAcuerdo'.",
                                                      nombreBloque))

                ElseIf String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informado el Parámetro 'AnioAcuerdo' del Bloque 'DatosResolucion', pero no tiene informados los Parámetros 'FechaAcuerdo' y 'NumeroAcuerdo'.",
                                                      nombreBloque))

                ElseIf String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informado el Parámetro 'NumeroAcuerdo' del Bloque 'DatosResolucion', pero no tiene informados los Parámetros 'FechaAcuerdo' y 'AnioAcuerdo'.",
                                                      nombreBloque))

                ElseIf Not String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informados los Parámetros 'FechaAcuerdo' y 'AnioAcuerdo' del Bloque 'DatosResolucion', pero no tiene informado el Parámetro 'NumeroAcuerdo'.",
                                                      nombreBloque))

                ElseIf Not String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informados los Parámetros 'FechaAcuerdo' y 'NumeroAcuerdo' del Bloque 'DatosResolucion', pero no tiene informado el Parámetro 'AnioAcuerdo'.",
                                                      nombreBloque))

                ElseIf String.IsNullOrEmpty(DatosResolucion.FechaAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.AnioAcuerdo) AndAlso Not String.IsNullOrEmpty(DatosResolucion.NumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' tiene informados los Parámetros 'AnioAcuerdo' y 'NumeroAcuerdo' del Bloque 'DatosResolucion', pero no tiene informado el Parámetro 'FechaAcuerdo'.",
                                                      nombreBloque))
                End If
            End If
        End Sub

        Private Sub ValidarDatosBasicosResponsablesTramitesManuales(nombreBloque As String)
            If IsNothing(DatosResponsableTramitesManuales) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Bloque 'DatosResponsableTramitesManuales' con los datos de Asignación para los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque))

            ElseIf String.IsNullOrEmpty(DatosResponsableTramitesManuales.SistemaFuncionalXML) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Sistema Funcional para la Asignación de los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque))

            ElseIf String.IsNullOrEmpty(DatosResponsableTramitesManuales.GrupoUsuariosXML) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de BKON '{0}' no tiene informado el Grupo de Usuarios para la Asignación de los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque))
            End If
        End Sub
#End Region
    End Class
End Namespace

