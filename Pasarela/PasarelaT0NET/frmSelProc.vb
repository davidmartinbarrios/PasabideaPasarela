Option Strict Off
Option Explicit On
Friend Class frmSelProc
	Inherits System.Windows.Forms.Form
	Dim rstProc2 As ADODB.Recordset
	Dim arrProc() As Object
    Dim arrProc2(,) As Object
    'Dim arrRami() As Object
    Dim arrRami(1, -1) As Object
	Dim arrlstP() As Object
	Dim colID As Collection
	Dim colNoProc As Collection
	Dim contador As Integer
	Dim bTree As Boolean
	Dim arrProcs As Object
    Dim colProcedim As Collection

    Dim tvwChild As System.Windows.Forms.TreeNode

	
	'UPGRADE_WARNING: Event cboRamif.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub cboRamif_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRamif.SelectedIndexChanged
		
		On Error Resume Next
		
		If cboRamif.SelectedIndex <> 0 Then
			colNoProc = New Collection
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            lstProcP.Items.Clear() '.ListItems.Clear()
			'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Tree.Nodes.Clear()
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
			Me.Enabled = False
			'UPGRADE_WARNING: Couldn't resolve default property of object arrRami(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            msCargalstProc(arrRami(0, cboRamif.SelectedIndex))
            'msCargalstProc(arrRami(cboRamif.SelectedIndex))
			Me.Enabled = True
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		End If
		
	End Sub
	
	'UPGRADE_WARNING: Event chkRamif.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkRamif_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRamif.CheckStateChanged
		
		If Me.chkRamif.CheckState = 1 Then
			bTree = False
			Me.Tree.Visible = False
			Me.lstProcP.Visible = True
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'lstProcP.ListItems.Clear()
            lstProcP.Items.Clear() '
			'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Tree.Nodes.Clear()
			Me.cboRamif.Enabled = True
			cboRamif.Items.Insert(cboRamif.Items.Count, "")
			msCargacboRamif()
		Else
			bTree = True
			Me.Tree.Visible = True
			Me.lstProcP.Visible = False
			Me.cboRamif.Items.Clear()
			Me.cboRamif.Enabled = False
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            lstProcP.Items.Clear()
			'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Tree.Nodes.Clear()
			msCargarcboProc()
		End If
		
	End Sub
	
	Private Sub cmdAceptar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAceptar.Click
		
		On Error GoTo procErr
		
		Dim arrCol(3) As Object
		Dim I As Integer
		Dim j As Integer
		Dim blnPrim As Boolean
		Dim sSQL As String
		'UPGRADE_NOTE: strConV was upgraded to strConV_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim strConV_Renamed As String
		Dim blnPasar As Boolean
		Dim rstAux As ADODB.Recordset
		Dim rstAux2 As ADODB.Recordset
		Dim blnCorrecto As Boolean
		Dim strBaseDatos As String
		Dim rst2 As ADODB.Recordset
		
		
		blnPasar = False
		sSQL = "INSERT INTO PROCESOS_HISTORICO SELECT * FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
		'sSQL = "INSERT INTO PROCESOS_HISTORICO SELECT * FROM PROCESOS_PENDIENTES"
		mfExecute(conPasarela, sSQL)
		sSQL = "DELETE PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
		'sSQL = "DELETE PROCESOS_PENDIENTES"
		mfExecute(conPasarela, sSQL)
		'*****************************
		blnCarga = False
		blnCargaInfra = False
		blnPrim = False
		colCola = New Collection
		sSQL = "SELECT NOMBRE_CM, USERID FROM PROCESOS_PENDIENTES WHERE FINALIZADO <>'S' and FINALIZADO <> 'C' order by NOMBRE_CM"
		'sSQL = "SELECT NOMBRE_CM, USERID FROM PROCESOS_HISTORICO WHERE FINALIZADO <>'S' and FINALIZADO <> 'C' order by NOMBRE_CM"
		rstAux = mfRecordset(conPasarela, sSQL)
		If bTree = False Then
			blnCorrecto = True
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            For I = 1 To lstProcP.Items.Count
                'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If lstProcP.Items(I).Checked = True And lstProcP.Items(I).ForeColor.ToArgb() <> System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red) Then
                    '----- comprobamos que no los este usando otro usuario (Aketza)
                    If rstAux.RecordCount > 0 Then
                        rstAux.MoveFirst()
                    End If
                    For j = 1 To rstAux.RecordCount '-1
                        'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        If (rstAux.Fields("NOMBRE_CM").Value = lstProcP.Items(I).Text) Then
                            sSQL = "SELECT NOMBRE FROM USUARIOS WHERE USERID='" & rstAux.Fields("USERID").Value & "'"
                            rstAux2 = mfRecordset(conPasarela, sSQL)
                            MsgBox("El proceso " & rstAux.Fields("NOMBRE_CM").Value & " lo tiene pendiente " & rstAux2.Fields("NOMBRE").Value)
                            'MsgBox "El proceso " & rstAux("NOMBRE_CM") & " está pendiente "
                            blnCorrecto = False
                            Exit For
                        Else
                            blnCorrecto = True
                        End If
                        rstAux.MoveNext()
                    Next j
                    If blnCorrecto = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim()(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'sSQL = "SELECT * FROM PROCEDIMIENTOS WHERE CODEXTPR='" & colProcedim.Item(Tree.Nodes.Item(I).Key)(3) & "'"
                        sSQL = "SELECT * FROM PROCEDIMIENTOS WHERE CODEXTPR='" & colProcedim.Item(Tree.Nodes.Item(I).Name)(3) & "'"

                        rst2 = mfRecordset(conInfra, sSQL)

                        If rst2.RecordCount > 0 Then
                            strConV_Renamed = ReadIniFile(INIFile, Trim(rst2.Fields("BASEDATOS").Value), "Connection")
                            strBaseDatos = Trim(rst2.Fields("BASEDATOS").Value)
                        Else
                            strConV_Renamed = ""
                            strBaseDatos = ""
                        End If
                        blnPasar = True
                        sSQL = "INSERT INTO PROCESOS_PENDIENTES (PROCEDIMIENTO,ID,NOMBRE_CM, FECHA_ACTIVACION,NUEVA_VERSION,"
                        sSQL = sSQL & "CONEXION, FINALIZADO,USERID,BASEDATOS, GRUPO) VALUES "
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim()(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        sSQL = sSQL & "('" & colProcedim.Item(Tree.Nodes.Item(I).Name)(3) & "'," & colProcedim.Item(Tree.Nodes.Item(I).Name)(0) & ", '" & colProcedim.Item(Tree.Nodes.Item(I).Name)(1) & "',"
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        sSQL = sSQL & "'" & VB6.Format(Today, "DD/MM/YYYY") & "','0', '" & strConV_Renamed & "','N','" & strUserId & "','" & strBaseDatos & "', '" & colProcedim.Item(Tree.Nodes.Item(I).Name)(2) & "')"
                        '               sSQL = "INSERT INTO PROCESOS_PENDIENTES (PROCEDIMIENTO,ID,NOMBRE_CM, FECHA_ACTIVACION,NUEVA_VERSION,"
                        '               sSQL = sSQL & "CONEXION, FINALIZADO,BASEDATOS, GRUPO) VALUES "
                        '               sSQL = sSQL & "('" & colProcedim(Tree.Nodes.Item(I).Key)(3) & "'," & colProcedim(Tree.Nodes.Item(I).Key)(0) & ", '" & colProcedim(Tree.Nodes.Item(I).Key)(1) & "',"
                        '               sSQL = sSQL & "'" & Format(Date, "DD/MM/YYYY") & "','0', '" & strConV & "','N','" & strBaseDatos & "', '" & colProcedim(Tree.Nodes.Item(I).Key)(2) & "')"
                        mfExecute(conPasarela, sSQL)
                        '***************************
                    End If
                End If
            Next I
		Else
			blnCorrecto = True
			'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			For I = 1 To Tree.Nodes.Count
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Tree.Nodes.Item(I).Checked = True And Mid(Tree.Nodes.Item(I).Name, 1, 1) <> "F" And Tree.Nodes.Item(I).ForeColor.ToArgb() <> System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red) Then
                    '----- comprobamos que no los este usando otro usuario (Aketza)
                    If rstAux.RecordCount > 0 Then
                        rstAux.MoveFirst()
                    End If
                    For j = 1 To rstAux.RecordCount '-1
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        If (rstAux.Fields("NOMBRE_CM").Value = Tree.Nodes.Item(I).Text) Then
                            sSQL = "SELECT NOMBRE FROM USUARIOS WHERE USERID='" & rstAux.Fields("USERID").Value & "'"
                            rstAux2 = mfRecordset(conPasarela, sSQL)
                            MsgBox("El proceso " & rstAux.Fields("NOMBRE_CM").Value & " está pendiente " & rstAux2.Fields("NOMBRE").Value)
                            '***********************************************
                            '                  MsgBox "El proceso " & rstAux("NOMBRE_CM") & " está pendiente "
                            blnCorrecto = False
                            Exit For
                        Else
                            blnCorrecto = True
                        End If
                        rstAux.MoveNext()
                    Next j
                    If blnCorrecto = True Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim()(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        sSQL = "SELECT * FROM PROCEDIMIENTOS WHERE CODEXTPR='" & colProcedim.Item(Tree.Nodes.Item(I).Name)(3) & "'"
                        rst2 = mfRecordset(conInfra, sSQL)
                        If rst2.RecordCount > 0 Then
                            strConV_Renamed = ReadIniFile(INIFile, Trim(rst2.Fields("BASEDATOS").Value), "Connection")
                            strBaseDatos = Trim(rst2.Fields("BASEDATOS").Value)
                        Else
                            strConV_Renamed = ""
                            strBaseDatos = ""
                        End If
                        blnPasar = True
                        sSQL = "INSERT INTO PROCESOS_PENDIENTES (PROCEDIMIENTO,ID,NOMBRE_CM, FECHA_ACTIVACION,NUEVA_VERSION,"
                        sSQL = sSQL & "CONEXION, FINALIZADO,USERID,BASEDATOS, GRUPO) VALUES "
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim()(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        sSQL = sSQL & "('" & colProcedim.Item(Tree.Nodes.Item(I).Name)(3) & "'," & colProcedim.Item(Tree.Nodes.Item(I).Name)(0) & ", '" & colProcedim.Item(Tree.Nodes.Item(I).Name)(1) & "',"
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object colProcedim(Tree.Nodes.Item(I).Key)(2). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        sSQL = sSQL & "'" & VB6.Format(Today, "DD/MM/YYYY") & "','0', '" & strConV_Renamed & "','N','" & strUserId & "','" & strBaseDatos & "', '" & colProcedim.Item(Tree.Nodes.Item(I).Name)(2) & "')"
                        '               sSQL = "INSERT INTO PROCESOS_PENDIENTES (PROCEDIMIENTO,ID,NOMBRE_CM, FECHA_ACTIVACION,NUEVA_VERSION,"
                        '               sSQL = sSQL & "CONEXION, FINALIZADO,BASEDATOS, GRUPO, USERID) VALUES "
                        '               sSQL = sSQL & "('" & colProcedim(Tree.Nodes.Item(I).Key)(3) & "'," & colProcedim(Tree.Nodes.Item(I).Key)(0) & ", '" & colProcedim(Tree.Nodes.Item(I).Key)(1) & "',"
                        '               sSQL = sSQL & "'" & Format(Date, "DD/MM/YYYY") & "','0', '" & strConV & "','N','" & strBaseDatos & "', '" & colProcedim(Tree.Nodes.Item(I).Key)(2) & "', '')"
                        '*****************************************
                        mfExecute(conPasarela, sSQL)
                    End If
                End If
				
			Next I
		End If
		If blnPasar = True Then
			For I = 1 To colProcedim.Count()
				colProcedim.Remove(1)
			Next I
			'UPGRADE_NOTE: Object colProcedim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			colProcedim = Nothing
			Me.Close()
			frmConf.Show()
		Else
			MsgBox("Debe seleccionar algún procedimiento", MsgBoxStyle.Critical)
		End If
		If Not rst2 Is Nothing Then
			rst2.Close()
		End If
		'UPGRADE_NOTE: Object rst2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rst2 = Nothing
		GoTo procFin
		
procErr: 
		
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		
	End Sub
	
	
	Private Sub cmdCancelar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelar.Click
		
		Me.Close()
		
		If Not conDP4 Is Nothing Then
			If conDP4.State = ADODB.ObjectStateEnum.adStateOpen Then conDP4.Close()
			'UPGRADE_NOTE: Object conDP4 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			conDP4 = Nothing
		End If
		If conInfra.State = ADODB.ObjectStateEnum.adStateOpen Then conInfra.Close()
		'UPGRADE_NOTE: Object conInfra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		conInfra = Nothing
		If Not conDB2 Is Nothing Then
			If conDB2.State = ADODB.ObjectStateEnum.adStateOpen Then conDB2.Close()
			'UPGRADE_NOTE: Object conDB2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			conDB2 = Nothing
		End If
		frmConexion.Show()
		
	End Sub
	
	Private Sub frmSelProc_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
		mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 400
		bMinimizado = False
		frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
		frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
		frmMDI.Icon = Me.Icon
		frmMDI.Text = Me.Text
		bMinimizado = True
		Me.Top = 0
		Me.Left = 0
		'UPGRADE_NOTE: Object colPasar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		colPasar = Nothing
		strNombre = ""
		Selectedflow = ""
		SelectedVersion = 0
		strProj = ""
		blnCarga = False
		blnCargaInfra = False
		strFecha = ""
		lngCodProc = 0
		blnErrRecordset = False
		blnCon1 = False
		gNomProc = ""
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Me.Enabled = False
		contador = 0
		msformateargrid()
		msCargarGrid()
		msCargarcboProc()
		Me.Enabled = True
		'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
		'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		frmMDI.SysTray.set_TrayTip("Selección de procedimiento")
		Me.Tree.Visible = True
		Me.lstProcP.Visible = False
		InitTree()
		bTree = True
		
	End Sub
	
	Private Sub msCargarcboProc()
		'Procedimiento encargado de cargar todos los procedimientos contenidos en DP4 en su combo correspondiente, buscando los nombres "Procedimiento"
		
		On Error GoTo procErr
		
		Dim strSQL As String
		Dim Nodo As System.Windows.Forms.TreeNode
		Dim Grupo As String
		Dim GrupoAnterior As String
		Dim I As Short
		Dim bProcAparece As Boolean
		Dim rstProc As ADODB.Recordset
		Dim colXML As Collection
		Dim colXMLD As Collection
		Dim colXML2 As Collection
		Dim colSinProp As Collection
		Dim arrProcedim(4) As String
		Dim icol As Object
		Dim strCadenaModelo As String
		'***************************
		'Ainhoa 23/11/2007
		'Añado un recordset para coger el ID de ANO_TABNR del modelo
		Dim rstIDDiagram As ADODB.Recordset
		'***************************
		'UPGRADE_NOTE: Object conDP4 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		conDP4 = Nothing
		conDP4 = New ADODB.Connection
		strCadenaModelo = ReadIniFile(INIFile, "SQLMODELO", "Connection")
		'strCadenaModelo = Replace(strCadenaModelo, "Data Source", "Initial Catalog=CM" & strModelo & ";Data Source")
		
		
		ComprobarConexion(conDP4, strCadenaModelo)
		'Ainhoa 23/11/2007
		rstIDDiagram = New ADODB.Recordset
		strSQL = "Select OT_ID From CW_OBJECT_TYPE Where OT_NAME='Procedimiento'"
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND MODEL_NAME ='" & strModelo & "'"
		'****
		rstIDDiagram = mfRecordset(conDP4, strSQL)
		'*****************************************
		rstProc = New ADODB.Recordset
		
		strSQL = "SELECT A.ANO_ID,A.DI_ID, B.DI_NAME FROM SHAPE A, DIAGRAM B, CW_LOOKUP C "
		strSQL = strSQL & "WHERE A.DI_ID=B.DI_ID AND A.ANO_TABNR=" & rstIDDiagram.Fields("OT_ID").Value
		strSQL = strSQL & " AND B.DI_TYPE=C.LU_ID AND C.LU_NAME = 'Procedimiento' "
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND A.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND B.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND C.MODEL_NAME ='" & strModelo & "'"
		'****
		strSQL = strSQL & " order by ltrim(replace(di_name,'*',''))"
		
		rstProc = mfRecordset(conDP4, strSQL)
		GrupoAnterior = ""
		colXML = New Collection
		colXML2 = New Collection
		colXML.Add("Grupo Procedimiento")
		
		colXML2.Add("Código Procedimiento")
		colProcedim = New Collection
		While Not rstProc.EOF
			colXMLD = frmProceso.mfBuscaXML("Procedimiento", colXML, rstProc.Fields("ANO_ID").Value, True, True)
			'UPGRADE_WARNING: Couldn't resolve default property of object colXMLD(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProcedim(2) = colXMLD.Item(1)
			colXMLD = frmProceso.mfBuscaXML("Procedimiento", colXML, rstProc.Fields("ANO_ID").Value, True)
			'UPGRADE_WARNING: Couldn't resolve default property of object colXMLD(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProcedim(4) = colXMLD.Item(1)
			colXMLD = frmProceso.mfBuscaXML("Procedimiento", colXML2, rstProc.Fields("ANO_ID").Value)
			arrProcedim(0) = rstProc.Fields("DI_ID").Value
			arrProcedim(1) = rstProc.Fields("DI_NAME").Value
			'UPGRADE_WARNING: Couldn't resolve default property of object colXMLD(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProcedim(3) = colXMLD.Item(1)
			If rstProc.Fields("DI_ID").Value = 1740 Then
				System.Windows.Forms.Application.DoEvents()
			End If
			colProcedim.Add(arrProcedim, CStr("P" & rstProc.Fields("DI_ID").Value))
			rstProc.MoveNext()
		End While
		Dim P As Short
		For	Each icol In colProcedim
			bProcAparece = True
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If IsNothing(arrProcs) = False Then
				For I = 0 To UBound(arrProcs, 2)
					'UPGRADE_WARNING: Couldn't resolve default property of object arrProcs(0, I). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					'UPGRADE_WARNING: Couldn't resolve default property of object icol(3). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					If icol(3) = arrProcs(0, I) Then
						bProcAparece = False
						Exit For
					End If
				Next I
			End If
			If bProcAparece = True Then
				'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Grupo = icol(4)
				System.Windows.Forms.Application.DoEvents()
				'## DS66 26/11/09 Para no tener en cuenta que empiecen por "P"
				'If InStr(1, icol(3), "P") > 0 Then
				'##
				If Grupo <> "" And Mid(Grupo, 1, 1) <> "P" Then
					If Grupo <> GrupoAnterior Then
						
						'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						For P = 1 To Tree.Nodes.Count
							'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							If Tree.Nodes.Item(P).Text = "Grupo " & Grupo Then
								System.Windows.Forms.Application.DoEvents()
								GoTo Err_Renamed
							End If
						Next 
						
						'UPGRADE_ISSUE: Constant tvwChild was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
						'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'Nodo = Tree.Nodes.Add(, tvwChild, "F" & Grupo, "Grupo " & Grupo)
                        Nodo = Tree.Nodes.Add("F" & Grupo, "Grupo " & Grupo)


						'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						'UPGRADE_ISSUE: Constant tvwChild was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
						'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'Nodo = Tree.Nodes.Add("F" & Grupo, tvwChild, "P" & icol(0), Trim(icol(1)))
                        Nodo = Tree.Nodes.Add("P" & icol(0), Trim(icol(1)))
					Else
Err_Renamed: 
						'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						'UPGRADE_ISSUE: Constant tvwChild was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
						'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'Nodo = Tree.Nodes.Add("F" & Grupo, tvwChild, "P" & icol(0), Trim(icol(1)))
                        Nodo = Tree.Nodes.Add("F" & Grupo, "P" & icol(0), Trim(icol(1)))
					End If
					GrupoAnterior = Grupo
				Else
					If colSinProp Is Nothing Then
						colSinProp = New Collection
					End If
					'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					colSinProp.Add(IIf(Grupo = "", "Grupo no informado ", IIf(Mid(Grupo, 1, 1) = "P", "Grupo mal definido ", "")) & Trim(icol(1)), CStr(icol(0)))
				End If
				'## DS66 26/11/09 Para no tener en cuenta que empiecen por "P"
				'Else
				'   If colSinProp Is Nothing Then
				'      Set colSinProp = New Collection
				'   End If
				'   colSinProp.Add IIf(Grupo = "", "Grupo no informado ", IIf(Mid(Grupo, 1, 1) = "P", "Grupo mal definido ", "")) & Trim(icol(1)), CStr(icol(0))
				'End If
				'##
			End If
		Next icol
		If Not colSinProp Is Nothing Then
			If colSinProp.Count() > 0 Then
				'UPGRADE_ISSUE: Constant tvwChild was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'Nodo = Tree.Nodes.Add(, tvwChild, "Errores", "Errores")
                Nodo = Tree.Nodes.Add("Errores", "Errores")
				Nodo.NodeFont = VB6.FontChangeBold(Nodo.NodeFont, True)
				Nodo.ForeColor = System.Drawing.Color.Red
			End If
			For	Each icol In colSinProp
				'UPGRADE_ISSUE: Constant tvwChild was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'Nodo = Tree.Nodes.Add("Errores", tvwChild, icol, icol)
                Nodo = Tree.Nodes.Add("Errores", icol, icol)
				Nodo.NodeFont = VB6.FontChangeBold(Nodo.NodeFont, True)
				Nodo.ForeColor = System.Drawing.Color.Red
			Next icol
		End If
		rstProc.Close()
		'UPGRADE_NOTE: Object rstProc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstProc = Nothing
		'## DS66 26/11/09 Ordena el arbol por el cambio de "P"
		'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Sorted. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Tree.Sorted = True
		'##
		GoTo procFin
		
procErr: 
		
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		If Not colSinProp Is Nothing Then
			For I = 1 To colSinProp.Count()
				colSinProp.Remove(1)
			Next I
			'UPGRADE_NOTE: Object colSinProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			colSinProp = Nothing
		End If
		
	End Sub
	
	
	Private Sub msCargacboRamif()
		
		Dim strSQL As String
		Dim rstRami As ADODB.Recordset
		
		On Error GoTo procErr
		
		rstRami = New ADODB.Recordset
		strSQL = "SELECT A.ANO_ID, A.DI_NAME FROM DIAGRAM A, CW_PROP_TYPE B, CW_LOOKUP C "
		strSQL = strSQL & "WHERE A.DI_TYPE=C.LU_ID AND B.PPT_ID=C.PPT_ID AND "
		strSQL = strSQL & "B.PPT_FIELD_NAME = 'DI_TYPE' AND C.LU_NAME = 'Ramificación' "
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND A.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND B.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND c.MODEL_NAME ='" & strModelo & "'"
		'**************
		strSQL = strSQL & " ORDER BY A.DI_NAME"
		rstRami = mfRecordset(conDP4, strSQL)
		ReDim arrRami(1, 0)
		'UPGRADE_WARNING: Couldn't resolve default property of object arrRami(0, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arrRami(0, 0) = ""
		'UPGRADE_WARNING: Couldn't resolve default property of object arrRami(1, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arrRami(1, 0) = ""
		ReDim Preserve arrRami(1, 1)
		While Not rstRami.EOF
			'UPGRADE_WARNING: Couldn't resolve default property of object arrRami(0, cboRamif.ListCount). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrRami(0, cboRamif.Items.Count) = rstRami.Fields("ANO_ID").Value
			'UPGRADE_WARNING: Couldn't resolve default property of object arrRami(1, cboRamif.ListCount). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrRami(1, cboRamif.Items.Count) = rstRami.Fields("DI_NAME").Value
			cboRamif.Items.Insert(cboRamif.Items.Count, rstRami.Fields("DI_NAME").Value)
			System.Windows.Forms.Application.DoEvents()
			ReDim Preserve arrRami(1, cboRamif.Items.Count)
			rstRami.MoveNext()
		End While
		rstRami.Close()
		'UPGRADE_NOTE: Object rstRami may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstRami = Nothing
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		
	End Sub
	
	Private Sub msCargalstProc(ByVal Id As Integer)
		
		On Error GoTo procErr
		
		Dim strSQL As String
		Dim arrPasar(1) As Object
		Dim icol As Object
		Dim bProcAparece As Boolean
		Dim I As Integer
		
		rstProc2 = New ADODB.Recordset
		strSQL = "SELECT DISTINCT A.DI_ID, B.DI_NAME, B.DI_TYPE, B.ANO_ID FROM SHAPE A, DIAGRAM B "
		strSQL = strSQL & "WHERE A.DI_ID=B.DI_ID AND A.ANO_ID=" & Id
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND A.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND B.MODEL_NAME ='" & strModelo & "'"
		'*******
		rstProc2 = mfRecordset(conDP4, strSQL)
		colID = New Collection
		While Not rstProc2.EOF
			bProcAparece = True
			For I = 0 To UBound(arrProcs, 2)
				'UPGRADE_WARNING: Couldn't resolve default property of object arrProcs(0, I). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If Mid(rstProc2.Fields("DI_NAME").Value, 1, 6) = arrProcs(0, I) Then
					bProcAparece = False
					Exit For
				End If
			Next I
			If bProcAparece = True Then
				If rstProc2.Fields("DI_TYPE").Value <> 1 Then
					System.Windows.Forms.Application.DoEvents()
					If rstProc2.Fields("ANO_ID").Value <> Id Then
						msDesglosaRamif((rstProc2.Fields("ANO_ID").Value))
					End If
				Else
					'UPGRADE_WARNING: Couldn't resolve default property of object arrPasar(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					arrPasar(0) = rstProc2.Fields("DI_ID").Value
					'UPGRADE_WARNING: Couldn't resolve default property of object arrPasar(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					arrPasar(1) = rstProc2.Fields("DI_NAME").Value
					If Not ExisteCol(colID, CStr(rstProc2.Fields("DI_ID").Value)) Then
						colID.Add(arrPasar, CStr(rstProc2.Fields("DI_ID").Value))
					End If
				End If
			End If
			rstProc2.MoveNext()
		End While
		ReDim arrProc2(1, 0)
		'UPGRADE_WARNING: Couldn't resolve default property of object arrProc2(0, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arrProc2(0, 0) = ""
		'UPGRADE_WARNING: Couldn't resolve default property of object arrProc2(1, 0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arrProc2(1, 0) = ""
		I = 0
		ReDim Preserve arrProc2(1, colID.Count())
		For	Each icol In colID
			I = I + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            lstProcP.Items.Add(CStr(icol(1))) ', , CStr(icol(1)))
			'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.ListItems. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            lstProcP.Items.Item(lstProcP.Items.Count).SubItems(1) = icol(0)
			'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object arrProc2(0, I). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProc2(0, I) = icol(0)
			'UPGRADE_WARNING: Couldn't resolve default property of object icol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object arrProc2(1, I). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProc2(1, I) = icol(1)
		Next icol
		'UPGRADE_WARNING: Couldn't resolve default property of object lstProcP.Sorted. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'lstProcP.Sorted = True
        lstProcP.Sort()
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		GoTo procFin
		
procFin: 
		
	End Sub
	
	
	Private Sub msDesglosaRamif(ByRef Id As Integer)
		
		On Error GoTo procErr
		
		Dim rstAux As ADODB.Recordset
		Dim strSQL As String
		Dim blnRamif As Boolean
		Dim arrPasar(1) As Object
		Dim bProcAparece As Boolean
		Dim I As Short
		
		rstAux = New ADODB.Recordset
		
		strSQL = "SELECT DISTINCT A.DI_ID, B.DI_NAME, B.DI_TYPE, B.ANO_ID FROM SHAPE A, DIAGRAM B "
		strSQL = strSQL & "WHERE A.DI_ID=B.DI_ID AND A.ANO_ID=" & Id & " AND B.DI_TYPE=1"
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND A.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND B.MODEL_NAME ='" & strModelo & "'"
		rstAux = mfRecordset(conDP4, strSQL)
		System.Windows.Forms.Application.DoEvents()
		While Not rstAux.EOF
			bProcAparece = True
			For I = 0 To UBound(arrProcs, 2)
				'UPGRADE_WARNING: Couldn't resolve default property of object arrProcs(0, I). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If Mid(rstAux.Fields("DI_NAME").Value, 1, 6) = arrProcs(0, I) Then
					bProcAparece = False
					Exit For
				End If
			Next I
			If bProcAparece = True Then
				If rstAux.Fields("ANO_ID").Value <> Id Then
					If rstAux.Fields("DI_TYPE").Value = 1 Then
						'UPGRADE_WARNING: Couldn't resolve default property of object arrPasar(0). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						arrPasar(0) = rstAux.Fields("DI_ID").Value
						'UPGRADE_WARNING: Couldn't resolve default property of object arrPasar(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						arrPasar(1) = rstAux.Fields("DI_NAME").Value
						If Not ExisteCol(colID, CStr(rstAux.Fields("DI_ID").Value)) Then
							colID.Add(arrPasar, CStr(rstAux.Fields("DI_ID").Value))
						End If
						'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
						System.Array.Clear(arrPasar, 0, arrPasar.Length)
					ElseIf (rstAux.Fields("DI_TYPE").Value = 3 Or rstAux.Fields("DI_TYPE").Value = 2 Or rstAux.Fields("DI_TYPE").Value = 12 Or rstAux.Fields("DI_TYPE").Value = 15 Or rstAux.Fields("DI_TYPE").Value = 20 Or rstAux.Fields("DI_TYPE").Value = 14) Then 
						If Not ExisteCol(colID, CStr(rstAux.Fields("DI_ID").Value)) Then
							If Not ExisteCol(colNoProc, CStr(rstAux.Fields("DI_ID").Value)) Then
								colNoProc.Add(CStr(rstAux.Fields("DI_ID").Value), CStr(rstAux.Fields("DI_ID").Value))
								msDesglosaRamif(rstAux.Fields("ANO_ID").Value)
							End If
						End If
					End If
				End If
			End If
			rstAux.MoveNext()
		End While
		rstAux.Close()
		'UPGRADE_NOTE: Object rstAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstAux = Nothing
		GoTo procFin
		
procErr: 
		MsgBox(Err.Description)
		Resume procFin
		
procFin: 
		
	End Sub
	
	
	Private Function mfisRamif(ByRef Id As Integer) As Object
		
		Dim rstAux2 As ADODB.Recordset
		Dim strSQL As String
		Dim blnRamif As Boolean
		
		rstAux2 = New ADODB.Recordset
		
		strSQL = "SELECT DISTINCT A.DI_ID, B.DI_NAME, C.LU_NAME FROM SHAPE A, DIAGRAM B, CW_LOOKUP C, CW_PROP_TYPE D "
		strSQL = strSQL & "WHERE A.DI_ID=B.DI_ID AND D.PPT_ID=C.PPT_ID AND B.DI_TYPE=C.LU_ID AND PPT_FIELD_NAME='DI_TYPE' AND  A.DI_ID=" & Id
		' **** NUEVO SD ENERO'08
		strSQL = strSQL & " AND A.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND B.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND C.MODEL_NAME ='" & strModelo & "'"
		strSQL = strSQL & " AND D.MODEL_NAME ='" & strModelo & "'"
		rstAux2 = mfRecordset(conDP4, strSQL)
		While Not rstAux2.EOF
			If rstAux2.Fields("LU_NAME").Value = "Ramificación" Then
				'UPGRADE_WARNING: Couldn't resolve default property of object mfisRamif. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mfisRamif = True
				rstAux2.Close()
				'UPGRADE_NOTE: Object rstAux2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				rstAux2 = Nothing
				Exit Function
			End If
			rstAux2.MoveNext()
		End While
		
	End Function
	
	Private Sub msformateargrid()
		
		On Error GoTo msformateargrid
		
		fgrProcs.Cols = 5
		fgrProcs.rows = 1
		fgrProcs.Row = 0
		
		fgrProcs.Col = 0
		fgrProcs.set_ColWidth(0, 0)
		fgrProcs.set_ColAlignment(0, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrProcs.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrProcs.CellFontName = "Arial"
		fgrProcs.CellFontItalic = True
		
		fgrProcs.Col = 1
		fgrProcs.set_ColWidth(1, 2150)
		fgrProcs.set_ColAlignment(1, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrProcs.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrProcs.CellFontName = "Arial"
		fgrProcs.CellFontItalic = True
		fgrProcs.set_TextMatrix(0, 1, "Nombre")
		
		fgrProcs.Col = 2
		fgrProcs.set_ColWidth(2, 2150)
		fgrProcs.set_ColAlignment(2, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrProcs.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrProcs.CellFontName = "Arial"
		fgrProcs.CellFontItalic = True
		fgrProcs.set_TextMatrix(0, 2, "Usuario")
		
		fgrProcs.Col = 3
		fgrProcs.set_ColWidth(3, 2150)
		fgrProcs.set_ColAlignment(3, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrProcs.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrProcs.CellFontName = "Arial"
		fgrProcs.CellFontItalic = True
		fgrProcs.set_TextMatrix(0, 3, "Estado")
		
		fgrProcs.Col = 4
		fgrProcs.set_ColWidth(4, 2150)
		fgrProcs.set_ColAlignment(4, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrProcs.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrProcs.CellFontName = "Arial"
		fgrProcs.CellFontItalic = True
		fgrProcs.set_TextMatrix(0, 4, "Fecha Inicio")
		
		fgrProcs.set_ColAlignment(1, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		fgrProcs.set_ColAlignment(2, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		fgrProcs.set_ColAlignment(3, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		fgrProcs.set_ColAlignment(4, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		
		Exit Sub
		
msformateargrid: 
		
	End Sub
	'fin aketza
	
	'aketza 27/6/2006
	Private Sub msCargarGrid(Optional ByRef pstrFiltro As String = "")
		
		Dim rstProcs As ADODB.Recordset
		Dim rstConexiones As ADODB.Recordset
		Dim strSQL As String
		Dim I As Short
		Dim j As Short
		
		Dim strBD As String
		
		'Obtener los errores de la tabla ERRORES
		j = 1
		rstProcs = New ADODB.Recordset
		strSQL = "SELECT PROCESOS_PENDIENTES.PROCEDIMIENTO, PROCESOS_PENDIENTES.FINALIZADO, PROCESOS_PENDIENTES.FECHAINICIO,USUARIOS.NOMBRE FROM PROCESOS_PENDIENTES INNER JOIN USUARIOS ON PROCESOS_PENDIENTES.USERID=USUARIOS.USERID WHERE FINALIZADO<>'S' and FINALIZADO<>'C' order by PROCEDIMIENTO"
		'************************
		If Trim(pstrFiltro) <> "" Then
			strSQL = strSQL & Trim(pstrFiltro)
		End If
		rstProcs = mfRecordset(conPasarela, strSQL)
		If Not rstProcs.EOF Then
			rstProcs.MoveFirst()
			'UPGRADE_WARNING: Couldn't resolve default property of object rstProcs.GetRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object arrProcs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrProcs = rstProcs.GetRows
			rstProcs.MoveFirst()
			fgrProcs.rows = UBound(arrProcs, 2) + 2
			For I = 1 To UBound(arrProcs, 2) + 1
				fgrProcs.set_TextMatrix(I, 1, Trim(rstProcs.Fields("PROCEDIMIENTO").Value))
				fgrProcs.set_TextMatrix(I, 2, Trim(rstProcs.Fields("NOMBRE").Value))
				'*********************************
				If Trim(rstProcs.Fields("FINALIZADO").Value) = "N" Then
					fgrProcs.set_TextMatrix(I, 3, "No iniciado")
				Else
					If Trim(rstProcs.Fields("FINALIZADO").Value) = "P" Then
						fgrProcs.set_TextMatrix(I, 3, "En proceso")
					Else
						fgrProcs.set_TextMatrix(I, 3, Trim(rstProcs.Fields("FINALIZADO").Value))
					End If
				End If
				fgrProcs.set_TextMatrix(I, 4, Trim(rstProcs.Fields("FECHAINICIO").Value) & "")
				rstProcs.MoveNext()
			Next 
		End If
		'UPGRADE_NOTE: Object rstProcs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstProcs = Nothing
		For I = 1 To fgrProcs.rows - 1
			Me.fgrProcs.set_RowHeight(I, 250)
		Next I
		
	End Sub
	
	Private Sub Tree_NodeCheck(ByVal Node As System.Windows.Forms.TreeNode)
		
		Dim j As Short
		Dim I As Short
		
		If Mid(Node.Name, 1, 1) = "F" Then
			If Node.Checked = True Then
				j = Node.Index + 1
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Do While Mid(Tree.Nodes.Item(j).Name, 1, 1) = "P"
                    'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Tree.Nodes.Item(j).Checked = True
                    j = j + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    If j > Tree.Nodes.Count Then
                        Exit Do
                    End If
                Loop
			Else
				j = Node.Index + 1
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Do While Mid(Tree.Nodes.Item(j).Name, 1, 1) = "P"
                    'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Tree.Nodes.Item(j).Checked = False
                    j = j + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    If j > Tree.Nodes.Count Then
                        Exit Do
                    End If
                Loop
			End If
		Else
			If Node.Checked = False Then
				'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				For j = 1 To Tree.Nodes.Count
					'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    If Mid(Tree.Nodes.Item(j).Name, 2, 2) = Mid(Node.Text, 2, 2) And Mid(Tree.Nodes.Item(j).Name, 1, 1) = "F" Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Nodes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        Tree.Nodes.Item(j).Checked = False
                    End If
				Next j
			End If
		End If
		
	End Sub
	
	Private Sub InitTree()
		
		On Error GoTo procErr
		
		'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Indentation. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        Tree.Indent = 4 '.Indentation = 4
		'UPGRADE_WARNING: Couldn't resolve default property of object Tree.Style. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_ISSUE: Constant tvwTreelinesPlusMinusPictureText was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        'Tree.Style = tvwTreelinesPlusMinusPictureText
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		
	End Sub
End Class