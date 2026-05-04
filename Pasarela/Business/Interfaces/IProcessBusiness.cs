using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IProcessBusiness
    {
        ResponseBase<IList<Process>> ObtenerPorModelo(string ModelName);
    }
}
