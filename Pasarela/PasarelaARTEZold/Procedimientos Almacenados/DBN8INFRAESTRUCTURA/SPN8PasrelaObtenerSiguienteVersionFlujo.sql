USE [DBN8INFRAESTRUCTURA]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ================================================================
-- Create date: 14/02/2023
-- Description: Obtener la Siguiente Versión de un Flujo
-- ================================================================
CREATE OR ALTER PROCEDURE [dbo].[SPN8PasrelaObtenerSiguienteVersionFlujo]
	@Idnt_Aplicacion		VARCHAR(2),
	@BaseDatosDestinoFlujo	VARCHAR(54),
	@NombreFlujo			VARCHAR(50),
	@SiguienteVersion		INT OUTPUT
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
		DECLARE @ExisteFlujoBaseDatosAplicacion		BIT = NULL,
				@SQLObtenerSiguienteVersion			NVARCHAR(500) = NULL,
				@ParametrosObtenerSiguienteVersion	NVARCHAR(100) = NULL

		-- Comprobamos si existe el Flujo en la Base de Datos
		EXEC [dbo].[SPT0VerificarFlujoBaseDatosAplicacion] @Idnt_Aplicacion = @Idnt_Aplicacion,
														   @BaseDatosDestinoFlujo = @BaseDatosDestinoFlujo,
														   @NombreFlujo = @NombreFlujo,
														   @Existe = @ExisteFlujoBaseDatosAplicacion OUTPUT
		IF @ExisteFlujoBaseDatosAplicacion = 1
			BEGIN
				-- Existe el Flujo en la Aplicación y Base de Datos, así que hay que calcular la Siguiente Versión
				SET @SQLObtenerSiguienteVersion = N'SELECT @SiguienteVersionOUT = MAX([Version]) + 1 ' +
												  N'FROM [' + @BaseDatosDestinoFlujo + N'].[dbo].[wfFlows] ' +
												  N'WHERE [Flow] = @NombreFlujoIN'
				SET @ParametrosObtenerSiguienteVersion = N'@NombreFlujoIN VARCHAR(50),  @SiguienteVersionOUT INT OUTPUT'
				EXECUTE sp_executesql @SQLObtenerSiguienteVersion,
									  @ParametrosObtenerSiguienteVersion,
									  @NombreFlujoIN = @NombreFlujo,
									  @SiguienteVersionOUT = @SiguienteVersion OUT
			END

		ELSE
			BEGIN
				-- No existe el Flujo en la Aplicación y Base de Datos, así que la Versión será la 1
				SET @SiguienteVersion = 1
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