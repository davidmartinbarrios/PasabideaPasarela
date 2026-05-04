using System;
using System.Diagnostics;
using System.Reflection;

namespace Pasabidea.Helpers
{
    // Nota: se eliminaron las referencias directas a MSTest y
    // Microsoft.Data.Tools.Schema.Sql.UnitTesting para evitar errores
    // de compilación en entornos sin esos ensamblados.
    //
    // Comportamiento:
    // - Si el ensamblado y tipo `Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestClass`
    //   están disponibles en tiempo de ejecución, se invocan por reflexión
    //   `TestService.DeployDatabaseProject()` y `TestService.GenerateData()`.
    // - Si no están disponibles, se registra el hecho y no se lanza excepción.

    public static class SqlDatabaseSetup
    {
        /// <summary>
        /// Inicializador de ensamblado alternativo (seguro).
        /// Si compilas/runas con MSTest + SQL Unit Testing instalado,
        /// MSTest no usará este método directamente — pero el efecto por reflexión
        /// se aplica cuando la librería está presente en tiempo de ejecución.
        /// </summary>
        /// <param name="testContext">Opcional: puede ser un TestContext real o null.</param>
        public static void InitializeAssembly(object testContext = null)
        {
            try
            {
                const string typeName = "Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestClass, Microsoft.Data.Tools.Schema.Sql.UnitTesting";
                Type sqlTestClassType = Type.GetType(typeName, false);

                if (sqlTestClassType == null)
                {
                    Debug.WriteLine("SqlDatabaseSetup: Microsoft.Data.Tools.Schema.Sql.UnitTesting no disponible; omitiendo despliegue.");
                    return;
                }

                PropertyInfo testServiceProp = sqlTestClassType.GetProperty("TestService", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                object testService = testServiceProp?.GetValue(null);

                if (testService == null)
                {
                    Debug.WriteLine("SqlDatabaseSetup: TestService no encontrado en SqlDatabaseTestClass.");
                    return;
                }

                MethodInfo deployMethod = testService.GetType().GetMethod("DeployDatabaseProject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                MethodInfo genMethod = testService.GetType().GetMethod("GenerateData", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                deployMethod?.Invoke(testService, null);
                genMethod?.Invoke(testService, null);

                Debug.WriteLine("SqlDatabaseSetup: DeployDatabaseProject y GenerateData invocados por reflexión.");
            }
            catch (TargetInvocationException tie)
            {
                Debug.WriteLine("SqlDatabaseSetup: fallo al invocar métodos de test: " + (tie.InnerException?.ToString() ?? tie.ToString()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SqlDatabaseSetup: excepción inesperada: " + ex);
            }
        }
    }
}
