namespace Lantik.Pasabidea.Core.Data
{
    /// <summary>
    /// TODO: Valorar las selects grandes Llevarlas a PAs
    /// </summary>
    internal sealed partial class ErwinPasarelaArtezData
    {
        private static class Sql
        {
            public const string PropiedadObjetoDiagrama = @"
SELECT TOP (1)
    LTRIM(RTRIM(
        COALESCE(
            NULLIF(P.N.value('(value/text())[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(text())[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@value)[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@id)[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@name)[1]', 'nvarchar(4000)'), N'')
        )
    )) AS VALOR
FROM dbo.SHAPE S
JOIN dbo.CW_OBJECT_TYPE OT
    ON OT.MODEL_NAME = S.MODEL_NAME
   AND OT.OT_ID = S.ANO_TABNR
JOIN dbo.CW_OBJECT CO
    ON CO.MODEL_NAME = S.MODEL_NAME
   AND CO.OT_ID = S.ANO_TABNR
   AND CO.GO_ID = S.ANO_ID
CROSS APPLY
(
    SELECT TRY_CAST(CONVERT(nvarchar(max), CO.USERDEFINED) AS xml) AS XML_DATA
) X
CROSS APPLY X.XML_DATA.nodes('//*[local-name()=""property""]') P(N)
WHERE S.MODEL_NAME = @MODEL_NAME
  AND S.DI_ID = @DI_ID
  AND OT.OT_NAME = @TIPO_OBJETO
  AND P.N.value('@scriptname', 'nvarchar(500)') COLLATE DATABASE_DEFAULT
      = @SCRIPTNAME_PROPIEDAD COLLATE DATABASE_DEFAULT
ORDER BY S.SH_SEQ;";

            public const string Diagrama = @"
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

            public const string ConectoresDi = @"
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

            public const string PropiedadesDi = @"
;WITH Base AS
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

            public const string AccionesDi = @"
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

            public const string ConectoresAcc = @"
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

            public const string ParamAcc = @"
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

        }
    }
}
