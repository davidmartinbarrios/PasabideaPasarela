<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmErrores
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
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
	Public WithEvents CmdContinuar As System.Windows.Forms.Button
	Public WithEvents cmdAplicar As System.Windows.Forms.Button
	Public WithEvents cmdTerminar As System.Windows.Forms.Button
	Public WithEvents cboAmbito As System.Windows.Forms.ComboBox
	Public WithEvents cmdNotepad As System.Windows.Forms.Button
	Public WithEvents fgrErrores As AxMSFlexGridLib.AxMSFlexGrid
	Public WithEvents lblProc As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblFilt As System.Windows.Forms.Label
	Public WithEvents lblError As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmErrores))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.CmdContinuar = New System.Windows.Forms.Button
		Me.cmdAplicar = New System.Windows.Forms.Button
		Me.cmdTerminar = New System.Windows.Forms.Button
		Me.cboAmbito = New System.Windows.Forms.ComboBox
		Me.cmdNotepad = New System.Windows.Forms.Button
		Me.fgrErrores = New AxMSFlexGridLib.AxMSFlexGrid
		Me.lblProc = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.lblFilt = New System.Windows.Forms.Label
		Me.lblError = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.fgrErrores, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Errores"
		Me.ClientSize = New System.Drawing.Size(775, 391)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("frmErrores.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmErrores"
		Me.CmdContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdContinuar.Text = "Continuar"
		Me.CmdContinuar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdContinuar.Size = New System.Drawing.Size(81, 29)
		Me.CmdContinuar.Location = New System.Drawing.Point(584, 352)
		Me.CmdContinuar.TabIndex = 9
		Me.CmdContinuar.BackColor = System.Drawing.SystemColors.Control
		Me.CmdContinuar.CausesValidation = True
		Me.CmdContinuar.Enabled = True
		Me.CmdContinuar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdContinuar.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdContinuar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdContinuar.TabStop = True
		Me.CmdContinuar.Name = "CmdContinuar"
		Me.cmdAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAplicar.Text = "Aplicar"
		Me.cmdAplicar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAplicar.Size = New System.Drawing.Size(97, 29)
		Me.cmdAplicar.Location = New System.Drawing.Point(360, 41)
		Me.cmdAplicar.TabIndex = 6
		Me.cmdAplicar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAplicar.CausesValidation = True
		Me.cmdAplicar.Enabled = True
		Me.cmdAplicar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAplicar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAplicar.TabStop = True
		Me.cmdAplicar.Name = "cmdAplicar"
		Me.cmdTerminar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdTerminar.Text = "Terminar"
		Me.cmdTerminar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdTerminar.Size = New System.Drawing.Size(81, 29)
		Me.cmdTerminar.Location = New System.Drawing.Point(674, 352)
		Me.cmdTerminar.TabIndex = 5
		Me.cmdTerminar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdTerminar.CausesValidation = True
		Me.cmdTerminar.Enabled = True
		Me.cmdTerminar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdTerminar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdTerminar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdTerminar.TabStop = True
		Me.cmdTerminar.Name = "cmdTerminar"
		Me.cboAmbito.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboAmbito.Size = New System.Drawing.Size(287, 23)
		Me.cboAmbito.Location = New System.Drawing.Point(71, 43)
		Me.cboAmbito.TabIndex = 4
		Me.cboAmbito.BackColor = System.Drawing.SystemColors.Window
		Me.cboAmbito.CausesValidation = True
		Me.cboAmbito.Enabled = True
		Me.cboAmbito.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboAmbito.IntegralHeight = True
		Me.cboAmbito.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboAmbito.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboAmbito.Sorted = False
		Me.cboAmbito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
		Me.cboAmbito.TabStop = True
		Me.cboAmbito.Visible = True
		Me.cboAmbito.Name = "cboAmbito"
		Me.cmdNotepad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdNotepad.Size = New System.Drawing.Size(23, 24)
		Me.cmdNotepad.Location = New System.Drawing.Point(726, 45)
		Me.cmdNotepad.Image = CType(resources.GetObject("cmdNotepad.Image"), System.Drawing.Image)
		Me.cmdNotepad.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.cmdNotepad, "Generar Bloq de Notas")
		Me.cmdNotepad.BackColor = System.Drawing.SystemColors.Control
		Me.cmdNotepad.CausesValidation = True
		Me.cmdNotepad.Enabled = True
		Me.cmdNotepad.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdNotepad.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdNotepad.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdNotepad.TabStop = True
		Me.cmdNotepad.Name = "cmdNotepad"
		fgrErrores.OcxState = CType(resources.GetObject("fgrErrores.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fgrErrores.Size = New System.Drawing.Size(733, 257)
		Me.fgrErrores.Location = New System.Drawing.Point(16, 81)
		Me.fgrErrores.TabIndex = 1
		Me.fgrErrores.Name = "fgrErrores"
		Me.lblProc.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblProc.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblProc.Size = New System.Drawing.Size(555, 31)
		Me.lblProc.Location = New System.Drawing.Point(22, 348)
		Me.lblProc.TabIndex = 8
		Me.lblProc.BackColor = System.Drawing.SystemColors.Control
		Me.lblProc.Enabled = True
		Me.lblProc.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblProc.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblProc.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblProc.UseMnemonic = True
		Me.lblProc.Visible = True
		Me.lblProc.AutoSize = False
		Me.lblProc.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblProc.Name = "lblProc"
		Me.Label1.Text = "Volcar errores a un fichero de texto"
		Me.Label1.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(197, 15)
		Me.Label1.Location = New System.Drawing.Point(528, 49)
		Me.Label1.TabIndex = 7
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.lblFilt.Text = "Ambito:"
		Me.lblFilt.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblFilt.Size = New System.Drawing.Size(43, 13)
		Me.lblFilt.Location = New System.Drawing.Point(23, 49)
		Me.lblFilt.TabIndex = 2
		Me.lblFilt.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblFilt.BackColor = System.Drawing.SystemColors.Control
		Me.lblFilt.Enabled = True
		Me.lblFilt.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblFilt.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblFilt.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblFilt.UseMnemonic = True
		Me.lblFilt.Visible = True
		Me.lblFilt.AutoSize = False
		Me.lblFilt.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblFilt.Name = "lblFilt"
		Me.lblError.Text = "Errores detectados por la pasarela"
		Me.lblError.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblError.Size = New System.Drawing.Size(495, 23)
		Me.lblError.Location = New System.Drawing.Point(23, 13)
		Me.lblError.TabIndex = 0
		Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblError.BackColor = System.Drawing.SystemColors.Control
		Me.lblError.Enabled = True
		Me.lblError.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblError.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblError.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblError.UseMnemonic = True
		Me.lblError.Visible = True
		Me.lblError.AutoSize = False
		Me.lblError.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblError.Name = "lblError"
		Me.Controls.Add(CmdContinuar)
		Me.Controls.Add(cmdAplicar)
		Me.Controls.Add(cmdTerminar)
		Me.Controls.Add(cboAmbito)
		Me.Controls.Add(cmdNotepad)
		Me.Controls.Add(fgrErrores)
		Me.Controls.Add(lblProc)
		Me.Controls.Add(Label1)
		Me.Controls.Add(lblFilt)
		Me.Controls.Add(lblError)
		CType(Me.fgrErrores, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class