using Lantik.Pasarela.Entities.POCOs;
using System.Collections.Generic;


namespace Lantik.Pasarela.Interfaces
{
    public interface IText_FieldRepository
    {
        ResponseBase<IList<Text_Field>> GetByAttributeAndID(string AnoTabnr, int ANO_ID, int attribute);
    }
}
