using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Cw_LookupRepository: ICw_LookupRepository
    {
        private readonly DBContext DB;

        public Cw_LookupRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CW_LOOKUP";
            public const string LU_ID = "LU_ID";
            public const string LU_NAME = "LU_NAME";
            public const string LU_ABBREVIATION = "LU_ABBREVIATION";

            //Querys
            public const string GetAll = "SELECT TOP  10 LU_ID, LU_NAME, LU_ABBREVIATION  FROM [erwin_evolve].[dbo].[cw_LOOKUP]";
            public const string GetByModelName = "SELECT LU_ID, LU_NAME, LU_ABBREVIATION  FROM [erwin_evolve].[dbo].[cw_LOOKUP] WHERE MODEL_NAME= {0}";
        }

        private Cw_Lookup Parse_DataRow_To_POCO(DataRow dr)
        {
            Cw_Lookup ret = new Cw_Lookup
            {
                LU_ID = (int)dr[Table.LU_ID],
                LU_NAME = dr[Table.LU_NAME].ToString(),
                LU_ABBREVIATION = dr[Table.LU_ABBREVIATION].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Cw_Lookup>> GetAll()
        {
            ResponseBase<IList<Cw_Lookup>> response = new ResponseBase<IList<Cw_Lookup>>();
            IList<Cw_Lookup> retList = new List<Cw_Lookup>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {

                DataTable listDT = this.DB.GetDataTable();

                Cw_Lookup lookup;

                foreach (DataRow dr in listDT.Rows)
                {
                    lookup = Parse_DataRow_To_POCO(dr);
                    retList.Add(lookup);
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

        public ResponseBase<IList<Cw_Lookup>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Cw_Lookup>> response = new ResponseBase<IList<Cw_Lookup>>();
            IList<Cw_Lookup> retList = new List<Cw_Lookup>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = String.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Cw_Lookup lookup;

                foreach (DataRow dr in listDT.Rows)
                {
                    lookup = Parse_DataRow_To_POCO(dr);
                    retList.Add(lookup);
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
