<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMDI
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
	Public WithEvents ImageList1 As System.Windows.Forms.PictureBox
    'Public WithEvents SysTray As AxSysTrayCtl.AxcSysTray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDI))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.PictureBox()
        CType(Me.ImageList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.BackColor = System.Drawing.SystemColors.Window
        Me.ImageList1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ImageList1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImageList1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ImageList1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ImageList1.Location = New System.Drawing.Point(0, 0)
        Me.ImageList1.Name = "ImageList1"
        Me.ImageList1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ImageList1.Size = New System.Drawing.Size(747, 32)
        Me.ImageList1.TabIndex = 0
        '
        'frmMDI
        '
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(747, 389)
        Me.Controls.Add(Me.ImageList1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmMDI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MDIForm1"
        CType(Me.ImageList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class