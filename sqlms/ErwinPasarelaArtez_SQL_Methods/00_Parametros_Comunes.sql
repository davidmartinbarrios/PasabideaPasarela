/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Copia este bloque al inicio de cualquier prueba. */
DECLARE @P_MODEL_NAME    sysname        = N'ARTEZELI';
DECLARE @P_DI_ID         int            = 3750;
DECLARE @P_PROCEDIMIENTO nvarchar(255)  = N'TA999900';

SELECT
    @P_MODEL_NAME AS P_MODEL_NAME,
    @P_DI_ID AS P_DI_ID,
    @P_PROCEDIMIENTO AS P_PROCEDIMIENTO;
