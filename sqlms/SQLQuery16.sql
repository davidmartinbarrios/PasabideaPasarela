DECLARE @DiID int = 0
SELECT @DiID=[DI_ID]
  FROM [erwin_evolve].[dbo].[DIAGRAM]
  WHERE DI_NAME LIKE 'P00010%'
  AND MODEL_NAME = 'BIDERAT1';


SELECT di_id, *
  FROM [erwin_evolve].[dbo].[CONNECTOR] 
  WHERE DI_ID = @DiID
  and MODEL_NAME = 'BIDERAT1'
SELECT di_id, *
  FROM [erwin_evolve].[dbo].JOINER 
  WHERE DI_ID = @DiID
  and MODEL_NAME = 'BIDERAT1'

  USE erwin_evolve;
GO

DECLARE @ModelName sysname = N'BIDERAT1';
DECLARE @DiNamePattern nvarchar(254) = N'P00010%';
DECLARE @DiID int;

SELECT TOP (1)
    @DiID = D.DI_ID
FROM dbo.DIAGRAM D
WHERE D.DI_NAME LIKE @DiNamePattern
  AND D.MODEL_NAME = @ModelName
ORDER BY D.DI_ID;

IF @DiID IS NULL
BEGIN
    RAISERROR(N'No se ha encontrado DIAGRAM para el patrón indicado.', 16, 1);
    RETURN;
END;

PRINT CONCAT('DI_ID = ', @DiID);

--------------------------------------------------------------------------------
-- 1) DIAGRAMA
--------------------------------------------------------------------------------
SELECT
    D.DI_ID,
    D.DI_NAME,
    D.DI_TYPE,
    D.ANO_ID,
    D.ANO_TABNR,
    D.MODEL_NAME,
    D.*
FROM dbo.DIAGRAM D
WHERE D.DI_ID = @DiID
  AND D.MODEL_NAME = @ModelName;

--------------------------------------------------------------------------------
-- 2) OBJETOS DIBUJADOS EN EL DIAGRAMA (SHAPE) + tipo + nombre resuelto
--    8953 = PROCESS / trámite
--    9097 = EVENT / resultado
--    5257 = PROCESS_BREAK
--------------------------------------------------------------------------------
SELECT
    S.DI_ID,
    S.SH_SEQ,
    S.ANO_ID,
    S.ANO_TABNR,
    S.SH_X,
    S.SH_Y,
    OT.OT_NAME,
    OT.OT_TABLE_NAME,
    P.PR_NAME,
    EV.EV_NAME,
    EV.EV_INTERNAL,
    PB.PB_NAME,
    COALESCE(
        P.PR_NAME,
        EV.EV_NAME,
        PB.PB_NAME,
        CONCAT(OT.OT_NAME, ' #', S.ANO_ID)
    ) AS OBJECT_NAME,
    S.*
FROM dbo.SHAPE S
LEFT JOIN dbo.CW_OBJECT_TYPE OT
    ON OT.OT_ID = S.ANO_TABNR
   AND OT.MODEL_NAME = S.MODEL_NAME
LEFT JOIN dbo.PROCESS P
    ON S.ANO_TABNR = 8953
   AND P.PR_ID = S.ANO_ID
   AND P.MODEL_NAME = S.MODEL_NAME
LEFT JOIN dbo.EVENT EV
    ON S.ANO_TABNR = 9097
   AND EV.EV_ID = S.ANO_ID
   AND EV.MODEL_NAME = S.MODEL_NAME
LEFT JOIN dbo.PROCESS_BREAK PB
    ON S.ANO_TABNR = 5257
   AND PB.PB_ID = S.ANO_ID
   AND PB.MODEL_NAME = S.MODEL_NAME
WHERE S.DI_ID = @DiID
  AND S.MODEL_NAME = @ModelName
ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ;

--------------------------------------------------------------------------------
-- 3) JOINER bruto
--------------------------------------------------------------------------------
SELECT
    J.DI_ID,
    J.JO_SEQ,
    J.ANO_ID,        -- normalmente enlaza con CONNECTOR.CO_ID
    J.ANO_TABNR,
    J.SH_SEQ_FROM,
    J.SH_SEQ_TO,
    J.MODEL_NAME,
    J.*
FROM dbo.JOINER J
WHERE J.DI_ID = @DiID
  AND J.MODEL_NAME = @ModelName
ORDER BY J.JO_SEQ;

