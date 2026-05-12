using Microsoft.Web.WebView2.WinForms;

namespace Pasabidea
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private WebView2 webView21;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtDiagramModelName;
        private System.Windows.Forms.Label lblDiId;
        private System.Windows.Forms.TextBox txtDI_ID;
        private System.Windows.Forms.Button btnVerBpmn;
        private System.Windows.Forms.Button btnGenerar;

        private System.Windows.Forms.ComboBox cmbProc;
        private System.Windows.Forms.ComboBox cmbModelos;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.ComboBox cmbBDMugi;

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivo;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivoNuevoProceso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorArchivo1;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivoAbrirConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivoGuardarConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivoGuardarConfiguracionComo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorArchivo2;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivoSalir;

        private System.Windows.Forms.ToolStripMenuItem mnuEdicion;
        private System.Windows.Forms.ToolStripMenuItem mnuEdicionSeleccionarTodo;
        private System.Windows.Forms.ToolStripMenuItem mnuEdicionDeseleccionarTodo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorEdicion1;
        private System.Windows.Forms.ToolStripMenuItem mnuEdicionBuscarProcedimiento;
        private System.Windows.Forms.ToolStripMenuItem mnuEdicionBuscarRamificacion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorEdicion2;
        private System.Windows.Forms.ToolStripMenuItem mnuEdicionRefrescar;

        private System.Windows.Forms.ToolStripMenuItem mnuHerramientas;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasConfiguracionConexiones;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasComprobarConexiones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHerramientas1;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasMigrarModelo;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasConfigurarProcedimientos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHerramientas2;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasProgramarEjecucion;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasEjecutarAhora;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHerramientas3;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasCancelarActual;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasCancelarTodos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHerramientas4;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasVerLog;
        private System.Windows.Forms.ToolStripMenuItem mnuHerramientasOpciones;

        private System.Windows.Forms.ToolStripMenuItem mnuAyuda;
        private System.Windows.Forms.ToolStripMenuItem mnuAyudaVerAyuda;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAyuda1;
        private System.Windows.Forms.ToolStripMenuItem mnuAyudaAcercaDe;

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;

        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.TreeView tvwProcs;
        private System.Windows.Forms.SplitContainer splitContainer1;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();

                if (webView21 != null)
                {
                    webView21.Dispose();
                    webView21 = null;
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("GPA");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Nodo7");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Nodo8");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("PA999900", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Nodo10");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("PA9999010", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("GPA1", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("GPA2");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("GPA3");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("GPA4");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("GPA5");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.mnuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArchivoNuevoProceso = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorArchivo1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArchivoAbrirConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArchivoGuardarConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArchivoGuardarConfiguracionComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorArchivo2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArchivoSalir = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuEdicion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdicionSeleccionarTodo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdicionDeseleccionarTodo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorEdicion1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdicionBuscarProcedimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdicionBuscarRamificacion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorEdicion2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdicionRefrescar = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuHerramientas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasConfiguracionConexiones = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasComprobarConexiones = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHerramientas1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHerramientasMigrarModelo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasConfigurarProcedimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHerramientas2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHerramientasProgramarEjecucion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasEjecutarAhora = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHerramientas3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHerramientasCancelarActual = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasCancelarTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHerramientas4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHerramientasVerLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHerramientasOpciones = new System.Windows.Forms.ToolStripMenuItem();

            this.mnuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAyudaVerAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorAyuda1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAyudaAcercaDe = new System.Windows.Forms.ToolStripMenuItem();

            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelContenido = new System.Windows.Forms.Panel();
<<<<<<< HEAD
            this.tvwProcs = new System.Windows.Forms.TreeView();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
=======
>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            this.panelTop = new System.Windows.Forms.Panel();
            this.cmbModelos = new System.Windows.Forms.ComboBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.cmbBDMugi = new System.Windows.Forms.ComboBox();
            this.cmbProc = new System.Windows.Forms.ComboBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtDiagramModelName = new System.Windows.Forms.TextBox();
            this.lblDiId = new System.Windows.Forms.Label();
            this.txtDI_ID = new System.Windows.Forms.TextBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnVerBpmn = new System.Windows.Forms.Button();

            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvwProcs = new System.Windows.Forms.TreeView();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();

            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();

            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArchivo,
            this.mnuEdicion,
            this.mnuHerramientas,
            this.mnuAyuda});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(1333, 28);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";

            // 
            // mnuArchivo
            // 
            this.mnuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArchivoNuevoProceso,
            this.toolStripSeparatorArchivo1,
            this.mnuArchivoAbrirConfiguracion,
            this.mnuArchivoGuardarConfiguracion,
            this.mnuArchivoGuardarConfiguracionComo,
            this.toolStripSeparatorArchivo2,
            this.mnuArchivoSalir});
            this.mnuArchivo.Name = "mnuArchivo";
            this.mnuArchivo.Size = new System.Drawing.Size(71, 24);
            this.mnuArchivo.Text = "&Archivo";

            // 
            // mnuArchivoNuevoProceso
            // 
            this.mnuArchivoNuevoProceso.Name = "mnuArchivoNuevoProceso";
            this.mnuArchivoNuevoProceso.Size = new System.Drawing.Size(302, 26);
            this.mnuArchivoNuevoProceso.Text = "&Nuevo proceso / nueva selección";
            this.mnuArchivoNuevoProceso.Click += new System.EventHandler(this.mnuArchivoNuevoProceso_Click);

            // 
            // toolStripSeparatorArchivo1
            // 
            this.toolStripSeparatorArchivo1.Name = "toolStripSeparatorArchivo1";
            this.toolStripSeparatorArchivo1.Size = new System.Drawing.Size(299, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuArchivoAbrirConfiguracion
            // 
            this.mnuArchivoAbrirConfiguracion.Name = "mnuArchivoAbrirConfiguracion";
            this.mnuArchivoAbrirConfiguracion.Size = new System.Drawing.Size(302, 26);
            this.mnuArchivoAbrirConfiguracion.Text = "&Abrir configuración";
            this.mnuArchivoAbrirConfiguracion.Click += new System.EventHandler(this.mnuArchivoAbrirConfiguracion_Click);

            // 
            // mnuArchivoGuardarConfiguracion
            // 
            this.mnuArchivoGuardarConfiguracion.Name = "mnuArchivoGuardarConfiguracion";
            this.mnuArchivoGuardarConfiguracion.Size = new System.Drawing.Size(302, 26);
            this.mnuArchivoGuardarConfiguracion.Text = "&Guardar configuración";
            this.mnuArchivoGuardarConfiguracion.Click += new System.EventHandler(this.mnuArchivoGuardarConfiguracion_Click);

            // 
            // mnuArchivoGuardarConfiguracionComo
            // 
            this.mnuArchivoGuardarConfiguracionComo.Name = "mnuArchivoGuardarConfiguracionComo";
            this.mnuArchivoGuardarConfiguracionComo.Size = new System.Drawing.Size(302, 26);
            this.mnuArchivoGuardarConfiguracionComo.Text = "Guardar configuración &como...";
            this.mnuArchivoGuardarConfiguracionComo.Click += new System.EventHandler(this.mnuArchivoGuardarConfiguracionComo_Click);

            // 
            // toolStripSeparatorArchivo2
            // 
            this.toolStripSeparatorArchivo2.Name = "toolStripSeparatorArchivo2";
            this.toolStripSeparatorArchivo2.Size = new System.Drawing.Size(299, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuArchivoSalir
            // 
            this.mnuArchivoSalir.Name = "mnuArchivoSalir";
            this.mnuArchivoSalir.Size = new System.Drawing.Size(302, 26);
            this.mnuArchivoSalir.Text = "&Salir";
            this.mnuArchivoSalir.Click += new System.EventHandler(this.mnuArchivoSalir_Click);

            // 
            // mnuEdicion
            // 
            this.mnuEdicion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdicionSeleccionarTodo,
            this.mnuEdicionDeseleccionarTodo,
            this.toolStripSeparatorEdicion1,
            this.mnuEdicionBuscarProcedimiento,
            this.mnuEdicionBuscarRamificacion,
            this.toolStripSeparatorEdicion2,
            this.mnuEdicionRefrescar});
            this.mnuEdicion.Name = "mnuEdicion";
            this.mnuEdicion.Size = new System.Drawing.Size(70, 24);
            this.mnuEdicion.Text = "&Edición";

            // 
            // mnuEdicionSeleccionarTodo
            // 
            this.mnuEdicionSeleccionarTodo.Name = "mnuEdicionSeleccionarTodo";
            this.mnuEdicionSeleccionarTodo.Size = new System.Drawing.Size(238, 26);
            this.mnuEdicionSeleccionarTodo.Text = "Seleccionar &todo";
            this.mnuEdicionSeleccionarTodo.Click += new System.EventHandler(this.mnuEdicionSeleccionarTodo_Click);

            // 
            // mnuEdicionDeseleccionarTodo
            // 
            this.mnuEdicionDeseleccionarTodo.Name = "mnuEdicionDeseleccionarTodo";
            this.mnuEdicionDeseleccionarTodo.Size = new System.Drawing.Size(238, 26);
            this.mnuEdicionDeseleccionarTodo.Text = "&Deseleccionar todo";
            this.mnuEdicionDeseleccionarTodo.Click += new System.EventHandler(this.mnuEdicionDeseleccionarTodo_Click);

            // 
            // toolStripSeparatorEdicion1
            // 
            this.toolStripSeparatorEdicion1.Name = "toolStripSeparatorEdicion1";
            this.toolStripSeparatorEdicion1.Size = new System.Drawing.Size(235, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuEdicionBuscarProcedimiento
            // 
            this.mnuEdicionBuscarProcedimiento.Name = "mnuEdicionBuscarProcedimiento";
            this.mnuEdicionBuscarProcedimiento.Size = new System.Drawing.Size(238, 26);
            this.mnuEdicionBuscarProcedimiento.Text = "Buscar &procedimiento...";
            this.mnuEdicionBuscarProcedimiento.Click += new System.EventHandler(this.mnuEdicionBuscarProcedimiento_Click);

            // 
            // mnuEdicionBuscarRamificacion
            // 
            this.mnuEdicionBuscarRamificacion.Name = "mnuEdicionBuscarRamificacion";
            this.mnuEdicionBuscarRamificacion.Size = new System.Drawing.Size(238, 26);
            this.mnuEdicionBuscarRamificacion.Text = "Buscar &ramificación...";
            this.mnuEdicionBuscarRamificacion.Click += new System.EventHandler(this.mnuEdicionBuscarRamificacion_Click);

            // 
            // toolStripSeparatorEdicion2
            // 
            this.toolStripSeparatorEdicion2.Name = "toolStripSeparatorEdicion2";
            this.toolStripSeparatorEdicion2.Size = new System.Drawing.Size(235, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuEdicionRefrescar
            // 
            this.mnuEdicionRefrescar.Name = "mnuEdicionRefrescar";
            this.mnuEdicionRefrescar.Size = new System.Drawing.Size(238, 26);
            this.mnuEdicionRefrescar.Text = "&Refrescar";
            this.mnuEdicionRefrescar.Click += new System.EventHandler(this.mnuEdicionRefrescar_Click);

            // 
            // mnuHerramientas
            // 
            this.mnuHerramientas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHerramientasConfiguracionConexiones,
            this.mnuHerramientasComprobarConexiones,
            this.toolStripSeparatorHerramientas1,
            this.mnuHerramientasMigrarModelo,
            this.mnuHerramientasConfigurarProcedimientos,
            this.toolStripSeparatorHerramientas2,
            this.mnuHerramientasProgramarEjecucion,
            this.mnuHerramientasEjecutarAhora,
            this.toolStripSeparatorHerramientas3,
            this.mnuHerramientasCancelarActual,
            this.mnuHerramientasCancelarTodos,
            this.toolStripSeparatorHerramientas4,
            this.mnuHerramientasVerLog,
            this.mnuHerramientasOpciones});
            this.mnuHerramientas.Name = "mnuHerramientas";
            this.mnuHerramientas.Size = new System.Drawing.Size(110, 24);
            this.mnuHerramientas.Text = "&Herramientas";

            // 
            // mnuHerramientasConfiguracionConexiones
            // 
            this.mnuHerramientasConfiguracionConexiones.Name = "mnuHerramientasConfiguracionConexiones";
            this.mnuHerramientasConfiguracionConexiones.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasConfiguracionConexiones.Text = "Configuración de &conexiones";
            this.mnuHerramientasConfiguracionConexiones.Click += new System.EventHandler(this.mnuHerramientasConfiguracionConexiones_Click);

            // 
            // mnuHerramientasComprobarConexiones
            // 
            this.mnuHerramientasComprobarConexiones.Name = "mnuHerramientasComprobarConexiones";
            this.mnuHerramientasComprobarConexiones.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasComprobarConexiones.Text = "&Comprobar conexiones";
            this.mnuHerramientasComprobarConexiones.Click += new System.EventHandler(this.mnuHerramientasComprobarConexiones_Click);

            // 
            // toolStripSeparatorHerramientas1
            // 
            this.toolStripSeparatorHerramientas1.Name = "toolStripSeparatorHerramientas1";
            this.toolStripSeparatorHerramientas1.Size = new System.Drawing.Size(273, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuHerramientasMigrarModelo
            // 
            this.mnuHerramientasMigrarModelo.Name = "mnuHerramientasMigrarModelo";
            this.mnuHerramientasMigrarModelo.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasMigrarModelo.Text = "&Migrar modelo DP4";
            this.mnuHerramientasMigrarModelo.Click += new System.EventHandler(this.mnuHerramientasMigrarModelo_Click);

            // 
            // mnuHerramientasConfigurarProcedimientos
            // 
            this.mnuHerramientasConfigurarProcedimientos.Name = "mnuHerramientasConfigurarProcedimientos";
            this.mnuHerramientasConfigurarProcedimientos.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasConfigurarProcedimientos.Text = "Configurar &procedimientos";
            this.mnuHerramientasConfigurarProcedimientos.Click += new System.EventHandler(this.mnuHerramientasConfigurarProcedimientos_Click);

            // 
            // toolStripSeparatorHerramientas2
            // 
            this.toolStripSeparatorHerramientas2.Name = "toolStripSeparatorHerramientas2";
            this.toolStripSeparatorHerramientas2.Size = new System.Drawing.Size(273, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuHerramientasProgramarEjecucion
            // 
            this.mnuHerramientasProgramarEjecucion.Name = "mnuHerramientasProgramarEjecucion";
            this.mnuHerramientasProgramarEjecucion.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasProgramarEjecucion.Text = "&Programar ejecución";
            this.mnuHerramientasProgramarEjecucion.Click += new System.EventHandler(this.mnuHerramientasProgramarEjecucion_Click);

            // 
            // mnuHerramientasEjecutarAhora
            // 
            this.mnuHerramientasEjecutarAhora.Name = "mnuHerramientasEjecutarAhora";
            this.mnuHerramientasEjecutarAhora.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasEjecutarAhora.Text = "Ejecutar &ahora";
            this.mnuHerramientasEjecutarAhora.Click += new System.EventHandler(this.mnuHerramientasEjecutarAhora_Click);

            // 
            // toolStripSeparatorHerramientas3
            // 
            this.toolStripSeparatorHerramientas3.Name = "toolStripSeparatorHerramientas3";
            this.toolStripSeparatorHerramientas3.Size = new System.Drawing.Size(273, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuHerramientasCancelarActual
            // 
            this.mnuHerramientasCancelarActual.Name = "mnuHerramientasCancelarActual";
            this.mnuHerramientasCancelarActual.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasCancelarActual.Text = "Cancelar proceso &actual";
            this.mnuHerramientasCancelarActual.Click += new System.EventHandler(this.mnuHerramientasCancelarActual_Click);

            // 
            // mnuHerramientasCancelarTodos
            // 
            this.mnuHerramientasCancelarTodos.Name = "mnuHerramientasCancelarTodos";
            this.mnuHerramientasCancelarTodos.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasCancelarTodos.Text = "Cancelar &todos los procesos";
            this.mnuHerramientasCancelarTodos.Click += new System.EventHandler(this.mnuHerramientasCancelarTodos_Click);

            // 
            // toolStripSeparatorHerramientas4
            // 
            this.toolStripSeparatorHerramientas4.Name = "toolStripSeparatorHerramientas4";
            this.toolStripSeparatorHerramientas4.Size = new System.Drawing.Size(273, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuHerramientasVerLog
            // 
            this.mnuHerramientasVerLog.Name = "mnuHerramientasVerLog";
            this.mnuHerramientasVerLog.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasVerLog.Text = "Ver &log / incidencias";
            this.mnuHerramientasVerLog.Click += new System.EventHandler(this.mnuHerramientasVerLog_Click);

            // 
            // mnuHerramientasOpciones
            // 
            this.mnuHerramientasOpciones.Name = "mnuHerramientasOpciones";
            this.mnuHerramientasOpciones.Size = new System.Drawing.Size(276, 26);
            this.mnuHerramientasOpciones.Text = "&Opciones";
            this.mnuHerramientasOpciones.Click += new System.EventHandler(this.mnuHerramientasOpciones_Click);

            // 
            // mnuAyuda
            // 
            this.mnuAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAyudaVerAyuda,
            this.toolStripSeparatorAyuda1,
            this.mnuAyudaAcercaDe});
            this.mnuAyuda.Name = "mnuAyuda";
            this.mnuAyuda.Size = new System.Drawing.Size(63, 24);
            this.mnuAyuda.Text = "A&yuda";

            // 
            // mnuAyudaVerAyuda
            // 
            this.mnuAyudaVerAyuda.Name = "mnuAyudaVerAyuda";
            this.mnuAyudaVerAyuda.Size = new System.Drawing.Size(150, 26);
            this.mnuAyudaVerAyuda.Text = "&Ayuda";
            this.mnuAyudaVerAyuda.Click += new System.EventHandler(this.mnuAyudaVerAyuda_Click);

            // 
            // toolStripSeparatorAyuda1
            // 
            this.toolStripSeparatorAyuda1.Name = "toolStripSeparatorAyuda1";
            this.toolStripSeparatorAyuda1.Size = new System.Drawing.Size(147, 6);
<<<<<<< HEAD
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // mnuAyudaAcercaDe
            // 
            this.mnuAyudaAcercaDe.Name = "mnuAyudaAcercaDe";
            this.mnuAyudaAcercaDe.Size = new System.Drawing.Size(150, 26);
            this.mnuAyudaAcercaDe.Text = "&Acerca de";
            this.mnuAyudaAcercaDe.Click += new System.EventHandler(this.mnuAyudaAcercaDe_Click);

            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado});
            this.statusStripMain.Location = new System.Drawing.Point(0, 636);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStripMain.Size = new System.Drawing.Size(1333, 25);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.Text = "statusStripMain";

            // 
            // lblEstado
            // 
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(49, 20);
            this.lblEstado.Text = "Listo...";

            // 
            // panelContenido
            // 
