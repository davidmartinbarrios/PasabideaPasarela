using System;
using System.Data;

namespace Lantik.Pasabidea.Core.Data
{
    internal sealed class ProcedimientosData : IDisposable
    {
        private readonly DbContext _db = DbContext.Get("BD_DP4");

        public ProcedimientosData()
        {
            _db.CommandTimeout = 120;
        }

        public DataTable ObtenerArbolProcedimientos(string modelName)
        {
            return _db.QueryStoredProcedure(
                "dbo.SPPasarelaObtenerArbolProcedimientos",
                DbContext.Param("@ModelName", SqlDbType.NVarChar, 128, modelName)
            );
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}