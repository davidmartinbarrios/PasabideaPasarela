using System;
using System.Data;
using System.Data.SqlClient;
using Lantik.Pasabidea.Core.Helpers;

namespace Lantik.Pasabidea.Core.Data
{
    internal sealed partial class ErwinPasarelaArtezData : IDisposable
    {
        private readonly DbContext _db = DbContext.Get("BD_DP4");

        public DataTable LeerDiagramas(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.Diagrama, CommonParams(diId, procedimiento, modelName));
        }

        public DataTable LeerConectoresDi(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.ConectoresDi, CommonParams(diId, procedimiento, modelName));
        }

        public DataTable LeerPropiedadesDi(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.PropiedadesDi, CommonParams(diId, procedimiento, modelName));
        }

        public DataTable LeerAccionesDi(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.AccionesDi, CommonParams(diId, procedimiento, modelName));
        }

        public DataTable LeerConectoresAcc(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.ConectoresAcc, CommonParams(diId, procedimiento, modelName));
        }

        public DataTable LeerParamAcc(int diId, string procedimiento, string modelName)
        {
            return Query(Sql.ParamAcc, CommonParams(diId, procedimiento, modelName));
        }

        public string ObtenerCodigoProcedimiento(string modelName, int diId)
        {
            return ObtenerPropiedadObjetoDiagrama(modelName, diId, "Procedimiento", "CÓDIGOPROCEDIMIENTO");
        }

        public string ObtenerPropiedadObjetoDiagrama(string modelName, int diId, string tipoObjeto, string scriptNamePropiedad)
        {
            object result = _db.Scalar(
                Sql.PropiedadObjetoDiagrama,
                DbHelpers.P("@MODEL_NAME", SqlDbType.NVarChar, 100, modelName),
                DbHelpers.P("@DI_ID", SqlDbType.Int, diId),
                DbHelpers.P("@TIPO_OBJETO", SqlDbType.NVarChar, 200, tipoObjeto),
                DbHelpers.P("@SCRIPTNAME_PROPIEDAD", SqlDbType.NVarChar, 500, scriptNamePropiedad));

            return result == null || result == DBNull.Value
                ? null
                : Convert.ToString(result).Trim();
        }

        private DataTable Query(string sql, params SqlParameter[] parameters)
        {
            Logger.Info("Ejecutamos ERWIN query: " + sql);
            return _db.QueryTable(sql, parameters);
        }

        private static SqlParameter[] CommonParams(int diId, string procedimiento, string modelName)
        {
            return new[]
            {
                DbHelpers.P("@P_MODEL_NAME", SqlDbType.NVarChar, 100, modelName),
                DbHelpers.P("@P_DI_ID", SqlDbType.Int, diId),
                DbHelpers.P("@P_PROCEDIMIENTO", SqlDbType.NVarChar, 255, procedimiento)
            };
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
