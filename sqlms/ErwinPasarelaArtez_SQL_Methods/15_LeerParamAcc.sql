/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Método: LeerParamAcc
   Ejecutar en BD origen ERWIN.
   Devuelve filas con forma de PARAM_ACC.
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
ORDER BY ORDEN_N1, ORDEN_ACC, ORDEN_PA;
