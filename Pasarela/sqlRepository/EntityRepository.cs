using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class EntityRepository: IEntityRepository
    {
        private readonly DBContext DB;

        public EntityRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "ENTITY";
            public const string EN_ID = "EN_ID";
            public const string EN_NAME = "EN_NAME";

            //Querys
            public const string GetAll = "SELECT TOP  10 EN_ID, EN_NAME  FROM [erwin_evolve].[dbo].[ENTITY]";
        }

        private Entity Parse_DataRow_To_POCO(DataRow dr)
        {
            Entity ret = new Entity
            {
                EN_ID = (int)dr[Table.EN_ID],
                EN_NAME = dr[Table.EN_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Entity>> GetAll()
        {
            ResponseBase<IList<Entity>> response = new ResponseBase<IList<Entity>>();
            IList<Entity> retList = new List<Entity>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entity entity;

                foreach (DataRow dr in listDT.Rows)
                {
                    entity = Parse_DataRow_To_POCO(dr);
                    retList.Add(entity);
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
