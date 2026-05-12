using System;
using System.Data;
using System.Data.SqlClient;
using Lantik.Pasabidea.Core.Helpers;

namespace Lantik.Pasabidea.Core.Data
{
    /// <summary>
    /// Acceso a datos PASARELA_ARTEZ. Solo DELETE/INSERT y transacción de escritura.
    /// </summary>
    internal sealed partial class PasarelaArtezData : IDisposable
    {
        private readonly DbContext _db = DbContext.Get("BD_PASARELA");

        public void BeginTransaction() => _db.BeginTransaction();
        public void Commit() => _db.Commit();
        public void Rollback() => _db.Rollback();

        public void DeleteByProcedimiento(string procedimiento)
        {
            Execute(Sql.DeleteParamAcc, ProcedimientoParam(procedimiento));
            Execute(Sql.DeleteConectorAcc, ProcedimientoParam(procedimiento));
            Execute(Sql.DeleteAccionesDi, ProcedimientoParam(procedimiento));
            Execute(Sql.DeletePropiedadesDi, ProcedimientoParam(procedimiento));
            Execute(Sql.DeleteConectoresDi, ProcedimientoParam(procedimiento));
            Execute(Sql.DeleteDiagrama, ProcedimientoParam(procedimiento));
        }

        public void InsertDiagramas(DataTable table) => InsertarTabla(table, InsertDiagrama);
        public void InsertConectoresDi(DataTable table) => InsertarTabla(table, InsertConectorDi);
        public void InsertPropiedadesDi(DataTable table) => InsertarTabla(table, InsertPropiedadDi);
        public void InsertAccionesDi(DataTable table) => InsertarTabla(table, InsertAccionDi);
        public void InsertConectoresAcc(DataTable table) => InsertarTabla(table, InsertConectorAcc);
        public void InsertParamAcc(DataTable table) => InsertarTabla(table, InsertParamAcc);

        private void InsertDiagrama(DataRow row)
        {
            Execute(Sql.InsertDiagrama,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ORDEN_N1", SqlDbType.Int, I(row, "ORDEN_N1")),
                P("@ORDEN_N2", SqlDbType.Int, I(row, "ORDEN_N2")),
                P("@ORDEN_N3", SqlDbType.Int, I(row, "ORDEN_N3")),
                P("@ORDEN_N4", SqlDbType.Int, I(row, "ORDEN_N4")),
                P("@ORDEN_N5", SqlDbType.Int, I(row, "ORDEN_N5")),
                P("@ID_DIAGRAMA", SqlDbType.Int, I(row, "ID_DIAGRAMA")),
                P("@ID_PADRE", SqlDbType.Int, I(row, "ID_PADRE")),
                P("@NUM_SEQ", SqlDbType.Int, I(row, "NUM_SEQ")),
                P("@DI_ID", SqlDbType.Int, I(row, "DI_ID")),
                P("@CAT_DIAGRAMA", SqlDbType.NVarChar, 50, S(row, "CAT_DIAGRAMA")),
                P("@NOMBRE", SqlDbType.NVarChar, 255, S(row, "NOMBRE")),
                P("@USERDEFINED", SqlDbType.NVarChar, S(row, "USERDEFINED")),
                P("@NIVEL", SqlDbType.Int, I(row, "NIVEL")),
                P("@ARBOL", SqlDbType.NVarChar, 1024, S(row, "ARBOL")),
                P("@PLAZOTIPO1", SqlDbType.NVarChar, 20, S(row, "PLAZOTIPO1")),
                P("@PLAZOTIPO2", SqlDbType.NVarChar, 20, S(row, "PLAZOTIPO2")),
                P("@NIV_TRAMIT", SqlDbType.NVarChar, 10, S(row, "NIV_TRAMIT")),
                P("@BLOQUEO_EXP", SqlDbType.NVarChar, 5, S(row, "BLOQUEO_EXP")),
                P("@UNION_RAMAS", SqlDbType.NVarChar, 5, S(row, "UNION_RAMAS")),
                P("@TRAMIT_SIMUL", SqlDbType.NVarChar, 5, S(row, "TRAMIT_SIMUL")),
                P("@TRAM_OCULTO", SqlDbType.NVarChar, 5, S(row, "TRAM_OCULTO")),
                P("@IND_VALORVAR", SqlDbType.NVarChar, 5, S(row, "IND_VALORVAR")),
                P("@VUELTA_ATRAS", SqlDbType.NVarChar, 5, S(row, "VUELTA_ATRAS")),
                P("@NOMBRE_TRAM", SqlDbType.NVarChar, 255, S(row, "NOMBRE_TRAM")));
        }

