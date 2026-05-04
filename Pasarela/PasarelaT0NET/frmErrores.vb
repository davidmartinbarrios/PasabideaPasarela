Option Strict Off
Option Explicit On
Friend Class frmErrores
	Inherits System.Windows.Forms.Form
	
	'Inicio Jonathan Prieto 07/06/2011
	Public intOpcion As Short
	'Fin Jonathan Prieto 07/06/2011
	
	Private Sub cmdAplicar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAplicar.Click
		
		Dim strFiltro As String
		Dim strBusqueda As String
		
		strBusqueda = Trim(cboAmbito.Text)
		If Trim(strBusqueda) <> "" Then
			strFiltro = " and AMBITO LIKE '%" & strBusqueda & "%' OR DESCRIPCION LIKE '%" & strBusqueda & "%' order by PROCEDIMIENTO "
			msCargarGrid(strFiltro)
		Else
			msCargarGrid()
		End If
		
	End Sub
	
	
	
	Private Sub CmdContinuar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdContinuar.Click
		
		Dim strSQL As String
		
		'Inicio Jonathan Prieto 07/06/2011 - Si intOpcion=1 el frmErrores se ha mostrado desde validaciones
		If intOpcion <> 1 Then
			strSQL = "INSERT INTO PROCESOS_HISTORICO SELECT * FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
			mfExecute(conPasarela, strSQL)
			strSQL = "DELETE FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
			mfExecute(conPasarela, strSQL)
			strSQL = "INSERT INTO ERRORES_HISTORICO SELECT * FROM ERRORES WHERE USERID='" & strUserId & "'"
			mfExecute(conPasarela, strSQL)
			strSQL = "DELETE FROM ERRORES WHERE USERID='" & strUserId & "'"
			mfExecute(conPasarela, strSQL)
		End If
		'***********************************************************
		Me.Close()
		'Inicio Jonathan Prieto 07/06/2011 - Si intOpcion=1 el frmErrores se ha mostrado desde validaciones
		If intOpcion <> 1 Then
			frmSelProc.Show()
		End If
		'Fin Jonathan Prieto 07/06/2011
		
	End Sub
	
	Private Sub cmdNotepad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNotepad.Click
		
		On Error GoTo procErr
		
		Dim nFnum As Integer
		Dim cSource As String
		Dim rstError As ADODB.Recordset
		Dim I As Short
		Dim strSQL As String
		Dim strFiltro As String
		Dim Num As Integer
		Dim arr As Object
		
		nFnum = FreeFile
		Kill(My.Application.Info.DirectoryPath & "\Errores.log")
		FileOpen(nFnum, My.Application.Info.DirectoryPath & "\Errores.log", OpenMode.Append)
		If Trim(Me.cboAmbito.Text) <> "" Then
			strFiltro = " and  AMBITO LIKE '%" & Trim(Me.cboAmbito.Text) & "%' OR DESCRIPCION LIKE '%" & Trim(Me.cboAmbito.Text) & "%'"
		End If
		rstError = New ADODB.Recordset
		strSQL = "SELECT distinct PROCEDIMIENTO,AMBITO, DESCRIPCION "
		strSQL = strSQL & "FROM ERRORES WHERE USERID='" & strUserId & "'"
		If Trim(strFiltro) <> "" Then
			strSQL = strSQL & Trim(strFiltro)
		End If
		rstError = mfRecordset(conPasarela, strSQL)
		rstError.MoveFirst()
		'UPGRADE_WARNING: Couldn't resolve default property of object rstError.GetRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object arr. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arr = rstError.GetRows
		rstError.MoveFirst()
		If Not rstError.EOF Then
			For I = 1 To UBound(arr, 2) + 1
				PrintLine(nFnum, rstError.Fields("PROCEDIMIENTO").Value & "    " & rstError.Fields("AMBITO").Value & "    " & rstError.Fields("DESCRIPCION").Value)
				rstError.MoveNext()
			Next 
		End If
		'UPGRADE_NOTE: Object rstError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstError = Nothing
		FileClose(nFnum)
		Num = StartDoc(My.Application.Info.DirectoryPath & "\Errores.log", "")
		GoTo procFin
		
