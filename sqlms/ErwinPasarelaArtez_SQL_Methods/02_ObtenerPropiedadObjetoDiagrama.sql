/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Método: ObtenerPropiedadObjetoDiagrama
   Ejecutar en BD origen ERWIN. Si estás ya en erwin_evolve, puedes quitar el prefijo erwin_evolve.dbo.
*/

-- USE erwin_evolve;
-- GO

DECLARE @MODEL_NAME sysname = N'ARTEZELI';
DECLARE @DI_ID int = 3750;
DECLARE @TIPO_OBJETO nvarchar(200) = N'Procedimiento';
DECLARE @SCRIPTNAME_PROPIEDAD nvarchar(500) = N'CÓDIGOPROCEDIMIENTO';

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
FROM erwin_evolve.dbo.SHAPE S
JOIN erwin_evolve.dbo.CW_OBJECT_TYPE OT
    ON OT.MODEL_NAME = S.MODEL_NAME
   AND OT.OT_ID = S.ANO_TABNR
JOIN erwin_evolve.dbo.CW_OBJECT CO
    ON CO.MODEL_NAME = S.MODEL_NAME
   AND CO.OT_ID = S.ANO_TABNR
   AND CO.GO_ID = S.ANO_ID
CROSS APPLY
(
    SELECT TRY_CAST(CONVERT(nvarchar(max), CO.USERDEFINED) AS xml) AS XML_DATA
) X
CROSS APPLY X.XML_DATA.nodes('//*[local-name()="property"]') P(N)
WHERE S.MODEL_NAME = @MODEL_NAME
  AND S.DI_ID = @DI_ID
  AND OT.OT_NAME = @TIPO_OBJETO
  AND P.N.value('@scriptname', 'nvarchar(500)') COLLATE DATABASE_DEFAULT
      = @SCRIPTNAME_PROPIEDAD COLLATE DATABASE_DEFAULT
ORDER BY S.SH_SEQ;
