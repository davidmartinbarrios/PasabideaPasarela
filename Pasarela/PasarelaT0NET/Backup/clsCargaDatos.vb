Option Strict Off
Option Explicit On
Friend Class clsCargaDatos
	
	'Tipos de objetos
	Private Const cintTipoObjProceso As Short = 8953
	
	'Etiquetas de propiedades de trámite
	Private Const cstrEtiquetaPropPlazoTipo1 As String = "APLAZOTIPO1"
	Private Const cstrEtiquetaPropPlazoTipo2 As String = "BPLAZOTIPO2"
	Private Const cstrEtiquetaPropNivTramit As String = "BNIVELTRAMITACIÓN"
	Private Const cstrEtiquetaPropBloqueoExp As String = "DINDICADORBLOQUEOEXPEDIENTE"
	Private Const cstrEtiquetaPropUnionRamas As String = "EINDICADORUNIÓNRAMASPARALELAS"
	Private Const cstrEtiquetaPropTramSimultanea As String = "GINDICADORTRAMITACIÓNSIMULTÁNEA"
	Private Const cstrEtiquetaPropTramOculto As String = "HTRÁMITEOCULTO"
	Private Const cstrEtiquetaPropIndValorVar As String = "AINDICADORVALORVARIABLE"
	Private Const cstrEtiquetaPropVueltaAtrasURP As String = "GVUELTAATRÁSCONURP"
	Private Const cstrEtiquetaPropNombreTram As String = "ANOMBRETRÁMITE"
	
	'Tipos de datos de propiedades de trámite
	Private Const cstrTipoDatoPropNumero As Short = 3
	Private Const cstrTipoDatoPropTexto As Short = 3
	Private Const cstrTipoDatoPropCheck As Short = 5
	Private Const cstrTipoDatoPropLista As Short = 9
	
	'Todas las posibles categorías de un trámite
	Private Const cstrCategoriasTramite As String = "'Trámite de Inicio','Trámite','Trámite automático','Trámite general'" & ",'Ramificación','Trámite General Automático'"
	
	'Todos los campos de la tabla DIAGRAMA, que voy a insertar, necesarios para la validación
	Private Const cstrCamposInsertDIAGRAMA As String = "PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5" & ",CAT_DIAGRAMA,NOMBRE,USERDEFINED,NIVEL,ARBOL,PLAZOTIPO1,PLAZOTIPO2,NIV_TRAMIT,BLOQUEO_EXP,UNION_RAMAS" & ",TRAMIT_SIMUL,TRAM_OCULTO,IND_VALORVAR,VUELTA_ATRAS,NOMBRE_TRAM"
	
	'Todos los tipos de datos de todos los campos de la tabla DIAGRAMA, que voy a insertar, necesarios para la validación
	Private Const cstrTipoDatosCamposDIAGRAMA As String = "1,2,2,2,2,2,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1" '1=cadena, 2=número
	
	'Tabla DIAGRAMA
	Private Const cstrNombreTablaDIAGRAMA As String = "DIAGRAMA"
	
	'Nombres de campos
	Private Const cstrCampoPROCEDIMIENTO As String = "PROCEDIMIENTO"
	Private Const cstrCampoCM_Level As String = "Level"
	Private Const cstrCampoCM_USERDEFINED As String = "UserDefined"
	Private Const cstrCampoCM_Categ As String = "Categ"
	Private Const cstrCampoCM_Prname As String = "Prname"
	Private Const cstrCampoCM_Arbol As String = "ro"
	Private Const cstrCampoXML_Properties As String = "properties"
	Private Const cstrCampoLookup_ID As String = "LU_ID"
	Private Const cstrCampoLookup_Name As String = "LU_NAME"
	
	'Diagrama de procedimiento
	Private cintDiagramaProc As Short
	
	'Colección con todos los registros de la tabla CW_LOOKUP, pública porque se utiliza también desde el basValidaciones
	Public cColLOOKUP As Collection
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>Función que lanza la carga de tablas de Pasarela</summary>
	'<param name="intDiagramaProc">ID del diagrama a cargar</param>
	'<return> Boolean:
	'&lt;li&gt; True: Se han cargado correctamente los datos
	'&lt;li&gt; False: No se han podido cargar correctamente los datos
	'</return>
	'<remarks>
	'&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
	'</remarks>
	'</comment>
	'--------------------------------------------------------------------------------------
	Public Function fCargaDatosCorporate(ByVal intDiagramaProc As Short) As Boolean
		Dim blnResul As Boolean
		
		On Error GoTo ERROR_Renamed
		
		cintDiagramaProc = intDiagramaProc
		
		'Cargo la colección de LOOKUP
		fCargaColeccionLOOKUP()
		
		'Limpio las tablas de Pasarela
		fLimpiaDatos()
		
		'Cargo la tabla DIAGRAMAS
		blnResul = fCargaTablaDiagramas
		If blnResul = False Then
			fCargaDatosCorporate = False
			Exit Function
		End If
		
		fCargaConectores()
		
		fCargaAcciones()
		
		fCargaUTs()
		
		fCargaDatosCorporate = blnResul
		Exit Function
		
