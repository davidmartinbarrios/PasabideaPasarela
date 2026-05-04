Namespace Bizkaia.Pasarela.Utilidades
    Friend NotInheritable Class UtilidadesCreacionLineasDetalleFlujo
#Region "Métodos Friend"
        Friend Shared Function ObtenerLineasDetalleInicioFlujo() As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=0, id:=0, action:=Constantes.ACCION_BEGIN, path:="INICIO", param:="STATE", value:="INICIO", comentario:="Definición Pasarela")
            }
        End Function

        Friend Shared Function ObtenerLineasDetalleFinFlujo(identificadorFlujo As String) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=999999999, id:=0, action:=Constantes.ACCION_END, path:=String.Format("FIN {0}", identificadorFlujo), param:=Constantes.PARAMETRO_ACCION_FLUSHALL, value:=Constantes.NUMERO_0, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=999999999, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:="1", comentario:=String.Empty)
            }
        End Function

        Friend Shared Function ObtenerLineasDetalleFinalizacionTramitacion(identificadorFlujo As String, baseDatosAccionesInfraestructura As String, baseDatosDestinoFlujo As String) As List(Of LineaDetalle)
            Dim lineasDetalleFinalizacionTramitacion As New List(Of LineaDetalle)
            lineasDetalleFinalizacionTramitacion.AddRange(ObtenerLineasDetalle_Accion_LABEL(flowOrder:=999999900, path:="FIN_TRAMITACION", tieneParametro_STATE:=True, valorParametro_STATE:="FIN_TRAMITACION", valorParametro_SUBMIT:="0", tipoAgrupacion:=String.Empty, level:=1,
                                                                                            comentario:=String.Empty))

            lineasDetalleFinalizacionTramitacion.AddRange(ObtenerLineasDetalle_Accion_APIINIENDEXT(identificadorFlujo:=identificadorFlujo, flowOrder:=999999950, path:="FINALIZAR_TRAMITACION", baseDatosAccion:=baseDatosAccionesInfraestructura, accion:="FINALIZAR_TRAMITACION",
                                                                                                   aplicacion:=Constantes.APLICACION, contadorReferencia:=9999, level:=2, diferido:=False, idnt_Elemento:=String.Empty, sistemaFuncional:=String.Empty,
                                                                                                   grupoUsuarios:=String.Empty, usuario:=String.Empty, baseDatosRetorno:=baseDatosDestinoFlujo, pathRetorno:=String.Format("FIN {0}", identificadorFlujo), comentario:=String.Empty,
                                                                                                   parametrosEspecificos:=Nothing, habilitado:=True))

            Return lineasDetalleFinalizacionTramitacion
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_LABEL(flowOrder As Integer, path As String, tieneParametro_STATE As Boolean, valorParametro_STATE As String, valorParametro_SUBMIT As String, tipoAgrupacion As String, level As Integer, comentario As String) As List(Of LineaDetalle)
            ' Creamos las líneas de Detalle básicas de la acción LABEL
            Dim lineasDetalleAccionLabel As New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_LABEL, path:=path, param:=If(tieneParametro_STATE, Constantes.PARAMETRO_ACCION_STATE, String.Empty), value:=If(tieneParametro_STATE, valorParametro_STATE, String.Empty), comentario:=If(tieneParametro_STATE, comentario, String.Empty)),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_SUBMIT, value:=valorParametro_SUBMIT, comentario:=If(Not tieneParametro_STATE, comentario, String.Empty))
            }

            ' Comprobamos si tenemos que crear el parámetro Tipo Agrupación
            If Not String.IsNullOrEmpty(tipoAgrupacion) Then
                lineasDetalleAccionLabel.Add(New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_TIPO_AGRUPACION, value:=tipoAgrupacion, comentario:=String.Empty))
            End If

            ' Incluimos el parámetro LEVEL
            lineasDetalleAccionLabel.Add(New LineaDetalle(flowOrder:=flowOrder, id:=If(Not String.IsNullOrEmpty(tipoAgrupacion), 3, 2), action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty))

            Return lineasDetalleAccionLabel
        End Function

        Friend Shared Function ObtenerLineasDetalle_Llamada_BloqueTramitacionComun(identificadorFlujo As String, flowOrder As Integer, path As String, baseDatosBloqueTramitacionComun As String, bloqueTramitacionComun As String, aplicacion As String,
                                                                                   contadorReferencia As Integer, level As Integer, diferido As Boolean, baseDatosRetorno As String, pathRetorno As String, comentario As String,
                                                                                   parametrosEspecificos As Dictionary(Of String, String), habilitado As Boolean) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_APIINIENDEXT, path:=path, param:=Constantes.PARAMETRO_ACCION_PROJECT, value:=baseDatosBloqueTramitacionComun, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_FLOW, value:=bloqueTramitacionComun, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_REFERENCE, value:=String.Format("=$CONCAT([%REFERENCE%];[_{0}_{1:0000}])/$", aplicacion, contadorReferencia), comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=3, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_INIT, value:=Constantes.NUMERO_1, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=4, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=5, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_DEFERRED, value:=If(diferido, Constantes.NUMERO_1, Constantes.NUMERO_0), comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=6, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CODIGO_APLICACION, value:=aplicacion, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=7, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_DOMINIO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_DOMINIO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=8, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=9, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_DESCRIPTOR_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_DESCRIPTOR_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=10, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FEC_INI_SISTEMA, value:=Constantes.APIINIENDEXT_VALOR_FEC_INI_SISTEMA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=11, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FEC_INI_VIGENCIA, value:=Constantes.APIINIENDEXT_VALOR_FEC_INI_VIGENCIA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=12, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_INSTANCIA_N8, value:=Constantes.APIINIENDEXT_VALOR_INSTANCIA_N8, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=13, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_INSTANCIA_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_INSTANCIA_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=14, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_TIPO_ALTA, value:=Constantes.APIINIENDEXT_VALOR_TIPO_ALTA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=15, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CENTRO_FORAL, value:=Constantes.APIINIENDEXT_VALOR_CENTRO_FORAL, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=16, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_ORGANICO_TRAMITADOR, value:=Constantes.APIINIENDEXT_VALOR_ORGANICO_TRAMITADOR, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=17, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_PROCEDIMIENTO_JX, value:=Constantes.APIINIENDEXT_VALOR_PROCEDIMIENTO_JX, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=18, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FORMULARIO_JX, value:=Constantes.APIINIENDEXT_VALOR_FORMULARIO_JX, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=19, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_INTERESADO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_INTERESADO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=20, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NIF_INTERESADO, value:=Constantes.APIINIENDEXT_VALOR_NIF_INTERESADO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=21, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_REPRESENTANTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_REPRESENTANTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=22, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NIF_REPRESENTANTE, value:=Constantes.APIINIENDEXT_VALOR_NIF_REPRESENTANTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=23, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_TRAMITACION_CARPETA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_TRAMITACION_CARPETA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=24, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE_CARPETA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_EXPEDIENTE_CARPETA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=25, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=26, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=27, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_DESCRIPTOR_TIPO_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_DESCRIPTOR_TIPO_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=28, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES, value:=Constantes.APIINIENDEXT_VALOR_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=29, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_SUBEXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_SUBEXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=30, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=31, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_REGISTRO_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_REGISTRO_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=32, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FECHA_REGISTRO_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_FECHA_REGISTRO_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=33, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_EJECUCION, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_EJECUCION, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=34, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_BBDD_RETORNO, value:=baseDatosRetorno, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=35, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FLUJO_RETORNO, value:=identificadorFlujo, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=36, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_PATH_RETORNO, value:=pathRetorno, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=37, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_REFERENCIA_RETORNO, value:=Constantes.APIINIENDEXT_VALOR_REFERENCIA_RETORNO, comentario:=String.Empty)
            }

            ' Comprobamos si tenemos parámetros específicos o la llamada Deshabilitada
            If Not IsNothing(parametrosEspecificos) OrElse Not habilitado Then
                Dim contadorParametro As Integer = Constantes.APIINIENDEXT_NUMERO_PARAMETROS_DEFECTO_BLOQUE_TRAMITACION_COMUN

                ' Comprobamos si tenemos parámetros específicos
                If Not IsNothing(parametrosEspecificos) Then
                    For Each parametroEspecifico As KeyValuePair(Of String, String) In parametrosEspecificos
                        lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=parametroEspecifico.Key, value:=parametroEspecifico.Value, comentario:=String.Empty))
                        contadorParametro += 1
                    Next
                End If

                ' Comprobamos si la llamada está Deshabilitada
                If Not habilitado Then
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ENABLE, value:=Constantes.NUMERO_0, comentario:=String.Empty))
                End If
            End If

            Return lineasDetalle
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_APIINIENDEXT(identificadorFlujo As String, flowOrder As Integer, path As String, baseDatosAccion As String, accion As String, aplicacion As String, contadorReferencia As Integer, level As Integer, diferido As Boolean,
                                                                        idnt_Elemento As String, sistemaFuncional As String, grupoUsuarios As String, usuario As String, baseDatosRetorno As String, pathRetorno As String, comentario As String,
                                                                        parametrosEspecificos As Dictionary(Of String, String), habilitado As Boolean) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_APIINIENDEXT, path:=path, param:=Constantes.PARAMETRO_ACCION_PROJECT, value:=baseDatosAccion, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_FLOW, value:=accion, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_REFERENCE, value:=String.Format("=$CONCAT([%REFERENCE%];[_{0}_{1:0000}])/$", aplicacion, contadorReferencia), comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=3, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_INIT, value:=Constantes.NUMERO_1, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=4, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=5, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_DEFERRED, value:=If(diferido, Constantes.NUMERO_1, Constantes.NUMERO_0), comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=6, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CODIGO_APLICACION, value:=aplicacion, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=7, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_DOMINIO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_DOMINIO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=8, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=9, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_DESCRIPTOR_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_DESCRIPTOR_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=10, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FEC_INI_SISTEMA, value:=Constantes.APIINIENDEXT_VALOR_FEC_INI_SISTEMA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=11, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FEC_INI_VIGENCIA, value:=Constantes.APIINIENDEXT_VALOR_FEC_INI_VIGENCIA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=12, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_INSTANCIA_N8, value:=Constantes.APIINIENDEXT_VALOR_INSTANCIA_N8, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=13, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_INSTANCIA_PROCEDIMIENTO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_INSTANCIA_PROCEDIMIENTO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=14, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_TIPO_ALTA, value:=Constantes.APIINIENDEXT_VALOR_TIPO_ALTA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=15, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CENTRO_FORAL, value:=Constantes.APIINIENDEXT_VALOR_CENTRO_FORAL, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=16, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_ORGANICO_TRAMITADOR, value:=Constantes.APIINIENDEXT_VALOR_ORGANICO_TRAMITADOR, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=17, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_PROCEDIMIENTO_JX, value:=Constantes.APIINIENDEXT_VALOR_PROCEDIMIENTO_JX, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=18, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FORMULARIO_JX, value:=Constantes.APIINIENDEXT_VALOR_FORMULARIO_JX, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=19, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_INTERESADO, value:=Constantes.APIINIENDEXT_VALOR_IDNT_INTERESADO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=20, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NIF_INTERESADO, value:=Constantes.APIINIENDEXT_VALOR_NIF_INTERESADO, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=21, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_REPRESENTANTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_REPRESENTANTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=22, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NIF_REPRESENTANTE, value:=Constantes.APIINIENDEXT_VALOR_NIF_REPRESENTANTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=23, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_TRAMITACION_CARPETA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_TRAMITACION_CARPETA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=24, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE_CARPETA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_EXPEDIENTE_CARPETA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=25, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=26, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=27, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_DESCRIPTOR_TIPO_EXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_DESCRIPTOR_TIPO_EXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=28, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES, value:=Constantes.APIINIENDEXT_VALOR_CODIGO_EXPEDIENTE_SISTEMAS_ACTUALES, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=29, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_SUBEXPEDIENTE, value:=Constantes.APIINIENDEXT_VALOR_IDNT_SUBEXPEDIENTE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=30, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_IDNT_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=31, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_REGISTRO_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_REGISTRO_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=32, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FECHA_REGISTRO_ENTRADA, value:=Constantes.APIINIENDEXT_VALOR_FECHA_REGISTRO_ENTRADA, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=33, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_NUMERO_EJECUCION, value:=Constantes.APIINIENDEXT_VALOR_NUMERO_EJECUCION, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=34, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_IDNT_ELEMENTO, value:=idnt_Elemento, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=35, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_SISTEMA_FUNCIONAL, value:=sistemaFuncional, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=36, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_GRUPO_USUARIOS, value:=grupoUsuarios, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=37, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_USUARIO, value:=usuario, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=38, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_BBDD_RETORNO, value:=baseDatosRetorno, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=39, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_FLUJO_RETORNO, value:=If(Not String.IsNullOrEmpty(baseDatosRetorno), identificadorFlujo, String.Empty), comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=40, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_PATH_RETORNO, value:=pathRetorno, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=41, action:=String.Empty, path:=String.Empty, param:=Constantes.APIINIENDEXT_PARAMETRO_REFERENCIA_RETORNO, value:=If(Not String.IsNullOrEmpty(baseDatosRetorno), Constantes.APIINIENDEXT_VALOR_REFERENCIA_RETORNO, String.Empty), comentario:=String.Empty)
            }

            ' Comprobamos si tenemos parámetros específicos o la llamada Deshabilitada
            If Not IsNothing(parametrosEspecificos) OrElse Not habilitado Then
                Dim contadorParametro As Integer = Constantes.APIINIENDEXT_NUMERO_PARAMETROS_DEFECTO_APIINIENDEXT

                ' Comprobamos si tenemos parámetros específicos
                If Not IsNothing(parametrosEspecificos) Then
                    For Each parametroEspecifico As KeyValuePair(Of String, String) In parametrosEspecificos
                        lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=parametroEspecifico.Key, value:=parametroEspecifico.Value, comentario:=String.Empty))
                        contadorParametro += 1
                    Next
                End If

                ' Comprobamos si la llamada está Deshabilitada
                If Not habilitado Then
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ENABLE, value:=Constantes.NUMERO_0, comentario:=String.Empty))
                End If
            End If

            Return lineasDetalle
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_ENDPROC(flowOrder As Integer, path As String, level As Integer, comentario As String) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_ENDPROC, path:=path, param:=Constantes.PARAMETRO_ACCION_COMENTARIO, value:=String.Empty, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty)
            }
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_JUMP(flowOrder As Integer, path As String, puntoSalto As String, level As Integer, comentario As String) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_JUMP, path:=path, param:=Constantes.PARAMETRO_ACCION_PATH, value:=puntoSalto, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty)
            }
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_UNION(flowOrder As Integer, path As String, numeroRamasParalelas As Integer, level As Integer, comentario As String) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_UNION, path:=path, param:=String.Format("{0}1", Constantes.PARAMETRO_ACCION_PATH), value:=String.Empty, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=String.Format("{0}1", Constantes.PARAMETRO_ACCION_CONDITION), value:=String.Empty, comentario:=String.Empty)
            }

            ' Creamos el resto de puntos de unión
            Dim contadorParametro As Integer = 2
            If numeroRamasParalelas > Constantes.NUMERO_1 Then
                For numeroRama As Integer = 2 To numeroRamasParalelas
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=String.Format("{0}{1}", Constantes.PARAMETRO_ACCION_PATH, numeroRama), value:=String.Empty, comentario:=String.Empty))
                    contadorParametro += 1
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=String.Format("{0}{1}", Constantes.PARAMETRO_ACCION_CONDITION, numeroRama), value:=String.Empty, comentario:=String.Empty))
                    contadorParametro += 1
                Next
            End If

            ' Insertamos la línea del Parámetro LEVEL
            lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty))

            Return lineasDetalle
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_IF(flowOrder As Integer, path As String, valorParametroVAR1 As String, valorParametroVAR2 As String, condicion As String, tipoComparacion As String, saltoTRUE As String, saltoFALSE As String, level As Integer,
                                                             comentario As String) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_IF, path:=path, param:=Constantes.PARAMETRO_ACCION_VAR1, value:=valorParametroVAR1, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_VAR2, value:=valorParametroVAR2, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_CONDITION, value:=condicion, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=3, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_PATH, value:=saltoTRUE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=4, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ELSEPATH, value:=saltoFALSE, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=5, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_TYPE, value:=tipoComparacion, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=6, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty)
            }
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_ALERT(flowOrder As Integer, path As String, fechaReferencia As String, maturation As String, interval As String, pathAlerta As String, level As Integer, comentario As String) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_ALERT, path:=path, param:=Constantes.PARAMETRO_ACCION_DATE, value:=fechaReferencia, comentario:=comentario),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_MATURATION, value:=maturation, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_INTERVAL, value:=interval, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=3, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ALERTPATH, value:=pathAlerta, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=4, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_DAYSOFWEEK, value:="LMXJVSD", comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=5, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_EXACTTIME, value:="1", comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=6, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty)
            }
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_FORM(flowOrder As Integer, path As String, level As Integer, idnt_Elemento As String, tipoEjecucion As String, listadoParamtrosEspecificos As Dictionary(Of String, String)) As List(Of LineaDetalle)
            Return ObtenerLineasDetalle_Accion_FORM(flowOrder, path, level, esTramite:=True, idnt_Tramite:=idnt_Elemento, tipoEjecucion:=tipoEjecucion, listadoParamtrosEspecificos:=listadoParamtrosEspecificos)
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_FORM(flowOrder As Integer, path As String, level As Integer, esTramite As Boolean, idnt_Tramite As String, tipoEjecucion As String, listadoParamtrosEspecificos As Dictionary(Of String, String)) As List(Of LineaDetalle)
            Dim lineasDetalle As New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_FORM, path:=path, param:=Constantes.PARAMETRO_ACCION_FORM, value:=Constantes.FORM_VALOR_FORM, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_FILTER, value:=String.Empty, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_GROUP, value:=String.Empty, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=3, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_USER, value:=String.Empty, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=4, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_WAIT, value:=Constantes.NUMERO_1, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=5, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_TO, value:=Constantes.ALL, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=6, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ICON, value:=String.Empty, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=7, action:=String.Empty, path:=String.Empty, param:=Constantes.FORM_PARAMETRO_TIPOEJECUCION, value:=tipoEjecucion, comentario:=String.Empty)
            }

            ' Comprobamos si estamos tratando un FORM asociado a un trámite
            Dim contadorParametro As Integer = Constantes.FORM_NUMERO_PARAMETROS_DEFECTO
            If esTramite Then
                lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.FORM_PARAMETRO_IDNT_ELEMENTO, value:=idnt_Tramite, comentario:=String.Empty))
                contadorParametro += 1
            End If

            ' Comprobamos si tenemos parámetros específicos
            If Not IsNothing(listadoParamtrosEspecificos) Then
                For Each parametroEspecifico As KeyValuePair(Of String, String) In listadoParamtrosEspecificos
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=parametroEspecifico.Key, value:=parametroEspecifico.Value, comentario:=String.Empty))
                    contadorParametro += 1
                Next
            End If

            ' Obtenemos la sentencia para el Parámetro LEVEL
            lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty))

            Return lineasDetalle
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_LETVAR(flowOrder As Integer, path As String, level As Integer, listadoVariables As Dictionary(Of String, String), deInicio As Boolean) As List(Of LineaDetalle)
            ' Creamos el listado de Sentencias SQL
            Dim lineasDetalle As New List(Of LineaDetalle)

            ' Nos recorremos las Variables
            Dim contadorParametro As Integer = 0
            For Each variable As KeyValuePair(Of String, String) In listadoVariables
                If contadorParametro.Equals(0) Then
                    ' Estamos insertando la primera sentencia SQL para la acción LETVAR
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=Constantes.ACCION_LETVAR, path:=path, param:=variable.Key, value:=variable.Value, comentario:=String.Empty))

                Else
                    ' Estamos insertando el resto de Variables
                    lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=variable.Key, value:=variable.Value, comentario:=String.Empty))
                End If
                contadorParametro += 1
            Next

            ' Comprobamos si las Variables son de INICIO
            If deInicio Then
                lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.LETVAR_VARIABLES_INICIO, value:=Constantes.NUMERO_1, comentario:=String.Empty))
                contadorParametro += 1
            End If

            ' Obtenemos la sentencia para el Parámetro LEVEL
            lineasDetalle.Add(New LineaDetalle(flowOrder:=flowOrder, id:=contadorParametro, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty))

            Return lineasDetalle
        End Function

        Friend Shared Function ObtenerLineasDetalle_Accion_CANCEL(flowOrder As Integer, path As String, valorPasoCancelar As String, level As Integer) As List(Of LineaDetalle)
            Return New List(Of LineaDetalle) From {
                New LineaDetalle(flowOrder:=flowOrder, id:=0, action:=Constantes.ACCION_CANCEL, path:=path, param:=Constantes.PARAMETRO_ACCION_PATH, value:=valorPasoCancelar, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=1, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_ALL, value:=Constantes.NUMERO_0, comentario:=String.Empty),
                New LineaDetalle(flowOrder:=flowOrder, id:=2, action:=String.Empty, path:=String.Empty, param:=Constantes.PARAMETRO_ACCION_LEVEL, value:=level.ToString, comentario:=String.Empty)
            }
        End Function
#End Region
    End Class
End Namespace