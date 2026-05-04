Imports System.Data.SqlClient
Imports System.Text

Namespace Bizkaia.Pasarela
    Friend NotInheritable Class Conexion
        Friend Shared Function EjecutarProcedimientoAlmacenado(cadenaConexion As String, procedimientoAlmacenado As String, listadoParametros As List(Of IDataParameter)) As DataTable
            Dim resultado As DataTable = Nothing

            ' Creamos y Abrimos la Conexión
            Using conexion As IDbConnection = New SqlConnection(cadenaConexion)
                conexion.Open()

                ' Creamos el Comando
                Using comando As IDbCommand = New SqlCommand
                    comando.Connection = conexion

                    ' Cargamos el comando con la sentencia a ejecutar
                    comando.CommandType = CommandType.StoredProcedure
                    comando.CommandText = procedimientoAlmacenado

                    ' Añadimos los Paráemtros al Comando
                    If Not IsNothing(listadoParametros) Then
                        For Each parametro As IDataParameter In listadoParametros
                            comando.Parameters.Add(parametro)
                        Next
                    End If

                    ' Creamos el Adaptador
                    Dim adaptador As IDbDataAdapter = New SqlDataAdapter
                    adaptador.SelectCommand = comando

                    ' Ejecutamos la Sentencia
                    Dim cacheDatos As DataSet = New DataSet
                    adaptador.Fill(cacheDatos)

                    ' Devolvemos el DataTable
                    resultado = If(cacheDatos.Tables.Count = 0, Nothing, cacheDatos.Tables(0))
                End Using
            End Using

            Return resultado
        End Function

        Friend Shared Function EjecutarProcedimientoAlmacenado(cadenaConexion As String, procedimientoAlmacenado As String, listadoParametrosEntrada As List(Of IDataParameter), ByRef listadoParametrosSalida As List(Of IDataParameter), ByRef returnValue As Integer?) As DataTable
            Dim resultado As DataTable = Nothing

            ' Creamos y Abrimos la Conexión
            Using conexion As IDbConnection = New SqlConnection(cadenaConexion)
                conexion.Open()

                ' Creamos el Comando
                Using comando As IDbCommand = New SqlCommand
                    comando.Connection = conexion

                    ' Cargamos el comando con la sentencia a ejecutar
                    comando.CommandType = CommandType.StoredProcedure
                    comando.CommandText = procedimientoAlmacenado

                    ' Añadimos los Paráemtros de Entrada al Comando
                    If Not IsNothing(listadoParametrosEntrada) Then
                        For Each parametro As IDataParameter In listadoParametrosEntrada
                            comando.Parameters.Add(parametro)
                        Next
                    End If

                    ' Añadimos los Parámetros de Salida al Comando
                    Dim hayParametroReturnValue As Boolean = False
                    If Not IsNothing(listadoParametrosSalida) Then
                        For Each parametro As IDataParameter In listadoParametrosSalida
                            ' Comprobamos si tenemos parámetro ReturnValue
                            If parametro.Direction = ParameterDirection.ReturnValue Then
                                hayParametroReturnValue = True
                            End If
                            comando.Parameters.Add(parametro)
                        Next
                    End If

                    ' Creamos el Adaptador
                    Dim adaptador As IDbDataAdapter = New SqlDataAdapter
                    adaptador.SelectCommand = comando

                    ' Ejecutamos la Sentencia
                    Dim cacheDatos As DataSet = New DataSet
                    adaptador.Fill(cacheDatos)

                    ' Devolvemos el DataTable
                    resultado = If(cacheDatos.Tables.Count = 0, Nothing, cacheDatos.Tables(0))

                    ' Sobreescribimos el parámetro de entrada con el listado de parámetros para actualziar el valor de los parámetros de Salida
                    listadoParametrosSalida.Clear()
                    For Each parametro As IDataParameter In comando.Parameters
                        If parametro.Direction = ParameterDirection.InputOutput OrElse parametro.Direction = ParameterDirection.Output Then
                            listadoParametrosSalida.Add(parametro)
                        ElseIf parametro.Direction = ParameterDirection.ReturnValue Then
                            returnValue = Integer.Parse(parametro.Value)
                        End If
                    Next
                End Using
            End Using

            Return resultado
        End Function
    End Class
End Namespace