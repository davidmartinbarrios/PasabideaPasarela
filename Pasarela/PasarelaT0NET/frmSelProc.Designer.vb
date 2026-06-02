<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSelProc
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
	Public WithEvents cboRamif As System.Windows.Forms.ComboBox
	Public WithEvents cmdAceptar As System.Windows.Forms.Button
	Public WithEvents cmdCancelar As System.Windows.Forms.Button
	Public WithEvents chkRamif As System.Windows.Forms.CheckBox
	Public WithEvents fgrProcs As AxMSFlexGridLib.AxMSFlexGrid
    Public WithEvents Tree As System.Windows.Forms.TreeView '.PictureBox
    Public WithEvents lstProcP As System.Windows.Forms.ListView 'System.Windows.Forms.PictureBox
	Public WithEvents Images As System.Windows.Forms.PictureBox
	Public WithEvents _Label1_0 As System.Windows.Forms.Label
	Public WithEvents _Label1_2 As System.Windows.Forms.Label
	Public WithEvents Label1 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelProc))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboRamif = New System.Windows.Forms.ComboBox()
        Me.cmdAceptar = New System.Windows.Forms.Button()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me.chkRamif = New System.Windows.Forms.CheckBox()
        Me.fgrProcs = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.Tree = New System.Windows.Forms.TreeView()
        Me.lstProcP = New System.Windows.Forms.ListView()
        Me.Images = New System.Windows.Forms.PictureBox()
        Me._Label1_0 = New System.Windows.Forms.Label()
        Me._Label1_2 = New System.Windows.Forms.Label()
        CType(Me.fgrProcs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Images, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboRamif
        '
        Me.cboRamif.BackColor = System.Drawing.SystemColors.Window
        Me.cboRamif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRamif.Enabled = False
        Me.cboRamif.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRamif.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRamif.Location = New System.Drawing.Point(8, 34)
        Me.cboRamif.Name = "cboRamif"
        Me.cboRamif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRamif.Size = New System.Drawing.Size(603, 22)
        Me.cboRamif.TabIndex = 3
        '
        'cmdAceptar
        '
        Me.cmdAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAceptar.Location = New System.Drawing.Point(414, 512)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAceptar.Size = New System.Drawing.Size(97, 23)
        Me.cmdAceptar.TabIndex = 2
        Me.cmdAceptar.Text = "Aceptar"
        Me.cmdAceptar.UseVisualStyleBackColor = False
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancelar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelar.Location = New System.Drawing.Point(516, 512)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelar.Size = New System.Drawing.Size(97, 23)
        Me.cmdCancelar.TabIndex = 1
        Me.cmdCancelar.Text = "Cancelar"
        Me.cmdCancelar.UseVisualStyleBackColor = False
        '
        'chkRamif
        '
        Me.chkRamif.BackColor = System.Drawing.SystemColors.Control
        Me.chkRamif.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRamif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRamif.Location = New System.Drawing.Point(10, 8)
        Me.chkRamif.Name = "chkRamif"
        Me.chkRamif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRamif.Size = New System.Drawing.Size(145, 23)
        Me.chkRamif.TabIndex = 0
        Me.chkRamif.Text = "Buscar por ramificación"
        Me.chkRamif.UseVisualStyleBackColor = False
        '
        'fgrProcs
        '
        Me.fgrProcs.Location = New System.Drawing.Point(10, 328)
        Me.fgrProcs.Name = "fgrProcs"
        Me.fgrProcs.OcxState = CType(resources.GetObject("fgrProcs.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fgrProcs.Size = New System.Drawing.Size(601, 169)
        Me.fgrProcs.TabIndex = 5
        '
        'Tree
        '
        Me.Tree.BackColor = System.Drawing.SystemColors.Control
        Me.Tree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Tree.Location = New System.Drawing.Point(10, 88)
        Me.Tree.Name = "Tree"
        Me.Tree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Tree.Size = New System.Drawing.Size(601, 207)
        Me.Tree.TabIndex = 7
        '
        'lstProcP
        '
        Me.lstProcP.BackColor = System.Drawing.SystemColors.Window
        Me.lstProcP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProcP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstProcP.Location = New System.Drawing.Point(10, 88)
        Me.lstProcP.Name = "lstProcP"
        Me.lstProcP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstProcP.Size = New System.Drawing.Size(601, 207)
        Me.lstProcP.TabIndex = 8
        Me.lstProcP.UseCompatibleStateImageBehavior = False
        '
        'Images
        '
        Me.Images.BackColor = System.Drawing.SystemColors.Window
        Me.Images.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Images.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Images.Location = New System.Drawing.Point(456, 0)
        Me.Images.Name = "Images"
        Me.Images.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Images.Size = New System.Drawing.Size(80, 32)
        Me.Images.TabIndex = 9
        '
        '_Label1_0
        '
        Me._Label1_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_0.Location = New System.Drawing.Point(10, 304)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(163, 19)
        Me._Label1_0.TabIndex = 6
        Me._Label1_0.Text = "Procedimientos no disponibles:"
        '
        '_Label1_2
        '
        Me._Label1_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_2.Location = New System.Drawing.Point(10, 64)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(203, 19)
        Me._Label1_2.TabIndex = 4
        Me._Label1_2.Text = "Procedimientos disponibles:"
        '
        'frmSelProc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(619, 553)
        Me.Controls.Add(Me.cboRamif)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.chkRamif)
        Me.Controls.Add(Me.fgrProcs)
        Me.Controls.Add(Me.Tree)
        Me.Controls.Add(Me.lstProcP)
        Me.Controls.Add(Me.Images)
        Me.Controls.Add(Me._Label1_0)
        Me.Controls.Add(Me._Label1_2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelProc"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Selección de Procedimiento"
        AddHandler Load, AddressOf Me.frmSelProc_Load
        AddHandler Shown, AddressOf Me.frmSelProc_Shown
        CType(Me.fgrProcs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Images, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class