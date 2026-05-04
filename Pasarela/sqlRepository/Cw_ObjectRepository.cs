using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Cw_ObjectRepository: ICw_ObjectRepository
    {
        private readonly DBContext DB;

        public Cw_ObjectRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CW_OBJECT";
            public const string GO_ID = "GO_ID";
            public const string OT_ID = "OT_ID";
            public const string GO_NAME = "GO_NAME";
            public const string USERDEFINED = "USERDEFINED";

            //Querys
            public const string GetByModelName = "SELECT GO_ID, OT_ID, GO_NAME, USERDEFINED FROM [erwin_evolve].[dbo].[CW_OBJECT] WHERE MODEL_NAME = {0}";
        }

        private Cw_Object Parse_DataRow_To_POCO(DataRow dr)
        {
            Cw_Object ret = new Cw_Object
            {
                GO_ID = (int)dr[Table.GO_ID],
                OT_ID = (int)dr[Table.OT_ID],
                GO_NAME = dr[Table.GO_NAME].ToString(),
                USERDEFINED = dr[Table.USERDEFINED].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Cw_Object>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Cw_Object>> response = new ResponseBase<IList<Cw_Object>>();
            IList<Cw_Object> retList = new List<Cw_Object>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = String.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Cw_Object _object;

                foreach (DataRow dr in listDT.Rows)
                {
                    _object = Parse_DataRow_To_POCO(dr);
                    retList.Add(_object);
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
