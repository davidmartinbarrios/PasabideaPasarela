using System.Data;
using Lantik.Pasabidea.Core.Business;
using Lantik.Pasabidea.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pasabidea.Test
{
    [TestClass]
    public class ModelosBusinessTest
    {
        [TestMethod]
        public void LeerDiagramas_Deberia_Devolver_Registros()
        {
            int diId = 3731;
            string procedimiento = "PA999910";
            string modelName = "ARTEZELI";

            using (var data = new ErwinPasarelaArtezData())
            {
                DataTable result = data.LeerDiagramas(diId, procedimiento, modelName);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Rows.Count > 0, "No se han devuelto registros.");
                Assert.IsTrue(result.Columns.Contains("PROCEDIMIENTO"), "Falta la columna PROCEDIMIENTO.");
                Assert.IsTrue(result.Columns.Contains("DI_ID"), "Falta la columna DI_ID.");
            }
        }


        [TestMethod]
        public void ObtenerListadoModelosArtez_DebeDevolverModelos()
        {
            var business = new ModelosBusiness();

            DataTable result = business.ObtenerListadoModelosArtez();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Rows.Count > 0, "No se han recuperado modelos ARTEZ.");
        }

        [TestMethod]
        public void ObtenerArbolProcedimientos_DebeDevolverDatos()
        {
            string modelName = "ARTEZELI";

            using (var data = new ProcedimientosData())
            {
                DataTable result = data.ObtenerArbolProcedimientos(modelName);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Rows.Count > 0, "No se han recuperado procedimientos.");
            }
        }
      
        [TestMethod]
        public void ObtenerCodigoProcedimiento_DebeDevolverCodigo()
        {
            string modelName = "ARTEZELI";
            int diId = 3731;

            using (var business = new ErwinPasarelaArtezBusiness())
            {
                string codigo = business.ObtenerCodigoProcedimiento(modelName, diId);

                Assert.IsFalse(string.IsNullOrWhiteSpace(codigo));
            }
        }

        [TestMethod]
        public void GenerarDesdeDiId_DebeGenerarDatosEnPasarelaArtez()
        {
            int diId = 3731;
            string procedimiento = "PA999910";
            string modelName = "ARTEZELI";

            using (var business = new ErwinPasarelaArtezBusiness())
            {
                int result = business.GenerarDesdeDiId(diId, procedimiento, modelName);

                Assert.AreEqual(1, result);
            }
        }
    }
}