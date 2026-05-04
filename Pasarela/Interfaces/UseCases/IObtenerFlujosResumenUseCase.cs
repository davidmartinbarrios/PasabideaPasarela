using Pasabidea.Application.Dtos;
using Pasabidea.Application.UseCases;

namespace Pasabidea.Interfaces.UseCases;

public interface IObtenerFlujosResumenUseCase
{
    Task<IReadOnlyList<FlujoResumenDto>> ExecuteAsync(
        ObtenerFlujosResumenRequest request,
        CancellationToken cancellationToken = default);
}
