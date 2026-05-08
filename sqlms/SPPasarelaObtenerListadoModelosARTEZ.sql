USE [erwin_evolve]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[SPPasarelaObtenerListadoModelosARTEZ]
AS
	SET NOCOUNT ON

BEGIN
	BEGIN TRY
		SELECT [MO_FILE] AS [CodigoModelo],
			   [MO_NAME] AS [DescripcionModelo]
		FROM [dbo].[MODEL]
		WHERE [MODEL_NAME] = 'CWADMN09'
		  AND [MO_NAME] LIKE 'ARTEZ%'
	END TRY
	
	BEGIN CATCH
		DECLARE @ErrorMessage AS NVARCHAR(2048) = '[' + CONVERT(VARCHAR, COALESCE(ERROR_NUMBER(), '')) + '] ' + COALESCE(ERROR_MESSAGE(), '') + ' --> [PA] ' + COALESCE(ERROR_PROCEDURE(), 'SPPasarelaObtenerListadoModelosARTEZ') + ' (línea: '+ CONVERT(VARCHAR(4), ERROR_LINE()) + ')';
		THROW 50000, @ErrorMessage, 1;
	END CATCH
END
GO

IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description', N'SCHEMA', N'dbo', N'PROCEDURE', N'SPPasarelaObtenerListadoModelosARTEZ', NULL, NULL))
	BEGIN
		EXEC sys.sp_addextendedproperty
			@name=N'MS_Description', @value=N'Obtiene el Listado de Modelos de ERWIN para la Infraestructura ARTEZ.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPPasarelaObtenerListadoModelosARTEZ'
	END
ELSE
	BEGIN
		EXEC sys.sp_updateextendedproperty
			@name=N'MS_Description', @value=N'Obtiene el Listado de Modelos de ERWIN para la Infraestructura ARTEZ.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPPasarelaObtenerListadoModelosARTEZ'
	END
GO

GRANT EXECUTE ON [dbo].[SPPasarelaObtenerListadoModelosARTEZ] TO [Ejecutar_Procedimientos] AS [dbo]
GO