ERROR_Renamed: 
		fCargaDatosCorporate = False
		If Err.Source <> "" Then
			Err.Raise(Err.Number, Err.Source, Err.Description)
		Else
			Err.Raise(Err.Number, "fCargaDatosCorporate", Err.Description)
		End If
		
	End Function
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>Limpia las tablas de Pasarela para el procedimiento tratado</summary>
	'<remarks>
	'&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
	'</remarks>
	'</comment>
	'--------------------------------------------------------------------------------------
	Private Function fLimpiaDatos() As Object
		
		On Error GoTo ERROR_Renamed
		
		'Limpio la tabla DIAGRAMA para el Procedimiento
		msLimpiar(cstrNombreTablaDIAGRAMA, cstrCampoPROCEDIMIENTO, gNomProcCorto, conPasarela)
		Exit Function
		
ERROR_Renamed: 
		If Err.Source <> "" Then
			Err.Raise(Err.Number, Err.Source, Err.Description)
		Else
			Err.Raise(Err.Number, "fLimpiaDatos", Err.Description)
		End If
		
	End Function
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>
	'Función que lanza la carga la tabla DIAGRAMAS con los datos necesarios para la validación, que son los siguientes campos:
	'PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, CAT_DIAGRAMA, NOMBRE, USERDEFINED, NIVEL, ARBOL
	'PLAZOTIPO1, PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS, TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR
	'VUELTA_ATRAS y NOMBRE_TRAM
	'</summary>
	'<return> Boolean:
	'&lt;li&gt; True: Se han cargado correctamente los datos
	'&lt;li&gt; False: No se han podido cargar correctamente los datos
	'</return>
	'<remarks>
	'&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
	'</remarks>
	'</comment>
	'--------------------------------------------------------------------------------------
	Private Function fCargaTablaDiagramas() As Boolean
		Dim strSQL As String
		Dim rstTramites As ADODB.Recordset
		Dim rstProp As ADODB.Recordset
		Dim intMax1 As Short
		Dim intMax2 As Short
		Dim intMax3 As Short
		Dim intMax4 As Short
		Dim intMax5 As Short
		Dim intDia1 As Short
		Dim intDia2 As Short
		Dim intDia3 As Short
		Dim intDia4 As Short
		Dim intDia5 As Short
		Dim colTramites As Collection
		Dim tipoTramite As clsTipoTramite
		Dim contTramites As Short
		Dim strDatoPlazoTipo1 As String
		Dim strDatoPlazoTipo2 As String
		Dim strDatoNivTramit As String
		Dim strDatoBloqueoExp As String
		Dim strDatoUnionRamas As String
		Dim strDatoTramSimultanea As String
		Dim strDatoTramOculto As String
		Dim strDatoIndValorVar As String
		Dim strDatoVueltaAtrasURP As String
		Dim strDatoNombreTram As String
		Dim arrayTramites(,) As String
		
		On Error GoTo ERROR_Renamed
		
		'Obtenemos por consulta todos los trámites/ramificaciones del procedimiento, mas el Nivel, el Arbol, el campo
		'USERDEFINED (XML), del que extraeremos el resto de datos de propiedades y los campos de Unidades de Tramitación
		'ID, Tipo y Nombre de la Organización donde está dibujado
		strSQL = "WITH DirectReports (PDiID, PSeq, DiID, PAnoID, AnoID, Categ, Seq, shy, shx, PrName, UserDefined, Level, ro) AS(" & " SELECT A.DI_ID, A.SH_SEQ, A.DI_ID, NULL , A.Ano_id, d.lu_name,  A.SH_SEQ, A.SH_Y, A.SH_X, B.PR_NAME, B.USERDEFINED, 0 AS Level, CAST(RIGHT('0000'+CAST(a.ANO_ID AS VARCHAR(4)),4) AS VARCHAR(250)) AS ro" & " FROM SHAPE A, PROCESS B, CW_PROP_TYPE C, CW_LOOKUP D" & " WHERE B.PR_TYPE = D.LU_ID AND D.PPT_ID = C.PPT_ID AND C.PPT_FIELD_NAME = 'PR_TYPE' AND D.LU_NAME IN (" & cstrCategoriasTramite & ")" & " AND A.ANO_TABNR=" & cintTipoObjProceso & " AND A.ANO_ID = B.PR_ID AND A.MODEL_NAME = '" & strModelo & "' AND B.MODEL_NAME = '" & strModelo & "' AND C.MODEL_NAME = '" & strModelo & "' AND D.MODEL_NAME = '" & strModelo & "'" & " Union All SELECT DR.PDiid, DR.Seq, E.DI_ID, E.ANO_ID, D.ANO_ID, c.Lu_name, D.SH_SEQ, D.SH_Y, D.SH_X,  A.PR_NAME , A.USERDEFINED, Level + 1, CAST(DR.ro + '-' + RIGHT('0000'+CAST(A.PR_ID AS VARCHAR(4)),4) AS VARCHAR(250)) AS ro" & " FROM PROCESS A INNER JOIN CW_LOOKUP C ON A.PR_TYPE = C.LU_ID AND A.MODEL_NAME = '" & strModelo & "' AND C.MODEL_NAME = '" & strModelo & "'" & " AND C.LU_NAME IN (" & cstrCategoriasTramite & ")" & " INNER JOIN CW_PROP_TYPE B ON B.PPT_ID = C.PPT_ID AND B.PPT_FIELD_NAME = 'PR_TYPE' AND B.MODEL_NAME = '" & strModelo & "'" & " INNER JOIN SHAPE D ON D.ANO_ID = A.PR_ID AND D.MODEL_NAME = '" & strModelo & "' AND D.ANO_TABNR = 8953" & " INNER JOIN DIAGRAM E ON E.DI_ID = D.DI_ID AND E.MODEL_NAME = '" & strModelo & "' AND E.DI_TYPE <> 1 AND E.ANO_TABNR = 8953 AND E.MODEL_NAME = '" & strModelo & "' AND D.ANO_ID <> E.ANO_ID" & " INNER JOIN DirectReports as DR ON E.ANO_ID = DR.AnoID)" & " SELECT row_number() over (order by (select 1))as nro, PDiID, Pseq, DiID, Panoid, AnoID, Categ, Seq, shy, shx, Prname, UserDefined, Level, ro, UT_OuID, UT_OuType, UT_OuName" & " FROM DirectReports dr LEFT OUTER JOIN (SELECT c.DI_ID as UT_DiID, OU_ID as UT_OuID, OU_TYPE as UT_OuType, OU_NAME as UT_OuName, B.PR_ID as UT_PrID, C.SH_SEQ as UT_Seq" & " FROM PROCESS B, SHAPE C, SHAPE D, ORGANIZATION E" & " WHERE B.PR_ID = c.ANO_ID AND C.DI_ID = D.DI_ID AND D.ANO_ID = E.OU_ID AND C.SH_X > D.SH_X AND C.SH_X < D.SH_WIDTH + D.SH_X AND C.SH_Y < D.SH_Y AND C.SH_Y > D.SH_Y - D.SH_HEIGHT AND B.MODEL_NAME = '" & strModelo & "' AND C.MODEL_NAME = '" & strModelo & "' AND D.MODEL_NAME = '" & strModelo & "' AND E.MODEL_NAME = '" & strModelo & "'" & " AND C.ANO_TABNR=" & cintTipoObjProceso & ") UT ON UT_DiID=DiID AND UT_PrID=AnoID AND UT_Seq=Seq" & " WHERE PDiID = " & cintDiagramaProc & " ORDER BY SHY DESC, SHX ASC"
		rstTramites = mfRecordset(conDP4, strSQL)
		If Not rstTramites.EOF Then
			
			colTramites = New Collection
			
			'======================================================================================================================
			'Recorro todos los trámites obtenidos en la consulta, consigo los datos necesarios de propiedades y cargo la colección
			rstTramites.MoveFirst()
			
			'Obtengo el mayor ORDEN_N1 para que todos los trámites a insertar sean mayores
			strSQL = "SELECT MAX(ORDEN_N1) FROM DIAGRAMA WHERE PROCEDIMIENTO='" & gNomProcCorto & "'"
			rstProp = mfRecordset(conPasarela, strSQL)
			intMax1 = Val(rstProp.Fields(0).Value & "")
			rstProp.Close()
			
			While Not rstTramites.EOF
				
				tipoTramite = New clsTipoTramite
				
				'Genero valor para los campos ORDEN_Nx ya que forman parte de la clave de la tabla DIAGRAMA
				intMax1 = intMax1 + 1
				intMax2 = 0
				intMax3 = 0
				intMax4 = 0
				intMax5 = 0
				
				'            Select Case rstTramites(cstrCampoCM_Level)
				'                Case "0"
				'                    intMax1 = intMax1 + 1
				'                Case "1"
				'                    intMax2 = intMax2 + 1
				'                Case "2"
				'                    intMax3 = intMax3 + 1
				'                Case "3"
				'                    intMax4 = intMax4 + 1
				'                Case "4"
				'                    intMax5 = intMax5 + 1
				'            End Select
				
				'Consigo los datos de las propiedades de cada trámite
				'Plazo Tipo 1
				strDatoPlazoTipo1 = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropPlazoTipo1, cstrTipoDatoPropNumero)
				'Plazo Tipo 2
				strDatoPlazoTipo2 = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropPlazoTipo2, cstrTipoDatoPropNumero)
				'Nivel Tramitación
				strDatoNivTramit = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropNivTramit, cstrTipoDatoPropNumero)
				'Bloqueo Expediente
				strDatoBloqueoExp = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropBloqueoExp, cstrTipoDatoPropCheck)
				'Unión Ramas Paralelas
				strDatoUnionRamas = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropUnionRamas, cstrTipoDatoPropCheck)
				'Tramitación Simultánea
				strDatoTramSimultanea = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropTramSimultanea, cstrTipoDatoPropCheck)
				'Trámite Oculto Consulta
				strDatoTramOculto = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropTramOculto, cstrTipoDatoPropCheck)
				'Indicador Valor Variable
				strDatoIndValorVar = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropIndValorVar, cstrTipoDatoPropCheck)
				'Vuelta atrás con URP
				strDatoVueltaAtrasURP = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropVueltaAtrasURP, cstrTipoDatoPropNumero)
				'Nombre Trámite
				strDatoNombreTram = fBuscaDatoXML(rstTramites.Fields(cstrCampoCM_USERDEFINED).Value, cstrEtiquetaPropNombreTram, cstrTipoDatoPropLista)
				
				'Cargo la colección de Trámites con los datos que necesito
				With tipoTramite
					.procedimiento = gNomProcCorto
					.ORDEN_N1 = intMax1
					.ORDEN_N2 = intMax2
					.ORDEN_N3 = intMax3
					.ORDEN_N4 = intMax4
					.ORDEN_N5 = intMax5
					.CAT_DIAGRAMA = Trim(rstTramites.Fields(cstrCampoCM_Categ).Value)
					.NOMBRE = Trim(rstTramites.Fields(cstrCampoCM_Prname).Value)
					.USERDEFINED = rstTramites.Fields(cstrCampoCM_USERDEFINED).Value
					.NIVEL = CShort(Trim(rstTramites.Fields(cstrCampoCM_Level).Value + 1))
					.ARBOL = Trim(rstTramites.Fields(cstrCampoCM_Arbol).Value)
					.PLAZOTIPO1 = strDatoPlazoTipo1
					.PLAZOTIPO2 = strDatoPlazoTipo2
					.NIV_TRAMIT = strDatoNivTramit
					.BLOQUEO_EXP = strDatoBloqueoExp
					.UNION_RAMAS = strDatoUnionRamas
					.TRAMIT_SIMUL = strDatoTramSimultanea
					.TRAM_OCULTO = strDatoTramOculto
					.IND_VALORVAR = strDatoIndValorVar
					.VUELTA_ATRAS = strDatoVueltaAtrasURP
					.NOMBRE_TRAM = strDatoNombreTram
				End With
				
				contTramites = contTramites + 1
				colTramites.Add(tipoTramite) ', contTramites
				
				rstTramites.MoveNext()
			End While
			
			'======================================================================================================================
			'Inserto todos los trámites, almacenados en la colección, en la tabla DIAGRAMAS
			
			ReDim arrayTramites(colTramites.Count(), 21)
			'Genero un array con todos los datos de la colección porque la función fCrearInsertSelect lo necesita así
			For contTramites = 1 To colTramites.Count()
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().procedimiento. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 1) = colTramites.Item(contTramites).procedimiento
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ORDEN_N1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 2) = colTramites.Item(contTramites).ORDEN_N1
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ORDEN_N2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 3) = colTramites.Item(contTramites).ORDEN_N2
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ORDEN_N3. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 4) = colTramites.Item(contTramites).ORDEN_N3
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ORDEN_N4. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 5) = colTramites.Item(contTramites).ORDEN_N4
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ORDEN_N5. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 6) = colTramites.Item(contTramites).ORDEN_N5
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().CAT_DIAGRAMA. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 7) = colTramites.Item(contTramites).CAT_DIAGRAMA
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().NOMBRE. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 8) = colTramites.Item(contTramites).NOMBRE
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().USERDEFINED. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 9) = colTramites.Item(contTramites).USERDEFINED
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().NIVEL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 10) = colTramites.Item(contTramites).NIVEL
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().ARBOL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 11) = colTramites.Item(contTramites).ARBOL
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().PLAZOTIPO1. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 12) = colTramites.Item(contTramites).PLAZOTIPO1
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().PLAZOTIPO2. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 13) = colTramites.Item(contTramites).PLAZOTIPO2
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().NIV_TRAMIT. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 14) = colTramites.Item(contTramites).NIV_TRAMIT
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().BLOQUEO_EXP. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 15) = colTramites.Item(contTramites).BLOQUEO_EXP
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().UNION_RAMAS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 16) = colTramites.Item(contTramites).UNION_RAMAS
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().TRAMIT_SIMUL. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 17) = colTramites.Item(contTramites).TRAMIT_SIMUL
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().TRAM_OCULTO. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 18) = colTramites.Item(contTramites).TRAM_OCULTO
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().IND_VALORVAR. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 19) = colTramites.Item(contTramites).IND_VALORVAR
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().VUELTA_ATRAS. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 20) = colTramites.Item(contTramites).VUELTA_ATRAS
				'UPGRADE_WARNING: Couldn't resolve default property of object colTramites().NOMBRE_TRAM. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				arrayTramites(contTramites, 21) = colTramites.Item(contTramites).NOMBRE_TRAM
			Next contTramites
			'La función fCrearInsertSelect genera una INSERT SELECT de todos los registros del array
			strSQL = fCrearInsertSelect(cstrNombreTablaDIAGRAMA, cstrCamposInsertDIAGRAMA, cstrTipoDatosCamposDIAGRAMA, arrayTramites)
			If strSQL = "" Then
				GoTo ERROR_Renamed
			End If
			mfExecute(conPasarela, strSQL)
			'======================================================================================================================
		End If
		
		CierraRS(rstTramites)
		CierraRS(rstProp)
		'UPGRADE_NOTE: Object rstTramites may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstTramites = Nothing
		'UPGRADE_NOTE: Object rstProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstProp = Nothing
		'UPGRADE_NOTE: Object colTramites may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		colTramites = Nothing
		'UPGRADE_NOTE: Object tipoTramite may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		tipoTramite = Nothing
		fCargaTablaDiagramas = True
		Exit Function
		
