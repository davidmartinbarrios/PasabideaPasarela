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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConexion))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSqlStringInfrT0 = New System.Windows.Forms.Button()
        Me.CmdSqlStringDocu = New System.Windows.Forms.Button()
        Me.CmdSqlStringGene = New System.Windows.Forms.Button()
        Me.CmdSqlStringMsj = New System.Windows.Forms.Button()
        Me.CmdSqlStringGes = New System.Windows.Forms.Button()
        Me.CmdSqlStringWord = New System.Windows.Forms.Button()
        Me.CmdSqlStringHid = New System.Windows.Forms.Button()
        Me.CmdSqlStringInf = New System.Windows.Forms.Button()
        Me.CmdSqlStringDB2 = New System.Windows.Forms.Button()
        Me.txtInfrT0 = New System.Windows.Forms.TextBox()
        Me.txtDocu = New System.Windows.Forms.TextBox()
        Me.txtAccionesGene = New System.Windows.Forms.TextBox()
        Me.txtMsj = New System.Windows.Forms.TextBox()
        Me.txtConexionDB2 = New System.Windows.Forms.TextBox()
        Me.txtConexionInfra = New System.Windows.Forms.TextBox()
        Me.txtConexionHid = New System.Windows.Forms.TextBox()
        Me.txtWord = New System.Windows.Forms.TextBox()
        Me.txtGestion = New System.Windows.Forms.TextBox()
        Me.cmdAceptar = New System.Windows.Forms.Button()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me._Label7_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me._Label7_0 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CmdSqlStringInfrT0
        '
        Me.CmdSqlStringInfrT0.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringInfrT0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringInfrT0.Image = CType(resources.GetObject("CmdSqlStringInfrT0.Image"), System.Drawing.Image)
        Me.CmdSqlStringInfrT0.Location = New System.Drawing.Point(648, 416)
        Me.CmdSqlStringInfrT0.Name = "CmdSqlStringInfrT0"
        Me.CmdSqlStringInfrT0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringInfrT0.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringInfrT0.TabIndex = 27
        Me.CmdSqlStringInfrT0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringInfrT0, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringInfrT0.UseVisualStyleBackColor = False
        '
        'CmdSqlStringDocu
        '
        Me.CmdSqlStringDocu.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringDocu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringDocu.Image = CType(resources.GetObject("CmdSqlStringDocu.Image"), System.Drawing.Image)
        Me.CmdSqlStringDocu.Location = New System.Drawing.Point(646, 320)
        Me.CmdSqlStringDocu.Name = "CmdSqlStringDocu"
        Me.CmdSqlStringDocu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringDocu.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringDocu.TabIndex = 24
        Me.CmdSqlStringDocu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringDocu, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringDocu.UseVisualStyleBackColor = False
        '
        'CmdSqlStringGene
        '
        Me.CmdSqlStringGene.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringGene.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringGene.Image = CType(resources.GetObject("CmdSqlStringGene.Image"), System.Drawing.Image)
        Me.CmdSqlStringGene.Location = New System.Drawing.Point(646, 122)
        Me.CmdSqlStringGene.Name = "CmdSqlStringGene"
        Me.CmdSqlStringGene.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringGene.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringGene.TabIndex = 21
        Me.CmdSqlStringGene.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringGene, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringGene.UseVisualStyleBackColor = False
        '
        'CmdSqlStringMsj
        '
        Me.CmdSqlStringMsj.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringMsj.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringMsj.Image = CType(resources.GetObject("CmdSqlStringMsj.Image"), System.Drawing.Image)
        Me.CmdSqlStringMsj.Location = New System.Drawing.Point(646, 370)
        Me.CmdSqlStringMsj.Name = "CmdSqlStringMsj"
        Me.CmdSqlStringMsj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringMsj.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringMsj.TabIndex = 17
        Me.CmdSqlStringMsj.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringMsj, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringMsj.UseVisualStyleBackColor = False
        '
        'CmdSqlStringGes
        '
        Me.CmdSqlStringGes.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringGes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringGes.Image = CType(resources.GetObject("CmdSqlStringGes.Image"), System.Drawing.Image)
        Me.CmdSqlStringGes.Location = New System.Drawing.Point(646, 222)
        Me.CmdSqlStringGes.Name = "CmdSqlStringGes"
        Me.CmdSqlStringGes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringGes.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringGes.TabIndex = 9
        Me.CmdSqlStringGes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringGes, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringGes.UseVisualStyleBackColor = False
        '
        'CmdSqlStringWord
        '
        Me.CmdSqlStringWord.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringWord.Image = CType(resources.GetObject("CmdSqlStringWord.Image"), System.Drawing.Image)
        Me.CmdSqlStringWord.Location = New System.Drawing.Point(646, 272)
        Me.CmdSqlStringWord.Name = "CmdSqlStringWord"
        Me.CmdSqlStringWord.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringWord.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringWord.TabIndex = 8
        Me.CmdSqlStringWord.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringWord, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringWord.UseVisualStyleBackColor = False
        '
        'CmdSqlStringHid
        '
        Me.CmdSqlStringHid.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringHid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringHid.Image = CType(resources.GetObject("CmdSqlStringHid.Image"), System.Drawing.Image)
        Me.CmdSqlStringHid.Location = New System.Drawing.Point(646, 172)
        Me.CmdSqlStringHid.Name = "CmdSqlStringHid"
        Me.CmdSqlStringHid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringHid.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringHid.TabIndex = 7
        Me.CmdSqlStringHid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringHid, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringHid.UseVisualStyleBackColor = False
        '
        'CmdSqlStringInf
        '
        Me.CmdSqlStringInf.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringInf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringInf.Image = CType(resources.GetObject("CmdSqlStringInf.Image"), System.Drawing.Image)
        Me.CmdSqlStringInf.Location = New System.Drawing.Point(646, 72)
        Me.CmdSqlStringInf.Name = "CmdSqlStringInf"
        Me.CmdSqlStringInf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringInf.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringInf.TabIndex = 5
        Me.CmdSqlStringInf.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringInf, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringInf.UseVisualStyleBackColor = False
        '
        'CmdSqlStringDB2
        '
        Me.CmdSqlStringDB2.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSqlStringDB2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSqlStringDB2.Image = CType(resources.GetObject("CmdSqlStringDB2.Image"), System.Drawing.Image)
        Me.CmdSqlStringDB2.Location = New System.Drawing.Point(646, 22)
        Me.CmdSqlStringDB2.Name = "CmdSqlStringDB2"
        Me.CmdSqlStringDB2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSqlStringDB2.Size = New System.Drawing.Size(25, 29)
        Me.CmdSqlStringDB2.TabIndex = 3
        Me.CmdSqlStringDB2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSqlStringDB2, "Generar cadena de conexión de proyecto")
        Me.CmdSqlStringDB2.UseVisualStyleBackColor = False
        '
        'txtInfrT0
        '
        Me.txtInfrT0.AcceptsReturn = True
        Me.txtInfrT0.BackColor = System.Drawing.SystemColors.Window
        Me.txtInfrT0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInfrT0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInfrT0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInfrT0.Location = New System.Drawing.Point(4, 416)
        Me.txtInfrT0.MaxLength = 0
        Me.txtInfrT0.Multiline = True
        Me.txtInfrT0.Name = "txtInfrT0"
        Me.txtInfrT0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInfrT0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfrT0.Size = New System.Drawing.Size(636, 37)
        Me.txtInfrT0.TabIndex = 28
        '
        'txtDocu
        '
        Me.txtDocu.AcceptsReturn = True
        Me.txtDocu.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocu.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocu.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocu.Location = New System.Drawing.Point(4, 316)
        Me.txtDocu.MaxLength = 0
        Me.txtDocu.Multiline = True
        Me.txtDocu.Name = "txtDocu"
        Me.txtDocu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDocu.Size = New System.Drawing.Size(635, 37)
        Me.txtDocu.TabIndex = 23
        '
        'txtAccionesGene
        '
        Me.txtAccionesGene.AcceptsReturn = True
        Me.txtAccionesGene.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccionesGene.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccionesGene.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccionesGene.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccionesGene.Location = New System.Drawing.Point(4, 118)
        Me.txtAccionesGene.MaxLength = 0
        Me.txtAccionesGene.Multiline = True
        Me.txtAccionesGene.Name = "txtAccionesGene"
        Me.txtAccionesGene.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccionesGene.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAccionesGene.Size = New System.Drawing.Size(635, 37)
        Me.txtAccionesGene.TabIndex = 20
        '
        'txtMsj
        '
        Me.txtMsj.AcceptsReturn = True
        Me.txtMsj.BackColor = System.Drawing.SystemColors.Window
        Me.txtMsj.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMsj.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMsj.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMsj.Location = New System.Drawing.Point(4, 366)
        Me.txtMsj.MaxLength = 0
        Me.txtMsj.Multiline = True
        Me.txtMsj.Name = "txtMsj"
        Me.txtMsj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMsj.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsj.Size = New System.Drawing.Size(635, 37)
        Me.txtMsj.TabIndex = 19
        '
        'txtConexionDB2
        '
        Me.txtConexionDB2.AcceptsReturn = True
        Me.txtConexionDB2.BackColor = System.Drawing.SystemColors.Window
        Me.txtConexionDB2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConexionDB2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConexionDB2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConexionDB2.Location = New System.Drawing.Point(4, 18)
        Me.txtConexionDB2.MaxLength = 0
        Me.txtConexionDB2.Multiline = True
        Me.txtConexionDB2.Name = "txtConexionDB2"
        Me.txtConexionDB2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConexionDB2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConexionDB2.Size = New System.Drawing.Size(635, 37)
        Me.txtConexionDB2.TabIndex = 16
        '
        'txtConexionInfra
        '
        Me.txtConexionInfra.AcceptsReturn = True
        Me.txtConexionInfra.BackColor = System.Drawing.SystemColors.Window
        Me.txtConexionInfra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConexionInfra.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConexionInfra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConexionInfra.Location = New System.Drawing.Point(4, 68)
        Me.txtConexionInfra.MaxLength = 0
        Me.txtConexionInfra.Multiline = True
        Me.txtConexionInfra.Name = "txtConexionInfra"
        Me.txtConexionInfra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConexionInfra.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConexionInfra.Size = New System.Drawing.Size(635, 37)
        Me.txtConexionInfra.TabIndex = 15
        '
        'txtConexionHid
        '
        Me.txtConexionHid.AcceptsReturn = True
        Me.txtConexionHid.BackColor = System.Drawing.SystemColors.Window
        Me.txtConexionHid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConexionHid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConexionHid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConexionHid.Location = New System.Drawing.Point(4, 168)
        Me.txtConexionHid.MaxLength = 0
        Me.txtConexionHid.Multiline = True
        Me.txtConexionHid.Name = "txtConexionHid"
        Me.txtConexionHid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConexionHid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConexionHid.Size = New System.Drawing.Size(635, 37)
        Me.txtConexionHid.TabIndex = 14
        '
        'txtWord
        '
        Me.txtWord.AcceptsReturn = True
        Me.txtWord.BackColor = System.Drawing.SystemColors.Window
        Me.txtWord.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWord.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWord.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWord.Location = New System.Drawing.Point(4, 268)
        Me.txtWord.MaxLength = 0
        Me.txtWord.Multiline = True
        Me.txtWord.Name = "txtWord"
        Me.txtWord.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWord.Size = New System.Drawing.Size(635, 37)
        Me.txtWord.TabIndex = 13
        '
        'txtGestion
        '
        Me.txtGestion.AcceptsReturn = True
        Me.txtGestion.BackColor = System.Drawing.SystemColors.Window
        Me.txtGestion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGestion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGestion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGestion.Location = New System.Drawing.Point(4, 218)
        Me.txtGestion.MaxLength = 0
        Me.txtGestion.Multiline = True
        Me.txtGestion.Name = "txtGestion"
        Me.txtGestion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGestion.Size = New System.Drawing.Size(635, 37)
        Me.txtGestion.TabIndex = 12
        '
        'cmdAceptar
        '
        Me.cmdAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAceptar.Location = New System.Drawing.Point(476, 464)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAceptar.Size = New System.Drawing.Size(97, 26)
        Me.cmdAceptar.TabIndex = 1
        Me.cmdAceptar.Text = "Aceptar"
        Me.cmdAceptar.UseVisualStyleBackColor = False
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelar.Location = New System.Drawing.Point(576, 464)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelar.Size = New System.Drawing.Size(97, 26)
        Me.cmdCancelar.TabIndex = 0
        Me.cmdCancelar.Text = "Cerrar"
        Me.cmdCancelar.UseVisualStyleBackColor = False
        '
        '_Label7_1
        '
        Me._Label7_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label7_1.Location = New System.Drawing.Point(6, 400)
        Me._Label7_1.Name = "_Label7_1"
        Me._Label7_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_1.Size = New System.Drawing.Size(307, 17)
        Me._Label7_1.TabIndex = 26
        Me._Label7_1.Text = "Cadena de Conexión a INFR"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(0, 304)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(307, 17)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Cadena de Conexión a Acciones de DOCU"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 106)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(307, 17)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Cadena de Conexión a Acciones Generales"
        '
        '_Label7_0
        '
        Me._Label7_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label7_0.Location = New System.Drawing.Point(6, 354)
        Me._Label7_0.Name = "_Label7_0"
        Me._Label7_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_0.Size = New System.Drawing.Size(307, 17)
        Me._Label7_0.TabIndex = 18
        Me._Label7_0.Text = "Cadena de Conexión a Acciones de Mensajes"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(215, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Cadena de Conexión a Acciones de Gestión"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 256)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(307, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Cadena de Conexión a Acciones de Word"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(307, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Cadena de Conexión Acciones Especificas"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(215, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Cadena de Conexión General Aplicación"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(243, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Cadena de Conexión a DB2"
        '
        'frmConexion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(685, 494)
        Me.Controls.Add(Me.txtInfrT0)
        Me.Controls.Add(Me.CmdSqlStringInfrT0)
        Me.Controls.Add(Me.CmdSqlStringDocu)
        Me.Controls.Add(Me.txtDocu)
        Me.Controls.Add(Me.CmdSqlStringGene)
        Me.Controls.Add(Me.txtAccionesGene)
        Me.Controls.Add(Me.txtMsj)
        Me.Controls.Add(Me.CmdSqlStringMsj)
        Me.Controls.Add(Me.txtConexionDB2)
        Me.Controls.Add(Me.txtConexionInfra)
        Me.Controls.Add(Me.txtConexionHid)
        Me.Controls.Add(Me.txtWord)
        Me.Controls.Add(Me.txtGestion)
        Me.Controls.Add(Me.CmdSqlStringGes)
        Me.Controls.Add(Me.CmdSqlStringWord)
        Me.Controls.Add(Me.CmdSqlStringHid)
        Me.Controls.Add(Me.CmdSqlStringInf)
        Me.Controls.Add(Me.CmdSqlStringDB2)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me._Label7_1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me._Label7_0)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmConexion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Conexión a Base de Datos"
        AddHandler Shown, AddressOf Me.frmConexion_Shown
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class