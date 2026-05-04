<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConexion
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
	Public WithEvents txtInfrT0 As System.Windows.Forms.TextBox
	Public WithEvents CmdSqlStringInfrT0 As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringDocu As System.Windows.Forms.Button
	Public WithEvents txtDocu As System.Windows.Forms.TextBox
	Public WithEvents CmdSqlStringGene As System.Windows.Forms.Button
	Public WithEvents txtAccionesGene As System.Windows.Forms.TextBox
	Public WithEvents txtMsj As System.Windows.Forms.TextBox
	Public WithEvents CmdSqlStringMsj As System.Windows.Forms.Button
	Public WithEvents txtConexionDB2 As System.Windows.Forms.TextBox
	Public WithEvents txtConexionInfra As System.Windows.Forms.TextBox
	Public WithEvents txtConexionHid As System.Windows.Forms.TextBox
	Public WithEvents txtWord As System.Windows.Forms.TextBox
	Public WithEvents txtGestion As System.Windows.Forms.TextBox
	Public WithEvents CmdSqlStringGes As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringWord As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringHid As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringInf As System.Windows.Forms.Button
	Public WithEvents CmdSqlStringDB2 As System.Windows.Forms.Button
	Public WithEvents cmdAceptar As System.Windows.Forms.Button
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public WithEvents _Label7_1 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents _Label7_0 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label7 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmConexion))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.txtInfrT0 = New System.Windows.Forms.TextBox
		Me.CmdSqlStringInfrT0 = New System.Windows.Forms.Button
		Me.CmdSqlStringDocu = New System.Windows.Forms.Button
		Me.txtDocu = New System.Windows.Forms.TextBox
		Me.CmdSqlStringGene = New System.Windows.Forms.Button
		Me.txtAccionesGene = New System.Windows.Forms.TextBox
		Me.txtMsj = New System.Windows.Forms.TextBox
		Me.CmdSqlStringMsj = New System.Windows.Forms.Button
		Me.txtConexionDB2 = New System.Windows.Forms.TextBox
		Me.txtConexionInfra = New System.Windows.Forms.TextBox
		Me.txtConexionHid = New System.Windows.Forms.TextBox
		Me.txtWord = New System.Windows.Forms.TextBox
		Me.txtGestion = New System.Windows.Forms.TextBox
		Me.CmdSqlStringGes = New System.Windows.Forms.Button
		Me.CmdSqlStringWord = New System.Windows.Forms.Button
		Me.CmdSqlStringHid = New System.Windows.Forms.Button
		Me.CmdSqlStringInf = New System.Windows.Forms.Button
		Me.CmdSqlStringDB2 = New System.Windows.Forms.Button
		Me.cmdAceptar = New System.Windows.Forms.Button
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me._Label7_1 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me._Label7_0 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label7 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Conexión a Base de Datos"
		Me.ClientSize = New System.Drawing.Size(685, 494)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.Icon = CType(resources.GetObject("frmConexion.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmConexion"
		Me.txtInfrT0.AutoSize = False
		Me.txtInfrT0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtInfrT0.Size = New System.Drawing.Size(636, 37)
		Me.txtInfrT0.Location = New System.Drawing.Point(4, 416)
		Me.txtInfrT0.MultiLine = True
		Me.txtInfrT0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtInfrT0.TabIndex = 28
		Me.txtInfrT0.AcceptsReturn = True
		Me.txtInfrT0.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtInfrT0.BackColor = System.Drawing.SystemColors.Window
		Me.txtInfrT0.CausesValidation = True
		Me.txtInfrT0.Enabled = True
		Me.txtInfrT0.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtInfrT0.HideSelection = True
		Me.txtInfrT0.ReadOnly = False
		Me.txtInfrT0.Maxlength = 0
		Me.txtInfrT0.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtInfrT0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtInfrT0.TabStop = True
		Me.txtInfrT0.Visible = True
		Me.txtInfrT0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtInfrT0.Name = "txtInfrT0"
		Me.CmdSqlStringInfrT0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringInfrT0.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringInfrT0.Location = New System.Drawing.Point(648, 416)
		Me.CmdSqlStringInfrT0.Image = CType(resources.GetObject("CmdSqlStringInfrT0.Image"), System.Drawing.Image)
		Me.CmdSqlStringInfrT0.TabIndex = 27
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringInfrT0, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringInfrT0.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringInfrT0.CausesValidation = True
		Me.CmdSqlStringInfrT0.Enabled = True
		Me.CmdSqlStringInfrT0.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringInfrT0.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringInfrT0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringInfrT0.TabStop = True
		Me.CmdSqlStringInfrT0.Name = "CmdSqlStringInfrT0"
		Me.CmdSqlStringDocu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringDocu.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringDocu.Location = New System.Drawing.Point(646, 320)
		Me.CmdSqlStringDocu.Image = CType(resources.GetObject("CmdSqlStringDocu.Image"), System.Drawing.Image)
		Me.CmdSqlStringDocu.TabIndex = 24
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringDocu, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringDocu.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringDocu.CausesValidation = True
		Me.CmdSqlStringDocu.Enabled = True
		Me.CmdSqlStringDocu.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringDocu.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringDocu.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringDocu.TabStop = True
		Me.CmdSqlStringDocu.Name = "CmdSqlStringDocu"
		Me.txtDocu.AutoSize = False
		Me.txtDocu.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDocu.Size = New System.Drawing.Size(635, 37)
		Me.txtDocu.Location = New System.Drawing.Point(4, 316)
		Me.txtDocu.MultiLine = True
		Me.txtDocu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtDocu.TabIndex = 23
		Me.txtDocu.AcceptsReturn = True
		Me.txtDocu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDocu.BackColor = System.Drawing.SystemColors.Window
		Me.txtDocu.CausesValidation = True
		Me.txtDocu.Enabled = True
		Me.txtDocu.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDocu.HideSelection = True
		Me.txtDocu.ReadOnly = False
		Me.txtDocu.Maxlength = 0
		Me.txtDocu.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDocu.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDocu.TabStop = True
		Me.txtDocu.Visible = True
		Me.txtDocu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtDocu.Name = "txtDocu"
		Me.CmdSqlStringGene.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringGene.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringGene.Location = New System.Drawing.Point(646, 122)
		Me.CmdSqlStringGene.Image = CType(resources.GetObject("CmdSqlStringGene.Image"), System.Drawing.Image)
		Me.CmdSqlStringGene.TabIndex = 21
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringGene, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringGene.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringGene.CausesValidation = True
		Me.CmdSqlStringGene.Enabled = True
		Me.CmdSqlStringGene.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringGene.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringGene.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringGene.TabStop = True
		Me.CmdSqlStringGene.Name = "CmdSqlStringGene"
		Me.txtAccionesGene.AutoSize = False
		Me.txtAccionesGene.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAccionesGene.Size = New System.Drawing.Size(635, 37)
		Me.txtAccionesGene.Location = New System.Drawing.Point(4, 118)
		Me.txtAccionesGene.MultiLine = True
		Me.txtAccionesGene.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtAccionesGene.TabIndex = 20
		Me.txtAccionesGene.AcceptsReturn = True
		Me.txtAccionesGene.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAccionesGene.BackColor = System.Drawing.SystemColors.Window
		Me.txtAccionesGene.CausesValidation = True
		Me.txtAccionesGene.Enabled = True
		Me.txtAccionesGene.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAccionesGene.HideSelection = True
		Me.txtAccionesGene.ReadOnly = False
		Me.txtAccionesGene.Maxlength = 0
		Me.txtAccionesGene.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAccionesGene.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAccionesGene.TabStop = True
		Me.txtAccionesGene.Visible = True
		Me.txtAccionesGene.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtAccionesGene.Name = "txtAccionesGene"
		Me.txtMsj.AutoSize = False
		Me.txtMsj.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtMsj.Size = New System.Drawing.Size(635, 37)
		Me.txtMsj.Location = New System.Drawing.Point(4, 366)
		Me.txtMsj.MultiLine = True
		Me.txtMsj.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtMsj.TabIndex = 19
		Me.txtMsj.AcceptsReturn = True
		Me.txtMsj.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtMsj.BackColor = System.Drawing.SystemColors.Window
		Me.txtMsj.CausesValidation = True
		Me.txtMsj.Enabled = True
		Me.txtMsj.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMsj.HideSelection = True
		Me.txtMsj.ReadOnly = False
		Me.txtMsj.Maxlength = 0
		Me.txtMsj.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMsj.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMsj.TabStop = True
		Me.txtMsj.Visible = True
		Me.txtMsj.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtMsj.Name = "txtMsj"
		Me.CmdSqlStringMsj.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringMsj.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringMsj.Location = New System.Drawing.Point(646, 370)
		Me.CmdSqlStringMsj.Image = CType(resources.GetObject("CmdSqlStringMsj.Image"), System.Drawing.Image)
		Me.CmdSqlStringMsj.TabIndex = 17
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringMsj, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringMsj.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringMsj.CausesValidation = True
		Me.CmdSqlStringMsj.Enabled = True
		Me.CmdSqlStringMsj.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringMsj.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringMsj.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringMsj.TabStop = True
		Me.CmdSqlStringMsj.Name = "CmdSqlStringMsj"
		Me.txtConexionDB2.AutoSize = False
		Me.txtConexionDB2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtConexionDB2.Size = New System.Drawing.Size(635, 37)
		Me.txtConexionDB2.Location = New System.Drawing.Point(4, 18)
		Me.txtConexionDB2.MultiLine = True
		Me.txtConexionDB2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtConexionDB2.TabIndex = 16
		Me.txtConexionDB2.AcceptsReturn = True
		Me.txtConexionDB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtConexionDB2.BackColor = System.Drawing.SystemColors.Window
		Me.txtConexionDB2.CausesValidation = True
		Me.txtConexionDB2.Enabled = True
		Me.txtConexionDB2.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtConexionDB2.HideSelection = True
		Me.txtConexionDB2.ReadOnly = False
		Me.txtConexionDB2.Maxlength = 0
		Me.txtConexionDB2.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtConexionDB2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtConexionDB2.TabStop = True
		Me.txtConexionDB2.Visible = True
		Me.txtConexionDB2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtConexionDB2.Name = "txtConexionDB2"
		Me.txtConexionInfra.AutoSize = False
		Me.txtConexionInfra.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtConexionInfra.Size = New System.Drawing.Size(635, 37)
		Me.txtConexionInfra.Location = New System.Drawing.Point(4, 68)
		Me.txtConexionInfra.MultiLine = True
		Me.txtConexionInfra.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtConexionInfra.TabIndex = 15
		Me.txtConexionInfra.AcceptsReturn = True
		Me.txtConexionInfra.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtConexionInfra.BackColor = System.Drawing.SystemColors.Window
		Me.txtConexionInfra.CausesValidation = True
		Me.txtConexionInfra.Enabled = True
		Me.txtConexionInfra.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtConexionInfra.HideSelection = True
		Me.txtConexionInfra.ReadOnly = False
		Me.txtConexionInfra.Maxlength = 0
		Me.txtConexionInfra.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtConexionInfra.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtConexionInfra.TabStop = True
		Me.txtConexionInfra.Visible = True
		Me.txtConexionInfra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtConexionInfra.Name = "txtConexionInfra"
		Me.txtConexionHid.AutoSize = False
		Me.txtConexionHid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtConexionHid.Size = New System.Drawing.Size(635, 37)
		Me.txtConexionHid.Location = New System.Drawing.Point(4, 168)
		Me.txtConexionHid.MultiLine = True
		Me.txtConexionHid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtConexionHid.TabIndex = 14
		Me.txtConexionHid.AcceptsReturn = True
		Me.txtConexionHid.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtConexionHid.BackColor = System.Drawing.SystemColors.Window
		Me.txtConexionHid.CausesValidation = True
		Me.txtConexionHid.Enabled = True
		Me.txtConexionHid.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtConexionHid.HideSelection = True
		Me.txtConexionHid.ReadOnly = False
		Me.txtConexionHid.Maxlength = 0
		Me.txtConexionHid.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtConexionHid.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtConexionHid.TabStop = True
		Me.txtConexionHid.Visible = True
		Me.txtConexionHid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtConexionHid.Name = "txtConexionHid"
		Me.txtWord.AutoSize = False
		Me.txtWord.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtWord.Size = New System.Drawing.Size(635, 37)
		Me.txtWord.Location = New System.Drawing.Point(4, 268)
		Me.txtWord.MultiLine = True
		Me.txtWord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtWord.TabIndex = 13
		Me.txtWord.AcceptsReturn = True
		Me.txtWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtWord.BackColor = System.Drawing.SystemColors.Window
		Me.txtWord.CausesValidation = True
		Me.txtWord.Enabled = True
		Me.txtWord.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtWord.HideSelection = True
		Me.txtWord.ReadOnly = False
		Me.txtWord.Maxlength = 0
		Me.txtWord.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtWord.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtWord.TabStop = True
		Me.txtWord.Visible = True
		Me.txtWord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtWord.Name = "txtWord"
		Me.txtGestion.AutoSize = False
		Me.txtGestion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtGestion.Size = New System.Drawing.Size(635, 37)
		Me.txtGestion.Location = New System.Drawing.Point(4, 218)
		Me.txtGestion.MultiLine = True
		Me.txtGestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtGestion.TabIndex = 12
		Me.txtGestion.AcceptsReturn = True
		Me.txtGestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtGestion.BackColor = System.Drawing.SystemColors.Window
		Me.txtGestion.CausesValidation = True
		Me.txtGestion.Enabled = True
		Me.txtGestion.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtGestion.HideSelection = True
		Me.txtGestion.ReadOnly = False
		Me.txtGestion.Maxlength = 0
		Me.txtGestion.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtGestion.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtGestion.TabStop = True
		Me.txtGestion.Visible = True
		Me.txtGestion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtGestion.Name = "txtGestion"
		Me.CmdSqlStringGes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringGes.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringGes.Location = New System.Drawing.Point(646, 222)
		Me.CmdSqlStringGes.Image = CType(resources.GetObject("CmdSqlStringGes.Image"), System.Drawing.Image)
		Me.CmdSqlStringGes.TabIndex = 9
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringGes, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringGes.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringGes.CausesValidation = True
		Me.CmdSqlStringGes.Enabled = True
		Me.CmdSqlStringGes.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringGes.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringGes.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringGes.TabStop = True
		Me.CmdSqlStringGes.Name = "CmdSqlStringGes"
		Me.CmdSqlStringWord.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringWord.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringWord.Location = New System.Drawing.Point(646, 272)
		Me.CmdSqlStringWord.Image = CType(resources.GetObject("CmdSqlStringWord.Image"), System.Drawing.Image)
		Me.CmdSqlStringWord.TabIndex = 8
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringWord, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringWord.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringWord.CausesValidation = True
		Me.CmdSqlStringWord.Enabled = True
		Me.CmdSqlStringWord.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringWord.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringWord.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringWord.TabStop = True
		Me.CmdSqlStringWord.Name = "CmdSqlStringWord"
		Me.CmdSqlStringHid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringHid.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringHid.Location = New System.Drawing.Point(646, 172)
		Me.CmdSqlStringHid.Image = CType(resources.GetObject("CmdSqlStringHid.Image"), System.Drawing.Image)
		Me.CmdSqlStringHid.TabIndex = 7
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringHid, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringHid.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringHid.CausesValidation = True
		Me.CmdSqlStringHid.Enabled = True
		Me.CmdSqlStringHid.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringHid.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringHid.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringHid.TabStop = True
		Me.CmdSqlStringHid.Name = "CmdSqlStringHid"
		Me.CmdSqlStringInf.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringInf.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringInf.Location = New System.Drawing.Point(646, 72)
		Me.CmdSqlStringInf.Image = CType(resources.GetObject("CmdSqlStringInf.Image"), System.Drawing.Image)
		Me.CmdSqlStringInf.TabIndex = 5
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringInf, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringInf.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringInf.CausesValidation = True
		Me.CmdSqlStringInf.Enabled = True
		Me.CmdSqlStringInf.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringInf.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringInf.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringInf.TabStop = True
		Me.CmdSqlStringInf.Name = "CmdSqlStringInf"
		Me.CmdSqlStringDB2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSqlStringDB2.Size = New System.Drawing.Size(25, 29)
		Me.CmdSqlStringDB2.Location = New System.Drawing.Point(646, 22)
		Me.CmdSqlStringDB2.Image = CType(resources.GetObject("CmdSqlStringDB2.Image"), System.Drawing.Image)
		Me.CmdSqlStringDB2.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.CmdSqlStringDB2, "Generar cadena de conexión de proyecto")
		Me.CmdSqlStringDB2.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSqlStringDB2.CausesValidation = True
		Me.CmdSqlStringDB2.Enabled = True
		Me.CmdSqlStringDB2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSqlStringDB2.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSqlStringDB2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSqlStringDB2.TabStop = True
		Me.CmdSqlStringDB2.Name = "CmdSqlStringDB2"
		Me.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAceptar.Text = "Aceptar"
		Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAceptar.Size = New System.Drawing.Size(97, 26)
		Me.cmdAceptar.Location = New System.Drawing.Point(476, 464)
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
		Me.cmdCancelar.Text = "Cerrar"
		Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancelar.Size = New System.Drawing.Size(97, 26)
		Me.cmdCancelar.Location = New System.Drawing.Point(576, 464)
		Me.cmdCancelar.TabIndex = 0
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me._Label7_1.Text = "Cadena de Conexión a INFR"
		Me._Label7_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Label7_1.Size = New System.Drawing.Size(307, 17)
		Me._Label7_1.Location = New System.Drawing.Point(6, 400)
		Me._Label7_1.TabIndex = 26
		Me._Label7_1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._Label7_1.BackColor = System.Drawing.SystemColors.Control
		Me._Label7_1.Enabled = True
		Me._Label7_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Label7_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._Label7_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Label7_1.UseMnemonic = True
		Me._Label7_1.Visible = True
		Me._Label7_1.AutoSize = False
		Me._Label7_1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Label7_1.Name = "_Label7_1"
		Me.Label1.Text = "Cadena de Conexión a Acciones de DOCU"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(307, 17)
		Me.Label1.Location = New System.Drawing.Point(0, 304)
		Me.Label1.TabIndex = 25
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
		Me.Label8.Text = "Cadena de Conexión a Acciones Generales"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(307, 17)
		Me.Label8.Location = New System.Drawing.Point(6, 106)
		Me.Label8.TabIndex = 22
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label8.BackColor = System.Drawing.SystemColors.Control
		Me.Label8.Enabled = True
		Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label8.UseMnemonic = True
		Me.Label8.Visible = True
		Me.Label8.AutoSize = False
		Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label8.Name = "Label8"
		Me._Label7_0.Text = "Cadena de Conexión a Acciones de Mensajes"
		Me._Label7_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Label7_0.Size = New System.Drawing.Size(307, 17)
		Me._Label7_0.Location = New System.Drawing.Point(6, 354)
		Me._Label7_0.TabIndex = 18
		Me._Label7_0.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._Label7_0.BackColor = System.Drawing.SystemColors.Control
		Me._Label7_0.Enabled = True
		Me._Label7_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Label7_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._Label7_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Label7_0.UseMnemonic = True
		Me._Label7_0.Visible = True
		Me._Label7_0.AutoSize = False
		Me._Label7_0.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Label7_0.Name = "_Label7_0"
		Me.Label6.Text = "Cadena de Conexión a Acciones de Gestión"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(215, 17)
		Me.Label6.Location = New System.Drawing.Point(6, 206)
		Me.Label6.TabIndex = 11
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label6.BackColor = System.Drawing.SystemColors.Control
		Me.Label6.Enabled = True
		Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label6.UseMnemonic = True
		Me.Label6.Visible = True
		Me.Label6.AutoSize = False
		Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label6.Name = "Label6"
		Me.Label5.Text = "Cadena de Conexión a Acciones de Word"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(307, 17)
		Me.Label5.Location = New System.Drawing.Point(6, 256)
		Me.Label5.TabIndex = 10
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label5.BackColor = System.Drawing.SystemColors.Control
		Me.Label5.Enabled = True
		Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label5.UseMnemonic = True
		Me.Label5.Visible = True
		Me.Label5.AutoSize = False
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label5.Name = "Label5"
		Me.Label4.Text = "Cadena de Conexión Acciones Especificas"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(307, 17)
		Me.Label4.Location = New System.Drawing.Point(6, 156)
		Me.Label4.TabIndex = 6
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = False
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me.Label3.Text = "Cadena de Conexión General Aplicación"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(215, 17)
		Me.Label3.Location = New System.Drawing.Point(6, 56)
		Me.Label3.TabIndex = 4
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
		Me.Label2.Text = "Cadena de Conexión a DB2"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(243, 17)
		Me.Label2.Location = New System.Drawing.Point(6, 6)
		Me.Label2.TabIndex = 2
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
		Me.Label7.SetIndex(_Label7_1, CType(1, Short))
		Me.Label7.SetIndex(_Label7_0, CType(0, Short))
		CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(txtInfrT0)
		Me.Controls.Add(CmdSqlStringInfrT0)
		Me.Controls.Add(CmdSqlStringDocu)
		Me.Controls.Add(txtDocu)
		Me.Controls.Add(CmdSqlStringGene)
		Me.Controls.Add(txtAccionesGene)
		Me.Controls.Add(txtMsj)
		Me.Controls.Add(CmdSqlStringMsj)
		Me.Controls.Add(txtConexionDB2)
		Me.Controls.Add(txtConexionInfra)
		Me.Controls.Add(txtConexionHid)
		Me.Controls.Add(txtWord)
		Me.Controls.Add(txtGestion)
		Me.Controls.Add(CmdSqlStringGes)
		Me.Controls.Add(CmdSqlStringWord)
		Me.Controls.Add(CmdSqlStringHid)
		Me.Controls.Add(CmdSqlStringInf)
		Me.Controls.Add(CmdSqlStringDB2)
		Me.Controls.Add(cmdAceptar)
		Me.Controls.Add(cmdCancelar)
		Me.Controls.Add(_Label7_1)
		Me.Controls.Add(Label1)
		Me.Controls.Add(Label8)
		Me.Controls.Add(_Label7_0)
		Me.Controls.Add(Label6)
		Me.Controls.Add(Label5)
		Me.Controls.Add(Label4)
		Me.Controls.Add(Label3)
		Me.Controls.Add(Label2)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class