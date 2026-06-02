using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Lantik.Pasabidea.Core.Entities;
using Lantik.Pasabidea.Core.Models.Mugi;

namespace Lantik.Pasabidea.Core.Data.Mugi
{
    public sealed class MugiFlowActionData
    {
        public async Task InsertAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            MugiWfFlowAction action,
            CancellationToken cancellationToken)
        {
            const string sql = @"
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

            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@Flow", action.Flow);
                command.Parameters.AddWithValue("@Version", action.Version);
                command.Parameters.AddWithValue("@FlowOrder", action.FlowOrder);
                command.Parameters.AddWithValue("@Id", action.Id);
                command.Parameters.AddWithValue("@Action", (object)action.Action ?? string.Empty);
                command.Parameters.AddWithValue("@Path", (object)action.Path ?? string.Empty);
                command.Parameters.AddWithValue("@Param", (object)action.Param ?? string.Empty);
                command.Parameters.AddWithValue("@Value", (object)action.Value ?? string.Empty);
                command.Parameters.AddWithValue("@Comments", (object)action.Comments ?? string.Empty);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task DeleteByFlowVersionAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            string flow,
            int version,
            CancellationToken cancellationToken)
        {
            const string sql = @"
DELETE FROM dbo.wfFlowActions
WHERE Flow = @Flow
  AND Version = @Version;";

            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@Flow", flow);
                command.Parameters.AddWithValue("@Version", version);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}