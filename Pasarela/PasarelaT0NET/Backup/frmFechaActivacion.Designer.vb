<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFechaActivacion
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
	Public WithEvents cmdCalendar As System.Windows.Forms.Button
	Public WithEvents _optProg_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optProg_0 As System.Windows.Forms.RadioButton
	Public WithEvents mskFecha As System.Windows.Forms.MaskedTextBox
	Public WithEvents mskHora As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdAceptar As System.Windows.Forms.Button
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public WithEvents optProg As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmFechaActivacion))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.cmdCalendar = New System.Windows.Forms.Button
		Me._optProg_1 = New System.Windows.Forms.RadioButton
		Me._optProg_0 = New System.Windows.Forms.RadioButton
		Me.mskFecha = New System.Windows.Forms.MaskedTextBox
		Me.mskHora = New System.Windows.Forms.MaskedTextBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.cmdAceptar = New System.Windows.Forms.Button
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me.optProg = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.Frame1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.optProg, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Pasarela Corporate Modeler - HidraNet - PROGRAMAR"
		Me.ClientSize = New System.Drawing.Size(364, 164)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.ControlBox = False
		Me.Icon = CType(resources.GetObject("frmFechaActivacion.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmFechaActivacion"
		Me.Frame1.Size = New System.Drawing.Size(341, 115)
		Me.Frame1.Location = New System.Drawing.Point(12, 9)
		Me.Frame1.TabIndex = 2
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.cmdCalendar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCalendar.Enabled = False
		Me.cmdCalendar.Size = New System.Drawing.Size(23, 23)
		Me.cmdCalendar.Location = New System.Drawing.Point(182, 74)
		Me.cmdCalendar.Image = CType(resources.GetObject("cmdCalendar.Image"), System.Drawing.Image)
		Me.cmdCalendar.TabIndex = 5
		Me.cmdCalendar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCalendar.CausesValidation = True
		Me.cmdCalendar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCalendar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCalendar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCalendar.TabStop = True
		Me.cmdCalendar.Name = "cmdCalendar"
		Me._optProg_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optProg_1.Text = "Programar ejecución"
		Me._optProg_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optProg_1.Size = New System.Drawing.Size(205, 25)
		Me._optProg_1.Location = New System.Drawing.Point(56, 44)
		Me._optProg_1.TabIndex = 4
		Me._optProg_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optProg_1.BackColor = System.Drawing.SystemColors.Control
		Me._optProg_1.CausesValidation = True
		Me._optProg_1.Enabled = True
		Me._optProg_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optProg_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optProg_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optProg_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optProg_1.TabStop = True
		Me._optProg_1.Checked = False
		Me._optProg_1.Visible = True
		Me._optProg_1.Name = "_optProg_1"
		Me._optProg_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optProg_0.Text = "Ejecutar ahora"
		Me._optProg_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optProg_0.Size = New System.Drawing.Size(171, 25)
		Me._optProg_0.Location = New System.Drawing.Point(56, 18)
		Me._optProg_0.TabIndex = 3
		Me._optProg_0.Checked = True
		Me._optProg_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optProg_0.BackColor = System.Drawing.SystemColors.Control
		Me._optProg_0.CausesValidation = True
		Me._optProg_0.Enabled = True
		Me._optProg_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optProg_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optProg_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optProg_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optProg_0.TabStop = True
		Me._optProg_0.Visible = True
		Me._optProg_0.Name = "_optProg_0"
		Me.mskFecha.AllowPromptAsInput = False
		Me.mskFecha.Size = New System.Drawing.Size(79, 21)
		Me.mskFecha.Location = New System.Drawing.Point(100, 74)
		Me.mskFecha.TabIndex = 6
		Me.mskFecha.Enabled = False
		Me.mskFecha.MaxLength = 10
		Me.mskFecha.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.mskFecha.Mask = "##/##/####"
		Me.mskFecha.PromptChar = "_"
		Me.mskFecha.Name = "mskFecha"
		Me.mskHora.AllowPromptAsInput = False
		Me.mskHora.Size = New System.Drawing.Size(47, 21)
		Me.mskHora.Location = New System.Drawing.Point(254, 74)
		Me.mskHora.TabIndex = 8
		Me.mskHora.Enabled = False
		Me.mskHora.MaxLength = 5
		Me.mskHora.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.mskHora.Mask = "##:##"
		Me.mskHora.PromptChar = "_"
		Me.mskHora.Name = "mskHora"
		Me.Label1.Text = "Hora:"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(29, 15)
		Me.Label1.Location = New System.Drawing.Point(216, 76)
		Me.Label1.TabIndex = 9
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
		Me.Label3.Text = "Fecha:"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(39, 15)
		Me.Label3.Location = New System.Drawing.Point(58, 76)
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
		Me.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAceptar.Text = "Aceptar"
		Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAceptar.Size = New System.Drawing.Size(97, 23)
		Me.cmdAceptar.Location = New System.Drawing.Point(156, 134)
		Me.cmdAceptar.TabIndex = 1
		Me.cmdAceptar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAceptar.CausesValidation = True
		Me.cmdAceptar.Enabled = True
		Me.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAceptar.TabStop = True
		Me.cmdAceptar.Name = "cmdAceptar"
		Me.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancelar.Text = "Cancelar"
		Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancelar.Size = New System.Drawing.Size(97, 23)
		Me.cmdCancelar.Location = New System.Drawing.Point(256, 133)
		Me.cmdCancelar.TabIndex = 0
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me.optProg.SetIndex(_optProg_1, CType(1, Short))
		Me.optProg.SetIndex(_optProg_0, CType(0, Short))
		CType(Me.optProg, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(Frame1)
		Me.Controls.Add(cmdAceptar)
		Me.Controls.Add(cmdCancelar)
		Me.Frame1.Controls.Add(cmdCalendar)
		Me.Frame1.Controls.Add(_optProg_1)
		Me.Frame1.Controls.Add(_optProg_0)
		Me.Frame1.Controls.Add(mskFecha)
		Me.Frame1.Controls.Add(mskHora)
		Me.Frame1.Controls.Add(Label1)
		Me.Frame1.Controls.Add(Label3)
		Me.Frame1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class