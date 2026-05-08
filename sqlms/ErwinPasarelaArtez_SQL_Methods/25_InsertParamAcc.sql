/*
Proyecto: Pasarela / ERWIN -> PASARELA_ARTEZ
Clase origen: ErwinPasarelaArtezTransformer
Uso: ajustar variables DECLARE superiores y ejecutar en SSMS.
Fecha generación: 2026-05-06
*/

/* Método: InsertParamAcc
   Ejecutar en BD destino PASARELA_ARTEZ / PASARELA.
   Plantilla para insertar UNA fila.
*/

-- USE PASARELA_ARTEZ;
-- GO

DECLARE @PROCEDIMIENTO nvarchar(255) = N'TA999900';
DECLARE @ORDEN_N1 int = 1, @ORDEN_N2 int = 0, @ORDEN_N3 int = 0, @ORDEN_N4 int = 0, @ORDEN_N5 int = 0;
DECLARE @ORDEN_ACC int = 1;
DECLARE @ID_ACCION int = 0;
DECLARE @PARAMETRO nvarchar(255) = N'@PON_PARAMETRO';
DECLARE @VALOR nvarchar(4000) = N'PON_VALOR';
DECLARE @ORDEN_PA int = 1;

INSERT INTO dbo.PARAM_ACC
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ORDEN_ACC, ID_ACCION, PARAMETRO, VALOR, ORDEN_PA
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ORDEN_ACC, @ID_ACCION, @PARAMETRO, @VALOR, @ORDEN_PA
);
