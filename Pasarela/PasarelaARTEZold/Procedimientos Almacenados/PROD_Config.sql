USE DBN8GESTORPROCP3
GO 

SET QUOTED_IDENTIFIER ON
-- 70041649W
-- REvisar recuoerar borradre
-- no mostrar los de que estan daros de baja
-- Revisar la parametrización para un procedimiento JX de los botones y formularios

  DECLARE @PROC_JX INT
  ,@Idnt_Dominio INT
  ,@Idnt_DominioP3 INT
  ,@Idnt_TipoExpediente INT 
  ,@Idnt_ProcedimientoBLT INT 
  ,@VersionProcedimientoBLT_Sistema_Inicio DATETIME2(7) 
  ,@VersionProcedimientoBLT_Vigencia_Inicio DATETIME2(7)
  ,@IdntElementoAnidacionExp INT 
  ,@NumExpediente VARCHAR(100) 
  ,@Descripcion_ProcedimientoBLT VARCHAR(100)  = 'deporte'
  ,@pBorrador BIT =0

  
DECLARE @vIdnt_Dominio_GestorProcComun INT; 
DECLARE @vIdnt_DominioComun INT;
SELECT @vIdnt_DominioComun = Idnt_Dominio, @vIdnt_Dominio_GestorProcComun = Idnt_Dominio_GestorProc FROM DBN8GESTORPROCP3.dbo.Dominio WHERE Descriptor = 'Comun';


  IF @NumExpediente IS NOT NULL 
  BEGIN

	  DECLARE @IdntElementoAnidacion TABLE (
		IdntElementoAnidacion INT,
		Tipo VARCHAR(10)
	  )

    SELECT  'N8 Expediente', [Idnt_ElementoAnidacion_ExpAdm]
        ,[Idnt_TipoExpediente]
        ,[Numero_Expediente]
        ,[Es_Agrupacion_Previa]
        ,[Fecha_Ultima_Modificacion]
        ,[Codigo_Usuario_Modificacion]
        ,[Fecha_Alta_Expediente]
        ,[Es_Expediente_Acumulacion]
        ,[Codigo_Centro_Foral]
        ,[Procedimiento_JX]
        ,[Codigo_Expediente_SSAA]
        FROM [DBN8GESTORPROC].[dbo].[TBN8ExpedienteAdministrativo] with (nolock)
        WHERE CAST([Numero_Expediente] as varchar(50))=@NumExpediente or Codigo_Expediente_SSAA=@NumExpediente
        
    SELECT  @PROC_JX=[Procedimiento_JX]
        FROM [DBN8GESTORPROC].[dbo].[TBN8ExpedienteAdministrativo] with (nolock)
        WHERE CAST([Numero_Expediente] as varchar(50))=@NumExpediente or Codigo_Expediente_SSAA=@NumExpediente

    SELECT  'N8 Expediente', [Idnt_ElementoAnidacion_ExpAdm]
        ,[Idnt_TipoExpediente]
        ,[Numero_Expediente]
        ,[Es_Agrupacion_Previa]
        ,[Fecha_Ultima_Modificacion]
        ,[Codigo_Usuario_Modificacion]
        ,[Fecha_Alta_Expediente]
        ,[Es_Expediente_Acumulacion]
        ,[Codigo_Centro_Foral]
        ,[Procedimiento_JX]
        ,[Codigo_Expediente_SSAA]
        FROM [DBN8GESTORPROC].[dbo].[TBN8ExpedienteAdministrativo] with (nolock)
        WHERE CAST([Numero_Expediente] as varchar(50))=@NumExpediente or Codigo_Expediente_SSAA=@NumExpediente
        
    SELECT  @IdntElementoAnidacionExp=[Idnt_ElementoAnidacion_ExpAdm]
            ,@Idnt_TipoExpediente=[Idnt_TipoExpediente]
        FROM [DBN8GESTORPROC].[dbo].[TBN8ExpedienteAdministrativo] with (nolock)
        WHERE CAST([Numero_Expediente] as varchar(50))=@NumExpediente or Codigo_Expediente_SSAA=@NumExpediente


    INSERT INTO @IdntElementoAnidacion 
    SELECT @IdntElementoAnidacionExp,'EXP'

    INSERT INTO @IdntElementoAnidacion 
    SELECT a.Idnt_ElementoAnidacion_AgrupaActiv_Rel, 'SUB'
      FROM [DBN8GESTORPROC].[dbo].[TBN8RelacionAgrupacionesActividades] a with (nolock)
      where @IdntElementoAnidacionExp=a.[Idnt_ElementoAnidacion_AgrupaActiv_Ppal]


	select 'N8 Procedmiento',
        * from DBN8GESTORPROC.[dbo].[TBN8EjecucionProcedimientoAdministrativo] with (nolock)
        INNER JOIN @IdntElementoAnidacion  b 
        ON TBN8EjecucionProcedimientoAdministrativo.Idnt_ElementoAnidacion_EjecProcAdm= b.IdntElementoAnidacion
        where b.Tipo= 'SUB'

    select @Idnt_ProcedimientoBLT = Idnt_ProcedimientoBLT
        from DBN8GESTORPROC.[dbo].[TBN8EjecucionProcedimientoAdministrativo] with (nolock)
        INNER JOIN @IdntElementoAnidacion  b 
        ON TBN8EjecucionProcedimientoAdministrativo.Idnt_ElementoAnidacion_EjecProcAdm= b.IdntElementoAnidacion
        where b.Tipo= 'SUB'
  END 

  IF @Idnt_ProcedimientoBLT IS NULL AND @Descripcion_ProcedimientoBLT IS NOT NULL
	BEGIN
		select @Idnt_ProcedimientoBLT= [VersionProcedimientoBLTDenominacion].Idnt_ProcedimientoBLT from [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLTDenominacion] with (nolock)
		INNER JOIN [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
		 on  [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT]					  = [VersionProcedimientoBLTDenominacion].[Idnt_ProcedimientoBLT]					 
		 and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]  =	[VersionProcedimientoBLTDenominacion].[VersionProcedimientoBLT_Sistema_Inicio] 
		 and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] = [VersionProcedimientoBLTDenominacion].[VersionProcedimientoBLT_Vigencia_Inicio]
		where Descripcion_ProcedimientoBLT  like (CONCAT('%',@Descripcion_ProcedimientoBLT,'%')) and (
		 (sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio] and [VersionProcedimientoBLT_Sistema_FIN]
		and sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] and [VersionProcedimientoBLT_Vigencia_Fin]
		and @pBorrador =0
		)
		or ( [VersionProcedimientoBLT].VersionProcedimientoBLT_Vigencia_Inicio = '0001-01-01 00:00:00.0000000'))
		order by [VersionProcedimientoBLT].VersionProcedimientoBLT_Vigencia_Inicio desc

		select * from [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLTDenominacion]  with (nolock)
					INNER JOIN [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
						on  [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT]					  = [VersionProcedimientoBLTDenominacion].[Idnt_ProcedimientoBLT]					 
						and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]  =	[VersionProcedimientoBLTDenominacion].[VersionProcedimientoBLT_Sistema_Inicio] 
						and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] = [VersionProcedimientoBLTDenominacion].[VersionProcedimientoBLT_Vigencia_Inicio]
					where Descripcion_ProcedimientoBLT  like (CONCAT('%',@Descripcion_ProcedimientoBLT,'%')) and Codigo_Idioma='ES' and (
					 (sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio] and [VersionProcedimientoBLT_Sistema_FIN]
					and sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] and [VersionProcedimientoBLT_Vigencia_Fin]
					and @pBorrador =0
					)
					or ([VersionProcedimientoBLT].VersionProcedimientoBLT_Vigencia_Inicio = '0001-01-01 00:00:00.0000000') and @pBorrador =1)
		order by [VersionProcedimientoBLT].VersionProcedimientoBLT_Vigencia_Inicio desc
	END

  IF @PROC_JX IS NULL 
  BEGIN

    SELECT @Idnt_TipoExpediente = Idnt_TipoExpediente
      FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteProcedimientoBLT] with (nolock)
      where Idnt_ProcedimientoBLT = @Idnt_ProcedimientoBLT

 
    SELECT  @PROC_JX = JXDGNXXX
      FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteProcedimientoBLT] with (nolock)
      where @Idnt_TipoExpediente = Idnt_TipoExpediente and @Idnt_ProcedimientoBLT= Idnt_ProcedimientoBLT

  END
