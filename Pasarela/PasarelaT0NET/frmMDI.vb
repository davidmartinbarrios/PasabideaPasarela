Option Strict Off
Option Explicit On
Friend Class frmMDI
	Inherits System.Windows.Forms.Form
	Dim blnSystray As Boolean
	
	Private Sub frmMDI_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		
		Dim hWnd As Object
		Dim hMenu As Object
		Dim Success As Object
		Dim I As Object
		
		'UPGRADE_WARNING: Couldn't resolve default property of object hWnd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		hWnd = Me.Handle.ToInt32
		'UPGRADE_WARNING: Couldn't resolve default property of object hWnd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object hMenu. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		hMenu = GetSystemMenu(hWnd, 0)
		
		'Usa esto para quitar los menús que te interesen:
		'UPGRADE_WARNING: Couldn't resolve default property of object hMenu. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object Success. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Success = DeleteMenu(hMenu, SC_SIZE, MF_BYCOMMAND)
		'UPGRADE_WARNING: Couldn't resolve default property of object hMenu. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object Success. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Success = DeleteMenu(hMenu, SC_CLOSE, MF_BYCOMMAND)
		'UPGRADE_WARNING: Couldn't resolve default property of object hMenu. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object Success. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Success = DeleteMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND)
		Me.SysTray.set_InTray(True)
		'UPGRADE_WARNING: Couldn't resolve default property of object Me.ImageList1.ListImages. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'Me.SysTray.set_TrayIcon(Me.ImageList1.ListImages(2).Picture)
        Me.SysTray.set_TrayIcon(Me.ImageList1.Image)
	End Sub
	
	'UPGRADE_WARNING: Event frmMDI.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmMDI_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		
		'aketza minimizado
		If bMinimizado = False Then
			Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		End If
		'If Me.WindowState <> vbMinimized Then
		If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized And Me.WindowState <> System.Windows.Forms.FormWindowState.Maximized Then
			'fin aketza
			
			'Me.Width = mdiAncho
			'Me.Height = mdiAlto
			Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
			Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
		Else
			If Me.Visible = True Then
				If blnSystray = False Then
					Me.Visible = False
					Me.SysTray.set_InTray(True)
				Else
					Me.WindowState = System.Windows.Forms.FormWindowState.Normal
					Me.Visible = True
					blnSystray = False
				End If
			Else
				Me.WindowState = System.Windows.Forms.FormWindowState.Normal
				Me.Visible = True
				Me.SysTray.set_InTray(True)
			End If
			
			
		End If
	End Sub
	
	Private Sub SysTray_MouseDblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxSysTrayCtl.__cSysTray_MouseDblClickEvent) Handles SysTray.MouseDblClick
		'/*** REV 610 ***/
		'FrmVisor.Show
		On Error Resume Next
		''    frmMDI.Show
		''    frmMDI.ZOrder 0
		''    frmMDI.WindowState = 0
		'/*** REV 610 FIN ***/
	End Sub
	
	Private Sub SysTray_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxSysTrayCtl.__cSysTray_MouseDownEvent) Handles SysTray.MouseDownEvent
		
		'PopupMenu mnuAcciones
		blnSystray = True
		Me.Visible = True
		
	End Sub
End Class