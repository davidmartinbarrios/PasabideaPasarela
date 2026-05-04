using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Process_BreakRepository: IProcess_BreakRepository
    {
        private readonly DBContext DB;

        public Process_BreakRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "PROCESS_BREAK";
            public const string PR_ID = "PR_ID";
            public const string PR_NAME = "PR_NAME";

            //Querys
            public const string GetByModelName = "SELECT P.PR_ID, P.PR_NAME " +
                                                 "FROM PROCESS P " +
                                                 "JOIN CW_PROP_TYPE PT " +
                                                 "ON PT.PPT_FIELD_NAME = 'PR_TYPE' " +
                                                      "AND PT.MODEL_NAME     = P.MODEL_NAME " +
                                                 "JOIN CW_LOOKUP L " +
                                                       "ON L.PPT_ID     = PT.PPT_ID " +
                                                      "AND L.LU_NUMBER  = P.PR_TYPE " +
                                                      "AND L.MODEL_NAME = P.MODEL_NAME " +
                                                 " WHERE P.MODEL_NAME = {0} " +
                                                   "AND L.LU_NAME    = 'PROCESS_BREAK'";
        }

        private Entities.POCOs.Process_Break Parse_DataRow_To_POCO(DataRow dr)
        {
            Entities.POCOs.Process_Break ret = new Entities.POCOs.Process_Break
            {
                PR_ID = (int)dr[Table.PR_ID],
                PR_NAME = dr[Table.PR_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Entities.POCOs.Process_Break>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Entities.POCOs.Process_Break>> response = new ResponseBase<IList<Entities.POCOs.Process_Break>>();
            IList<Entities.POCOs.Process_Break> retList = new List<Entities.POCOs.Process_Break>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entities.POCOs.Process_Break process_Break;

                foreach (DataRow dr in listDT.Rows)
                {
                    process_Break = Parse_DataRow_To_POCO(dr);
                    retList.Add(process_Break);
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
