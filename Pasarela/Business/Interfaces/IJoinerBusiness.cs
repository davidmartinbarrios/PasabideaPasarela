using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IJoinerBusiness
    {
        ResponseBase<IList<Joiner>> ObtenerPorIDyModelo(string ModelName, int DI_ID);
    }
}
