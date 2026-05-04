Namespace Bizkaia.Pasarela
    Friend NotInheritable Class Constantes
        Friend Const APLICACION As String = "N8"

        Friend Const USUARIO_PASARELA As String = "PASARELA"

        Friend Const CADENAS_CONEXION_INFRAESTRUCTURA As String = "INFRAESTRUCTURA"
        Friend Const CADENAS_CONEXION_DESTINO As String = "DESTINO"

        Friend Const S As String = "S"
        Friend Const N As String = "N"
        Friend Const VALOR_TRUE As String = "TRUE"
        Friend Const VALOR_FALSE As String = "FALSE"

        Friend Const LITERAL_TRUE As String = "true"
        Friend Const LITERAL_FALSE As String = "false"

        Friend Const NUMERO_0 As String = "0"
        Friend Const NUMERO_1 As String = "1"

        Friend Const ALL As String = "ALL"

        ' Tamaño campos tablas HIDRA
        Friend Const WFFLOWACTIONS_PATH_NUMERO_CARACTERES As Integer = 25

        ' Tipo de Modo de Creación de la Tareas Manuales
        Friend Const MODO_CREACION_TAREA_MANUAL_GRUPO As String = "00"
        Friend Const MODO_CREACION_TAREA_MANUAL_NOMINATIVA As String = "01"

        ' Tipo de Trámite para la Acción de Gestión FINALIZAR_TAREA
        Friend Const TIPO_TAREA_MANUAL As String = "M"
        Friend Const TIPO_TAREA_AUTOMATICA As String = "A"

        ' Variables Generales para la creación de las Tareas
        Friend Const VARIABLE_GENERAL_SISTEMA_FUNCIONAL As String = "SISTEMA_FUNCIONAL"
        Friend Const VARIABLE_GENERAL_GRUPO_USUARIOS As String = "GRUPO_USUARIOS"
        Friend Const VARIABLE_GENERAL_USUARIO As String = "USUARIO"

        ' Tipos de Agrupación para las LABEL
        Friend Const PARAMETRO_TIPO_AGRUPACION As String = "TIPO_AGRUPACION"
        Friend Const TIPO_AGRUPACION_BLOQUE_TRAMITACION_COMUN As String = "BLOQUE_TRAMITACION_COMUN"

        ' Bloques de Tramitación Común
        Friend Const BASE_DATOS_BLOQUES_TRAMITACION_COMUN As String = "DBN8DOMINIOGENERAL"
        Friend Const BLOQUE_TRAMITACION_COMUN_NOTIFICACION As String = "BLOQUE_TRAM_NOTIFICACION"
        Friend Const BLOQUE_TRAMITACION_COMUN_FIRMA As String = "BLOQUE_TRAM_FIRMA"
        Friend Const BLOQUE_TRAMITACION_COMUN_INTEGRACION_BKON As String = "BLOQUE_TRAM_BKON"
        Friend Const BLOQUE_TRAMITACION_COMUN_PUESTA_MANIFIESTO As String = "BLOQUE_TRAM_PUESTA_MANIFIESTO"

        ' Acciones de Gestión
        Friend Const ACCION_GESTION_OBTENER_PLAZO As String = "OBTENER_PLAZO"
        Friend Const ACCION_GESTION_CADUCAR_TAREA As String = "CADUCAR_TAREA"
        Friend Const ACCION_GESTION_CREAR_TAREA_AUTOMATICA As String = "CREAR_TAREA_AUTOMATICA"
        Friend Const ACCION_GESTION_CREAR_TAREA_MANUAL As String = "CREAR_TAREA_MANUAL"
        Friend Const ACCION_GESTION_EJECUTAR_TAREA_AUTOMATICA As String = "EJECUTAR_TAREA_AUTOMATICA"
        Friend Const ACCION_GESTION_FINALIZAR_TAREA As String = "FINALIZAR_TAREA"
        Friend Const ACCION_GESTION_FIN_TRAMITACION As String = "FIN_TRAMITACION"
        Friend Const ACCION_GESTION_CREAR_TAREA_RECEPCION_ACUSE_RECIBO As String = "CREAR_TAREA_RECEP_ACUSE"
        Friend Const ACCION_GESTION_CREAR_TAREA_COMPROBACION_REQUISITOS As String = "CREAR_TAREA_COMPROBACION_REQUISITOS"

        ' Tipo de Trámites
        Friend Const TRAMITE_AUTOMATICO As String = "AUTOMATICO"
        Friend Const TRAMITE_MANUAL As String = "MANUAL"
        Friend Const TRAMITE_RECEPCION_ACUSE_RECIBO As String = "RECEPCION_ACUSE_RECIBO"
        Friend Const TRAMITE_COMPROBACION_REQUISITOS As String = "COMPROBACION_REQUISITOS"
        Friend Const TRAMITE_FIRMA_SELLO As String = "FIRMA_SELLO"
        Friend Const TRAMITE_PLANIFICADO As String = "PLANIFICADO"
        Friend Const TRAMITE_PROCESO_PLANIFICADO As String = "PROCESO_PLANIFICADO"
        Friend Const TRAMITE_NOTIFICACION As String = "NOTIFICACION"
        Friend Const TRAMITE_FIRMA As String = "FIRMA"
        Friend Const TRAMITE_BKON As String = "INTEGRACIÓN_BKON"
        Friend Const TRAMITE_PUESTA_MANIFIESTO As String = "PUESTA_MANIFIESTO"
        Friend Const TRAMITE_PORTA_FIRMAS As String = "PORTA_FIRMAS"
        Friend Const TRAMITE_RECHAZO_FIRMAS As String = "RECHAZO_FIRMAS"
        Friend Const TRAMITE_ORDEN_NOTIFICACION As String = "ORDEN_NOTIFICACION"
        Friend Const TRAMITE_PROCESAR_NOTIFICACION As String = "PROCESAR_NOTIFICACION"
        Friend Const TRAMITE_ORDEN_NOTIFICACION_MANUAL As String = "ORDEN_NOTIFICACION_MANUAL"
        Friend Const TRAMITE_PORTA_FIRMAS_CONTROL_M As String = "PORTA_FIRMAS_CONTROL-M"
        Friend Const TRAMITE_AUTOMATICO_CONTROL_M As String = "AUTOMATICO_CONTROL-M"

        ' Tipo de Firma
        Friend Const TIPO_FIRMA_PROPIA As String = "PROPIA"
        Friend Const TIPO_FIRMA_PROPIA_Y_NIVELES As String = "PROPIA+NIVELES"
        Friend Const TIPO_FIRMA_PREDEFINIDA As String = "PREDEFINIDA"
        Friend Const TIPO_FIRMA_NIVELES As String = "NIVELES"
        Friend Const TIPO_FIRMA_CSV_ORGANICO As String = "CSVORGANICO"
        Friend Const TIPO_FIRMA_ADHOCK As String = "ADHOCK"
        Friend Const TIPO_FIRMA_DEDUCIDA_EJECUCION As String = "DEDUCIDA EN EJECUCION"

        ' Condiciones para la Acción de Motor IF
        Friend Const IGUAL As String = "="
        Friend Const DIFERENTE As String = "<>"
        Friend Const MAYOR As String = ">"
        Friend Const MENOR As String = "<"
        Friend Const MAYOR_IGUAL As String = ">="
        Friend Const MENOR_IGUAL As String = "<="

        ' Acciones Motor
        Friend Const ACCION_BEGIN As String = "BEGIN"
        Friend Const ACCION_END As String = "END"
        Friend Const ACCION_LABEL As String = "LABEL"
        Friend Const ACCION_JUMP As String = "JUMP"
        Friend Const ACCION_UNION As String = "UNION"
        Friend Const ACCION_IF As String = "IF"
        Friend Const ACCION_ALERT As String = "ALERT"
        Friend Const ACCION_CANCEL As String = "CANCEL"
        Friend Const ACCION_FORM As String = "FORM"
        Friend Const ACCION_APIINIENDEXT As String = "APIINIENDEXT"
        Friend Const ACCION_LETVAR As String = "LETVAR"
        Friend Const ACCION_ENDPROC As String = "ENDPROC"

        ' Parámetros Acciones Motor
        Friend Const PARAMETRO_ACCION_STATE As String = "STATE"
        Friend Const PARAMETRO_ACCION_SUBMIT As String = "SUBMIT"
        Friend Const PARAMETRO_ACCION_LEVEL As String = "LEVEL"
        Friend Const PARAMETRO_ACCION_PROJECT As String = "PROJECT"
        Friend Const PARAMETRO_ACCION_FLOW As String = "FLOW"
        Friend Const PARAMETRO_ACCION_REFERENCE As String = "REFERENCE"
        Friend Const PARAMETRO_ACCION_INIT As String = "INIT"
        Friend Const PARAMETRO_ACCION_DEFERRED As String = "DEFERRED"
        Friend Const PARAMETRO_ACCION_FLUSHALL As String = "FLUSHALL"
        Friend Const PARAMETRO_ACCION_ENABLE As String = "ENABLE"
        Friend Const PARAMETRO_ACCION_PATH As String = "PATH"
        Friend Const PARAMETRO_ACCION_ELSEPATH As String = "ELSEPATH"
        Friend Const PARAMETRO_ACCION_VAR1 As String = "VAR1"
        Friend Const PARAMETRO_ACCION_VAR2 As String = "VAR2"
        Friend Const PARAMETRO_ACCION_CONDITION As String = "CONDITION"
        Friend Const PARAMETRO_ACCION_TYPE As String = "TYPE"
        Friend Const PARAMETRO_ACCION_DATE As String = "DATE"
        Friend Const PARAMETRO_ACCION_MATURATION As String = "MATURATION"
        Friend Const PARAMETRO_ACCION_INTERVAL As String = "INTERVAL"
        Friend Const PARAMETRO_ACCION_ALERTPATH As String = "ALERTPATH"
        Friend Const PARAMETRO_ACCION_DAYSOFWEEK As String = "DAYSOFWEEK"
        Friend Const PARAMETRO_ACCION_EXACTTIME As String = "EXACTTIME"
        Friend Const PARAMETRO_ACCION_FORM As String = "FORM"
        Friend Const PARAMETRO_ACCION_FILTER As String = "FILTER"
        Friend Const PARAMETRO_ACCION_GROUP As String = "GROUP"
        Friend Const PARAMETRO_ACCION_USER As String = "USER"
        Friend Const PARAMETRO_ACCION_WAIT As String = "WAIT"
        Friend Const PARAMETRO_ACCION_TO As String = "TO"
        Friend Const PARAMETRO_ACCION_ICON As String = "ICON"
        Friend Const PARAMETRO_ACCION_ALL As String = "ALL"
        Friend Const PARAMETRO_ACCION_COMENTARIO As String = "COMENTARIO"

        ' -------------------------------- BLOQUE TRAMITACIÓN COMÚN -----------------------------------
        Friend Const APIINIENDEXT_NUMERO_PARAMETROS_DEFECTO_BLOQUE_TRAMITACION_COMUN As Integer = 38

        ' -------------------------------- APIINIENDEXT -----------------------------------------------
        Friend Const APIINIENDEXT_NUMERO_PARAMETROS_DEFECTO_APIINIENDEXT As Integer = 42
        ' Parámetros por Defecto
        Friend Const APIINIENDEXT_PARAMETRO_CODIGO_APLICACION As String = "@CODIGO_APLICACION"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_DOMINIO As String = "@IDNT_DOMINIO"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_PROCEDIMIENTO As String = "@IDNT_PROCEDIMIENTO"
        Friend Const APIINIENDEXT_PARAMETRO_DESCRIPTOR_PROCEDIMIENTO As String = "@DESCRIPTOR_PROCEDIMIENTO"
        Friend Const APIINIENDEXT_PARAMETRO_FEC_INI_SISTEMA As String = "@FEC_INI_SISTEMA"
        Friend Const APIINIENDEXT_PARAMETRO_FEC_INI_VIGENCIA As String = "@FEC_INI_VIGENCIA"
        Friend Const APIINIENDEXT_PARAMETRO_INSTANCIA_N8 As String = "@INSTANCIA_N8"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_INSTANCIA_PROCEDIMIENTO As String = "@IDNTINSPROC"
        Friend Const APIINIENDEXT_PARAMETRO_TIPO_ALTA As String = "@TIPOALTA"
        Friend Const APIINIENDEXT_PARAMETRO_CENTRO_FORAL As String = "@CENTROFORAL"
        Friend Const APIINIENDEXT_PARAMETRO_ORGANICO_TRAMITADOR As String = "@ORGANICOTRAMITADOR"
        Friend Const APIINIENDEXT_PARAMETRO_PROCEDIMIENTO_JX As String = "@PROCEDIMIENTOJX"
        Friend Const APIINIENDEXT_PARAMETRO_FORMULARIO_JX As String = "@FORMULARIOJX"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_INTERESADO As String = "@IDINTERESADO"
        Friend Const APIINIENDEXT_PARAMETRO_NIF_INTERESADO As String = "@NIFINTERESADO"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_REPRESENTANTE As String = "@IDREPRESENTANTE"
        Friend Const APIINIENDEXT_PARAMETRO_NIF_REPRESENTANTE As String = "@NIFREPRESENTANTE"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_TRAMITACION_CARPETA As String = "@IDTRAMITACIONEW"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE_CARPETA As String = "@IDEXPEDIENTEEW"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE As String = "@IDEXPEDIENTE"
        Friend Const APIINIENDEXT_PARAMETRO_NUMERO_EXPEDIENTE As String = "@NUMEROEXPEDIENTE"
        Friend Const APIINIENDEXT_PARAMETRO_DESCRIPTOR_TIPO_EXPEDIENTE As String = "@DESCRIP_TIPO_EXPTE"
        Friend Const APIINIENDEXT_PARAMETRO_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES As String = "@COD_EXPTE_SSAA"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_SUBEXPEDIENTE As String = "@IDNTSUBEXPEDIENTE"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_ENTRADA As String = "@IDENTRADA"
        Friend Const APIINIENDEXT_PARAMETRO_NUMERO_REGISTRO_ENTRADA As String = "@REGENTRADA"
        Friend Const APIINIENDEXT_PARAMETRO_FECHA_REGISTRO_ENTRADA As String = "@FECHAENTRADA"
        Friend Const APIINIENDEXT_PARAMETRO_NUMERO_EJECUCION As String = "@NUMERO_EJECUCION"
        Friend Const APIINIENDEXT_PARAMETRO_IDNT_ELEMENTO As String = "@IDNT_ELEMENTO"
        Friend Const APIINIENDEXT_PARAMETRO_SISTEMA_FUNCIONAL As String = "@SISTEMA_FUNCIONAL"
        Friend Const APIINIENDEXT_PARAMETRO_GRUPO_USUARIOS As String = "@GRUPO_USUARIOS"
        Friend Const APIINIENDEXT_PARAMETRO_USUARIO As String = "@USUARIO"
        Friend Const APIINIENDEXT_PARAMETRO_BBDD_RETORNO As String = "@BBDD_RETORNO"
        Friend Const APIINIENDEXT_PARAMETRO_FLUJO_RETORNO As String = "@FLUJO_RETORNO"
        Friend Const APIINIENDEXT_PARAMETRO_PATH_RETORNO As String = "@PATH_RETORNO"
        Friend Const APIINIENDEXT_PARAMETRO_REFERENCIA_RETORNO As String = "@REFERENCIA_RETORNO"
        ' Valores por Defecto
        Friend Const APIINIENDEXT_VALOR_IDNT_DOMINIO As String = "@INICIO!IDNT_DOMINIO"
        Friend Const APIINIENDEXT_VALOR_IDNT_PROCEDIMIENTO As String = "@INICIO!IDNT_PROCEDIMIENTO"
        Friend Const APIINIENDEXT_VALOR_DESCRIPTOR_PROCEDIMIENTO As String = "@INICIO!DESCRIPTOR_PROCEDIMIENTO"
        Friend Const APIINIENDEXT_VALOR_FEC_INI_SISTEMA As String = "@INICIO!FEC_INI_SISTEMA"
        Friend Const APIINIENDEXT_VALOR_FEC_INI_VIGENCIA As String = "@INICIO!FEC_INI_VIGENCIA"
        Friend Const APIINIENDEXT_VALOR_INSTANCIA_N8 As String = "@INICIO!INSTANCIA_N8"
        Friend Const APIINIENDEXT_VALOR_IDNT_INSTANCIA_PROCEDIMIENTO As String = "@INICIO!IDNTINSPROC"
        Friend Const APIINIENDEXT_VALOR_TIPO_ALTA As String = "@INICIO!TIPOALTA"
        Friend Const APIINIENDEXT_VALOR_CENTRO_FORAL As String = "@INICIO!CENTROFORAL"
        Friend Const APIINIENDEXT_VALOR_ORGANICO_TRAMITADOR As String = "@INICIO!ORGANICOTRAMITADOR"
        Friend Const APIINIENDEXT_VALOR_PROCEDIMIENTO_JX As String = "@INICIO!PROCEDIMIENTOJX"
        Friend Const APIINIENDEXT_VALOR_FORMULARIO_JX As String = "@INICIO!FORMULARIOJX"
        Friend Const APIINIENDEXT_VALOR_IDNT_INTERESADO As String = "@INICIO!IDINTERESADO"
        Friend Const APIINIENDEXT_VALOR_NIF_INTERESADO As String = "@INICIO!NIFINTERESADO"
        Friend Const APIINIENDEXT_VALOR_IDNT_REPRESENTANTE As String = "@INICIO!IDREPRESENTANTE"
        Friend Const APIINIENDEXT_VALOR_NIF_REPRESENTANTE As String = "@INICIO!NIFREPRESENTANTE"
        Friend Const APIINIENDEXT_VALOR_IDNT_TRAMITACION_CARPETA As String = "@INICIO!IDTRAMITACIONEW"
        Friend Const APIINIENDEXT_VALOR_IDNT_EXPEDIENTE_CARPETA As String = "@INICIO!IDEXPEDIENTEEW"
        Friend Const APIINIENDEXT_VALOR_IDNT_EXPEDIENTE As String = "@INICIO!IDEXPEDIENTE"
        Friend Const APIINIENDEXT_VALOR_NUMERO_EXPEDIENTE As String = "@INICIO!NUMEROEXPEDIENTE"
        Friend Const APIINIENDEXT_VALOR_DESCRIPTOR_TIPO_EXPEDIENTE As String = "@INICIO!DESCRIP_TIPO_EXPTE"
        Friend Const APIINIENDEXT_VALOR_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES As String = "@INICIO!COD_EXPTE_SSAA"
        Friend Const APIINIENDEXT_VALOR_IDNT_SUBEXPEDIENTE As String = "@INICIO!IDNTSUBEXPEDIENTE"
        Friend Const APIINIENDEXT_VALOR_IDNT_ENTRADA As String = "@INICIO!IDENTRADA"
        Friend Const APIINIENDEXT_VALOR_NUMERO_REGISTRO_ENTRADA As String = "@INICIO!REGENTRADA"
        Friend Const APIINIENDEXT_VALOR_FECHA_REGISTRO_ENTRADA As String = "@INICIO!FECHAENTRADA"
        Friend Const APIINIENDEXT_VALOR_NUMERO_EJECUCION As String = "@INICIO!NUMERO_EJECUCION"
        Friend Const APIINIENDEXT_VALOR_REFERENCIA_RETORNO As String = "%REFERENCE%"

        ' -------------------------------- FORM -----------------------------------------------
        Friend Const FORM_NUMERO_PARAMETROS_DEFECTO As Integer = 8
        ' Parámetros por defecto
        Friend Const FORM_PARAMETRO_IDNT_ELEMENTO As String = "@IDNT_ELEMENTO"
        Friend Const FORM_PARAMETRO_TIPOEJECUCION As String = "@TIPOEJECUCION"
        ' Valores por Defecto
        Friend Const FORM_VALOR_FORM As String = "FORMULARIO TAREA"

        ' -------------------------------- LETVAR -----------------------------------------------
        Friend Const LETVAR_VARIABLES_INICIO As String = "INICIO"

        ' Parámetros para ejecutar las Sentencias SQL
        Friend Const PARAMETRO_FLOW As String = "@Flow"
        Friend Const PARAMETRO_VERSION As String = "@Version"
        Friend Const PARAMETRO_FLOW_NAME As String = "@FlowName"
        Friend Const PARAMETRO_DESCRIPCION_FLUJO As String = "@DescripcionFlujo"
        Friend Const PARAMETRO_FLOWORDER As String = "@FlowOrder"
        Friend Const PARAMETRO_ID As String = "@ID"
        Friend Const PARAMETRO_ACTION As String = "@Action"
        Friend Const PARAMETRO_PATH As String = "@Path"
        Friend Const PARAMETRO_PARAM As String = "@Param"
        Friend Const PARAMETRO_VALUE As String = "@Value"
        Friend Const PARAMETRO_COMENTARIO As String = "@Comentario"

        ' Constantes para la creación de las Sentencias
        Friend Const DELETE_CABECERA_FLUJO As String = "DELETE FROM [dbo].[wfFlows] WHERE [Flow] = @Flow And [Version] = @Version"
        Friend Const DELETE_DETALLE_FLUJO As String = "DELETE FROM [dbo].[wfFlowActions] WHERE [Flow] = @Flow And [Version] = @Version"
        Friend Const INSERT_CABECERA_FLUJO As String = "INSERT INTO [dbo].[wfFlows] ([FLow], [Version], [Active], [FlowName], [Comments], [Running], [Start], [StopOlderVersions]) VALUES (@Flow, @Version, 1, @FlowName, @DescripcionFlujo, 1, '', 0)"
        Friend Const INSERT_DETALLE_FLUJO As String = "INSERT INTO [dbo].[wfFlowActions] ([Flow], [Version], [FlowOrder], [Id], [Action], [Path], [Param], [Value], [Comments]) VALUES (@Flow, @Version, @FlowOrder, @ID, @Action, @Path, @Param, @Value, @Comentario)"
    End Class
End Namespace