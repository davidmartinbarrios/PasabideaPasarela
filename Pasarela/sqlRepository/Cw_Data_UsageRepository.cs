using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Cw_Data_UsageRepository: ICw_Data_UsageRepository
    {
        private readonly DBContext DB;

        public Cw_Data_UsageRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "CW_DATA_USAGE";
            public const string DM_DELETES = "DM_DELETES";
            public const string DM_INSERTS = "DM_INSERTS";
            public const string LU_ABBREVIATION = "LU_ABBREVIATION";

            //Querys
            public const string GetAll = "SELECT DU.DM_ID, DU.AT_ID, DU.DM_DESCRIPTION FROM [erwin_evolve].[dbo].[CW_DATA_USAGE] DU WHERE DU.MODEL_NAME = {0}";
        }

        private Cw_Data_Usage Parse_DataRow_To_POCO(DataRow dr)
        {
            Cw_Data_Usage ret = new Cw_Data_Usage
            {
                DM_DELETES = (int)dr[Table.DM_DELETES],
                DM_INSERTS = (int)dr[Table.DM_INSERTS],
            };
            return ret;
        }

        public ResponseBase<IList<Cw_Data_Usage>> GetAll()
        {
            ResponseBase<IList<Cw_Data_Usage>> response = new ResponseBase<IList<Cw_Data_Usage>>();
            IList<Cw_Data_Usage> retList = new List<Cw_Data_Usage>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Cw_Data_Usage usage;

                foreach (DataRow dr in listDT.Rows)
                {
                    usage = Parse_DataRow_To_POCO(dr);
                    retList.Add(usage);
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
