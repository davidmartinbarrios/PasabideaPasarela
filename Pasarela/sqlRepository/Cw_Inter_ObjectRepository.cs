using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Cw_Inter_ObjectRepository: ICw_Inter_ObjectRepository
    {
        private readonly DBContext DB;

        public Cw_Inter_ObjectRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CW_INTER_OBJECT";
            public const string ANO_ID_BELOW = "ANO_ID_BELOW";
            public const string GO_NAME = "GO_NAME";

            //Querys
            public const string GetAll = "SELECT TOP  10 ANO_ID_BELOW, GO_NAME  FROM [erwin_evolve].[dbo].[CW_INTER_OBJECT]";
        }

        private Cw_Inter_Object Parse_DataRow_To_POCO(DataRow dr)
        {
            Cw_Inter_Object ret = new Cw_Inter_Object
            {
                ANO_ID_BELOW = (int)dr[Table.ANO_ID_BELOW],
                GO_NAME = dr[Table.GO_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Cw_Inter_Object>> GetAll()
        {
            ResponseBase<IList<Cw_Inter_Object>> response = new ResponseBase<IList<Cw_Inter_Object>>();
            IList<Cw_Inter_Object> retList = new List<Cw_Inter_Object>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Cw_Inter_Object _object;

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
