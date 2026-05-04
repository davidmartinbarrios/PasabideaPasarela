Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    Friend NotInheritable Class Entorno
        Friend ReadOnly Property EsPredeterminado() As Boolean

        Friend ReadOnly Property CadenaConexionInfraestructura() As String

        Friend ReadOnly Property CadenaConexionDestino() As String

        Friend ReadOnly Property CadenaConexionMotor() As String

        Friend Sub New(esPredeterminado As Boolean, cadenaConexionDestino As String, cadenaConexionInfraestructura As String, cadenaConexionMotor As String)
            Me.EsPredeterminado = esPredeterminado
            Me.CadenaConexionDestino = cadenaConexionDestino
            Me.CadenaConexionInfraestructura = cadenaConexionInfraestructura
            Me.CadenaConexionMotor = cadenaConexionMotor
        End Sub

        Friend Sub AddFlujoAplicacionMotor(aplicacion As String, baseDatosDestinoFlujo As String, identificadorFlujo As String, descripcionFlujo As String, version As Integer, ByRef detalleFlujo As DetalleFlujo)
            Dim definicionCreada As Boolean = False

            Try
                ' Creamos la Definición del Flujo y damos de alta el Flujo en el Motor
                AccesoDatos.CrearDefinicionFlujo(CadenaConexionMotor, aplicacion, baseDatosDestinoFlujo, identificadorFlujo, version, descripcionFlujo, detalleFlujo.ToXML, Constantes.USUARIO_PASARELA)

            Catch ex As Exception
                ' Comprobamos si estamos tratando el Entorno Predeterminado
                If EsPredeterminado Then
                    Throw New Exception("ERROR al dar de alta el Flujo para la Aplicación en el motor.", ex)
                End If
            End Try
        End Sub
    End Class
End Namespace