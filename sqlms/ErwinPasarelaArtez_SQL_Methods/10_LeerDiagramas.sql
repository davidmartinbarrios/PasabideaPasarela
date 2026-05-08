DECLARE @P_MODEL_NAME nvarchar(255) = N'ARTEZELI';
DECLARE @P_DI_ID int = 3751;
DECLARE @P_PROCEDIMIENTO nvarchar(255) = N'3751'; -- cámbialo si el procedimiento es otro código, ej. TA999900

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
OPTION (MAXRECURSION 10000);