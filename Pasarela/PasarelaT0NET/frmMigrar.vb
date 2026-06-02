Option Strict Off
Option Explicit On
Friend Class frmMigrar
    Inherits System.Windows.Forms.Form
    Dim DataLink As MSDASC.DataLinks
    Dim strCadenaConexion As String
    Dim strCadenaConexionCM10 As String
    Dim colRef As Collection
    Dim colMod As Collection



    'UPGRADE_WARNING: Event cboModelo.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub cboModelo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboModelo.SelectedIndexChanged
        Dim strCadenaModelo As String
        'UPGRADE_WARNING: Couldn't resolve default property of object colMod(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        strModelo = colMod.Item(cboModelo.SelectedIndex + 1)
        strCadenaModelo = ReadIniFile(INIFile, "SQLMODELO", "Connection")
        txtCadena.Text = strCadenaModelo
    End Sub

    'UPGRADE_WARNING: Event cboReferencias.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub cboReferencias_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReferencias.SelectedIndexChanged
        If cboReferencias.Text = "Accion Social" Then
            strFamil = "AS"
        Else
            strFamil = "FP"
        End If

    End Sub


    Private Sub cmdAceptar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAceptar.Click

        Dim strProviderCM10 As String
        Dim strPasswordCM10 As String
        Dim blnSecurityCM10 As Boolean
        Dim strUserCM10 As String
        Dim strCatalogSQLCM10 As String
        Dim strDataSourceCM10 As String
        Dim strProviderSQL As String
        Dim strPasswordSQL As String
        Dim blnSecuritySQL As Boolean
        Dim strUserSQL As String
        Dim strCatalogSQL As String
        Dim strDataSourceSQL As String
        Dim blnResultado As Boolean
        Dim strResultado As String
        Dim CodAplicacion As String


        cboReferencias.Text = "ARTEZ"
        'cboModelo.Text = "ARTEZELI"
        cboModelo.SelectedValue = "ARTEZ"


        If cboReferencias.Text = "" Then
            MsgBox("SELECCIONE UNA FAMILIA DE REFERENCIAS")
            cboReferencias.Focus()
            Exit Sub
            'Ainhoa 12/11/2007
            'Es obligatorio seleccionar un modelo. El valor del
            'modelo se utiliza para obtener el nombre de la BD:
            '"CM+Valor Modelo"
        ElseIf cboModelo.Text = "" Then
            MsgBox("SELECCIONE UN MODELO", MsgBoxStyle.Information)
            cboModelo.Focus()
            Exit Sub
        Else

            '      strFamil = "FP" 'LeeTablaINI("FAMILIAVARIABLES", colRef(cboReferencias.ListIndex + 1))
            '***** Ainhoa 12/11/2007
            'Lo vuelvo a dejar como en la version CM8 para que funcione el combo
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            'TODO ARTEZ
            lngNombre = 0
            CodAplicacion = "N8"

            'lngNombre = CInt(LeeTablaINI("NOMBRE", (cboReferencias.Text)))
            'CodAplicacion = LeeTablaINI("CodAplicacion", (cboReferencias.Text))


            'Ainhoa 12/11/2007
            'Asigno valor a la variable del modelo
            'UPGRADE_WARNING: Couldn't resolve default property of object colMod.Item(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strModelo = colMod.Item(cboModelo.Text)


        End If

        strCadenaConexion = txtCadena.Text
        conSqlServer = New ADODB.Connection
        If ComprobarConexion(conSqlServer, strCadenaConexion) = False Then
            MsgBox("La cadena de conexión a SQL Server no es correcta o el servidor no se encuentra disponible.")
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
            GoTo procFin
        End If

        'Inicio Jonathan Prieto 16/05/2013: Obtenemos el código del Trámite Usuario para el modelo seleccionado
        gstrCodTramUsu = CStr(fObtenerCodigoTramiteUsuario(strModelo))
        If gstrCodTramUsu = vbNullString Then
            MsgBox("No se ha podido obtener el código del Trámite Usuario para el Modelo seleccionado.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
            GoTo procFin
        End If
        'Fin Jonathan Prieto 16/05/2013



        Dim padre As Form = Me.MdiParent

        Dim _frmConexion As New frmConexion()

        If padre IsNot Nothing Then
            _frmConexion.MdiParent = padre
            _frmConexion.StartPosition = FormStartPosition.Manual
            _frmConexion.Location = New Point(0, 0)
        End If

        _frmConexion.Show()
        _frmConexion.BringToFront()

        Me.Hide()

        'frmConexion.Show()
        'Dim padre As Form = Me.MdiParent
        'Dim siguiente As New frmConexion()
        'If padre IsNot Nothing Then
        'siguiente.MdiParent = padre
        'siguiente.StartPosition = FormStartPosition.Manual
        'siguiente.Location = New Point(0, 0)
        'siguiente.WindowState = FormWindowState.Maximized
        'End If
        'siguiente.Show()
        'siguiente.BringToFront()
        'Me.Hide()



procFin:

    End Sub

    Private Sub CmdBuscar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdBuscar.Click

        On Error Resume Next
        Dim strCadenaModelo As String
        Dim Cnx As String

        Cnx = txtCadena.Text
        txtCadena.Text = DataLink.PromptNew
        strCadenaModelo = txtCadena.Text
        Call WriteIniFile(INIFile, "SQLMODELO", "Connection", strCadenaModelo)

    End Sub



    Private Sub cmdCerrar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCerrar.Click

        Dim strSQL As String

        strSQL = "UPDATE USUARIOS SET ACTIVO=0 WHERE USERID='" & strUserId & "'"
        mfExecute(conPasarela, strSQL)
        End

    End Sub


    Private Sub frmMigrar_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim strFamilia As String

        'Ainhoa 09/10/2007
        'Para CM8 esto se hacía en frmLogin
        'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        INIFile = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".ini"
        conPasarela = New ADODB.Connection
        If ComprobarConexion(conPasarela, ReadIniFile(INIFile, "PASARELA", "Connection")) = False Then
            MsgBox("La cadena de conexión a Pasarela no es correcta o el servidor no se encuentra disponible. Revise el fichero INI")
            Me.Enabled = True
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
            End
        End If
        'fin Ainhoa
        conDP4 = New ADODB.Connection
        If ComprobarConexion(conDP4, ReadIniFile(INIFile, "SQLMODELO", "Connection")) = False Then
            MsgBox("La cadena de conexión a Corporate Modeler no es correcta o el servidor no se encuentra disponible. Revise el fichero INI")
            Me.Enabled = True
            'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
            'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
            End
        End If
        DataLink = New MSDASC.DataLinks

        'Me.Top = 0
        'Me.Left = 0
        'mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
        'mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 400
        'bMinimizado = False
        'frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
        'frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
        'bMinimizado = True

        Me.MdiParent.Icon = Me.Icon
        Me.MdiParent.Text = Me.Text

        Me.MdiParent.Width = 1000
        Me.MdiParent.Height = 700

        Me.Width = 900
        Me.Height = 600

        Me.Left = 0
        Me.Top = 0


        'Me.StartPosition = FormStartPosition.CenterScreen
        'frmMDI.StartPosition = FormStartPosition.CenterScreen
        'frmMDI.Size = New Size(Me.Width + 40, Me.Height + 80)
        'bMinimizado = True


        'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        INIFile = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".ini"
        msCargarComboReferencias()
        msCargarComboModelo()
        strFamilia = ReadIniFile(INIFile, "REFERENCIA", "FAMILIA")
        If strFamilia <> "" Then
            Me.cboReferencias.Text = strFamilia
        End If

        cboModelo.Text = "ARTEZ"


    End Sub

    Private Sub frmMigrar_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Dim I As Integer

        For I = 1 To colRef.Count()
            colRef.Remove(1)
        Next I
        'UPGRADE_NOTE: Object colRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        colRef = Nothing

    End Sub


    Private Sub msCargarComboReferencias()

        On Error GoTo procErr

        Dim strSQL As String
        Dim rs As ADODB.Recordset
        Dim strFamilia As String

        rs = New ADODB.Recordset
        strSQL = "SELECT CLAVE, VALOR FROM INI WHERE SECCION='REFERENCIAS'"
        rs = mfRecordset(conPasarela, strSQL)
        cboReferencias.Items.Clear()
        colRef = New Collection
        While Not rs.EOF
            cboReferencias.Items.Add(CStr(rs.Fields("CLAVE").Value))
            colRef.Add(CStr(rs.Fields("VALOR").Value), CStr(rs.Fields("CLAVE").Value))
            rs.MoveNext()
        End While
        strFamilia = ReadIniFile(INIFile, "REFERENCIA", "FAMILIA")
        If strFamilia <> "" Then
            cboReferencias.Text = strFamilia
        End If
        rs.Close()

        'TODO ARTEZ
        cboReferencias.Items.Add("ARTEZ")
        colRef.Add("ARTEZ", "ARTEZ")




        'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        rs = Nothing
        txtCadena.Text = ReadIniFile(INIFile, "SQLMODELO", "Connection")
        GoTo procFin

procErr:
        MsgBox(Err.Description)
        Resume procFin
procFin:
        CierraRS(rs)
        'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        rs = Nothing
    End Sub

    Private Sub msCargarComboModelo()

        On Error GoTo procErr

        Dim strSQL As String
        Dim rs As ADODB.Recordset
        Dim strFamilia As String

        rs = New ADODB.Recordset
        strSQL = "Select MO_FILE AS VALOR, MO_NAME AS CLAVE "
        strSQL = strSQL & "FROM MODEL WHERE MODEL_NAME='CWADMN09' ORDER BY CLAVE"
        rs = mfRecordset(conDP4, strSQL)
        cboModelo.Items.Clear()
        colMod = New Collection
        While Not rs.EOF
            cboModelo.Items.Add(CStr(rs.Fields("CLAVE").Value))
            colMod.Add(CStr(rs.Fields("VALOR").Value), CStr(rs.Fields("CLAVE").Value))
            rs.MoveNext()
        End While
        rs.Close()

        'TODO ARTEZ
        'cboModelo.Items.Add("ARTEZELI")
        cboModelo.SelectedText = "ARTEZ"

        'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        rs = Nothing
        GoTo procFin

procErr:
        MsgBox(Err.Description)
        Resume procFin
procFin:
        CierraRS(rs)
        'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        rs = Nothing
    End Sub
End Class