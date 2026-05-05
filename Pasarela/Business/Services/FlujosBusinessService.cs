using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pasabidea.Business.Models;
using Pasabidea.Interfaces.Repositories;
using Pasabidea.Interfaces.Services;

namespace Pasabidea.Business.Services
{
    public sealed class FlujosBusinessService : IFlujosBusinessService
    {
        private readonly IFlujosRepository _repository;

        public FlujosBusinessService(IFlujosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<FlujoResumen>> ObtenerFlujosResumenAsync(
            string patronBaseDatos,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(patronBaseDatos))
            {
                patronBaseDatos = "DBN8%";
            }

            return await _repository.ObtenerUltimasVersionesAsync(
                patronBaseDatos,
                cancellationToken);
        }
    }
}
