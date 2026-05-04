using Pasabidea.Business.Models;

namespace Pasabidea.Interfaces.Repositories;

public interface IFlujosRepository
{
    Task<IReadOnlyList<FlujoResumen>> ObtenerUltimasVersionesAsync(
        string patronBaseDatos,
        CancellationToken cancellationToken = default);
}
