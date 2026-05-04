-- Auto-generado a partir de PASARELA_schema.txt
-- Cambia el nombre de BD si lo necesitas.
SET NOCOUNT ON;
GO

IF DB_ID(N'PASARELA') IS NULL
BEGIN
    CREATE DATABASE [PASARELA] COLLATE Modern_Spanish_CI_AS;
END
GO
USE [PASARELA];
GO

GO

IF OBJECT_ID(N'dbo.ACCIONES_DI', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ACCIONES_DI] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ORDEN_N1] numeric(18,0) CONSTRAINT [DF_ACCIONES_DI_ORDEN_N1] DEFAULT (0) NOT NULL,
        [ORDEN_N2] numeric(18,0) CONSTRAINT [DF_ACCIONES_DI_ORDEN_N2] DEFAULT (0) NOT NULL,
        [ORDEN_N3] numeric(18,0) CONSTRAINT [DF_ACCIONES_DI_ORDEN_N3] DEFAULT (0) NOT NULL,
        [ORDEN_N4] numeric(18,0) CONSTRAINT [DF_ACCIONES_DI_ORDEN_N4] DEFAULT (0) NOT NULL,
        [ORDEN_N5] numeric(18,0) CONSTRAINT [DF_ACCIONES_DI_ORDEN_N5] DEFAULT (0) NOT NULL,
        [ORDEN_ACC] numeric(18,0) NOT NULL,
        [ID_ACCION] numeric(18,0) NULL,
        [NOM_ACCION] varchar(250) NULL,
        [NUM_ACCION] numeric(18,0) NULL,
        [TIPO_ACCION] varchar(250) NULL,
        [PATH_HIDRA] varchar(50) NULL,
        [NUM_SEQ] numeric(18,0) NULL,
        [DI_ID] numeric(18,0) NULL,
        [NOMBRE] varchar(250) CONSTRAINT [DF_ACCIONES_DI_NOMBRE] DEFAULT ('') NULL,
        [METOFLY] char(1) CONSTRAINT [DF_ACCIONES_DI_METOFLY] DEFAULT ('S') NULL,
        [NOMBRE_CM] varchar(255) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.BDDDP4', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BDDDP4] (
        [CONTENIDO] varchar(8000) NULL,
        [ORDEN] int NULL
    );
END
GO

IF OBJECT_ID(N'dbo.BDDDP4_BAK', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BDDDP4_BAK] (
        [CONTENIDO] varchar(8000) NULL,
        [ORDEN] int NULL
    );
END
GO

IF OBJECT_ID(N'dbo.BDDDP4_ORIGINAL', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BDDDP4_ORIGINAL] (
        [CONTENIDO] varchar(8000) NULL,
        [ORDEN] int NULL
    );
END
GO

