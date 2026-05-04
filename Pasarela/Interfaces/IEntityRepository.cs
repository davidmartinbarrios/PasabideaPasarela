using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;

namespace Lantik.Pasarela.Interfaces
{
    public interface IEntityRepository
    {
        ResponseBase<IList<Entity>> GetAll();
    }
}
