using System.Data;
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
            string modelName = "BIDERAT1";

            using (var data = new ErwinPasarelaArtezData())
            {
                DataTable result = data.LeerDiagramas(diId, procedimiento, modelName);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Rows.Count > 0, "No se han devuelto registros.");
                Assert.IsTrue(result.Columns.Contains("PROCEDIMIENTO"), "Falta la columna PROCEDIMIENTO.");
                Assert.IsTrue(result.Columns.Contains("DI_ID"), "Falta la columna DI_ID.");
            }
        }
    }
}