IF OBJECT_ID(N'dbo.CONECTORES_DI', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CONECTORES_DI] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ID_CONECTOR] varchar(250) NULL,
        [DIAGRAMA] varchar(250) NULL,
        [NUM] numeric(18,0) NULL,
        [NUM_SEC_DESDE] numeric(18,0) NULL,
        [NUM_SEC_HASTA] numeric(18,0) NULL,
        [CAT_CONECTOR] varchar(250) NULL,
        [CAT_CONECTOR2] varchar(250) CONSTRAINT [DF_CONECTORES_DI_CAT_CONECTOR2] DEFAULT ('') NULL,
        [DI_ID] numeric(18,0) NULL,
        [ORDEN_N1] numeric(18,0) NULL,
        [ORDEN_N2] numeric(18,0) NULL,
        [ORDEN_N3] numeric(18,0) NULL,
        [ORDEN_N4] numeric(18,0) NULL,
        [TIPO_CONECTOR] numeric(18,0) NULL,
        [SALIDA] char(1) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.CONECTOR_ACC', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CONECTOR_ACC] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ID_CONECTOR] numeric(18,0) NULL,
        [ID_DIAGRAMA] numeric(18,0) NULL,
        [NUM_CONECTOR] numeric(18,0) NULL,
        [NUM_SEQ_DESDE] numeric(18,0) NULL,
        [NUM_SEQ_HASTA] numeric(18,0) NULL,
        [CAT_CONECTOR] varchar(250) NULL,
        [IND_SALIDA_TRAM] char(1) NULL,
        [DI_ID] numeric(18,0) NULL,
        [ACC_DESDE] char(50) NULL,
        [ACC_HASTA] char(50) NULL,
        [CAT_CONECTOR2] varchar(250) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.DIAGRAMA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[DIAGRAMA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ORDEN_N1] numeric(18,0) CONSTRAINT [DF_DIAGRAMA_ORDEN_N1] DEFAULT (0) NOT NULL,
        [ORDEN_N2] numeric(18,0) CONSTRAINT [DF_DIAGRAMA_ORDEN_N2] DEFAULT (0) NOT NULL,
        [ORDEN_N3] numeric(18,0) CONSTRAINT [DF_DIAGRAMA_ORDEN_N3] DEFAULT (0) NOT NULL,
        [ORDEN_N4] numeric(18,0) CONSTRAINT [DF_DIAGRAMA_ORDEN_N4] DEFAULT (0) NOT NULL,
        [ORDEN_N5] numeric(18,0) CONSTRAINT [DF_DIAGRAMA_ORDEN_N5] DEFAULT (0) NOT NULL,
        [ID_DIAG_N1] numeric(18,0) NULL,
        [ID_DIAG_N2] numeric(18,0) NULL,
        [ID_DIAG_N3] numeric(18,0) NULL,
        [ID_DIAG_N4] numeric(18,0) NULL,
        [ID_DIAG_N5] numeric(18,0) NULL,
        [ID_DIAGRAMA] numeric(18,0) NULL,
        [CAT_DIAGRAMA] varchar(250) NULL,
        [ID_PADRE] numeric(18,0) NULL,
        [NUM_DIAGRAMA] numeric(18,0) NULL,
        [EXPLOTA_ACC] char(1) NULL,
        [NUM_SEQ] numeric(18,0) NULL,
        [INDPARA] char(3) NULL,
        [INDJUMP] char(1) CONSTRAINT [DF_DIAGRAMA_INDJUMP] DEFAULT ('N') NULL,
        [INDCANCEL] char(1) NULL,
        [NOMBRE] varchar(250) NULL,
        [SALIDAS] numeric(18,0) NULL,
        [INDCOMUN] char(1) CONSTRAINT [DF_DIAGRAMA_INDCOMUN] DEFAULT ('') NULL,
        [DI_ID] numeric(18,0) NULL,
        [NIVEL] numeric(18,0) NULL,
        [ARBOL] varchar(250) NULL,
        [USERDEFINED] nvarchar(4000) NULL,
        [PLAZOTIPO1] char(4) NULL,
        [PLAZOTIPO2] char(4) NULL,
        [NIV_TRAMIT] char(4) NULL,
        [BLOQUEO_EXP] char(1) NULL,
        [UNION_RAMAS] char(1) NULL,
        [TRAMIT_SIMUL] char(1) NULL,
        [TRAM_OCULTO] char(1) NULL,
        [IND_VALORVAR] char(1) NULL,
        [VUELTA_ATRAS] char(1) NULL,
        [NOMBRE_TRAM] varchar(250) NULL,
        [CODFASE] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [CODSFASE] varchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [DATFASUB] varchar(25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    );
END
GO

