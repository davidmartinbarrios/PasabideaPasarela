using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class DiagramRepository : IDiagramRepository
    {
        private readonly DBContext DB;
        private readonly DBContext DBPasarela;

        public DiagramRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
            this.DBPasarela = new DBContext(Settings.BD_PASARELA);
        }
        public struct Table
        {
            public const string _Name = "DIAGRAM";
            public const string PROCEDIMIENTO = "PROCEDIMIENTO";
            public const string ORDEN_N1 = "ORDEN_N1";
            public const string ORDEN_N2 = "ORDEN_N2";
            public const string ORDEN_N3 = "ORDEN_N3";
            public const string ORDEN_N4 = "ORDEN_N4";
            public const string ORDEN_N5 = "ORDEN_N5";
            public const string CAT_DIAGRAMA = "CAT_DIAGRAMA";
            public const string NOMBRE = "NOMBRE";
            public const string USERDEFINED = "USERDEFINED";
            public const string NIVEL = "NIVEL";
            public const string ARBOL = "ARBOL";
            public const string PLAZOTIPO1 = "PLAZOTIPO1";
            public const string PLAZOTIPO2 = "PLAZOTIPO2";
            public const string NIV_TRAMIT = "NIV_TRAMIT";
            public const string BLOQUEO_EXP = "BLOQUEO_EXP";
            public const string UNION_RAMAS = "UNION_RAMAS";
            public const string TRAMIT_SIMUL = "TRAMIT_SIMUL";
            public const string TRAM_OCULTO = "TRAM_OCULTO";
            public const string IND_VALORVAR = "IND_VALORVAR";
            public const string VUELTA_ATRAS = "VUELTA_ATRAS";
            public const string NOMBRE_TRAM = "NOMBRE_TRAM";

            //Querys
            public const string DeleteByProcedimiento = "DELETE FROM [DIAGRAMA] WHERE PROCEDIMIENTO = '{0}'";
            public const string GetAll = "SELECT TOP  10 DI_ID, DI_NAME, DI_TYPE, ANO_ID, ANO_TABNR  FROM [erwin_evolve].[dbo].[DIAGRAM]";
            public const string GetAByDIID = "SELECT* FROM DIAGRAM WHERE MODEL_NAME='{0}' AND DI_ID = {1}";

            public const string GetAByDIModelName = "DECLARE @ModelName sysname = N'{0}'; " + //ARTEZ
                                                    "DECLARE @Procedimiento int = 1904;" +
                                                    "DECLARE @DiID int = {1};" + // 3
                                                    "DECLARE @DiAnoID int = 67890;" +
                                                    "WITH DirectReports AS (/* Semilla: trámites = SHAPE que referencian a PROCESS */" +
                                                    "SELECT PDiID = @DiID, DiID = S.DI_ID, PAnoID = @DiAnoID, AnoID = S.ANO_ID, " +
                                                    "Categ = CASE WHEN D.ANO_TABNR = 8953 THEN 'P' " +
                                                    "WHEN D.ANO_TABNR = 9096 THEN 'R' ELSE 'O' END, " +
                                                    "Seq     = S.SH_SEQ, shy     = S.SH_Y, " +
                                                    "shx     = S.SH_X, PrName  = P.PR_NAME, " +
                                                    "UserDefined = P.USERDEFINED, [Level] = 1, " +
                                                    "ARBOL   = CAST(RIGHT('000000' + CAST(S.SH_SEQ AS varchar(6)), 6) AS varchar(1024)) " +
                                                    "FROM dbo.SHAPE S " +
                                                    "JOIN dbo.DIAGRAM D ON D.DI_ID  = S.DI_ID  AND D.MODEL_NAME = @ModelName " +
                                                    "JOIN dbo.PROCESS P ON P.PR_ID  = S.ANO_ID AND P.MODEL_NAME = @ModelName " +
                                                    "WHERE S.MODEL_NAME = @ModelName AND S.DI_ID      = @DiID " +
                                                    "UNION ALL " +
                                                    "SELECT DR.PDiID, S2.DI_ID, DR.PAnoID, S2.ANO_ID, DR.Categ, S2.SH_SEQ, S2.SH_Y, S2.SH_X, " +
                                                    "P2.PR_NAME, P2.USERDEFINED, DR.[Level] + 1, CAST(DR.ARBOL + '.' + RIGHT('000000'+CAST(S2.SH_SEQ AS varchar(6)),6) AS varchar(1024)) " +
                                                    "FROM DirectReports DR " +
                                                    "JOIN dbo.JOINER J2 ON J2.DI_ID = DR.DiID AND J2.SH_SEQ_FROM = DR.Seq AND J2.MODEL_NAME = @ModelName " +
                                                    "JOIN dbo.SHAPE  S2 ON S2.DI_ID = J2.DI_ID AND S2.SH_SEQ     = J2.SH_SEQ_TO AND S2.MODEL_NAME = @ModelName " +
                                                    "JOIN dbo.PROCESS P2 ON P2.PR_ID = S2.ANO_ID AND P2.MODEL_NAME = @ModelName )," +
                                                    "Orden AS ( " +
                                                    "SELECT dr.*, ROW_NUMBER() OVER (ORDER BY dr.shy DESC, dr.shx ASC, dr.Seq) AS ORDEN_N1, " +
                                                    "CAST(0 AS int) AS ORDEN_N2, CAST(0 AS int) AS ORDEN_N3, " +
                                                    "CAST(0 AS int) AS ORDEN_N4, CAST(0 AS int) AS ORDEN_N5, " +
                                                    "dr.UserDefined AS UDxml " +
                                                    "FROM DirectReports dr ), " +
                                                    "Flags AS ( " +
                                                    "SELECT o.*, " +
                                                    "o.UDxml.value('(/UD/PlazoTipo1/text())[1]'      ,'varchar(20)') AS PLAZOTIPO1, " +
                                                    "o.UDxml.value('(/UD/PlazoTipo2/text())[1]'      ,'varchar(20)') AS PLAZOTIPO2, " +
                                                    "o.UDxml.value('(/UD/NivTramit/text())[1]'       ,'varchar(10)') AS NIV_TRAMIT, " +
                                                    "o.UDxml.value('(/UD/BloqueoExp/text())[1]'      ,'varchar(5)')  AS BLOQUEO_EXP, " +
                                                    "o.UDxml.value('(/UD/UnionRamas/text())[1]'      ,'varchar(5)')  AS UNION_RAMAS, " +
                                                    "o.UDxml.value('(/UD/TramitSimul/text())[1]'     ,'varchar(5)')  AS TRAMIT_SIMUL, " +
                                                    "o.UDxml.value('(/UD/TramOculto/text())[1]'      ,'varchar(5)')  AS TRAM_OCULTO, " +
                                                    "o.UDxml.value('(/UD/IndValorVar/text())[1]'     ,'varchar(5)')  AS IND_VALORVAR, " +
                                                    "o.UDxml.value('(/UD/VueltaAtras/text())[1]'     ,'varchar(5)')  AS VUELTA_ATRAS " +
                                                    "FROM Orden o )" +
                                                    "SELECT @Procedimiento as PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, " +
                                                    "Categ as CAT_DIAGRAMA, PrName as NOMBRE, CONVERT(varchar(max), UserDefined) as USERDEFINED, " +
                                                    "[Level] as NIVEL, ARBOL, PLAZOTIPO1, PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS, " +
                                                    "TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR, VUELTA_ATRAS, PrName as NOMBRE_TRAM " +
                                                    "FROM Flags " +
                                                    "ORDER BY shy DESC, shx ASC;";

            public const string InsertDiagramIntoPasarela = "INSERT INTO DIAGRAMA (PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, CAT_DIAGRAMA, NOMBRE, USERDEFINED, NIVEL, ARBOL, PLAZOTIPO1, " +
                                                            "PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS, TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR, VUELTA_ATRAS, NOMBRE_TRAM ) " +
                                                            "VALUES ('{0}',{1},{2},{3},{4},{5},'{6}','{7}','{8}',{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},'{20}')";



            public const string InsertDiagram1 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC, NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram2 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA, EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram3 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1 , ID_DIAG_N2, ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram4 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , ORDEN_N4 , , , , , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram5 = "*************************************INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,INDPARA,INDJUMP,INDCANCEL,NOMBRE,SALIDAS, DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES ( ' ', ORDEN_N1 , ORDEN_N2 , ORDEN_N3 , ORDEN_N4 , ORDEN_N5 , ID_DIAG_N1 , ID_DIAG_N2 , ID_DIAG_N3 , ID_DIAG_N4 , ID_DIAG_N5 , ID_DIAGRAMA , ' CAT_DIAGRAMA ', ID_PADRE , NUM_DIAGRAMA ,' EXPLOTA_ACC ', NUM_SEQ ,' I";
            public const string InsertDiagram6 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1, ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram7 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram8 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram9 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ, NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', ,0,0,0,0, ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ , ' PR_NAME ', , ' ', ' ', ' ')";
            public const string InsertDiagram10 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC, NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram11 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA, EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram12 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1 , ID_DIAG_N2, ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram13 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , ORDEN_N4 , , , , , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram14 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, ID_DIAG_N1, ID_DIAGRAMA, CAT_DIAGRAMA, ID_PADRE, NUM_DIAGRAMA, EXPLOTA_ACC, NUM_SEQ, NOMBRE, DI_ID, CODFASE, CODSFASE, DATFASUB) VALUES ( ' ', , 0, 0, 0, 0, ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ , ' PR_NAME ', , ' ', ' ', ' ')";
            public const string InsertDiagram15 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1, ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram16 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram17 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram18 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ, NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', ,0,0,0,0, ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ , ' PR_NAME ', , ' ', ' ', ' ')";
            public const string InsertDiagram19 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC, NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram20 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA, EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID , ' ', ' ', ' ')";
            public const string InsertDiagram21 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1 , ID_DIAG_N2, ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , , , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram22 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5, ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3,ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA, ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID,CODFASE,CODSFASE,DATFASUB) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , ORDEN_N4 , , , , , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID ,' ', ' ', ' ')";
            public const string InsertDiagram23 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, ID_DIAG_N1, ID_DIAGRAMA, CAT_DIAGRAMA, ID_PADRE, NUM_DIAGRAMA, EXPLOTA_ACC, NUM_SEQ, NOMBRE, DI_ID, CODFASE, CODSFASE, DATFASUB) VALUES ( ' ', , 0, 0, 0, 0, ANO_ID , ANO_ID , ' ', , , 'S' 'N' , SH_SEQ , ' PR_NAME ', , ' ', ' ', ' ')";
            public const string InsertDiagram24 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1, ID_DIAG_N2,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , 0,0,0, , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram25 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , , ,0,0, , , ANO_ID , ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";
            public const string InsertDiagram26 = "INSERT INTO DIAGRAMA (PROCEDIMIENTO,ORDEN_N1,ORDEN_N2,ORDEN_N3,ORDEN_N4,ORDEN_N5,ID_DIAG_N1,ID_DIAG_N2,ID_DIAG_N3, ID_DIAG_N4,ID_DIAG_N5,ID_DIAGRAMA,CAT_DIAGRAMA,ID_PADRE,NUM_DIAGRAMA,EXPLOTA_ACC,NUM_SEQ,NOMBRE,DI_ID) VALUES (' ', , ORDEN_N2 , ORDEN_N3 , , 0, , , , ANO_ID , 0, ANO_ID ,' ', , , 'S' 'N' , SH_SEQ ,' PR_NAME ', DI_ID )";

            public const string Update = "UPDATE DIAGRAMA SET ID_DIAGRAMA = {0}, NUM_SEQ = {1} WHERE PROCEDIMIENTO = ' ' AND NOMBRE = 'Arranque Expediente'";

            public const string GetAByDIIDTree = @"
