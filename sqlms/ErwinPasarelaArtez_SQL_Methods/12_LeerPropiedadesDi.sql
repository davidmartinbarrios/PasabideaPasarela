/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Método: LeerPropiedadesDi
   Ejecutar en BD origen ERWIN.
   Devuelve filas con forma de PROPIEDADES_DI.
*/

-- USE erwin_evolve;
-- GO

DECLARE @P_MODEL_NAME sysname = N'ARTEZELI';
DECLARE @P_DI_ID int = 3750;
DECLARE @P_PROCEDIMIENTO nvarchar(255) = N'TA999900';

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
ORDER BY SH_Y DESC, SH_X ASC, SH_SEQ;
