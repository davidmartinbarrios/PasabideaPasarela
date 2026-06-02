Imports System.Windows.Forms

Public Class TestProcesoLauncher
    Inherits Form

    Private intTimer As Integer

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Me.IsMdiContainer = True
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "TEST frmProceso"

        PrepararContextoTest()
        AbrirFrmProceso()
    End Sub

    Private Sub PrepararContextoTest()

        INIFile = IO.Path.Combine(
        My.Application.Info.DirectoryPath,
        My.Application.Info.AssemblyName & ".ini"
    )

        ' Ajusta estos valores a tu caso real
        strUserId = Environment.UserName.ToUpper()
        strFamil = "N8"
        'strModelo = "ARDATZ"
        strModelo = "ARTEZ"
        lngNombre = 0
        gstrCodTramUsu = "0"

        strFechaProgramada = ""
        blnParar = False
        blnCancelar = False
        blnCon1 = True
        intTimer = 0

        conPasarela = AbrirConexionIni("PASARELA")
        conDP4 = AbrirConexionIni("SQLMODELO")
        conDB2 = AbrirConexionIni("DB2")

        conInfra = AbrirConexionIni("GENERALAPLICACION" & strFamil)
        conEspe = AbrirConexionIni("ESPECIFICA" & strFamil)
        conGene = AbrirConexionIni("GENERALES" & strFamil)
        conGestion = AbrirConexionIni("GESTION" & strFamil)
        conWord = AbrirConexionIni("WORD" & strFamil)
        conDocu = AbrirConexionIni("DOCU" & strFamil)
        conMSJ = AbrirConexionIni("MSJ" & strFamil)
        conInfrT0 = AbrirConexionIni("INFRT0")

        ' Inicialización conservadora para código legacy.
        ' Si detectas que conCarga debe apuntar a otra BBDD destino,
        ' cámbialo por la conexión del proceso pendiente.
        conCarga = conEspe
        conCarga2 = conEspe

        PrepararProcesoPendienteTest()

    End Sub

    Private Function AbrirConexionIni(seccion As String) As ADODB.Connection

        Dim cn As New ADODB.Connection
        Dim cadena As String = ReadIniFile(INIFile, seccion, "Connection")

        If String.IsNullOrWhiteSpace(cadena) Then
            Throw New Exception("No existe cadena de conexión en INI: [" & seccion & "]")
        End If

        If Not ComprobarConexion(cn, cadena) Then
            Throw New Exception("No conecta con sección INI: [" & seccion & "]")
        End If

        Return cn

    End Function

    Private Sub PrepararProcesoPendienteTest()

        ' Ajusta estos 3 datos al procedimiento que quieras probar
        Dim procedimiento As String = "PO052118"
        Dim diId As Integer = 12345
        Dim nombreCm As String = "PO052118 SME Comunicación espontánea"

        Dim sql As String

        sql = "DELETE FROM PROCESOS_PENDIENTES " &
              "WHERE PROCEDIMIENTO='" & procedimiento & "' " &
              "AND USERID='" & strUserId & "'"

        mfExecute(conPasarela, sql)

        sql = "INSERT INTO PROCESOS_PENDIENTES " &
              "(PROCEDIMIENTO, ID, NOMBRE_CM, FECHA_ACTIVACION, NUEVA_VERSION, CONEXION, FINALIZADO, USERID, BASEDATOS, GRUPO) VALUES (" &
              "'" & procedimiento & "', " &
              diId & ", " &
              "'" & nombreCm.Replace("'", "''") & "', " &
              "GETDATE(), " &
              "'0', " &
              "'" & ReadIniFile(INIFile, "PASARELA", "Connection").Replace("'", "''") & "', " &
              "'N', " &
              "'" & strUserId & "', " &
              "'DBT0GEST', " &
              "'000')"

        mfExecute(conPasarela, sql)
    End Sub

    Private Sub AbrirFrmProceso()

        Dim f As New frmProceso()

        f.MdiParent = Me
        f.StartPosition = FormStartPosition.Manual
        f.Location = New Drawing.Point(0, 0)
        f.Size = Me.ClientSize

        f.Show()
        f.BringToFront()

    End Sub

End Class