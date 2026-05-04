Option Strict Off
Option Explicit On
Friend Class frmFechaActivacion
	Inherits System.Windows.Forms.Form
	
	Private Sub cmdAceptar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAceptar.Click
		
		On Error GoTo procErr
		
		If Me.optProg(1).Checked = True Then
			If Not IsDate(Me.mskFecha.Text) Or (CDate(Me.mskFecha.Text) < CDate(VB6.Format(Today, "dd/mm/yyyy"))) Then
				MsgBox("Introduzca una fecha correcta", MsgBoxStyle.Critical)
				Me.mskFecha.Focus()
				GoTo procFin
			Else
				If CInt(Mid(Me.mskHora.Text, 1, 2)) > 23 Or CInt(Mid(Me.mskHora.Text, 4, 2)) > 59 Then
					MsgBox("Introduzca una hora correcta", MsgBoxStyle.Critical)
					Me.mskHora.Focus()
					GoTo procFin
				Else
					If (CDate(Me.mskFecha.Text) = CDate(VB6.Format(Today, "dd/mm/yyyy"))) And (VB6.Format(Me.mskHora.Text, "hh:mm") < VB6.Format(Now, "hh:mm")) Then
						MsgBox("Introduzca una hora correcta", MsgBoxStyle.Critical)
						Me.mskHora.Focus()
						GoTo procFin
					Else
						strFechaProgramada = Me.mskFecha.Text & " " & Me.mskHora.Text & ":00"
						strFechaOriginal = Me.mskFecha.Text 'iniciamos la fecha original (aketza)
					End If
				End If
			End If
		Else
			strFechaProgramada = ""
			'Inicio Jonathan - 03/07/2012 - La fecha debe tener formato dd/mm/yyyy
			'strFechaOriginal = Mid(Now, 1, 10) ' si la fecha esta vacia le damos la actual (aketza)
			strFechaOriginal = Mid(VB6.Format(Now, "dd/mm/yyyy"), 1, 10) ' si la fecha esta vacia le damos la actual (aketza)
			'Fin Jonathan - 03/07/2012
		End If
		mfExecute(conPasarela, "INSERT INTO ERRORES_HISTORICO SELECT * FROM ERRORES WHERE USERID='" & strUserId & "'")
		mfExecute(conPasarela, "DELETE FROM ERRORES WHERE USERID='" & strUserId & "'")
		'*********************
		'UPGRADE_NOTE: Object formCargado may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		formCargado = Nothing
		Me.Close()
		frmProceso.Show()
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		
	End Sub
	
	Private Sub cmdCalendar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCalendar.Click
		
		formCargado = Me
		frmCalendar.ShowDialog()
		
	End Sub
	
	Private Sub cmdCancelar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelar.Click
		
		'para q recupere el tamaño normal de frmConf (aketza)
		frmMDI.Width = VB6.TwipsToPixelsX(10785 + 100)
		frmMDI.Height = VB6.TwipsToPixelsY(6990 + 320)
		Me.Close()
		frmConf.Show()
		
	End Sub
	
	Private Sub frmFechaActivacion_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		Me.Top = 0
		Me.Left = 0
		mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
		mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 380
		bMinimizado = False
		frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
		frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
		bMinimizado = True
		frmMDI.Icon = Me.Icon
		frmMDI.Text = Me.Text
		frmMDI.SysTray.set_TrayTip("Programar")
		
	End Sub
	
	'UPGRADE_WARNING: Event optProg.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub optProg_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optProg.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = optProg.GetIndex(eventSender)
			
			If Index = 1 Then
				Me.mskFecha.Enabled = True
				Me.mskFecha.Text = VB6.Format(Today, "dd/mm/yyyy")
				Me.cmdCalendar.Enabled = True
				Me.mskHora.Enabled = True
				Me.mskHora.Text = VB6.Format(Now, "hh:mm")
			Else
				Me.mskFecha.Enabled = False
				Me.mskFecha.Text = "__/__/____"
				Me.cmdCalendar.Enabled = False
				Me.mskHora.Enabled = False
				Me.mskHora.Text = "__:__"
			End If
			
		End If
	End Sub
End Class