Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    Friend NotInheritable Class Entornos
        Private _listadoEntornos As List(Of Entorno) = Nothing

        Friend Sub AddEntorno(esPredeterminado As Boolean, cadenaConexionDestino As String, cadenaConexionInfraestructura As String, cadenaConexionMotor As String)
            If IsNothing(_listadoEntornos) Then
                _listadoEntornos = New List(Of Entorno)
            End If

            _listadoEntornos.Add(New Entorno(esPredeterminado, cadenaConexionDestino, cadenaConexionInfraestructura, cadenaConexionMotor))
        End Sub

        Friend Function ObtenerCadenaConexionInfraestructuraEntornoPredeterminado() As String
            Return _listadoEntornos.Find(Function(x) x.EsPredeterminado.Equals(True)).CadenaConexionInfraestructura
        End Function

        Friend Function ObtenerCadenaConexionMotorEntornoPredeterminado() As String
            Return _listadoEntornos.Find(Function(x) x.EsPredeterminado.Equals(True)).CadenaConexionMotor
        End Function

        Friend Sub AddFlujoAplicacionMotor(aplicacion As String, baseDatosDestinoFlujo As String, identificadorFlujo As String, descripcionFlujo As String, version As Integer, ByRef detalleFlujo As DetalleFlujo)
            ' Se añade el Flujo al entorno Predeterminado
            _listadoEntornos.Find(Function(x) x.EsPredeterminado.Equals(True)).AddFlujoAplicacionMotor(aplicacion, baseDatosDestinoFlujo, identificadorFlujo, descripcionFlujo, version, detalleFlujo)

            ' Se añade el Flujo a los entornos Adicionales
            For Each entorno As Entorno In _listadoEntornos.FindAll(Function(x) x.EsPredeterminado.Equals(False))
                entorno.AddFlujoAplicacionMotor(aplicacion, baseDatosDestinoFlujo, identificadorFlujo, descripcionFlujo, version, detalleFlujo)
            Next
        End Sub
    End Class
End Namespace