        private void InsertConectorDi(DataRow row)
        {
            Execute(Sql.InsertConectorDi,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ID_CONECTOR", SqlDbType.Int, I(row, "ID_CONECTOR")),
                P("@DIAGRAMA", SqlDbType.Int, I(row, "DIAGRAMA")),
                P("@NUM", SqlDbType.Int, I(row, "NUM")),
                P("@NUM_SEC_DESDE", SqlDbType.Int, I(row, "NUM_SEC_DESDE")),
                P("@NUM_SEC_HASTA", SqlDbType.Int, I(row, "NUM_SEC_HASTA")),
                P("@CAT_CONECTOR", SqlDbType.NVarChar, 100, S(row, "CAT_CONECTOR")),
                P("@DI_ID", SqlDbType.Int, I(row, "DI_ID")),
                P("@ORDEN_N1", SqlDbType.Int, I(row, "ORDEN_N1")),
                P("@ORDEN_N2", SqlDbType.Int, I(row, "ORDEN_N2")),
                P("@ORDEN_N3", SqlDbType.Int, I(row, "ORDEN_N3")),
                P("@ORDEN_N4", SqlDbType.Int, I(row, "ORDEN_N4")),
                P("@TIPO_CONECTOR", SqlDbType.NVarChar, 100, S(row, "TIPO_CONECTOR")),
                P("@SALIDA", SqlDbType.NVarChar, 1, S(row, "SALIDA")));
        }

        private void InsertPropiedadDi(DataRow row)
        {
            Execute(Sql.InsertPropiedadDi,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ORDEN_N1", SqlDbType.Int, I(row, "ORDEN_N1")),
                P("@ORDEN_N2", SqlDbType.Int, I(row, "ORDEN_N2")),
                P("@ORDEN_N3", SqlDbType.Int, I(row, "ORDEN_N3")),
                P("@ORDEN_N4", SqlDbType.Int, I(row, "ORDEN_N4")),
                P("@ORDEN_N5", SqlDbType.Int, I(row, "ORDEN_N5")),
                P("@ID_DIAGRAMA", SqlDbType.Int, I(row, "ID_DIAGRAMA")),
                P("@NOM_DIAGRAMA", SqlDbType.NVarChar, 255, S(row, "NOM_DIAGRAMA")),
                P("@TIPO_DIAGRAMA", SqlDbType.NVarChar, 100, S(row, "TIPO_DIAGRAMA")),
                P("@PLAZTIP1_DI", SqlDbType.NVarChar, 250, S(row, "PLAZTIP1_DI")),
                P("@PLAZTIP2_DI", SqlDbType.NVarChar, 250, S(row, "PLAZTIP2_DI")),
                P("@NIVELTRAM_DI", SqlDbType.NVarChar, 250, S(row, "NIVELTRAM_DI")),
                P("@INDBLOQ_DI", SqlDbType.NVarChar, 250, S(row, "INDBLOQ_DI")),
                P("@INDRAM_DI", SqlDbType.NVarChar, 250, S(row, "INDRAM_DI")),
                P("@INDPERSINT", SqlDbType.NVarChar, 20, S(row, "INDPERSINT")));
        }

