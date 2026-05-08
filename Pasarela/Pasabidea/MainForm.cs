using Lantik.Pasabidea;
using Lantik.Pasarela.Application.AOs;
using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Pasabidea.VO;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Pasabidea
{
    public partial class MainForm : Form
    {
        private TaskCompletionSource<bool> _pageLoadedTcs =
            new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        private bool _webView2HooksInstalled = false;

        private static JavaScriptSerializer CreateJsonSerializer()
        {
            return new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeBusiness();
        }

        private async Task InitializeUXFixAsync()
        {
            this.Font = FontManager.DefaultFont;
            ApplyFontToAllControls(this, FontManager.DefaultFont);

            if (webView21.CoreWebView2 == null)
                await webView21.EnsureCoreWebView2Async();

            webView21.CoreWebView2.WebMessageReceived -= CoreWebView2_WebMessageReceived;
            webView21.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private async void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var json = e.WebMessageAsJson;

                var serializer = CreateJsonSerializer();
                var root = serializer.Deserialize<Dictionary<string, object>>(json);

                object typeObject;
                if (root == null ||
                    !root.TryGetValue("type", out typeObject) ||
                    Convert.ToString(typeObject) != "procedure-selected")
                    return;

                object payloadObject;
                var payload = root.TryGetValue("payload", out payloadObject)
                    ? payloadObject as Dictionary<string, object>
                    : null;

                if (payload == null)
                    return;

                int diId = Convert.ToInt32(payload["diId"]);
                string diName = Convert.ToString(payload["diName"]);

                txtDI_ID.Text = diId.ToString();
                cmbProc.Text = diName;

                await ExportarYRenderizarBpmnAsync(diId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error selección procedimiento");
            }
        }

        private async Task ExportarYRenderizarBpmnAsync(int diId)
        {
            string modelName = txtDiagramModelName.Text;

            var resp = new BpmnExportApplication().ExportarBpmnXml(modelName, diId);

            var qr = resp?.Query_Result;
            if (qr != null && !string.IsNullOrWhiteSpace(qr.SQLMessage))
            {
                MessageBox.Show(qr.SQLMessage, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(resp?.Data))
            {
                MessageBox.Show("No se ha devuelto XML BPMN.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string xmlJson = CreateJsonSerializer().Serialize(resp.Data);

            string script = $@"
        window.renderBpmnXml({xmlJson});
    ";

            await webView21.CoreWebView2.ExecuteScriptAsync(script);
        }

        private void ApplyFontToAllControls(Control parent, Font font)
        {
            parent.Font = font;

            foreach (Control c in parent.Controls)
                ApplyFontToAllControls(c, font);
        }

        private void btnCargarFlujos_Click(object sender, EventArgs e)
        {
            try
            {
                MugiLatestWfFlowApplication app = new MugiLatestWfFlowApplication();
                ResponseBaseDTO<IList<MugiLatestWfFlowDTO>> response = app.ObtenerUltimasVersionesDBN8();

                if (response == null)
                {
                    MessageBox.Show("La respuesta es nula.");
                    return;
                }

                if (response.Data == null)
                {
                    MessageBox.Show("No se han recibido datos.");
                    return;
                }

                //dataGridView1.AutoGenerateColumns = true;
                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CargarUltimosFlujos()
        {
            MugiLatestWfFlowApplication app = new MugiLatestWfFlowApplication();
            ResponseBaseDTO<IList<MugiLatestWfFlowDTO>> response = app.ObtenerUltimasVersionesDBN8();

            if (response != null && response.Data != null)
            {
                //dataGridView1.AutoGenerateColumns = true;
                //dataGridView1.DataSource = response.Data;
            }
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                CargarProcedimientosEnCombo();
                CargarModelosEnCombo();

                await InitializeUXFixAsync();

                var baseFolder = Path.Combine(Application.StartupPath, "base");
                var indexFile = Path.Combine(baseFolder, "index.html");

                if (!File.Exists(indexFile))
                {
                    MessageBox.Show($"No se encuentra: {indexFile}");
                    return;
                }

                webView21.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "app.local",
                    baseFolder,
                    CoreWebView2HostResourceAccessKind.Allow
                );

                _pageLoadedTcs = new TaskCompletionSource<bool>(
                    TaskCreationOptions.RunContinuationsAsynchronously);

                webView21.CoreWebView2.NavigationCompleted -= CoreWebView2_NavigationCompleted;
                webView21.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;


                webView21.CoreWebView2.Navigate("https://app.local/index.html");

                //await EsperarCargaPaginaAsync();

                //await CargarProcedimientosEnWebViewAsync();

                
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message, "Error WebView2");
            }
        }

        private void CargarProcedimientosEnCombo()
        {
            var app = new ErwinDiagramApplication();
            var result = app.ObtenerPorModelo(txtDiagramModelName.Text);

            cmbProc.DataSource = result.Data.ToList();
            cmbProc.DisplayMember = "DI_NAME";
            //cmbProc.ValueMember = "DI_ID";

            // Por defeto, seleccionamos por defecto el que empiece por "PA9999" (si existe)
            for (int i = 0; i < cmbProc.Items.Count; i++)
            {
                if (cmbProc.GetItemText(cmbProc.Items[i]).StartsWith("PA9999"))
                {
                    cmbProc.SelectedIndex = i;
                    break;
                }
            }

        }



        private void CargarModelosEnCombo()
        {
            using (var db = DbContext.Get("BD_DP4"))
            {
                DataTable dt = db.QueryStoredProcedure("SPPasarelaObtenerListadoModelosARTEZ");

                cmbModelos.DisplayMember = "DescripcionModelo";
                cmbModelos.ValueMember = "CodigoModelo";
                cmbModelos.DataSource = dt;
            }

            //cmbProc.DataSource = result.Data.ToList();
            //cmbProc.DisplayMember = "DI_NAME";
            ////cmbProc.ValueMember = "DI_ID";

            //// Por defeto, seleccionamos por defecto el que empiece por "PA9999" (si existe)
            //for (int i = 0; i < cmbProc.Items.Count; i++)
            //{
            //    if (cmbProc.GetItemText(cmbProc.Items[i]).StartsWith("PA9999"))
            //    {
            //        cmbProc.SelectedIndex = i;
            //        break;
            //    }
            //}

        }

        public DataTable ObtenerArbolProcedimientos(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("El modelo no puede estar vacío.", nameof(modelName));

            DbContext db = DbContext.Get("BD_DP4"); // nombre de la connectionString en App.config
            db.CommandTimeout = 120;

            return db.QueryStoredProcedure(
                "dbo.SPPasarelaObtenerArbolProcedimientos",
                DbContext.Param("@ModelName", SqlDbType.NVarChar, 128, modelName.Trim())
            );
        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs ev)
        {
            Debug.WriteLine($"NavigationCompleted IsSuccess={ev.IsSuccess}, Status={ev.WebErrorStatus}, Id={ev.NavigationId}");

            if (ev.IsSuccess)
                _pageLoadedTcs.TrySetResult(true);
            else
                _pageLoadedTcs.TrySetException(
                    new Exception($"Error WebView2: {ev.WebErrorStatus}"));
        }

        private async Task EsperarCargaPaginaAsync()
        {
            var finished = await Task.WhenAny(_pageLoadedTcs.Task, Task.Delay(10000));

            if (finished != _pageLoadedTcs.Task)
                throw new TimeoutException("Timeout esperando a que cargue base/index.html.");

            await _pageLoadedTcs.Task;
        }

        //        private async void MainForm_Load(object sender, EventArgs e)
        //        {
        //            try
        //            {
        //                await InitializeUXFixAsync();

        //                CargarUltimosFlujos();

        //                var file = System.IO.Path.Combine(Application.StartupPath, "base", "index.html");
        //                if (!System.IO.File.Exists(file))
        //                {
        //                    MessageBox.Show($"No se encuentra: {file}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }

        //                var uri = new Uri(file).AbsoluteUri; // file:///...

        //                await webView21.EnsureCoreWebView2Async(null);

        //                // Instala hooks 1 sola vez (compatible con SDKs antiguos: sin ConsoleMessageReceived)
        //                if (!_webView2HooksInstalled)
        //                {
        //                    _webView2HooksInstalled = true;

        //                    // Resetea el “loaded” en cada navegación
        //                    //webView21.CoreWebView2.NavigationStarting += (s, ev) =>
        //                    //{
        //                    //    _pageLoadedTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        //                    //};

        //                    webView21.CoreWebView2.NavigationCompleted += (s, ev) =>
        //                    {
        //                        // _pageLoadedTcs.TrySetResult(true);
        //                        if (ev.IsSuccess)
        //                            _pageLoadedTcs.TrySetResult(true);
        //                        else
        //                            _pageLoadedTcs.TrySetException(
        //                                new Exception("Error cargando WebView2: " + ev.WebErrorStatus));
        //                    };

        //                    // Recibir mensajes desde JS (para ver errores/console.*)
        //                    webView21.CoreWebView2.WebMessageReceived += (s, ev) =>
        //                    {
        //                        var msg = ev.TryGetWebMessageAsString();
        //                        Debug.WriteLine("[WebView2] " + msg);
        //                    };

        //                    // Hook console.* -> window.chrome.webview.postMessage(...)
        //                    await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(@"
        //(() => {
        //  const safe = (x) => {
        //    try { return typeof x === 'string' ? x : JSON.stringify(x); }
        //    catch(e) { return String(x); }
        //  };
        //  const send = (type, args) => {
        //    try { window.chrome.webview.postMessage('[' + type + '] ' + args.map(safe).join(' ')); }
        //    catch(e) {}
        //  };
        //  ['log','info','warn','error'].forEach(fn => {
        //    const orig = console[fn];
        //    console[fn] = (...args) => { send(fn, args); orig.apply(console, args); };
        //  });
        //})();");
        //                }

        //                // Navega al viewer BPMN
        //                webView21.CoreWebView2.Navigate(uri);

        //                //await _pageLoadedTcs.Task;
        //                //await CargarProcedimientosEnWebViewAsync();

        //                _pageLoadedTcs = new TaskCompletionSource<bool>(
        //    TaskCreationOptions.RunContinuationsAsynchronously);

        //                webView21.CoreWebView2.Navigate(uri);

        //                await EsperarCargaPaginaAsync();

        //                await CargarProcedimientosEnWebViewAsync();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Error WebView2", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }

        //private async Task EsperarCargaPaginaAsync()
        //{
        //    var completedTask = _pageLoadedTcs.Task;
        //    var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));

        //    var finished = await Task.WhenAny(completedTask, timeoutTask);

        //    if (finished == timeoutTask)
        //        throw new TimeoutException("Timeout esperando a que cargue base/index.html.");
        //}

        private void webView21_Click(object sender, EventArgs e)
        {
        }

    //    private async Task CargarProcedimientosEnWebViewAsync()
    //    {
    //        string modelName = txtDiagramModelName.Text; // "ARTEZ"

    //        var app = new ErwinDiagramApplication();
    //        var result = app.ObtenerPorModelo(modelName);

    //        var data = result.Data.ToList();

    //        string json = CreateJsonSerializer().Serialize(data);

    //        string script = $@"
    //    window.loadProceduresIntoIframe({json});
    //";

    //        await webView21.CoreWebView2.ExecuteScriptAsync(script);
    //    }

        private async Task CargarProcedimientosEnWebViewAsync()
        {
            string modelName = txtDiagramModelName.Text;

            if (string.IsNullOrWhiteSpace(modelName))
                modelName = "ARTEZ";

            var erwinDiagramApplication = new ErwinDiagramApplication();

            var result = erwinDiagramApplication.ObtenerPorModelo(modelName);

            if (result?.Data == null)
            {
                MessageBox.Show("No se han obtenido procedimientos.");
                return;
            }

            var data = result.Data.ToList();

            cmbProc.Items.Clear();
            cmbProc.Items.AddRange(data.Select(x => x.DI_NAME).ToArray());

            string json = CreateJsonSerializer().Serialize(data);

            string script = $@"
        window.loadProceduresIntoIframe({json});
    ";

            await webView21.CoreWebView2.ExecuteScriptAsync(script);
        }

        private void btnVerBpmn_Click(object sender, EventArgs e)
        {

            //ErwinDiagramApplication erwinDiagramApplication = new ErwinDiagramApplication();

            //var result = erwinDiagramApplication.ObtenerPorModelo(txtDiagramModelName.Text); // p.ej. "ARTEZ"

            //cmbProc.Items.AddRange((from x in result.Data.ToList() select x.DI_NAME).ToArray());


            SafeExecutor.SafeExecution(async () =>
            {
                string modelName = txtDiagramModelName.Text; // p.ej. "ARTEZ"

                if (!int.TryParse(txtDI_ID.Text, out var diId))
                {
                    MessageBox.Show("DI_ID inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Logger.Info("Exportando BPMN XML para model=" + modelName + " diId=" + diId);

                var resp = new BpmnExportApplication().ExportarBpmnXml(modelName, diId);

                var qr = resp?.Query_Result;
                if (qr != null && !string.IsNullOrWhiteSpace(qr.SQLMessage))
                {
                    Logger.Error(TraceHelper.StackTracePath(new StackTrace()), qr.SQLMessage);
                    MessageBox.Show(qr.SQLMessage, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(resp?.Data))
                {
                    MessageBox.Show("No se ha devuelto XML BPMN.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // IMPORTANTE: forzar render SIEMPRE en hilo UI
                BeginInvoke(new Action(() =>
                {
                    _ = RenderBpmnXmlInWebView(resp.Data);
                }));

            }, "btnVerBpmn_Click");
        }


        private async Task RenderBpmnXmlInWebView(string bpmnXml)
        {
            if (webView21.CoreWebView2 == null)
                await webView21.EnsureCoreWebView2Async();

            await _pageLoadedTcs.Task;

            string jsArg = ToJsStringLiteral(bpmnXml);

            string script = @"
(async () => {
  try {
    const iframe = document.getElementById('iframeBpmn');
    if (!iframe || !iframe.contentWindow) {
      console.error('iframeBpmn no disponible');
      return false;
    }

    iframe.contentWindow.postMessage({
      type: 'render-bpmn',
      xml: " + jsArg + @"
    }, '*');

    window.dispatchEvent(new Event('resize'));
    await new Promise(r => requestAnimationFrame(r));
    return true;
  } catch (e) {
    console.error('Error enviando BPMN al iframe:', e);
    return false;
  }
})();";

            string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
            Debug.WriteLine("Resultado RenderBpmnXmlInWebView: " + result);
        }

        /*
        private async Task RenderBpmnXmlInWebView(string bpmnXml)
        {
            if (webView21.CoreWebView2 == null)
                await webView21.EnsureCoreWebView2Async();

            // Espera a que index.html/JS esté cargado
            await _pageLoadedTcs.Task;

            // Pasar XML a JS de forma segura
            string jsArg = ToJsStringLiteral(bpmnXml);

            // Llama a window.renderBpmn(xml) y fuerza relayout/repaint justo después
            string script = @"
(async () => {
  try {
    if (typeof window.renderBpmn !== 'function') {
      console.error('window.renderBpmn no está definido');
      return false;
    }
    await window.renderBpmn(" + jsArg + @");
    window.dispatchEvent(new Event('resize'));
    await new Promise(r => requestAnimationFrame(r));
    return true;
  } catch (e) {
    console.error('Error renderBpmn:', e);
    return false;
  }
})();";

            await webView21.CoreWebView2.ExecuteScriptAsync(script);

            // Repinta el control WinForms (no recarga la página)
            webView21.Refresh();
        }
        */

        // Escapa para literal JS '...'
        private static string ToJsStringLiteral(string s)
        {
            if (s == null) return "''";
            return "'" + s
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", "\\n") + "'";
        }


        //--
        #region Archivo

        private void mnuArchivoNuevoProceso_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Nueva selección de procedimientos.";

            MessageBox.Show(
                "Aquí deberías abrir el formulario de selección de procedimientos o reiniciar el proceso actual.",
                "Nuevo proceso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Ejemplo:
            // using (var frm = new FrmSeleccionProcedimientos())
            // {
            //     frm.MdiParent = this;
            //     frm.Show();
            // }
        }

        private void mnuArchivoAbrirConfiguracion_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Abrir configuración";
                ofd.Filter = "Archivos INI (*.ini)|*.ini|Todos los archivos (*.*)|*.*";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    lblEstado.Text = "Configuración abierta: " + ofd.FileName;

                    MessageBox.Show(
                        "Pendiente enlazar la carga real del fichero de configuración.\r\n\r\n" + ofd.FileName,
                        "Abrir configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void mnuArchivoGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Guardando configuración...";

            MessageBox.Show(
                "Pendiente enlazar el guardado real de la configuración actual.",
                "Guardar configuración",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuArchivoGuardarConfiguracionComo_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Guardar configuración como";
                sfd.Filter = "Archivos INI (*.ini)|*.ini|Todos los archivos (*.*)|*.*";
                sfd.FileName = "Pasarela.ini";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    lblEstado.Text = "Configuración guardada como: " + sfd.FileName;

                    MessageBox.Show(
                        "Pendiente enlazar el guardado real.\r\n\r\nDestino:\r\n" + sfd.FileName,
                        "Guardar configuración como",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void mnuArchivoSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Edición

        private void mnuEdicionSeleccionarTodo_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Seleccionar todo.";

            MessageBox.Show(
                "Aquí deberías seleccionar todos los procedimientos del control activo.",
                "Seleccionar todo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuEdicionDeseleccionarTodo_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Deseleccionar todo.";

            MessageBox.Show(
                "Aquí deberías deseleccionar todos los procedimientos del control activo.",
                "Deseleccionar todo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuEdicionBuscarProcedimiento_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Buscar procedimiento.";

            MessageBox.Show(
                "Aquí deberías abrir la búsqueda de procedimiento.",
                "Buscar procedimiento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuEdicionBuscarRamificacion_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Buscar ramificación.";

            MessageBox.Show(
                "Aquí deberías abrir la búsqueda de ramificación.",
                "Buscar ramificación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuEdicionRefrescar_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Refrescando...";

            MessageBox.Show(
                "Aquí deberías recargar los datos del formulario activo.",
                "Refrescar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            lblEstado.Text = "Listo...";
        }

        #endregion

        #region Herramientas

        private void mnuHerramientasConfiguracionConexiones_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Configuración de conexiones.";

            MessageBox.Show(
                "Aquí deberías abrir el formulario de configuración de conexiones.",
                "Configuración de conexiones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Ejemplo:
            // using (var frm = new FrmConfiguracionConexiones())
            // {
            //     frm.ShowDialog(this);
            // }
        }

        private void mnuHerramientasComprobarConexiones_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Comprobando conexiones...";

            try
            {
                // Aquí irían tus comprobaciones reales de conexión.
                MessageBox.Show(
                    "Pendiente implementar comprobación real de conexiones.",
                    "Comprobar conexiones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblEstado.Text = "Conexiones comprobadas.";
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error al comprobar conexiones.";

                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void mnuHerramientasMigrarModelo_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Migración de modelo DP4.";

            DialogResult result = MessageBox.Show(
                "Esto debería lanzar la migración del modelo DP4.\r\n\r\n¿Continuar?",
                "Migrar modelo DP4",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Pendiente enlazar la migración real.",
                    "Migración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblEstado.Text = "Migración lanzada.";
            }
            else
            {
                lblEstado.Text = "Migración cancelada.";
            }
        }

        private void mnuHerramientasConfigurarProcedimientos_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Configuración de procedimientos.";

            MessageBox.Show(
                "Aquí deberías abrir el formulario de configuración de procedimientos.",
                "Configurar procedimientos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuHerramientasProgramarEjecucion_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Programación de ejecución.";

            MessageBox.Show(
                "Aquí deberías abrir la pantalla de programación de ejecución batch.",
                "Programar ejecución",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuHerramientasEjecutarAhora_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Ejecutando proceso...";

            DialogResult result = MessageBox.Show(
                "Se va a lanzar la ejecución inmediata.\r\n\r\n¿Continuar?",
                "Ejecutar ahora",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Pendiente enlazar la ejecución real del proceso.",
                    "Ejecución",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblEstado.Text = "Proceso lanzado.";
            }
            else
            {
                lblEstado.Text = "Ejecución cancelada.";
            }
        }

        private void mnuHerramientasCancelarActual_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Cancelando proceso actual...";

            DialogResult result = MessageBox.Show(
                "¿Quieres cancelar el proceso actual?",
                "Cancelar proceso actual",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Pendiente enlazar la cancelación real del proceso actual.",
                    "Cancelar actual",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblEstado.Text = "Proceso actual cancelado.";
            }
            else
            {
                lblEstado.Text = "Cancelación anulada.";
            }
        }

        private void mnuHerramientasCancelarTodos_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Cancelando todos los procesos...";

            DialogResult result = MessageBox.Show(
                "¿Quieres cancelar todos los procesos pendientes?",
                "Cancelar todos",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Pendiente enlazar la cancelación real de todos los procesos.",
                    "Cancelar todos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblEstado.Text = "Todos los procesos cancelados.";
            }
            else
            {
                lblEstado.Text = "Cancelación anulada.";
            }
        }

        private void mnuHerramientasVerLog_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Visualización de log.";

            string rutaLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pasabidea.log");

            if (File.Exists(rutaLog))
            {
                MessageBox.Show(
                    "Existe el fichero de log en:\r\n" + rutaLog + "\r\n\r\nAquí podrías abrir un visor propio.",
                    "Log",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "No se ha encontrado el fichero de log.\r\n\r\nRuta esperada:\r\n" + rutaLog,
                    "Log",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void mnuHerramientasOpciones_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Opciones.";

            MessageBox.Show(
                "Aquí deberías abrir un formulario de opciones generales de la aplicación.",
                "Opciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Ayuda

        private void mnuAyudaVerAyuda_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Ayuda.";

            MessageBox.Show(
                "Aquí deberías abrir la ayuda de la aplicación o documentación asociada.",
                "Ayuda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnuAyudaAcercaDe_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Acerca de.";

            MessageBox.Show(
                "Pasabidea\r\n\r\nAplicación de pasarela CM -> HidraNet / MUGI\r\nLantik",
                "Acerca de",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        private string connectionPasarela =
            ConfigurationManager.ConnectionStrings["BD_PASARELA"].ConnectionString;

        private string connectionErwin =
            ConfigurationManager.ConnectionStrings["BD_DP4"].ConnectionString;


        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            // Generar va a Hacer dos acciones principales:
            // 1) Generar las tablas intermedias en PASARELA_ARTEZ (o la que toque) a partir del DI_ID seleccionado.




            int diId = Convert.ToInt32(txtDI_ID.Text);

            /// TODO: Es necesario integrar el desarrollo de Lantik.Pasarela.sqlRepository.ErwinPasarelaArtezTransformer 
            /// dentro de esta aplicación, o convertirlo en una librería que se pueda consumir desde aquí, para que 
            /// al hacer clic en "Generar" se ejecute el proceso completo de generación de tablas intermedias a 
            /// partir del DI_ID seleccionado. HAY QUE REVISAR EL InsertarDiagrama1 e integrarlo con esta clase.
            var transformer = new Lantik.Pasarela.sqlRepository.ErwinPasarelaArtezTransformer();

            //string procedimiento = cmbProc.Text;
            string procedimiento = Lantik.Pasarela.sqlRepository.ErwinPasarelaArtezTransformer.ObtenerCodigoProcedimiento(
                connectionErwin,
                "ARTEZELI",
                diId
            );

            var result = transformer.GenerarDesdeDiId(diId, procedimiento);            

            MessageBox.Show(
                $"Transformación finalizada a PASARELA_ARTEZ. Diagramas generados: {result.Data}",
                "Pasarela",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            
            // 2) A partir de esas tablas, generar el código SQL para crear la nueva versión del procedimiento almacenado en destino (DBN8POCARTEZ) y ejecutarlo allí.

            var generator = new WfActionsGenerator();

            var result2 = await generator.GenerarWfActionsAsync(new WfActionsGenerationRequest
            {
                Procedimiento = cmbProc.Text, //"TA999900",
                DiId = int.Parse(txtDI_ID.Text.Trim()),
                PasarelaConnectionString = connectionPasarela,
                DestinoConnectionString =
                    "Data Source=D2VAPPHIDES;Initial Catalog=DBN8POCARTEZ;Integrated Security=SSPI;TrustServerCertificate=True",

                InfraConnectionString = connectionPasarela,

                NuevaVersion = true,
                SobrescribirVersion = true,
                DryRun = false,
                FechaActivacion = DateTime.Today,

                ProjectDatabaseName = "DBN8POCARTEZ",
                CodigoAplicacion = "N8PA",

                DbGenerales = "DBT0GENE",
                DbGestion = "DBT0GEST",
                DbEspecificas = "DBPGES",
                DbWord = "DBT0WORD",
                DbDocumentos = "DBT0DOCU",
                DbArranque = "DBT0ARRA",
                DbMensajes = "DBT0MENS",
                DbInfra = "DBT0INFR",


            });
        }

        private void cmbProc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDI_ID.Text = (cmbProc.SelectedItem as ErwinDiagramDTO).DI_ID.ToString();
        }

        private readonly ErwinRepository _erwinRepository = new ErwinRepository();

        private void cmbModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modelName = cmbModelos.SelectedValue != null
                ? cmbModelos.SelectedValue.ToString()
                : cmbModelos.Text;

            CargarTreeViewProcedimientos(modelName);
        }

        private void CargarTreeViewProcedimientos(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
                return;

            tvwProcs.BeginUpdate();

            try
            {
                tvwProcs.Nodes.Clear();

                DataTable dt = _erwinRepository.ObtenerArbolProcedimientos(modelName);

                Dictionary<string, TreeNode> nodosPorRuta = new Dictionary<string, TreeNode>();

                foreach (DataRow row in dt.Rows)
                {
                    int raizDiId = GetInt(row, "RAIZ_DI_ID");
                    string raizDiName = GetString(row, "RAIZ_DI_NAME");

                    int nivel = GetInt(row, "NIVEL");

                    int diId = GetInt(row, "DI_ID");
                    int anoId = GetInt(row, "ANO_ID");
                    int anoTabnr = GetInt(row, "ANO_TABNR");
                    string diName = GetString(row, "DI_NAME");
                    string diTitle = GetString(row, "DI_TITLE");
                    string tipoLogico = GetString(row, "TIPO_LOGICO");
                    string diTypeDesc = GetString(row, "DI_TYPE_DESC");
                    string rutaIds = GetString(row, "RUTA_IDS");

                    if (string.IsNullOrWhiteSpace(rutaIds))
                        continue;

                    // 1) Crear raíz si no existe.
                    string rutaRaiz = raizDiId.ToString();

                    if (!nodosPorRuta.ContainsKey(rutaRaiz))
                    {
                        TreeNode nodoRaiz = new TreeNode(raizDiName);
                        nodoRaiz.Name = raizDiId.ToString();
                        nodoRaiz.Tag = new NodoProcedimientoInfo
                        {
                            DiId = raizDiId,
                            DiName = raizDiName,
                            DiTitle = raizDiName,
                            TipoLogico = "GRUPO_PROCEDIMIENTO",
                            DiTypeDesc = "Grupo de Procedimientos",
                            Nivel = 0,
                            RutaIds = rutaRaiz
                        };

                        tvwProcs.Nodes.Add(nodoRaiz);
                        nodosPorRuta.Add(rutaRaiz, nodoRaiz);
                    }

                    // 2) Evitar duplicados.
                    if (nodosPorRuta.ContainsKey(rutaIds))
                        continue;

                    // 3) Buscar padre.
                    string rutaPadre = ObtenerRutaPadre(rutaIds);

                    TreeNode nodoPadre;
                    if (!nodosPorRuta.TryGetValue(rutaPadre, out nodoPadre))
                    {
                        // Fallback: si por cualquier motivo no está el padre,
                        // cuelga directamente de la raíz.
                        nodoPadre = nodosPorRuta[rutaRaiz];
                    }

                    // 4) Crear nodo actual.
                    TreeNode nodoActual = new TreeNode(diName);
                    nodoActual.Name = diId.ToString();
                    nodoActual.Tag = new NodoProcedimientoInfo
                    {
                        DiId = diId,
                        AnoId = anoId,
                        AnoTabnr = anoTabnr,
                        DiName = diName,
                        DiTitle = diTitle,
                        TipoLogico = tipoLogico,
                        DiTypeDesc = diTypeDesc,
                        Nivel = nivel,
                        RutaIds = rutaIds
                    };

                    nodoPadre.Nodes.Add(nodoActual);
                    nodosPorRuta.Add(rutaIds, nodoActual);
                }

                tvwProcs.ExpandAll();
            }
            finally
            {
                tvwProcs.EndUpdate();
            }
        }

        private static string ObtenerRutaPadre(string rutaIds)
        {
            int pos = rutaIds.LastIndexOf(" > ", StringComparison.Ordinal);

            if (pos <= 0)
                return rutaIds;

            return rutaIds.Substring(0, pos);
        }

        private static string GetString(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return string.Empty;

            object value = row[columnName];

            return value == DBNull.Value || value == null
                ? string.Empty
                : value.ToString().Trim();
        }

        private static int GetInt(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return 0;

            object value = row[columnName];

            if (value == DBNull.Value || value == null)
                return 0;

            return Convert.ToInt32(value);
        }

        private void tvwProcs_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }

    internal sealed class NodoProcedimientoInfo
    {
        public int DiId { get; set; }
        public int AnoId { get; set; }
        public int AnoTabnr { get; set; }

        public string DiName { get; set; }
        public string DiTitle { get; set; }
        public string DiTypeDesc { get; set; }
        public string TipoLogico { get; set; }

        public int Nivel { get; set; }
        public string RutaIds { get; set; }

        public bool EsProcedimiento
        {
            get { return string.Equals(TipoLogico, "PROCEDIMIENTO", StringComparison.OrdinalIgnoreCase); }
        }

        public bool EsGrupoProcedimiento
        {
            get { return string.Equals(TipoLogico, "GRUPO_PROCEDIMIENTO", StringComparison.OrdinalIgnoreCase); }
        }

        public override string ToString()
        {
            return DiName;
        }
    }

    internal sealed class ErwinRepository
    {
        public DataTable ObtenerArbolProcedimientos(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException("El modelo no puede estar vacío.", nameof(modelName));

            DbContext db = DbContext.Get("BD_DP4"); // nombre de la connectionString en App.config
            db.CommandTimeout = 120;

            return db.QueryStoredProcedure(
                "dbo.SPPasarelaObtenerArbolProcedimientos",
                DbContext.Param("@ModelName", SqlDbType.NVarChar, 128, modelName.Trim())
            );
        }
    }
}