/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Método: LeerAccionesDi
   Ejecutar en BD origen ERWIN.
   Devuelve filas con forma de ACCIONES_DI.
*/

-- USE erwin_evolve;
-- GO

DECLARE @P_MODEL_NAME sysname = N'ARTEZELI';
DECLARE @P_DI_ID int = 3750;
DECLARE @P_PROCEDIMIENTO nvarchar(255) = N'TA999900';

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
ORDER BY ORDEN_N1, ORDEN_ACC;
