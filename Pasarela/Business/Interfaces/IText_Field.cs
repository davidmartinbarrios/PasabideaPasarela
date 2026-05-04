using System.Collections.Generic;
using Lantik.Pasarela.Entities.POCOs;

namespace Lantik.Pasarela.Business.Interfaces
{
    public interface IText_Field
    {
        ResponseBase<IList<Text_Field>> GetByAttributeAndID(string AnoTabnr, int ANO_ID, int attribute);
    }
}