DECLARE @ModelName sysname = N'{0}';
DECLARE @DiID int = {1};
DECLARE @Procedimiento int = {1};
DECLARE @DiAnoID int;

SELECT @DiAnoID = D.ANO_ID
FROM dbo.DIAGRAM D
WHERE D.MODEL_NAME = @ModelName
  AND D.DI_ID = @DiID;

WITH DirectReports AS
(
    SELECT
        PDiID = @DiID,
        DiID = S.DI_ID,
        PAnoID = @DiAnoID,
        AnoID = S.ANO_ID,
        Categ = CASE 
                    WHEN D.ANO_TABNR = 8953 THEN 'P'
                    WHEN D.ANO_TABNR = 9096 THEN 'R'
                    ELSE 'O'
                END,
        Seq = S.SH_SEQ,
        shy = S.SH_Y,
        shx = S.SH_X,
        PrName = P.PR_NAME,
        UserDefined = P.USERDEFINED,
        [Level] = 1,
        ARBOL = CAST(RIGHT('000000' + CAST(S.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM dbo.SHAPE S
    JOIN dbo.DIAGRAM D 
        ON D.DI_ID = S.DI_ID 
       AND D.MODEL_NAME = @ModelName
    JOIN dbo.PROCESS P 
        ON P.PR_ID = S.ANO_ID 
       AND P.MODEL_NAME = @ModelName
    WHERE S.MODEL_NAME = @ModelName
      AND S.DI_ID = @DiID

    UNION ALL

    SELECT
        DR.PDiID,
        S2.DI_ID,
        DR.PAnoID,
        S2.ANO_ID,
        DR.Categ,
        S2.SH_SEQ,
        S2.SH_Y,
        S2.SH_X,
        P2.PR_NAME,
        P2.USERDEFINED,
        DR.[Level] + 1,
        CAST(DR.ARBOL + '.' + RIGHT('000000' + CAST(S2.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM DirectReports DR
    JOIN dbo.JOINER J2 
        ON J2.DI_ID = DR.DiID
       AND J2.SH_SEQ_FROM = DR.Seq
       AND J2.MODEL_NAME = @ModelName
    JOIN dbo.SHAPE S2 
        ON S2.DI_ID = J2.DI_ID
       AND S2.SH_SEQ = J2.SH_SEQ_TO
       AND S2.MODEL_NAME = @ModelName
    JOIN dbo.PROCESS P2 
        ON P2.PR_ID = S2.ANO_ID
       AND P2.MODEL_NAME = @ModelName
),
Orden AS
(
    SELECT
        dr.*,
        ROW_NUMBER() OVER (ORDER BY dr.shy DESC, dr.shx ASC, dr.Seq) AS ORDEN_N1,
        CAST(0 AS int) AS ORDEN_N2,
        CAST(0 AS int) AS ORDEN_N3,
        CAST(0 AS int) AS ORDEN_N4,
        CAST(0 AS int) AS ORDEN_N5,
        dr.UserDefined AS UDxml
    FROM DirectReports dr
),
Flags AS
(
    SELECT
        o.*,
        o.UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(20)') AS PLAZOTIPO1,
        o.UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(20)') AS PLAZOTIPO2,
        o.UDxml.value('(/UD/NivTramit/text())[1]',  'varchar(10)') AS NIV_TRAMIT,
        o.UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(5)')  AS BLOQUEO_EXP,
        o.UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(5)')  AS UNION_RAMAS,
        o.UDxml.value('(/UD/TramitSimul/text())[1]','varchar(5)')  AS TRAMIT_SIMUL,
        o.UDxml.value('(/UD/TramOculto/text())[1]', 'varchar(5)')  AS TRAM_OCULTO,
        o.UDxml.value('(/UD/IndValorVar/text())[1]','varchar(5)')  AS IND_VALORVAR,
        o.UDxml.value('(/UD/VueltaAtras/text())[1]','varchar(5)')  AS VUELTA_ATRAS
    FROM Orden o
)
SELECT
    @Procedimiento AS PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    Categ AS CAT_DIAGRAMA,
    PrName AS NOMBRE,
    CONVERT(varchar(max), UserDefined) AS USERDEFINED,
    [Level] AS NIVEL,
    ARBOL,
    PLAZOTIPO1,
    PLAZOTIPO2,
    NIV_TRAMIT,
    BLOQUEO_EXP,
    UNION_RAMAS,
    TRAMIT_SIMUL,
    TRAM_OCULTO,
    IND_VALORVAR,
    VUELTA_ATRAS,
    PrName AS NOMBRE_TRAM
FROM Flags
ORDER BY shy DESC, shx ASC
OPTION (MAXRECURSION 10000);
";
        }

        private Diagram Parse_DataRow_To_POCO(DataRow dr)
        {
            Diagram ret = new Diagram
            {
                PROCEDIMIENTO = dr[Table.PROCEDIMIENTO].ToString(),
                ORDEN_N1 = int.Parse(dr[Table.ORDEN_N1].ToString()),
                ORDEN_N2 = int.Parse(dr[Table.ORDEN_N2].ToString()),
                ORDEN_N3 = int.Parse(dr[Table.ORDEN_N3].ToString()),
                ORDEN_N4 = int.Parse(dr[Table.ORDEN_N4].ToString()),
                ORDEN_N5 = int.Parse(dr[Table.ORDEN_N5].ToString()),
                CAT_DIAGRAMA = string.IsNullOrEmpty(dr[Table.CAT_DIAGRAMA].ToString()) ? null : dr[Table.CAT_DIAGRAMA].ToString(),
                NOMBRE = string.IsNullOrEmpty(dr[Table.NOMBRE].ToString()) ? null : dr[Table.NOMBRE].ToString(),
                USERDEFINED = string.IsNullOrEmpty(dr[Table.USERDEFINED].ToString()) ? null : dr[Table.USERDEFINED].ToString(),
                NIVEL = int.Parse(dr[Table.NIVEL].ToString()),
                ARBOL = string.IsNullOrEmpty(dr[Table.PLAZOTIPO1].ToString()) ? null : dr[Table.PLAZOTIPO1].ToString(),
                PLAZOTIPO1 = string.IsNullOrEmpty(dr[Table.PLAZOTIPO1].ToString()) ? null : dr[Table.PLAZOTIPO1].ToString(),
                PLAZOTIPO2 = string.IsNullOrEmpty(dr[Table.PLAZOTIPO2].ToString()) ? null : dr[Table.PLAZOTIPO2].ToString(),
                NIV_TRAMIT = string.IsNullOrEmpty(dr[Table.NIV_TRAMIT].ToString()) ? null : dr[Table.NIV_TRAMIT].ToString(),
                BLOQUEO_EXP = string.IsNullOrEmpty(dr[Table.BLOQUEO_EXP].ToString()) ? null : dr[Table.BLOQUEO_EXP].ToString(),
                UNION_RAMAS = string.IsNullOrEmpty(dr[Table.UNION_RAMAS].ToString()) ? null : dr[Table.UNION_RAMAS].ToString(),
                TRAMIT_SIMUL = string.IsNullOrEmpty(dr[Table.TRAMIT_SIMUL].ToString()) ? null : dr[Table.TRAMIT_SIMUL].ToString(),
                TRAM_OCULTO = string.IsNullOrEmpty(dr[Table.TRAM_OCULTO].ToString()) ? null : dr[Table.TRAM_OCULTO].ToString(),
                IND_VALORVAR = string.IsNullOrEmpty(dr[Table.IND_VALORVAR].ToString()) ? null : dr[Table.IND_VALORVAR].ToString(),
                VUELTA_ATRAS = string.IsNullOrEmpty(dr[Table.VUELTA_ATRAS].ToString()) ? null : dr[Table.VUELTA_ATRAS].ToString(),
                NOMBRE_TRAM = string.IsNullOrEmpty(dr[Table.NOMBRE_TRAM].ToString()) ? null : dr[Table.NOMBRE_TRAM].ToString(),
            };
            return ret;
        }

        public ResponseBase<int> DeleteByProcedimiento(string Procedimiento)
        {
            Logger.Info("Accedemos a la función DeleteByProcedimiento con el procedimiento: " + Procedimiento);
            ResponseBase<int> response = new ResponseBase<int>();
            //IList<Diagram> retList = new List<Diagram>();
            this.DBPasarela.ClearParameters();
            this.DBPasarela.Type = CommandType.Text;

            this.DBPasarela.SqlStatment = string.Format(Table.DeleteByProcedimiento, Procedimiento);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DBPasarela.SqlStatment + " y procedemos a borrar los registros del procedimiento: " + Procedimiento);
                this.DBPasarela.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DBPasarela.CloseConnection();
            }
            return response;
        }
        public ResponseBase<IList<Diagram>> GetAll()
        {
            ResponseBase<IList<Diagram>> response = new ResponseBase<IList<Diagram>>();
            IList<Diagram> retList = new List<Diagram>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Diagram diagram;

                foreach (DataRow dr in listDT.Rows)
                {
                    diagram = Parse_DataRow_To_POCO(dr);
                    retList.Add(diagram);
                }
                response.Data = retList;

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);

            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }
            return response;
        }

        public ResponseBase<IList<Diagram>> GetByDIModelName(string ModelName, int id)
        {
            Logger.Info("Accedemos a la función GetByDIModelName con los siguientes valores: ModelName: " + ModelName + " e id: " + id);
            ResponseBase<IList<Diagram>> response = new ResponseBase<IList<Diagram>>();
            IList<Diagram> retList = new List<Diagram>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetAByDIModelName,ModelName, id);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Diagram diagram;

                foreach (DataRow dr in listDT.Rows)
                {
                    diagram = Parse_DataRow_To_POCO(dr);
                    retList.Add(diagram);
                }
                response.Data = retList;
                Logger.Info("Obtenemos " + retList.Count + " diagramas asociados al ModelName: " + ModelName + " e id: " + id);

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);

            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }
            return response;
        }

        public ResponseBase<Diagram> GetByDIID(string ModelName, int id)
        {
            ResponseBase<Diagram> response = new ResponseBase<Diagram>();
            Diagram ret = null;
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetAByDIID, ModelName, id);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                if (listDT.Rows.Count > 0)
                {
                    DataRow dr = listDT.Rows[0];
                    ret = Parse_DataRow_To_POCO(dr);
                    response.Data = ret;
                }

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);

            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }
            return response;
        }

        public ResponseBase<int> InsertDiagramList(IEnumerable<Diagram> diagrams)
        {
            Logger.Info("Accedemos a InsertDiagramList para insertar los registros ");
            string strQuery = string.Empty;

            ResponseBase<int> response = new ResponseBase<int>();

            using (var connection = new SqlConnection(DBPasarela.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var obj in diagrams)
                        {
                            using (var command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.CommandType = CommandType.Text;

                                command.CommandText = string.Format(
                                    Table.InsertDiagramIntoPasarela,
                                    obj.PROCEDIMIENTO, obj.ORDEN_N1, obj.ORDEN_N2, obj.ORDEN_N3, obj.ORDEN_N4, obj.ORDEN_N5,
                                    obj.CAT_DIAGRAMA, obj.NOMBRE, obj.USERDEFINED, obj.NIVEL,
                                    ConvertEmptyToNull(obj.ARBOL), ConvertEmptyToNull(obj.PLAZOTIPO1),
                                    ConvertEmptyToNull(obj.PLAZOTIPO2), ConvertEmptyToNull(obj.NIV_TRAMIT),
                                    ConvertEmptyToNull(obj.BLOQUEO_EXP), ConvertEmptyToNull(obj.UNION_RAMAS),
                                    ConvertEmptyToNull(obj.TRAMIT_SIMUL), ConvertEmptyToNull(obj.TRAM_OCULTO),
                                    ConvertEmptyToNull(obj.IND_VALORVAR), ConvertEmptyToNull(obj.VUELTA_ATRAS),
                                    obj.NOMBRE_TRAM
                                );
                                strQuery = command.CommandText;
                                Logger.Info( "Ejecutamos la query: " + command.CommandText);
                                command.ExecuteNonQuery();
                            }
                        }
                        Logger.Info("Se han insertado correctamente " + diagrams.Count() + " registros. Procedemos a hacer commit de la transacción.");
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Logger.Info("Se ha producido un error durante la inserción de los registros. " + Environment.NewLine + "Procedemos a hacer rollback de la transacción." +
                            "El Error se ha producido al ejecutar esta query: " + strQuery);  
                        Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                        transaction.Rollback();
                        response.Data = int.MinValue;
                    }
                }
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram1(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagramIntoPasarela.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DBPasarela.ClearParameters();
            this.DBPasarela.Type = CommandType.Text;

            this.DBPasarela.SqlStatment = string.Format(Table.InsertDiagramIntoPasarela, obj.PROCEDIMIENTO, obj.ORDEN_N1, obj.ORDEN_N2, obj.ORDEN_N3, obj.ORDEN_N4, obj.ORDEN_N5, obj.CAT_DIAGRAMA, obj.NOMBRE,
                obj.USERDEFINED, obj.NIVEL, ConvertEmptyToNull(obj.ARBOL), ConvertEmptyToNull(obj.PLAZOTIPO1), ConvertEmptyToNull(obj.PLAZOTIPO2), ConvertEmptyToNull(obj.NIV_TRAMIT),
                ConvertEmptyToNull(obj.BLOQUEO_EXP), ConvertEmptyToNull(obj.UNION_RAMAS), ConvertEmptyToNull(obj.TRAMIT_SIMUL), ConvertEmptyToNull(obj.TRAM_OCULTO), ConvertEmptyToNull(obj.IND_VALORVAR),
                ConvertEmptyToNull(obj.VUELTA_ATRAS), obj.NOMBRE_TRAM);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DBPasarela.SqlStatment);
                this.DBPasarela.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                Logger.Info("Cerramos la conexión a la base de datos.");
                this.DBPasarela.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram2(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram2.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram2, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram3(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram3.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram3, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram4(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram4.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram4, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram5(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram5.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram5, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram6(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram6.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram6, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram7(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram7.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram7, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram8(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram8.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram8, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram9(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram9.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram9, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram10(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram10.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram10, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram11(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram11.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram11, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram12(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram12.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram12, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram13(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram13.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram13, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram14(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram14.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram14, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram15(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram15.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram15, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram16(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram16.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram16, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram17(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram17.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram17, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram18(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram18.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram18, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram19(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram19.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram19, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram20(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram20.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram20, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram21(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram21.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram21, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram22(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram22.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram22, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram23(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram23.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram23, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram24(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram24.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram24, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram25(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram25.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram25, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> InsertDiagram26(Diagram obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagram26.");

            ResponseBase<int> response = new ResponseBase<int>();
            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(Table.InsertDiagram26, 1, 1, "prueba", 1);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                Logger.Info("La query se ha ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = int.MinValue;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
        public ResponseBase<int> Update(Diagram obj)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            response.Data = obj.NIVEL;

            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment =
                string.Format(Table.Update, obj.NIVEL, obj.PROCEDIMIENTO);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                this.DB.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);

            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }

        private static string ConvertEmptyToNull(string input)
        {
            return string.IsNullOrEmpty(input) ? "NULL" : $"'{input}'";
        }



        public ResponseBase<IList<Diagram>> GetByDIId(int diId)
        {
            ResponseBase<IList<Diagram>> response = new ResponseBase<IList<Diagram>>();
            IList<Diagram> retList = new List<Diagram>();

            this.DB.ClearParameters();
            this.DB.Type = CommandType.Text;

            this.DB.SqlStatment = string.Format(
                Table.GetAByDIIDTree,
                "ARTEZ",
                diId
            );

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);

                DataTable listDT = this.DB.GetDataTable();

                foreach (DataRow dr in listDT.Rows)
                {
                    Diagram diagram = Parse_DataRow_To_POCO(dr);
                    retList.Add(diagram);
                }

                response.Data = retList;

                if (response.Query_Result.Query_HasError())
                {
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
    }
}
