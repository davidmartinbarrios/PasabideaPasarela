<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CrearProcedimientoDeXML
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.EtiquetaSeleccionarXML = New System.Windows.Forms.LinkLabel()
        Me.GrupoSeleccionarXML = New System.Windows.Forms.GroupBox()
        Me.SeleccionBaseDatosAccionesInfraestructura = New System.Windows.Forms.ComboBox()
        Me.EtiquetaBaseDatosAccionesInfraestructura = New System.Windows.Forms.Label()
        Me.EtiquetaEntornosDestinoAdicionales = New System.Windows.Forms.Label()
        Me.SeleccionEntornosDestinoFlujoAdicionales = New System.Windows.Forms.CheckedListBox()
        Me.EtiquetaBaseDatosDestinoFlujo = New System.Windows.Forms.Label()
        Me.SeleccionBaseDatosDestinoFlujo = New System.Windows.Forms.ComboBox()
        Me.CrearProcedimiento = New System.Windows.Forms.Button()
        Me.XML = New System.Windows.Forms.TextBox()
        Me.SeleccionarFicheroXML = New System.Windows.Forms.OpenFileDialog()
        Me.GrupoSeleccionarXML.SuspendLayout()
        Me.SuspendLayout()
        '
        'EtiquetaSeleccionarXML
        '
        Me.EtiquetaSeleccionarXML.AutoSize = True
        Me.EtiquetaSeleccionarXML.Font = New System.Drawing.Font("Lato", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EtiquetaSeleccionarXML.Location = New System.Drawing.Point(15, 170)
        Me.EtiquetaSeleccionarXML.Name = "EtiquetaSeleccionarXML"
        Me.EtiquetaSeleccionarXML.Size = New System.Drawing.Size(92, 13)
        Me.EtiquetaSeleccionarXML.TabIndex = 0
        Me.EtiquetaSeleccionarXML.TabStop = True
        Me.EtiquetaSeleccionarXML.Text = "Seleccionar XML:"
        '
        'GrupoSeleccionarXML
        '
        Me.GrupoSeleccionarXML.Controls.Add(Me.SeleccionBaseDatosAccionesInfraestructura)
        Me.GrupoSeleccionarXML.Controls.Add(Me.EtiquetaBaseDatosAccionesInfraestructura)
        Me.GrupoSeleccionarXML.Controls.Add(Me.EtiquetaEntornosDestinoAdicionales)
        Me.GrupoSeleccionarXML.Controls.Add(Me.SeleccionEntornosDestinoFlujoAdicionales)
        Me.GrupoSeleccionarXML.Controls.Add(Me.EtiquetaBaseDatosDestinoFlujo)
        Me.GrupoSeleccionarXML.Controls.Add(Me.SeleccionBaseDatosDestinoFlujo)
        Me.GrupoSeleccionarXML.Controls.Add(Me.CrearProcedimiento)
        Me.GrupoSeleccionarXML.Controls.Add(Me.XML)
        Me.GrupoSeleccionarXML.Controls.Add(Me.EtiquetaSeleccionarXML)
        Me.GrupoSeleccionarXML.Font = New System.Drawing.Font("Lato", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrupoSeleccionarXML.Location = New System.Drawing.Point(12, 12)
        Me.GrupoSeleccionarXML.Name = "GrupoSeleccionarXML"
        Me.GrupoSeleccionarXML.Size = New System.Drawing.Size(1090, 390)
        Me.GrupoSeleccionarXML.TabIndex = 1
        Me.GrupoSeleccionarXML.TabStop = False
        Me.GrupoSeleccionarXML.Text = "Seleccionar XML"
        '
        'SeleccionBaseDatosAccionesInfraestructura
        '
        Me.SeleccionBaseDatosAccionesInfraestructura.CausesValidation = False
        Me.SeleccionBaseDatosAccionesInfraestructura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SeleccionBaseDatosAccionesInfraestructura.FormattingEnabled = True
        Me.SeleccionBaseDatosAccionesInfraestructura.Location = New System.Drawing.Point(246, 110)
        Me.SeleccionBaseDatosAccionesInfraestructura.Name = "SeleccionBaseDatosAccionesInfraestructura"
        Me.SeleccionBaseDatosAccionesInfraestructura.Size = New System.Drawing.Size(254, 21)
        Me.SeleccionBaseDatosAccionesInfraestructura.TabIndex = 9
        '
        'EtiquetaBaseDatosAccionesInfraestructura
        '
        Me.EtiquetaBaseDatosAccionesInfraestructura.AutoSize = True
        Me.EtiquetaBaseDatosAccionesInfraestructura.Font = New System.Drawing.Font("Lato", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EtiquetaBaseDatosAccionesInfraestructura.Location = New System.Drawing.Point(15, 113)
        Me.EtiquetaBaseDatosAccionesInfraestructura.Name = "EtiquetaBaseDatosAccionesInfraestructura"
        Me.EtiquetaBaseDatosAccionesInfraestructura.Size = New System.Drawing.Size(203, 13)
        Me.EtiquetaBaseDatosAccionesInfraestructura.TabIndex = 8
        Me.EtiquetaBaseDatosAccionesInfraestructura.Text = "Base de Datos Acciones Infraestructura:"
        '
        'EtiquetaEntornosDestinoAdicionales
        '
        Me.EtiquetaEntornosDestinoAdicionales.AutoSize = True
        Me.EtiquetaEntornosDestinoAdicionales.Font = New System.Drawing.Font("Lato", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EtiquetaEntornosDestinoAdicionales.Location = New System.Drawing.Point(15, 66)
        Me.EtiquetaEntornosDestinoAdicionales.Name = "EtiquetaEntornosDestinoAdicionales"
        Me.EtiquetaEntornosDestinoAdicionales.Size = New System.Drawing.Size(179, 13)
        Me.EtiquetaEntornosDestinoAdicionales.TabIndex = 6
        Me.EtiquetaEntornosDestinoAdicionales.Text = "Entornos Destino Flujo Adicionales:"
        '
        'SeleccionEntornosDestinoFlujoAdicionales
        '
        Me.SeleccionEntornosDestinoFlujoAdicionales.FormattingEnabled = True
        Me.SeleccionEntornosDestinoFlujoAdicionales.Location = New System.Drawing.Point(246, 52)
        Me.SeleccionEntornosDestinoFlujoAdicionales.Name = "SeleccionEntornosDestinoFlujoAdicionales"
        Me.SeleccionEntornosDestinoFlujoAdicionales.Size = New System.Drawing.Size(254, 52)
        Me.SeleccionEntornosDestinoFlujoAdicionales.TabIndex = 5
        '
        'EtiquetaBaseDatosDestinoFlujo
        '
        Me.EtiquetaBaseDatosDestinoFlujo.AutoSize = True
        Me.EtiquetaBaseDatosDestinoFlujo.Font = New System.Drawing.Font("Lato", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EtiquetaBaseDatosDestinoFlujo.Location = New System.Drawing.Point(15, 28)
        Me.EtiquetaBaseDatosDestinoFlujo.Name = "EtiquetaBaseDatosDestinoFlujo"
        Me.EtiquetaBaseDatosDestinoFlujo.Size = New System.Drawing.Size(145, 13)
        Me.EtiquetaBaseDatosDestinoFlujo.TabIndex = 4
        Me.EtiquetaBaseDatosDestinoFlujo.Text = "Base de Datos Destino Flujo:"
        '
        'SeleccionBaseDatosDestinoFlujo
        '
        Me.SeleccionBaseDatosDestinoFlujo.CausesValidation = False
        Me.SeleccionBaseDatosDestinoFlujo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SeleccionBaseDatosDestinoFlujo.FormattingEnabled = True
        Me.SeleccionBaseDatosDestinoFlujo.Location = New System.Drawing.Point(246, 25)
        Me.SeleccionBaseDatosDestinoFlujo.Name = "SeleccionBaseDatosDestinoFlujo"
        Me.SeleccionBaseDatosDestinoFlujo.Size = New System.Drawing.Size(254, 21)
        Me.SeleccionBaseDatosDestinoFlujo.TabIndex = 3
        '
        'CrearProcedimiento
        '
        Me.CrearProcedimiento.Font = New System.Drawing.Font("Lato Black", 9.749999!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CrearProcedimiento.Location = New System.Drawing.Point(113, 219)
        Me.CrearProcedimiento.Name = "CrearProcedimiento"
        Me.CrearProcedimiento.Size = New System.Drawing.Size(168, 28)
        Me.CrearProcedimiento.TabIndex = 2
        Me.CrearProcedimiento.Text = "Crear Procedimiento"
        Me.CrearProcedimiento.UseVisualStyleBackColor = True
        '
        'XML
        '
        Me.XML.Enabled = False
        Me.XML.Location = New System.Drawing.Point(113, 167)
        Me.XML.Name = "XML"
        Me.XML.Size = New System.Drawing.Size(952, 21)
        Me.XML.TabIndex = 1
        '
        'CrearProcedimientoDeXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1114, 450)
        Me.Controls.Add(Me.GrupoSeleccionarXML)
        Me.Name = "CrearProcedimientoDeXML"
        Me.Text = "Crear Procedimiento y Flujo HIDRA a partir de un XML"
        Me.GrupoSeleccionarXML.ResumeLayout(False)
        Me.GrupoSeleccionarXML.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EtiquetaSeleccionarXML As LinkLabel
    Friend WithEvents GrupoSeleccionarXML As GroupBox
    Friend WithEvents XML As TextBox
    Friend WithEvents SeleccionarFicheroXML As OpenFileDialog
    Friend WithEvents CrearProcedimiento As Button
    Friend WithEvents EtiquetaBaseDatosDestinoFlujo As Label
    Friend WithEvents SeleccionBaseDatosDestinoFlujo As ComboBox
    Friend WithEvents EtiquetaEntornosDestinoAdicionales As Label
    Friend WithEvents SeleccionEntornosDestinoFlujoAdicionales As CheckedListBox
    Friend WithEvents SeleccionBaseDatosAccionesInfraestructura As ComboBox
    Friend WithEvents EtiquetaBaseDatosAccionesInfraestructura As Label
End Class
