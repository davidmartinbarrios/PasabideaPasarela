USE erwin_evolve;
go
DECLARE @DI_NAME NVARCHAR(254) = N'_Modelo 030 NIF V5';

;WITH DiagramaRaiz AS (
    SELECT TOP (1)
        D.DI_ID,
        D.ANO_ID,
        D.MODEL_NAME,
        D.DI_NAME,
        D.ANO_TABNR
    FROM dbo.DIAGRAM D
    WHERE D.DI_NAME = @DI_NAME
),
DirectReports AS (
    SELECT
        PDiID = DR.DI_ID,
        DiID = S.DI_ID,
        PAnoID = DR.ANO_ID,
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
    FROM DiagramaRaiz DR
    JOIN dbo.SHAPE S
        ON S.DI_ID = DR.DI_ID
       AND S.MODEL_NAME = DR.MODEL_NAME
    JOIN dbo.DIAGRAM D
        ON D.DI_ID = S.DI_ID
       AND D.MODEL_NAME = DR.MODEL_NAME
    JOIN dbo.PROCESS P
        ON P.PR_ID = S.ANO_ID
       AND P.MODEL_NAME = DR.MODEL_NAME

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
    JOIN dbo.SHAPE S2
        ON S2.DI_ID = J2.DI_ID
       AND S2.SH_SEQ = J2.SH_SEQ_TO
       AND S2.MODEL_NAME = J2.MODEL_NAME
    JOIN dbo.PROCESS P2
        ON P2.PR_ID = S2.ANO_ID
       AND P2.MODEL_NAME = S2.MODEL_NAME
    WHERE J2.MODEL_NAME = P2.MODEL_NAME
),
Orden AS (
    SELECT
        dr.*,
        ROW_NUMBER() OVER (ORDER BY dr.shy DESC, dr.shx ASC, dr.Seq) AS ORDEN_N1,
        CAST(0 AS int) AS ORDEN_N2,
        CAST(0 AS int) AS ORDEN_N3,
        CAST(0 AS int) AS ORDEN_N4,
        CAST(0 AS int) AS ORDEN_N5,
        CAST(dr.UserDefined AS xml) AS UDxml
    FROM DirectReports dr
),
Flags AS (
    SELECT
        o.*,
        o.UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(20)') AS PLAZOTIPO1,
        o.UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(20)') AS PLAZOTIPO2,
        o.UDxml.value('(/UD/NivTramit/text())[1]', 'varchar(10)') AS NIV_TRAMIT,
        o.UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(5)') AS BLOQUEO_EXP,
        o.UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(5)') AS UNION_RAMAS,
        o.UDxml.value('(/UD/TramitSimul/text())[1]', 'varchar(5)') AS TRAMIT_SIMUL,
        o.UDxml.value('(/UD/TramOculto/text())[1]', 'varchar(5)') AS TRAM_OCULTO,
        o.UDxml.value('(/UD/IndValorVar/text())[1]', 'varchar(5)') AS IND_VALORVAR,
        o.UDxml.value('(/UD/VueltaAtras/text())[1]', 'varchar(5)') AS VUELTA_ATRAS
    FROM Orden o
)
SELECT
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
--OPTION (MAXRECURSION 1);