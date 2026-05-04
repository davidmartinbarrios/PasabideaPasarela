<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConf
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
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public WithEvents _lstProc_ColumnHeader_1 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_2 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_3 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_4 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_5 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_6 As System.Windows.Forms.ColumnHeader
	Public WithEvents _lstProc_ColumnHeader_7 As System.Windows.Forms.ColumnHeader
	Public WithEvents lstProc As System.Windows.Forms.ListView
	Public WithEvents cmdComenzar As System.Windows.Forms.Button
	Public WithEvents chkVolcar As System.Windows.Forms.CheckBox
	Public WithEvents txtConexion2 As System.Windows.Forms.TextBox
	Public WithEvents cmdCadenaConexion2 As System.Windows.Forms.Button
	Public WithEvents CmbBD As System.Windows.Forms.ComboBox
	Public WithEvents cmdAplicar As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringDB2 As System.Windows.Forms.Button
	Public WithEvents txtConexion As System.Windows.Forms.TextBox
	Public WithEvents cmdCalendar As System.Windows.Forms.Button
	Public WithEvents chkNuevaVersion As System.Windows.Forms.CheckBox
	Public WithEvents txtObserva As System.Windows.Forms.TextBox
	Public WithEvents mskFecha As System.Windows.Forms.MaskedTextBox
	Public WithEvents lblConexion2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblComen As System.Windows.Forms.Label
	Public WithEvents lblFecha As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmConf))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me.lstProc = New System.Windows.Forms.ListView
		Me._lstProc_ColumnHeader_1 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_2 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_3 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_4 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_5 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_6 = New System.Windows.Forms.ColumnHeader
		Me._lstProc_ColumnHeader_7 = New System.Windows.Forms.ColumnHeader
		Me.cmdComenzar = New System.Windows.Forms.Button
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.chkVolcar = New System.Windows.Forms.CheckBox
		Me.txtConexion2 = New System.Windows.Forms.TextBox
		Me.cmdCadenaConexion2 = New System.Windows.Forms.Button
		Me.CmbBD = New System.Windows.Forms.ComboBox
		Me.cmdAplicar = New System.Windows.Forms.Button
		Me.CmdSqlStringDB2 = New System.Windows.Forms.Button
		Me.txtConexion = New System.Windows.Forms.TextBox
		Me.cmdCalendar = New System.Windows.Forms.Button
		Me.chkNuevaVersion = New System.Windows.Forms.CheckBox
		Me.txtObserva = New System.Windows.Forms.TextBox
		Me.mskFecha = New System.Windows.Forms.MaskedTextBox
		Me.lblConexion2 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.lblComen = New System.Windows.Forms.Label
		Me.lblFecha = New System.Windows.Forms.Label
		Me.lstProc.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Pasarela Corporate Modeler - HidraNet - CONFIGURACIÓN PROCEDIMIENTOS"
		Me.ClientSize = New System.Drawing.Size(719, 456)
		Me.Location = New System.Drawing.Point(-25, 7)
		Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("frmConf.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmConf"
		Me.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancelar.Text = "Cancelar"
		Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancelar.Size = New System.Drawing.Size(103, 25)
		Me.cmdCancelar.Location = New System.Drawing.Point(608, 420)
		Me.cmdCancelar.TabIndex = 13
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me.lstProc.Size = New System.Drawing.Size(701, 197)
		Me.lstProc.Location = New System.Drawing.Point(8, 8)
		Me.lstProc.TabIndex = 12
		Me.lstProc.View = System.Windows.Forms.View.Details
		Me.lstProc.LabelEdit = False
		Me.lstProc.MultiSelect = True
		Me.lstProc.LabelWrap = True
		Me.lstProc.HideSelection = False
		Me.lstProc.FullRowSelect = True
		Me.lstProc.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lstProc.BackColor = System.Drawing.SystemColors.Window
		Me.lstProc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lstProc.Name = "lstProc"
		Me._lstProc_ColumnHeader_1.Width = 0
		Me._lstProc_ColumnHeader_2.Text = "Nombre"
		Me._lstProc_ColumnHeader_2.Width = 412
		Me._lstProc_ColumnHeader_3.Text = "Activación"
		Me._lstProc_ColumnHeader_3.Width = 118
		Me._lstProc_ColumnHeader_4.Text = "Nueva versión"
		Me._lstProc_ColumnHeader_4.Width = 147
		Me._lstProc_ColumnHeader_5.Text = "BBDD"
		Me._lstProc_ColumnHeader_5.Width = 118
		Me._lstProc_ColumnHeader_6.Text = "Conexión"
		Me._lstProc_ColumnHeader_6.Width = 824
		Me._lstProc_ColumnHeader_7.Text = "Observaciones"
		Me._lstProc_ColumnHeader_7.Width = 170
		Me.cmdComenzar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdComenzar.Text = "Aceptar"
		Me.cmdComenzar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdComenzar.Size = New System.Drawing.Size(103, 25)
		Me.cmdComenzar.Location = New System.Drawing.Point(500, 420)
		Me.cmdComenzar.TabIndex = 11
		Me.cmdComenzar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdComenzar.CausesValidation = True
		Me.cmdComenzar.Enabled = True
		Me.cmdComenzar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdComenzar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdComenzar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdComenzar.TabStop = True
		Me.cmdComenzar.Name = "cmdComenzar"
		Me.Frame1.Size = New System.Drawing.Size(701, 215)
		Me.Frame1.Location = New System.Drawing.Point(8, 194)
		Me.Frame1.TabIndex = 0
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.chkVolcar.Text = "Volcado Adicional"
		Me.chkVolcar.Size = New System.Drawing.Size(141, 19)
		Me.chkVolcar.Location = New System.Drawing.Point(212, 48)
		Me.chkVolcar.TabIndex = 19
		Me.chkVolcar.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkVolcar.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkVolcar.BackColor = System.Drawing.SystemColors.Control
		Me.chkVolcar.CausesValidation = True
		Me.chkVolcar.Enabled = True
		Me.chkVolcar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkVolcar.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkVolcar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkVolcar.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkVolcar.TabStop = True
		Me.chkVolcar.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.chkVolcar.Visible = True
		Me.chkVolcar.Name = "chkVolcar"
		Me.txtConexion2.AutoSize = False
		Me.txtConexion2.Size = New System.Drawing.Size(557, 43)
		Me.txtConexion2.Location = New System.Drawing.Point(106, 128)
		Me.txtConexion2.MultiLine = True
		Me.txtConexion2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtConexion2.TabIndex = 17
		Me.txtConexion2.Visible = False
		Me.txtConexion2.AcceptsReturn = True
		Me.txtConexion2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtConexion2.BackColor = System.Drawing.SystemColors.Window
		Me.txtConexion2.CausesValidation = True
		Me.txtConexion2.Enabled = True
		Me.txtConexion2.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtConexion2.HideSelection = True
		Me.txtConexion2.ReadOnly = False
		Me.txtConexion2.Maxlength = 0
		Me.txtConexion2.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtConexion2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtConexion2.TabStop = True
		Me.txtConexion2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtConexion2.Name = "txtConexion2"
		Me.cmdCadenaConexion2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCadenaConexion2.Size = New System.Drawing.Size(25, 25)
		Me.cmdCadenaConexion2.Location = New System.Drawing.Point(668, 128)
		Me.cmdCadenaConexion2.Image = CType(resources.GetObject("cmdCadenaConexion2.Image"), System.Drawing.Image)
		Me.cmdCadenaConexion2.TabIndex = 16
		Me.ToolTip1.SetToolTip(Me.cmdCadenaConexion2, "Generar cadena de conexión de proyecto")
		Me.cmdCadenaConexion2.Visible = False
		Me.cmdCadenaConexion2.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCadenaConexion2.CausesValidation = True
		Me.cmdCadenaConexion2.Enabled = True
		Me.cmdCadenaConexion2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCadenaConexion2.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCadenaConexion2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCadenaConexion2.TabStop = True
		Me.cmdCadenaConexion2.Name = "cmdCadenaConexion2"
		Me.CmbBD.Size = New System.Drawing.Size(81, 22)
		Me.CmbBD.Location = New System.Drawing.Point(96, 48)
		Me.CmbBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CmbBD.TabIndex = 15
		Me.CmbBD.BackColor = System.Drawing.SystemColors.Window
		Me.CmbBD.CausesValidation = True
		Me.CmbBD.Enabled = True
		Me.CmbBD.ForeColor = System.Drawing.SystemColors.WindowText
		Me.CmbBD.IntegralHeight = True
		Me.CmbBD.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmbBD.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmbBD.Sorted = False
		Me.CmbBD.TabStop = True
		Me.CmbBD.Visible = True
		Me.CmbBD.Name = "CmbBD"
		Me.cmdAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAplicar.Text = "Aplicar Cambios"
		Me.cmdAplicar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAplicar.Size = New System.Drawing.Size(121, 25)
		Me.cmdAplicar.Location = New System.Drawing.Point(572, 180)
		Me.cmdAplicar.TabIndex = 10
		Me.cmdAplicar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAplicar.CausesValidation = True
		Me.cmdAplicar.Enabled = True
		Me.cmdAplicar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAplicar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAplicar.TabStop = True
		Me.cmdAplicar.Name = "cmdAplicar"
		Me.CmdSqlStringDB2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringDB2.Size = New System.Drawing.Size(25, 25)
		Me.CmdSqlStringDB2.Location = New System.Drawing.Point(668, 80)
		Me.CmdSqlStringDB2.Image = CType(resources.GetObject("CmdSqlStringDB2.Image"), System.Drawing.Image)
		Me.CmdSqlStringDB2.TabIndex = 9
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringDB2, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringDB2.Visible = False
		Me.CmdSqlStringDB2.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringDB2.CausesValidation = True
		Me.CmdSqlStringDB2.Enabled = True
		Me.CmdSqlStringDB2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringDB2.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringDB2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringDB2.TabStop = True
		Me.CmdSqlStringDB2.Name = "CmdSqlStringDB2"
		Me.txtConexion.AutoSize = False
		Me.txtConexion.Size = New System.Drawing.Size(557, 43)
		Me.txtConexion.Location = New System.Drawing.Point(106, 80)
		Me.txtConexion.ReadOnly = True
		Me.txtConexion.MultiLine = True
		Me.txtConexion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtConexion.TabIndex = 7
		Me.txtConexion.AcceptsReturn = True
		Me.txtConexion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtConexion.BackColor = System.Drawing.SystemColors.Window
		Me.txtConexion.CausesValidation = True
		Me.txtConexion.Enabled = True
		Me.txtConexion.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtConexion.HideSelection = True
		Me.txtConexion.Maxlength = 0
		Me.txtConexion.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtConexion.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtConexion.TabStop = True
		Me.txtConexion.Visible = True
		Me.txtConexion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtConexion.Name = "txtConexion"
		Me.cmdCalendar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCalendar.Size = New System.Drawing.Size(23, 23)
		Me.cmdCalendar.Location = New System.Drawing.Point(180, 14)
		Me.cmdCalendar.Image = CType(resources.GetObject("cmdCalendar.Image"), System.Drawing.Image)
		Me.cmdCalendar.TabIndex = 3
		Me.cmdCalendar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCalendar.CausesValidation = True
		Me.cmdCalendar.Enabled = True
		Me.cmdCalendar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCalendar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCalendar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCalendar.TabStop = True
		Me.cmdCalendar.Name = "cmdCalendar"
		Me.chkNuevaVersion.Text = "Activar nueva Versión"
		Me.chkNuevaVersion.Size = New System.Drawing.Size(141, 19)
		Me.chkNuevaVersion.Location = New System.Drawing.Point(212, 16)
		Me.chkNuevaVersion.TabIndex = 2
		Me.chkNuevaVersion.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkNuevaVersion.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkNuevaVersion.BackColor = System.Drawing.SystemColors.Control
		Me.chkNuevaVersion.CausesValidation = True
		Me.chkNuevaVersion.Enabled = True
		Me.chkNuevaVersion.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkNuevaVersion.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkNuevaVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkNuevaVersion.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkNuevaVersion.TabStop = True
		Me.chkNuevaVersion.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.chkNuevaVersion.Visible = True
		Me.chkNuevaVersion.Name = "chkNuevaVersion"
		Me.txtObserva.AutoSize = False
		Me.txtObserva.Enabled = False
		Me.txtObserva.Size = New System.Drawing.Size(263, 43)
		Me.txtObserva.Location = New System.Drawing.Point(430, 14)
		Me.txtObserva.Maxlength = 250
		Me.txtObserva.MultiLine = True
		Me.txtObserva.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtObserva.TabIndex = 1
		Me.txtObserva.AcceptsReturn = True
		Me.txtObserva.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtObserva.BackColor = System.Drawing.SystemColors.Window
		Me.txtObserva.CausesValidation = True
		Me.txtObserva.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtObserva.HideSelection = True
		Me.txtObserva.ReadOnly = False
		Me.txtObserva.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtObserva.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtObserva.TabStop = True
		Me.txtObserva.Visible = True
		Me.txtObserva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtObserva.Name = "txtObserva"
		Me.mskFecha.AllowPromptAsInput = False
		Me.mskFecha.Size = New System.Drawing.Size(79, 21)
		Me.mskFecha.Location = New System.Drawing.Point(98, 14)
		Me.mskFecha.TabIndex = 4
		Me.mskFecha.MaxLength = 10
		Me.mskFecha.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.mskFecha.Mask = "##/##/####"
		Me.mskFecha.PromptChar = "_"
		Me.mskFecha.Name = "mskFecha"
		Me.lblConexion2.Text = "Volcado Adicional:"
		Me.lblConexion2.Size = New System.Drawing.Size(91, 17)
		Me.lblConexion2.Location = New System.Drawing.Point(6, 124)
		Me.lblConexion2.TabIndex = 18
		Me.lblConexion2.Visible = False
		Me.lblConexion2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblConexion2.BackColor = System.Drawing.SystemColors.Control
		Me.lblConexion2.Enabled = True
		Me.lblConexion2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblConexion2.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblConexion2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblConexion2.UseMnemonic = True
		Me.lblConexion2.AutoSize = False
		Me.lblConexion2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblConexion2.Name = "lblConexion2"
		Me.Label3.Text = "Base de datos:"
		Me.Label3.Size = New System.Drawing.Size(89, 17)
		Me.Label3.Location = New System.Drawing.Point(8, 48)
		Me.Label3.TabIndex = 14
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
		Me.Label1.Text = "Cadena Conexión:"
		Me.Label1.Size = New System.Drawing.Size(91, 17)
		Me.Label1.Location = New System.Drawing.Point(6, 80)
		Me.Label1.TabIndex = 8
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
		Me.lblComen.Text = "Comentarios:"
		Me.lblComen.Size = New System.Drawing.Size(65, 17)
		Me.lblComen.Location = New System.Drawing.Point(362, 18)
		Me.lblComen.TabIndex = 6
		Me.lblComen.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblComen.BackColor = System.Drawing.SystemColors.Control
		Me.lblComen.Enabled = True
		Me.lblComen.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblComen.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblComen.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblComen.UseMnemonic = True
		Me.lblComen.Visible = True
		Me.lblComen.AutoSize = False
		Me.lblComen.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblComen.Name = "lblComen"
		Me.lblFecha.Text = "Fecha Activación:"
		Me.lblFecha.Size = New System.Drawing.Size(91, 17)
		Me.lblFecha.Location = New System.Drawing.Point(6, 16)
		Me.lblFecha.TabIndex = 5
		Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblFecha.BackColor = System.Drawing.SystemColors.Control
		Me.lblFecha.Enabled = True
		Me.lblFecha.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblFecha.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblFecha.UseMnemonic = True
		Me.lblFecha.Visible = True
		Me.lblFecha.AutoSize = False
		Me.lblFecha.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblFecha.Name = "lblFecha"
		Me.Controls.Add(cmdCancelar)
		Me.Controls.Add(lstProc)
		Me.Controls.Add(cmdComenzar)
		Me.Controls.Add(Frame1)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_1)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_2)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_3)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_4)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_5)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_6)
		Me.lstProc.Columns.Add(_lstProc_ColumnHeader_7)
		Me.Frame1.Controls.Add(chkVolcar)
		Me.Frame1.Controls.Add(txtConexion2)
		Me.Frame1.Controls.Add(cmdCadenaConexion2)
		Me.Frame1.Controls.Add(CmbBD)
		Me.Frame1.Controls.Add(cmdAplicar)
		Me.Frame1.Controls.Add(CmdSqlStringDB2)
		Me.Frame1.Controls.Add(txtConexion)
		Me.Frame1.Controls.Add(cmdCalendar)
		Me.Frame1.Controls.Add(chkNuevaVersion)
		Me.Frame1.Controls.Add(txtObserva)
		Me.Frame1.Controls.Add(mskFecha)
		Me.Frame1.Controls.Add(lblConexion2)
		Me.Frame1.Controls.Add(Label3)
		Me.Frame1.Controls.Add(Label1)
		Me.Frame1.Controls.Add(lblComen)
		Me.Frame1.Controls.Add(lblFecha)
		Me.lstProc.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class