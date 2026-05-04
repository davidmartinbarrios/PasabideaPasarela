Imports System.IO
Imports System.Reflection
Imports Bizkaia.Pasarela
Imports Bizkaia.Pasarela.Utilidades

Public Class CrearProcedimientoDeXML
    Private _versionEjecutable As Version
    Private _versionVigentePasarela As VersionPasarela

    Private Sub EtiquetaSeleccionarXML_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles EtiquetaSeleccionarXML.LinkClicked
        SeleccionarFicheroXML.Multiselect = False
        SeleccionarFicheroXML.Filter = "Archivo XML (*.xml)|*.xml"
        SeleccionarFicheroXML.InitialDirectory = Environment.SpecialFolder.MyDocuments
        SeleccionarFicheroXML.ShowDialog()
        XML.Text = SeleccionarFicheroXML.FileName
    End Sub

    Private Sub CrearProcedimiento_Click(sender As Object, e As EventArgs) Handles CrearProcedimiento.Click
        Dim mensajeAviso As String = String.Empty
        Dim tituloAviso As String = String.Empty

        Try
            CrearProcedimiento.Enabled = False

            ' Comprobamos si es la Versión vigente
            If EsVersionVigente() Then
                ' Validamos que se hayan completado los parámetros básicos para la creación del Flujo del Procedimiento
                ValidarCamposCreacionProcedimiento()

                ' Obtenemos el Fichero XML con la Definicón del Procedimiento del que se debe Crear el Flujo
                Dim ficheroXML As FileInfo = ObtenerFicheroXML()

                ' Cargamos la Definicón del Procedimiento a partir del XML
                Dim procedimiento As Procedimiento = Utilidades.CargarProcedimiento(ficheroXML)

                ' Verificamos si se ha podido cargar la Definicón del Procedimiento
                If Not IsNothing(procedimiento) Then
                    With procedimiento
                        ' Verificamos que la Definición cargada se puede procesar
                        .VerificarInicializarDefiniconCargada()

                        ' Asignamos los datos de Bases de Datos para poder obtener las Sentencias de Creación del Procedimiento
                        .BaseDatosInfraestructura = SeleccionBaseDatosAccionesInfraestructura.SelectedItem.ToString.Trim
                        .BaseDatosDestinoFlujo = SeleccionBaseDatosDestinoFlujo.SelectedItem.ToString.Trim

                        'Ejecutamos el Proceso para la Creación del Flujo del Procedimiento
                        .Crear(entornosAdicionalesSeleccionados:=SeleccionEntornosDestinoFlujoAdicionales.SelectedItems.Cast(Of String).ToList)

                        ' Comprobamos si en el Flujo tenemos una Unión de Ramas Paralelas
                        If .HayUnionRamasParalelas Then
                            ' Se va a mostrar un Aviso del Flujo Generado, pero indicando el Número de Unión de Ramas Paralelas que tiene
                            tituloAviso = "AVISO"
                            mensajeAviso = String.Format("Se ha generado el Flujo del Procedimiento: [{0}] {1}{2}{2}{3}Número de Unión de Ramas Paralelas: {4}", .DatosGenerales.IdentificadorFlujo, .DatosGenerales.Descripcion, vbCrLf, vbTab, .NumeroUnionRamasParalelas)

                        Else
                            ' Se va a mostrar un Aviso del Flujo Generado
                            tituloAviso = "Información"
                            mensajeAviso = String.Format("Se ha generado el Flujo del Procedimiento: [{0}] {1}", .DatosGenerales.IdentificadorFlujo, .DatosGenerales.Descripcion)
                        End If
                    End With

                Else
                    Throw New Exception("No se ha podido cargar la Definicón del Procedimiento del XML.")
                End If

            Else
                ' No estamos en la última versión de Pasarela, así que mostramos un mensaje
                Throw New Exception("No está usando la Última Versión de Pasarela")
            End If

        Catch ex As Exception
            ' Inicializamos las variables para mostrar el aviso
            mensajeAviso = ex.Message
            tituloAviso = "ERROR"

        Finally
            MessageBox.Show(mensajeAviso, tituloAviso, MessageBoxButtons.OK)
            CrearProcedimiento.Enabled = True
        End Try
    End Sub

    Private Sub ValidarCamposCreacionProcedimiento()
        ' Comprobamos que se haya seleccionado una Base de Datos Destino para el Procedimiento a Crear
        If IsNothing(SeleccionBaseDatosDestinoFlujo.SelectedItem) OrElse String.IsNullOrEmpty(SeleccionBaseDatosDestinoFlujo.SelectedItem.ToString.Trim) Then
            Throw New Exception("Se debe seleccionar la Base de Datos en la que se debe crear el Flujo del Procedimiento.")
        End If

        ' Comprobamos que se haya seleccionado una Base de Datos de Acciones de Infraestructura para el Procedimiento a Crear
        If IsNothing(SeleccionBaseDatosAccionesInfraestructura.SelectedItem) OrElse String.IsNullOrEmpty(SeleccionBaseDatosAccionesInfraestructura.SelectedItem.ToString.Trim) Then
            Throw New Exception("Se debe seleccionar la Base de Datos de las Acciones de Infraestructura para crear el Flujo del Procedimiento.")
        End If

        ' Comprobamos que se haya seleccionado un XML
        If String.IsNullOrEmpty(XML.Text.Trim) Then
            Throw New Exception("Se debe seleccionar un Fichero XML para generar el Procedimiento.")
        End If
    End Sub

    Private Function ObtenerFicheroXML() As FileInfo
        ' Comprobamos que exista el Fichero TXT
        Dim ficheroXML As New FileInfo(XML.Text.Trim)
        If Not ficheroXML.Exists() Then
            Throw New Exception(String.Format("No existe el Fichero XML seleccionado: {0}", ficheroXML.FullName))
        End If

        Return ficheroXML
    End Function

    Private Sub CrearProcedimientoDeXML_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cargamos la Versión del Ejecutable
        _versionEjecutable = Assembly.GetExecutingAssembly().GetName().Version

        ' Cargamos el nombre del Ejecutable
        Text = ObtenerNombreVentana()

        ' Cargamos el campo de selección para las Bases de Datos de Destino para los Flujos, y seleccionamos el primer elemento
        SeleccionBaseDatosDestinoFlujo.Items.AddRange(Utilidades.ObtenerBaseDatosDestinoFlujos())
        SeleccionBaseDatosDestinoFlujo.SelectedIndex = 0

        ' Cargamos el campo de selección múltiple para los Entornos de Destino para los Flujos
        SeleccionEntornosDestinoFlujoAdicionales.Items.AddRange(Utilidades.ObtenerEntornosDestinoFlujosAdicionales())

        ' Cargamos el campo de selección para las Bases de Datos de Acciones de Infraestructura, y seleccionamos el primer elemento
        SeleccionBaseDatosAccionesInfraestructura.Items.AddRange(Utilidades.ObtenerBaseDatosAccionesInfraestructura())
        SeleccionBaseDatosAccionesInfraestructura.SelectedIndex = 0

        ' Comprobamos si es la Versión vigente
        If Not EsVersionVigente() Then
            ' No estamos en la última versión de Pasarela, así que mostramos un mensaje
            MessageBox.Show("No está usando la Última Versión de Pasarela", "ERROR", MessageBoxButtons.OK)
        End If
    End Sub

    Private Function ObtenerNombreVentana() As String
        With _versionEjecutable
            Return String.Format("Crear Procedimiento y Flujo HIDRA a partir de un XML [{0}.{1}.{2}.{3}]", .Major, .Minor, .Build, .Revision)
        End With
    End Function

    Private Function EsVersionVigente()
        ' Obtenemos la Versión de Pasarela Vigente
        _versionVigentePasarela = AccesoDatos.ObtenerVersionPasarela(Utilidades.ObtenerEntornos(Nothing, String.Empty).ObtenerCadenaConexionInfraestructuraEntornoPredeterminado())

        ' Verificamos si la Versión de Pasarela Vigente coincide con la Versión de la Aplicación
        With _versionVigentePasarela
            Return If(.Major.Equals(_versionEjecutable.Major) AndAlso .Minor.Equals(_versionEjecutable.Minor) AndAlso .Build.Equals(_versionEjecutable.Build) AndAlso .Revision.Equals(_versionEjecutable.Revision), True, False)
        End With
    End Function
End Class

