USE [DBT0MOTOR]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ================================================================
-- Create date: 14/02/2023
-- Description: Comprueba si el flujo ya se había incluido en el motor para la Base de Datos
-- ================================================================
CREATE OR ALTER PROCEDURE [dbo].[SPT0VerificarFlujoBaseDatosAplicacion]
	@Idnt_Aplicacion		VARCHAR(2),
	@BaseDatosDestinoFlujo	VARCHAR(54),
	@NombreFlujo			VARCHAR(50),
	@Existe					BIT OUTPUT -- 0: No existe, 1: Existe
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
		-- Comprobamos si existe el Flujo en la Base de Datos
		IF EXISTS(SELECT 1 FROM [dbo].[TBT0ENBaseDatosFlujoAplicacion] WHERE [Idnt_Aplicacion] = @Idnt_Aplicacion AND [Nombre_Flujo] = @NombreFlujo AND [Nombre_Base_Datos_Ejecucion] = @BaseDatosDestinoFlujo)
			BEGIN
				SET @Existe = 1
			END
		ELSE
			BEGIN
				SET @Existe = 0
			END
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

IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description', N'SCHEMA', N'dbo', N'PROCEDURE', N'SPT0VerificarFlujoBaseDatosAplicacion', NULL, NULL))
	BEGIN
		EXEC sys.sp_addextendedproperty
			@name=N'MS_Description', @value=N'Comprueba si el Flujo ya existe en el Motor para la Aplicación y Base de Datos.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPT0VerificarFlujoBaseDatosAplicacion'
	END
ELSE
	BEGIN
		EXEC sys.sp_updateextendedproperty
			@name=N'MS_Description', @value=N'Comprueba si el Flujo ya existe en el Motor para la Aplicación y Base de Datos.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPT0VerificarFlujoBaseDatosAplicacion'
	END
GO

GRANT EXECUTE ON [dbo].[SPT0VerificarFlujoBaseDatosAplicacion] TO [Ejecutar_Procedimientos] AS [dbo]
GO