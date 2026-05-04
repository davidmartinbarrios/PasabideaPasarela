using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IConectores_DIBusiness
    {
        ResponseBase<IList<Conectores_DI>> ObtenerPorIDyModelo(string ModelName, int DI_ID);
        ResponseBase<int> Insertar(Conectores_DI business);
        ResponseBase<bool> Borrar(int IdDiagram);
    }
}
