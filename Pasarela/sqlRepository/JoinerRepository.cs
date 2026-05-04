using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class JoinerRepository: IJoinerRepository
    {
        private readonly DBContext DB;

        public JoinerRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "JOINER";
            public const string DI_ID = "DI_ID";
            public const string ID_CONECTOR = "ID_CONECTOR";
            public const string NUM_SEQ_DESDE = "NUM_SEQ_DESDE";
            public const string NUM_SEQ_HASTA = "NUM_SEQ_HASTA";

            //Querys
            public const string GetByModelNameAndID = "SELECT J.DI_ID, J.JO_SEQ AS ID_CONECTOR, J.SH_SEQ_FROM AS NUM_SEQ_DESDE, J.SH_SEQ_TO AS NUM_SEQ_HASTA FROM [erwin_evolve].[dbo].[JOINER] J " +
                                         "WHERE J.MODEL_NAME = {0} AND J.DI_ID = {1}";
        }

        private Joiner Parse_DataRow_To_POCO(DataRow dr)
        {
            Joiner ret = new Joiner
            {
                DI_ID = (int)dr[Table.DI_ID],
                ID_CONECTOR = (int)dr[Table.ID_CONECTOR],
                NUM_SEQ_DESDE = (int)dr[Table.NUM_SEQ_DESDE],
                NUM_SEQ_HASTA = (int)dr[Table.NUM_SEQ_HASTA],
            };
            return ret;
        }

        public ResponseBase<IList<Joiner>> GetByModelNameAndID(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Joiner>> response = new ResponseBase<IList<Joiner>>();
            IList<Joiner> retList = new List<Joiner>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = String.Format(Table.GetByModelNameAndID, ModelName, DI_ID);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Joiner joiner;

                foreach (DataRow dr in listDT.Rows)
                {
                    joiner = Parse_DataRow_To_POCO(dr);
                    retList.Add(joiner);
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
