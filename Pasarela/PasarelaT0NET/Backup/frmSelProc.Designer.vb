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
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSelProc))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.cboRamif = New System.Windows.Forms.ComboBox
		Me.cmdAceptar = New System.Windows.Forms.Button
		Me.cmdCancelar = New System.Windows.Forms.Button
		Me.chkRamif = New System.Windows.Forms.CheckBox
		Me.fgrProcs = New AxMSFlexGridLib.AxMSFlexGrid
        Me.Tree = New System.Windows.Forms.TreeView 'PictureBox
        Me.lstProcP = New System.Windows.Forms.ListView 'PictureBox
		Me.Images = New System.Windows.Forms.PictureBox
		Me._Label1_0 = New System.Windows.Forms.Label
		Me._Label1_2 = New System.Windows.Forms.Label
		Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.fgrProcs, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Text = "Selección de Procedimiento"
		Me.ClientSize = New System.Drawing.Size(619, 553)
		Me.Location = New System.Drawing.Point(0, 0)
		Me.Icon = CType(resources.GetObject("frmSelProc.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmSelProc"
		Me.cboRamif.Enabled = False
		Me.cboRamif.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboRamif.Size = New System.Drawing.Size(603, 22)
		Me.cboRamif.Location = New System.Drawing.Point(8, 34)
		Me.cboRamif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboRamif.TabIndex = 3
		Me.cboRamif.BackColor = System.Drawing.SystemColors.Window
		Me.cboRamif.CausesValidation = True
		Me.cboRamif.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboRamif.IntegralHeight = True
		Me.cboRamif.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboRamif.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboRamif.Sorted = False
		Me.cboRamif.TabStop = True
		Me.cboRamif.Visible = True
		Me.cboRamif.Name = "cboRamif"
		Me.cmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAceptar.Text = "Aceptar"
		Me.cmdAceptar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAceptar.Size = New System.Drawing.Size(97, 23)
		Me.cmdAceptar.Location = New System.Drawing.Point(414, 512)
		Me.cmdAceptar.TabIndex = 2
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
		Me.cmdCancelar.Location = New System.Drawing.Point(516, 512)
		Me.cmdCancelar.TabIndex = 1
		Me.cmdCancelar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancelar.CausesValidation = True
		Me.cmdCancelar.Enabled = True
		Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancelar.TabStop = True
		Me.cmdCancelar.Name = "cmdCancelar"
		Me.chkRamif.Text = "Buscar por ramificación"
		Me.chkRamif.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkRamif.Size = New System.Drawing.Size(145, 23)
		Me.chkRamif.Location = New System.Drawing.Point(10, 8)
		Me.chkRamif.TabIndex = 0
		Me.chkRamif.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkRamif.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkRamif.BackColor = System.Drawing.SystemColors.Control
		Me.chkRamif.CausesValidation = True
		Me.chkRamif.Enabled = True
		Me.chkRamif.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkRamif.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkRamif.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkRamif.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkRamif.TabStop = True
		Me.chkRamif.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.chkRamif.Visible = True
		Me.chkRamif.Name = "chkRamif"
		fgrProcs.OcxState = CType(resources.GetObject("fgrProcs.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fgrProcs.Size = New System.Drawing.Size(601, 169)
		Me.fgrProcs.Location = New System.Drawing.Point(10, 328)
		Me.fgrProcs.TabIndex = 5
		Me.fgrProcs.Name = "fgrProcs"
		Me.Tree.Size = New System.Drawing.Size(601, 207)
		Me.Tree.Location = New System.Drawing.Point(10, 88)
		Me.Tree.TabIndex = 7
		Me.Tree.Dock = System.Windows.Forms.DockStyle.None
		Me.Tree.BackColor = System.Drawing.SystemColors.Control
		Me.Tree.CausesValidation = True
		Me.Tree.Enabled = True
		Me.Tree.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Tree.Cursor = System.Windows.Forms.Cursors.Default
		Me.Tree.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Tree.TabStop = True
		Me.Tree.Visible = True
        'Me.Tree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.Tree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Tree.Name = "Tree"
		Me.lstProcP.BackColor = System.Drawing.SystemColors.Window
		Me.lstProcP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lstProcP.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lstProcP.Size = New System.Drawing.Size(601, 207)
		Me.lstProcP.Location = New System.Drawing.Point(10, 88)
		Me.lstProcP.TabIndex = 8
		Me.lstProcP.Dock = System.Windows.Forms.DockStyle.None
		Me.lstProcP.CausesValidation = True
		Me.lstProcP.Enabled = True
		Me.lstProcP.Cursor = System.Windows.Forms.Cursors.Default
		Me.lstProcP.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lstProcP.TabStop = True
		Me.lstProcP.Visible = True
        'Me.lstProcP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.lstProcP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lstProcP.Name = "lstProcP"
		Me.Images.BackColor = System.Drawing.SystemColors.Window
		Me.Images.Size = New System.Drawing.Size(80, 32)
		Me.Images.Location = New System.Drawing.Point(456, 0)
		Me.Images.TabIndex = 9
		Me.Images.Dock = System.Windows.Forms.DockStyle.None
		Me.Images.CausesValidation = True
		Me.Images.Enabled = True
		Me.Images.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Images.Cursor = System.Windows.Forms.Cursors.Default
		Me.Images.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Images.TabStop = True
		Me.Images.Visible = True
		Me.Images.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.Images.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Images.Name = "Images"
		Me._Label1_0.Text = "Procedimientos no disponibles:"
		Me._Label1_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Label1_0.Size = New System.Drawing.Size(163, 19)
		Me._Label1_0.Location = New System.Drawing.Point(10, 304)
		Me._Label1_0.TabIndex = 6
		Me._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._Label1_0.BackColor = System.Drawing.SystemColors.Control
		Me._Label1_0.Enabled = True
		Me._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Label1_0.UseMnemonic = True
		Me._Label1_0.Visible = True
		Me._Label1_0.AutoSize = False
		Me._Label1_0.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Label1_0.Name = "_Label1_0"
		Me._Label1_2.Text = "Procedimientos disponibles:"
		Me._Label1_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Label1_2.Size = New System.Drawing.Size(203, 19)
		Me._Label1_2.Location = New System.Drawing.Point(10, 64)
		Me._Label1_2.TabIndex = 4
		Me._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._Label1_2.BackColor = System.Drawing.SystemColors.Control
		Me._Label1_2.Enabled = True
		Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Label1_2.UseMnemonic = True
		Me._Label1_2.Visible = True
		Me._Label1_2.AutoSize = False
		Me._Label1_2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Label1_2.Name = "_Label1_2"
		Me.Label1.SetIndex(_Label1_0, CType(0, Short))
		Me.Label1.SetIndex(_Label1_2, CType(2, Short))
		CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fgrProcs, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(cboRamif)
		Me.Controls.Add(cmdAceptar)
		Me.Controls.Add(cmdCancelar)
		Me.Controls.Add(chkRamif)
		Me.Controls.Add(fgrProcs)
		Me.Controls.Add(Tree)
		Me.Controls.Add(lstProcP)
		Me.Controls.Add(Images)
		Me.Controls.Add(_Label1_0)
		Me.Controls.Add(_Label1_2)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class