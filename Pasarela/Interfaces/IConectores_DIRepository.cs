using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IConectores_DIRepository
    {
        ResponseBase<IList<Conectores_DI>> GetByModelNameAndID(string ModelName, int DI_ID);

        ResponseBase<bool> Delete(int IdDiagrama);
        ResponseBase<int> Insert(Conectores_DI obj);
    }
}
