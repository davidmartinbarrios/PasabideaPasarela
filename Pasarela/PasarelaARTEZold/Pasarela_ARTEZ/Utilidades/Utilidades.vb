Imports System.Configuration
Imports System.IO
Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela.Utilidades
    Friend NotInheritable Class Utilidades
        Friend Shared Function ObtenerValorParaParametro(valor As String) As String
            Return If(String.IsNullOrEmpty(valor), String.Empty, If(valor.StartsWith("@INICIO!"), valor, If(valor.StartsWith("@"), String.Format("@INICIO!{0}", valor.Replace("@", String.Empty)), valor)))
        End Function

        Friend Shared Function ObtenerIdentificadorTramiteAutomatico(tramiteAutomatico As DatosTramiteAutomatico) As String
            Return If(Not IsNothing(tramiteAutomatico), tramiteAutomatico.Idnt_Elemento, String.Empty)
        End Function

        Friend Shared Function ObtenerIdentificadorTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.Idnt_Elemento, String.Empty)
        End Function

        Friend Shared Function ObtenerSistemaFuncionalTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.SistemaFuncional, String.Empty)
        End Function

        Friend Shared Function ObtenerGrupoUsuariosTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.GrupoUsuarios, String.Empty)
        End Function

        Friend Shared Function ObtenerUsuarioTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.Usuario, String.Empty)
        End Function

        Friend Shared Function ObtenerModoCreacionTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.ModoCreacion, String.Empty)
        End Function

        Friend Shared Function ObtenerObservacionesTramiteManual(tramiteManual As DatosTramiteManual) As String
            Return If(Not IsNothing(tramiteManual), tramiteManual.Observaciones, String.Empty)
        End Function

        Friend Shared Function ObtenerBaseDatosDestinoFlujos() As String()
            Return ObtenerParametroConfiguracion_ListadoString("BaseDatosDestinoFlujos").ToArray
        End Function

        Friend Shared Function ObtenerBaseDatosConAccionesDiferidas() As List(Of String)
            Return ObtenerParametroConfiguracion_ListadoString("BaseDatosConAccionesDiferidas")
        End Function

        Friend Shared Function ObtenerEntornosDestinoFlujosAdicionales() As String()
            Return ObtenerParametroConfiguracion_ListadoString("EntornosDestinoFlujosAdicionales").ToArray
        End Function

        Friend Shared Function ObtenerBaseDatosAccionesInfraestructura() As String()
            Return ObtenerParametroConfiguracion_ListadoString("BaseDatosAccionesInfraestructura").ToArray
        End Function

        Friend Shared Function CargarProcedimiento(ficheroXML As FileInfo) As Procedimiento
            Dim procedimiento As Procedimiento = Nothing

            Try
                Using secuenciaArchivo As New FileStream(ficheroXML.FullName, FileMode.Open)
                    Dim serialitzador As New XmlSerializer(GetType(Procedimiento))
                    procedimiento = serialitzador.Deserialize(secuenciaArchivo)
                End Using

            Catch ex As Exception
                Throw New Exception(String.Format("ERROR al Cargar la Definición XML del Procedimiento: {0}", ex.ToString))
            End Try

            Return procedimiento
        End Function

        Friend Shared Function CargarCatalogoAcciones() As CatalogoAcciones
            Dim catalogoAcciones As CatalogoAcciones = Nothing

            Try
                ' Obtenemos el Fichero XML con el Catálogo de Acciones
                Dim ficheroXMLCatalogoAcciones As FileInfo = ObtenerXMLCatalogoAcciones()

                ' Obtenemos el Catálogo de Acciones del XML
                Using secuenciaArchivo As New FileStream(ficheroXMLCatalogoAcciones.FullName, FileMode.Open)
                    Dim serialitzador As New XmlSerializer(GetType(CatalogoAcciones))
                    catalogoAcciones = serialitzador.Deserialize(secuenciaArchivo)
                End Using

                ' Obtenemos el Listado de Bases de Datos con Acciones Diferidas
                Dim listadoBaseDatosConAccionesDiferidas As List(Of String) = ObtenerBaseDatosConAccionesDiferidas()

                ' Nos recorremos el Catálogo de Acciones para Inicializar el Parámetro Ejecución Diferida
                For Each definicionAccion As DefinicionAccion In catalogoAcciones.DefinicionesAcciones
                    ' Comprobamos si la Acción está marcada como Diferida en la Definición
                    If definicionAccion.MarcadaComoDiferidaEnDefinicion Then
                        ' La Acción está marcada como Diferida en Definición, así que activamos el indicador Ejecución Diferida
                        definicionAccion.EjecucionDiferida = True

                    ElseIf Not IsNothing(listadoBaseDatosConAccionesDiferidas) AndAlso listadoBaseDatosConAccionesDiferidas.Contains(definicionAccion.BaseDatos) Then
                        ' La Acción no está marcada como Diferida en la Definición, pero su Base de Datos está configurada como Base de Datos con Acciones Diferidas, así que activamos el indicador Ejecución Diferida
                        definicionAccion.EjecucionDiferida = True

                    Else
                        definicionAccion.EjecucionDiferida = False
                    End If
                Next

            Catch ex As Exception
                Throw New Exception("ERROR al Cargar el Catálogo de Acciones.")
            End Try

            Return catalogoAcciones
        End Function

        Friend Shared Function ObtenerEntornos(entornosAdicionalesSeleccionados As List(Of String), baseDatosDestinoFlujo As String) As Entornos
            Dim entornosSeleccionados As New Entornos

            ' Obtenemos las Cadenas de Conexión parametrizadas
            Dim cadenasConexionEntornos As Dictionary(Of String, String) = ObtenerCadenasConexionEntornos()

            ' Obtenemos la Base de Datos de Infraestructura
            Dim baseDatosInfraestructura As String = ObtenerBaseDatosInfraestructura()

            ' Obtenemos la Base de Datos de Motor
            Dim baseDatosMotor As String = ObtenerBaseDatosMotor()

            ' Obtenemos el nombre del Entorno por Defecto
            Dim entornoDestinoFlujoPorDefecto As String = ObtenerEntornoDestinoFlujoPorDefecto()

            ' Cargamos el entorno Por Defecto
            entornosSeleccionados.AddEntorno(esPredeterminado:=True,
                                             cadenaConexionDestino:=String.Format(cadenasConexionEntornos.Item(entornoDestinoFlujoPorDefecto), baseDatosDestinoFlujo),
                                             cadenaConexionInfraestructura:=String.Format(cadenasConexionEntornos.Item(entornoDestinoFlujoPorDefecto), baseDatosInfraestructura),
                                             cadenaConexionMotor:=String.Format(cadenasConexionEntornos.Item(entornoDestinoFlujoPorDefecto), baseDatosMotor))

            ' Nos recorremos los Entornos Seleccionados para obtener las cadenas de conexión correspondientes
            If Not IsNothing(entornosAdicionalesSeleccionados) AndAlso entornosAdicionalesSeleccionados.Count > 0 Then
                For Each entorno As String In entornosAdicionalesSeleccionados
                    entornosSeleccionados.AddEntorno(esPredeterminado:=False,
                                                     cadenaConexionDestino:=String.Format(cadenasConexionEntornos.Item(entorno), baseDatosDestinoFlujo),
                                                     cadenaConexionInfraestructura:=String.Format(cadenasConexionEntornos.Item(entorno), baseDatosInfraestructura),
                                                     cadenaConexionMotor:=String.Format(cadenasConexionEntornos.Item(entorno), baseDatosMotor))
                Next
            End If

            Return entornosSeleccionados
        End Function

        Friend Shared Function SuperaNumeroMaximoCaracteresPATH(nombrePATH As String) As Boolean
            Return If(nombrePATH.Length > Constantes.WFFLOWACTIONS_PATH_NUMERO_CARACTERES, True, False)
        End Function

        Friend Shared Function NumeroCaracteresExtraPATH(nombrePath As String) As Integer
            Return nombrePath.Length - Constantes.WFFLOWACTIONS_PATH_NUMERO_CARACTERES
        End Function

        Friend Shared Function ObtenerPathTramite(ByRef resumenTramitesNombreEnFlujo As Dictionary(Of String, String), tramite As String) As String
            Dim pathTramite As String = String.Empty

            If resumenTramitesNombreEnFlujo.ContainsKey(tramite.ToUpper) Then
                pathTramite = resumenTramitesNombreEnFlujo.Item(tramite.ToUpper)
            Else
                Throw New Exception(String.Format("No se localiza el trámite '{0}' como trámite del Flujo", tramite.ToUpper))
            End If

            Return pathTramite
        End Function

