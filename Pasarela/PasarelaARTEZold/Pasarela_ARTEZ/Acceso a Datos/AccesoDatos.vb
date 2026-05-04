Imports System.Data.SqlTypes
Imports System.IO
Imports System.Text
Imports Bizkaia.Pasarela.Utilidades

Namespace Bizkaia.Pasarela
    Friend NotInheritable Class AccesoDatos
        Friend Shared Function ObtenerVersionPasarela(cadenaConexion As String) As VersionPasarela
            ' Creamos los Parámetros de Salida
            Dim listadoParametrosSalida As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@MajorVigente",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.Int32
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@MinorVigente",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.Int32
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@BuildVigente",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.Int32
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@RevisionVigente",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.Int32
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@FechaInicioVigencia",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.DateTime2
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Observaciones",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.String,
                        .Size = 4000
                }
            }

            ' Ejecutamos el Procedimiento
            Dim valorRetorno As Integer? = Nothing
            Conexion.EjecutarProcedimientoAlmacenado(cadenaConexion, "SPN8PasrelaObtenerVersionVigentePasarela", Nothing, listadoParametrosSalida, returnValue:=valorRetorno)

            ' Devolvemos el resultado
            Return New VersionPasarela With {
                        .Major = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@MajorVigente")).Value,
                        .Minor = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@MinorVigente")).Value,
                        .Build = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@BuildVigente")).Value,
                        .Revision = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@RevisionVigente")).Value,
                        .FechaInicioVigencia = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@FechaInicioVigencia")).Value,
                        .Observaciones = listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@Observaciones")).Value
                   }
        End Function

        Friend Shared Sub VerificarRegistrarBaseDatosDestinoFlujo(cadenaConexion As String, aplicacion As String, baseDatosDestinoFlujo As String, nombreFlujo As String, ByRef baseDatosDestinoFlujoCorrecta As Boolean, ByRef baseDatosDestinoFlujoRegistrada As String)
            ' Creamos los Parámetros de Entrada
            Dim listadoParametrosEntrada As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Idnt_Aplicacion",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 2,
                        .Value = aplicacion
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@BaseDatosDestinoFlujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 54,
                        .Value = baseDatosDestinoFlujo
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@NombreFlujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 50,
                        .Value = nombreFlujo
                }
            }

            ' Creamos los Parámetros de Salida
            Dim listadoParametrosSalida As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@BaseDatosDestinoCorrecta",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.String,
                        .Size = 1
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@BaseDatosDestinoExistente",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.String,
                        .Size = 54
                }
            }

            ' Ejecutamos el Procedimiento
            Dim valorRetorno As Integer? = Nothing
            Conexion.EjecutarProcedimientoAlmacenado(cadenaConexion, "SPT0VerificarBaseDatosDestinoFlujoAplicacion", listadoParametrosEntrada, listadoParametrosSalida, returnValue:=valorRetorno)

            ' Devolvemos el resultado
            baseDatosDestinoFlujoCorrecta = If(listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@BaseDatosDestinoCorrecta")).Value.Equals(Constantes.S), True, False)
            baseDatosDestinoFlujoRegistrada = If(listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@BaseDatosDestinoExistente")).Value.Equals(DBNull.Value), String.Empty, listadoParametrosSalida.Find(Function(x) x.ParameterName.Equals("@BaseDatosDestinoExistente")).Value.ToString)
        End Sub

        Friend Shared Function ObtenerSiguienteVersionFlujo(cadenaConexion As String, aplicacion As String, baseDatosDestinoFlujo As String, nombreFlujo As String) As Integer
            ' Creamos los Parámetros de Entrada
            Dim listadoParametrosEntrada As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Idnt_Aplicacion",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 2,
                        .Value = aplicacion
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@BaseDatosDestinoFlujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 54,
                        .Value = baseDatosDestinoFlujo
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@NombreFlujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 50,
                        .Value = nombreFlujo
                }
            }

            ' Creamos los Parámetros de Salida
            Dim listadoParametrosSalida As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@SiguienteVersion",
                        .Direction = ParameterDirection.Output,
                        .DbType = DbType.Int32
                }
            }

            ' Ejecutamos el Procedimiento
            Dim valorRetorno As Integer? = Nothing
            Conexion.EjecutarProcedimientoAlmacenado(cadenaConexion, "SPN8PasrelaObtenerSiguienteVersionFlujo", listadoParametrosEntrada, listadoParametrosSalida, returnValue:=valorRetorno)

            ' Devolvemos el resultado
            Return Integer.Parse(listadoParametrosSalida.Item(0).Value)
        End Function

        Friend Shared Sub CrearDefinicionFlujo(cadenaConexion As String, aplicacion As String, baseDatosDestinoFlujo As String, identificadorFlujo As String, version As Integer, descripcionFlujo As String, xmlLineasDetalle As String, usuario As String)
            ' Creamos los Parámetros de Entrada
            Dim listadoParametrosEntrada As New List(Of IDataParameter) From {
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Usuario",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 17,
                        .Value = usuario
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Idnt_Aplicacion",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 2,
                        .Value = aplicacion
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Nombre_Base_Datos_Ejecucion",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 54,
                        .Value = baseDatosDestinoFlujo
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Nombre_Flujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 50,
                        .Value = identificadorFlujo
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Descripcion_Flujo",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.String,
                        .Size = 255,
                        .Value = descripcionFlujo
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@Version",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.Int32,
                        .Value = version
                },
                New SqlClient.SqlParameter With {
                        .ParameterName = "@DetalleFlujoXML",
                        .Direction = ParameterDirection.Input,
                        .DbType = DbType.Xml,
                        .Value = New SqlXml(New MemoryStream(Encoding.UTF8.GetBytes(xmlLineasDetalle)))
                }
            }

            ' Ejecutamos el Procedimiento
            Conexion.EjecutarProcedimientoAlmacenado(cadenaConexion, "SPT0CrearDefinicionFlujo_AltaBBDDFlujoAplicacion", listadoParametrosEntrada)
        End Sub
    End Class
End Namespace