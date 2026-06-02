using System;
using System.Collections.Generic;
using System.Data;
using Lantik.Pasabidea.Core.Helpers;
using Lantik.Pasabidea.Core.Models.Mugi;

namespace Lantik.Pasabidea.Core.Data.Mugi
{
    /// <summary>
    /// Data de MUGI para wfFlows y wfFlowActions.
    /// Encapsula DbContext, conexión, transacción, insert/delete y commit/rollback.
    /// </summary>
    internal sealed class MugiWfFlowData : IDisposable
    {
        private readonly DbContext _db;

        public MugiWfFlowData(string connectionStringName = "BD_MUGI_DBN8POCARTEZ")
        {
            _db = DbContext.Get(connectionStringName);
        }

        public int GrabarFlujo(
            MugiWfFlow flow,
            IEnumerable<MugiWfFlowAction> actions,
            bool sobrescribir,
            bool dryRun)
        {
            if (flow == null)
                throw new ArgumentNullException(nameof(flow));

            if (actions == null)
                throw new ArgumentNullException(nameof(actions));

            var rowsInserted = 0;

            try
            {
                _db.BeginTransaction(IsolationLevel.ReadCommitted);

                if (sobrescribir)
                {
                    Logger.Info("Eliminamos versión previa MUGI. Flow=" + flow.Flow + ", Version=" + flow.Version);
                    DeleteWfFlowActions(flow.Flow, flow.Version);
                    DeleteWfFlow(flow.Flow, flow.Version);
                }

                InsertWfFlow(flow);

                foreach (var action in actions)
                {
                    InsertWfFlowAction(action);
                    rowsInserted++;
                }

                if (dryRun)
                {
                    Logger.Info("DryRun activo. Rollback MUGI. Flow=" + flow.Flow + ", Version=" + flow.Version);
                    _db.Rollback();
                }
                else
                {
                    _db.Commit();
                }

                return rowsInserted;
            }
            catch
            {
                TryRollback();
                throw;
            }
        }

        public void InsertWfFlow(MugiWfFlow flow)
        {
            Logger.Info("Insertamos wfFlows. Flow=" + flow.Flow + ", Version=" + flow.Version);

            _db.Execute(
                Sql.InsertWfFlow,
                DbHelpers.P("@Flow", SqlDbType.NVarChar, 100, flow.Flow),
                DbHelpers.P("@Version", SqlDbType.Int, flow.Version),
                DbHelpers.P("@Active", SqlDbType.NVarChar, 1, flow.Active),
                DbHelpers.P("@FlowName", SqlDbType.NVarChar, 255, flow.FlowName),
                DbHelpers.P("@Comments", SqlDbType.NVarChar, 1000, flow.Comments),
                DbHelpers.P("@Running", SqlDbType.NVarChar, 1, flow.Running),
                DbHelpers.P("@Start", SqlDbType.NVarChar, 8, flow.Start),
                DbHelpers.P("@StopOlderVersions", SqlDbType.NVarChar, 1, flow.StopOlderVersions));
        }

        public void InsertWfFlowAction(MugiWfFlowAction action)
        {
            _db.Execute(
                Sql.InsertWfFlowAction,
                DbHelpers.P("@Flow", SqlDbType.NVarChar, 100, action.Flow),
                DbHelpers.P("@Version", SqlDbType.Int, action.Version),
                DbHelpers.P("@FlowOrder", SqlDbType.Int, action.FlowOrder),
                DbHelpers.P("@Id", SqlDbType.Int, action.Id),
                DbHelpers.P("@Action", SqlDbType.NVarChar, 100, action.Action),
                DbHelpers.P("@Path", SqlDbType.NVarChar, 255, action.Path),
                DbHelpers.P("@Param", SqlDbType.NVarChar, 255, action.Param),
                DbHelpers.P("@Value", SqlDbType.NVarChar, 4000, action.Value),
                DbHelpers.P("@Comments", SqlDbType.NVarChar, 1000, action.Comments));
        }

        public void DeleteWfFlowActions(string flow, int version)
        {
            _db.Execute(
                Sql.DeleteWfFlowActions,
                DbHelpers.P("@Flow", SqlDbType.NVarChar, 100, flow),
                DbHelpers.P("@Version", SqlDbType.Int, version));
        }

        public void DeleteWfFlow(string flow, int version)
        {
            _db.Execute(
                Sql.DeleteWfFlow,
                DbHelpers.P("@Flow", SqlDbType.NVarChar, 100, flow),
                DbHelpers.P("@Version", SqlDbType.Int, version));
        }

        private void TryRollback()
        {
            try
            {
                _db.Rollback();
            }
            catch
            {
                // No ocultar el error original.
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        private static class Sql
        {
            public const string InsertWfFlow = @"
INSERT INTO dbo.wfFlows
(
    Flow,
    Version,
    Active,
    FlowName,
    Comments,
    Running,
    Start,
    StopOlderVersions
)
VALUES
(
    @Flow,
    @Version,
    @Active,
    @FlowName,
    @Comments,
    @Running,
    @Start,
    @StopOlderVersions
);";

            public const string InsertWfFlowAction = @"
INSERT INTO dbo.wfFlowActions
(
    Flow,
    Version,
    FlowOrder,
    Id,
    Action,
    Path,
    Param,
    Value,
    Comments
)
VALUES
(
    @Flow,
    @Version,
    @FlowOrder,
    @Id,
    @Action,
    @Path,
    @Param,
    @Value,
    @Comments
);";

            public const string DeleteWfFlowActions = @"
DELETE FROM dbo.wfFlowActions
WHERE Flow = @Flow
  AND Version = @Version;";

            public const string DeleteWfFlow = @"
DELETE FROM dbo.wfFlows
WHERE Flow = @Flow
  AND Version = @Version;";
        }
    }
}
