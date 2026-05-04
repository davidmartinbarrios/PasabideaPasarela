using System;
using System.Collections.Generic;
using System.Data;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;
using System.Diagnostics;

namespace Lantik.Pasarela.sqlRepository
{
    public class ShapeRepository: IShapeRepository
    {
        private readonly DBContext DB;

        public ShapeRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "SHAPE";
            public const string DI_ID = "DI_ID";
            public const string NUM_SEQ = "NUM_SEQ";
            public const string ANO_TABNR = "ANO_TABNR";
            public const string ANO_ID = "ANO_ID";
            public const string SH_Y = "SH_Y";
            public const string SH_X = "SH_X";
            public const string OT_NAME = "OT_NAME";

            //Querys
            public const string GetByModelNameAndID = "SELECT S.DI_ID, S.SH_SEQ AS NUM_SEQ, S.ANO_TABNR, S.ANO_ID, S.SH_Y, S.SH_X, OT.OT_NAME FROM [erwin_evolve].[dbo].[SHAPE] S " +
                                         "LEFT JOIN [erwin_evolve].[dbo].[CW_OBJECT_TYPE] OT ON OT.OT_ID = S.ANO_TABNR AND OT.MODEL_NAME = S.MODEL_NAME " +
                                         "WHERE S.MODEL_NAME = '{0}' AND S.DI_ID = {1};";
        }

        private Shape Parse_DataRow_To_POCO(DataRow dr)
        {
            Shape ret = new Shape
            {
                DI_ID = (int)dr[Table.DI_ID],
                NUM_SEQ = (int)dr[Table.NUM_SEQ],
                ANO_TABNR = (int)dr[Table.ANO_TABNR],
                ANO_ID = (int)dr[Table.ANO_ID],
                SH_Y = (int)dr[Table.SH_Y],
                SH_X = (int)dr[Table.SH_X],
                OT_NAME = dr[Table.OT_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Shape>> GetByModelNameAndID(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Shape>> response = new ResponseBase<IList<Shape>>();
            IList<Shape> retList = new List<Shape>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelNameAndID, ModelName, DI_ID);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Shape shape;

                foreach (DataRow dr in listDT.Rows)
                {
                    shape = Parse_DataRow_To_POCO(dr);
                    retList.Add(shape);
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
