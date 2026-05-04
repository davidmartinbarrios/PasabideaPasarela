using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Cw_Prop_TypeBusinnes: ICw_Prop_TypeBusinnes
    {
        public ResponseBase<IList<Cw_Prop_Type>> ObtenerPorModelo(string ModelName)
        {
            ResponseBase<IList<Cw_Prop_Type>> businessResponse;
            businessResponse = new Cw_Prop_TypeRepository().GetByModelName(ModelName);
            return businessResponse;
        }
    }
}
