using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class Conectores_DIRepository : IConectores_DIRepository
    {
        private readonly DBContext DB;
        private readonly DBContext DBPasarela;

        public Conectores_DIRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
            this.DBPasarela = new DBContext(Settings.BD_PASARELA);
        }
        public struct Table
        {
            public const string _Name = "CONNECTOR";
            public const string PROCEDIMIENTO = "PROCEDIMIENTO";
            public const string ID_CONECTOR = "ID_CONECTOR";
            public const string DIAGRAMA = "DIAGRAMA";
            public const string NUM = "NUM";
            public const string NUM_SEC_DESDE = "NUM_SEC_DESDE";
            public const string NUM_SEC_HASTA = "NUM_SEC_HASTA";
            public const string CAT_CONECTOR = "CAT_CONECTOR";
            public const string CAT_CONECTOR2 = "CAT_CONECTOR2";
            public const string DI_ID = "DI_ID";
            public const string ORDEN_N1 = "ORDEN_N1";
            public const string ORDEN_N2 = "ORDEN_N2";
            public const string ORDEN_N3 = "ORDEN_N3";
            public const string ORDEN_N4 = "ORDEN_N4";
            public const string TIPO_CONECTOR = "TIPO_CONECTOR";
            public const string SALIDA = "SALIDA";

            //Querys
            public const string Delete = "DELETE FROM [PASARELA].[dbo].[CONECTORES_DI] WHERE DI_ID = {0}";

            public const string GetByModelNameAndID = "IF OBJECT_ID('tempdb..#NODOS_DI') IS NOT NULL DROP TABLE #NODOS_DI; " +
                                                 "SELECT " +
                                                 "S.DI_ID, " +
                                                 "D.ANO_ID AS ID_DIAGRAMA, " +
                                                 "S.SH_SEQ AS NUM_SEQ, " +
                                                 "COALESCE(P.PR_NAME, E.EV_NAME, O.OU_NAME) AS NOMBRE, " +
                                                 "OT.OT_NAME AS TIPO " +
                                                 "INTO #NODOS_DI " +
                                                 "FROM dbo.SHAPE S " +
                                                 "JOIN dbo.DIAGRAM D ON D.DI_ID = S.DI_ID AND D.MODEL_NAME = {0} " +
                                                 "LEFT JOIN dbo.CW_OBJECT_TYPE OT ON OT.OT_ID = S.ANO_TABNR AND OT.MODEL_NAME = S.MODEL_NAME " +
                                                 "LEFT JOIN dbo.PROCESS P  ON P.PR_ID = S.ANO_ID AND P.MODEL_NAME = S.MODEL_NAME " +
                                                 "LEFT JOIN dbo.EVENT E  ON E.EV_ID = S.ANO_ID AND E.MODEL_NAME = S.MODEL_NAME " +
                                                 "LEFT JOIN dbo.ORGANIZATION O  ON O.OU_ID = S.ANO_ID AND O.MODEL_NAME = S.MODEL_NAME " +
                                                 "WHERE S.MODEL_NAME = {1} " +
                                                 "AND S.DI_ID      = {2};";

            public const string InsertConectoresDIIntoPasarela = "( ID_CONECTOR, DI_ID, ID_DIAGRAMA, NUM_SEQ_DESDE, TIPO_DESDE, NOMBRE_DESDE, " +
                                                            "NUM_SEQ_HASTA, TIPO_HASTA, NOMBRE_HASTA ) " +
                                                            "SELECT " +
                                                            "J.JO_SEQ AS ID_CONECTOR, " +
                                                            "J.DI_ID, " +
                                                            "@DiAnoID AS ID_DIAGRAMA, " +
                                                            "J.SH_SEQ_FROM, " +
                                                            "ND.TIPO, ND.NOMBRE, " +
                                                            "J.SH_SEQ_TO, " +
                                                            "NH.TIPO, NH.NOMBRE " +
                                                            "FROM dbo.JOINER J " +
                                                            "LEFT JOIN #NODOS_DI ND ON ND.DI_ID = J.DI_ID AND ND.NUM_SEQ = J.SH_SEQ_FROM " +
                                                            "LEFT JOIN #NODOS_DI NH ON NH.DI_ID = J.DI_ID AND NH.NUM_SEQ = J.SH_SEQ_TO " +
                                                            "WHERE J.MODEL_NAME = @ModelName " +
                                                            "AND J.DI_ID      = @DiID " +
                                                            "AND NOT EXISTS ( " +
                                                            "    SELECT 1 FROM CONECTORES_DI C " +
                                                            "    WHERE C.ID_CONECTOR = J.JO_SEQ " +
                                                            "    );";
        }

        private Conectores_DI Parse_DataRow_To_POCO(DataRow dr)
        {
            Conectores_DI ret = new Conectores_DI
            {
                PROCEDIMIENTO = dr[Table.PROCEDIMIENTO].ToString(),
                ID_CONECTOR = dr[Table.ID_CONECTOR].ToString(),
                DIAGRAMA = dr[Table.DIAGRAMA].ToString(),
                NUM = (int)dr[Table.NUM],
                NUM_SEC_DESDE = (int)dr[Table.NUM_SEC_DESDE],
                NUM_SEC_HASTA = (int)dr[Table.NUM_SEC_HASTA],
                CAT_CONECTOR = dr[Table.CAT_CONECTOR].ToString(),
                CAT_CONECTOR2 = dr[Table.CAT_CONECTOR2].ToString(),
                DI_ID = (int)dr[Table.DI_ID],
                ORDEN_N1 = (int)dr[Table.ORDEN_N1],
                ORDEN_N2 = (int)dr[Table.ORDEN_N2],
                ORDEN_N3 = (int)dr[Table.ORDEN_N3],
                ORDEN_N4 = (int)dr[Table.ORDEN_N4],
                TIPO_CONECTOR = (int)dr[Table.TIPO_CONECTOR],
                SALIDA = dr[Table.SALIDA].ToString(),

            };
            return ret;
        }

        public ResponseBase<IList<Conectores_DI>> GetByDIModelName()
        {
            ResponseBase<IList<Conectores_DI>> response = new ResponseBase<IList<Conectores_DI>>();
            IList<Conectores_DI> retList = new List<Conectores_DI>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelNameAndID, "", "", 1);

            //try
            //{
            //    Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
            //    DataTable listDT = this.DB.GetDataTable();
            //
            //    Diagram diagram;
            //
            //    foreach (DataRow dr in listDT.Rows)
            //    {
            //        diagram = Parse_DataRow_To_POCO(dr);
            //        retList.Add(diagram);
            //    }
            //    response.Data = retList;
            //
            //    if (response.Query_Result.Query_HasError())
            //        Logger.Error(
            //            TraceHelper.StackTracePath(new StackTrace()),
            //            response.Query_Result.SQLMessage);
            //
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
            //    throw;
            //}
            //finally
            //{
            //    this.DB.CloseConnection();
            //}
            return response;
        }
        public ResponseBase<IList<Conectores_DI>> GetByModelNameAndID(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Conectores_DI>> response = new ResponseBase<IList<Conectores_DI>>();
            IList<Conectores_DI> retList = new List<Conectores_DI>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelNameAndID, ModelName, ModelName, DI_ID);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Conectores_DI connector;

                foreach (DataRow dr in listDT.Rows)
                {
                    connector = Parse_DataRow_To_POCO(dr);
                    retList.Add(connector);
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

        public ResponseBase<int> Insert(Conectores_DI obj)
        {
            Logger.Info("Accedemos a la funcion InsertDiagramIntoPasarela.");

            ResponseBase<int> response = new ResponseBase<int>();
            //this.DBPasarela.ClearParameters();
            //this.DBPasarela.Type = CommandType.Text;
            //
            //
            ////var getAByDIIDResponse = GetByDIID(ModelName, id);
            ////
            ////if (getAByDIIDResponse.Query_Result.Query_HasError())
            ////{
            ////    Logger.Error(
            ////        TraceHelper.StackTracePath(new StackTrace()),
            ////        getAByDIIDResponse.Query_Result.SQLMessage);
            ////    response.Data = int.MinValue;
            ////    return response;
            ////}
            ////this.DB.ClearParameters();
            ////this.DB.Type = CommandType.Text;
            //this.DBPasarela.SqlStatment = string.Format(Table.InsertDiagramIntoPasarela, obj.PROCEDIMIENTO, obj.ORDEN_N1, obj.ORDEN_N2, obj.ORDEN_N3, obj.ORDEN_N4, obj.ORDEN_N5, obj.CAT_DIAGRAMA, obj.NOMBRE,
            //    obj.USERDEFINED, obj.NIVEL, ConvertEmptyToNull(obj.ARBOL), ConvertEmptyToNull(obj.PLAZOTIPO1), ConvertEmptyToNull(obj.PLAZOTIPO2), ConvertEmptyToNull(obj.NIV_TRAMIT),
            //    ConvertEmptyToNull(obj.BLOQUEO_EXP), ConvertEmptyToNull(obj.UNION_RAMAS), ConvertEmptyToNull(obj.TRAMIT_SIMUL), ConvertEmptyToNull(obj.TRAM_OCULTO), ConvertEmptyToNull(obj.IND_VALORVAR),
            //    ConvertEmptyToNull(obj.VUELTA_ATRAS), obj.NOMBRE_TRAM);
            //
            //try
            //{
            //    Logger.Info("Ejecutamos la query: " + this.DBPasarela.SqlStatment);
            //    this.DBPasarela.ExecuteNonQuery();
            //
            //    if (response.Query_Result.Query_HasError())
            //        Logger.Error(
            //            TraceHelper.StackTracePath(new StackTrace()),
            //            response.Query_Result.SQLMessage);
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(
            //        TraceHelper.StackTracePath(new StackTrace()),
            //        ex);
            //    response.Data = int.MinValue;
            //}
            //finally
            //{
            //    this.DBPasarela.CloseConnection();
            //}

            return response;
        }

        public ResponseBase<bool> Delete(int IdDiagrama)
        {
            Logger.Info("Accedemos a la funcion Delete.");

            ResponseBase<bool> response = new ResponseBase<bool>();
            this.DBPasarela.ClearParameters();
            this.DBPasarela.Type = CommandType.Text;


            
            this.DBPasarela.SqlStatment = string.Format(Table.Delete, IdDiagrama);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DBPasarela.SqlStatment);
                this.DBPasarela.ExecuteNonQuery();

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);
                else
                    response.Data = true;
            }
            catch (Exception ex)
            {
                Logger.Error(
                    TraceHelper.StackTracePath(new StackTrace()),
                    ex);
                response.Data = false;
            }
            finally
            {
                this.DBPasarela.CloseConnection();
            }

            return response;
        }
    }
}
