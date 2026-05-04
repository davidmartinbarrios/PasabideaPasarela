Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class DefinicionBloqueTramitacionComunPuestaManifiesto
        <XmlElement("TramiteElaborarPuestaManifiesto")>
        Public Property TramiteElaborarPuestaManifiesto As DatosTramiteManual

        <XmlElement("TramiteVistoBuenoPuestaManifiesto")>
        Public Property TramiteVistoBuenoPuestaManifiesto As DatosTramiteManual

        <XmlElement("TramiteCierreParcialExpediente")>
        Public Property TramiteCierreParcialExpediente As DatosTramiteAutomatico

        <XmlElement("TramiteEsperaGeneracionIndice")>
        Public Property TramiteEsperaGeneracionIndice As DatosTramiteAutomatico

        <XmlElement("Idnt_PuestaDisposicion")>
        Public Property Idnt_PuestaDisposicion As String

        <XmlElement("Variable_Idnt_PuestaManifiesto")>
        Public Property Variable_Idnt_PuestaManifiesto As String

        <XmlIgnore>
        Private ReadOnly Property IdentificadorPuestaManifiesto() As String
            Get
                Return ObtenerValorParaParametro(Idnt_PuestaDisposicion)
            End Get
        End Property

#Region "Métodos Friend"
        Friend Sub Verificar(nombreBloque As String)
            ' Verificamos que los Datos Obligatorios Generales están Informados
            If IsNothing(TramiteElaborarPuestaManifiesto) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informado el Trámite 'Elaborar Puesta de Manifiesto'.", nombreBloque))
            End If

            If IsNothing(TramiteVistoBuenoPuestaManifiesto) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informado el Trámite 'Visto Bueno Puesta Manifiesto'.", nombreBloque))
            End If

            If IsNothing(TramiteCierreParcialExpediente) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informado el Trámite 'Cierre Parcial Expediente'.", nombreBloque))
            End If

            If IsNothing(TramiteEsperaGeneracionIndice) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informado el Trámite 'Espera Gneración Índice'.", nombreBloque))
            End If

            ' Comprobamos si Tenemos los Datos Básicos de los Trámites del Bloque
            If Not TramiteElaborarPuestaManifiesto.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informados los Datos Básicos del Trámite 'Elaborar Puesta de Manifiesto'.", nombreBloque))
            End If

            If Not TramiteVistoBuenoPuestaManifiesto.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informados los Datos Básicos del Trámite 'Visto Bueno Puesta Manifiesto'.", nombreBloque))
            End If

            If Not TramiteCierreParcialExpediente.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informados los Datos Básicos del Trámite 'Cierre Parcial Expediente'.", nombreBloque))
            End If

            If Not TramiteEsperaGeneracionIndice.DatosBasicosInformados Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informados los Datos Básicos del Trámite 'Espera Gneración Índice'.", nombreBloque))
            End If

            ' Comprobamos si está informada la Variable en la que devolver el Identificador de la Puesta de Manifiesto tratada
            If String.IsNullOrEmpty(Variable_Idnt_PuestaManifiesto) Then
                Throw New Exception(String.Format("El Bloque de Tramitación Común de Puesta de Manifiesto '{0}' no tiene informado el Nombre de la Variable para el Identificador de la Puesta de Manifiesto.", nombreBloque))
            End If
        End Sub

        Friend Function ObtenerParametrosEspecificos() As Dictionary(Of String, String)
            Return New Dictionary(Of String, String) From {
                {"@TRAM_ELAB_PUESTA_MANI_IN", TramiteElaborarPuestaManifiesto.Idnt_Elemento},
                {"@TRAM_VB_PUESTA_MANIFI_IN", TramiteVistoBuenoPuestaManifiesto.Idnt_Elemento},
                {"@TRAM_CIERRE_PARCIAL_IN", TramiteCierreParcialExpediente.Idnt_Elemento},
                {"@TRAM_ESPERA_GEN_INDIC_IN", TramiteEsperaGeneracionIndice.Idnt_Elemento},
                {"@MODO_CREA_ELAB_PUESTA_IN", TramiteElaborarPuestaManifiesto.ModoCreacion},
                {"@OBSERVA_ELAB_PUESTA_M_IN", TramiteElaborarPuestaManifiesto.Observaciones},
                {"@MODO_CREA_VB_PUESTA_M_IN", TramiteVistoBuenoPuestaManifiesto.ModoCreacion},
                {"@OBSERVA_VB_PUESTA_MAN_IN", TramiteVistoBuenoPuestaManifiesto.Observaciones},
                {"@SF_TRAM_ELAB_PUESTA_M_IN", TramiteElaborarPuestaManifiesto.SistemaFuncional},
                {"@GU_TRAM_ELAB_PUESTA_M_IN", TramiteElaborarPuestaManifiesto.GrupoUsuarios},
                {"@US_TRAM_ELAB_PUESTA_M_IN", TramiteElaborarPuestaManifiesto.Usuario},
                {"@IDNT_PUESTA_MANIFIEST_IN", IdentificadorPuestaManifiesto},
                {"@IDNT_PUESTA_MANIFIES_OUT", Variable_Idnt_PuestaManifiesto}
            }
        End Function
#End Region
    End Class
End Namespace