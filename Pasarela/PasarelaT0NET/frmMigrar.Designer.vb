<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMigrar
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
		Me.MDIParent = PasarelaT0NET.frmMDI
		PasarelaT0NET.frmMDI.Show
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
	Public WithEvents cboModelo As System.Windows.Forms.ComboBox
	Public WithEvents cboReferencias As System.Windows.Forms.ComboBox
	Public WithEvents txtCadena As System.Windows.Forms.TextBox
	Public WithEvents cmdAceptar As System.Windows.Forms.Button
	Public WithEvents cmdCerrar As System.Windows.Forms.Button
	Public WithEvents CmdBuscar As System.Windows.Forms.Button
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMigrar))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.cboModelo = New System.Windows.Forms.ComboBox
		Me.cboReferencias = New System.Windows.Forms.ComboBox
		Me.txtCadena = New System.Windows.Forms.TextBox
		Me.cmdAceptar = New System.Windows.Forms.Button
		Me.cmdCerrar = New System.Windows.Forms.Button
		Me.CmdBuscar = New System.Windows.Forms.Button
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Pasarela"
		Me.ClientSize = New System.Drawing.Size(406, 218)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.ShowInTaskbar = False
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
		Me.Name = "frmMigrar"
		Me.cboModelo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboModelo.Size = New System.Drawing.Size(259, 22)
		Me.cboModelo.Location = New System.Drawing.Point(132, 48)
		Me.cboModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboModelo.TabIndex = 8
		Me.cboModelo.BackColor = System.Drawing.SystemColors.Window
		Me.cboModelo.CausesValidation = True
		Me.cboModelo.Enabled = True
		Me.cboModelo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboModelo.IntegralHeight = True
		Me.cboModelo.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboModelo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboModelo.Sorted = False
		Me.cboModelo.TabStop = True
		Me.cboModelo.Visible = True
		Me.cboModelo.Name = "cboModelo"
		Me.cboReferencias.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboReferencias.Size = New System.Drawing.Size(259, 22)
		Me.cboReferencias.Location = New System.Drawing.Point(132, 16)
		Me.cboReferencias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboReferencias.TabIndex = 5
		Me.cboReferencias.BackColor = System.Drawing.SystemColors.Window
		Me.cboReferencias.CausesValidation = True
		Me.cboReferencias.Enabled = True
		Me.cboReferencias.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboReferencias.IntegralHeight = True
		Me.cboReferencias.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboReferencias.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboReferencias.Sorted = False
		Me.cboReferencias.TabStop = True
		Me.cboReferencias.Visible = True
		Me.cboReferencias.Name = "cboReferencias"
		Me.txtCadena.AutoSize = False
		Me.txtCadena.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCadena.Size = New System.Drawing.Size(351, 61)
		Me.txtCadena.Location = New System.Drawing.Point(8, 106)
		Me.txtCadena.MultiLine = True
		Me.txtCadena.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtCadena.TabIndex = 4
		Me.txtCadena.AcceptsReturn = True
		Me.txtCadena.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCadena.BackColor = System.Drawing.SystemColors.Window
		Me.txtCadena.CausesValidation = True
		Me.txtCadena.Enabled = True
		Me.txtCadena.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCadena.HideSelection = True
		Me.txtCadena.ReadOnly = False
		Me.txtCadena.Maxlength = 0
		Me.txtCadena.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCadena.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCadena.TabStop = True
		Me.txtCadena.Visible = True
		Me.txtCadena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtCadena.Name = "txtCadena"
		Me.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAceptar.Text = "Continuar"
		Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAceptar.Size = New System.Drawing.Size(97, 25)
		Me.cmdAceptar.Location = New System.Drawing.Point(192, 184)
		Me.cmdAceptar.TabIndex = 3
		Me.cmdAceptar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAceptar.CausesValidation = True
		Me.cmdAceptar.Enabled = True
		Me.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAceptar.TabStop = True
		Me.cmdAceptar.Name = "cmdAceptar"
		Me.cmdCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCerrar.Text = "Cerrar"
		Me.cmdCerrar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCerrar.Size = New System.Drawing.Size(97, 25)
		Me.cmdCerrar.Location = New System.Drawing.Point(304, 184)
		Me.cmdCerrar.TabIndex = 2
		Me.cmdCerrar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCerrar.CausesValidation = True
		Me.cmdCerrar.Enabled = True
		Me.cmdCerrar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCerrar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCerrar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCerrar.TabStop = True
		Me.cmdCerrar.Name = "cmdCerrar"
		Me.CmdBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdBuscar.Size = New System.Drawing.Size(25, 25)
		Me.CmdBuscar.Location = New System.Drawing.Point(364, 108)
		Me.CmdBuscar.Image = CType(resources.GetObject("CmdBuscar.Image"), System.Drawing.Image)
		Me.CmdBuscar.TabIndex = 0
		Me.ToolTip1.SetToolTip(Me.CmdBuscar, "Generar cadena de conexión de proyecto")
		Me.CmdBuscar.BackColor = System.Drawing.SystemColors.Control
		Me.CmdBuscar.CausesValidation = True
		Me.CmdBuscar.Enabled = True
		Me.CmdBuscar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdBuscar.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdBuscar.TabStop = True
		Me.CmdBuscar.Name = "CmdBuscar"
		Me.Label3.Text = "Modelo"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(117, 19)
		Me.Label3.Location = New System.Drawing.Point(10, 48)
		Me.Label3.TabIndex = 7
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = False
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Me.Label9.Text = "Familia Procedimientos:"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(117, 19)
		Me.Label9.Location = New System.Drawing.Point(10, 16)
		Me.Label9.TabIndex = 6
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label9.BackColor = System.Drawing.SystemColors.Control
		Me.Label9.Enabled = True
		Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label9.UseMnemonic = True
		Me.Label9.Visible = True
		Me.Label9.AutoSize = False
		Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label9.Name = "Label9"
		Me.Label2.Text = "Cadena de conexion a CorporateModeler10"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(345, 17)
		Me.Label2.Location = New System.Drawing.Point(16, 88)
		Me.Label2.TabIndex = 1
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.Controls.Add(cboModelo)
		Me.Controls.Add(cboReferencias)
		Me.Controls.Add(txtCadena)
		Me.Controls.Add(cmdAceptar)
		Me.Controls.Add(cmdCerrar)
		Me.Controls.Add(CmdBuscar)
		Me.Controls.Add(Label3)
		Me.Controls.Add(Label9)
		Me.Controls.Add(Label2)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class