        private void InsertAccionDi(DataRow row)
        {
            Execute(Sql.InsertAccionDi,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ORDEN_N1", SqlDbType.Int, I(row, "ORDEN_N1")),
                P("@ORDEN_N2", SqlDbType.Int, I(row, "ORDEN_N2")),
                P("@ORDEN_N3", SqlDbType.Int, I(row, "ORDEN_N3")),
                P("@ORDEN_N4", SqlDbType.Int, I(row, "ORDEN_N4")),
                P("@ORDEN_N5", SqlDbType.Int, I(row, "ORDEN_N5")),
                P("@ORDEN_ACC", SqlDbType.Int, I(row, "ORDEN_ACC")),
                P("@ID_ACCION", SqlDbType.Int, I(row, "ID_ACCION")),
                P("@NOM_ACCION", SqlDbType.NVarChar, 255, S(row, "NOM_ACCION")),
                P("@NUM_ACCION", SqlDbType.Int, I(row, "NUM_ACCION")),
                P("@TIPO_ACCION", SqlDbType.NVarChar, 10, S(row, "TIPO_ACCION")),
                P("@PATH_HIDRA", SqlDbType.NVarChar, 255, S(row, "PATH_HIDRA")),
                P("@NUM_SEQ", SqlDbType.Int, I(row, "NUM_SEQ")),
                P("@DI_ID", SqlDbType.Int, I(row, "DI_ID")));
        }

        private void InsertConectorAcc(DataRow row)
        {
            Execute(Sql.InsertConectorAcc,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ID_CONECTOR", SqlDbType.Int, I(row, "ID_CONECTOR")),
                P("@ID_DIAGRAMA", SqlDbType.Int, I(row, "ID_DIAGRAMA")),
                P("@NUM_CONECTOR", SqlDbType.Int, I(row, "NUM_CONECTOR")),
                P("@NUM_SEQ_DESDE", SqlDbType.Int, I(row, "NUM_SEQ_DESDE")),
                P("@NUM_SEQ_HASTA", SqlDbType.Int, I(row, "NUM_SEQ_HASTA")),
                P("@CAT_CONECTOR", SqlDbType.NVarChar, 100, S(row, "CAT_CONECTOR")),
                P("@IND_SALIDA_TRAM", SqlDbType.NVarChar, 1, S(row, "IND_SALIDA_TRAM")),
                P("@DI_ID", SqlDbType.Int, I(row, "DI_ID")));
        }

        private void InsertParamAcc(DataRow row)
        {
            Execute(Sql.InsertParamAcc,
                P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, S(row, "PROCEDIMIENTO")),
                P("@ORDEN_N1", SqlDbType.Int, I(row, "ORDEN_N1")),
                P("@ORDEN_N2", SqlDbType.Int, I(row, "ORDEN_N2")),
                P("@ORDEN_N3", SqlDbType.Int, I(row, "ORDEN_N3")),
                P("@ORDEN_N4", SqlDbType.Int, I(row, "ORDEN_N4")),
                P("@ORDEN_N5", SqlDbType.Int, I(row, "ORDEN_N5")),
                P("@ORDEN_ACC", SqlDbType.Int, I(row, "ORDEN_ACC")),
                P("@ID_ACCION", SqlDbType.Int, I(row, "ID_ACCION")),
                P("@PARAMETRO", SqlDbType.NVarChar, 255, S(row, "PARAMETRO")),
                P("@VALOR", SqlDbType.NVarChar, 4000, S(row, "VALOR")),
                P("@ORDEN_PA", SqlDbType.Int, I(row, "ORDEN_PA")));
        }

        private int Execute(string sql, params SqlParameter[] parameters)
        {
            Logger.Info("Ejecutamos PASARELA query: " + sql);
            return _db.Execute(sql, parameters);
        }

        private static void InsertarTabla(DataTable table, Action<DataRow> insert)
        {
            if (table == null) return;

            foreach (DataRow row in table.Rows)
                insert(row);
        }

        private static SqlParameter ProcedimientoParam(string procedimiento)
        {
            return P("@PROCEDIMIENTO", SqlDbType.NVarChar, 50, procedimiento);
        }

        private static SqlParameter P(string name, SqlDbType type, object value) => DbHelpers.P(name, type, value);
        private static SqlParameter P(string name, SqlDbType type, int size, object value) => DbHelpers.P(name, type, size, value);
        private static object S(DataRow row, string columnName) => DbHelpers.GetStringOrDbNull(row, columnName);
        private static object I(DataRow row, string columnName) => DbHelpers.GetIntOrDbNull(row, columnName);

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
