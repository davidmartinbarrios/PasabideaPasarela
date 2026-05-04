<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLogin
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
		'This form is an MDI child.
		'This code simulates the VB6 
		' functionality of automatically
		' loading and showing an MDI
		' child's parent.
		Me.MDIParent = Pasarela.frmMDI
		Pasarela.frmMDI.Show
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cmdAceptar As System.Windows.Forms.Button
	Public WithEvents txtUsuario As System.Windows.Forms.TextBox
	Public WithEvents txtPassword As System.Windows.Forms.TextBox
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public WithEvents lbl_Version As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents lblError As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmLogin))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.cmdAceptar = New System.Windows.Forms.Button
		Me.txtUsuario = New System.Windows.Forms.TextBox
		Me.txtPassword = New System.Windows.Forms.TextBox
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me.lbl_Version = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.lblError = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "LOGIN"
		Me.ClientSize = New System.Drawing.Size(331, 217)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.ForeColor = System.Drawing.Color.Black
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.ShowInTaskbar = False
		Me.Visible = False
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmLogin"
		Me.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAceptar.Text = "Aceptar"
		Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAceptar.Size = New System.Drawing.Size(97, 25)
		Me.cmdAceptar.Location = New System.Drawing.Point(67, 129)
		Me.cmdAceptar.TabIndex = 2
		Me.cmdAceptar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAceptar.CausesValidation = True
		Me.cmdAceptar.Enabled = True
		Me.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAceptar.TabStop = True
		Me.cmdAceptar.Name = "cmdAceptar"
		Me.txtUsuario.AutoSize = False
		Me.txtUsuario.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtUsuario.Size = New System.Drawing.Size(129, 22)
		Me.txtUsuario.Location = New System.Drawing.Point(143, 63)
		Me.txtUsuario.TabIndex = 0
		Me.txtUsuario.AcceptsReturn = True
		Me.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtUsuario.BackColor = System.Drawing.SystemColors.Window
		Me.txtUsuario.CausesValidation = True
		Me.txtUsuario.Enabled = True
		Me.txtUsuario.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtUsuario.HideSelection = True
		Me.txtUsuario.ReadOnly = False
		Me.txtUsuario.Maxlength = 0
		Me.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtUsuario.MultiLine = False
		Me.txtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtUsuario.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtUsuario.TabStop = True
		Me.txtUsuario.Visible = True
		Me.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtUsuario.Name = "txtUsuario"
		Me.txtPassword.AutoSize = False
		Me.txtPassword.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPassword.Size = New System.Drawing.Size(129, 22)
		Me.txtPassword.IMEMode = System.Windows.Forms.ImeMode.Disable
		Me.txtPassword.Location = New System.Drawing.Point(143, 95)
		Me.txtPassword.PasswordChar = ChrW(42)
		Me.txtPassword.TabIndex = 1
		Me.txtPassword.AcceptsReturn = True
		Me.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
		Me.txtPassword.CausesValidation = True
		Me.txtPassword.Enabled = True
		Me.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPassword.HideSelection = True
		Me.txtPassword.ReadOnly = False
		Me.txtPassword.Maxlength = 0
		Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPassword.MultiLine = False
		Me.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPassword.TabStop = True
		Me.txtPassword.Visible = True
		Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtPassword.Name = "txtPassword"
		Me.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancelar.Text = "Cancelar"
		Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancelar.Size = New System.Drawing.Size(97, 25)
		Me.cmdCancelar.Location = New System.Drawing.Point(169, 129)
		Me.cmdCancelar.TabIndex = 3
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me.lbl_Version.Text = "xx.xx.xx"
		Me.lbl_Version.ForeColor = System.Drawing.SystemColors.Info
		Me.lbl_Version.Size = New System.Drawing.Size(65, 17)
		Me.lbl_Version.Location = New System.Drawing.Point(8, 200)
		Me.lbl_Version.TabIndex = 7
		Me.lbl_Version.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl_Version.BackColor = System.Drawing.SystemColors.Control
		Me.lbl_Version.Enabled = True
		Me.lbl_Version.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl_Version.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl_Version.UseMnemonic = True
		Me.lbl_Version.Visible = True
		Me.lbl_Version.AutoSize = False
		Me.lbl_Version.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl_Version.Name = "lbl_Version"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label1.Text = "Usuario:"
		Me.Label1.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(77, 17)
		Me.Label1.Location = New System.Drawing.Point(61, 65)
		Me.Label1.TabIndex = 6
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label2.Text = "Contraseña:"
		Me.Label2.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(77, 17)
		Me.Label2.Location = New System.Drawing.Point(61, 97)
		Me.Label2.TabIndex = 5
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblError.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblError.ForeColor = System.Drawing.Color.FromARGB(192, 0, 0)
		Me.lblError.Size = New System.Drawing.Size(325, 25)
		Me.lblError.Location = New System.Drawing.Point(3, 168)
		Me.lblError.TabIndex = 4
		Me.lblError.Visible = False
		Me.lblError.BackColor = System.Drawing.SystemColors.Control
		Me.lblError.Enabled = True
		Me.lblError.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblError.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblError.UseMnemonic = True
		Me.lblError.AutoSize = False
		Me.lblError.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblError.Name = "lblError"
		Me.Controls.Add(cmdAceptar)
		Me.Controls.Add(txtUsuario)
		Me.Controls.Add(txtPassword)
		Me.Controls.Add(cmdCancelar)
		Me.Controls.Add(lbl_Version)
		Me.Controls.Add(Label1)
		Me.Controls.Add(Label2)
		Me.Controls.Add(lblError)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class