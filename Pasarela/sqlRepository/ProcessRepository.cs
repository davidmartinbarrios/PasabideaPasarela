using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class ProcessRepository: IProcessRepository
    {
        private readonly DBContext DB;

        public ProcessRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "PROCESS";
            public const string PR_ID = "PR_ID";
            public const string PR_NAME = "PR_NAME";
            public const string PR_TYPE = "PR_TYPE";
            public const string PR_ALT_NAME = "PR_ALT_NAME";

            //Querys
            public const string GetByModelName = "SELECT P.PR_ID, P.PR_NAME, P.PR_TYPE, L.LU_NAME AS TIPO FROM [erwin_evolve].[dbo].[PROCESS] P " +
                                         "LEFT JOIN [erwin_evolve].[dbo].[CW_LOOKUP] L ON L.LU_ID = P.PR_TYPE AND L.MODEL_NAME = P.MODEL_NAME " +
                                         "WHERE P.MODEL_NAME = {0}";
        }

        private Entities.POCOs.Process Parse_DataRow_To_POCO(DataRow dr)
        {
            Entities.POCOs.Process ret = new Entities.POCOs.Process
            {
                PR_ID = (int)dr[Table.PR_ID],
                PR_NAME = dr[Table.PR_NAME].ToString(),
                PR_TYPE = (int)dr[Table.PR_TYPE],
                TIPO = dr[Table.PR_ALT_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Entities.POCOs.Process>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Entities.POCOs.Process>> response = new ResponseBase<IList<Entities.POCOs.Process>>();
            IList<Entities.POCOs.Process> retList = new List<Entities.POCOs.Process>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName,ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entities.POCOs.Process process;

                foreach (DataRow dr in listDT.Rows)
                {
                    process = Parse_DataRow_To_POCO(dr);
                    retList.Add(process);
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