#Region "Métodos Privados"
        Private Shared Function ObtenerBaseDatosInfraestructura() As String
            Return ObtenerParametroConfiguracion_String("BaseDatosInfraestructura")
        End Function

        Private Shared Function ObtenerBaseDatosMotor() As String
            Return ObtenerParametroConfiguracion_String("BaseDatosMotor")
        End Function

        Private Shared Function ObtenerEntornoDestinoFlujoPorDefecto() As String
            Return ObtenerParametroConfiguracion_String("EntornoDestinoFlujoPorDefecto")
        End Function

        Private Shared Function ObtenerXMLCatalogoAcciones() As FileInfo
            Return ObtenerParametroConfiguracion_Fichero("CatalogoAcciones")
        End Function

        Private Shared Function ObtenerParametroConfiguracion_Fichero(parametro As String) As FileInfo
            Return If(String.IsNullOrEmpty(ConfigurationManager.AppSettings(parametro)), Nothing, New FileInfo(ConfigurationManager.AppSettings(parametro).ToString.Trim))
        End Function

        Private Shared Function ObtenerParametroConfiguracion_String(parametro As String) As String
            Return If(String.IsNullOrEmpty(ConfigurationManager.AppSettings().Item(parametro)), String.Empty, ConfigurationManager.AppSettings().Item(parametro).ToString.Trim)
        End Function

        Private Shared Function ObtenerParametroConfiguracion_Boolean(parametro As String) As Boolean
            Dim parametroConfiguracion As Boolean

            ' Obtenemos el valor del parámetro de Configuración
            Dim valorParametroConfiguracion As String = ObtenerParametroConfiguracion_String(parametro)

            If String.IsNullOrEmpty(valorParametroConfiguracion) Then
                parametroConfiguracion = False

            Else
                Select Case valorParametroConfiguracion.Trim.ToUpper
                    Case Constantes.N
                        parametroConfiguracion = False

                    Case Constantes.S
                        parametroConfiguracion = True

                    Case Else
                        parametroConfiguracion = False
                End Select
            End If

            Return parametroConfiguracion
        End Function

        Private Shared Function ObtenerParametroConfiguracion_ListadoString(parametro As String) As List(Of String)
            Return ObtenerParametroConfiguracion_ListadoString(parametro, Nothing)
        End Function

        Private Shared Function ObtenerParametroConfiguracion_ListadoString(parametro As String, separador As String) As List(Of String)
            If String.IsNullOrEmpty(separador) Then
                separador = "|"
            End If

            Return If(String.IsNullOrEmpty(ConfigurationManager.AppSettings().Item(parametro)), Nothing, ConfigurationManager.AppSettings().Item(parametro).ToString.Trim.Split(separador).ToList)
        End Function

        Private Shared Function ObtenerCadenasConexionEntornos() As Dictionary(Of String, String)
            Dim cadenasConexionEntornos As New Dictionary(Of String, String)

            For Each configuracionCadenaConexion As ConnectionStringSettings In ConfigurationManager.ConnectionStrings()
                cadenasConexionEntornos.Add(configuracionCadenaConexion.Name, configuracionCadenaConexion.ConnectionString)
            Next

            Return cadenasConexionEntornos
        End Function
#End Region
    End Class
End Namespace

