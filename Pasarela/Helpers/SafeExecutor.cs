using System;
using System.Diagnostics;
using System.Web.UI;

namespace Lantik.Pasarela.Helpers
{
    public static class SafeExecutor
    {
        public static void SafeExecution(Action action, string contextInfo = null)
        {
            var stackTrace = $"{contextInfo ?? TraceHelper.StackTracePath(new StackTrace())}";
            Logger.Info($"Inicio {stackTrace}");
            try
            {
                action();
            }
            catch (ArgumentException ex)
            {
                Logger.Error(stackTrace, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(stackTrace, ex);
            }
            finally
            {
                Logger.Info($"Fin {stackTrace}");
            }
        }
    }
}


