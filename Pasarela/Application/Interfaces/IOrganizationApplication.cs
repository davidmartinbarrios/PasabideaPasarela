using Lantik.Pasarela.Application.DTOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.Interfaces
{
    public interface IOrganizationApplication
    {
        ResponseBaseDTO<IList<OrganizationDTO>> ObtenerPorModelo(string ModelName);
    }
}
