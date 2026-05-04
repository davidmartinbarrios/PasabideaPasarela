USE [DBN8INFRAESTRUCTURA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Creamos la Tabla para el Control de las Versiones de Pasarela
CREATE TABLE [dbo].[TBN8Pasarela_Versiones](
	[Major] INT
		CONSTRAINT [TBN8Pasarela_Versiones_Major_DF] DEFAULT (0) NOT NULL,
	[Minor] INT
		CONSTRAINT [TBN8Pasarela_Versiones_Minor_DF] DEFAULT (0) NOT NULL,
	[Build] INT
		CONSTRAINT [TBN8Pasarela_Versiones_Build_DF] DEFAULT (0) NOT NULL,
	[Revision] INT
		CONSTRAINT [TBN8Pasarela_Versiones_Revision_DF] DEFAULT (0) NOT NULL,
	[Fecha_Creacion] DATETIME2(7)
		CONSTRAINT [TBN8Pasarela_Versiones_Fecha_Creacion_DF] DEFAULT (SYSDATETIME()) NOT NULL,
	[Usuario_Creacion] VARCHAR(8)
		CONSTRAINT [TBN8Pasarela_Versiones_Usuario_Creacion_DF] DEFAULT ('') NOT NULL,
	[Fecha_Inicio_Vigencia] DATETIME2(7) NOT NULL,
	[Fecha_Fin_Vigencia] DATETIME2(7) NULL,
	[Observaciones] VARCHAR(4000)
		CONSTRAINT [TBN8Pasarela_Versiones_Observaciones_DF] DEFAULT ('') NOT NULL,
 CONSTRAINT [TBN8Pasarela_Versiones_PK] PRIMARY KEY CLUSTERED 
(
	[Major] ASC,
	[Minor] ASC,
	[Build] ASC,
	[Revision] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Añadimos las Descripciones de los Campos y de la Tabla
EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Entidad que almacena las Versiones de la Pasarela ARTEZ.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[CLUSTER][UNICO]',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'CONSTRAINT', @level2name=N'TBN8Pasarela_Versiones_PK'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Versión Principal.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Major'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Versión Secundaria.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Minor'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Versión Compilación.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Build'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Versión Revisión.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Revision'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Fecha de Creación de la Versión.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Fecha_Creacion'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Usuario de Creación de la Versión.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Usuario_Creacion'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Fecha Inicio Vigencia.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Fecha_Inicio_Vigencia'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Fecha Fin Vigencia.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Fecha_Fin_Vigencia'
GO

EXEC sys.sp_addextendedproperty
	@name=N'MS_Description', @value=N'[NO-MASK][NO-DCP] Observaciones de la Versión.',
	@level0type=N'SCHEMA', @level0name=N'dbo',
	@level1type=N'TABLE', @level1name=N'TBN8Pasarela_Versiones',
	@level2type=N'COLUMN', @level2name=N'Observaciones'
GO