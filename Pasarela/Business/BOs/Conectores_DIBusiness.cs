using Lantik.Pasarela.Business.Interfaces;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.sqlRepository;
using System;
using System.Collections.Generic;

namespace Lantik.Pasarela.Business.BOs
{
    public class Conectores_DIBusiness : IConectores_DIBusiness
    {
        public ResponseBase<IList<Conectores_DI>> ObtenerPorIDyModelo(string ModelName, int DI_ID)
        {
            ResponseBase<IList<Conectores_DI>> businessResponse;
            businessResponse = new Conectores_DIRepository().GetByModelNameAndID(ModelName, DI_ID);
            return businessResponse;
        }
        public ResponseBase<int> Insertar(Conectores_DI business)
        {
            return new Conectores_DIRepository().Insert(business);
        }

        public ResponseBase<bool> Borrar(int IdDiagram)
        {
            return new Conectores_DIRepository().Delete(IdDiagram);
        }
    }
}