ELSE
BEGIN
  SELECT @Idnt_TipoExpediente = Idnt_TipoExpediente
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteProcedimientoBLT] with (nolock)
    where JXDGNXXX = @PROC_JX
END
IF @Idnt_TipoExpediente IS NOT NULL 
	BEGIN
	  SELECT @Idnt_Dominio = Idnt_Dominio
		FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpediente] with (nolock)
		where Idnt_TipoExpediente = @Idnt_TipoExpediente
	END
ELSE
	BEGIN
		SELECT @Idnt_Dominio = Idnt_Dominio_GestorProc
      FROM [DBN8GESTORPROCP3].[dbo].[ProcesoProcedimientoBLT]
      inner join [DBN8GESTORPROCP3].[dbo].[Dominio]
      on [Dominio].Idnt_Dominio = [ProcesoProcedimientoBLT].Idnt_Dominio
      where [Idnt_ProcedimientoBLT]=@Idnt_ProcedimientoBLT
	END

	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT @Idnt_DominioP3 = [Idnt_Dominio]
  FROM [DBN8GESTORPROCP3].[dbo].[Dominio]
  where [Idnt_Dominio_GestorProc] = @Idnt_Dominio
	

  IF @Idnt_ProcedimientoBLT IS NOT NULL AND @VersionProcedimientoBLT_Sistema_Inicio IS NULL AND @VersionProcedimientoBLT_Sistema_Inicio IS NULL 
  BEGIN
    SELECT TOP(1) @VersionProcedimientoBLT_Sistema_Inicio = [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]
    ,@VersionProcedimientoBLT_Vigencia_Inicio = [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio]
    FROM [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
  where ([VersionProcedimientoBLT].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio] and [VersionProcedimientoBLT_Sistema_FIN]
    and sysdatetime() between [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] and [VersionProcedimientoBLT_Vigencia_Fin]
    and @pBorrador =0
	)
	or ([VersionProcedimientoBLT].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT and VersionProcedimientoBLT_Vigencia_Inicio = '0001-01-01 00:00:00.0000000'  and @pBorrador =1)
	order by VersionProcedimientoBLT_Vigencia_Inicio desc

	SELECT @Idnt_ProcedimientoBLT as 'Idnt_ProcedimientoBLT' ,@VersionProcedimientoBLT_Sistema_Inicio as 'VersionProcedimientoBLT_Sistema_Inicio' ,@VersionProcedimientoBLT_Vigencia_Inicio as 'VersionProcedimientoBLT_Vigencia_Inicio'
		,@Idnt_TipoExpediente  as 'Idnt_TipoExpediente', @PROC_JX as 'PROC_JX',@Idnt_Dominio as 'Idnt_Dominio'
		,Descripcion_ProcedimientoBLT as 'Descripcion_ProcedimientoBLT', [Descripcion_VersionProcedimientoBLT] as 'Descripcion_VersionProcedimientoBLT'
		 from [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLTDenominacion] with (nolock)
		 where Idnt_ProcedimientoBLT= @Idnt_ProcedimientoBLT
		 and VersionProcedimientoBLT_Sistema_Inicio= @VersionProcedimientoBLT_Sistema_Inicio
		 and VersionProcedimientoBLT_Vigencia_Inicio= @VersionProcedimientoBLT_Vigencia_Inicio
		 and [Codigo_Idioma] = 'ES'
  END

  select 'VersionProcedimientoBLT' as 'VersionProcedimientoBLT', * from [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
	where Idnt_ProcedimientoBLT= @Idnt_ProcedimientoBLT
	and VersionProcedimientoBLT_Sistema_Inicio= @VersionProcedimientoBLT_Sistema_Inicio
	and VersionProcedimientoBLT_Vigencia_Inicio= @VersionProcedimientoBLT_Vigencia_Inicio
  -- Vista con formularios asociados a un procedimiento JX
  BEGIN TRY
    select 'ProcedimientoJX' as 'ProcedimientoJX', * from DBN8GESTORPROC.[dbo].[SVJXFOPR] with (nolock) WHERE PRONUM=@PROC_JX
  END TRY
  BEGIN CATCH	   
    select 'ProcedimientoJX' as 'ProcedimientoJX', 'No tienes permisos para realizar la consulta de SVJXFOPR'
  END CATCH


  SELECT TOP (1000) 'FormularioJXTipoActuacion' as 'FormularioJXTipoActuacion'
	,JXDGNXXX	
	,JXFRCLA1	
	,[TBN8TipoActuacion].Idnt_TipoActuacion
	,Codigo_Tipo_Actuador
	,Descripcion_Corta_ES
	,Codigo_Etiqueta
	,Codigo_Publicacion_Sede
	,EWCFTR_IDFTR
	,Es_Preclusiva	
	,Fecha_Fin_Vigencia
	,[TBN8TipoActuacion].Idnt_Dominio	
	,[TBN8Denominacion].Idnt_Dominio

    FROM [DBN8GESTORPROC].[dbo].[TBN8FormularioJXTipoActuacion] with (nolock)
    left join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    on [TBN8TipoActuacion].Idnt_TipoActuacion = [TBN8FormularioJXTipoActuacion].Idnt_TipoActuacion
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    where JXDGNXXX = @PROC_JX

  SELECT 'Dominio' as 'Dominio'
    ,Dominio.[Idnt_Dominio]         
      ,Dominio.[Dominio_Sistema_Inicio] 
      ,Dominio.[Dominio_Sistema_Fin]    
      ,Dominio.[Idnt_Dominio_GestorProc]
      ,Dominio.[Descriptor]             
      ,(SELECT TOP(1) Descripcion from [DBN8GESTORPROCP3].[dbo].[DominioDescripcion] as DominioDescripcion with (nolock)
      where DominioDescripcion.Idnt_Dominio = Dominio.Idnt_Dominio
      and DominioDescripcion.[Dominio_Sistema_Inicio] = Dominio_Sistema_Inicio
      and DominioDescripcion.Codigo_Idioma = 'ES'
      ) as  [Dominio/@DominioDescripcionES]
      ,(SELECT TOP(1) Descripcion from [DBN8GESTORPROCP3].[dbo].[DominioDescripcion] as DominioDescripcion with (nolock)
      where DominioDescripcion.Idnt_Dominio = Dominio.Idnt_Dominio
      and DominioDescripcion.[Dominio_Sistema_Inicio] = Dominio_Sistema_Inicio
      and DominioDescripcion.Codigo_Idioma = 'EU'
      ) as  [Dominio/@DominioDescripcionEU]
      FROM [DBN8GESTORPROCP3].[dbo].[Dominio]
      where [Idnt_Dominio_GestorProc] = @Idnt_Dominio
      and SYSDATETIME() BETWEEN Dominio_Sistema_Inicio and Dominio_Sistema_Fin

  SELECT TOP (1000) 'TipoActuacion Dominio' as 'TipoActuacion Dominio'
  ,[TBN8TipoActuacion].Idnt_TipoActuacion
    ,[Descripcion_Larga_ES]
    ,Codigo_Tipo_Actuador
    ,[TBN8TipoActuacion].Idnt_Dominio
    ,Codigo_Etiqueta
    ,EWCFTR_IDFTR
    ,[Codigo_Publicacion_Sede]
    --,[Es_Relacionada]
	, *
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    
    where [TBN8TipoActuacion].Idnt_Dominio in(@Idnt_Dominio,@vIdnt_Dominio_GestorProcComun)

SELECT TOP (1000) 'TipoActuacion Dominio Atributos Caracterizacion' as 'TipoActuacion Dominio Atributos Caracterizacion'
    ,[TBN8TipoActuacion].Idnt_TipoActuacion
    ,[Descripcion_Larga_ES]
    ,Codigo_Tipo_Actuador
    ,[TBN8TipoActuacion].Idnt_Dominio
    ,Codigo_Etiqueta
    ,EWCFTR_IDFTR
    ,[Codigo_Publicacion_Sede]
    --,[Es_Relacionada]
    ,[TBN8TipoActuacion].[Idnt_TipoActuacion]
    ,Codigo_Etiqueta_Caracterizacion
    ,Tipo_Dato
    ,[TBN8Caracterizacion].Idnt_Dominio
    ,Orden
    ,Patron
    ,Es_Visible_En_Carpeta_Ciudadana
    ,*
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    inner join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacionAtributoCaracterizacion]
    on [TBN8TipoActuacion].[Idnt_TipoActuacion]= [TBN8TipoActuacionAtributoCaracterizacion].[Idnt_TipoActuacion]
    left join [DBN8GESTORPROC].[dbo].[TBN8Caracterizacion]
    on [TBN8Caracterizacion].Idnt_Caracterizacion = [TBN8TipoActuacionAtributoCaracterizacion].Idnt_Caracterizacion
    where [TBN8TipoActuacion].Idnt_Dominio = @Idnt_Dominio

  SELECT TOP(1000) 'TipoActuacion Relacion' as 'TipoActuacion Relacion'
    ,TipoActuacionPpal.Idnt_TipoActuacion
    ,DenominacionPpal.[Descripcion_Larga_ES]
    ,TipoActuacionPpal.Codigo_Tipo_Actuador
    ,TipoActuacionPpal.Idnt_Dominio
    ,TipoActuacionPpal.Codigo_Etiqueta
    ,TipoActuacionPpal.EWCFTR_IDFTR
    ,TipoActuacionPpal.[Codigo_Publicacion_Sede]
   -- ,TipoActuacionPpal.[Es_Relacionada]
    ,TipoActuacionRel.Idnt_TipoActuacion
    ,DenominacionRel.[Descripcion_Larga_ES]
    ,TipoActuacionRel.Codigo_Tipo_Actuador
    ,TipoActuacionRel.Idnt_Dominio
    ,TipoActuacionRel.Codigo_Etiqueta
    ,TipoActuacionRel.EWCFTR_IDFTR
    ,TipoActuacionRel.[Codigo_Publicacion_Sede]
   -- ,TipoActuacionRel.[Es_Relacionada]
  , *
    FROM  [DBN8GESTORPROC].[dbo].[TBN8RelacionTiposActuacion]
    inner join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] TipoActuacionPpal with (nolock)
    on TipoActuacionPpal.Idnt_TipoActuacion = TBN8RelacionTiposActuacion.Idnt_TipoActuacion_Ppal
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] DenominacionPpal with (nolock)
    ON [TipoActuacionPpal].Idnt_TipoActuacion = DenominacionPpal.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    inner join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] TipoActuacionRel with (nolock)
    on TipoActuacionRel.Idnt_TipoActuacion = TBN8RelacionTiposActuacion.Idnt_TipoActuacion_Rel
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] DenominacionRel with (nolock)
    ON [TipoActuacionRel].Idnt_TipoActuacion = DenominacionRel.Idnt_Entidad_Denominada
    AND DenominacionPpal.Idnt_InventarioEntidades = 14
    where [TipoActuacionPpal].Idnt_Dominio = @Idnt_Dominio
 
  
 SELECT TOP (1000) 'TipoActuacion Fase Estado' as 'TipoActuacion Fase Estado'
	,[TBN8TipoActuacion].[EWCFTR_IDFTR]
	,[TBN8Denominacion].Descripcion_Larga_ES
	,[EstadoExterno].*
	,[Fase].*
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
	left join [DBN8GESTORPROCP3].[dbo].[EstadoExterno] with (nolock)
	on [EstadoExterno].[Idnt_Tramite_SEDE] = [TBN8TipoActuacion].[EWCFTR_IDFTR]
	left join [DBN8GESTORPROCP3].[dbo].[Fundamento] FundamentoEstado
	on [EstadoExterno].[Codigo_CategoriaFundamento] = FundamentoEstado.[Codigo_CategoriaFundamento]
	and [EstadoExterno].[Idnt_EstadoExterno] = FundamentoEstado.[Idnt_Fundamento]
	and [EstadoExterno].[EstadoExterno_Sistema_Inicio] = FundamentoEstado.[Fundamento_Sistema_Inicio] 
	left join [DBN8GESTORPROCP3].[dbo].[FaseEstadoExterno] with (nolock)
	on [EstadoExterno].[Codigo_CategoriaFundamento] = [FaseEstadoExterno].[Codigo_CategoriaFundamento_EstadoExterno]
	and [EstadoExterno].[Idnt_EstadoExterno] = [FaseEstadoExterno].[Idnt_EstadoExterno]
	and [EstadoExterno].[EstadoExterno_Sistema_Inicio] = [FaseEstadoExterno].[EstadoExterno_Sistema_Inicio]
	left join [DBN8GESTORPROCP3].[dbo].[Fase] with (nolock)
	on [Fase].[Codigo_CategoriaFundamento] = [FaseEstadoExterno].[Codigo_CategoriaFundamento_Fase]
	and [Fase].[Idnt_Fase] = [FaseEstadoExterno].[Idnt_Fase]
	and [Fase].[Fase_Sistema_Inicio] = [FaseEstadoExterno].[Fase_Sistema_Inicio]
	left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    where [TBN8TipoActuacion].Idnt_Dominio = @Idnt_Dominio and FundamentoEstado.Idnt_Dominio = @Idnt_DominioP3
    order by  [TBN8TipoActuacion].[EWCFTR_IDFTR]


 SELECT TOP (1000) 'TipoActuacionAtributoCaracterizacion' as 'TipoActuacionAtributoCaracterizacion'
	,[TBN8TipoActuacionAtributoCaracterizacion].[Orden]
	,[TBN8TipoActuacion].[Codigo_Etiqueta]
	,[TBN8Caracterizacion].[Codigo_Etiqueta_Caracterizacion]
	,[TBN8TipoActuacionAtributoCaracterizacion].[Codigo_Tipo_Relacion]
	,[TBN8TipoActuacionAtributoCaracterizacion].[Patron]
	,[TBN8TipoActuacionAtributoCaracterizacion].[Es_Visible_En_Carpeta_Ciudadana]
	,*
	FROM [DBN8GESTORPROC].[dbo].[TBN8TipoActuacionAtributoCaracterizacion] with (nolock)
    inner join  [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
	on [TBN8TipoActuacion].[Idnt_TipoActuacion]= [TBN8TipoActuacionAtributoCaracterizacion].[Idnt_TipoActuacion]
	inner join [DBN8GESTORPROC].[dbo].[TBN8Caracterizacion] with (nolock)
	on [TBN8Caracterizacion].[Idnt_Caracterizacion]= [TBN8TipoActuacionAtributoCaracterizacion].[Idnt_Caracterizacion]
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    where [TBN8TipoActuacion].Idnt_Dominio = @Idnt_Dominio
	order by [TBN8TipoActuacionAtributoCaracterizacion].Orden

  SELECT TOP (1000) 'TipoExpediente' as 'TipoExpediente', TBN8TipoExpediente.*
      ,Descripcion_Corta_ES	
      ,Descripcion_Larga_ES	
      ,Descripcion_Corta_EU	
      ,Descripcion_Larga_EU
      ,TBN8TipoExpediente.Idnt_Dominio as Dominio_Exp
      ,TBN8Denominacion.Idnt_Dominio as Dominio_Den
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpediente] with (nolock)
    INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON TBN8TipoExpediente.Idnt_TipoExpediente = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 22
    where Idnt_TipoExpediente = @Idnt_TipoExpediente

  SELECT TOP (1000) 'TipoExpedienteProcedimientoBLT' as 'TipoExpedienteProcedimientoBLT',*
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteProcedimientoBLT] with (nolock)
    where Idnt_TipoExpediente = @Idnt_TipoExpediente or Idnt_ProcedimientoBLT=@Idnt_ProcedimientoBLT

    SELECT TOP (1000) 'TipoExpedienteConcepto' as 'TipoExpedienteConcepto',*
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteConcepto] with (nolock)
    left join [DBN8GESTORPROC].[dbo].[TBN8ConceptoExpediente] with (nolock)
    on TBN8TipoExpedienteConcepto.Idnt_ConceptoExpediente = TBN8ConceptoExpediente.Idnt_ConceptoExpediente
    where Idnt_TipoExpediente = @Idnt_TipoExpediente

  SELECT TOP (1000) 'TipoExpedienteAgrupacionDocumental' as 'TipoExpedienteAgrupacionDocumental',*
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteAgrupacionDocumental] with (nolock)
    where Idnt_TipoExpediente = @Idnt_TipoExpediente


  SELECT TOP (1000) 'TipoExpedienteAtributoCaracterizacion' as 'TipoExpedienteAtributoCaracterizacion'
		,TBN8Caracterizacion.Idnt_Caracterizacion	
		,Codigo_Etiqueta_Caracterizacion
		,[TBN8Caracterizacion].Idnt_Dominio	
		,Tipo_Dato
		,Descripcion_Larga_ES
		,Es_Atributo_Caracterizacion_Primario	
		,Es_Atributo_Caracterizacion_Identificativo
		,TBN8Caracterizacion.Presencia_Filtro_Cabecera_Resultado
		,[Marca_Dato_Editable]
		,[Codigo_Propiedad]
		,[Valor_Propiedad]
		,Orden	
		,Es_Visible
		,Marca_Multivaluado
    FROM [DBN8GESTORPROC].[dbo].[TBN8TipoExpedienteAtributoCaracterizacion] with (nolock)
    INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8Caracterizacion] with (nolock)
    ON TBN8Caracterizacion.Idnt_Caracterizacion = TBN8TipoExpedienteAtributoCaracterizacion.Idnt_Caracterizacion
    INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoExpedienteAtributoCaracterizacion].Idnt_Caracterizacion = TBN8Denominacion.[Idnt_Entidad_Denominada]
    AND Idnt_InventarioEntidades = 2
    INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8ValorPropiedad] with (nolock)
    ON [TBN8TipoExpedienteAtributoCaracterizacion].Idnt_Caracterizacion = [TBN8ValorPropiedad].[Idnt_Caracterizacion]
    where Idnt_TipoExpediente = @Idnt_TipoExpediente
    order by Orden

  SELECT TOP (1000)
  'CaracterizacionProcedimiento' as  'CaracterizacionProcedimiento'
  ,[TBN8CaracterizacionProcedimiento].Idnt_CaracterizacionProcedimiento
  ,TBN8Caracterizacion.Idnt_Caracterizacion
  ,Codigo_Etiqueta_Caracterizacion
  ,[TBN8Caracterizacion].Idnt_Dominio
  ,Tipo_Dato
  ,Descripcion_Larga_ES
  ,[Marca_Dato_Editable]
  ,Es_Atributo_Caracterizacion_Identificativo
  ,TBN8Caracterizacion.Presencia_Filtro_Cabecera_Resultado
  ,[Codigo_Propiedad]
  ,[Valor_Propiedad]
  ,Orden
  ,Es_Visible
  ,Marca_Multivaluado
  FROM [DBN8GESTORPROC].[dbo].[TBN8CaracterizacionProcedimiento] with (nolock)
  INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8Caracterizacion] with (nolock)
  ON TBN8Caracterizacion.Idnt_Caracterizacion = [TBN8CaracterizacionProcedimiento].Idnt_Caracterizacion
  INNER JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
  ON [TBN8CaracterizacionProcedimiento].Idnt_Caracterizacion = TBN8Denominacion.[Idnt_Entidad_Denominada]
  AND Idnt_InventarioEntidades = 2
  LEFT JOIN [DBN8GESTORPROC].[dbo].[TBN8ValorPropiedad] with (nolock)
  ON [TBN8CaracterizacionProcedimiento].Idnt_Caracterizacion = [TBN8ValorPropiedad].[Idnt_Caracterizacion]
  INNER JOIN [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
  on  [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT]					  = [TBN8CaracterizacionProcedimiento].[Idnt_ProcedimientoBLT]					 
  and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]  =	[TBN8CaracterizacionProcedimiento].[VersionProcedimientoBLT_Sistema_Inicio] 
  and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] = [TBN8CaracterizacionProcedimiento].[VersionProcedimientoBLT_Vigencia_Inicio] 
  where [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
  and [VersionProcedimientoBLT].VersionProcedimientoBLT_Sistema_Inicio= @VersionProcedimientoBLT_Sistema_Inicio
  and [VersionProcedimientoBLT].VersionProcedimientoBLT_Vigencia_Inicio= @VersionProcedimientoBLT_Vigencia_Inicio
	order by Orden


  select 'Config Integracion'

  -- Configuración de grupo que revisará las entradas del procedimiento para el formulario
  SELECT 'ConfiguracionTratamientoEntradaIntegracion' as 'ConfiguracionTratamientoEntradaIntegracion', *
  FROM DBN8GESTORPROCP3.dbo.TBN8ConfiguracionTratamientoEntradaIntegracion with (nolock)
  where N3EDAC_PROCEDIMIENTO LIKE (CONCAT('JX',@PROC_JX,'%'))

  SELECT 'ServicioIntegracion' as 'ServicioIntegracion',* FROM [DBN8GESTORPROCP3_2].[dbo].[TBN8ServicioIntegracion] with (nolock)
  where Tipo_Servicio like CONCAT('JX',@PROC_JX,'%')


  -- Configuración del enlace para el procedimiento JX, procedimiento BLT y trámite
  SELECT'SedeFormularioIntegracion' as 'SedeFormularioIntegracion', * FROM [DBN8GESTORPROCP3_2].[dbo].[TBN8SedeFormularioIntegracion] with (nolock)
  WHERE Procedimiento_JX = @PROC_JX
  order by 1 desc




    -- Descripción del botón de segunda actuación que veremos en SEDE
  BEGIN TRY
    SELECT 'SedeFormularioDenominacionIntegracion' as 'SedeFormularioDenominacionIntegracion',* FROM [DBN8GESTORPROCP3].[dbo].[TBN8SedeFormularioDenominacionIntegracion] with (nolock)
    WHERE Formulario IN (select NUMFOR from DBN8GESTORPROC.[dbo].[SVJXFOPR] with (nolock) WHERE PRONUM=@PROC_JX)
  END TRY
  BEGIN CATCH	   
    SELECT 'TBN8SedeFormularioDenominacionIntegracion' as 'TBN8SedeFormularioDenominacionIntegracion'
		,[TBN8SedeFormularioDenominacionIntegracion].*
		,[TBN8SedeFormularioIntegracion].*
		FROM [DBN8GESTORPROCP3_2].[dbo].[TBN8SedeFormularioIntegracion] with (nolock)
		left join [DBN8GESTORPROCP3].[dbo].[TBN8SedeFormularioDenominacionIntegracion] with (nolock)
		on [TBN8SedeFormularioIntegracion].Formulario= [TBN8SedeFormularioDenominacionIntegracion].Formulario
		WHERE 
		 [TBN8SedeFormularioIntegracion].Formulario <> 0
		and Procedimiento_JX = @PROC_JX
  END CATCH


  SELECT 'N8 Config Procedimiento'

  SELECT 'N8 Tramites' as 'N8 Tramites'
        ,[VersionProcedimientoBLT].[Idnt_ProcedimientoBLT]
        ,[VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]
        ,[VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio]
        ,[ActividadBPMNTramite].[Idnt_Tramite]
    ,[ActividadBPMNTramite].[Tramite_Sistema_Inicio]
    ,[ElementoModeladoBPMNDescripcion].Descripcion
    ,[ActividadBPMNTramite].[Idnt_ElementoModeladoBPMN]
    ,[Formulario].[Idnt_Formulario]
    ,[Formulario].[Formulario_Sistema_Inicio]
    ,[Formulario].[Codigo_Aplicacion]
    ,[Formulario].[Codigo_Vista]
	,[Formulario].*
    ,Configuracion_Tramite
    ,( CASE  
         WHEN [TBN8VersionProcedimientoBLTSugerenciaInicio].[Idnt_ElementoModeladoBPMN] IS NOT NULL THEN 'SI' 
         ELSE '' 
       END  
    )  AS 'Tramite Inicio'
    FROM [DBN8GESTORPROCP3].[dbo].[VersionProcedimientoBLT] with (nolock)
    left join [DBN8GESTORPROCP3].[dbo].[ActividadBPMNTramite] with (nolock)
    on  [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT]					  = [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					 
    and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio]  =	[ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio] 
    and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] = [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio]
    left join [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTSugerenciaInicio] with (nolock)
  	ON [TBN8VersionProcedimientoBLTSugerenciaInicio].[Idnt_ProcedimientoBLT]				  = ActividadBPMNTramite.[Idnt_ProcedimientoBLT]
    and [TBN8VersionProcedimientoBLTSugerenciaInicio].[VersionProcedimientoBLT_Sistema_Inicio]  = ActividadBPMNTramite.[VersionProcedimientoBLT_Sistema_Inicio]
    and [TBN8VersionProcedimientoBLTSugerenciaInicio].[VersionProcedimientoBLT_Vigencia_Inicio] = ActividadBPMNTramite.[VersionProcedimientoBLT_Vigencia_Inicio]
    and [TBN8VersionProcedimientoBLTSugerenciaInicio].[Idnt_ElementoModeladoBPMN]				  = [ActividadBPMNTramite].[Idnt_ElementoModeladoBPMN]
    left join [DBN8GESTORPROCP3].[dbo].[ElementoModeladoBPMNDescripcion] with (nolock)
    on  [ElementoModeladoBPMNDescripcion].[Idnt_ProcedimientoBLT]					  = [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Sistema_Inicio]  =	[ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Vigencia_Inicio] = [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ElementoModeladoBPMNDescripcion].[Idnt_ElementoModeladoBPMN]				  = [ActividadBPMNTramite].[Idnt_ElementoModeladoBPMN]
    and [ElementoModeladoBPMNDescripcion].[Codigo_Idioma] = 'ES'
    left join [DBN8GESTORPROCP3].[dbo].[ElementoModeladoBPMNDescripcion] ElementoModeladoBPMNDescripcionEU with (nolock)
    on  ElementoModeladoBPMNDescripcionEU.[Idnt_ProcedimientoBLT]					  = [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					 
    and ElementoModeladoBPMNDescripcionEU.[VersionProcedimientoBLT_Sistema_Inicio]  =	[ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio] 
    and ElementoModeladoBPMNDescripcionEU.[VersionProcedimientoBLT_Vigencia_Inicio] = [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio]
    and ElementoModeladoBPMNDescripcionEU.[Idnt_ElementoModeladoBPMN]				  = [ActividadBPMNTramite].[Idnt_ElementoModeladoBPMN]
    and ElementoModeladoBPMNDescripcionEU.[Codigo_Idioma] = 'EU'
    left join [DBN8GESTORPROCP3].[dbo].[FormularioTramite] with (nolock)
    on  [ActividadBPMNTramite].[Idnt_Tramite]					      = [FormularioTramite].[Idnt_Tramite]					     
    and [ActividadBPMNTramite].[Codigo_CategoriaFundamento_Tramite] =	[FormularioTramite].[Codigo_CategoriaFundamento_Tramite]
    and [ActividadBPMNTramite].[Tramite_Sistema_Inicio]			  = [FormularioTramite].[Tramite_Sistema_Inicio]	
    left join [DBN8GESTORPROCP3].[dbo].[Formulario] with (nolock)
    on  [Formulario].[Idnt_Formulario]					      = [FormularioTramite].[Idnt_Formulario]					     
    and [Formulario].[Codigo_CategoriaFundamento] =	[FormularioTramite].[Codigo_CategoriaFundamento_Formulario]
    and [Formulario].[Formulario_Sistema_Inicio]			  = [FormularioTramite].[Formulario_Sistema_Inicio]	
    left join [DBN8GESTORPROCP3].[dbo].[FundamentoDescripcion] with (nolock)
    on  [ActividadBPMNTramite].[Idnt_Tramite]					  = [FundamentoDescripcion].[Idnt_Fundamento]					 
    and [ActividadBPMNTramite].[Codigo_CategoriaFundamento_Tramite]  =	[FundamentoDescripcion].[Codigo_CategoriaFundamento] 
    and [ActividadBPMNTramite].[Tramite_Sistema_Inicio] = [FundamentoDescripcion].[Fundamento_Sistema_Inicio]
    and [FundamentoDescripcion].[Codigo_Idioma] = 'ES'
    where [VersionProcedimientoBLT].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [VersionProcedimientoBLT].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
    order by Descripcion asc

    select 'N8 Conexion' as 'N8 Conexion'
    
	,[ConexionElementosModeladoBPMN].Idnt_ProcedimientoBLT	
    ,[ConexionElementosModeladoBPMN].VersionProcedimientoBLT_Sistema_Inicio	
    ,[ConexionElementosModeladoBPMN].VersionProcedimientoBLT_Vigencia_Inicio	
    ,Idnt_ElementoModeladoBPMN_Origen	
    ,[ActividadBPMNTramite].Idnt_Tramite
    ,[ElementoModeladoBPMNDescripcion].Descripcion
	,CASE WHEN TBN8ActividadBPMNTramiteLibreAsignacion.Codigo_SistFunc_Grupo_Seguridad IS NOT NULL THEN CONCAT(TBN8ActividadBPMNTramiteLibreAsignacion.Codigo_SistFunc_Grupo_Seguridad,'@@',TBN8ActividadBPMNTramiteLibreAsignacion.Descriptor_Grupo_Seguridad) ELSE '' END as 'Grupo Seguridad'
	,TBN8ActividadBPMNTramiteLibreDecision.Codigo_Decision
	,Idnt_ElementoModeladoBPMN_Destino
    ,ActividadBPMNTramiteDest.Idnt_Tramite
    ,ElementoModeladoBPMNDescripcionDest.Descripcion
	,TBN8ActividadBPMNTramiteLibreDecision.[Marca_Observaciones]
	,TBN8ActividadBPMNTramiteLibreDecision.[Codigo_Ejecucion]
	,TBN8ActividadBPMNTramiteLibreDecision.[Codigo_Decision_GAD]
	,TBN8ActividadBPMNTramiteLibreDecision.[Marca_Visible]
	,TBN8ActividadBPMNTramiteLibreDecision.[Orden]
	
    from [DBN8GESTORPROCP3].[dbo].[ConexionElementosModeladoBPMN] with (nolock)
	
    left join [DBN8GESTORPROCP3].[dbo].[ActividadBPMNTramite] with (nolock)
    on  [ConexionElementosModeladoBPMN].[Idnt_ProcedimientoBLT]					= [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					 
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Sistema_Inicio]  = [ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Vigencia_Inicio] = [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ConexionElementosModeladoBPMN].Idnt_ElementoModeladoBPMN_Origen			= [ActividadBPMNTramite].Idnt_ElementoModeladoBPMN
	left join [DBN8GESTORPROCP3].[dbo].[ActividadBPMNTramite] ActividadBPMNTramiteDest with (nolock)
    on  [ConexionElementosModeladoBPMN].[Idnt_ProcedimientoBLT]					= ActividadBPMNTramiteDest.[Idnt_ProcedimientoBLT]					 
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Sistema_Inicio]  = ActividadBPMNTramiteDest.[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Vigencia_Inicio] = ActividadBPMNTramiteDest.[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ConexionElementosModeladoBPMN].Idnt_ElementoModeladoBPMN_Destino			= ActividadBPMNTramiteDest.Idnt_ElementoModeladoBPMN
	left join [DBN8GESTORPROCP3].[dbo].TBN8ActividadBPMNTramiteLibreAsignacion
	on  TBN8ActividadBPMNTramiteLibreAsignacion.[Idnt_ProcedimientoBLT]					  = [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					 
	and TBN8ActividadBPMNTramiteLibreAsignacion.[VersionProcedimientoBLT_Sistema_Inicio]  =	[ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio] 
	and TBN8ActividadBPMNTramiteLibreAsignacion.[VersionProcedimientoBLT_Vigencia_Inicio] = [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio]
	and TBN8ActividadBPMNTramiteLibreAsignacion.[Idnt_ElementoModeladoBPMN]				  = [ActividadBPMNTramite].[Idnt_ElementoModeladoBPMN]
	left join [DBN8GESTORPROCP3].[dbo].TBN8ActividadBPMNTramiteLibreDecision TBN8ActividadBPMNTramiteLibreDecision with (nolock)
    on  [ActividadBPMNTramite].[Idnt_ProcedimientoBLT]					= TBN8ActividadBPMNTramiteLibreDecision.[Idnt_ProcedimientoBLT]					 
    and [ActividadBPMNTramite].[VersionProcedimientoBLT_Sistema_Inicio]  = TBN8ActividadBPMNTramiteLibreDecision.[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ActividadBPMNTramite].[VersionProcedimientoBLT_Vigencia_Inicio] = TBN8ActividadBPMNTramiteLibreDecision.[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ActividadBPMNTramite].Idnt_ElementoModeladoBPMN		= TBN8ActividadBPMNTramiteLibreDecision.Idnt_ElementoModeladoBPMN
	and ActividadBPMNTramiteDest.Idnt_ElementoModeladoBPMN			= TBN8ActividadBPMNTramiteLibreDecision.[Idnt_ElementoModeladoBPMN_Siguiente]
    
    left join [DBN8GESTORPROCP3].[dbo].[ElementoModeladoBPMNDescripcion] with (nolock)
    on  [ElementoModeladoBPMNDescripcion].[Idnt_ProcedimientoBLT]					  = [ConexionElementosModeladoBPMN].[Idnt_ProcedimientoBLT]					 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Sistema_Inicio]  =	[ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Vigencia_Inicio] = [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ElementoModeladoBPMNDescripcion].[Idnt_ElementoModeladoBPMN]				  = [ConexionElementosModeladoBPMN].[Idnt_ElementoModeladoBPMN_Origen]
    and [ElementoModeladoBPMNDescripcion].[Codigo_Idioma] = 'ES'
    left join [DBN8GESTORPROCP3].[dbo].[ElementoModeladoBPMNDescripcion]  ElementoModeladoBPMNDescripcionDest with (nolock)
    on  ElementoModeladoBPMNDescripcionDest.[Idnt_ProcedimientoBLT]					  = [ConexionElementosModeladoBPMN].[Idnt_ProcedimientoBLT]					 
    and ElementoModeladoBPMNDescripcionDest.[VersionProcedimientoBLT_Sistema_Inicio]  =	[ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Sistema_Inicio] 
    and ElementoModeladoBPMNDescripcionDest.[VersionProcedimientoBLT_Vigencia_Inicio] =	[ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Vigencia_Inicio]
    and ElementoModeladoBPMNDescripcionDest.[Idnt_ElementoModeladoBPMN]				  = [ConexionElementosModeladoBPMN].[Idnt_ElementoModeladoBPMN_Destino]
    and ElementoModeladoBPMNDescripcionDest.[Codigo_Idioma] = 'ES'
    where [ConexionElementosModeladoBPMN].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [ConexionElementosModeladoBPMN].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
   
	order by [ElementoModeladoBPMNDescripcion].Descripcion asc, TBN8ActividadBPMNTramiteLibreDecision.[Orden] asc
	
	BEGIN TRY  
		SELECT 'N8 Documentos' as 'N8 Documentos'
		,*
		from [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTDocumento] with (nolock)
		inner join dbo.SVN2TipoDocumento with (nolock)
		on [TBN8VersionProcedimientoBLTDocumento].Idnt_TipoDocumento_N2= SVN2TipoDocumento.Idnt_TipoDocumento
		inner join [DBN8GESTORPROCP3].[dbo].[Documento] with (nolock)
		on [TBN8VersionProcedimientoBLTDocumento].Idnt_Documento = [Documento].Idnt_Documento
		where [TBN8VersionProcedimientoBLTDocumento].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
		and [TBN8VersionProcedimientoBLTDocumento].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
		and [TBN8VersionProcedimientoBLTDocumento].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
	END TRY  
	BEGIN CATCH  
		SELECT 'N8 Documentos' as 'N8 Documentos'
		,*
		from [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTDocumento] with (nolock)
		inner join [DBN8GESTORPROCP3].[dbo].[Documento] with (nolock)
		on [TBN8VersionProcedimientoBLTDocumento].Idnt_Documento = [Documento].Idnt_Documento
		where [TBN8VersionProcedimientoBLTDocumento].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
		and [TBN8VersionProcedimientoBLTDocumento].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
		and [TBN8VersionProcedimientoBLTDocumento].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
	END CATCH  

    SELECT 'N8 GrupoUsuario' as 'N8 GrupoUsuario'
    ,*
    from [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTGrupoUsuario] with (nolock)
    where [TBN8VersionProcedimientoBLTGrupoUsuario].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [TBN8VersionProcedimientoBLTGrupoUsuario].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [TBN8VersionProcedimientoBLTGrupoUsuario].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio

    SELECT 'N8 TipoActuacion' as 'N8 TipoActuacion'
    ,*
    from [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTTipoActuacion] with (nolock)
    inner join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    on TBN8VersionProcedimientoBLTTipoActuacion.[Idnt_TipoActuacion] = TBN8TipoActuacion.[Idnt_TipoActuacion]
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    where [TBN8VersionProcedimientoBLTTipoActuacion].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [TBN8VersionProcedimientoBLTTipoActuacion].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [TBN8VersionProcedimientoBLTTipoActuacion].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio

    SELECT 'N8 CircuitoFirma' as 'N8 CircuitoFirma'
    ,*
    from [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTCircuitoFirma] with (nolock)
    where [TBN8VersionProcedimientoBLTCircuitoFirma].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [TBN8VersionProcedimientoBLTCircuitoFirma].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [TBN8VersionProcedimientoBLTCircuitoFirma].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio



    SELECT 'N8 Acciones Flujo' as 'N8 Acciones Flujo'
    ,[TBN8VersionProcedimientoBLTAccion].Idnt_ProcedimientoBLT	
    ,[TBN8VersionProcedimientoBLTAccion].VersionProcedimientoBLT_Sistema_Inicio	
    ,[TBN8VersionProcedimientoBLTAccion].VersionProcedimientoBLT_Vigencia_Inicio	
    ,[TBN8VersionProcedimientoBLTAccion].Idnt_Accion	
    ,[TBN8VersionProcedimientoBLTAccion].Tipo_Evento
    ,[TBN8Accion].Tipo_Accion
	,[TBN8AccionActuacion].Idnt_TipoActuacion
    ,[TBN8TipoActuacion].Codigo_Tipo_Actuador
    ,[TBN8TipoActuacion].Idnt_Dominio	
    ,[TBN8TipoActuacion].Es_Preclusiva	
    ,[TBN8TipoActuacion].Codigo_Etiqueta	
    ,[TBN8TipoActuacion].EWCFTR_IDFTR	
    ,[TBN8TipoActuacion].Codigo_Publicacion_Sede
    ,[TBN8AccionFaseEstadoExterno].Idnt_Fase	
    ,[TBN8AccionFaseEstadoExterno].Fase_Sistema_Inicio	
    ,[TBN8AccionFaseEstadoExterno].Codigo_CategoriaFundamento_EstadoExterno	
    ,[TBN8AccionFaseEstadoExterno].Idnt_EstadoExterno	
    ,[TBN8AccionFaseEstadoExterno].EstadoExterno_Sistema_Inicio	
    ,[TBN8AccionFaseEstadoExterno].Fecha_Inicio_Sistema
    ,[TBN8ServicioTramitacion].[Tipo_Servicio]
    ,[TBN8ServicioTramitacion].[Descripcion_Corta_Castellano]
    ,[TBN8ServicioTramitacion].[Nombre_JNDI]
    ,[TBN8ServicioTramitacion].[Codigo_Sistema_Funcional]
    ,[TBN8ServicioTramitacion].[Descriptor]
    from 
    [DBN8GESTORPROCP3].[dbo].[TBN8VersionProcedimientoBLTAccion] with (nolock)
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8Accion] with (nolock)
    on [TBN8VersionProcedimientoBLTAccion].[Idnt_Accion] = [TBN8Accion].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionActuacion] with (nolock)
    on [TBN8VersionProcedimientoBLTAccion].[Idnt_Accion] = [TBN8AccionActuacion].[Idnt_Accion]
    left join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    on [TBN8TipoActuacion].Idnt_TipoActuacion = [TBN8AccionActuacion].Idnt_TipoActuacion
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionFaseEstadoExterno] with (nolock)
    on [TBN8VersionProcedimientoBLTAccion].[Idnt_Accion] = [TBN8AccionFaseEstadoExterno].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionLLamadaServicio] with (nolock)
    on [TBN8VersionProcedimientoBLTAccion].[Idnt_Accion] = [TBN8AccionLLamadaServicio].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8ServicioTramitacion] with (nolock)
    on [TBN8AccionLLamadaServicio].[Idnt_ServicioTramitacion] = [TBN8ServicioTramitacion].[Idnt_ServicioTramitacion]
    where [TBN8VersionProcedimientoBLTAccion].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [TBN8VersionProcedimientoBLTAccion].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [TBN8VersionProcedimientoBLTAccion].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
	 
    SELECT 'N8 Acciones Tramite' as 'N8 Acciones Tramite'
    ,[TBN8ActividadBPMNTramiteAccion].*
    ,[ElementoModeladoBPMNDescripcion].Descripcion
	,[TBN8Accion].Tipo_Accion
    ,[TBN8Accion].Idnt_Accion
	,[TBN8ActividadBPMNTramiteAccion].Resultado
    ,[TBN8AccionActuacion].Idnt_TipoActuacion
    ,[TBN8TipoActuacion].Codigo_Tipo_Actuador
    ,[TBN8TipoActuacion].Idnt_Dominio	
    ,[TBN8TipoActuacion].Es_Preclusiva	
    ,[TBN8TipoActuacion].Codigo_Etiqueta	
    ,[TBN8TipoActuacion].EWCFTR_IDFTR	
    ,[TBN8TipoActuacion].Codigo_Publicacion_Sede
    ,[TBN8AccionFaseEstadoExterno].Idnt_Fase	
    ,[TBN8AccionFaseEstadoExterno].Fase_Sistema_Inicio	
    ,[TBN8AccionFaseEstadoExterno].Codigo_CategoriaFundamento_EstadoExterno	
    ,[TBN8AccionFaseEstadoExterno].Idnt_EstadoExterno	
    ,[TBN8AccionFaseEstadoExterno].EstadoExterno_Sistema_Inicio	
    ,[TBN8AccionFaseEstadoExterno].Fecha_Inicio_Sistema
    ,[TBN8ServicioTramitacion].[Tipo_Servicio]
    ,[TBN8ServicioTramitacion].[Descripcion_Corta_Castellano]
    ,[TBN8ServicioTramitacion].[Nombre_JNDI]
    ,[TBN8ServicioTramitacion].[Codigo_Sistema_Funcional]
    ,[TBN8ServicioTramitacion].[Descriptor]
    from 
    [DBN8GESTORPROCP3].[dbo].[TBN8ActividadBPMNTramiteAccion] with (nolock)
    inner join [DBN8GESTORPROCP3].[dbo].[ElementoModeladoBPMNDescripcion]  with (nolock)
    on  [ElementoModeladoBPMNDescripcion].[Idnt_ProcedimientoBLT]					  = [TBN8ActividadBPMNTramiteAccion].[Idnt_ProcedimientoBLT]					 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Sistema_Inicio]  =	[TBN8ActividadBPMNTramiteAccion].[VersionProcedimientoBLT_Sistema_Inicio] 
    and [ElementoModeladoBPMNDescripcion].[VersionProcedimientoBLT_Vigencia_Inicio] = [TBN8ActividadBPMNTramiteAccion].[VersionProcedimientoBLT_Vigencia_Inicio]
    and [ElementoModeladoBPMNDescripcion].[Idnt_ElementoModeladoBPMN]				  = [TBN8ActividadBPMNTramiteAccion].[Idnt_ElementoModeladoBPMN]
    and [ElementoModeladoBPMNDescripcion].[Codigo_Idioma] = 'ES'
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8Accion] with (nolock)
    on [TBN8ActividadBPMNTramiteAccion].[Idnt_Accion] = [TBN8Accion].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionActuacion] with (nolock)
    on [TBN8ActividadBPMNTramiteAccion].[Idnt_Accion] = [TBN8AccionActuacion].[Idnt_Accion]
    left join [DBN8GESTORPROC].[dbo].[TBN8TipoActuacion] with (nolock)
    on [TBN8TipoActuacion].Idnt_TipoActuacion = [TBN8AccionActuacion].Idnt_TipoActuacion
    left JOIN [DBN8GESTORPROC].[dbo].[TBN8Denominacion] with (nolock)
    ON [TBN8TipoActuacion].Idnt_TipoActuacion = TBN8Denominacion.Idnt_Entidad_Denominada
    AND Idnt_InventarioEntidades = 14
  LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionFaseEstadoExterno] with (nolock)
    on [TBN8ActividadBPMNTramiteAccion].[Idnt_Accion] = [TBN8AccionFaseEstadoExterno].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8AccionLLamadaServicio] with (nolock)
    on [TBN8ActividadBPMNTramiteAccion].[Idnt_Accion] = [TBN8AccionLLamadaServicio].[Idnt_Accion]
    LEFT JOIN [DBN8GESTORPROCP3].[dbo].[TBN8ServicioTramitacion] with (nolock)
    on [TBN8AccionLLamadaServicio].[Idnt_ServicioTramitacion] = [TBN8ServicioTramitacion].[Idnt_ServicioTramitacion]
    where [TBN8ActividadBPMNTramiteAccion].[Idnt_ProcedimientoBLT] = @Idnt_ProcedimientoBLT
    and [TBN8ActividadBPMNTramiteAccion].[VersionProcedimientoBLT_Sistema_Inicio] = @VersionProcedimientoBLT_Sistema_Inicio
    and [TBN8ActividadBPMNTramiteAccion].[VersionProcedimientoBLT_Vigencia_Inicio] = @VersionProcedimientoBLT_Vigencia_Inicio
