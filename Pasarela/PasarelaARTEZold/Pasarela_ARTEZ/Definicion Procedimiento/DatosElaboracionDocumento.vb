Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    Public NotInheritable Class DatosElaboracionDocumento
        <XmlElement("DocumentoParametrizado", IsNullable:=True)>
        Public Property DocumentoParametrizadoXML As String

        <XmlElement("CodigoDocumento", IsNullable:=True)>
        Public Property CodigoDocumentoXML As String

        <XmlElement("HabilitarPestanaFirma", IsNullable:=True)>
        Public Property HabilitarPestanaFirmaXML As String

        <XmlElement("VariableDocumentoElaborado")>
        Public Property VariableDocumentoElaboradoXML As String

        <XmlElement("VariableDocumentoFirmadoFueraARTEZ", IsNullable:=True)>
        Public Property VariableDocumentoFirmadoFueraARTEZXML As String

        <XmlElement("DatosFirma", IsNullable:=True)>
        Public DatosFirma As DatosFirmaTramiteElaboracionDocumento

        <XmlIgnore>
        Friend ReadOnly Property DocumentoParametrizado As Boolean
            Get
                Return If(String.IsNullOrEmpty(DocumentoParametrizadoXML), False, If(DocumentoParametrizadoXML.Equals(Constantes.S), True, False))
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property DatosInformados As Boolean
            Get
                Return If(Not DocumentoParametrizado,
                            If(Not String.IsNullOrEmpty(CodigoDocumentoXML) AndAlso Not String.IsNullOrEmpty(VariableDocumentoElaboradoXML), True, False),
                            If(Not String.IsNullOrEmpty(VariableDocumentoElaboradoXML), True, False))
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property HabilitarPestanaFirma As Boolean
            Get
                Dim habilitar As Boolean = False

                If Not String.IsNullOrEmpty(HabilitarPestanaFirmaXML) Then
                    Select Case HabilitarPestanaFirmaXML.ToUpper
                        Case Constantes.S
                            habilitar = True

                        Case Constantes.VALOR_TRUE
                            habilitar = True

                        Case Else
                            If HabilitarPestanaFirmaXML.StartsWith("@") Then
                                habilitar = True
                            End If
                    End Select
                End If

                Return habilitar
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property ValorValidoHabilitarPestanaFirma As Boolean
            Get
                With HabilitarPestanaFirmaXML.ToUpper
                    Return If(.StartsWith("@") OrElse .Equals(Constantes.S) OrElse .Equals(Constantes.VALOR_TRUE), True, False)
                End With

            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property ValorHabilitarPestanaFirma As String
            Get
                ' Obtenemos el Valor para el Parámetro
                Dim valorParametro As String = ObtenerValorParaParametro(HabilitarPestanaFirmaXML)

                ' Comprobamos si nos devuelve un valor constante
                If Not valorParametro.StartsWith("@") Then
                    ' Nos devuelve un valor constante, así que independientemente del valor recuperado, hay que enviar el literal "true"
                    valorParametro = Constantes.LITERAL_TRUE
                End If

                Return valorParametro
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property CodigoDocumento As String
            Get
                Return ObtenerValorParaParametro(CodigoDocumentoXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property VariableDocumentoElaborado As String
            Get
                Return If(Not VariableDocumentoElaboradoXML.StartsWith("@"), String.Format("@{0}", VariableDocumentoElaboradoXML), VariableDocumentoElaboradoXML)
            End Get
        End Property

        <XmlIgnore>
        Friend ReadOnly Property VariableDocumentoFirmadoFueraARTEZ As String
            Get
                Return If(Not VariableDocumentoFirmadoFueraARTEZXML.StartsWith("@"), String.Format("@{0}", VariableDocumentoFirmadoFueraARTEZXML), VariableDocumentoFirmadoFueraARTEZXML)
            End Get
        End Property

        '<XmlIgnore>
        'Friend ReadOnly Property HayVariableFechaFirma As Boolean
        '    Get
        '        Return If(Not String.IsNullOrEmpty(VariableFechaFirmaXML), True, False)
        '    End Get
        'End Property

        '<XmlIgnore>
        'Friend ReadOnly Property VariableFechaFirma As String
        '    Get
        '        Return If(Not VariableFechaFirmaXML.StartsWith("@"), String.Format("@{0}", VariableFechaFirmaXML), VariableFechaFirmaXML)
        '    End Get
        'End Property

        '<XmlIgnore>
        'Friend ReadOnly Property VariableIdntSolicitudFirma As String
        '    Get
        '        Return If(Not VariableSolicitudFirmaXML.StartsWith("@"), String.Format("@{0}", VariableSolicitudFirmaXML), VariableSolicitudFirmaXML)
        '    End Get
        'End Property
    End Class
End Namespace