<<<<<<< HEAD
            this.panelContenido.Controls.Add(this.tvwProcs);
            this.panelContenido.Controls.Add(this.webView21);
=======
            this.panelContenido.Controls.Add(this.splitContainer1);
>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            this.panelContenido.Controls.Add(this.panelTop);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 28);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1333, 608);
            this.panelContenido.TabIndex = 2;
<<<<<<< HEAD
            // 
            // tvwProcs
            // 
            this.tvwProcs.Location = new System.Drawing.Point(4, 49);
            this.tvwProcs.Margin = new System.Windows.Forms.Padding(4);
            this.tvwProcs.Name = "tvwProcs";
            treeNode1.Name = "GPA";
            treeNode1.Text = "GPA";
            treeNode2.Name = "Nodo7";
            treeNode2.Text = "Nodo7";
            treeNode3.Name = "Nodo8";
            treeNode3.Text = "Nodo8";
            treeNode4.Name = "Nodo6";
            treeNode4.Text = "PA999900";
            treeNode5.Name = "Nodo10";
            treeNode5.Text = "Nodo10";
            treeNode6.Name = "Nodo9";
            treeNode6.Text = "PA9999010";
            treeNode7.Name = "Nodo1";
            treeNode7.Text = "GPA1";
            treeNode8.Name = "Nodo2";
            treeNode8.Text = "GPA2";
            treeNode9.Name = "Nodo3";
            treeNode9.Text = "GPA3";
            treeNode10.Name = "Nodo4";
            treeNode10.Text = "GPA4";
            treeNode11.Name = "Nodo5";
            treeNode11.Text = "GPA5";
            this.tvwProcs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            this.tvwProcs.Size = new System.Drawing.Size(420, 285);
            this.tvwProcs.TabIndex = 12;
            this.tvwProcs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwProcs_AfterSelect);
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 49);
            this.webView21.Margin = new System.Windows.Forms.Padding(4);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1333, 559);
            this.webView21.TabIndex = 10;
            this.webView21.ZoomFactor = 1D;
            this.webView21.Click += new System.EventHandler(this.webView21_Click);
