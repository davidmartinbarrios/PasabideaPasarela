using System.Data;
using Lantik.Pasabidea.Core.Data;

namespace Lantik.Pasabidea.Core.Business
{
    public sealed class ModelosBusiness
    {
        public DataTable ObtenerListadoModelosArtez()
        {
            using (var data = new ModelosData())
            {
                return data.ObtenerListadoModelosArtez();
            }
        }
    }
}