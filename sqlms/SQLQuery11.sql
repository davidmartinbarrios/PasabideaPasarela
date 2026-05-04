USE erwin_evolve;
GO

DECLARE @DI_NAME NVARCHAR(254) = N'_Modelo 030 NIF V5';

SELECT
    ORDEN_N1 = 1,
    ORDEN_N2 = 0,
    ORDEN_N3 = 0,
    ORDEN_N4 = 0,
    ORDEN_N5 = 0,

    CAT_DIAGRAMA = CASE
        WHEN D.ANO_TABNR = 8953 THEN 'P'
        WHEN D.ANO_TABNR = 9096 THEN 'R'
        ELSE 'O'
    END,

    NOMBRE = D.DI_NAME,
    USERDEFINED = CAST(NULL AS varchar(max)),
    NIVEL = 0,

    ARBOL = RIGHT('000000' + CAST(D.DI_ID AS varchar(6)), 6),

    PLAZOTIPO1   = CAST(NULL AS varchar(20)),
    PLAZOTIPO2   = CAST(NULL AS varchar(20)),
    NIV_TRAMIT   = CAST(NULL AS varchar(10)),
    BLOQUEO_EXP  = CAST(NULL AS varchar(5)),
    UNION_RAMAS  = CAST(NULL AS varchar(5)),
    TRAMIT_SIMUL = CAST(NULL AS varchar(5)),
    TRAM_OCULTO  = CAST(NULL AS varchar(5)),
    IND_VALORVAR = CAST(NULL AS varchar(5)),
    VUELTA_ATRAS = CAST(NULL AS varchar(5)),

    NOMBRE_TRAM = D.DI_NAME,

    -- auxiliares útiles para depurar en esta fase
    D.DI_ID,
    D.ANO_ID,
    D.MODEL_NAME,
    D.ANO_TABNR
FROM dbo.DIAGRAM D
WHERE D.DI_NAME = @DI_NAME
ORDER BY D.DI_ID;