IF OBJECT_ID(N'dbo.EQUIV_UT', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[EQUIV_UT] (
        [CODCM] int NOT NULL,
        [CODHN] int NOT NULL,
        [CODUTRHN] char(15) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.ERRORES', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ERRORES] (
        [PROCEDIMIENTO] varchar(255) NULL,
        [USERID] varchar(50) NOT NULL,
        [AMBITO] varchar(100) NULL,
        [DESCRIPCION] varchar(1024) NULL,
        [FECHA] datetime CONSTRAINT [DF_ERRORES_FECHA] DEFAULT (getdate()) NOT NULL,
        [LUGAR] varchar(200) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.ERRORES_HISTORICO', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ERRORES_HISTORICO] (
        [PROCEDIMIENTO] varchar(255) NULL,
        [USERID] varchar(50) NOT NULL,
        [AMBITO] varchar(100) NULL,
        [DESCRIPCION] varchar(1024) NULL,
        [FECHA] datetime NULL,
        [LUGAR] varchar(200) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.INI', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[INI] (
        [SECCION] varchar(50) NOT NULL,
        [CLAVE] varchar(50) NOT NULL,
        [VALOR] varchar(255) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.PARAMETROS', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PARAMETROS] (
        [SECCION] nvarchar(20) NOT NULL,
        [IDENTIFICADOR] nvarchar(20) NOT NULL,
        [VALOR] nvarchar(1000) NULL,
        [OWNER] nvarchar(5) NULL,
        [ENTIDAD] nvarchar(5) NULL,
        [FAMILIA] nvarchar(5) NULL,
        [NOMBRE] nvarchar(2) NULL,
        [REFERENCIA] nvarchar(2) NULL,
        [ARRANQUE] nvarchar(20) NULL,
        [GESTION] nvarchar(20) NULL,
        [GENERALES] nvarchar(20) NULL,
        [WORD] nvarchar(20) NULL,
        [DOCUMENTOS] nvarchar(20) NULL,
        [MENSAJES] nvarchar(20) NULL,
        [ESPECIFICAS] nvarchar(20) NULL,
        [GENE_APLIC] nvarchar(20) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.PARAM_ACC', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PARAM_ACC] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ORDEN_N1] numeric(18,0) CONSTRAINT [DF_PARAM_ACC_ORDEN_N1] DEFAULT (0) NOT NULL,
        [ORDEN_N2] numeric(18,0) CONSTRAINT [DF_PARAM_ACC_ORDEN_N2] DEFAULT (0) NOT NULL,
        [ORDEN_N3] numeric(18,0) CONSTRAINT [DF_PARAM_ACC_ORDEN_N3] DEFAULT (0) NOT NULL,
        [ORDEN_N4] numeric(18,0) CONSTRAINT [DF_PARAM_ACC_ORDEN_N4] DEFAULT (0) NOT NULL,
        [ORDEN_N5] numeric(18,0) CONSTRAINT [DF_PARAM_ACC_ORDEN_N5] DEFAULT (0) NOT NULL,
        [ORDEN_ACC] decimal(18,0) NOT NULL,
        [ORDEN_AC_SUB] decimal(18,0) NULL,
        [ID_ACCION] numeric(18,0) NULL,
        [PARAMETRO] varchar(250) NULL,
        [VALOR] varchar(250) NULL,
        [ORDEN_PA] numeric(18,0) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.PROCESOS_HISTORICO', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PROCESOS_HISTORICO] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [USERID] varchar(50) NOT NULL,
        [ID] numeric(18,0) NOT NULL,
        [NOMBRE_CM] varchar(255) NULL,
        [FECHA_ACTIVACION] varchar(10) NULL,
        [NUEVA_VERSION] char(1) NULL,
        [CONEXION] varchar(255) NULL,
        [OBSERVACIONES] varchar(255) NULL,
        [FINALIZADO] char(1) NULL,
        [CONEXION2] varchar(255) CONSTRAINT [DF_PROCESOS_HISTORICO_CONEXION2] DEFAULT ('') NULL,
        [BASEDATOS] char(10) NULL,
        [FECHA] datetime NOT NULL,
        [FECHAINICIO] datetime NULL,
        [FECHAFIN] datetime NULL,
        [GRUPO] varchar(50) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.PROCESOS_PENDIENTES', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PROCESOS_PENDIENTES] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [USERID] varchar(50) NOT NULL,
        [ID] numeric(18,0) NOT NULL,
        [NOMBRE_CM] varchar(255) NULL,
        [FECHA_ACTIVACION] varchar(10) NULL,
        [NUEVA_VERSION] char(1) NULL,
        [CONEXION] varchar(255) NULL,
        [OBSERVACIONES] varchar(255) NULL,
        [FINALIZADO] char(1) NULL,
        [CONEXION2] varchar(255) CONSTRAINT [DF_PROCESOS_PENDIENTES_CONEXION2] DEFAULT ('') NOT NULL,
        [BASEDATOS] char(10) NULL,
        [FECHA] datetime CONSTRAINT [DF_PROCESOS_PENDIENTES_FECHA] DEFAULT (getdate()) NULL,
        [FECHAINICIO] datetime NULL,
        [FECHAFIN] datetime NULL,
        [GRUPO] varchar(50) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.PROPIEDADES_DI', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PROPIEDADES_DI] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ORDEN_N1] numeric(18,0) CONSTRAINT [DF_PROPIEDADES_DI_ORDEN_N1] DEFAULT (0) NOT NULL,
        [ORDEN_N2] numeric(18,0) CONSTRAINT [DF_PROPIEDADES_DI_ORDEN_N2] DEFAULT (0) NOT NULL,
        [ORDEN_N3] numeric(18,0) CONSTRAINT [DF_PROPIEDADES_DI_ORDEN_N3] DEFAULT (0) NOT NULL,
        [ORDEN_N4] numeric(18,0) CONSTRAINT [DF_PROPIEDADES_DI_ORDEN_N4] DEFAULT (0) NOT NULL,
        [ORDEN_N5] numeric(18,0) CONSTRAINT [DF_PROPIEDADES_DI_ORDEN_N5] DEFAULT (0) NOT NULL,
        [ID_DIAGRAMA] numeric(18,0) NULL,
        [NOM_DIAGRAMA] varchar(250) NULL,
        [TIPO_DIAGRAMA] varchar(250) NULL,
        [PLAZTIP1_DI] varchar(250) NULL,
        [PLAZTIP2_DI] varchar(250) NULL,
        [NIVELTRAM_DI] varchar(250) NULL,
        [INDBLOQ_DI] varchar(250) NULL,
        [INDRAM_DI] varchar(250) NULL,
        [INDPERSINT] char(1) CONSTRAINT [DF_PROPIEDADES_DI_INDPERSINT] DEFAULT (0) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBDCELTA_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBDCELTA_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [ELTA_AMBITO] char(10) NOT NULL,
        [ELTA_CODTAB] char(2) NOT NULL,
        [ELTA_IDIOMA] char(1) NOT NULL,
        [ELTA_CLAVE] char(15) NOT NULL,
        [ELTA_DESC20] char(20) NULL,
        [ELTA_DESC40] char(40) NULL,
        [ELTA_DESC60] char(60) NULL,
        [ELTA_FECBAJA] datetime NULL,
        [AUX] numeric(18,0) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBPFIN01_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBPFIN01_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [CODPROCE] decimal(18,0) NOT NULL,
        [CODENTID] decimal(18,0) NOT NULL,
        [CODFAMIL] decimal(18,0) NOT NULL,
        [CODEXTPR] char(15) NOT NULL,
        [CODGRPRO] char(2) NOT NULL,
        [EFSITADC] varchar(100) NULL,
        [EFSITADE] varchar(100) NULL,
        [DIRECURL] varchar(250) NULL,
        [DIRECCM] varchar(250) NULL,
        [ISOLITEL] char(1) NULL,
        [INDICADO] char(1) CONSTRAINT [DF_TBPFIN01_PA_INDICADO] DEFAULT ('N') NOT NULL,
        [CODASUNT] decimal(18,0) NULL,
        [CODURESP] decimal(18,0) NULL,
        [CODURESU] decimal(18,0) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBPFIN02_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBPFIN02_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [CODPROCE] decimal(18,0) NOT NULL,
        [VERSIONP] decimal(18,0) NOT NULL,
        [VERSIONH] decimal(18,0) NOT NULL,
        [FECVALDE] datetime NOT NULL,
        [FECVALHA] datetime NULL,
        [NIVAUTOR] decimal(18,0) NULL,
        [ESTADOVP] char(1) NULL,
        [OBSERVAC] varchar(250) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBPFIN03_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBPFIN03_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [CODPROCE] decimal(18,0) NOT NULL,
        [VERSIONP] decimal(18,0) NULL,
        [ORDENTRA] decimal(18,0) NOT NULL,
        [CODTRAAD] decimal(18,0) NULL,
        [INDPROCE] char(1) NULL,
        [NIVTRAMI] decimal(18,0) NULL,
        [INDBLOQ] char(1) NULL,
        [NOMBRE] char(50) NULL,
        [CDESTRAM] numeric(18,0) NULL,
        [INDSIMUL] char(1) NULL,
        [TRAMOCUL] char(1) NULL,
        [CODFASE] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [CODSFASE] varchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [DATFASUB] varchar(25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [VAR_FLUJO] varchar(254) NULL,
        [CRITERIO_COMP] varchar(2) NULL,
        [VALOR_FIJO] varchar(254) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBPFIN04_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBPFIN04_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [CODPROCE] decimal(18,0) NOT NULL,
        [VERSIONP] decimal(18,0) NULL,
        [ORDENTRA] decimal(18,0) NOT NULL,
        [CODTRAAD] decimal(18,0) NOT NULL,
        [CODUNITR] char(15) NOT NULL,
        [CODUTRHN] char(15) NULL,
        [INDTRAMI] char(1) NULL,
        [INDCOMUN] char(1) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.TBPFIN23_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBPFIN23_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [CODENTID] decimal(18,0) NOT NULL,
        [CODFAMIL] decimal(18,0) NOT NULL,
        [CODUNITR] decimal(18,0) NOT NULL,
        [CODUTRHN] char(15) NOT NULL,
        [INDCONVE] char(1) NULL,
        [TIPOUNTR] char(1) NULL,
        [CARGO] char(8) NULL,
        [UTSUSTIT] decimal(18,0) NULL,
        [NUMORG] decimal(18,0) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.USUARIOS', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[USUARIOS] (
        [USERID] varchar(50) NOT NULL,
        [PASSWORD] varchar(50) NOT NULL,
        [NOMBRE] varchar(50) NOT NULL,
        [Activo] bit CONSTRAINT [DF_USUARIOS_Activo] DEFAULT (0) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.VALIDACIONES', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[VALIDACIONES] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [MENSAJE] varchar(510) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.VARIABLES_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[VARIABLES_PA] (
        [IDATRIBU] decimal(18,0) NOT NULL,
        [DESATRIB] varchar(250) NULL,
        [CODHIDRA] varchar(25) NOT NULL,
        [INDTIPOA] char(1) NULL,
        [INDCTA] char(1) NULL,
        [DESCATRI] varchar(250) NULL,
        [DESEATRI] varchar(250) NULL,
        [FAMILIA] char(2) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.VAR_ALTA_PA', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[VAR_ALTA_PA] (
        [PROCEDIMIENTO] varchar(50) NOT NULL,
        [NOMPROC] varchar(15) NULL,
        [ORDEN] decimal(18,0) NOT NULL,
        [CODHIDRA] varchar(25) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.dtproperties', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[dtproperties] (
        [id] int IDENTITY(1,1) NOT NULL,
        [objectid] int NULL,
        [property] varchar(64) NOT NULL,
        [value] varchar(255) NULL,
        [uvalue] nvarchar(255) NULL,
        [lvalue] image NULL,
        [version] int CONSTRAINT [DF_dtproperties_version] DEFAULT (0) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.sysdiagrams', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[sysdiagrams] (
        [name] sysname NOT NULL,
        [principal_id] int NOT NULL,
        [diagram_id] int IDENTITY(1,1) NOT NULL,
        [version] int NULL,
        [definition] varbinary(max) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.wfFlowActions', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[wfFlowActions] (
        [Flow] char(25) NOT NULL,
        [Version] int CONSTRAINT [DF_wfFlowActions_Version] DEFAULT (0) NOT NULL,
        [FlowOrder] int NOT NULL,
        [Id] int CONSTRAINT [DF_wfFlowActions_Id] DEFAULT (0) NOT NULL,
        [Action] char(25) CONSTRAINT [DF_wfFlowActions_Action] DEFAULT ('') NULL,
        [Path] char(25) CONSTRAINT [DF_wfFlowActions_Path] DEFAULT ('') NULL,
        [Param] char(25) CONSTRAINT [DF_wfFlowActions_Param] DEFAULT ('') NULL,
        [Value] varchar(500) CONSTRAINT [DF_wfFlowActions_Value] DEFAULT ('') NULL,
        [Comments] varchar(255) NULL
    );
END
GO

IF OBJECT_ID(N'dbo.wfFlows', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[wfFlows] (
        [Flow] char(25) NOT NULL,
        [Version] int NOT NULL,
        [Active] char(1) NOT NULL,
        [FlowName] char(25) NULL,
        [Comments] varchar(255) NULL,
        [Running] char(1) NOT NULL,
        [Start] datetime NULL,
        [StopOlderVersions] char(1) NOT NULL
    );
END
GO

-- Primary Keys
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.ACCIONES_DI') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[ACCIONES_DI] ADD CONSTRAINT [PK_ACCIONES_DI] PRIMARY KEY ([PROCEDIMIENTO], [ORDEN_N1], [ORDEN_N2], [ORDEN_N3], [ORDEN_N4], [ORDEN_N5], [ORDEN_ACC]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.DIAGRAMA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[DIAGRAMA] ADD CONSTRAINT [PK_DIAGRAMA] PRIMARY KEY ([PROCEDIMIENTO], [ORDEN_N1], [ORDEN_N2], [ORDEN_N3], [ORDEN_N4], [ORDEN_N5]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.EQUIV_UT') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[EQUIV_UT] ADD CONSTRAINT [PK_EQUIV_UT] PRIMARY KEY ([CODCM], [CODHN]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.INI') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[INI] ADD CONSTRAINT [PK_INI] PRIMARY KEY ([SECCION], [CLAVE]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.PARAM_ACC') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[PARAM_ACC] ADD CONSTRAINT [PK_PARAM_ACC] PRIMARY KEY ([PROCEDIMIENTO], [ORDEN_N1], [ORDEN_N2], [ORDEN_N3], [ORDEN_N4], [ORDEN_N5], [ORDEN_ACC], [ORDEN_PA]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.PROCESOS_HISTORICO') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[PROCESOS_HISTORICO] ADD CONSTRAINT [PK_PROCESOS_HISTORICO] PRIMARY KEY ([PROCEDIMIENTO], [USERID], [ID], [FECHA]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.PROCESOS_PENDIENTES') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[PROCESOS_PENDIENTES] ADD CONSTRAINT [PK_PROCESOS_PENDIENTES] PRIMARY KEY ([PROCEDIMIENTO], [USERID], [ID]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.TBPFIN02_PA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[TBPFIN02_PA] ADD CONSTRAINT [PK_TBPFIN02_PA] PRIMARY KEY ([PROCEDIMIENTO], [CODPROCE], [VERSIONP], [VERSIONH]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.TBPFIN03_PA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[TBPFIN03_PA] ADD CONSTRAINT [PK_TBPFIN03_PA] PRIMARY KEY ([PROCEDIMIENTO], [CODPROCE], [ORDENTRA]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.TBPFIN04_PA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[TBPFIN04_PA] ADD CONSTRAINT [PK_TBPFIN24_PA] PRIMARY KEY ([PROCEDIMIENTO], [CODPROCE], [ORDENTRA], [CODTRAAD], [CODUNITR]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.TBPFIN23_PA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[TBPFIN23_PA] ADD CONSTRAINT [PK_TBPFIN23_PA] PRIMARY KEY ([PROCEDIMIENTO], [CODENTID], [CODFAMIL], [CODUNITR]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.USUARIOS') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[USUARIOS] ADD CONSTRAINT [PK_USUARIOS] PRIMARY KEY ([USERID]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.VARIABLES_PA') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[VARIABLES_PA] ADD CONSTRAINT [PK_VARIABLES_PA] PRIMARY KEY ([IDATRIBU], [CODHIDRA]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.dtproperties') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[dtproperties] ADD CONSTRAINT [pk_dtproperties] PRIMARY KEY ([id], [property]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.sysdiagrams') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[sysdiagrams] ADD CONSTRAINT [PK__sysdiagr__C2B05B61D2242FAC] PRIMARY KEY ([diagram_id]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.wfFlowActions') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[wfFlowActions] ADD CONSTRAINT [PK_wfFlowActions] PRIMARY KEY ([Flow], [Version], [FlowOrder], [Id]);
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.wfFlows') AND [type] = 'PK')
BEGIN
    ALTER TABLE [dbo].[wfFlows] ADD CONSTRAINT [PK_wfFlows] PRIMARY KEY ([Flow], [Version]);
END
GO

-- Foreign Keys

-- Descripciones (MS_Description)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de Fase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'DIAGRAMA',
    @level2type=N'COLUMN',@level2name=N'CODFASE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de SubFase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'DIAGRAMA',
    @level2type=N'COLUMN',@level2name=N'CODSFASE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dato FaseSubfase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'DIAGRAMA',
    @level2type=N'COLUMN',@level2name=N'DATFASUB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de Fase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'TBPFIN03_PA',
    @level2type=N'COLUMN',@level2name=N'CODFASE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de SubFase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'TBPFIN03_PA',
    @level2type=N'COLUMN',@level2name=N'CODSFASE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dato FaseSubfase',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE',@level1name=N'TBPFIN03_PA',
    @level2type=N'COLUMN',@level2name=N'DATFASUB';
GO