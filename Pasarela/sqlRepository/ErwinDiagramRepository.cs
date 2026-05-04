using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class ErwinDiagramRepository : IErwinDiagramRepository
    {
        private readonly DBContext DB;

        public ErwinDiagramRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }

        public struct Table
        {
            public const string _Name = "DIAGRAM";
            public const string DI_ID = "DI_ID";
            public const string DI_NAME = "DI_NAME";
            public const string DI_TYPE = "DI_TYPE";
            public const string ANO_ID = "ANO_ID";
            public const string ANO_TABNR = "ANO_TABNR";

            public const string GetByModelName =
                "SELECT D.DI_ID, D.DI_NAME, D.DI_TYPE, D.ANO_ID, D.ANO_TABNR " +
                "FROM DIAGRAM D " +
                "WHERE D.MODEL_NAME = '{0}' " +
                "ORDER BY D.DI_NAME";
        }

        private Entities.POCOs.ErwinDiagram Parse_DataRow_To_POCO(DataRow dr)
        {
            var debug = dr[Table.DI_ID] + " - " + dr[Table.DI_NAME] + " - " + dr[Table.DI_TYPE] + " - " + dr[Table.ANO_ID] + " - " + dr[Table.ANO_TABNR];

            Entities.POCOs.ErwinDiagram ret = new Entities.POCOs.ErwinDiagram
            {
                DI_ID = (int)dr[Table.DI_ID],
                DI_NAME = dr[Table.DI_NAME].ToString(),
                DI_TYPE = (int)dr[Table.DI_TYPE],
                ANO_ID = (int)dr[Table.ANO_ID],
                ANO_TABNR = dr[Table.ANO_TABNR] == DBNull.Value ? 0 : (int)dr[Table.ANO_TABNR]
            };
            return ret;
        }

        public ResponseBase<IList<Entities.POCOs.ErwinDiagram>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Entities.POCOs.ErwinDiagram>> response = new ResponseBase<IList<Entities.POCOs.ErwinDiagram>>();
            IList<Entities.POCOs.ErwinDiagram> retList = new List<Entities.POCOs.ErwinDiagram>();
            this.DB.ClearParameters();

            string safeModelName = ModelName.Replace("'", "''");
            this.DB.SqlStatment = string.Format(Table.GetByModelName, safeModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entities.POCOs.ErwinDiagram erwinDiagram;

                foreach (DataRow dr in listDT.Rows)
                {
                    erwinDiagram = Parse_DataRow_To_POCO(dr);
                    retList.Add(erwinDiagram);
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
