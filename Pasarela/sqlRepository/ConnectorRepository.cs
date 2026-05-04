using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class ConnectorRepository: IConnectorRepository
    {
        private readonly DBContext DB;

        public ConnectorRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CONNECTOR";
            public const string CO_ID = "CO_ID";
            public const string CO_CONDITION = "CO_CONDITION";
            public const string MODEL_NAME = "MODEL_NAME";

            //Querys
            public const string GetByModelName = "SELECT C.CO_ID, C.CO_CONDITION as CO_NAME, C.MODEL_NAME FROM [erwin_evolve].[dbo].[CONNECTOR] C WHERE C.MODEL_NAME = {0}";
        }

        private Connector Parse_DataRow_To_POCO(DataRow dr)
        {
            Connector ret = new Connector
            {
                CO_ID = (int)dr[Table.CO_ID],
                CO_CONDITION = dr[Table.CO_CONDITION].ToString(),
                MODEL_NAME = dr[Table.MODEL_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Connector>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Connector>> response = new ResponseBase<IList<Connector>>();
            IList<Connector> retList = new List<Connector>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Connector connector;

                foreach (DataRow dr in listDT.Rows)
                {
                    connector = Parse_DataRow_To_POCO(dr);
                    retList.Add(connector);
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