=======

>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.cmbModelos);
            this.panelTop.Controls.Add(this.lblDest);
            this.panelTop.Controls.Add(this.cmbBDMugi);
            this.panelTop.Controls.Add(this.cmbProc);
            this.panelTop.Controls.Add(this.lblModel);
            this.panelTop.Controls.Add(this.txtDiagramModelName);
            this.panelTop.Controls.Add(this.lblDiId);
            this.panelTop.Controls.Add(this.txtDI_ID);
            this.panelTop.Controls.Add(this.btnGenerar);
            this.panelTop.Controls.Add(this.btnVerBpmn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panelTop.Size = new System.Drawing.Size(1333, 49);
            this.panelTop.TabIndex = 11;

            // 
            // cmbModelos
            // 
            this.cmbModelos.FormattingEnabled = true;
            this.cmbModelos.Location = new System.Drawing.Point(73, 10);
            this.cmbModelos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbModelos.Name = "cmbModelos";
            this.cmbModelos.Size = new System.Drawing.Size(229, 24);
            this.cmbModelos.TabIndex = 6;
            this.cmbModelos.SelectedIndexChanged += new System.EventHandler(this.cmbModelos_SelectedIndexChanged);

            // 
            // cmbModelos
            // 
            this.cmbModelos.FormattingEnabled = true;
            this.cmbModelos.Location = new System.Drawing.Point(73, 10);
            this.cmbModelos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbModelos.Name = "cmbModelos";
            this.cmbModelos.Size = new System.Drawing.Size(229, 24);
            this.cmbModelos.TabIndex = 6;
            this.cmbModelos.SelectedIndexChanged += new System.EventHandler(this.cmbModelos_SelectedIndexChanged);
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(952, 15);
            this.lblDest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(70, 17);
            this.lblDest.TabIndex = 5;
            this.lblDest.Text = "BD MUGI:";

            // 
            // cmbBDMugi
            // 
            this.cmbBDMugi.FormattingEnabled = true;
            this.cmbBDMugi.Location = new System.Drawing.Point(1033, 10);
            this.cmbBDMugi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbBDMugi.Name = "cmbBDMugi";
            this.cmbBDMugi.Size = new System.Drawing.Size(192, 24);
            this.cmbBDMugi.TabIndex = 4;

            // 
            // cmbProc
            // 
            this.cmbProc.FormattingEnabled = true;
            this.cmbProc.Location = new System.Drawing.Point(361, 10);
            this.cmbProc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbProc.Name = "cmbProc";
            this.cmbProc.Size = new System.Drawing.Size(251, 24);
            this.cmbProc.TabIndex = 3;
            this.cmbProc.Visible = false;
            this.cmbProc.SelectedIndexChanged += new System.EventHandler(this.cmbProc_SelectedIndexChanged);

            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(13, 15);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(50, 17);
            this.lblModel.TabIndex = 0;
            this.lblModel.Text = "Model:";

            // 
            // txtDiagramModelName
            // 
            this.txtDiagramModelName.Location = new System.Drawing.Point(73, 11);
            this.txtDiagramModelName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiagramModelName.Name = "txtDiagramModelName";
            this.txtDiagramModelName.Size = new System.Drawing.Size(145, 22);
            this.txtDiagramModelName.TabIndex = 0;
            this.txtDiagramModelName.Text = "ARTEZELI";
            this.txtDiagramModelName.Visible = false;

            // 
            // lblDiId
            // 
            this.lblDiId.AutoSize = true;
            this.lblDiId.Location = new System.Drawing.Point(619, 14);
            this.lblDiId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiId.Name = "lblDiId";
            this.lblDiId.Size = new System.Drawing.Size(46, 17);
            this.lblDiId.TabIndex = 1;
            this.lblDiId.Text = "DI_ID:";

            // 
            // txtDI_ID
            // 
<<<<<<< HEAD
            this.txtDI_ID.Location = new System.Drawing.Point(669, 9);
=======
            this.txtDI_ID.Location = new System.Drawing.Point(673, 11);
>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            this.txtDI_ID.Margin = new System.Windows.Forms.Padding(4);
            this.txtDI_ID.Name = "txtDI_ID";
            this.txtDI_ID.Size = new System.Drawing.Size(79, 22);
            this.txtDI_ID.TabIndex = 1;
            this.txtDI_ID.Text = "3731";

            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.Location = new System.Drawing.Point(1247, 7);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(71, 28);
            this.btnGenerar.TabIndex = 2;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);

            // 
            // btnVerBpmn
            // 
            this.btnVerBpmn.Location = new System.Drawing.Point(311, 9);
            this.btnVerBpmn.Margin = new System.Windows.Forms.Padding(4);
            this.btnVerBpmn.Name = "btnVerBpmn";
