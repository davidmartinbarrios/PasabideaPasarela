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
	Public WithEvents SysTray As AxSysTrayCtl.AxcSysTray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMDI))
		Me.IsMDIContainer = True
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.ImageList1 = New System.Windows.Forms.PictureBox
		Me.SysTray = New AxSysTrayCtl.AxcSysTray
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SysTray, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.BackColor = System.Drawing.SystemColors.AppWorkspace
		Me.Text = "MDIForm1"
		Me.ClientSize = New System.Drawing.Size(317, 213)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.Icon = CType(resources.GetObject("frmMDI.Icon"), System.Drawing.Icon)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Enabled = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmMDI"
		Me.ImageList1.Dock = System.Windows.Forms.DockStyle.Top
		Me.ImageList1.BackColor = System.Drawing.SystemColors.Window
		Me.ImageList1.Size = New System.Drawing.Size(317, 32)
		Me.ImageList1.Location = New System.Drawing.Point(0, 0)
		Me.ImageList1.TabIndex = 0
		Me.ImageList1.CausesValidation = True
		Me.ImageList1.Enabled = True
		Me.ImageList1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ImageList1.Cursor = System.Windows.Forms.Cursors.Default
		Me.ImageList1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ImageList1.TabStop = True
		Me.ImageList1.Visible = True
		Me.ImageList1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.ImageList1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.ImageList1.Name = "ImageList1"
		SysTray.OcxState = CType(resources.GetObject("SysTray.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SysTray.Location = New System.Drawing.Point(176, 48)
		Me.SysTray.Name = "SysTray"
		Me.Controls.Add(ImageList1)
		Me.Controls.Add(SysTray)
		CType(Me.SysTray, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class