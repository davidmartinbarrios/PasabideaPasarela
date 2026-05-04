using System;
using System.Collections.Generic;
using System.Data;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;
using System.Diagnostics;

namespace Lantik.Pasarela.sqlRepository
{
    public class Text_FieldRepository : IText_FieldRepository
    {
        private readonly DBContext DB;

        public Text_FieldRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "TEXT_FIELD";
            public const string ANO_TABNR = "ANO_TABNR";
            public const string ANO_ID = "ANO_ID";
            public const string TT_ATTRIBUTE = "TT_ATTRIBUTE";
            public const string VALUE = "VALUE";

            //Querys
            public const string GetByModeGetByAttributeAndID = "SELECT E.ANO_TABNR,E.ANO_ID,E.TT_ATTRIBUTE,E.TT_TEXT_0 + E.TT_TEXT_1 + E.TT_TEXT_2 + E.TT_TEXT_3 + " +
                                                      "E.TT_TEXT_4 + E.TT_TEXT_5 + E.TT_TEXT_6 + E.TT_TEXT_7 AS VALUE " +
                                                      "FROM TEXT_FIELD E " +
                                                      "WHERE E.ANO_TABNR = {0} " +
                                                      "AND E.ANO_ID    = {1} " +
                                                      "AND E.TT_ATTRIBUTE = {2}";

        }
            private Text_Field Parse_DataRow_To_POCO(DataRow dr)
            {
                Text_Field ret = new Text_Field
                {
                    ANO_TABNR = (int)dr[Table.ANO_TABNR],
                    ANO_ID = (int)dr[Table.ANO_ID],
                    TT_ATTRIBUTE = (int)dr[Table.TT_ATTRIBUTE],
                    VALUE = dr[Table.VALUE].ToString(),
                };
                return ret;
            }
        public ResponseBase<IList<Text_Field>> GetByAttributeAndID(string AnoTabnr, int ANO_ID, int attribute)
        {
            ResponseBase<IList<Text_Field>> response = new ResponseBase<IList<Text_Field>>();
            IList<Text_Field> retList = new List<Text_Field>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModeGetByAttributeAndID, AnoTabnr, ANO_ID, attribute);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Text_Field text_Field;

                foreach (DataRow dr in listDT.Rows)
                {
                    text_Field = Parse_DataRow_To_POCO(dr);
                    retList.Add(text_Field);
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
