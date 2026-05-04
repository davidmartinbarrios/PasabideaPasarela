using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Lantik.Pasarela.sqlRepository
{
    /// <summary>
    /// Transformador inicial ERWIN -> PASARELA_ARTEZ.
    ///
    /// Objetivo:
    ///   - Partir de un DI_ID de Erwin/Evolve.
    ///   - Leer estructura principal del procedimiento.
    ///   - Generar registros para las tablas intermedias de Pasarela:
    ///       DIAGRAMA
    ///       CONECTORES_DI
    ///       PROPIEDADES_DI
    ///       ACCIONES_DI
    ///       CONECTOR_ACC
    ///       PARAM_ACC
    ///
    /// NOTA IMPORTANTE:
    ///   Esta clase está pensada como reemplazo/orquestador limpio del comportamiento repartido
    ///   en msLec_Diagram_Proc, msLec_Conectores_DI, msLecTramRamif, msLec_Conectores_ACC,
    ///   msLecAccTram y mslecParamAcc. ES NECESARIO HACER LIMPIEZA Y ORDENAR MÉTODOS Y CLASES.
    ///
    ///   La parte más fiable ya queda implementada: DIAGRAMA + conectores base + propiedades base.
    ///   ACCIONES_DI, CONECTOR_ACC y PARAM_ACC quedan implementadas con SQL base razonable,
    ///   pero probablemente tendrás que ajustar columnas según tu modelo real de PASARELA_ARTEZ.
    /// </summary>
    public class ErwinPasarelaArtezTransformer
    {
        private readonly DBContext DBErwin;
        private readonly DBContext DBPasarela;

        public ErwinPasarelaArtezTransformer()
        {
            DBErwin = new DBContext(Settings.BD_DP4);
            DBPasarela = new DBContext(Settings.BD_PASARELA);
        }

        public ResponseBase<int> GenerarDesdeDiId(int diId, string procedimiento, string modelName = "ARTEZELI")
        {
            var response = new ResponseBase<int>();

            try
            {
                //int procedimiento = diId;

                DeleteByProcedimiento(procedimiento.ToString());

                var diagramas = LeerDiagramas(diId, procedimiento, modelName);
                  foreach (DataRow dr in diagramas.Rows)
                    InsertDiagrama(dr);

                var conectoresDi = LeerConectoresDi(diId, procedimiento, modelName);
                foreach (DataRow dr in conectoresDi.Rows)
                    InsertConectorDi(dr);

                var propiedades = LeerPropiedadesDi(diId, procedimiento, modelName);
                foreach (DataRow dr in propiedades.Rows)
                    InsertPropiedadDi(dr);

                var acciones = LeerAccionesDi(diId, procedimiento, modelName);
                foreach (DataRow dr in acciones.Rows)
                    InsertAccionDi(dr);

                var conectoresAcc = LeerConectoresAcc(diId, procedimiento, modelName);
                foreach (DataRow dr in conectoresAcc.Rows)
                    InsertConectorAcc(dr);

                var parametros = LeerParamAcc(diId, procedimiento, modelName);
                foreach (DataRow dr in parametros.Rows)
                    InsertParamAcc(dr);

                response.Data = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                response.Data = int.MinValue;
                throw;
            }
            finally
            {
                DBErwin.CloseConnection();
                DBPasarela.CloseConnection();
            }

            return response;
        }

        private void DeleteByProcedimiento(string procedimiento)
        {
            ExecutePasarela("DELETE FROM PARAM_ACC WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM CONECTOR_ACC WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM ACCIONES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM PROPIEDADES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM CONECTORES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM DIAGRAMA WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.Addpply("@PROCEDIMIENTO", procedimiento));

        }

        private DataTable LeerDiagramas(int diId, string procedimiento, string modelName)
        {
            string sql = @"
DECLARE @ModelName sysname = @P_MODEL_NAME;
DECLARE @DiID int = @P_DI_ID;
DECLARE @Procedimiento nvarchar(255) = @P_PROCEDIMIENTO;
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
      AND S.ANO_TABNR = 8953

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
     AND S2.ANO_TABNR = 8953
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
        TRY_CAST(dr.UserDefined AS xml) AS UDxml
    FROM DirectReports dr
),
Flags AS
(
    SELECT
        o.*,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(20)') END AS PLAZOTIPO1,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(20)') END AS PLAZOTIPO2,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/NivTramit/text())[1]',  'varchar(10)') END AS NIV_TRAMIT,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(5)') END AS BLOQUEO_EXP,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(5)') END AS UNION_RAMAS,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/TramitSimul/text())[1]','varchar(5)') END AS TRAMIT_SIMUL,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/TramOculto/text())[1]', 'varchar(5)') END AS TRAM_OCULTO,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/IndValorVar/text())[1]','varchar(5)') END AS IND_VALORVAR,
        CASE WHEN o.UDxml IS NULL THEN NULL ELSE o.UDxml.value('(/UD/VueltaAtras/text())[1]','varchar(5)') END AS VUELTA_ATRAS
    FROM Orden o
)
SELECT
    @Procedimiento AS PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    AnoID AS ID_DIAGRAMA,
    PAnoID AS ID_PADRE,
    Seq AS NUM_SEQ,
    DiID AS DI_ID,
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
OPTION (MAXRECURSION 10000);";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        private DataTable LeerConectoresDi(int diId, string procedimiento, string modelName)
        {
            string sql = @"
SELECT
    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
    J.ANO_ID AS ID_CONECTOR,
    J.DI_ID AS DIAGRAMA,
    J.JO_SEQ AS NUM,
    J.SH_SEQ_FROM AS NUM_SEC_DESDE,
    J.SH_SEQ_TO AS NUM_SEC_HASTA,
    ISNULL(LU.LU_NAME, 'Normal') AS CAT_CONECTOR,
    J.DI_ID AS DI_ID,
    D.ORDEN_N1,
    D.ORDEN_N2,
    D.ORDEN_N3,
    D.ORDEN_N4,
    C.CO_TYPE AS TIPO_CONECTOR,
    'N' AS SALIDA
FROM dbo.JOINER J
JOIN dbo.SHAPE SF
  ON SF.MODEL_NAME = J.MODEL_NAME
 AND SF.DI_ID = J.DI_ID
 AND SF.SH_SEQ = J.SH_SEQ_FROM
JOIN dbo.SHAPE ST
  ON ST.MODEL_NAME = J.MODEL_NAME
 AND ST.DI_ID = J.DI_ID
 AND ST.SH_SEQ = J.SH_SEQ_TO
LEFT JOIN dbo.CONNECTOR C
  ON C.MODEL_NAME = J.MODEL_NAME
 AND C.CO_ID = J.ANO_ID
LEFT JOIN dbo.CW_LOOKUP LU
  ON LU.MODEL_NAME = J.MODEL_NAME
 AND LU.LU_ID = C.CO_TYPE
JOIN
(
    SELECT
        S.DI_ID,
        S.SH_SEQ,
        ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
        0 AS ORDEN_N2,
        0 AS ORDEN_N3,
        0 AS ORDEN_N4
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @P_MODEL_NAME
      AND S.DI_ID = @P_DI_ID
      AND S.ANO_TABNR = 8953
) D
  ON D.DI_ID = J.DI_ID
 AND D.SH_SEQ = J.SH_SEQ_FROM
WHERE J.MODEL_NAME = @P_MODEL_NAME
  AND J.DI_ID = @P_DI_ID;";

//SELECT
//    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
//    J.ANO_ID AS ID_CONECTOR,
//    J.DI_ID AS DIAGRAMA,
//    J.JO_SEQ AS NUM,
//    J.SH_SEQ_FROM AS NUM_SEC_DESDE,
//    J.SH_SEQ_TO AS NUM_SEC_HASTA,
//    ISNULL(LU.LU_NAME, 'Normal') AS CAT_CONECTOR,
//    J.DI_ID AS DI_ID,
//    D.ORDEN_N1,
//    D.ORDEN_N2,
//    D.ORDEN_N3,
//    D.ORDEN_N4,
//    ISNULL(LU.LU_NAME, 'Normal') AS TIPO_CONECTOR,
//    'N' AS SALIDA
//FROM dbo.JOINER J
//JOIN dbo.SHAPE SF
//  ON SF.MODEL_NAME = J.MODEL_NAME
// AND SF.DI_ID = J.DI_ID
// AND SF.SH_SEQ = J.SH_SEQ_FROM
//JOIN dbo.SHAPE ST
//  ON ST.MODEL_NAME = J.MODEL_NAME
// AND ST.DI_ID = J.DI_ID
// AND ST.SH_SEQ = J.SH_SEQ_TO
//LEFT JOIN dbo.CONNECTOR C
//  ON C.MODEL_NAME = J.MODEL_NAME
// AND C.CO_ID = J.ANO_ID
//LEFT JOIN dbo.CW_LOOKUP LU
//  ON LU.MODEL_NAME = J.MODEL_NAME
// AND LU.LU_ID = C.CO_TYPE
//JOIN
//(
//    SELECT
//        S.DI_ID,
//        S.SH_SEQ,
//        ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
//        0 AS ORDEN_N2,
//        0 AS ORDEN_N3,
//        0 AS ORDEN_N4
//    FROM dbo.SHAPE S
//    WHERE S.MODEL_NAME = @P_MODEL_NAME
//      AND S.DI_ID = @P_DI_ID
//      AND S.ANO_TABNR = 8953
//) D
//  ON D.DI_ID = J.DI_ID
// AND D.SH_SEQ = J.SH_SEQ_FROM
//WHERE J.MODEL_NAME = @P_MODEL_NAME
//  AND J.DI_ID = @P_DI_ID;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        private DataTable LeerPropiedadesDi(int diId, string procedimiento, string modelName)
        {
            string sql = 
//@"
//SELECT
//    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
//    ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
//    0 AS ORDEN_N2,
//    0 AS ORDEN_N3,
//    0 AS ORDEN_N4,
//    0 AS ORDEN_N5,
//    P.PR_ID AS ID_DIAGRAMA,
//    P.PR_NAME AS NOM_DIAGRAMA,
//    P.PR_NAME AS NOMBRE_TRAM,
//    CONVERT(varchar(max), P.USERDEFINED) AS USERDEFINED,
//    S.DI_ID,
//    S.SH_SEQ AS NUM_SEQ,
//    ISNULL(LU.LU_NAME, '') AS CAT_DIAGRAMA
//FROM dbo.SHAPE S
//JOIN dbo.PROCESS P
//  ON P.MODEL_NAME = S.MODEL_NAME
// AND P.PR_ID = S.ANO_ID
//LEFT JOIN dbo.CW_LOOKUP LU
//  ON LU.MODEL_NAME = P.MODEL_NAME
// AND LU.LU_ID = P.PR_TYPE
//WHERE S.MODEL_NAME = @P_MODEL_NAME
//  AND S.DI_ID = @P_DI_ID
//  AND S.ANO_TABNR = 8953
//ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ;";
           @"; WITH Base AS
(
    SELECT
        S.SH_Y,
        S.SH_X,
        S.SH_SEQ,
        P.PR_ID,
        P.PR_NAME,
        ISNULL(LU.LU_NAME, '') AS TIPO_DIAGRAMA,
        TRY_CONVERT(xml, P.USERDEFINED) AS UDxml
    FROM dbo.SHAPE S
    JOIN dbo.PROCESS P
      ON P.MODEL_NAME = S.MODEL_NAME
     AND P.PR_ID = S.ANO_ID
    LEFT JOIN dbo.CW_LOOKUP LU
      ON LU.MODEL_NAME = P.MODEL_NAME
     AND LU.LU_ID = P.PR_TYPE
    WHERE S.MODEL_NAME = @P_MODEL_NAME
      AND S.DI_ID = @P_DI_ID
      AND S.ANO_TABNR = 8953
)
SELECT
    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
    ROW_NUMBER() OVER(ORDER BY SH_Y DESC, SH_X ASC, SH_SEQ) AS ORDEN_N1,
    0 AS ORDEN_N2,
    0 AS ORDEN_N3,
    0 AS ORDEN_N4,
    0 AS ORDEN_N5,

    PR_ID AS ID_DIAGRAMA,
    PR_NAME AS NOM_DIAGRAMA,
    TIPO_DIAGRAMA,

    UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(250)') AS PLAZTIP1_DI,
    UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(250)') AS PLAZTIP2_DI,
    UDxml.value('(/UD/NivTramit/text())[1]', 'varchar(250)') AS NIVELTRAM_DI,
    UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(250)') AS INDBLOQ_DI,
    UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(250)') AS INDRAM_DI,

    'N' AS INDPERSINT
FROM Base
ORDER BY SH_Y DESC, SH_X ASC, SH_SEQ;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        private DataTable LeerAccionesDi(int diId, string procedimiento, string modelName)
        {
            string sql = @"
;WITH Tramites AS
(
    SELECT
        ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
        S.ANO_ID AS ID_DIAGRAMA,
        S.SH_SEQ AS NUM_SEQ,
        S.DI_ID
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @P_MODEL_NAME
      AND S.DI_ID = @P_DI_ID
      AND S.ANO_TABNR = 8953
),
Acciones AS
(
    SELECT
        T.ORDEN_N1,
        ROW_NUMBER() OVER (PARTITION BY T.ORDEN_N1 ORDER BY SA.SH_Y DESC, SA.SH_X ASC, SA.SH_SEQ) AS ORDEN_ACC,
        PA.PR_ID AS ID_ACCION,
        PA.PR_NAME AS NOM_ACCION,
        SA.SH_SEQ AS NUM_SEQ,
        SA.DI_ID,
        ISNULL(PA.PR_ALT_NAME, PA.PR_NAME) AS PATH_HIDRA
    FROM Tramites T
    JOIN dbo.DIAGRAM DChild
      ON DChild.MODEL_NAME = @P_MODEL_NAME
     AND DChild.ANO_ID = T.ID_DIAGRAMA
     AND DChild.DI_TYPE <> 1
    JOIN dbo.SHAPE SA
      ON SA.MODEL_NAME = @P_MODEL_NAME
     AND SA.DI_ID = DChild.DI_ID
     AND SA.ANO_TABNR = 8953
    JOIN dbo.PROCESS PA
      ON PA.MODEL_NAME = @P_MODEL_NAME
     AND PA.PR_ID = SA.ANO_ID
)
SELECT
    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
    ORDEN_N1,
    0 AS ORDEN_N2,
    0 AS ORDEN_N3,
    0 AS ORDEN_N4,
    0 AS ORDEN_N5,
    ORDEN_ACC,
    ID_ACCION,
    NOM_ACCION,
    ID_ACCION AS NUM_ACCION,
    'T' AS TIPO_ACCION,
    PATH_HIDRA,
    NUM_SEQ,
    DI_ID
FROM Acciones
ORDER BY ORDEN_N1, ORDEN_ACC;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        private DataTable LeerConectoresAcc(int diId, string procedimiento, string modelName)
        {
            string sql = @"
;WITH Tramites AS
(
    SELECT
        ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
        S.ANO_ID AS ID_DIAGRAMA
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @P_MODEL_NAME
      AND S.DI_ID = @P_DI_ID
      AND S.ANO_TABNR = 8953
),
DiagramasHijo AS
(
    SELECT
        T.ORDEN_N1,
        T.ID_DIAGRAMA,
        D.DI_ID AS DI_ID_ACC
    FROM Tramites T
    JOIN dbo.DIAGRAM D
      ON D.MODEL_NAME = @P_MODEL_NAME
     AND D.ANO_ID = T.ID_DIAGRAMA
     AND D.DI_TYPE <> 1
)
SELECT
    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
    J.ANO_ID AS ID_CONECTOR,
    DH.ID_DIAGRAMA,
    J.JO_SEQ AS NUM_CONECTOR,
    J.SH_SEQ_FROM AS NUM_SEQ_DESDE,
    J.SH_SEQ_TO AS NUM_SEQ_HASTA,
    ISNULL(LU.LU_NAME, 'Normal') AS CAT_CONECTOR,
    'N' AS IND_SALIDA_TRAM,
    J.DI_ID,
    DH.ORDEN_N1
FROM DiagramasHijo DH
JOIN dbo.JOINER J
  ON J.MODEL_NAME = @P_MODEL_NAME
 AND J.DI_ID = DH.DI_ID_ACC
LEFT JOIN dbo.CONNECTOR C
  ON C.MODEL_NAME = J.MODEL_NAME
 AND C.CO_ID = J.ANO_ID
LEFT JOIN dbo.CW_LOOKUP LU
  ON LU.MODEL_NAME = J.MODEL_NAME
 AND LU.LU_ID = C.CO_TYPE
ORDER BY DH.ORDEN_N1, J.JO_SEQ;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        private DataTable LeerParamAcc(int diId, string procedimiento, string modelName)
        {
            string sql = @"
;WITH Tramites AS
(
    SELECT
        ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ) AS ORDEN_N1,
        S.ANO_ID AS ID_DIAGRAMA
    FROM dbo.SHAPE S
    WHERE S.MODEL_NAME = @P_MODEL_NAME
      AND S.DI_ID = @P_DI_ID
      AND S.ANO_TABNR = 8953
),
Acciones AS
(
    SELECT
        T.ORDEN_N1,
        ROW_NUMBER() OVER (PARTITION BY T.ORDEN_N1 ORDER BY SA.SH_Y DESC, SA.SH_X ASC, SA.SH_SEQ) AS ORDEN_ACC,
        PA.PR_ID AS ID_ACCION
    FROM Tramites T
    JOIN dbo.DIAGRAM DChild
      ON DChild.MODEL_NAME = @P_MODEL_NAME
     AND DChild.ANO_ID = T.ID_DIAGRAMA
     AND DChild.DI_TYPE <> 1
    JOIN dbo.SHAPE SA
      ON SA.MODEL_NAME = @P_MODEL_NAME
     AND SA.DI_ID = DChild.DI_ID
     AND SA.ANO_TABNR = 8953
    JOIN dbo.PROCESS PA
      ON PA.MODEL_NAME = @P_MODEL_NAME
     AND PA.PR_ID = SA.ANO_ID
),
Params AS
(
    SELECT
        A.ORDEN_N1,
        A.ORDEN_ACC,
        A.ID_ACCION,
        ROW_NUMBER() OVER (PARTITION BY A.ORDEN_N1, A.ORDEN_ACC ORDER BY DU.DM_SEQ) AS ORDEN_PA,
        '@' + AT.AT_NAME AS PARAMETRO,
        AT.AT_NAME AS VALOR
    FROM Acciones A
    JOIN dbo.CW_DATA_USAGE DU
      ON DU.MODEL_NAME = @P_MODEL_NAME
     AND DU.PR_ID = A.ID_ACCION
    JOIN dbo.ATTRIBUTE AT
      ON AT.MODEL_NAME = @P_MODEL_NAME
     AND AT.AT_ID = DU.AT_ID
)
SELECT
    @P_PROCEDIMIENTO AS PROCEDIMIENTO,
    ORDEN_N1,
    0 AS ORDEN_N2,
    0 AS ORDEN_N3,
    0 AS ORDEN_N4,
    0 AS ORDEN_N5,
    ORDEN_ACC,
    ID_ACCION,
    PARAMETRO,
    VALOR,
    ORDEN_PA
FROM Params
ORDER BY ORDEN_N1, ORDEN_ACC, ORDEN_PA;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        //        private void InsertDiagrama(DataRow dr)
        //        {
        //            string sql = @"
        //INSERT INTO DIAGRAMA
        //(
        //    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
        //    ID_DIAGRAMA, ID_PADRE, NUM_SEQ, DI_ID,
        //    CAT_DIAGRAMA, NOMBRE, USERDEFINED, NIVEL, ARBOL,
        //    PLAZOTIPO1, PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS,
        //    TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR, VUELTA_ATRAS, NOMBRE_TRAM
        //)
        //VALUES
        //(
        //    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
        //    @ID_DIAGRAMA, @ID_PADRE, @NUM_SEQ, @DI_ID,
        //    @CAT_DIAGRAMA, @NOMBRE, @USERDEFINED, @NIVEL, @ARBOL,
        //    @PLAZOTIPO1, @PLAZOTIPO2, @NIV_TRAMIT, @BLOQUEO_EXP, @UNION_RAMAS,
        //    @TRAMIT_SIMUL, @TRAM_OCULTO, @IND_VALORVAR, @VUELTA_ATRAS, @NOMBRE_TRAM
        //);";

        //            ExecutePasarela(sql, p => AddCommonDiagramaParams(p, dr));
        //        }

        private void InsertDiagrama(DataRow dr)
        {
            string sql = @"
INSERT INTO DIAGRAMA
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ID_DIAGRAMA, ID_PADRE, NUM_SEQ, DI_ID,
    CAT_DIAGRAMA, NOMBRE, USERDEFINED, NIVEL, ARBOL,
    PLAZOTIPO1, PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS,
    TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR, VUELTA_ATRAS, NOMBRE_TRAM
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ID_DIAGRAMA, @ID_PADRE, @NUM_SEQ, @DI_ID,
    @CAT_DIAGRAMA, @NOMBRE, CONVERT(varchar(max), @USERDEFINED), @NIVEL, @ARBOL,
    @PLAZOTIPO1, @PLAZOTIPO2, @NIV_TRAMIT, @BLOQUEO_EXP, @UNION_RAMAS,
    @TRAMIT_SIMUL, @TRAM_OCULTO, @IND_VALORVAR, @VUELTA_ATRAS, @NOMBRE_TRAM
);";

            ExecutePasarela(sql, p =>
            {
                p.Set("@PROCEDIMIENTO", GetStringOrDbNull(dr, "PROCEDIMIENTO"));

                p.Set("@ORDEN_N1", GetIntOrDbNull(dr, "ORDEN_N1"));
                p.Set("@ORDEN_N2", GetIntOrDbNull(dr, "ORDEN_N2"));
                p.Set("@ORDEN_N3", GetIntOrDbNull(dr, "ORDEN_N3"));
                p.Set("@ORDEN_N4", GetIntOrDbNull(dr, "ORDEN_N4"));
                p.Set("@ORDEN_N5", GetIntOrDbNull(dr, "ORDEN_N5"));

                p.Set("@ID_DIAGRAMA", GetIntOrDbNull(dr, "ID_DIAGRAMA"));
                p.Set("@ID_PADRE", GetIntOrDbNull(dr, "ID_PADRE"));
                p.Set("@NUM_SEQ", GetIntOrDbNull(dr, "NUM_SEQ"));
                p.Set("@DI_ID", GetIntOrDbNull(dr, "DI_ID"));

                p.Set("@CAT_DIAGRAMA", GetStringOrDbNull(dr, "CAT_DIAGRAMA"));
                p.Set("@NOMBRE", GetStringOrDbNull(dr, "NOMBRE"));
                p.Set("@USERDEFINED", GetStringOrDbNull(dr, "USERDEFINED"));
                p.Set("@NIVEL", GetIntOrDbNull(dr, "NIVEL"));
                p.Set("@ARBOL", GetStringOrDbNull(dr, "ARBOL"));

                p.Set("@PLAZOTIPO1", GetStringOrDbNull(dr, "PLAZOTIPO1"));
                p.Set("@PLAZOTIPO2", GetStringOrDbNull(dr, "PLAZOTIPO2"));
                p.Set("@NIV_TRAMIT", GetStringOrDbNull(dr, "NIV_TRAMIT"));
                p.Set("@BLOQUEO_EXP", GetStringOrDbNull(dr, "BLOQUEO_EXP"));
                p.Set("@UNION_RAMAS", GetStringOrDbNull(dr, "UNION_RAMAS"));

                p.Set("@TRAMIT_SIMUL", GetStringOrDbNull(dr, "TRAMIT_SIMUL"));
                p.Set("@TRAM_OCULTO", GetStringOrDbNull(dr, "TRAM_OCULTO"));
                p.Set("@IND_VALORVAR", GetStringOrDbNull(dr, "IND_VALORVAR"));
                p.Set("@VUELTA_ATRAS", GetStringOrDbNull(dr, "VUELTA_ATRAS"));
                p.Set("@NOMBRE_TRAM", GetStringOrDbNull(dr, "NOMBRE_TRAM"));
                p.Apply();
            });
        }


        private static object GetIntOrDbNull(DataRow dr, string columnName)
        {
            if (!dr.Table.Columns.Contains(columnName))
                return DBNull.Value;

            object value = dr[columnName];

            if (value == null || value == DBNull.Value)
                return DBNull.Value;

            return Convert.ToInt32(value);
        }

        private static object GetStringOrDbNull(DataRow dr, string columnName)
        {
            if (!dr.Table.Columns.Contains(columnName))
                return DBNull.Value;

            object value = dr[columnName];

            return value == null || value == DBNull.Value
                ? (object)DBNull.Value
                : value.ToString();
        }

        private void InsertConectorDi(DataRow dr)
        {
            string sql = @"
INSERT INTO CONECTORES_DI
(
    PROCEDIMIENTO, ID_CONECTOR, DIAGRAMA, NUM, NUM_SEC_DESDE, NUM_SEC_HASTA,
    CAT_CONECTOR, DI_ID, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, TIPO_CONECTOR, SALIDA
)
VALUES
(
    @PROCEDIMIENTO, @ID_CONECTOR, @DIAGRAMA, @NUM, @NUM_SEC_DESDE, @NUM_SEC_HASTA,
    @CAT_CONECTOR, @DI_ID, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @TIPO_CONECTOR, @SALIDA
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ID_CONECTOR", dr["ID_CONECTOR"]);
                Add(p, "@DIAGRAMA", dr["DIAGRAMA"]);
                Add(p, "@NUM", dr["NUM"]);
                Add(p, "@NUM_SEC_DESDE", dr["NUM_SEC_DESDE"]);
                Add(p, "@NUM_SEC_HASTA", dr["NUM_SEC_HASTA"]);
                Add(p, "@CAT_CONECTOR", dr["CAT_CONECTOR"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@TIPO_CONECTOR", dr["TIPO_CONECTOR"]);
                Add(p, "@SALIDA", dr["SALIDA"]);
                p.Apply();
            });
        }

        private void InsertPropiedadDi(DataRow dr)
        {
            string sql = @"
INSERT INTO PROPIEDADES_DI
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ID_DIAGRAMA, NOM_DIAGRAMA, TIPO_DIAGRAMA,
    PLAZTIP1_DI, PLAZTIP2_DI, NIVELTRAM_DI,
    INDBLOQ_DI, INDRAM_DI, INDPERSINT
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ID_DIAGRAMA, @NOM_DIAGRAMA, @TIPO_DIAGRAMA,
    @PLAZTIP1_DI, @PLAZTIP2_DI, @NIVELTRAM_DI,
    @INDBLOQ_DI, @INDRAM_DI, @INDPERSINT
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);

                Add(p, "@ID_DIAGRAMA", dr["ID_DIAGRAMA"]);
                Add(p, "@NOM_DIAGRAMA", dr["NOM_DIAGRAMA"]);
                Add(p, "@TIPO_DIAGRAMA", dr["TIPO_DIAGRAMA"]);

                Add(p, "@PLAZTIP1_DI", dr["PLAZTIP1_DI"]);
                Add(p, "@PLAZTIP2_DI", dr["PLAZTIP2_DI"]);
                Add(p, "@NIVELTRAM_DI", dr["NIVELTRAM_DI"]);
                Add(p, "@INDBLOQ_DI", dr["INDBLOQ_DI"]);
                Add(p, "@INDRAM_DI", dr["INDRAM_DI"]);
                Add(p, "@INDPERSINT", dr["INDPERSINT"]);

                p.Apply();
            });
        }

        private void InsertAccionDi(DataRow dr)
        {
            string sql = @"
INSERT INTO ACCIONES_DI
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ORDEN_ACC, ID_ACCION, NOM_ACCION, NUM_ACCION, TIPO_ACCION, PATH_HIDRA, NUM_SEQ, DI_ID
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ORDEN_ACC, @ID_ACCION, @NOM_ACCION, @NUM_ACCION, @TIPO_ACCION, @PATH_HIDRA, @NUM_SEQ, @DI_ID
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);
                Add(p, "@ORDEN_ACC", dr["ORDEN_ACC"]);
                Add(p, "@ID_ACCION", dr["ID_ACCION"]);
                Add(p, "@NOM_ACCION", dr["NOM_ACCION"]);
                Add(p, "@NUM_ACCION", dr["NUM_ACCION"]);
                Add(p, "@TIPO_ACCION", dr["TIPO_ACCION"]);
                Add(p, "@PATH_HIDRA", dr["PATH_HIDRA"]);
                Add(p, "@NUM_SEQ", dr["NUM_SEQ"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                p.Apply();
            });
        }

        private void InsertConectorAcc(DataRow dr)
        {
            string sql = @"
INSERT INTO CONECTOR_ACC
(
    PROCEDIMIENTO, ID_CONECTOR, ID_DIAGRAMA, NUM_CONECTOR,
    NUM_SEQ_DESDE, NUM_SEQ_HASTA, CAT_CONECTOR, IND_SALIDA_TRAM, DI_ID
)
VALUES
(
    @PROCEDIMIENTO, @ID_CONECTOR, @ID_DIAGRAMA, @NUM_CONECTOR,
    @NUM_SEQ_DESDE, @NUM_SEQ_HASTA, @CAT_CONECTOR, @IND_SALIDA_TRAM, @DI_ID
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ID_CONECTOR", dr["ID_CONECTOR"]);
                Add(p, "@ID_DIAGRAMA", dr["ID_DIAGRAMA"]);
                Add(p, "@NUM_CONECTOR", dr["NUM_CONECTOR"]);
                Add(p, "@NUM_SEQ_DESDE", dr["NUM_SEQ_DESDE"]);
                Add(p, "@NUM_SEQ_HASTA", dr["NUM_SEQ_HASTA"]);
                Add(p, "@CAT_CONECTOR", dr["CAT_CONECTOR"]);
                Add(p, "@IND_SALIDA_TRAM", dr["IND_SALIDA_TRAM"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                p.Apply();
            });
        }

        private void InsertParamAcc(DataRow dr)
        {
            string sql = @"
INSERT INTO PARAM_ACC
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ORDEN_ACC, ID_ACCION, PARAMETRO, VALOR, ORDEN_PA
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ORDEN_ACC, @ID_ACCION, @PARAMETRO, @VALOR, @ORDEN_PA
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);
                Add(p, "@ORDEN_ACC", dr["ORDEN_ACC"]);
                Add(p, "@ID_ACCION", dr["ID_ACCION"]);
                Add(p, "@PARAMETRO", dr["PARAMETRO"]);
                Add(p, "@VALOR", dr["VALOR"]);
                Add(p, "@ORDEN_PA", dr["ORDEN_PA"]);
                p.Apply();
            });
        }

        private void AddCommonDiagramaParams(ParameterBag p, DataRow dr)
        {
            Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
            Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
            Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
            Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
            Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
            Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);
            Add(p, "@ID_DIAGRAMA", dr["ID_DIAGRAMA"]);
            Add(p, "@ID_PADRE", dr["ID_PADRE"]);
            Add(p, "@NUM_SEQ", dr["NUM_SEQ"]);
            Add(p, "@DI_ID", dr["DI_ID"]);
            Add(p, "@CAT_DIAGRAMA", dr["CAT_DIAGRAMA"]);
            Add(p, "@NOMBRE", dr["NOMBRE"]);
            Add(p, "@USERDEFINED", dr["USERDEFINED"]);
            Add(p, "@NIVEL", dr["NIVEL"]);
            Add(p, "@ARBOL", dr["ARBOL"]);
            Add(p, "@PLAZOTIPO1", dr["PLAZOTIPO1"]);
            Add(p, "@PLAZOTIPO2", dr["PLAZOTIPO2"]);
            Add(p, "@NIV_TRAMIT", dr["NIV_TRAMIT"]);
            Add(p, "@BLOQUEO_EXP", dr["BLOQUEO_EXP"]);
            Add(p, "@UNION_RAMAS", dr["UNION_RAMAS"]);
            Add(p, "@TRAMIT_SIMUL", dr["TRAMIT_SIMUL"]);
            Add(p, "@TRAM_OCULTO", dr["TRAM_OCULTO"]);
            Add(p, "@IND_VALORVAR", dr["IND_VALORVAR"]);
            Add(p, "@VUELTA_ATRAS", dr["VUELTA_ATRAS"]);
            Add(p, "@NOMBRE_TRAM", dr["NOMBRE_TRAM"]);
            p.Apply();
        }

        private DataTable QueryErwin(string sql, Action<ParameterBag> configureParameters)
        {
            DBErwin.ClearParameters();
            DBErwin.Type = CommandType.Text;
            DBErwin.SqlStatment = sql;

            var bag = new ParameterBag(DBErwin);
            configureParameters?.Invoke(bag);

            Logger.Info("Ejecutamos ERWIN query: " + sql);
            return DBErwin.GetDataTable();
        }

        private void ExecutePasarela(string sql, Action<ParameterBag> configureParameters)
        {
            DBPasarela.ClearParameters();
            DBPasarela.Type = CommandType.Text;
            DBPasarela.SqlStatment = sql;

            var bag = new ParameterBag(DBPasarela);
            configureParameters?.Invoke(bag);

            Logger.Info("Ejecutamos PASARELA query: " + sql);
            DBPasarela.ExecuteNonQuery();
        }

        private static void Add(ParameterBag p, string name, object value)
        {
            p.Add(name, value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()) ? DBNull.Value : value);
        }

        /// <summary>
        /// Adaptador mínimo para no depender del nombre exacto del método de parámetros de DBContext.
        // </summary>
        private sealed class ParameterBag
        {
            private readonly DBContext _db;

            private readonly Dictionary<string, object> _values =
                new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            public ParameterBag(DBContext db)
            {
                _db = db;
            }

            public void Add(string name, object value)
            {
                Set(name, value);
                //Apply();
            }

            public void Addpply(string name, object value)
            {
                Set(name, value);
                Apply();
            }
            

            public void Set(string name, object value)
            {
                _values[NormalizeParameterName(name)] = value ?? DBNull.Value;
            }

            public void Apply()
            {
                foreach (var kv in _values)
                {
                    _db.AddParameter(
                        kv.Key,
                        ParameterDirection.Input,
                        GuessSqlDbType(kv.Value),
                        kv.Value
                    );
                }
            }

            private static string NormalizeParameterName(string name)
            {
                return name.StartsWith("@") ? name : "@" + name;
            }

            private static SqlDbType GuessSqlDbType(object value)
            {
                if (value == null || value == DBNull.Value)
                    return SqlDbType.NVarChar;

                Type type = value.GetType();

                if (type == typeof(int)) return SqlDbType.Int;
                if (type == typeof(long)) return SqlDbType.BigInt;
                if (type == typeof(short)) return SqlDbType.SmallInt;
                if (type == typeof(bool)) return SqlDbType.Bit;
                if (type == typeof(DateTime)) return SqlDbType.DateTime;
                if (type == typeof(decimal)) return SqlDbType.Decimal;
                if (type == typeof(double)) return SqlDbType.Float;
                if (type == typeof(float)) return SqlDbType.Real;
                if (type == typeof(byte[])) return SqlDbType.VarBinary;

                return SqlDbType.NVarChar;
            }
        }
    }
}