ERROR_Renamed: 
		'UPGRADE_NOTE: Object rstTramites may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstTramites = Nothing
		'UPGRADE_NOTE: Object rstProp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rstProp = Nothing
		'UPGRADE_NOTE: Object colTramites may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		colTramites = Nothing
		'UPGRADE_NOTE: Object tipoTramite may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		tipoTramite = Nothing
		fCargaTablaDiagramas = False
		If Err.Source <> "" Then
			Err.Raise(Err.Number, Err.Source, Err.Description)
		Else
			Err.Raise(Err.Number, "fCargaDatosCorporate", Err.Description)
		End If
		
	End Function
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>Función que devuelve el dato de la etiqueta (strEtiqueta) que contiene el campo XML (strCampoXML)</summary>
	'<param name="strCampoXML">XML en el que se busca</param>
	'<param name="strEtiqueta">Eqitueta que se busca en el XML</param>
	'<param name="intTipoDato">Tipo de dato: Numérico, Texto, Check ó Lista</param>
	'<return> String:
	'&lt;li&gt; Devuelve el dato contenido en el XML para la etiqueta buscada
	'</return>
	'<remarks>
	'&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
	'</remarks>
	'</comment>
	'--------------------------------------------------------------------------------------
	Public Function fBuscaDatoXML(ByVal strCampoXML As String, ByVal strEtiqueta As String, ByVal intTipoDato As Short) As String
		Dim xmlUserdefined As MSXML2.DOMDocument30
		Dim nodoXML As MSXML2.IXMLDOMNode
		Dim strDatoXML As String
		Dim contEtiquetas As Short
		
		On Error GoTo ERROR_Renamed
		
		'Genero un objeto XML con el contenido del strCampoXML
		xmlUserdefined = New MSXML2.DOMDocument30
		xmlUserdefined.loadXML(Trim(strCampoXML))
		nodoXML = xmlUserdefined.selectSingleNode(cstrCampoXML_Properties)
		'Busco el dato que contenga la strEtiqueta
		For contEtiquetas = 0 To nodoXML.childNodes.length - 1
			If Trim(nodoXML.childNodes(contEtiquetas).Attributes.Item(0).Text) = Trim(strEtiqueta) Then
				strDatoXML = Trim(nodoXML.childNodes(contEtiquetas).Text) 'dato encontrado, salgo del bucle
				Exit For
			End If
		Next contEtiquetas
		
		'Dependiendo del intTipoDato, tendré que realizar un tratamiento diferente
		Select Case intTipoDato
			'Numérico
			Case cstrTipoDatoPropNumero Or cstrTipoDatoPropTexto
				fBuscaDatoXML = strDatoXML
				'Texto
			Case cstrTipoDatoPropTexto
				fBuscaDatoXML = strDatoXML
				'Check
			Case cstrTipoDatoPropCheck
				'Devuelve S ó N
				If strDatoXML = "" Or strDatoXML = "0" Then
					'Si no encuentra el dato ó tiene valor 0 devuelve N
					fBuscaDatoXML = "N"
				ElseIf strDatoXML = "-1" Or strDatoXML = "1" Then 
					'Si tiene valor -1 ó 1 devuelve S
					fBuscaDatoXML = "S"
				End If
				'Lista
			Case cstrTipoDatoPropLista
				'Ha de buscar el valor en la tabla CW_LOOKUP
				If strDatoXML <> "" Then
					If cColLOOKUP.Count() > 0 And CShort(strDatoXML) > 0 Then
						'UPGRADE_WARNING: Couldn't resolve default property of object cColLOOKUP()(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						fBuscaDatoXML = cColLOOKUP.Item(CShort(strDatoXML))(1)
					End If
				End If
				
		End Select
		
		'UPGRADE_NOTE: Object xmlUserdefined may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		xmlUserdefined = Nothing
		'UPGRADE_NOTE: Object nodoXML may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		nodoXML = Nothing
		Exit Function
		
ERROR_Renamed: 
		fBuscaDatoXML = ""
		'UPGRADE_NOTE: Object xmlUserdefined may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		xmlUserdefined = Nothing
		'UPGRADE_NOTE: Object nodoXML may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		nodoXML = Nothing
		If Err.Source <> "" Then
			Err.Raise(Err.Number, Err.Source, Err.Description)
		Else
			Err.Raise(Err.Number, "fBuscaDatoXML", Err.Description)
		End If
		
	End Function
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>
	'Carga en una colección todos los registros de CW_LOOKUP, que mantendrá activo en memoria.
	'Rellena los ID's para que sean consecutivos y no existan saltos, así un ID ocupará exactamente la misma
	'posición en la colección, ésto es, el ID=5 ocupará la posición 5 de la colección (cColLOOKUP(5)(0) y cColLOOKUP(5)(1))
	'Estructura de la colección cColLOOKUP:
	'cColLOOKUP(1)(0) = Identificador (LU_ID)
	'cColLOOKUP(1)(1) = Nombre (LU_NAME)
	'</summary>
	'<remarks>
	'&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
	'</remarks>
	'</comment>
	'--------------------------------------------------------------------------------------
	Private Sub fCargaColeccionLOOKUP()
		Dim strSQL As String
		Dim rstLookup As ADODB.Recordset
		Dim contColLookup As Short
		Dim arrayLookup(1) As String
		
		On Error GoTo ERROR_Renamed
		
		'Obtengo todos los registros de la tabla CW_LOOKUP
		strSQL = "SELECT " & cstrCampoLookup_ID & "," & cstrCampoLookup_Name & " FROM CW_LOOKUP WHERE MODEL_NAME='" & strModelo & "' ORDER BY " & cstrCampoLookup_ID
		rstLookup = mfRecordset(conDP4, strSQL)
		If Not rstLookup.EOF Then
			cColLOOKUP = New Collection
			'Relleno los ID's que falten, para que la colección sea consecutiva y sin saltos
			contColLookup = 1
			While Not rstLookup.EOF
				If contColLookup < CShort(rstLookup.Fields(cstrCampoLookup_ID).Value) Then
					arrayLookup(0) = CStr(contColLookup)
					arrayLookup(1) = ""
				ElseIf contColLookup = CShort(rstLookup.Fields(cstrCampoLookup_ID).Value) Then 
					arrayLookup(0) = CStr(CShort(rstLookup.Fields(cstrCampoLookup_ID).Value))
					arrayLookup(1) = Trim(rstLookup.Fields(cstrCampoLookup_Name).Value)
					rstLookup.MoveNext()
				End If
				cColLOOKUP.Add(arrayLookup)
				contColLookup = contColLookup + 1
			End While
		End If
		
		CierraRS(rstLookup)
		Exit Sub
		
ERROR_Renamed: 
		'Si casca dejo la colección vacía
		For contColLookup = 1 To cColLOOKUP.Count()
			cColLOOKUP.Remove((contColLookup))
		Next contColLookup
		If Err.Source <> "" Then
			Err.Raise(Err.Number, Err.Source, Err.Description)
		Else
			Err.Raise(Err.Number, "fCargaColeccionLOOKUP", Err.Description)
		End If
		
	End Sub
	
	'--------------------------------------------------------------------------------------
	'<comment>
	'<summary>
    'Public Function fCrearInsertSelect(ByVal strTabla As String, ByVal strCampos As String, ByVal strTipoDatosCampos As String, ByRef arrayDatos(,) As String) As String
    '1) Se le deben informar los campos de la insert en strCampos, con el formato "CAMPO1,CAMPO2,CAMPO3", a modo de cabecera
    '2) strCampos y arrayDatos (en la segunda dimensión) deben tener el mismo orden de campos
    '3) Debe coincidir el orden de strCampos y arrayDatos (en la segunda dimensión)
    'Ej. El dato para el CAMPO5 de strCampos, será el que se encuentre en arrayDatos(fila,5)
    '4) Siempre tratará el arrayDatos desde base 1
    '5) strTipoDatosCampos debe tener los tipos de datos para los campos informados, en el mismo orden que strCampos
    '1: cadena texto, 2: número
    '</summary>
    '<param name="strTabla">Tabla donde se va a ejecutar la INSERT</param>
    '<param name="strCampos">Campos</param>
    '<param name="strTipoDatosCampos">Tipo de dato de cada uno de los campos</param>
    '<param name="arrayDatos()">Contenido de cada campo a insertar</param>
    '<return> String:
    '&lt;li&gt; Devuelve el dato contenido en el XML para la etiqueta buscada
    '</return>
    '<remarks>
    '&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
    '</remarks>
    '</comment>
    '--------------------------------------------------------------------------------------
    'End Function
    Public Function fCrearInsertSelect(ByVal strTabla As String, ByVal strCampos As String, ByVal strTipoDatosCampos As String, ByRef arrayDatos(,) As String) As String
        Dim contFila As Short
        Dim contCol As Short
        Dim strSQL As String
        Dim arrayCampos() As String
        Dim arrayTipoDatos() As String

        On Error GoTo ERROR_Renamed

        If UBound(arrayDatos) < 1 Then
            GoTo ERROR_Renamed
        End If
        If Trim(strTabla) = "" And Trim(strCampos) = "" Then
            GoTo ERROR_Renamed
        End If
        If Trim(strTipoDatosCampos) = "" Then
            GoTo ERROR_Renamed
        End If

        arrayCampos = Split(strCampos, ",") 'el arrayTipoDatos tendrá base 0
        arrayTipoDatos = Split(strTipoDatosCampos, ",") 'el arrayTipoDatos tendrá base 0

        If UBound(arrayCampos) = UBound(arrayTipoDatos) And UBound(arrayCampos) + 1 = UBound(arrayDatos, 2) Then

            strSQL = strSQL & "INSERT INTO " & strTabla & " (" & strCampos & ")"

            For contFila = 1 To UBound(arrayDatos)
                For contCol = 1 To UBound(arrayDatos, 2)
                    If contFila = 1 And contCol = 1 Then
                        Select Case arrayTipoDatos(contCol - 1)
                            Case "1" 'cadena texto
                                strSQL = strSQL & " SELECT '" & arrayDatos(contFila, contCol) & "' AS " & Trim(arrayCampos(contCol - 1))
                            Case "2" 'número
                                strSQL = strSQL & " SELECT " & arrayDatos(contFila, contCol) & " AS " & Trim(arrayCampos(contCol - 1))
                        End Select
                    ElseIf contFila > 1 And contCol = 1 Then
                        Select Case arrayTipoDatos(contCol - 1)
                            Case "1" 'cadena texto
                                strSQL = strSQL & " UNION SELECT '" & arrayDatos(contFila, contCol) & "' AS " & Trim(arrayCampos(contCol - 1))
                            Case "2" 'número
                                strSQL = strSQL & " UNION SELECT " & arrayDatos(contFila, contCol) & " AS " & Trim(arrayCampos(contCol - 1))
                        End Select
                    Else
                        Select Case arrayTipoDatos(contCol - 1)
                            Case "1" 'cadena texto
                                strSQL = strSQL & ",'" & arrayDatos(contFila, contCol) & "' AS " & Trim(arrayCampos(contCol - 1))
                            Case "2" 'número
                                strSQL = strSQL & "," & arrayDatos(contFila, contCol) & " AS " & Trim(arrayCampos(contCol - 1))
                        End Select
                    End If
                Next contCol
            Next contFila

            fCrearInsertSelect = strSQL
            Exit Function

        Else
            GoTo ERROR_Renamed
        End If

ERROR_Renamed:
        fCrearInsertSelect = ""
        If Err.Source <> "" Then
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Else
            Err.Raise(Err.Number, "fCrearInsertSelect", Err.Description)
        End If

    End Function

    '--------------------------------------------------------------------------------------
    '<comment>
    '<summary>'Función que lanza la carga la tabla CONECTORES'</summary>
    '<return> Boolean:
    '&lt;li&gt; True: Se han cargado correctamente los datos
    '&lt;li&gt; False: No se han podido cargar correctamente los datos
    '</return>
    '<remarks>
    '&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
    '</remarks>
    '</comment>
    '--------------------------------------------------------------------------------------
    Private Function fCargaConectores() As Object

        On Error GoTo ERROR_Renamed

        Exit Function

ERROR_Renamed:
        If Err.Source <> "" Then
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Else
            Err.Raise(Err.Number, "fCargaConectores", Err.Description)
        End If

    End Function

    '--------------------------------------------------------------------------------------
    '<comment>
    '<summary>'Función que lanza la carga la tabla ACCIONES'</summary>
    '<return> Boolean:
    '&lt;li&gt; True: Se han cargado correctamente los datos
    '&lt;li&gt; False: No se han podido cargar correctamente los datos
    '</return>
    '<remarks>
    '&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
    '</remarks>
    '</comment>
    '--------------------------------------------------------------------------------------
    Private Function fCargaAcciones() As Object

        On Error GoTo ERROR_Renamed

        Exit Function

ERROR_Renamed:
        If Err.Source <> "" Then
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Else
            Err.Raise(Err.Number, "fCargaAcciones", Err.Description)
        End If

    End Function

    '--------------------------------------------------------------------------------------
    '<comment>
    '<summary>'Función que lanza la carga la tabla UT'</summary>
    '<return> Boolean:
    '&lt;li&gt; True: Se han cargado correctamente los datos
    '&lt;li&gt; False: No se han podido cargar correctamente los datos
    '</return>
    '<remarks>
    '&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
    '</remarks>
    '</comment>
    '--------------------------------------------------------------------------------------
    Private Function fCargaUTs() As Object

        On Error GoTo ERROR_Renamed

        Exit Function

ERROR_Renamed:
        If Err.Source <> "" Then
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Else
            Err.Raise(Err.Number, "fCargaUTs", Err.Description)
        End If

    End Function

    '--------------------------------------------------------------------------------------
    '<comment>
    '<summary>Elimina de memoria las colecciones de datos almacenadas</summary>
    '<remarks>
    '&lt;li&gt; 1.0     J. Prieto   07/06/2011  Creación
    '</remarks>
    '</comment>
    '--------------------------------------------------------------------------------------
    Public Function fDescargaDatosCorporate() As Object

        On Error GoTo ERROR_Renamed

        'UPGRADE_NOTE: Object cColLOOKUP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        cColLOOKUP = Nothing

        Exit Function

ERROR_Renamed:
        If Err.Source <> "" Then
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Else
            Err.Raise(Err.Number, "fDescargaDatosCorporate", Err.Description)
        End If

    End Function
End Class