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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProceso))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdNotepad = New System.Windows.Forms.Button()
        Me.pgbProceso = New System.Windows.Forms.ProgressBar()
        Me.CmdCancelarActual = New System.Windows.Forms.Button()
        Me.tmrProg = New System.Windows.Forms.Timer(Me.components)
        Me.cmdForzar = New System.Windows.Forms.Button()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.LblError = New System.Windows.Forms.Label()
        Me.lblProc = New System.Windows.Forms.Label()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdNotepad
        '
        Me.cmdNotepad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNotepad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNotepad.Image = CType(resources.GetObject("cmdNotepad.Image"), System.Drawing.Image)
        Me.cmdNotepad.Location = New System.Drawing.Point(576, 8)
        Me.cmdNotepad.Name = "cmdNotepad"
        Me.cmdNotepad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNotepad.Size = New System.Drawing.Size(23, 24)
        Me.cmdNotepad.TabIndex = 5
        Me.cmdNotepad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdNotepad, "Generar Excel")
        Me.cmdNotepad.UseVisualStyleBackColor = False
        Me.cmdNotepad.Visible = False
        '
        'pgbProceso
        '
        Me.pgbProceso.BackColor = System.Drawing.SystemColors.Control
        Me.pgbProceso.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pgbProceso.Location = New System.Drawing.Point(16, 64)
        Me.pgbProceso.Name = "pgbProceso"
        Me.pgbProceso.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pgbProceso.Size = New System.Drawing.Size(561, 25)
        Me.pgbProceso.TabIndex = 7
        '
        'CmdCancelarActual
        '
        Me.CmdCancelarActual.BackColor = System.Drawing.SystemColors.Control
        Me.CmdCancelarActual.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdCancelarActual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCancelarActual.Location = New System.Drawing.Point(184, 98)
        Me.CmdCancelarActual.Name = "CmdCancelarActual"
        Me.CmdCancelarActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCancelarActual.Size = New System.Drawing.Size(119, 23)
        Me.CmdCancelarActual.TabIndex = 4
        Me.CmdCancelarActual.Text = "Cancelar Actual"
        Me.CmdCancelarActual.UseVisualStyleBackColor = False
        '
        'tmrProg
        '
        Me.tmrProg.Interval = 30000
        AddHandler Me.tmrProg.Tick, AddressOf Me.tmrProg_Tick
        '
        'cmdForzar
        '
        Me.cmdForzar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdForzar.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdForzar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdForzar.Location = New System.Drawing.Point(480, 40)
        Me.cmdForzar.Name = "cmdForzar"
        Me.cmdForzar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdForzar.Size = New System.Drawing.Size(87, 23)
        Me.cmdForzar.TabIndex = 3
        Me.cmdForzar.Text = "Forzar"
        Me.cmdForzar.UseVisualStyleBackColor = False
        Me.cmdForzar.Visible = False
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelar.Location = New System.Drawing.Point(320, 98)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelar.Size = New System.Drawing.Size(119, 23)
        Me.cmdCancelar.TabIndex = 2
        Me.cmdCancelar.Text = "Cancelar Todos"
        Me.cmdCancelar.UseVisualStyleBackColor = False
        '
        'CommonDialog1Open
        '
        Me.CommonDialog1Open.Title = "Selecciona un archivo"
        '
        'LblError
        '
        Me.LblError.BackColor = System.Drawing.SystemColors.Control
        Me.LblError.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblError.Location = New System.Drawing.Point(456, 16)
        Me.LblError.Name = "LblError"
        Me.LblError.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblError.Size = New System.Drawing.Size(113, 17)
        Me.LblError.TabIndex = 6
        Me.LblError.Text = "Ha ocurrido un error"
        Me.LblError.Visible = False
        '
        'lblProc
        '
        Me.lblProc.BackColor = System.Drawing.SystemColors.Control
        Me.lblProc.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProc.Location = New System.Drawing.Point(3, 6)
        Me.lblProc.Name = "lblProc"
        Me.lblProc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProc.Size = New System.Drawing.Size(609, 31)
        Me.lblProc.TabIndex = 1
        Me.lblProc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblMensaje
        '
        Me.lblMensaje.BackColor = System.Drawing.SystemColors.Control
        Me.lblMensaje.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMensaje.Location = New System.Drawing.Point(6, 36)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMensaje.Size = New System.Drawing.Size(603, 33)
        Me.lblMensaje.TabIndex = 0
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmProceso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(614, 124)
        Me.ControlBox = False
        Me.Controls.Add(Me.pgbProceso)
        Me.Controls.Add(Me.cmdNotepad)
        Me.Controls.Add(Me.CmdCancelarActual)
        Me.Controls.Add(Me.cmdForzar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.LblError)
        Me.Controls.Add(Me.lblProc)
        Me.Controls.Add(Me.lblMensaje)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProceso"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        AddHandler Load, AddressOf Me.frmProceso_Load
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class