--------------------------------------------------------------------------------
-- 4) CONNECTOR bruto
--------------------------------------------------------------------------------
SELECT
    C.DI_ID,
    C.CO_ID,
    C.CO_TYPE,
    C.CO_CONDITION,
    C.USERDEFINED,
    C.MODEL_NAME,
    C.*
FROM dbo.CONNECTOR C
WHERE C.DI_ID = @DiID
  AND C.MODEL_NAME = @ModelName
ORDER BY C.CO_ID;

--------------------------------------------------------------------------------
-- 5) JOINER + CONNECTOR + origen/destino resueltos
--------------------------------------------------------------------------------
SELECT
    J.JO_SEQ,
    J.DI_ID,
    J.ANO_ID AS CO_ID_JOINER,
    C.CO_ID AS CO_ID_CONNECTOR,
    C.CO_TYPE,
    C.CO_CONDITION,
    C.USERDEFINED AS CONNECTOR_USERDEFINED,

    J.SH_SEQ_FROM,
    SF.ANO_ID     AS SRC_ANO_ID,
    SF.ANO_TABNR  AS SRC_ANO_TABNR,
    OTF.OT_NAME   AS SRC_OBJECT_TYPE,
    COALESCE(PF.PR_NAME, EVF.EV_NAME, PBF.PB_NAME, CONCAT(OTF.OT_NAME, ' #', SF.ANO_ID)) AS SRC_NAME,

    J.SH_SEQ_TO,
    ST.ANO_ID     AS DST_ANO_ID,
    ST.ANO_TABNR  AS DST_ANO_TABNR,
    OTT.OT_NAME   AS DST_OBJECT_TYPE,
    COALESCE(PT.PR_NAME, EVT.EV_NAME, PBT.PB_NAME, CONCAT(OTT.OT_NAME, ' #', ST.ANO_ID)) AS DST_NAME

FROM dbo.JOINER J
LEFT JOIN dbo.CONNECTOR C
    ON C.CO_ID = J.ANO_ID
   AND C.MODEL_NAME = J.MODEL_NAME
   AND C.DI_ID = J.DI_ID

LEFT JOIN dbo.SHAPE SF
    ON SF.DI_ID = J.DI_ID
   AND SF.SH_SEQ = J.SH_SEQ_FROM
   AND SF.MODEL_NAME = J.MODEL_NAME
LEFT JOIN dbo.CW_OBJECT_TYPE OTF
    ON OTF.OT_ID = SF.ANO_TABNR
   AND OTF.MODEL_NAME = SF.MODEL_NAME
LEFT JOIN dbo.PROCESS PF
    ON SF.ANO_TABNR = 8953
   AND PF.PR_ID = SF.ANO_ID
   AND PF.MODEL_NAME = SF.MODEL_NAME
LEFT JOIN dbo.EVENT EVF
    ON SF.ANO_TABNR = 9097
   AND EVF.EV_ID = SF.ANO_ID
   AND EVF.MODEL_NAME = SF.MODEL_NAME
LEFT JOIN dbo.PROCESS_BREAK PBF
    ON SF.ANO_TABNR = 5257
   AND PBF.PB_ID = SF.ANO_ID
   AND PBF.MODEL_NAME = SF.MODEL_NAME

LEFT JOIN dbo.SHAPE ST
    ON ST.DI_ID = J.DI_ID
   AND ST.SH_SEQ = J.SH_SEQ_TO
   AND ST.MODEL_NAME = J.MODEL_NAME
LEFT JOIN dbo.CW_OBJECT_TYPE OTT
    ON OTT.OT_ID = ST.ANO_TABNR
   AND OTT.MODEL_NAME = ST.MODEL_NAME
LEFT JOIN dbo.PROCESS PT
    ON ST.ANO_TABNR = 8953
   AND PT.PR_ID = ST.ANO_ID
   AND PT.MODEL_NAME = ST.MODEL_NAME
LEFT JOIN dbo.EVENT EVT
    ON ST.ANO_TABNR = 9097
   AND EVT.EV_ID = ST.ANO_ID
   AND EVT.MODEL_NAME = ST.MODEL_NAME
LEFT JOIN dbo.PROCESS_BREAK PBT
    ON ST.ANO_TABNR = 5257
   AND PBT.PB_ID = ST.ANO_ID
   AND PBT.MODEL_NAME = ST.MODEL_NAME

WHERE J.DI_ID = @DiID
  AND J.MODEL_NAME = @ModelName
ORDER BY J.JO_SEQ;

