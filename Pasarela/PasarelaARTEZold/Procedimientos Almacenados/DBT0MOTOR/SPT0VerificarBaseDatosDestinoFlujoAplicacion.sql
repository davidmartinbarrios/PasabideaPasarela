USE [DBT0MOTOR]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ================================================================
-- Create date: 14/02/2023
-- Description: Verifica si la Base de Datos Destino seleccionada para el Flujo en la Aplicación es Válida
-- ================================================================
CREATE OR ALTER PROCEDURE [dbo].[SPT0VerificarBaseDatosDestinoFlujoAplicacion]
	@Idnt_Aplicacion					VARCHAR(2),
	@BaseDatosDestinoFlujo				VARCHAR(54),
	@NombreFlujo						VARCHAR(50),
	@BaseDatosDestinoCorrecta			CHAR(1) OUTPUT, -- S: Correcta, N: No correcta
	@BaseDatosDestinoExistente			VARCHAR(54) OUTPUT
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
		-- Obtenemos la Base de Datos en la que se encuentra definido el Flujo
		SET @BaseDatosDestinoExistente = NULL
		SELECT @BaseDatosDestinoExistente = [Nombre_Base_Datos_Ejecucion]
		FROM [dbo].[TBT0ENBaseDatosFlujoAplicacion]
		WHERE [Idnt_Aplicacion] = @Idnt_Aplicacion
		  AND [Nombre_Flujo] = @NombreFlujo

		-- Comprobamos si ya existe el flujo para la Aplicación recibida por parámetro, ya que en caso afirmativo, la Base de Datos Destino tiene que coincidir
		IF @BaseDatosDestinoExistente IS NOT NULL OR @BaseDatosDestinoExistente != N''
			BEGIN
				-- Está registrado el flujo para la Aplicaciíon, así que hay que comprobar si la Base de Datos Destino para el Flujo recibida coincide con la que está dada de alta en el motor
				IF @BaseDatosDestinoExistente = @BaseDatosDestinoFlujo
					BEGIN
						-- Coinciden, así que la Base de Datos es correcta
						SET @BaseDatosDestinoCorrecta = N'S'
					END

				ELSE
					BEGIN
						-- No coinciden, así que la Base de Datos no es correcta
						SET @BaseDatosDestinoCorrecta = N'N'
					END
			END

		ELSE
			BEGIN
				-- No está registrado el flujo para la Aplicación, así que se puede dar por buena la Base de Datos Destino para el Flujo recibida, y registramos el Flujo para la Aplicación con la Base de Datos
				SET @BaseDatosDestinoCorrecta = N'S'
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

IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description', N'SCHEMA', N'dbo', N'PROCEDURE', N'SPT0VerificarBaseDatosDestinoFlujoAplicacion', NULL, NULL))
	BEGIN
		EXEC sys.sp_addextendedproperty
			@name=N'MS_Description', @value=N'Comprueba si la Base de Datos Destino seleccionada para el Flujo y Aplicación es correcta, ya que si ya se ha registrado, la Base de Datos seleccionada debe coincidir con la parametrizada.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPT0VerificarBaseDatosDestinoFlujoAplicacion'
	END
ELSE
	BEGIN
		EXEC sys.sp_updateextendedproperty
			@name=N'MS_Description', @value=N'Comprueba si la Base de Datos Destino seleccionada para el Flujo y Aplicación es correcta, ya que si ya se ha registrado, la Base de Datos seleccionada debe coincidir con la parametrizada.',
			@level0type=N'SCHEMA', @level0name=N'dbo',
			@level1type=N'PROCEDURE', @level1name=N'SPT0VerificarBaseDatosDestinoFlujoAplicacion'
	END
GO

GRANT EXECUTE ON [dbo].[SPT0VerificarBaseDatosDestinoFlujoAplicacion] TO [Ejecutar_Procedimientos] AS [dbo]
GO