using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Cw_Object_TypeRepository: ICw_Object_TypeRepository
    {
        private readonly DBContext DB;

        public Cw_Object_TypeRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CW_OBJECT_TYPE";
            public const string OT_ID = "OT_ID";
            public const string OT_NAME = "OT_NAME";

            //Querys
            public const string GetByModelName = "SELECT OT_ID, OT_NAME FROM [erwin_evolve].[dbo].[CW_OBJECT_TYPE] WHERE MODEL_NAME = {0}";
        }

        private Cw_Object_Type Parse_DataRow_To_POCO(DataRow dr)
        {
            Cw_Object_Type ret = new Cw_Object_Type
            {
                OT_ID = (int)dr[Table.OT_ID],
                OT_NAME = dr[Table.OT_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Cw_Object_Type>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Cw_Object_Type>> response = new ResponseBase<IList<Cw_Object_Type>>();
            IList<Cw_Object_Type> retList = new List<Cw_Object_Type>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Cw_Object_Type _type;

                foreach (DataRow dr in listDT.Rows)
                {
                    _type = Parse_DataRow_To_POCO(dr);
                    retList.Add(_type);
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