<<<<<<< HEAD
            this.btnVerBpmn.Size = new System.Drawing.Size(15, 28);
=======
            this.btnVerBpmn.Size = new System.Drawing.Size(43, 28);
>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
            this.btnVerBpmn.TabIndex = 2;
            this.btnVerBpmn.Text = "Ver BPMN";
            this.btnVerBpmn.UseVisualStyleBackColor = true;
            this.btnVerBpmn.Visible = false;
            this.btnVerBpmn.Click += new System.EventHandler(this.btnVerBpmn_Click);

            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer1.Panel1MinSize = 220;
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.Size = new System.Drawing.Size(1333, 559);
            this.splitContainer1.TabIndex = 13;

            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvwProcs);

            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webView21);

            // 
            // tvwProcs
            // 
            this.tvwProcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwProcs.Location = new System.Drawing.Point(0, 0);
            this.tvwProcs.Margin = new System.Windows.Forms.Padding(4);
            this.tvwProcs.Name = "tvwProcs";

            treeNode1.Name = "GPA";
            treeNode1.Text = "GPA";
            treeNode2.Name = "Nodo7";
            treeNode2.Text = "Nodo7";
            treeNode3.Name = "Nodo8";
            treeNode3.Text = "Nodo8";
            treeNode4.Name = "Nodo6";
            treeNode4.Text = "PA999900";
            treeNode5.Name = "Nodo10";
            treeNode5.Text = "Nodo10";
            treeNode6.Name = "Nodo9";
            treeNode6.Text = "PA9999010";
            treeNode7.Name = "Nodo1";
            treeNode7.Text = "GPA1";
            treeNode8.Name = "Nodo2";
            treeNode8.Text = "GPA2";
            treeNode9.Name = "Nodo3";
            treeNode9.Text = "GPA3";
            treeNode10.Name = "Nodo4";
            treeNode10.Text = "GPA4";
            treeNode11.Name = "Nodo5";
            treeNode11.Text = "GPA5";

            this.tvwProcs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            this.tvwProcs.Size = new System.Drawing.Size(320, 559);
            this.tvwProcs.TabIndex = 12;
            this.tvwProcs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwProcs_AfterSelect);

            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 0);
            this.webView21.Margin = new System.Windows.Forms.Padding(4);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1008, 559);
            this.webView21.TabIndex = 10;
            this.webView21.ZoomFactor = 1D;
            this.webView21.Click += new System.EventHandler(this.webView21_Click);

            // 
            // MainForm
            // 
            this.AcceptButton = this.btnVerBpmn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 661);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pasabidea";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
<<<<<<< HEAD

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.TreeView tvwProcs;
        private System.Windows.Forms.ComboBox cmbModelos;
=======
>>>>>>> 098effc ([N8MUGIPASARELA-1] Ok)
    }
}