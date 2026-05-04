Option Strict Off
Option Explicit On
Friend Class frmCalendar
	Inherits System.Windows.Forms.Form
	
	Private Sub Calendar1_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Calendar1.DblClick
		
		On Error GoTo procErr
		
		frmFechaActivacion.mskFecha.Text = Calendar1.Value
		Me.Close()
		GoTo procFin
		
procErr: 
		logError("frmCalendar.Calendar1_DblClick - " & Err.Description)
		Resume procFin
		
procFin: 
		
	End Sub
	
	
	Private Sub frmCalendar_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		If frmFechaActivacion.mskFecha.Text <> "__/__/____" Then
			Calendar1.Value = frmFechaActivacion.mskFecha
		Else
			Calendar1.Value = Today
		End If
		
	End Sub
End Class