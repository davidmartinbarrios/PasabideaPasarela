Option Strict Off
Option Explicit On
Friend Class frmLogin
	Inherits System.Windows.Forms.Form
	
	Private Sub cmdAceptar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAceptar.Click
		
		
		On Error GoTo procErr
		
		Dim rstAux As ADODB.Recordset
		Dim strSQL As String
		
		conPasarela = New ADODB.Connection
		If ComprobarConexion(conPasarela, ReadIniFile(INIFile, "PASARELA", "Connection")) = False Then
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow 'vbNormal
			GoTo procFin
		End If
		strSQL = "SELECT * FROM USUARIOS WHERE USERID='" & txtUsuario.Text & "'"
		rstAux = mfRecordset(conPasarela, strSQL)
		If (rstAux.EOF) Then
			lblError.Text = "El usuario " & txtUsuario.Text & " no existe"
			lblError.Visible = True
			txtUsuario.Text = ""
			txtPassword.Text = ""
			txtUsuario.Focus()
		Else
			If rstAux.Fields("PASSWORD").Value = SetPassword(txtPassword.Text) Then
				If rstAux.Fields("ACTIVO").Value = True Then
					lblError.Text = "El usuario ya está registrado"
					lblError.Visible = True
					txtPassword.Text = ""
				Else
					If LeeTablaINI("CONFIGURACION", "VERSION") <> My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision Then
						'If MsgBox("La versión de pasarela que está ejecutandose en su equipo es diferente de la correcta, y esto puede provocar que los procedimientos no se vuelquen correctamente, ¿desea continuar de todos modos?", vbInformation + vbYesNo, "Versión Pasarela") = vbNo Then
						MsgBox("La versión de pasarela que está ejecutandose en su equipo está desactualizada")
						lblError.Text = ""
						End
						'End If
					End If
					strUserId = rstAux.Fields("USERID").Value
					strSQL = "UPDATE USUARIOS SET ACTIVO=1 WHERE USERID='" & txtUsuario.Text & "'"
					mfExecute(conPasarela, strSQL)
					Me.Close()
					frmMigrar.Show()
				End If
			Else
				lblError.Text = "Contraseña Incorrecta"
				lblError.Visible = True
				Me.txtPassword.Text = ""
				Me.txtPassword.Focus()
			End If
		End If
		GoTo procFin
		Exit Sub
procErr: 
		MsgBox(Err.Description, MsgBoxStyle.Critical)
		Resume procFin
		
procFin: 
		CierraRS(rstAux)
		'UPGRADE_NOTE: Object rstAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstAux = Nothing
		
	End Sub
	
	Private Sub cmdCancelar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelar.Click
		
		End
	End Sub
	
	Private Sub frmLogin_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		INIFile = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".ini"
		Me.Top = 0
		Me.Left = 0
		mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
		mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 400
		bMinimizado = False
		frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
		frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
		bMinimizado = True
		frmMDI.Icon = Me.Icon
		frmMDI.Text = Me.Text
		lbl_Version.Text = "V. " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
	End Sub
	
	
	Private Sub txtPassword_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If KeyAscii = 13 Then
			cmdAceptar_Click(cmdAceptar, New System.EventArgs())
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtUsuario_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUsuario.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If KeyAscii = 13 Then
			cmdAceptar_Click(cmdAceptar, New System.EventArgs())
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class