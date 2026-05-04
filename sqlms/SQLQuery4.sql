USE erwin_evolve;
go
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) ug.ug_name, cw_u.[MODEL_NAME]
      ,cw_u.[US_ID]
      ,[PL_ID]
      ,[US_DATAFIT_NAME]      
      ,[US_NAME]      
  FROM [erwin_evolve].[dbo].[CW_USER] cw_u, MEMBERSHIP mem  
  , USER_GROUP ug
  --where US_DATAFIT_NAME in ('KX150004', 'CO21')
  --where US_DATAFIT_NAME in ('CO21')
  where US_DATAFIT_NAME in ('MB57')
  and cw_u.US_ID = mem.US_ID
  and mem.UG_ID = ug.UG_ID