using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pasabidea.Business.Models;

namespace Pasabidea.Interfaces.Services
{
    public interface IFlujosBusinessService
    {
        Task<IReadOnlyList<FlujoResumen>> ObtenerFlujosResumenAsync(
            string patronBaseDatos,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
