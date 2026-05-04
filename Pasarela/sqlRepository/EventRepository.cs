using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class EventRepository: IEventRepository
    {
        private readonly DBContext DB;

        public EventRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "EVENT";
            public const string EV_ID = "EV_ID";
            public const string EV_NAME = "EV_NAME";

            //Querys
            public const string GetByModelName = "SELECT E.EV_ID, E.EV_NAME FROM [erwin_evolve].[dbo].[EVENT] E WHERE E.MODEL_NAME = {0};";
        }

        private Event Parse_DataRow_To_POCO(DataRow dr)
        {
            Event ret = new Event
            {
                EV_ID = (int)dr[Table.EV_ID],
                EV_NAME = dr[Table.EV_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Event>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Event>> response = new ResponseBase<IList<Event>>();
            IList<Event> retList = new List<Event>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Event _event;

                foreach (DataRow dr in listDT.Rows)
                {
                    _event = Parse_DataRow_To_POCO(dr);
                    retList.Add(_event);
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
