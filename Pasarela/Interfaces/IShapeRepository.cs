using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;


namespace Lantik.Pasarela.Interfaces
{
    public interface IShapeRepository
    {
        ResponseBase<IList<Shape>> GetByModelNameAndID(string ModelName, int DI_ID);
    }
}