--------------------------------------------------------------------------------
-- 6) ATRIBUTOS DEL CONECTOR: entradas/salidas
--    DM_INSERTS = entrada
--    DM_DELETES = salida
--------------------------------------------------------------------------------
SELECT
    C.CO_ID,
    J.JO_SEQ,
    CASE
        WHEN DU.DM_INSERTS = 1 THEN 'ENTRADA'
        WHEN DU.DM_DELETES = 1 THEN 'SALIDA'
        ELSE 'OTRO'
    END AS TIPO_USO,
    DU.DM_SEQ,
    DU.DM_ID,
    A.AT_ID,
    E.EN_NAME,
    A.AT_NAME,
    DU.*
FROM dbo.CONNECTOR C
INNER JOIN dbo.JOINER J
    ON J.ANO_ID = C.CO_ID
   AND J.DI_ID = C.DI_ID
   AND J.MODEL_NAME = C.MODEL_NAME
LEFT JOIN dbo.CW_DATA_USAGE DU
    ON DU.CO_ID = C.CO_ID
   AND DU.MODEL_NAME = C.MODEL_NAME
LEFT JOIN dbo.ATTRIBUTE A
    ON A.AT_ID = DU.AT_ID
   AND A.MODEL_NAME = DU.MODEL_NAME
LEFT JOIN dbo.ENTITY E
    ON E.EN_ID = A.EN_ID
   AND E.MODEL_NAME = A.MODEL_NAME
WHERE C.DI_ID = @DiID
  AND C.MODEL_NAME = @ModelName
ORDER BY
    C.CO_ID,
    CASE
        WHEN DU.DM_INSERTS = 1 THEN 1
        WHEN DU.DM_DELETES = 1 THEN 2
        ELSE 3
    END,
    DU.DM_SEQ,
    A.AT_NAME;

--------------------------------------------------------------------------------
-- 7) SOLO VARIABLES DE ENTRADA DEL CONECTOR
--------------------------------------------------------------------------------
SELECT
    C.CO_ID,
    J.JO_SEQ,
    DU.DM_SEQ,
    A.AT_ID,
    E.EN_NAME,
    A.AT_NAME
FROM dbo.CONNECTOR C
INNER JOIN dbo.JOINER J
    ON J.ANO_ID = C.CO_ID
   AND J.DI_ID = C.DI_ID
   AND J.MODEL_NAME = C.MODEL_NAME
INNER JOIN dbo.CW_DATA_USAGE DU
    ON DU.CO_ID = C.CO_ID
   AND DU.MODEL_NAME = C.MODEL_NAME
   AND DU.DM_INSERTS = 1
LEFT JOIN dbo.ATTRIBUTE A
    ON A.AT_ID = DU.AT_ID
   AND A.MODEL_NAME = DU.MODEL_NAME
LEFT JOIN dbo.ENTITY E
    ON E.EN_ID = A.EN_ID
   AND E.MODEL_NAME = A.MODEL_NAME
WHERE C.DI_ID = @DiID
  AND C.MODEL_NAME = @ModelName
ORDER BY C.CO_ID, DU.DM_SEQ, A.AT_NAME;

--------------------------------------------------------------------------------
-- 8) SOLO VARIABLES DE SALIDA DEL CONECTOR
--------------------------------------------------------------------------------
SELECT
    C.CO_ID,
    J.JO_SEQ,
    DU.DM_SEQ,
    A.AT_ID,
    E.EN_NAME,
    A.AT_NAME
FROM dbo.CONNECTOR C
INNER JOIN dbo.JOINER J
    ON J.ANO_ID = C.CO_ID
   AND J.DI_ID = C.DI_ID
   AND J.MODEL_NAME = C.MODEL_NAME
INNER JOIN dbo.CW_DATA_USAGE DU
    ON DU.CO_ID = C.CO_ID
   AND DU.MODEL_NAME = C.MODEL_NAME
   AND DU.DM_DELETES = 1
LEFT JOIN dbo.ATTRIBUTE A
    ON A.AT_ID = DU.AT_ID
   AND A.MODEL_NAME = DU.MODEL_NAME
LEFT JOIN dbo.ENTITY E
    ON E.EN_ID = A.EN_ID
   AND E.MODEL_NAME = A.MODEL_NAME
WHERE C.DI_ID = @DiID
  AND C.MODEL_NAME = @ModelName
ORDER BY C.CO_ID, DU.DM_SEQ, A.AT_NAME;