using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Text_FieldBusiness : IText_Field
    {
        public ResponseBase<IList<Text_Field>> GetByAttributeAndID(string AnoTabnr, int ANO_ID, int attribute)
        {
            ResponseBase<IList<Text_Field>> businessResponse;
            businessResponse = new Text_FieldRepository().GetByAttributeAndID(AnoTabnr, ANO_ID, attribute);
            return businessResponse;
        }
    }
}
