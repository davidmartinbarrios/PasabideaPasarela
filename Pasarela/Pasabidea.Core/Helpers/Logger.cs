using log4net;
using System;
using System.Collections.Generic;

namespace Lantik.Pasabidea.Core.Data
{
    public class Logger
    {
        #region Log4Net
        // INFO: Si hay que añadir o modificar appenders, basta con meterlos aquí para que se registren en todas partes.
        private static readonly IList<ILog> loggers = new List<ILog>
        {
            LogManager.GetLogger("TraceAppender"),
            LogManager.GetLogger("RollingFileAppender")
        };
        private static void LogMessage(Action<ILog> logAction)
        {
            if (loggers == null)
            {
                System.Diagnostics.Debug.WriteLine("No loggers available to log the message.");
                return;
            }

            foreach (var logger in loggers)
                try
                {
                    logAction(logger);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en logger {logger.Logger.Name}: {ex.Message}");
                }
        }



        public static void Error(string message, Exception ex = null)
        {
            LogMessage(logger => logger.Error(message, ex));
        }
        public static void Error(string path, string message)
        {
            Logger.Error($"{path}:{Environment.NewLine}{message}");
        }

        public static void Debug(string message, Exception ex = null)
        {
            LogMessage(logger => logger.Debug(message, ex));
        }
        public static void Debug(string path, string message)
        {
            Logger.Debug($"{path}:{Environment.NewLine}{message}");
        }

        public static void Fatal(string message, Exception ex = null)
        {
            LogMessage(logger => logger.Fatal(message, ex));
        }
        public static void Fatal(string path, string message)
        {
            Logger.Fatal($"{path}:{Environment.NewLine}{message}");
        }

        public static void Info(string message, Exception ex = null)
        {
            LogMessage(logger => logger.Info(message, ex));
        }
        public static void Info(string path, string message)
        {
            Logger.Info($"{path}:{Environment.NewLine}{message}");
        }

        public static void Warning(string message, Exception ex = null)
        {
            LogMessage(logger => logger.Warn(message, ex));
        }
        public static void Warning(string path, string message)
        {
            Logger.Warning($"{path}:{Environment.NewLine}{message}");
        }
        #endregion
    }
}
