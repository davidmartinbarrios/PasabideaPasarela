Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionBloqueTramitacionComunFirma
        <XmlElement("DocumentoFirmadoFueraARTEZ", IsNullable:=True)>
        Public Property DocumentoFirmadoFueraARTEZ As String

        <XmlElement("TipoFirma")>
        Public Property TipoFirma As TipoFirma

        <XmlElement("DocumentoAFirmar", IsNullable:=True)>
        Public Property DocumentoAFirmar As String

        <XmlElement("TipoDocumentoAFirmar", IsNullable:=True)>
        Public Property TipoDocumentoAFirmar As String

        <XmlElement("SolicitudFirmaARTEZ", IsNullable:=True)>
        Public Property SolicitudFirmaARTEZ As String

        <XmlElement("ModoCreacionTramitesManuales", IsNullable:=True)>
        Public Property ModoCreacionTramitesManuales As String

        <XmlElement("ObservacionesTramitesManuales", IsNullable:=True)>
        Public Property ObservacionesTramitesManuales As String

        <XmlElement("DatosReponsableTramitesManuales", IsNullable:=True)>
        Public Property DatosReponsableTramitesManuales As DatosResponsableTarea

        <XmlElement("DatosFirmaFlujoPredefinida", IsNullable:=True)>
        Public Property DatosFirmaFlujoPredefinida As DatosFirmaFlujoPredefinido

        <XmlElement("DatosFirmaFlujoNiveles", IsNullable:=True)>
        Public Property DatosFirmaFlujoNiveles As DatosFirmaFlujoPorNiveles

        <XmlElement("DatosFirmaCSVOrganico", IsNullable:=True)>
        Public Property DatosFirmaCSVOrganico As DatosFirmaCSVOrganico

        <XmlElement("DatosFirmaPropia", IsNullable:=True)>
        Public Property DatosFirmaPropia As DatosFirmaPropia

        <XmlElement("DatosFirmaFlujoAdHock", IsNullable:=True)>
        Public Property DatosFirmaFlujoAdHock As DatosFirmaFlujoAdHock

        <XmlElement("DatosFirmaDeducidaEjecucion", IsNullable:=True)>
        Public Property DatosFirmaDeducidaEjecucion As DatosFirmaDeducidaEjecucion

        <XmlElement("DatosRA", IsNullable:=True)>
        Public Property DatosRA As DatosRA

        <XmlElement("VariableRespuestaFK")>
        Public Property VariableRespuestaFK As String

        <XmlElement("VariableFechaFirma")>
        Public Property VariableFechaFirma As String

        <XmlElement("VariableFechaAcuerdo", IsNullable:=True)>
        Public Property VariableFechaAcuerdo As String

        <XmlElement("VariableAnioAcuerdo", IsNullable:=True)>
        Public Property VariableAnioAcuerdo As String

        <XmlElement("VariableNumeroAcuerdo", IsNullable:=True)>
        Public Property VariableNumeroAcuerdo As String

#Region "Métodos Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Verificamos que los Datos Obligatorios Generales están Informados
            If TipoFirma.Equals(TipoFirma.None) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' no tiene informado el Tipo de Firma.", nombreBloque))
            End If

            ' Verificamos que tenemos la Información necesaria para poder realizar la Llamada al Bloque de Tramitación Común
            Select Case TipoFirma
                Case TipoFirma.Propia
                    ' Validamos los Datos Básicos para la Firma Propia
                    ValidarDatosBasicosFirmaPropia(nombreBloque)

                Case TipoFirma.Propia_Y_Niveles
                    ' Validamos los Datos Básicos para la Firma Propia
                    ValidarDatosBasicosFirmaPropia(nombreBloque)

                    ' Validamos los Datos Báscios para la Firma por Niveles
                    ValidarDatosBasicosFirmaFlujoNiveles(nombreBloque)

                Case TipoFirma.Predefinida
                    ' Validamos los Datos Básicos para la Firma Predefinida
                    ValidarDatosBasicosFirmaFlujoPredefinido(nombreBloque)

                Case TipoFirma.Niveles
                    ' Validamos los Datos Báscios para la Firma por Niveles
                    ValidarDatosBasicosFirmaFlujoNiveles(nombreBloque)

                Case TipoFirma.CSVOrganico
                    ' Validamos los Datos Báscios para la Firma de CSV de Orgánico
                    ValidarDatosBasicosFirmaCSVOrganico(nombreBloque)

                Case TipoFirma.AdHock
                    ' Validamos los Datos Báscios para la Firma Ad-Hock
                    ValidarDatosBasicosFirmaFlujoAdHock(nombreBloque)

                Case TipoFirma.Deducida_Ejecucion
                    ' Validamos los Datos Báscios para la Firma Deducida en Ejecución
                    ValidarDatosBasicosFirmaDeducidaEjecucion(nombreBloque)

                Case Else
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no está permitido.", nombreBloque, ObtenerDescripcionTipoFirma()))
            End Select

            ' Verificamos los Datos de RA
            VerificarDatosRA(nombreBloque)
        End Sub

        Friend Function ObtenerParametrosEspecificos() As Dictionary(Of String, String)
            Return ObtenerParametrosLlamada()
        End Function
