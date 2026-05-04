Option Strict Off
Option Explicit On
Friend Class frmConf
	Inherits System.Windows.Forms.Form
	Dim DataLinkCon As MSDASC.DataLinks
	Dim strproc As String
	
	'UPGRADE_WARNING: Event chkNuevaVersion.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkNuevaVersion_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkNuevaVersion.CheckStateChanged
		
		If Me.chkNuevaVersion.CheckState = 1 Then
			Me.txtObserva.Enabled = True
			Me.txtObserva.Focus()
		Else
			Me.txtObserva.Text = ""
			Me.txtObserva.Enabled = False
		End If
		
	End Sub
	
	'UPGRADE_WARNING: Event CmbBD.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub CmbBD_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmbBD.SelectedIndexChanged
		
		Me.txtConexion.Text = ReadIniFile(INIFile, CmbBD.Text, "Connection")
		
	End Sub
	
	Private Sub cmdAplicar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAplicar.Click
		If Me.txtConexion2.Text <> "" Then
			If MsgBox("Va a volcar el procedimiento en DOS bases de datos, una predeterminada y otra adicional." & Chr(13) & Chr(10) & "¿Desea continuar?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
				Exit Sub
			End If
		End If
		msGrabaDatos()
		msCargarDatos()
		lstProc_Click(lstProc, New System.EventArgs())
		
	End Sub
	
	Private Sub cmdCadenaConexion2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCadenaConexion2.Click
		On Error Resume Next
		
		Dim Cnx As String
		
		DataLinkCon = New MSDASC.DataLinks
		Cnx = txtConexion2.Text
		txtConexion2.Text = DataLinkCon.PromptNew
	End Sub
	
	Private Sub cmdCalendar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCalendar.Click
		
		formCargado = Me
		Me.Close()
		frmCalendar.ShowDialog()
		
	End Sub
	
	
	Private Sub cmdCancelar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelar.Click
		
		Dim sSQL As String
		sSQL = "DELETE PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
		'****************************
		mfExecute(conPasarela, sSQL)
		Me.Close()
		frmSelProc.Show()
		
	End Sub
	
	Private Sub cmdComenzar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdComenzar.Click
		
		On Error GoTo procErr
		
		Dim I As Integer
		Dim ConValidacion As ADODB.Connection
		Dim sSQL As String
		Dim rst As ADODB.Recordset
		
		ConValidacion = New ADODB.Connection
		For I = 1 To Me.lstProc.Items.Count
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If ComprobarConexion(ConValidacion, lstProc.Items.Item(I).SubItems.Item(5).Text) = False Then
				'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				MsgBox("La cadena de conexión del procedimiento " & Mid(lstProc.Items.Item(I).SubItems.Item(1).Text, 1, 6) & " no es correcta", MsgBoxStyle.Critical)
				GoTo procFin
			Else
				'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				strproc = Mid(Me.lstProc.Items.Item(I).SubItems.Item(1).Text, 1, lngNombre)
				sSQL = "SELECT * FROM PROCEDIMIENTOS WHERE CODEXTPR='" & strproc & "'"
				rst = mfRecordset(conInfra, sSQL)
				If rst.RecordCount > 0 Then
					sSQL = "UPDATE PROCEDIMIENTOS SET "
					'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					sSQL = sSQL & "BASEDATOS='" & lstProc.Items.Item(I).SubItems.Item(4).Text & "' "
					sSQL = sSQL & "WHERE CODEXTPR= '" & strproc & "'"
				Else
					sSQL = "INSERT INTO PROCEDIMIENTOS "
					sSQL = sSQL & " (CODEXTPR,BASEDATOS)"
					'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					sSQL = sSQL & "VALUES('" & strproc & "','" & lstProc.Items.Item(I).SubItems.Item(4).Text & "')"
				End If
				mfExecute(conInfra, sSQL)
			End If
		Next I
		'UPGRADE_NOTE: Object formCargado may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		formCargado = Nothing
		Me.Close()
		frmFechaActivacion.Show()
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		CierraCN(ConValidacion)
		'UPGRADE_NOTE: Object ConValidacion may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ConValidacion = Nothing
		CierraRS(rst)
		'UPGRADE_NOTE: Object rst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rst = Nothing
		
	End Sub
	
	
	Private Sub CmdSqlStringDB2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringDB2.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		DataLinkCon = New MSDASC.DataLinks
		Cnx = txtConexion.Text
		txtConexion.Text = DataLinkCon.PromptNew
		
	End Sub
	
	Private Sub frmConf_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 320
		mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
		bMinimizado = False
		frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
		frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
		bMinimizado = True
		frmMDI.Icon = Me.Icon
		frmMDI.Text = Me.Text
		Me.Top = 0
		Me.Left = 0
		msCargarDatos()
		frmMDI.SysTray.set_TrayTip("Configuración procedimientos")
		lstProc_Click(lstProc, New System.EventArgs())
		
	End Sub
	
	Private Sub msCargarDatos()
		
		On Error GoTo procErr
		
		Dim rst1 As ADODB.Recordset
		Dim sSQL As String
		
		lstProc.Items.Clear()
		'-------- solo apareceran los que tengan el estado finalizado a N  -- Aketza
		sSQL = "SELECT * FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "' and FINALIZADO ='N' ORDER BY NOMBRE_CM"
		'**********************************
		rst1 = mfRecordset(conPasarela, sSQL)
		While Not rst1.EOF
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			lstProc.Items.Insert(lstProc.Items.Count + 1, CStr(Trim(rst1.Fields("ID").Value)) & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
            'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            'lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(1, , Trim(rst1.Fields("NOMBRE_CM").Value) & "")
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("NOMBRE_CM").Value) & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("FECHA_ACTIVACION").Value) & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(IIf(Trim(rst1.Fields("NUEVA_VERSION").Value) = "1", "Si", "No") & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("BASEDATOS").Value) & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("CONEXION").Value) & "")
			'lstProc.ListItems(lstProc.ListItems.Count).ListSubItems.Add 6, , Trim(rst1("OBSERVACIONES")) & ""
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("CONEXION2").Value) & "")
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.ListSubItems method lstProc.ListItems.ListSubItems.Add was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            lstProc.Items.Item(lstProc.Items.Count).SubItems.Add(Trim(rst1.Fields("OBSERVACIONES").Value) & "")
			rst1.MoveNext()
		End While
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		CierraRS(rst1)
		'UPGRADE_NOTE: Object rst1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rst1 = Nothing
		
	End Sub
	
	Private Sub lstProc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstProc.Click
		
		On Error GoTo procErr
		
		msAsignaDatos()
		
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
procFin: 
		
	End Sub
	
	Private Sub msAsignaDatos()
		
		On Error GoTo procErr
		
		Dim I As Integer
		Dim blnFecDupl As Boolean
		Dim blnConDupl As Boolean
		Dim blnComenDupl As Boolean
		Dim j As Integer
		Dim blnVS As Boolean
		Dim strNumeroBBDD As String
		Dim strBBDD As String
		
		strNumeroBBDD = ReadIniFile(INIFile, "BBDD", "Numero")
		CmbBD.Items.Clear()
		CmbBD.Items.Add((""))
		For I = 1 To CShort(strNumeroBBDD)
			strBBDD = ReadIniFile(INIFile, "BBDD", "BBDD" & I)
			CmbBD.Items.Add((strBBDD))
		Next I
		blnFecDupl = False
		blnConDupl = False
		blnComenDupl = False
		j = 0
		For I = 1 To lstProc.Items.Count
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If lstProc.Items.Item(I).Selected = True Then
				j = j + 1
			End If
		Next I
		If j > 1 Then blnVS = True
		For I = 1 To lstProc.Items.Count
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If lstProc.Items.Item(I).Selected = True Then
				If blnVS = False Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					Me.mskFecha.Text = Me.lstProc.Items.Item(I).SubItems.Item(2).Text
				Else
					If Me.mskFecha.Text <> "__/__/____" Then
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						If Me.mskFecha.Text <> Me.lstProc.Items.Item(I).SubItems.Item(2).Text Then
							blnFecDupl = True
							Me.mskFecha.Text = "__/__/____"
						End If
					Else
						If blnFecDupl = False Then
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							Me.mskFecha.Text = Me.lstProc.Items.Item(I).SubItems.Item(2).Text
						End If
					End If
				End If
				If blnVS = False Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If Me.lstProc.Items.Item(I).SubItems.Item(3).Text = "No" Then
						Me.chkNuevaVersion.CheckState = System.Windows.Forms.CheckState.Unchecked
					Else
						Me.chkNuevaVersion.CheckState = System.Windows.Forms.CheckState.Checked
					End If
				Else
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If Me.lstProc.Items.Item(I).SubItems.Item(3).Text = "No" Then
						If Me.chkNuevaVersion.CheckState <> 0 Then
							Me.chkNuevaVersion.CheckState = System.Windows.Forms.CheckState.Indeterminate
						End If
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf Me.lstProc.Items.Item(I).SubItems.Item(3).Text = "Si" Then 
						If Me.chkNuevaVersion.CheckState <> 1 Then
							Me.chkNuevaVersion.CheckState = System.Windows.Forms.CheckState.Indeterminate
						End If
					End If
				End If
				'aketza 16/03/2007
				'         If blnVS = False Then
				'            Me.txtObserva = Me.lstProc.ListItems(I).ListSubItems(6)
				'            If Trim(Me.txtObserva) <> "" Then
				'               If Trim(Me.txtObserva) <> Me.lstProc.ListItems(I).ListSubItems(6) Then
				'                  blnComenDupl = True
				'                  Me.txtObserva = ""
				'               End If
				'            Else
				'               If blnComenDupl = False Then
				'                  Me.txtObserva = Me.lstProc.ListItems(I).ListSubItems(6)
				'               End If
				'            End If
				'         End If
				If blnVS = False Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					Me.txtObserva.Text = Me.lstProc.Items.Item(I).SubItems.Item(7).Text
					If Trim(Me.txtObserva.Text) <> "" Then
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						If Trim(Me.txtObserva.Text) <> Me.lstProc.Items.Item(I).SubItems.Item(7).Text Then
							blnComenDupl = True
							Me.txtObserva.Text = ""
						End If
					Else
						If blnComenDupl = False Then
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							Me.txtObserva.Text = Me.lstProc.Items.Item(I).SubItems.Item(7).Text
						End If
					End If
				End If
				'fin aketza
				If blnVS = False Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					Me.txtConexion.Text = Me.lstProc.Items.Item(I).SubItems.Item(5).Text
					If Trim(Me.txtConexion.Text) <> "" Then
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						If Trim(Me.txtConexion.Text) <> Me.lstProc.Items.Item(I).SubItems.Item(5).Text Then
							blnConDupl = True
							Me.txtConexion.Text = ""
						End If
					Else
						If blnConDupl = False Then
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							Me.txtConexion.Text = Me.lstProc.Items.Item(I).SubItems.Item(5).Text
						End If
					End If
				End If
				'aketza 16/03/2007
				If blnVS = False Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					Me.txtConexion2.Text = Me.lstProc.Items.Item(I).SubItems.Item(6).Text
					If Trim(Me.txtConexion2.Text) <> "" Then
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						If Trim(Me.txtConexion2.Text) <> Me.lstProc.Items.Item(I).SubItems.Item(6).Text Then
							blnConDupl = True
							Me.txtConexion2.Text = ""
						End If
					Else
						If blnConDupl = False Then
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							Me.txtConexion2.Text = Me.lstProc.Items.Item(I).SubItems.Item(6).Text
						End If
					End If
				End If
				'fin aketza
			End If
			'UPGRADE_WARNING: Lower bound of collection lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If lstProc.Items.Item(I).Selected = True Then
				'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If Me.lstProc.Items.Item(I).SubItems.Item(4).Text <> "" Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems().ListSubItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					CmbBD.Text = Me.lstProc.Items.Item(I).SubItems.Item(4).Text
				End If
			End If
		Next I
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		
	End Sub
	
	
	Private Sub msGrabaDatos()
		
		On Error GoTo procErr
		
		Dim strIDS As String
		Dim sSQL As String
		Dim I As Integer
		Dim strproc As String
		
		If mfValidaCampos = True Then
			For I = 1 To Me.lstProc.Items.Count
				'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If Me.lstProc.Items.Item(I).Selected = True Then
					'UPGRADE_WARNING: Lower bound of collection Me.lstProc.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					strIDS = strIDS & Me.lstProc.Items.Item(I).Text & ","
				End If
			Next I
			strIDS = Mid(strIDS, 1, Len(strIDS) - 1)
			sSQL = "UPDATE PROCESOS_PENDIENTES SET "
			sSQL = sSQL & "FECHA_ACTIVACION='" & Me.mskFecha.Text & "', "
			sSQL = sSQL & "NUEVA_VERSION='" & IIf(Me.chkNuevaVersion.CheckState = 1, 1, 0) & "', "
			sSQL = sSQL & "CONEXION='" & Me.txtConexion.Text & "', "
			sSQL = sSQL & "CONEXION2='" & Me.txtConexion2.Text & "', "
			sSQL = sSQL & "BASEDATOS='" & Me.CmbBD.Text & "', "
			sSQL = sSQL & "OBSERVACIONES='" & Me.txtObserva.Text & "' WHERE "
			sSQL = sSQL & "ID IN (" & strIDS & ")"
			mfExecute(conPasarela, sSQL)
		End If
		
		GoTo procFin
