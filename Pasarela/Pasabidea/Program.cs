using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Pasabidea;
using Pasabidea;
using System;
using System.Windows.Forms;

namespace App
{
    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            // Log application start
            Logger.Info("---= Inicio de la aplicación =---");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // Perform cleanup tasks here
            Logger.Info("---= Final de la aplicación =---");
        }
    }
}
