Option Strict Off
Option Explicit On
Module basGeneral
	'''FICHERO INI
	Public INIFile As String
	'''DECLARACION DE CONEXIONES
	Public conSqlServer As ADODB.Connection
	Public conDP4 As ADODB.Connection
	Public conPasarela As ADODB.Connection
	Public conCaseWise As ADODB.Connection
	Public conInfra As ADODB.Connection
	Public conEspe As ADODB.Connection
	Public conCarga As ADODB.Connection
	'
	Public conCarga2 As ADODB.Connection
	'
	Public conDB2 As ADODB.Connection
	Public conGestion As ADODB.Connection
	Public conWord As ADODB.Connection
	'## Ds66, Para la nueva BD DBT0DOCU
	Public conDocu As ADODB.Connection
	'##
	Public conMSJ As ADODB.Connection
	Public conGene As ADODB.Connection
	Public conInfrT0 As ADODB.Connection 'J.Lerma 13/03/2019
	Public gblnNuevaVersion As Boolean
	Public blnPreparado As Boolean
	Public colPasar As Collection
	Public colCola As Collection
	Public strNombre As String
	Public Selectedflow As String
	Public SelectedVersion As Integer
	Public VersionHidra As Integer
	Public strProj As String
	Public blnCarga As Boolean
	Public blnCargaInfra As Boolean
	Public strFecha As String
	Public strFechaOriginal As String
	Public lngCodProc As Integer
	Public blnErrRecordset As Boolean
	Public blnCon1 As Boolean
	Public gNomProc As String
	Public gNomProcCorto As String
	Public blnParar As Boolean
	Public gstrObserva As String
	Public blnCancelar As Boolean
	Public mdiAlto As Integer
	Public mdiAncho As Integer
	Public formCargado As System.Windows.Forms.Form
	Public strFechaProgramada As String
	Public bMinimizado As Boolean
	Public strTipoReferencia As String
	Public strTipoFamilia As String
	Public strCodEntid As String
	Public strCodFamil As String
	Public strFamil As String
	Public strModelo As String
	Public lngNombre As Integer
	Public strUserId As String
	Public gstrGrupo As String
	Public strSeg As String
	'Inicio Jonathan Prieto 18/08/2011
	Public strVarVacio As String
	'Fin Jonathan Prieto 18/08/2011
	'Inicio Jonathan Prieto 16/05/2013
	Public gstrCodTramUsu As String
	'Fin Jonathan Prieto 16/05/2013
	'Inicio Jonathan Prieto 12/12/2013
	Public strVarAsuntoDelegacion As String
	Public strVarIndPrimerVB As String
	Public strVarIndUltimoVB As String
	Public strVarIndVB As String
	Public strVarUltimoDocVB As String
	Public strVarNotifPresencial As String
	Public strVarIDCircuito As String
	'Fin Jonathan Prieto 12/12/2013
	
	Public Declare Function GetDesktopWindow Lib "user32" () As Integer
	Public Declare Function ShellExecute Lib "shell32.dll"  Alias "ShellExecuteA"(ByVal hWnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	        Public Declare Ansi Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As System.Text.StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	        Public Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
	'variable para saber si han dado a cancelar durante el proceso(aketza)
	Public Declare Function GetSystemMenu Lib "user32" (ByVal hWnd As Integer, ByVal bRevert As Integer) As Integer
	Public Declare Function DeleteMenu Lib "user32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
	
	'Constantes
	Public Const SC_SIZE As Integer = &HF000
	Public Const SC_MOVE As Integer = &HF010
	Public Const SC_MINIMIZE As Integer = &HF020
	Public Const SC_MAXIMIZE As Integer = &HF030
	Public Const SC_CLOSE As Integer = &HF060
	Public Const SC_RESTORE As Integer = &HF120
	
	Public Const MF_SEPARATOR As Integer = &H800
	Public Const MF_BYPOSITION As Integer = &H400
	Public Const MF_BYCOMMAND As Integer = &H0
	
	Private Const gintMAX_SIZE As Short = 255 'Maximum buffer size
	
        Public Sub logError(ByVal Texto As String)

                On Error Resume Next

                Dim nFnum As Integer
                nFnum = FreeFile()
                FileOpen(nFnum, My.Application.Info.DirectoryPath & "\Traza.log", OpenMode.Append)
                PrintLine(nFnum, Texto & " #(" & Now & ")#")
                FileClose(nFnum)

        End Sub
	
	'Lectura de INI
        Public Function ReadIniFile(ByVal strINIFile As String, ByVal strSection As String, ByVal strKey As String) As String

                Dim strBuffer As New System.Text.StringBuilder(255)

                If GetPrivateProfileString(strSection, strKey, vbNullString, strBuffer, gintMAX_SIZE, strINIFile) Then
                        ReadIniFile = StringFromBuffer(strBuffer.ToString())
                End If
		
	End Function
	
	'Escritura en INI
	Public Function WriteIniFile(ByVal strINIFile As String, ByVal strSection As String, ByVal strKey As String, ByRef lpString As String) As Boolean
		
		WriteIniFile = WritePrivateProfileString(strSection, strKey, lpString, strINIFile) <> 0
		
	End Function
	
	'para Lectura de INI
	Public Function StringFromBuffer(ByRef Buffer As String) As String
		
		On Error GoTo procErr
		
		Dim nPos As Integer
		
		nPos = InStr(Buffer, vbNullChar)
		If nPos > 0 Then
			StringFromBuffer = Left(Buffer, nPos - 1)
		Else
			StringFromBuffer = Buffer
		End If
		GoTo procFin
		
procErr: 
		Resume procFin
procFin: 
		
	End Function
	
	Public Function ExisteCol(ByRef mCol As Object, ByVal IndexKey As String) As Boolean
		
		On Error GoTo procErr
		
		Dim X As Object
		
		'UPGRADE_WARNING: Couldn't resolve default property of object mCol(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object X. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		X = mCol(IndexKey)
		ExisteCol = True
		GoTo procFin
		
procErr: 
		ExisteCol = False
		Resume procFin
procFin: 
		
	End Function
	
	Function snulo(ByRef valor As String, Optional ByRef EsFecha As Boolean = False) As String
		
		If Trim(valor) = "" Then
			snulo = "Null"
		Else
			If EsFecha = False Then
				snulo = CStr(valor)
			ElseIf valor = "" Then 
				snulo = "Null"
			Else
				snulo = VB6.Format(valor, "dd/mm/yyyy")
			End If
		End If
		
	End Function
	
	
	
	
	Public Function mfRecordset(ByRef conexion As ADODB.Connection, ByRef strSQL As String, Optional ByRef DB2 As String = "") As ADODB.Recordset
		
		On Error GoTo procErr
		
		Dim I As Integer
		Dim j As Integer
		
		I = 0
		j = 0
		
		If DB2 <> "" Then
			strSQL = Replace(strSQL, "%owner%", ReadIniFile(INIFile, DB2, "Owner"))
		End If
		mfRecordset = New ADODB.Recordset
		mfRecordset.CursorType = ADODB.CursorTypeEnum.adOpenForwardOnly
		mfRecordset.LockType = ADODB.LockTypeEnum.adLockReadOnly
		mfRecordset.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		mfRecordset.Open(strSQL, conexion)
		GoTo procFin
		
procErr: 
		'    MsgBox "Error en query -- " & strSQL
		If Err.Description = "Error no especificado" Then
			I = I + 1
			If I < 10 Then
				Resume 
			End If
		End If
		If Err.Number = -2147467259 And j < 6 Then
			j = j + 1
			Resume 
		End If
		
		If blnParar = True Then Exit Function
		blnErrRecordset = True
		logError(strSQL & " - " & Err.Description)
		Resume procFin
		
procFin: 
		
	End Function
	
	
	Public Function mfExecute(ByRef conexion As ADODB.Connection, ByRef strSQL As String, Optional ByRef DB2 As String = "", Optional ByRef rows As Integer = 0) As Boolean
		
		Dim posicion As Short
		
		'#### DS66 Busca texto en la SQL que lanza
        If InStr(strSQL, "@LETPR_COMPROBACI�NDEUM01") <> 0 Then
            System.Windows.Forms.Application.DoEvents()
        End If
		'####
		
		On Error GoTo procErr
		
		Dim con As ADODB.Command
		Dim I As Integer
		Dim dbFailOnError As Object
		Dim contError As Short
		
		con = New ADODB.Command
		contError = 0
		If DB2 <> "" Then
			strSQL = Replace(strSQL, "%owner%", ReadIniFile(INIFile, DB2, "Owner"))
		End If
		con.let_ActiveConnection(conexion)
		con.CommandText = strSQL
		con.CommandType = ADODB.CommandTypeEnum.adCmdText
		con.Execute(dbFailOnError)
		'UPGRADE_WARNING: Couldn't resolve default property of object dbFailOnError. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		rows = dbFailOnError
		mfExecute = True
		GoTo procFin
		
procErr: 
		If blnParar = True Then Exit Function
		mfExecute = False
		If (Err.Number = -2147467259 Or Err.Number = -2147217871) And contError < 6 Then
			contError = contError + 1
			Resume 
		End If
		
		logError(strSQL & " - " & Err.Description)
		Resume procFin
		
procFin: 
		
	End Function
	
	Public Function ComprobarConexion(ByRef cn As ADODB.Connection, ByRef ConnectionString As String) As Boolean
		
		On Error GoTo ComprobarConexion_err
		
		If cn Is Nothing Then
			cn = New ADODB.Connection
		End If
		If cn.State = 1 Then
			cn.Close()
			'UPGRADE_NOTE: Object cn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			cn = Nothing
			cn = New ADODB.Connection
		End If
		cn.Open(ConnectionString)
		ComprobarConexion = True
		Exit Function
		
ComprobarConexion_err: 
		ComprobarConexion = False
		
	End Function
	
	Public Function GetParameterFromString(ByRef ParameterString As String, ByRef Sep As String, ByRef QueParametro As Integer) As String
		
		On Error GoTo procErr
		
		Dim Param() As String
		Dim Dummy As Object
		Dim NroParametros As Integer
		
		GetParameterFromString = ""
		NroParametros = CountPars(ParameterString, Sep)
		ReDim Param(NroParametros)
		'UPGRADE_WARNING: Couldn't resolve default property of object Dummy. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Dummy = Text2Array0(ParameterString, Param, Sep)
		If QueParametro > NroParametros Or QueParametro < 0 Then
			GetParameterFromString = ""
		Else
			GetParameterFromString = Param(QueParametro)
		End If
		GoTo procFin
		
procErr: 
		
procFin: 
		
	End Function
	
	Public Function Text2Array0(ByRef Var As String, ByRef Parray() As String, ByRef Sep As String) As Short
		
		On Error GoTo procErr
		
		Dim c As Short
		Dim X As Short
		Dim LastX As Short
		
		LastX = 1
StrArray_Next: 
		X = InStr(LastX, Var, Sep)
		If X Then
			Parray(c) = Mid(Var, LastX, X - LastX)
			c = c + 1
			LastX = X + 1
			GoTo StrArray_Next
		Else
			Parray(c) = Mid(Var, LastX)
		End If
		Parray(c + 1) = ""
		Text2Array0 = c
		GoTo procFin
		
procErr: 
		
procFin: 
		
	End Function
	
	Public Function LeeCampo(ByRef RsLeer As ADODB.Recordset, ByRef campo As String) As Object
		
		On Error GoTo procErr
		
		Dim QueCampo As Short
		Dim Tamano As Short
		
		QueCampo = ExisteCampo(RsLeer, campo)
		If QueCampo <> -1 Then
			If Not RsLeer.EOF Then
				Select Case RsLeer.Fields(QueCampo).Type
					Case Is = 2 'SQL=SMALLINT               DB2=SMALLINT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 3 'SQL=INTEGER                DB2=INTEGER
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 4 'SQL=REAL                   DB2=REAL
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 5 'SQL=FLOAT                  DB2=DOUBLE
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 6 'SQL=MONEY,SMALLMONEY
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 20 'SQL=BIGINT                 DB2=BIGINT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 11 'SQL=BIT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
					Case Is = 12 'SQL=SQL_VARIANT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 17 'SQL=TINYINT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 72 'SQL=UNIQUEIDIENTIFIER
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 128 'SQL=BINARY,TIMESTAMP       DB2=BLOB,CLOB
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 129 'SQL=CHAR                   DB2=CHARACTER
						Tamano = RsLeer.Fields(QueCampo).DefinedSize
						If Tamano = 1 Then
							If RsLeer.Fields(QueCampo).Value = "1" Then
								'UPGRADE_WARNING: Couldn't resolve default property of object LeeCampo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								LeeCampo = True
							ElseIf RsLeer.Fields(QueCampo).Value = "0" Then 
								'UPGRADE_WARNING: Couldn't resolve default property of object LeeCampo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								LeeCampo = False
							ElseIf RsLeer.Fields(QueCampo).Value = "" Then 
								'UPGRADE_WARNING: Couldn't resolve default property of object LeeCampo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								LeeCampo = False
								'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							ElseIf IsDbNull(RsLeer.Fields(QueCampo).Value) Then 
								'UPGRADE_WARNING: Couldn't resolve default property of object LeeCampo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								LeeCampo = False
							Else
								'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
							End If
						Else
							'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
						End If
					Case Is = 130 'SQL=NCHAR
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
					Case Is = 131 'SQL=DECIMAL,NUMERIC        DB2=DECIMAL
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 133 '                           DB2=DATE
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 134 '                           DB2=TIME
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 135 'SQL=DATETIME,SMALLDATETIME DB2=TIMESTAMP
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 200 'SQL=VARCHAR                DB2=VARCHAR
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
					Case Is = 201 'SQL=TEXT                   DB2=LONG VARCHAR
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
					Case Is = 202 'SQL=NVARCHAR
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
					Case Is = 203 'SQL=NTEXT
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull(Trim(RsLeer.Fields(QueCampo).Value))
					Case Else
						'UPGRADE_WARNING: Couldn't resolve default property of object NoNull(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						LeeCampo = NoNull((RsLeer.Fields(QueCampo).Value))
				End Select
			Else
				'UPGRADE_WARNING: Couldn't resolve default property of object LeeCampo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				LeeCampo = ""
			End If
		End If
		LeeCampo = Replace(LeeCampo, Chr(10), Chr(13) & Chr(10))
		LeeCampo = Replace(LeeCampo, Chr(13) & Chr(32), Chr(13) & Chr(10))
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		
	End Function
	
	
	
	Public Function NoNull(ByRef Var As Object) As Object
		
		On Error GoTo procErr
		
		Dim VarEmpty As Object
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(Var) Then
			'UPGRADE_WARNING: Couldn't resolve default property of object VarEmpty. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object NoNull. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			NoNull = VarEmpty
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object Var. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object NoNull. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			NoNull = Var
		End If
		GoTo procFin
		
procErr: 
		Resume procFin
procFin: 
		
	End Function
	
	Public Function Getvalue(ByRef valor As String) As String
		
		Select Case valor
			Case Is = "S"
				Getvalue = "Segundo(s)"
			Case Is = "N"
				Getvalue = "Minuto(s)"
			Case Is = "H"
				Getvalue = "Hora(s)"
			Case Is = "D"
				Getvalue = "Dia(s)"
			Case Is = "M"
				Getvalue = "Mes(es)"
			Case Is = "A"
				Getvalue = "Año(s)"
			Case Else
				Getvalue = valor
		End Select
		
	End Function
	
	Public Function CountPars(ByRef Var As String, ByRef Sep As String) As Short
		
		On Error GoTo procErr
		
		Dim CurrentLoc As Short
		Dim NextStart As Short
		Dim Cuantos As Short
		Dim TmpVar As String
		
		If Len(Var) = 0 Then
			CountPars = 0
		Else
			Cuantos = 0
			NextStart = 1
			TmpVar = Var
			Do 
				System.Windows.Forms.Application.DoEvents()
				CurrentLoc = InStr(1, TmpVar, Sep)
				If CurrentLoc <> 0 Then
					Cuantos = Cuantos + 1
					NextStart = CurrentLoc + Len(Sep)
					TmpVar = Mid(TmpVar, NextStart)
				Else
					Cuantos = Cuantos + 1
					Exit Do
				End If
			Loop 
			CountPars = Cuantos
		End If
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		
	End Function
	
	
	
	Public Function ExisteCampo(ByRef rs As ADODB.Recordset, ByRef campo As String) As Short
		
		Dim I As Short
		
		ExisteCampo = -1
		If Not rs Is Nothing Then
			For I = 0 To rs.Fields.Count - 1
				If UCase(rs.Fields(I).Name) = UCase(campo) Then
					ExisteCampo = I
					Exit For
				End If
			Next I
		End If
		
	End Function
	
	
	
	Public Function StartDoc(ByRef DocName As String, ByRef sPath As String) As Integer
		
		Dim Scr_hDC As Integer
		
		Scr_hDC = GetDesktopWindow()
		StartDoc = ShellExecute(Scr_hDC, "Open", DocName, "", sPath, 1)
		
	End Function
	
	
	Public Function CierraRS(ByRef rs As ADODB.Recordset) As Object
		
		If Not rs Is Nothing Then
			If rs.State <> ADODB.ObjectStateEnum.adStateClosed Then
				rs.Close()
			End If
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
		End If
		
	End Function
	
	
	Public Function CierraCN(ByRef cn As ADODB.Connection) As Object
		
		On Error Resume Next
		
		If Not cn Is Nothing Then
			If cn.State <> ADODB.ObjectStateEnum.adStateClosed Then
				cn.Close()
			End If
			'UPGRADE_NOTE: Object cn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			cn = Nothing
		End If
		
	End Function
	
	
	
	Public Function SetPassword(ByVal P As String, Optional ByVal m As Object = 2) As String
		'  /******************************************************************\
		'  **   Función que se encarga de encriptar o desenciptar cadenas de texto
		'  **   P. Salida:
		'  **             SetPassword: Cadena obtenida
		'  **   Control Versiones: (Versión, Nombre, fecha, modificación)
		'  **   ------------------------------------------------------------------------------------------
		'  **    1.0    Igor Rementeria       19/04/2006       Creación de la función
		'  \******************************************************************/
		
		Dim r As Object
		Dim e As Object
		Dim A As Object
		
		For r = 1 To Len(P)
			'UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object A. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			A = Asc(Mid(P, r, 1))
			'UPGRADE_WARNING: Couldn't resolve default property of object m. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object r. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object e. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			e = e & Chr(A Xor 255 - r * m)
		Next 
		'UPGRADE_WARNING: Couldn't resolve default property of object e. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SetPassword = e
		
	End Function
	
	
	
	Public Function LeeTablaINI(ByRef Seccion As String, ByRef Clave As String) As String
		'  /******************************************************************\
		'  **   Función que se encarga de obtener un valor de la tabla INI
		'  **   P. Salida:
		'  **             LeeTablaINI: valor
		'  **   Control Versiones: (Versión, Nombre, fecha, modificación)
		'  **   ------------------------------------------------------------------------------------------
		'  **    1.0    Aketza Martinez       19/06/2006       Creación de la función
		'  \******************************************************************/
		
		Dim rst As ADODB.Recordset
		Dim sSQL As String
		
		rst = New ADODB.Recordset
		sSQL = "SELECT VALOR FROM INI WHERE SECCION='" & Seccion & "' and CLAVE='" & Clave & "'"
		rst = mfRecordset(conPasarela, sSQL)
		
		If Not rst.EOF Then
			LeeTablaINI = Trim(rst.Fields("VALOR").Value)
		Else
			LeeTablaINI = "NOTFOUND"
		End If
		
	End Function
	
	
	Public Sub Insertar_Error(ByRef frm As System.Windows.Forms.Form, ByRef strAmbito As String, ByRef strDescripcion As String, ByRef strLugar As String)
		
		Dim strSQL As String
		
		On Error GoTo Insertar_Error
		
		'#### DS66 Busca texto en la SQL que lanza
		If InStr(strDescripcion, "no tiene parámetros") <> 0 Then
			System.Windows.Forms.Application.DoEvents()
		End If
		'####
		
		strSQL = "INSERT INTO ERRORES (PROCEDIMIENTO,USERID,AMBITO, DESCRIPCION, LUGAR) VALUES ('" & gNomProcCorto & "', '" & strUserId & "', '" & strAmbito & "', '" & strDescripcion & "', '" & strLugar & "')"
		'Ultima buena strSQL = "INSERT INTO ERRORES (PROCEDIMIENTO,USERID,AMBITO, DESCRIPCION) VALUES ('" & gNomProcCorto & "', '" & strUserId & "', '" & strAmbito & "', '" & strDescripcion & "')"
		'strSQL = "INSERT INTO ERRORES (PROCEDIMIENTO,AMBITO, DESCRIPCION, USERID) VALUES ('" & gNomProcCorto & "', '" & strAmbito & "', '" & strDescripcion & "','')"
		'*************************************
		mfExecute(conPasarela, strSQL)
		Exit Sub
		
Insertar_Error: 
		MsgBox("error al insertar")
	End Sub
	
	Public Function ObtenerNombre(ByRef strTexto As String) As String
		Dim strAux As String
		Dim strAux2 As String
		Dim intPos As Short
		Dim intPosAnt As Short
		Dim intAux As Short
		Dim intBarra As Short
		
		strTexto = Trim(strTexto)
		intAux = 4
		strTexto = Trim(strTexto)
Repetir: 
		strAux = ""
		intPos = 1
		intPosAnt = 1
		intBarra = InStr(intPos, strTexto, "/")
		If InStr(intPos, strTexto, " ") <> 0 Then
			Do While intPos < Len(strTexto)
				If Len(Trim(Mid(strTexto, intPos, intAux))) > 2 And InStr(1, Trim(Mid(strTexto, intPos, intAux)), " ") = 0 And (InStr(1, Trim(Mid(strTexto, intPos + 1)), " ") + Len(Mid(strTexto, 1, intPos))) > intPosAnt Then
					'quitamos preposiciones, determinantes y demás palabras que no interesen
					If Trim(Mid(strTexto, intPos, intAux)) <> "del" And Trim(Mid(strTexto, intPos, intAux)) <> "con" And Trim(Mid(strTexto, intPos, intAux)) <> "por" And Trim(Mid(strTexto, intPos, intAux)) <> "sin" And Trim(Mid(strTexto, intPos, intAux)) <> "las" And Trim(Mid(strTexto, intPos, intAux)) <> "los" Then
						strAux = strAux & Trim(Mid(strTexto, intPos, intAux))
					End If
				End If
				If intPosAnt < intBarra And InStr(intPos, strTexto, " ") + 1 > intBarra Then
					intPos = InStr(intPos, strTexto, "/") + 1
				Else
					intPos = InStr(intPos, strTexto, " ") + 1
				End If
				intBarra = InStr(intPos, strTexto, "/") + 1
				If intPosAnt > intPos Then
					Exit Do
				End If
				intPosAnt = intPos
			Loop 
			If Len(strAux) + Len(strAux2) > 25 And intAux <> 3 Then
				intAux = 3
				GoTo Repetir
			End If
		End If
		strAux2 = Mid(strTexto, intPosAnt)
		strAux = strAux & strAux2
		strAux = Replace(strAux, "/", "")
		strAux = Replace(strAux, "(", "")
		strAux = Replace(strAux, ")", "")
		'Inicio Jonathan Prieto 31/01/2011: Que no devuelva más de 24 caracteres, para que el valor después no pase de 25.
		If Len(strAux) > 24 Then
			strAux = Mid(strAux, 1, 24)
		End If
		'Fin Jonathan Prieto 31/01/2011
		strAux = QuitarAcentos(strAux)
		strAux = UCase(strAux)
		ObtenerNombre = strAux
		
	End Function
	Public Function QuitarAcentos(ByRef strTexto As String) As String
		
		strTexto = Replace(strTexto, "á", "a")
		strTexto = Replace(strTexto, "é", "e")
		strTexto = Replace(strTexto, "í", "i")
		strTexto = Replace(strTexto, "ó", "o")
		strTexto = Replace(strTexto, "ú", "u")
		strTexto = Replace(strTexto, "à", "a")
		strTexto = Replace(strTexto, "è", "e")
		strTexto = Replace(strTexto, "ì", "i")
		strTexto = Replace(strTexto, "ò", "o")
		strTexto = Replace(strTexto, "ù", "u")
		strTexto = Replace(strTexto, "ä", "a")
		strTexto = Replace(strTexto, "ë", "e")
		strTexto = Replace(strTexto, "ï", "i")
		strTexto = Replace(strTexto, "ö", "o")
		strTexto = Replace(strTexto, "ü", "u")
		strTexto = Replace(strTexto, "â", "a")
		strTexto = Replace(strTexto, "ê", "e")
		strTexto = Replace(strTexto, "î", "i")
		strTexto = Replace(strTexto, "ô", "o")
		strTexto = Replace(strTexto, "û", "u")
		strTexto = Replace(strTexto, "Á", "a")
		strTexto = Replace(strTexto, "É", "e")
		strTexto = Replace(strTexto, "Í", "i")
        'strTexto = Replace(strTexto, "Ó", "o")
		strTexto = Replace(strTexto, "Ú", "u")
		strTexto = Replace(strTexto, "À", "a")
		strTexto = Replace(strTexto, "È", "e")
		strTexto = Replace(strTexto, "Ì", "i")
		strTexto = Replace(strTexto, "Ò", "o")
		strTexto = Replace(strTexto, "Ù", "u")
		strTexto = Replace(strTexto, "Ä", "a")
		strTexto = Replace(strTexto, "Ë", "e")
		strTexto = Replace(strTexto, "Ï", "i")
		strTexto = Replace(strTexto, "Ö", "o")
		strTexto = Replace(strTexto, "Ü", "u")
		strTexto = Replace(strTexto, "Â", "a")
		strTexto = Replace(strTexto, "Ê", "e")
		strTexto = Replace(strTexto, "Î", "i")
        'strTexto = Replace(strTexto, "Ô", "o")
		strTexto = Replace(strTexto, "Û", "u")
		QuitarAcentos = strTexto
		
	End Function
	
	
	
	Public Function msProcedimientoAlmacenado(ByVal StoredProc As String, ByVal conexion As ADODB.Connection, ByVal arrParam As Object) As Object
		On Error GoTo procErr
		Dim sql As String
		Dim comando As ADODB.Command
		Dim Param As ADODB.Parameter
		Dim I As Integer
		Dim j As Integer
		comando = New ADODB.Command
		comando.CommandText = StoredProc
		comando.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc
		For I = 0 To UBound(arrParam)
			'UPGRADE_WARNING: Couldn't resolve default property of object arrParam(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Param = comando.CreateParameter(arrParam(I, 1), ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 50, arrParam(I, 0))
			comando.Parameters.Append(Param)
		Next I
		comando.let_ActiveConnection(conexion)
		j = 0
		comando.Execute()
		'UPGRADE_NOTE: Object comando may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		comando = Nothing
		Exit Function
procErr: 
		If j < 5 Then
			j = j + 1
			Resume 
		End If
	End Function
	
	
	Public Sub msLimpiar(ByVal tabla As String, ByVal campo As String, ByVal procedimiento As String, ByVal conexion As ADODB.Connection)
		
		Dim arrParam(2, 1) As String
		
		arrParam(0, 0) = tabla
		arrParam(0, 1) = "tabla"
		arrParam(1, 0) = campo
		arrParam(1, 1) = "campo"
		arrParam(2, 0) = procedimiento
		arrParam(2, 1) = "flujo"
		msProcedimientoAlmacenado("SP_LIMPIAR", conexion, arrParam)
		'UPGRADE_NOTE: Erase was upgraded to System.Array.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		System.Array.Clear(arrParam, 0, arrParam.Length)
		
	End Sub
	
	Public Function mfInserta_ACCIONES(ByVal O_N1 As Integer, ByVal O_N2 As Integer, ByVal O_N3 As Integer, ByVal O_N4 As Integer, ByVal O_N5 As Integer, ByVal O_Acc As Integer, ByVal Id_Acc As Integer, ByVal Nom_Acc As String, ByVal Num_Acc As Integer, ByVal Tip_Acc As String, ByVal Path As String, ByVal Num_Sec As Integer, ByVal Di_Id As Integer, ByVal NOMBRE As String, ByVal metoFly As String, ByVal NombreCM As String) As Integer
		
		
		On Error GoTo procErr
		
		Dim strSQL As String
		Dim rows As Integer
		
		Dim arrParam(17, 1) As String
		
		If Path = "JUMP285" Or Path = "JUMP284" Then
			System.Windows.Forms.Application.DoEvents()
		End If
		
		If metoFly = "" Then metoFly = "S"
		strSQL = "INSERT INTO ACCIONES_DI(PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,"
		strSQL = strSQL & "ORDEN_ACC,ID_ACCION,NOM_ACCION,NUM_ACCION, TIPO_ACCION, PATH_HIDRA, NUM_SEQ, DI_ID, NOMBRE, METOFLY, NOMBRE_CM) VALUES ("
		strSQL = strSQL & "'" & gNomProcCorto & "'," & O_N1 & ", " & O_N2 & ", " & O_N3 & ", " & O_N4 & ", " & O_N5 & ","
		strSQL = strSQL & O_Acc & ", " & Id_Acc & ", '" & Nom_Acc & "', " & Num_Acc & ", '" & Tip_Acc & "',"
		strSQL = strSQL & "'" & Path & "', " & Num_Sec & ", " & Di_Id & ", '" & NOMBRE & "', '" & metoFly & "', '" & NombreCM & "')"
		mfExecute(conPasarela, strSQL,  , rows)
		
		If rows = 0 Then
			GoTo procErr
		End If
		'''''
		'''''   arrParam(0, 0) = gNomProcCorto
		'''''   arrParam(0, 1) = "NOMPROC"
		'''''   arrParam(1, 0) = O_N1
		'''''   arrParam(1, 1) = "O1"
		'''''   arrParam(2, 0) = O_N2
		'''''   arrParam(2, 1) = "O2"
		'''''   arrParam(3, 0) = O_N3
		'''''   arrParam(3, 1) = "O3"
		'''''   arrParam(4, 0) = O_N4
		'''''   arrParam(4, 1) = "O4"
		'''''   arrParam(5, 0) = O_N5
		'''''   arrParam(5, 1) = "O5"
		'''''   arrParam(6, 0) = O_Acc
		'''''   arrParam(6, 1) = "OACC"
		'''''   arrParam(7, 0) = Id_Acc
		'''''   arrParam(7, 1) = "ID_ACC "
		'''''   arrParam(8, 0) = Nom_Acc
		'''''   arrParam(8, 1) = "NOMACC"
		'''''   arrParam(9, 0) = Num_Acc
		'''''   arrParam(9, 1) = "NUM"
		'''''   arrParam(10, 0) = Tip_Acc
		'''''   arrParam(10, 1) = "TIPO"
		'''''   arrParam(11, 0) = Path
		'''''   arrParam(11, 1) = "PATH"
		'''''   arrParam(12, 0) = Num_Sec
		'''''   arrParam(12, 1) = "SEQ INT"
		'''''   arrParam(13, 0) = Di_Id
		'''''   arrParam(13, 1) = "DI_ID"
		'''''   arrParam(14, 0) = nombre
		'''''   arrParam(14, 1) = "NOMBRE"
		'''''   arrParam(15, 0) = metoFly
		'''''   arrParam(15, 1) = "FLY"
		'''''   arrParam(16, 0) = NombreCM
		'''''   arrParam(16, 1) = "NOMBRE_CM"
		'''''   arrParam(17, 0) = rows
		'''''   arrParam(17, 1) = "FILAS"
		'''''   msProcedimientoAlmacenado "INSERTA_ACCIONES_DI", conPasarela, arrParam
		
		
		
		mfInserta_ACCIONES = rows
		Exit Function
procErr: 
		logError(mfInserta_ACCIONES & "-" & Err.Description)
		
	End Function
	
	
	
	Public Function mfInserta_PROPIEDADES(ByVal O_N1 As Integer, ByVal O_N2 As Integer, ByVal O_N3 As Integer, ByVal O_N4 As Integer, ByVal O_N5 As Integer, ByVal Id As Integer, ByVal NOM As String, ByVal TIPO As String, ByVal PLAZT1 As String, ByVal PLAZT2 As String, ByVal NIVEL As String, ByVal BLOQ As String, ByVal RAM As String, ByVal PERSINT As String) As Integer
		
		
		On Error GoTo procErr
		
		Dim strSQL As String
		Dim rows As Integer
		
		If O_N1 & "-" & O_N2 & "-" & O_N3 & "-" & O_N4 & "-" & O_N5 = "3-1-0-0-0" Then
			System.Windows.Forms.Application.DoEvents()
		End If
		
		strSQL = "INSERT INTO PROPIEDADES_DI (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3, ORDEN_N4,ORDEN_N5,ID_DIAGRAMA,"
		strSQL = strSQL & "NOM_DIAGRAMA,TIPO_DIAGRAMA,PLAZTIP1_DI,PLAZTIP2_DI,NIVELTRAM_DI, INDBLOQ_DI,INDRAM_DI, INDPERSINT) VALUES ("
		strSQL = strSQL & "'" & gNomProcCorto & "'," & O_N1 & "," & O_N2 & "," & O_N3 & "," & O_N4 & "," & O_N5 & ","
		strSQL = strSQL & Id & ",'" & NOM & "','" & TIPO & "','" & PLAZT1 & "','" & PLAZT2 & "',"
		strSQL = strSQL & "'" & NIVEL & "','" & BLOQ & "','" & IIf(RAM <> "", "S", "N") & "','" & PERSINT & "')"
		mfExecute(conPasarela, strSQL,  , rows)
		If rows = 0 Then
			GoTo procErr
		End If
		mfInserta_PROPIEDADES = rows
		Exit Function
procErr: 
		logError(mfInserta_PROPIEDADES & "-" & Err.Description)
		
	End Function
	
	
	Public Function fObtenerCodigoTramiteUsuario(ByVal pstrModelo As String) As Integer
		'   /******************************************************************\
		'   **  Obtiene el código del Trámite Usuario para el modelo informado
		'   **  Parámetros Entrada:
		'           pstrModelo: Modelo de Corporate seleccionado
		'   **  Parámetros Salida:
		'   **      String: Código del Trámite Usuario (LU_ID) de la tabla CW_LOOKUP de la bbdd Corporate
		'   **  Control Versiones: (Versión, Nombre, fecha, modificación)
		'   **  ------------------------------------------------------------------------------------------
		'   **  1.0     J. Prieto   16/05/2013  Creación de la función
		'   \******************************************************************/
		
		On Error GoTo Err_fObtenerCodigoTramiteUsuario
		
		Dim strSQL As String
		Dim rstLookup As ADODB.Recordset
		Dim strCodTramUsu As String
		
		Const strTramUsu As String = "TRU"
		Const strTramUsuDesc As String = "%TRÁMITE%USUARIO%"
		
		If Trim(pstrModelo) = vbNullString Then
			GoTo Err_fObtenerCodigoTramiteUsuario
		End If
		
		strSQL = "SELECT LU_ID " & "FROM CW_LOOKUP " & "WHERE MODEL_NAME='" & Trim(pstrModelo) & "' " & "AND (LU_ABBREVIATION='" & strTramUsu & "' OR UPPER(LU_NAME) LIKE '" & strTramUsuDesc & "')"
		rstLookup = mfRecordset(conDP4, strSQL)
		If Not rstLookup.EOF Then
			If Trim(rstLookup.Fields("LU_ID").Value) <> vbNullString Then
				strCodTramUsu = Trim(rstLookup.Fields("LU_ID").Value)
			End If
		Else
			strCodTramUsu = "0"
		End If
		
		GoTo Fin_fObtenerCodigoTramiteUsuario
		
Err_fObtenerCodigoTramiteUsuario: 
		strCodTramUsu = vbNullString
		logError("fObtenerCodigoTramiteUsuario: No se ha podido obtener el código del Trámite Usuario. -" & Err.Description)
		
Fin_fObtenerCodigoTramiteUsuario: 
		fObtenerCodigoTramiteUsuario = CInt(strCodTramUsu)
		'UPGRADE_NOTE: Object rstLookup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstLookup = Nothing
	End Function
End Module