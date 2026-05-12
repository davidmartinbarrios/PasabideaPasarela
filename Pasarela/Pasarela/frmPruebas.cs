using Lantik.Pasarela.Application.AOs;
using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Lantik.Pasarela.Pasarela
{
    public partial class frmPruebas : Form
    {
        public frmPruebas()
        {
            InitializeComponent();
        }

        private void btnListarShape_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Shape");
                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<ShapeDTO>> response = new ShapeApplication().ObtenerPorIDyModelo("ARDATZ", 1);

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<ShapeDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");
                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    
                    Logger.Info(rowData.ToString());
                }
                dgvShape.DataSource = dt;
                dgvShape.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarShape_Click");
        }

        private void btnListartabProcess_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Process");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<ProcessDTO>> response = new ProcessApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<ProcessDTO>(response.Data);
                
                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }
                dgvProcess.DataSource = dt;
                dgvProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListartabProcess_Click");
        }

        private DiagramDTO _diagramaTarget = null;

        private void btnListarDiagram_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Diagram");
                dgvDiagram.Hide();
                this.Cursor = Cursors.WaitCursor;

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<DiagramDTO>> response;

                if (string.IsNullOrEmpty(txtDiagramModelName.Text))
                    response = new DiagramApplication().ObtenerTodos();
                else if (!string.IsNullOrEmpty(txtDiagramModelName.Text) && !string.IsNullOrEmpty(txtDI_ID.Text))
                {
                    response = new DiagramApplication().GetByDIModelName(txtDiagramModelName.Text, int.Parse(txtDI_ID.Text));
                    _diagramaTarget = response.Data.Count > 0 ? response.Data[0] : null;
                }
                else
                {
                    var singleResponse = new DiagramApplication().ObtenerPorId(txtDiagramModelName.Text, int.Parse(txtDI_ID.Text));
                    response = new ResponseBaseDTO<IList<DiagramDTO>>
                    {
                        Data = new List<DiagramDTO> { singleResponse.Data },
                    };
                }

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<DiagramDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvDiagram.DataSource = dt;
                dgvDiagram.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDiagram.Show();
                this.Cursor = Cursors.Default;
            }, $"Form1.btnListarDiagram_Click");
        }

        private void btnListarJoiner_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Joiner");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<JoinerDTO>> response = new JoinerApplication().ObtenerPorIDyModelo("ARDATZ", 1);

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<JoinerDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");
                
                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvJoiner.DataSource = dt;
                dgvJoiner.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarJoiner_Click");
        }

        private void btnListarConnector_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Connector");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<ConnectorDTO>> response = new ConnectorApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<ConnectorDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvConnector.DataSource = dt;
                dgvConnector.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarConnector_Click");
        }

        private void btnListarEvent_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Event");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<EventDTO>> response = new EventApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<EventDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvEvent.DataSource = dt;
                dgvEvent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarEvent_Click");
        }

        private void btnListarAttribute_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Attribute");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<AttributeDTO>> response = new AttributeApplication().ObtenerTodos();

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<AttributeDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvAttribute.DataSource = dt;
                dgvAttribute.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarAttribute_Click");
        }

        private void btnListarEntity_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Entity");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<EntityDTO>> response = new EntityApplication().ObtenerTodos();

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<EntityDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvEntity.DataSource = dt;
                dgvEntity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarEntity_Click");
        }

        private void btnListarOrganization_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Organization");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<OrganizationDTO>> response = new OrganizationApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<OrganizationDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvOrganization.DataSource = dt;
                dgvOrganization.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarOrganization_Click");
        }

        private void btnListarW_PROP_TYPE_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla CW_PROP_TYPE");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_Prop_TypeDTO>> response = new Cw_Prop_TypeApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_Prop_TypeDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCW_PROP_TYPE.DataSource = dt;
                dgvCW_PROP_TYPE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarW_PROP_TYPE_Click");
        }

        private void btnListarCW_Lookup_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla CW_Lookup");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_LookupDTO>> response;

                if (string.IsNullOrEmpty(txtModelName.Text))
                    response = new Cw_LookupApplication().ObtenerTodos();
                else
                    response = new Cw_LookupApplication().ObtenerPorModelo(txtModelName.Text);

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_LookupDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCW_Lookup.DataSource = dt;
                dgvCW_Lookup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarCW_Lookup_Click");
        }

        private void btnListarCW_Data_Usage_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla CW_Data_Usage");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_Data_UsageDTO>> response = new Cw_Data_UsageApplication().ObtenerTodos();

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_Data_UsageDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCW_Data_Usage.DataSource = dt;
                dgvCW_Data_Usage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarCW_Data_Usage_Click");
        }

        private void btnListarCW_Object_Type_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla CW_Object_Type");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_Object_TypeDTO>> response = new Cw_Object_TypeApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_Object_TypeDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCW_Object_Type.DataSource = dt;
                dgvCW_Object_Type.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarCW_Object_Type_Click");
        }

        private void btnListarCW_Object_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla CW_Object");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_ObjectDTO>> response = new Cw_ObjectApplication().ObtenerPorModelo("ARDATZ");

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_ObjectDTO>(response.Data);

                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCW_Object.DataSource = dt;
                dgvCW_Object.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarCW_Object_Click");
        }

        private void btnListarCw_Inter_Object_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                Logger.Debug("LLamamos a listar informacion de la tabla Cw_Inter_Object");

                // Declaramos la lista que vamos a devolver
                ResponseBaseDTO<IList<Cw_Inter_ObjectDTO>> response = new Cw_Inter_ObjectApplication().ObtenerTodos();

                // Convertimos la lista obtenida en un DataTable
                DataTable dt = VO.Utils.ToDataTable<Cw_Inter_ObjectDTO>(response.Data);
                
                Logger.Info("Mostramos la información que obtenemos de BD con " + dt.Rows.Count + " registros");

                foreach (DataRow row in dt.Rows)
                {
                    var rowData = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columnName = dt.Columns[i].ColumnName;
                        string columnValue = row[i]?.ToString() ?? "NULL";
                        rowData.AppendFormat("{0}: {1}, ", columnName, columnValue);
                    }
                    // Remove the trailing comma and space
                    if (rowData.Length > 2)
                    {
                        rowData.Length -= 2;
                    }
                    Logger.Info(rowData.ToString());
                }

                dgvCw_Inter_Object.DataSource = dt;
                dgvCw_Inter_Object.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }, $"Form1.btnListarCw_Inter_Object_Click");
        }

        private void txtDI_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número ni una tecla de control, cancela el evento
                e.Handled = true;
            }
        }

        /// <summary>
        /// MB57
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            SafeExecutor.SafeExecution(() =>
            {
                if (_diagramaTarget == null)
                    return;

                Logger.Debug("Ejecutamos " + _diagramaTarget.NOMBRE);

                DiagramDTO diagram = _diagramaTarget; //dgvDiagram.DataSource

                // GET_DIAGRAM_BY_DI_NAME_SQL
                var response = new DiagramApplication().InsertDiagram1(diagram);

                // Convertimos la lista obtenida en un DataTable
                //DataTable dt = VO.Utils.ToDataTable<DiagramDTO>(response.Data);
                             

            }, $"Form1.btnEjecutar_Click");
        }
    }
}
