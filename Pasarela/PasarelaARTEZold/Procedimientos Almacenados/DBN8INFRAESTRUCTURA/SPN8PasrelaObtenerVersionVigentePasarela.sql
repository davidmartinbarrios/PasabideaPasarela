USE [DBN8INFRAESTRUCTURA]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ================================================================
-- Create date: 26/09/2024
-- Description: Obtener la Versión Vigente de Pasarela
-- ================================================================
CREATE OR ALTER PROCEDURE [dbo].[SPN8PasrelaObtenerVersionVigentePasarela]
	@MajorVigente			INT OUTPUT,
	@MinorVigente			INT OUTPUT,
	@BuildVigente			INT OUTPUT,
	@RevisionVigente		INT OUTPUT,
	@FechaInicioVigencia	DATETIME2(7) OUTPUT,
	@Observaciones			VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	--Variables para control de errores	
    DECLARE @ErrorSeverity	INT,
			@ErrorState		INT,
			@Error			INT,
			@ErrorLine		INT,
			@ErrorMessage	NVARCHAR(4000),
			@ProcedureName	NVARCHAR(128),
			@MensajeRetorno	VARCHAR(8000)

	BEGIN TRY
		-- Obtenemos el número de Versiones Vigentes
		DECLARE @NumeroVersionesVigentes	INT = (SELECT COUNT(*) FROM [dbo].[TBN8Pasarela_Versiones] WHERE [Fecha_Fin_Vigencia] IS NULL)
		
		-- Comprobamos el Número de Versiones Vigentes
		IF @NumeroVersionesVigentes = 0
			BEGIN
				THROW 51000, 'No hay una versión vigente de Pasarela.', 1;
			END
			
		ELSE IF @NumeroVersionesVigentes > 1
			BEGIN
				THROW 52000, 'Hay más de una versión vigente de Pasarela.', 1;
			END
			
		ELSE
			BEGIN
				-- Tenemos una Versión Vigente, así que obtenemos sus datos
				SELECT @MajorVigente = [Major],
					   @MinorVigente = [Minor],
					   @BuildVigente = [Build],
					   @RevisionVigente = [Revision],
					   @FechaInicioVigencia = [Fecha_Inicio_Vigencia],
					   @Observaciones = [Observaciones]
				FROM [dbo].[TBN8Pasarela_Versiones]
				WHERE [Fecha_Fin_Vigencia] IS NULL
			END
			
		FROM [dbo].[TBN8Pasarela_Versiones]
		WHERE [Fecha_Fin_Vigencia] IS NULL
	END TRY

	BEGIN CATCH
		--Manejo de errores
		SELECT @ErrorSeverity = ERROR_SEVERITY(),
			   @ErrorState = ERROR_STATE(),
			   @Error= @@ERROR,
			   @ErrorLine=ERROR_LINE(),
			   @ErrorMessage = ERROR_MESSAGE()
		
		SELECT @ProcedureName = schema_name(schema_id) + '.' + object_name(object_id) FROM sys.objects WHERE object_id = @@procid
		
		SET @MensajeRetorno = N'Error en el procedimiento %s. Código de error: %d Linea: %d - Mensaje: %s';
		RAISERROR (@MensajeRetorno, @ErrorSeverity, @ErrorState, @ProcedureName, @Error, @ErrorLine, @ErrorMessage)
	END CATCH
END
GO

IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description', N'SCHEMA', N'dbo', N'PROCEDURE', N'SPN8PasrelaObtenerSiguienteVersionFlujo', NULL, NULL))
	BEGIN
		EXEC sys.sp_addextendedproperty
			@name=N'MS_Description', @value=N'Obtiene la Siguiente Versión para el Flujo a Crear con Pasarela.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPN8PasrelaObtenerSiguienteVersionFlujo'
	END
ELSE
	BEGIN
		EXEC sys.sp_updateextendedproperty
			@name=N'MS_Description', @value=N'Obtiene la Siguiente Versión para el Flujo a Crear con Pasarela.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPN8PasrelaObtenerSiguienteVersionFlujo'
	END
GO

GRANT EXECUTE ON [dbo].[SPN8PasrelaObtenerSiguienteVersionFlujo] TO [Ejecutar_Procedimientos] AS [dbo]
GO