using System;
using System.Diagnostics;
using Lantik.Pasabidea.Core.Helpers;
using Lantik.Pasabidea.Core.Data;

namespace Lantik.Pasabidea.Core.Business
{
    /// <summary>
    /// Orquesta la transformación ERWIN/Evolve -> PASARELA_ARTEZ.
    /// </summary>
    public sealed class ErwinPasarelaArtezBusiness : IDisposable
    {
        private const string DefaultModelName = "ARTEZELI";

        private readonly ErwinPasarelaArtezData _erwinData = new ErwinPasarelaArtezData();
        private readonly PasarelaArtezData _pasarelaData = new PasarelaArtezData();

        public int GenerarDesdeDiId(int diId, string procedimiento, string modelName = DefaultModelName)
        {
            if (diId <= 0)
                throw new ArgumentException("El DI_ID no es válido.", nameof(diId));

            if (string.IsNullOrWhiteSpace(procedimiento))
                throw new ArgumentException("El procedimiento es obligatorio.", nameof(procedimiento));

            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("El modelo es obligatorio.", nameof(modelName));

            try
            {
                _pasarelaData.BeginTransaction();

                _pasarelaData.DeleteByProcedimiento(procedimiento);

                _pasarelaData.InsertDiagramas(_erwinData.LeerDiagramas(diId, procedimiento, modelName));
                _pasarelaData.InsertConectoresDi(_erwinData.LeerConectoresDi(diId, procedimiento, modelName));
                _pasarelaData.InsertPropiedadesDi(_erwinData.LeerPropiedadesDi(diId, procedimiento, modelName));
                _pasarelaData.InsertAccionesDi(_erwinData.LeerAccionesDi(diId, procedimiento, modelName));
                _pasarelaData.InsertConectoresAcc(_erwinData.LeerConectoresAcc(diId, procedimiento, modelName));
                _pasarelaData.InsertParamAcc(_erwinData.LeerParamAcc(diId, procedimiento, modelName));

                _pasarelaData.Commit();

                return 1;
            }
            catch (Exception ex)
            {
                _pasarelaData.Rollback();
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
        }

        public string ObtenerCodigoProcedimiento(string modelName, int diId)
        {
            if (diId <= 0)
                throw new ArgumentException("El DI_ID no es válido.", nameof(diId));

            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("El modelo es obligatorio.", nameof(modelName));

            return _erwinData.ObtenerCodigoProcedimiento(modelName.Trim(), diId);
        }

        public void Dispose()
        {
            _erwinData?.Dispose();
            _pasarelaData?.Dispose();
        }
    }
}