#End Region

#Region "Métodos Privados"
        Private Sub VerificarDatosRA(nombreBloque As String)
            If Not IsNothing(DatosRA) Then
                ' Deben estar las dos Propiedades Informadas, sino lanzamos un error
                If String.IsNullOrEmpty(DatosRA.TipoAcuerdo) AndAlso String.IsNullOrEmpty(DatosRA.CodigoAsunto) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' tiene informado el Bloque 'DatosRA', pero ninguna de las Propiedades está Informada.",
                                                      nombreBloque,
                                                      ObtenerDescripcionTipoFirma()))
                End If

                ' Si se han informado Datos de RA, es obligatorio tener Informadas las Variables para Recuperar los Datos del Acuerdo
                If String.IsNullOrEmpty(VariableFechaAcuerdo) OrElse String.IsNullOrEmpty(VariableAnioAcuerdo) OrElse String.IsNullOrEmpty(VariableNumeroAcuerdo) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' tiene informado el Bloque 'DatosRA', pero no tiene Informadas todas las Variables de Retorno para Recuperar los Datos del Acuerdo.",
                                                      nombreBloque,
                                                      ObtenerDescripcionTipoFirma()))
                End If
            End If
        End Sub

        Private Sub ValidarDatosBasicosFirmaPropia(nombreBloque As String)
            ' Comprobamos si tenemos informada la Solicitud de Firma de ARTEZ
            If String.IsNullOrEmpty(SolicitudFirmaARTEZ) Then
                ' No tenemos informada la Solicitud de Firma de ARTEZ, así que es obligatorio informar el Trámite de Firma Propia
                If IsNothing(DatosFirmaPropia) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaPropia' con los datos del Trámite de Firma Propia.",
                                                      nombreBloque,
                                                      ObtenerDescripcionTipoFirma()))

                ElseIf String.IsNullOrEmpty(DatosFirmaPropia.Idnt_TramiteFirmaPropia) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma Propia.", nombreBloque, ObtenerDescripcionTipoFirma()))
                End If

                ' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
                ValidarDatosBasicosResponsablesTramitesManuales(nombreBloque)
            End If
        End Sub

        Private Sub ValidarDatosBasicosFirmaFlujoPredefinido(nombrebloque As String)
            If IsNothing(DatosFirmaFlujoPredefinida) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaFlujoPredefinida' con los datos para la Firma Predefinida.",
                                                  nombrebloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoPredefinida.Idnt_TramiteFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoPredefinida.Idnt_TramiteRevisionRechazoFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Rechazo de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))
            End If

            ' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
            ValidarDatosBasicosResponsablesTramitesManuales(nombrebloque)
        End Sub

        Private Sub ValidarDatosBasicosFirmaFlujoNiveles(nombrebloque As String)
            If IsNothing(DatosFirmaFlujoNiveles) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaFlujoNiveles' con los datos para la Firma por Niveles.",
                                                  nombrebloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoNiveles.Idnt_TramiteFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoNiveles.Idnt_TramiteRevisionRechazoFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Rechazo de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))
            End If

            ' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
            ValidarDatosBasicosResponsablesTramitesManuales(nombrebloque)
        End Sub

        Private Sub ValidarDatosBasicosFirmaCSVOrganico(nombrebloque As String)
            If IsNothing(DatosFirmaCSVOrganico) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaCSVOrganico' con los datos para la Firma de CSV de Orgánico.",
                                                  nombrebloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaCSVOrganico.Idnt_TramiteFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaCSVOrganico.NivelOrganico) AndAlso String.IsNullOrEmpty(DatosFirmaCSVOrganico.Politica) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Nivel de Firma o la Política de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))
            End If
        End Sub

        Private Sub ValidarDatosBasicosFirmaFlujoAdHock(nombrebloque As String)
            If IsNothing(DatosFirmaFlujoAdHock) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaFlujoAdHock' con los datos para la Firma Ad-Hock.",
                                                  nombrebloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoAdHock.Idnt_TramiteFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaFlujoAdHock.Idnt_TramiteRevisionRechazoFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Rechazo de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            Else
                If String.IsNullOrEmpty(SolicitudFirmaARTEZ) AndAlso String.IsNullOrEmpty(DatosFirmaFlujoAdHock.Idnt_TramiteSeleccionFirmantes) Then
                    Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Selección de Firmantes.", nombrebloque, ObtenerDescripcionTipoFirma()))
                End If
            End If

            ' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
            ValidarDatosBasicosResponsablesTramitesManuales(nombrebloque)
        End Sub

        Private Sub ValidarDatosBasicosFirmaDeducidaEjecucion(nombrebloque As String)
            If IsNothing(DatosFirmaDeducidaEjecucion) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosFirmaDeducidaEjecucion' con los datos para el Tipo de Firma Deducido en Ejecución.",
                                                  nombrebloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaDeducidaEjecucion.TipoFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el campo Tipo de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf Not DatosFirmaDeducidaEjecucion.TipoFirma.StartsWith("@") AndAlso Not DatosFirmaDeducidaEjecucion.TipoFirma.StartsWith("=$IIF") Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado en el campo Tipo de Firma una Variable de la que Obtener el Tipo de Firma en Ejecución.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaDeducidaEjecucion.Idnt_TramiteFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosFirmaDeducidaEjecucion.Idnt_TramiteRevisionRechazoFirma) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Identificador del Trámite de Rechazo de Firma.", nombrebloque, ObtenerDescripcionTipoFirma()))
            End If

            '' Validamos si tenemos informado el grupo con los datos para la Asignación de Trámites Manuales
            'ValidarDatosBasicosResponsablesTramitesManuales(nombrebloque)
        End Sub

        Private Sub ValidarDatosBasicosResponsablesTramitesManuales(nombreBloque As String)
            If IsNothing(DatosReponsableTramitesManuales) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Bloque 'DatosReponsableTramitesManuales' con los datos de Asignación para los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosReponsableTramitesManuales.SistemaFuncionalXML) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Sistema Funcional para la Asignación de los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque,
                                                  ObtenerDescripcionTipoFirma()))

            ElseIf String.IsNullOrEmpty(DatosReponsableTramitesManuales.GrupoUsuariosXML) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Firma '{0}' de Tipo '{1}' no tiene informado el Grupo de Usuarios para la Asignación de los Trámites Manuales correspondientes al tipo de Bloque.",
                                                  nombreBloque,
                                                  ObtenerDescripcionTipoFirma()))
            End If
        End Sub

        Private Function ObtenerDescripcionTipoFirma() As String
            Dim descripcionTipoFirma As String = String.Empty

            Select Case TipoFirma
                Case TipoFirma.Propia
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_PROPIA

                Case TipoFirma.Propia_Y_Niveles
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_PROPIA_Y_NIVELES

                Case TipoFirma.Predefinida
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_PREDEFINIDA

                Case TipoFirma.Niveles
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_NIVELES

                Case TipoFirma.CSVOrganico
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_CSV_ORGANICO

                Case TipoFirma.AdHock
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_ADHOCK

                Case TipoFirma.Deducida_Ejecucion
                    descripcionTipoFirma = Constantes.TIPO_FIRMA_DEDUCIDA_EJECUCION
            End Select

            Return descripcionTipoFirma
        End Function

        Private Function ObtenerNombreVariableDocumentoFirmado() As String
            Return If(DocumentoAFirmar.StartsWith("@"), DocumentoAFirmar.Replace("@", String.Empty), DocumentoAFirmar)
        End Function

        Private Function ObtenerValorParametroDocumento() As String
            Return ObtenerValorParaParametro(DocumentoAFirmar)
        End Function

        Private Function ObtenerValorParametroTipoDocumento() As String
            Return ObtenerValorParaParametro(TipoDocumentoAFirmar)
        End Function

        Private Function ObtenerValorParametroSolicitudFirmaARTEZ() As String
            Return ObtenerValorParaParametro(SolicitudFirmaARTEZ)
        End Function

        Private Function ObtenerIdentificadorTramiteFirma() As String
            Dim identificadorTramiteFirma As String = String.Empty

            Select Case TipoFirma
                Case TipoFirma.Predefinida
                    identificadorTramiteFirma = DatosFirmaFlujoPredefinida.Idnt_TramiteFirma

                Case TipoFirma.Niveles, TipoFirma.Propia_Y_Niveles
                    identificadorTramiteFirma = DatosFirmaFlujoNiveles.Idnt_TramiteFirma

                Case TipoFirma.CSVOrganico
                    identificadorTramiteFirma = DatosFirmaCSVOrganico.Idnt_TramiteFirma

                Case TipoFirma.AdHock
                    identificadorTramiteFirma = DatosFirmaFlujoAdHock.Idnt_TramiteFirma

                Case Else
                    identificadorTramiteFirma = String.Empty
            End Select

            Return identificadorTramiteFirma
        End Function

        Private Function ObtenerIdentificadorTramiteRechazoFirma() As String
            Dim identificadorTramiteRechazoFirma As String = String.Empty

            Select Case TipoFirma
                Case TipoFirma.Predefinida
                    identificadorTramiteRechazoFirma = DatosFirmaFlujoPredefinida.Idnt_TramiteRevisionRechazoFirma

                Case TipoFirma.Niveles, TipoFirma.Propia_Y_Niveles
                    identificadorTramiteRechazoFirma = DatosFirmaFlujoNiveles.Idnt_TramiteRevisionRechazoFirma

                Case TipoFirma.AdHock
                    identificadorTramiteRechazoFirma = DatosFirmaFlujoAdHock.Idnt_TramiteRevisionRechazoFirma

                Case Else
                    identificadorTramiteRechazoFirma = String.Empty
            End Select

            Return identificadorTramiteRechazoFirma
        End Function

        Private Function ObtenerIdentificadorTramiteFirmaPropia() As String
            Dim identificadorTramiteFirmaPropia As String = String.Empty

            Select Case TipoFirma
                Case TipoFirma.Propia, TipoFirma.Propia_Y_Niveles
                    identificadorTramiteFirmaPropia = If(IsNothing(DatosFirmaPropia), String.Empty, DatosFirmaPropia.Idnt_TramiteFirmaPropia)

                Case Else
                    identificadorTramiteFirmaPropia = String.Empty
            End Select

            Return identificadorTramiteFirmaPropia
        End Function

        Private Function ObtenerIdentificadorTramiteSeleccionFirmantes() As String
            Dim identificadorTramiteSeleccionFirmantes As String = String.Empty

            Select Case TipoFirma
                Case TipoFirma.AdHock
                    identificadorTramiteSeleccionFirmantes = If(IsNothing(DatosFirmaFlujoAdHock), String.Empty, DatosFirmaFlujoAdHock.Idnt_TramiteSeleccionFirmantes)

                Case Else
                    identificadorTramiteSeleccionFirmantes = String.Empty
            End Select

            Return identificadorTramiteSeleccionFirmantes
        End Function

        Private Function ObtenerValorVariableFechaFirma() As String
            Return If(String.IsNullOrEmpty(VariableFechaFirma), String.Empty, VariableFechaFirma)
        End Function

        Private Function ObtenerValorVariableFechaAcuerdo() As String
            Return If(String.IsNullOrEmpty(VariableFechaAcuerdo), String.Empty, VariableFechaAcuerdo)
        End Function

        Private Function ObtenerValorVariableAnioAcuerdo() As String
            Return If(String.IsNullOrEmpty(VariableAnioAcuerdo), String.Empty, VariableAnioAcuerdo)
        End Function

        Private Function ObtenerValorVariableNumeroAcuerdo() As String
            Return If(String.IsNullOrEmpty(VariableNumeroAcuerdo), String.Empty, VariableNumeroAcuerdo)
        End Function

        Private Function ObtenerParametrosLlamada() As Dictionary(Of String, String)
            ' Variable para Devolver los Parámetros de llamada al Bloque de Tramitación Común de Firma
            Dim parametrosLlamada As Dictionary(Of String, String) = Nothing

            ' Variables Generales para todas las llamadas al Bloque de Tramitación Común de Firma
            Dim documento As String = ObtenerValorParametroDocumento()
            Dim tipoDocumento As String = ObtenerValorParametroTipoDocumento()
            Dim nombreVariableDocumentoFirmado As String = ObtenerNombreVariableDocumentoFirmado()

            ' Variables para obtener los Datos Responsable para los Trámites Manuales
            Dim sistemaFuncionalTramiteRevisionRechazoFirma As String = String.Empty
            Dim grupoUsuariosTramiteRevisionRechazoFirma As String = String.Empty
            Dim usuarioTramiteRevisionRechazoFirma As String = String.Empty

            ' Variables con los datos para la llamada a RA
            Dim tipoAcuerdoRA As String = String.Empty
            Dim codigoAsuntoRA As String = String.Empty

            ' Obtenemos el Modo de Creación para los Trámites Manuales
            Dim modoCreacion As String = ObtenerValorParaParametro(ModoCreacionTramitesManuales)

            ' Obtenemos las Observaciones para los Trámites Manuales
            Dim observaciones As String = ObtenerValorParaParametro(ObservacionesTramitesManuales)

            ' Obtenemos los Datos Responsables para los Trámites Manuales
            If Not IsNothing(DatosReponsableTramitesManuales) Then
                With DatosReponsableTramitesManuales
                    sistemaFuncionalTramiteRevisionRechazoFirma = .SistemaFuncional
                    grupoUsuariosTramiteRevisionRechazoFirma = .GrupoUsuarios
                    usuarioTramiteRevisionRechazoFirma = .Usuario
                End With
            End If

            ' Obtenemos los datos de RA
            If Not IsNothing(DatosRA) Then
                With DatosRA
                    tipoAcuerdoRA = ObtenerValorParaParametro(.TipoAcuerdo)
                    codigoAsuntoRA = ObtenerValorParaParametro(.CodigoAsunto)
                End With
            End If

            Select Case TipoFirma
                Case TipoFirma.Predefinida
                    With DatosFirmaFlujoPredefinida
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_PREDEFINIDA},
                            {"@TRAM_FIRMA_IN", .Idnt_TramiteFirma},
                            {"@TRAM_REV_RECHAZO_IN", .Idnt_TramiteRevisionRechazoFirma},
                            {"@TRAM_FIRMA_PROPIA_IN", String.Empty},
                            {"@TRAM_SEL_FIRMANTES_IN", String.Empty},
                            {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                            {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                            {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                            {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                            {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", String.Empty},
                            {"@IDNT_FLUJO_FK_IN", ObtenerValorParaParametro(.FlujoFK)},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", String.Empty},
                            {"@ORGANICO_FIRMA_IN", String.Empty},
                            {"@TIPO_SELLO_IN", String.Empty},
                            {"@NIVEL_ORGANICO_IN", String.Empty},
                            {"@POLITICA_IN", String.Empty},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With

                Case TipoFirma.CSVOrganico
                    With DatosFirmaCSVOrganico
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_CSV_ORGANICO},
                            {"@TRAM_FIRMA_IN", .Idnt_TramiteFirma},
                            {"@TRAM_REV_RECHAZO_IN", String.Empty},
                            {"@TRAM_FIRMA_PROPIA_IN", String.Empty},
                            {"@TRAM_SEL_FIRMANTES_IN", String.Empty},
                            {"@MODO_CREACION_TRAM_MA_IN", String.Empty},
                            {"@OBSERVACIONES_TRAM_MA_IN", String.Empty},
                            {"@SF_TRAM_MANUALES_IN", String.Empty},
                            {"@GU_TRAM_MANUALES_IN", String.Empty},
                            {"@US_TRAM_MANUALES_IN", String.Empty},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", ObtenerValorParametroSolicitudFirmaARTEZ()},
                            {"@IDNT_FLUJO_FK_IN", String.Empty},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", String.Empty},
                            {"@ORGANICO_FIRMA_IN", ObtenerValorParaParametro(.Organico)},
                            {"@TIPO_SELLO_IN", String.Empty},
                            {"@NIVEL_ORGANICO_IN", ObtenerValorParaParametro(.NivelOrganico)},
                            {"@POLITICA_IN", ObtenerValorParaParametro(.Politica)},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With

                Case TipoFirma.Niveles
                    With DatosFirmaFlujoNiveles
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_NIVELES},
                            {"@TRAM_FIRMA_IN", .Idnt_TramiteFirma},
                            {"@TRAM_REV_RECHAZO_IN", .Idnt_TramiteRevisionRechazoFirma},
                            {"@TRAM_FIRMA_PROPIA_IN", String.Empty},
                            {"@TRAM_SEL_FIRMANTES_IN", String.Empty},
                            {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                            {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                            {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                            {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                            {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", String.Empty},
                            {"@IDNT_FLUJO_FK_IN", String.Empty},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", ObtenerValorParaParametro(.FlujoFK)},
                            {"@ORGANICO_FIRMA_IN", ObtenerValorParaParametro(.Organico)},
                            {"@TIPO_SELLO_IN", ObtenerValorParaParametro(.TipoSello)},
                            {"@NIVEL_ORGANICO_IN", String.Empty},
                            {"@POLITICA_IN", String.Empty},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With

                Case TipoFirma.Propia
                    With DatosFirmaPropia
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_PROPIA},
                            {"@TRAM_FIRMA_IN", String.Empty},
                            {"@TRAM_REV_RECHAZO_IN", String.Empty},
                            {"@TRAM_FIRMA_PROPIA_IN", .Idnt_TramiteFirmaPropia},
                            {"@TRAM_SEL_FIRMANTES_IN", String.Empty},
                            {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                            {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                            {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                            {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                            {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", ObtenerValorParametroSolicitudFirmaARTEZ()},
                            {"@IDNT_FLUJO_FK_IN", String.Empty},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", String.Empty},
                            {"@ORGANICO_FIRMA_IN", String.Empty},
                            {"@TIPO_SELLO_IN", String.Empty},
                            {"@NIVEL_ORGANICO_IN", String.Empty},
                            {"@POLITICA_IN", String.Empty},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With

                Case TipoFirma.Propia_Y_Niveles
                    parametrosLlamada = New Dictionary(Of String, String) From {
                        {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                        {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_PROPIA_Y_NIVELES},
                        {"@TRAM_FIRMA_IN", DatosFirmaFlujoNiveles.Idnt_TramiteFirma},
                        {"@TRAM_REV_RECHAZO_IN", DatosFirmaFlujoNiveles.Idnt_TramiteRevisionRechazoFirma},
                        {"@TRAM_FIRMA_PROPIA_IN", DatosFirmaPropia.Idnt_TramiteFirmaPropia},
                        {"@TRAM_SEL_FIRMANTES_IN", String.Empty},
                        {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                        {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                        {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                        {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                        {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                        {"@DOCUMENTO_IN", documento},
                        {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                        {"@SOLICITUD_FIRMA_ARTEZ_IN", ObtenerValorParametroSolicitudFirmaARTEZ()},
                        {"@IDNT_FLUJO_FK_IN", String.Empty},
                        {"@IDNT_FLUJO_NIVELES_FK_IN", ObtenerValorParaParametro(DatosFirmaFlujoNiveles.FlujoFK)},
                        {"@ORGANICO_FIRMA_IN", ObtenerValorParaParametro(DatosFirmaFlujoNiveles.Organico)},
                        {"@TIPO_SELLO_IN", ObtenerValorParaParametro(DatosFirmaFlujoNiveles.TipoSello)},
                        {"@NIVEL_ORGANICO_IN", String.Empty},
                        {"@POLITICA_IN", String.Empty},
                        {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                        {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                        {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                        {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                        {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                        {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                        {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                        {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                    }

                Case TipoFirma.AdHock
                    With DatosFirmaFlujoAdHock
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", Constantes.TIPO_FIRMA_ADHOCK},
                            {"@TRAM_FIRMA_IN", .Idnt_TramiteFirma},
                            {"@TRAM_REV_RECHAZO_IN", .Idnt_TramiteRevisionRechazoFirma},
                            {"@TRAM_FIRMA_PROPIA_IN", String.Empty},
                            {"@TRAM_SEL_FIRMANTES_IN", .Idnt_TramiteSeleccionFirmantes},
                            {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                            {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                            {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                            {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                            {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", ObtenerValorParametroSolicitudFirmaARTEZ()},
                            {"@IDNT_FLUJO_FK_IN", String.Empty},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", String.Empty},
                            {"@ORGANICO_FIRMA_IN", String.Empty},
                            {"@TIPO_SELLO_IN", String.Empty},
                            {"@NIVEL_ORGANICO_IN", String.Empty},
                            {"@POLITICA_IN", String.Empty},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With

                Case TipoFirma.Deducida_Ejecucion
                    With DatosFirmaDeducidaEjecucion
                        parametrosLlamada = New Dictionary(Of String, String) From {
                            {"@DOC_FIR_FUERA_ARTEZ_IN", ObtenerValorParaParametro(DocumentoFirmadoFueraARTEZ)},
                            {"@TIPO_FIRMA_IN", ObtenerValorParaParametro(.TipoFirma)},
                            {"@TRAM_FIRMA_IN", .Idnt_TramiteFirma},
                            {"@TRAM_REV_RECHAZO_IN", .Idnt_TramiteRevisionRechazoFirma},
                            {"@TRAM_FIRMA_PROPIA_IN", .Idnt_TramiteFirmaPropia},
                            {"@TRAM_SEL_FIRMANTES_IN", .Idnt_TramiteSeleccionFirmantes},
                            {"@MODO_CREACION_TRAM_MA_IN", modoCreacion},
                            {"@OBSERVACIONES_TRAM_MA_IN", observaciones},
                            {"@SF_TRAM_MANUALES_IN", sistemaFuncionalTramiteRevisionRechazoFirma},
                            {"@GU_TRAM_MANUALES_IN", grupoUsuariosTramiteRevisionRechazoFirma},
                            {"@US_TRAM_MANUALES_IN", usuarioTramiteRevisionRechazoFirma},
                            {"@DOCUMENTO_IN", documento},
                            {"@TIPO_DOCUMENTO_IN", tipoDocumento},
                            {"@SOLICITUD_FIRMA_ARTEZ_IN", ObtenerValorParametroSolicitudFirmaARTEZ()},
                            {"@IDNT_FLUJO_FK_IN", ObtenerValorParaParametro(.FlujoFK_Predefinido)},
                            {"@IDNT_FLUJO_NIVELES_FK_IN", ObtenerValorParaParametro(.FlujoFK_Niveles)},
                            {"@ORGANICO_FIRMA_IN", ObtenerValorParaParametro(.Organico)},
                            {"@TIPO_SELLO_IN", ObtenerValorParaParametro(.TipoSello)},
                            {"@NIVEL_ORGANICO_IN", ObtenerValorParaParametro(.NivelOrganico)},
                            {"@POLITICA_IN", ObtenerValorParaParametro(.Politica)},
                            {"@TIPO_ACUERDO_RA_IN", tipoAcuerdoRA},
                            {"@CODIGO_ASUNTO_RA_IN", codigoAsuntoRA},
                            {"@RESPUESTA_FK_OUT", VariableRespuestaFK},
                            {"@FECHA_FIRMA_OUT", ObtenerValorVariableFechaFirma()},
                            {"@DOCUMENTO_FIRMADO_OUT", nombreVariableDocumentoFirmado},
                            {"@FECHA_ACUERDO_OUT", ObtenerValorVariableFechaAcuerdo()},
                            {"@ANIO_ACUERDO_OUT", ObtenerValorVariableAnioAcuerdo()},
                            {"@NUMERO_ACUERDO_OUT", ObtenerValorVariableNumeroAcuerdo()}
                        }
                    End With
            End Select

            ' Devolvemos el listado de Parámetros para la Llamada del Bloque de Tramitación Común de Firma
            Return parametrosLlamada
        End Function
#End Region
    End Class
End Namespace