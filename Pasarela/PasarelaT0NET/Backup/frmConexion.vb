Option Strict Off
Option Explicit On
Friend Class frmConexion
	Inherits System.Windows.Forms.Form
	Dim DataLinkDP4 As MSDASC.DataLinks
	Dim DataLinkDB2 As MSDASC.DataLinks
	Dim DataLinkInf As MSDASC.DataLinks
	Dim DataLinkHid As MSDASC.DataLinks
	Dim DataLinkGEs As MSDASC.DataLinks
	Dim DataLinkWor As MSDASC.DataLinks
	Dim DataLinkMsj As MSDASC.DataLinks
	Dim DataLinkGene As MSDASC.DataLinks
	Dim strCadena As String
	'Inicio Jonathan Prieto 10/06/2011
	'Dim colRef        As Collection
	'Fin Jonathan Prieto 10/06/2011


    Dim blnSalir As Boolean = False

	Public Sub cmdAceptar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAceptar.Click
		
        'gsComprobarConexion(False)
		
	End Sub
	
	Private Sub cmdCancelar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancelar.Click
		
		Dim strSQL As String
		strSQL = "UPDATE USUARIOS SET ACTIVO=0 WHERE USERID='" & strUserId & "'"
		mfExecute(conPasarela, strSQL)
		
		End
		
	End Sub
	
	Private Sub CmdSqlStringDocu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringDocu.Click
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionHid.Text
		txtDocu.Text = DataLinkWor.PromptNew
	End Sub
	
	Private Sub CmdSqlStringGene_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringGene.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = Me.txtAccionesGene.Text
		txtAccionesGene.Text = DataLinkGene.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringGes_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringGes.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionHid.Text
		txtGestion.Text = DataLinkGEs.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringHid_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringHid.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionHid.Text
		txtConexionHid.Text = DataLinkHid.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringInf_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringInf.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionInfra.Text
		txtConexionInfra.Text = DataLinkInf.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringDB2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringDB2.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionDB2.Text
		txtConexionDB2.Text = DataLinkDB2.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringInfrT0_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringInfrT0.Click
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtInfrT0.Text
		txtInfrT0.Text = DataLinkMsj.PromptNew
	End Sub
	
	Private Sub CmdSqlStringMsj_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringMsj.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionHid.Text
		txtMsj.Text = DataLinkMsj.PromptNew
		
	End Sub
	
	Private Sub CmdSqlStringWord_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSqlStringWord.Click
		
		On Error Resume Next
		
		Dim Cnx As String
		
		Cnx = txtConexionHid.Text
		txtWord.Text = DataLinkWor.PromptNew
		
	End Sub
	
	Private Sub frmConexion_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim externo As Boolean = False


		DataLinkDP4 = New MSDASC.DataLinks
		DataLinkDB2 = New MSDASC.DataLinks
		DataLinkInf = New MSDASC.DataLinks
		DataLinkHid = New MSDASC.DataLinks
		DataLinkGEs = New MSDASC.DataLinks
		DataLinkWor = New MSDASC.DataLinks
		DataLinkMsj = New MSDASC.DataLinks
		DataLinkGene = New MSDASC.DataLinks
		ConexionesBBDD()
		mdiAncho = VB6.PixelsToTwipsX(Me.Width) + 100
		mdiAlto = VB6.PixelsToTwipsY(Me.Height) + 400
		bMinimizado = False
		frmMDI.Width = VB6.TwipsToPixelsX(mdiAncho)
		frmMDI.Height = VB6.TwipsToPixelsY(mdiAlto)
		bMinimizado = True
		frmMDI.Icon = Me.Icon
		frmMDI.Text = Me.Text
		'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		INIFile = My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".ini"
		Me.Top = 0
		Me.Left = 0
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Me.Enabled = False
		blnSalir = False
		conPasarela = New ADODB.Connection
		If ComprobarConexion(conPasarela, ReadIniFile(INIFile, "PASARELA", "Connection")) = False Then
			MsgBox("La cadena de conexión a Pasarela no es correcta o el servidor no se encuentra disponible. Revise el fichero INI")
			blnSalir = True
			Me.Enabled = True
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow 'vbNormal()
			GoTo procFin
		End If
		conDB2 = New ADODB.Connection
		If ComprobarConexion(conDB2, IIf(externo, ReadIniFile(INIFile, "DB2", "Connection"), Me.txtConexionDB2.Text)) = False Then
			MsgBox("La cadena de conexión a DB2 no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtConexionDB2.Focus()
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow

			GoTo procFin
		Else
			WriteIniFile(INIFile, "DB2", "Connection", txtConexionDB2.Text)
		End If
		conGene = New ADODB.Connection
		If ComprobarConexion(conGene, IIf(externo, ReadIniFile(INIFile, "GENERALES" & strFamil, "Connection"), Me.txtAccionesGene.Text)) = False Then
			MsgBox("La cadena de conexión a Acciones Generales no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtAccionesGene.Focus()
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
			GoTo procFin
		Else
			WriteIniFile(INIFile, "GENERALES" & strFamil, "Connection", txtAccionesGene.Text)
		End If
		conInfra = New ADODB.Connection
		If ComprobarConexion(conInfra, IIf(externo, ReadIniFile(INIFile, "GENERALAPLICACION" & strFamil, "Connection"), Me.txtConexionInfra.Text)) = False Then
			MsgBox("La cadena de conexión a la base de datos general de aplicación no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtConexionInfra.Focus()
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
			GoTo procFin
		Else
			WriteIniFile(INIFile, "GENERALAPLICACION" & strFamil, "Connection", txtConexionInfra.Text)
		End If
		conEspe = New ADODB.Connection
		If ComprobarConexion(conEspe, IIf(externo, ReadIniFile(INIFile, "ESPECIFICA" & strFamil, "Connection"), Me.txtConexionHid.Text)) = False Then
			MsgBox("La cadena de conexión a Acciones Específicas no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtConexionHid.Focus()
			'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = Cursors.Arrow
			GoTo procFin
		Else
			WriteIniFile(INIFile, "ESPECIFICA" & strFamil, "Connection", txtConexionHid.Text)
		End If
		conGestion = New ADODB.Connection
		If ComprobarConexion(conGestion, IIf(externo, ReadIniFile(INIFile, "GESTION" & strFamil, "Connection"), Me.txtGestion.Text)) = False Then
			MsgBox("La cadena de conexión a Acciones de Gestion no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtGestion.Focus()
			GoTo procFin
		Else
			WriteIniFile(INIFile, "GESTION" & strFamil, "Connection", txtGestion.Text)
		End If
		conWord = New ADODB.Connection
		If ComprobarConexion(conWord, IIf(externo, ReadIniFile(INIFile, "WORD" & strFamil, "Connection"), Me.txtWord.Text)) = False Then
			MsgBox("La cadena de conexión a Acciones de Word no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtWord.Focus()
			GoTo procFin
		Else
			WriteIniFile(INIFile, "WORD" & strFamil, "Connection", txtWord.Text)
		End If
		'## Ds66, Para la nueva BD DBT0DOCU
		conDocu = New ADODB.Connection
		If ComprobarConexion(conDocu, IIf(externo, ReadIniFile(INIFile, "DOCU" & strFamil, "Connection"), Me.txtDocu.Text)) = False Then
			MsgBox("La cadena de conexión a Acciones de DOCU no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			'Me.txtWord.SetFocus
			GoTo procFin
			'Else
			'WriteIniFile INIFile, "WORD" & strFamil, "Connection", txtWord
		End If
		'##
		conMSJ = New ADODB.Connection
		If ComprobarConexion(conMSJ, IIf(externo, ReadIniFile(INIFile, "MSJ" & strFamil, "Connection"), Me.txtMsj.Text)) = False Then
		'UPGRADE_ISSUE: Unable to determine which constant to upgrade System.Windows.Forms.Cursors.Default to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Me.txtMsj.Focus()
			GoTo procFin
		Else
			WriteIniFile(INIFile, "MSJ" & strFamil, "Connection", txtMsj.Text)
		End If
		'J.Lerma
		conInfrT0 = New ADODB.Connection
		If ComprobarConexion(conInfrT0, IIf(externo, ReadIniFile(INIFile, "INFRT0", "Connection"), Me.txtInfrT0.Text)) = False Then
			MsgBox("La cadena de conexión a INFRT0 no es correcta o el servidor no se encuentra disponible.")
			Me.Enabled = True
			Me.txtInfrT0.Focus()
			GoTo procFin
		Else
			WriteIniFile(INIFile, "INFRT0", "Connection", txtInfrT0.Text)
		End If
		'Fin J.Lerma
		Me.Enabled = True
		'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
		'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = Cursors.Arrow
		blnCon1 = True
		Me.Close()
		'Inicio Jonathan Prieto 10/06/2011
		'frmSelProc.Show 1
		frmSelProc.Show()
		'Fin Jonathan Prieto 10/06/2011
		GoTo procFin
		
procErr: 
		Resume procFin
		
procFin: 
		
	End Sub
	
	Private Sub ConexionesBBDD()
		
		Me.txtConexionDB2.Text = ReadIniFile(INIFile, "DB2", "Connection")
		Me.txtConexionInfra.Text = ReadIniFile(INIFile, "GENERALAPLICACION" & strFamil, "Connection")
		Me.txtConexionHid.Text = ReadIniFile(INIFile, "ESPECIFICA" & strFamil, "Connection")
		Me.txtGestion.Text = ReadIniFile(INIFile, "GESTION" & strFamil, "Connection")
		Me.txtWord.Text = ReadIniFile(INIFile, "WORD" & strFamil, "Connection")
		'## Ds66, Para la nueva BD DBT0DOCU
		Me.txtDocu.Text = ReadIniFile(INIFile, "DOCU" & strFamil, "Connection")
		'##
		Me.txtMsj.Text = ReadIniFile(INIFile, "MSJ" & strFamil, "Connection")
		Me.txtAccionesGene.Text = ReadIniFile(INIFile, "GENERALES" & strFamil, "Connection")
		'J.Lerma 13/03/2019
		Me.txtInfrT0.Text = ReadIniFile(INIFile, "INFRT0", "Connection")
		
	End Sub
	
	Private Sub frmConexion_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		On Error Resume Next
		
		'Inicio Jonathan Prieto 10/06/2011
		'Dim I As Long
		'
		'   For I = 1 To colRef.Count
		'      colRef.Remove 1
		'   Next I
		'   Set colRef = Nothing
		'Fin Jonathan Prieto 10/06/2011
		
	End Sub
End Class