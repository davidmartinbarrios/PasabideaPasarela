using Lantik.Pasabidea.Core.Data;
using Pasabidea.Core.Entities.POCOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasabidea.Core.Data.sqlRepository
{
    internal sealed partial class XMLsqlRepository : IDisposable
    {
        private readonly DbContext _db = DbContext.Get("BD_N8");
        private readonly string _ProcedureName = "SPN8ADGrabarConfiguracionBorradorProcedimientoBLT";
        public void BeginTransaction() => _db.BeginTransaction();
        public void Commit() => _db.Commit();
        public void Rollback() => _db.Rollback();


        public SP_Response GrabarConfiguracionBorrador(string XML, out int sqlCode, out string sqlMessage)
        {
            var pSQLCode = new SqlParameter("@SQLCode_OUT", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var pSQLState = new SqlParameter("@SQLState_OUT", SqlDbType.NVarChar, 10)
            {
                Direction = ParameterDirection.Output
            };

            var pSQLMessage = new SqlParameter("@SQLMessage_OUT", SqlDbType.NVarChar, 4000)
            {
                Direction = ParameterDirection.Output
            };

            var result = _db.QueryStoredProcedure(
                _ProcedureName,
                DbContext.Param("@pCodigo_Usuario_Conectado_IN", SqlDbType.NVarChar, 128, "CC0032"),
                DbContext.Param("@pConfiguracionVersionProcedimientoNegocio_IN", SqlDbType.NVarChar, XML),
                DbContext.Param("@pTipoCreacionConfiguracion_IN", SqlDbType.NVarChar, 128, "00"),
                DbContext.Param("@pObligacionCreacion_IN", SqlDbType.Int, 1),
                pSQLCode,
                pSQLState,
                pSQLMessage
            );

            sqlCode = pSQLCode.Value != DBNull.Value ? (int)pSQLCode.Value : 0;
            sqlMessage = pSQLMessage.Value?.ToString();

            return new SP_Response
            {
                Data = result,
                SQLCode = sqlCode,
                SQLMessage = sqlMessage
            };
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