procErr: 
		'Resume Next
		'Resume procFin
		
procFin: 
		CierraRS(rstError)
		'UPGRADE_NOTE: Object rstError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstError = Nothing
		
	End Sub
	
	Private Sub cmdTerminar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTerminar.Click
		
		Dim strSQL As String
		
		strSQL = "INSERT INTO PROCESOS_HISTORICO SELECT * FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		strSQL = "DELETE FROM PROCESOS_PENDIENTES WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		strSQL = "INSERT INTO ERRORES_HISTORICO SELECT * FROM ERRORES WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		strSQL = "DELETE FROM ERRORES WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		Me.Close()
		strSQL = "UPDATE USUARIOS SET ACTIVO=0 WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		'************************************************
		End
		
	End Sub
	
	Private Sub frmErrores_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		On Error GoTo procErr
		
		Dim I As Short
		
		msCargarCombo()
		msformateargrid()
		msCargarGrid()
		'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
		'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Me.fgrErrores.WordWrap = True
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
		'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		
	End Sub
	Private Sub msCargarCombo()
		
		Dim rstAmbito As ADODB.Recordset
		Dim strSQL As String
		Dim I As Short
		Dim arrAmbito As Object
		'Obtener los errores de la tabla ERRORES
		
		rstAmbito = New ADODB.Recordset
		strSQL = "SELECT DISTINCT AMBITO FROM ERRORES where USERID='" & strUserId & "'"
		'********************************************
		rstAmbito = mfRecordset(conPasarela, strSQL)
		rstAmbito.MoveFirst()
		'UPGRADE_WARNING: Couldn't resolve default property of object rstAmbito.GetRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object arrAmbito. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		arrAmbito = rstAmbito.GetRows
		rstAmbito.MoveFirst()
		If Not rstAmbito.EOF Then
			cboAmbito.Items.Insert(cboAmbito.Items.Count, "")
			For I = 1 To UBound(arrAmbito, 2) + 1
				cboAmbito.Items.Insert(cboAmbito.Items.Count, rstAmbito.Fields("AMBITO").Value)
				rstAmbito.MoveNext()
			Next 
		End If
		'UPGRADE_NOTE: Object rstAmbito may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstAmbito = Nothing
		
	End Sub
	Private Sub msformateargrid()
		
		On Error GoTo msformateargrid
		
		fgrErrores.Cols = 3
		fgrErrores.rows = 1
		fgrErrores.Row = 0
		
		fgrErrores.Col = 0
		fgrErrores.set_ColWidth(0, 0)
		fgrErrores.set_ColAlignment(0, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrErrores.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrErrores.CellFontName = "Arial"
		fgrErrores.CellFontItalic = True
		
		fgrErrores.Col = 1
		fgrErrores.set_ColWidth(1, 3500)
		fgrErrores.set_ColAlignment(1, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrErrores.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrErrores.CellFontName = "Arial"
		fgrErrores.CellFontItalic = True
		fgrErrores.set_TextMatrix(0, 1, "Ambito")
		
		fgrErrores.Col = 2
		fgrErrores.set_ColWidth(2, 7150)
		fgrErrores.set_ColAlignment(2, MSFlexGridLib.AlignmentSettings.flexAlignLeftCenter)
		fgrErrores.CellForeColor = System.Drawing.Color.FromARGB(192, 0, 0) 'gintGranate
		fgrErrores.CellFontName = "Arial"
		fgrErrores.CellFontItalic = True
		fgrErrores.set_TextMatrix(0, 2, "Descripción")
		Me.fgrErrores.set_ColAlignment(1, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		Me.fgrErrores.set_ColAlignment(2, MSFlexGridLib.AlignmentSettings.flexAlignLeftTop)
		
		Exit Sub
		
msformateargrid: 
		
	End Sub
	Private Sub msCargarGrid(Optional ByRef pstrFiltro As String = "")
		
		Dim rstError As ADODB.Recordset
		Dim rstConexiones As ADODB.Recordset
		Dim strSQL As String
		Dim I As Short
		Dim j As Short
		Dim arrError As Object
		Dim strServidor As String
		Dim strBD As String
		
		j = 1
		rstError = New ADODB.Recordset
		strSQL = "SELECT DISTINCT PROCEDIMIENTO, AMBITO, DESCRIPCION FROM ERRORES WHERE USERID='" & strUserId & "' order by PROCEDIMIENTO"
		'****************************************
		If Trim(pstrFiltro) <> "" Then
			strSQL = strSQL & Trim(pstrFiltro)
		End If
		rstError = mfRecordset(conPasarela, strSQL)
		If Not rstError.EOF Then
			rstError.MoveFirst()
			'UPGRADE_WARNING: Couldn't resolve default property of object rstError.GetRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object arrError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			arrError = rstError.GetRows
			rstError.MoveFirst()
			fgrErrores.rows = UBound(arrError, 2) + 2
			For I = 1 To UBound(arrError, 2) + 1
				If Trim(rstError.Fields("DESCRIPCION").Value) = "El proceso se ha volcado sin errores" Then
					'## ds66
					Me.fgrErrores.ForeColor = System.Drawing.ColorTranslator.FromOle(&H8000)
					'##
					rstConexiones = New ADODB.Recordset
					strSQL = "SELECT PROCEDIMIENTO, CONEXION2 FROM PROCESOS_PENDIENTES WHERE PROCEDIMIENTO='" & rstError.Fields("PROCEDIMIENTO").Value & "' and USERID='" & strUserId & "' and FINALIZADO='S' order by NOMBRE_CM"
					'********************
					rstConexiones = mfRecordset(conPasarela, strSQL)
					If Not rstConexiones.EOF Then
						strServidor = rstConexiones.Fields("CONEXION2").Value
						If Trim(strServidor) <> "" Then
							strBD = Mid(Mid(strServidor, InStr(1, strServidor, "Catalog=")), 9)
							strServidor = Mid(Mid(strServidor, InStr(1, strServidor, "Data Source=")), 13)
							If InStr(1, strServidor, ";") Then
								strServidor = Mid(strServidor, 1, InStr(1, strServidor, ";"))
								strServidor = Mid(strServidor, 1, Len(strServidor) - 1)
							End If
							If InStr(1, strBD, ";") Then
								strBD = Mid(strBD, 1, InStr(1, strBD, ";"))
								strBD = Mid(strBD, 1, Len(strBD) - 1)
							End If
							strServidor = ". También se ha volcado en " & strBD & " en el servidor " & strServidor
							strSQL = "UPDATE ERRORES SET DESCRIPCION='" & strServidor & "' WHERE PROCEDIMIENTO='" & rstError.Fields("PROCEDIMIENTO").Value & "' and USERID='" & strUserId & "'"
							'**************************
							mfExecute(conPasarela, strSQL)
						End If
					End If
				End If
				strServidor = Trim(rstError.Fields("DESCRIPCION").Value) & strServidor
				fgrErrores.set_TextMatrix(I, 1, Trim(rstError.Fields("PROCEDIMIENTO").Value) & " - " & Trim(rstError.Fields("AMBITO").Value))
				fgrErrores.set_TextMatrix(I, 2, strServidor)
				strServidor = ""
				rstError.MoveNext()
			Next 
		End If
		'UPGRADE_NOTE: Object rstError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstError = Nothing
		For I = 1 To fgrErrores.rows - 1
			Me.fgrErrores.set_RowHeight(I, 800)
		Next I
		
	End Sub
End Class