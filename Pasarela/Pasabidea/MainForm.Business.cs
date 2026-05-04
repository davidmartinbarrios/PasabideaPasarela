using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasabidea
{
    public partial class MainForm
    {
        private List<string> ObtenerNombresBDMugi()
        {
            return ConfigurationManager
                .ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .Where(cs => cs != null
                             && !string.IsNullOrWhiteSpace(cs.Name)
                             && cs.Name.StartsWith("BD_MUGI_"))
                .Select(cs => cs.Name.Substring("BD_MUGI_".Length))
                .ToList();
        }

        private void CargarComboBDMugi()
        {
            List<string> nombres = ObtenerNombresBDMugi();

            cmbBDMugi.DataSource = null;
            cmbBDMugi.Items.Clear();
            cmbBDMugi.DataSource = nombres;
        }

        //--
        private void InitializeBusiness()
        {
            this.Shown += async (s, e) => await InitializeWebViewAsync(); // Carga de los componentes enriquecidos locales
            CargarComboBDMugi();
        }

        private async Task InitializeWebViewAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);

            string folder = AppDomain.CurrentDomain.BaseDirectory;

            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "app.local",
                folder,
                CoreWebView2HostResourceAccessKind.Allow
            );

            webView21.CoreWebView2.Navigate("https://app.local/base/index.html");
        }

    }
}
