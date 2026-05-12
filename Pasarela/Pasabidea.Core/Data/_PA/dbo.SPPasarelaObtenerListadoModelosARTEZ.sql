USE [erwin_evolve]
GO
/****** Object:  StoredProcedure [dbo].[SPPasarelaObtenerListadoModelosARTEZ]    Script Date: 12/05/2026 15:03:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[SPPasarelaObtenerListadoModelosARTEZ]
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
