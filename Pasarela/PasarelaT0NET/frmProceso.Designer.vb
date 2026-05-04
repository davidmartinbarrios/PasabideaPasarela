<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProceso
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
    Public WithEvents pgbProceso As System.Windows.Forms.ProgressBar 'PictureBox
	Public WithEvents cmdNotepad As System.Windows.Forms.Button
	Public WithEvents CmdCancelarActual As System.Windows.Forms.Button
	Public WithEvents tmrProg As System.Windows.Forms.Timer
	Public WithEvents cmdForzar As System.Windows.Forms.Button
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents LblError As System.Windows.Forms.Label
	Public WithEvents lblProc As System.Windows.Forms.Label
	Public WithEvents lblMensaje As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmProceso))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.pgbProceso = New System.Windows.Forms.ProgressBar  ' PictureBox
		Me.cmdNotepad = New System.Windows.Forms.Button
		Me.CmdCancelarActual = New System.Windows.Forms.Button
		Me.tmrProg = New System.Windows.Forms.Timer(components)
		Me.cmdForzar = New System.Windows.Forms.Button
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog
		Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog
		Me.CommonDialog1Font = New System.Windows.Forms.FontDialog
		Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog
		Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog
		Me.LblError = New System.Windows.Forms.Label
		Me.lblProc = New System.Windows.Forms.Label
		Me.lblMensaje = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.ControlBox = False
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.ClientSize = New System.Drawing.Size(614, 124)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.Icon = CType(resources.GetObject("frmProceso.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmProceso"
		Me.pgbProceso.Size = New System.Drawing.Size(561, 25)
		Me.pgbProceso.Location = New System.Drawing.Point(16, 64)
		Me.pgbProceso.TabIndex = 7
		Me.pgbProceso.Dock = System.Windows.Forms.DockStyle.None
		Me.pgbProceso.BackColor = System.Drawing.SystemColors.Control
		Me.pgbProceso.CausesValidation = True
		Me.pgbProceso.Enabled = True
		Me.pgbProceso.ForeColor = System.Drawing.SystemColors.ControlText
		Me.pgbProceso.Cursor = System.Windows.Forms.Cursors.Default
		Me.pgbProceso.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.pgbProceso.TabStop = True
		Me.pgbProceso.Visible = True
        'Me.pgbProceso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        'Me.pgbProceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.pgbProceso.Name = "pgbProceso"
		Me.cmdNotepad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdNotepad.Size = New System.Drawing.Size(23, 24)
		Me.cmdNotepad.Location = New System.Drawing.Point(576, 8)
		Me.cmdNotepad.Image = CType(resources.GetObject("cmdNotepad.Image"), System.Drawing.Image)
		Me.cmdNotepad.TabIndex = 5
		Me.ToolTip1.SetToolTip(Me.cmdNotepad, "Generar Excel")
		Me.cmdNotepad.Visible = False
		Me.cmdNotepad.BackColor = System.Drawing.SystemColors.Control
		Me.cmdNotepad.CausesValidation = True
		Me.cmdNotepad.Enabled = True
		Me.cmdNotepad.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdNotepad.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdNotepad.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdNotepad.TabStop = True
		Me.cmdNotepad.Name = "cmdNotepad"
		Me.CmdCancelarActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdCancelarActual.Text = "Cancelar Actual"
		Me.CmdCancelarActual.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdCancelarActual.Size = New System.Drawing.Size(119, 23)
		Me.CmdCancelarActual.Location = New System.Drawing.Point(184, 98)
		Me.CmdCancelarActual.TabIndex = 4
		Me.CmdCancelarActual.BackColor = System.Drawing.SystemColors.Control
		Me.CmdCancelarActual.CausesValidation = True
		Me.CmdCancelarActual.Enabled = True
		Me.CmdCancelarActual.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdCancelarActual.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdCancelarActual.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdCancelarActual.TabStop = True
		Me.CmdCancelarActual.Name = "CmdCancelarActual"
		Me.tmrProg.Enabled = False
		Me.tmrProg.Interval = 30000
		Me.cmdForzar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdForzar.Text = "Forzar"
		Me.cmdForzar.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdForzar.Size = New System.Drawing.Size(87, 23)
		Me.cmdForzar.Location = New System.Drawing.Point(480, 40)
		Me.cmdForzar.TabIndex = 3
		Me.cmdForzar.Visible = False
		Me.cmdForzar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdForzar.CausesValidation = True
		Me.cmdForzar.Enabled = True
		Me.cmdForzar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdForzar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdForzar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdForzar.TabStop = True
		Me.cmdForzar.Name = "cmdForzar"
		Me.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancelar.Text = "Cancelar Todos"
		Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancelar.Size = New System.Drawing.Size(119, 23)
		Me.cmdCancelar.Location = New System.Drawing.Point(320, 98)
		Me.cmdCancelar.TabIndex = 2
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me.CommonDialog1Open.Title = "Selecciona un archivo"
		Me.LblError.Text = "Ha ocurrido un error"
		Me.LblError.ForeColor = System.Drawing.Color.FromARGB(192, 0, 0)
		Me.LblError.Size = New System.Drawing.Size(113, 17)
		Me.LblError.Location = New System.Drawing.Point(456, 16)
		Me.LblError.TabIndex = 6
		Me.LblError.Visible = False
		Me.LblError.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.LblError.BackColor = System.Drawing.SystemColors.Control
		Me.LblError.Enabled = True
		Me.LblError.Cursor = System.Windows.Forms.Cursors.Default
		Me.LblError.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LblError.UseMnemonic = True
		Me.LblError.AutoSize = False
		Me.LblError.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.LblError.Name = "LblError"
		Me.lblProc.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblProc.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblProc.Size = New System.Drawing.Size(609, 31)
		Me.lblProc.Location = New System.Drawing.Point(3, 6)
		Me.lblProc.TabIndex = 1
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
		Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblMensaje.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMensaje.Size = New System.Drawing.Size(603, 33)
		Me.lblMensaje.Location = New System.Drawing.Point(6, 36)
		Me.lblMensaje.TabIndex = 0
		Me.lblMensaje.BackColor = System.Drawing.SystemColors.Control
		Me.lblMensaje.Enabled = True
		Me.lblMensaje.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblMensaje.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblMensaje.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblMensaje.UseMnemonic = True
		Me.lblMensaje.Visible = True
		Me.lblMensaje.AutoSize = False
		Me.lblMensaje.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblMensaje.Name = "lblMensaje"
		Me.Controls.Add(pgbProceso)
		Me.Controls.Add(cmdNotepad)
		Me.Controls.Add(CmdCancelarActual)
		Me.Controls.Add(cmdForzar)
		Me.Controls.Add(cmdCancelar)
		Me.Controls.Add(LblError)
		Me.Controls.Add(lblProc)
		Me.Controls.Add(lblMensaje)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class