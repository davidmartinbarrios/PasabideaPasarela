using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace Lantik.Pasarela.Helpers
{
    public static class TraceHelper
    {
        private static readonly ConcurrentDictionary<string, string> _methodCache = new ConcurrentDictionary<string, string>();

        public static string StackTracePath(StackTrace stackTrace)
        {
            if (stackTrace == null)
                return "(stackTrace=null)";

            var frame = stackTrace.GetFrame(0);
            if (frame == null)
                return "(frame=null)";

            var method = frame.GetMethod();
            if (method == null)
                return "(method=null)";

            try
            {
                var key = GetMethodKey(method);
                return _methodCache.GetOrAdd(key, _ => FormatMethod(method));
            }
            catch
            {
                return "(exception generating method info)";
            }
        }

        private static string GetMethodKey(MethodBase method)
        {
            try
            {
                // ModuleVersionId es único por ensamblado cargado
                var moduleId = method.Module.ModuleVersionId.ToString();
                var token = method.MetadataToken;
                return $"{moduleId}:{token}";
            }
            catch
            {
                // Fallback genérico si no hay metadata
                return $"[unknown]:{method.Name}";
            }
        }

        private static string FormatMethod(MethodBase method)
        {
            var declaringType = method.DeclaringType;

            string namespaceName = declaringType?.Namespace ?? "(unknown_namespaceName)";
            string className = declaringType?.Name ?? "(unknown_className)";
            string methodName = method.Name ?? "(unknown_methodName)";

            string paramString;
            try
            {
                var parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    paramString = "()";
                }
                else
                {
                    var paramDetails = parameters
                        .Select(p => $"{p.ParameterType?.Name ?? "(unknown_type)"} {p.Name}")
                        .ToArray();

                    paramString = $"({string.Join(", ", paramDetails)})";
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(
                    $"{TraceHelper.StackTracePath(new StackTrace())}",
                    $"No se pudieron obtener los parámetros del método {methodName}: {ex.Message}"
                );
                paramString = "(unknown_params)";
            }

            return $"{namespaceName}.{className}.{methodName}{paramString}";
        }

    }
}
