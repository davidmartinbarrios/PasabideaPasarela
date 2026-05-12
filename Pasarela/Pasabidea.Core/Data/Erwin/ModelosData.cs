using System;
using System.Data;

namespace Lantik.Pasabidea.Core.Data
{
    internal sealed class ModelosData : IDisposable
    {
        private readonly DbContext _db = DbContext.Get("BD_DP4");

        public DataTable ObtenerListadoModelosArtez()
        {
            return _db.QueryStoredProcedure("SPPasarelaObtenerListadoModelosARTEZ");
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}