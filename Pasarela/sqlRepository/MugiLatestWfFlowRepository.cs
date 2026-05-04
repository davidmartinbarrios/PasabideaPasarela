using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class MugiLatestWfFlowRepository : IMugiLatestWfFlowRepository
    {
        private readonly DBContext DB;

        public MugiLatestWfFlowRepository()
        {
            // TODO: cambiar al Settings de la conexion al servidor donde residen las BBDD DBN8*
            this.DB = new DBContext(Settings.BD_DP4);
        }

        public struct Table
        {
            public const string BaseDatos = "BaseDatos";
            public const string Flow = "Flow";
            public const string Version = "Version";
            public const string Comments = "Comments";


            public const string GetLatestVersionsDBN8 =
    "IF OBJECT_ID('tempdb..#Flujos') IS NOT NULL " +
    "    DROP TABLE #Flujos; " +

    "CREATE TABLE #Flujos ( " +
    "    BaseDatos sysname, " +
    "    Flow nvarchar(255), " +
    "    Version nvarchar(50), " +
    "    Comments nvarchar(max) " +
    "); " +

    "DECLARE @db sysname; " +
    "DECLARE @sql nvarchar(max); " +

    "DECLARE db_cursor CURSOR FAST_FORWARD FOR " +
    "SELECT d.name " +
    "FROM sys.databases d " +
    "WHERE d.name LIKE 'DBN8%' " +
    "  AND d.state_desc = 'ONLINE' " +
    "  AND HAS_DBACCESS(d.name) = 1; " +

    "OPEN db_cursor; " +
    "FETCH NEXT FROM db_cursor INTO @db; " +

    "WHILE @@FETCH_STATUS = 0 " +
    "BEGIN " +
    "    SET @sql = N' " +
    "    IF EXISTS ( " +
    "        SELECT 1 " +
    "        FROM ' + QUOTENAME(@db) + N'.sys.objects o " +
    "        INNER JOIN ' + QUOTENAME(@db) + N'.sys.schemas s ON s.schema_id = o.schema_id " +
    "        WHERE o.name = ''wfFlows'' " +
    "          AND s.name = ''dbo'' " +
    "          AND o.type = ''U'' " +
    "    ) " +
    "    BEGIN " +
    "        INSERT INTO #Flujos (BaseDatos, Flow, Version, Comments) " +
    "        SELECT ' + QUOTENAME(@db, '''') + N', Flow, Version, Comments " +
    "        FROM ' + QUOTENAME(@db) + N'.dbo.wfFlows " +
    "        WHERE Flow IS NOT NULL " +
    "          AND LTRIM(RTRIM(Flow)) <> '''''' " +
    "    END'; " +

    "    EXEC sys.sp_executesql @sql; " +
    "    FETCH NEXT FROM db_cursor INTO @db; " +
    "END; " +

    "CLOSE db_cursor; " +
    "DEALLOCATE db_cursor; " +

    "WITH FlujosRanked AS ( " +
    "    SELECT " +
    "        BaseDatos, " +
    "        Flow, " +
    "        Version, " +
    "        Comments, " +
    "        ROW_NUMBER() OVER ( " +
    "            PARTITION BY BaseDatos, Flow " +
    "            ORDER BY TRY_CONVERT(int, Version) DESC, Version DESC " +
    "        ) AS rn " +
    "    FROM #Flujos " +
    ") " +
    "SELECT " +
    "    BaseDatos, " +
    "    Flow, " +
    "    Version, " +
    "    Comments " +
    "FROM FlujosRanked " +
    "WHERE rn = 1 " +
    "ORDER BY BaseDatos, Flow;";


            //public const string GetLatestVersionsDBN8 =
            //    "DECLARE @sql nvarchar(max) = N''; " +
            //    "SELECT @sql = @sql + " +
            //    "    CASE WHEN @sql = N'' THEN N'' ELSE CHAR(13) + N'UNION ALL' + CHAR(13) END + " +
            //    "    N'SELECT " +
            //    "          ' + QUOTENAME(d.name, '''''') + N' AS BaseDatos, " +
            //    "          Flow, " +
            //    "          Version, " +
            //    "          Comments " +
            //    "      FROM ' + QUOTENAME(d.name) + N'.dbo.wfFlows' " +
            //    "FROM sys.databases d " +
            //    "WHERE d.name LIKE 'DBN8%' " +
            //    "  AND d.state_desc = 'ONLINE' " +
            //    "  AND HAS_DBACCESS(d.name) = 1 " +
            //    "  AND OBJECT_ID(d.name + '.dbo.wfFlows') IS NOT NULL; " +
            //    "SET @sql = N' " +
            //    "WITH Flujos AS ( " +
            //    "' + @sql + N' " +
            //    "), " +
            //    "FlujosRanked AS ( " +
            //    "    SELECT " +
            //    "        BaseDatos, " +
            //    "        Flow, " +
            //    "        Version, " +
            //    "        Comments, " +
            //    "        ROW_NUMBER() OVER ( " +
            //    "            PARTITION BY BaseDatos, Flow " +
            //    "            ORDER BY TRY_CONVERT(int, Version) DESC, Version DESC " +
            //    "        ) AS rn " +
            //    "    FROM Flujos " +
            //    "    WHERE Flow IS NOT NULL " +
            //    "      AND LTRIM(RTRIM(Flow)) <> '''' " +
            //    ") " +
            //    "SELECT " +
            //    "    BaseDatos, " +
            //    "    Flow, " +
            //    "    Version, " +
            //    "    Comments " +
            //    "FROM FlujosRanked " +
            //    "WHERE rn = 1 " +
            //    "ORDER BY BaseDatos, Flow;'; " +
            //    "EXEC sys.sp_executesql @sql;";
        }

        private Entities.POCOs.MugiLatestWfFlow Parse_DataRow_To_POCO(DataRow dr)
        {
            Entities.POCOs.MugiLatestWfFlow ret = new Entities.POCOs.MugiLatestWfFlow
            {
                BaseDatos = dr[Table.BaseDatos] == DBNull.Value ? null : dr[Table.BaseDatos].ToString(),
                Flow = dr[Table.Flow] == DBNull.Value ? null : dr[Table.Flow].ToString(),
                Version = dr[Table.Version] == DBNull.Value ? null : dr[Table.Version].ToString(),
                Comments = dr[Table.Comments] == DBNull.Value ? null : dr[Table.Comments].ToString()
            };
            return ret;
        }

        public ResponseBase<IList<Entities.POCOs.MugiLatestWfFlow>> GetLatestVersionsDBN8()
        {
            ResponseBase<IList<Entities.POCOs.MugiLatestWfFlow>> response = new ResponseBase<IList<Entities.POCOs.MugiLatestWfFlow>>();
            IList<Entities.POCOs.MugiLatestWfFlow> retList = new List<Entities.POCOs.MugiLatestWfFlow>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetLatestVersionsDBN8;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entities.POCOs.MugiLatestWfFlow item;

                foreach (DataRow dr in listDT.Rows)
                {
                    item = Parse_DataRow_To_POCO(dr);
                    retList.Add(item);
                }

                response.Data = retList;

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }

            return response;
        }
    }
}