procErr: 
		MsgBox(Err.Description)
		Resume procFin
procFin: 
		
	End Sub
	
	
	
	Private Function mfValidaCampos() As Boolean
		
		On Error GoTo procErr
		
		Dim ConValidacion As ADODB.Connection
		
		ConValidacion = New ADODB.Connection
		mfValidaCampos = False
		If Me.mskFecha.Text = "__/__/____" Then
			MsgBox("Introduzca una fecha de activación.", MsgBoxStyle.Critical)
			Me.mskFecha.Focus()
			GoTo procFin
		End If
		If Not IsDate(Me.mskFecha.Text) Then
			MsgBox("Introduzca una fecha de activación correcta.", MsgBoxStyle.Critical)
			Me.mskFecha.Focus()
			GoTo procFin
		End If
		If Me.chkNuevaVersion.CheckState = 2 Then
			MsgBox("Valor ambiguo de activación de nueva versión.", MsgBoxStyle.Critical)
			Me.chkNuevaVersion.Focus()
			GoTo procFin
		End If
		If ComprobarConexion(ConValidacion, (Me.txtConexion).Text) = False Then
			MsgBox("La cadena de conexión no es correcta.", MsgBoxStyle.Critical)
			Me.txtConexion.Focus()
			GoTo procFin
		End If
		mfValidaCampos = True
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		'UPGRADE_NOTE: Object ConValidacion may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ConValidacion = Nothing
		
	End Function
	
	
	
	'UPGRADE_WARNING: Event chkVolcar.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkVolcar_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkVolcar.CheckStateChanged
		If Me.chkVolcar.CheckState = 1 Then
			Me.lblConexion2.Visible = True
			Me.txtConexion2.Visible = True
			Me.cmdCadenaConexion2.Visible = True
			Me.txtConexion2.Focus()
		Else
			Me.lblConexion2.Visible = False
			Me.txtConexion2.Text = ""
			Me.txtConexion2.Visible = False
			Me.cmdCadenaConexion2.Visible = False
		End If
	End Sub
End Class