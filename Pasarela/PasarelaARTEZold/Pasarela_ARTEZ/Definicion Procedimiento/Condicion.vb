Imports System.Xml.Serialization
Imports Bizkaia.Pasarela.Utilidades.Utilidades

Namespace Bizkaia.Pasarela
    <Serializable()>
    <XmlType>
    Public NotInheritable Class Condicion
        '<XmlElement("OperadorUnion", IsNullable:=True)>
        'Public Property OperadorUnion As TipoOperadorUnion?

        <XmlElement("Operando1")>
        Public Property Operando1 As String

        <XmlElement("Operador")>
        Public Property Operador As TipoOperador

        <XmlElement("Operando2")>
        Public Property Operando2 As String

        Friend Function ObtenerValorOperando1(ByRef datosGeneralesTramite As DatosGeneralesTramite, ordenTramite As Integer) As String
            Dim valorOperando As String = String.Empty

            ' Comprobamos si tenemos informado el Operando 1
            If Not String.IsNullOrEmpty(Operando1) Then
                valorOperando = ObtenerValorParaParametro(Operando1)

            Else
                ' Comprobamos el tipo de Trámite para determinar de donde obtener el primer operando
                With datosGeneralesTramite
                    valorOperando = If(.TipoTramite.Equals(TipoTramite.Manual) OrElse .TipoTramite.Equals(TipoTramite.ComprobacionRequisitos) OrElse .TipoTramite.Equals(TipoTramite.Planificado) OrElse .TipoTramite.Equals(TipoTramite.PortaFirmas) OrElse
                                       .TipoTramite.Equals(TipoTramite.RechazoFirmas) OrElse .TipoTramite.Equals(TipoTramite.ProcesarNotificacion) OrElse .TipoTramite.Equals(TipoTramite.PortaFirmasControlM) OrElse .TipoTramite.Equals(TipoTramite.AutomaticoControlM),
                                        String.Format("@{0}!RESPUESTA", .ObtenerNombrePathFROM()),
                                        String.Format("@INICIO!DECISION_T_{0}", ordenTramite))
                End With
            End If

            Return valorOperando
        End Function

        Friend Function ObtenerValorOperando2() As String
            Return If(Not String.IsNullOrEmpty(Operando2), ObtenerValorParaParametro(Operando2), String.Empty)
        End Function

        Friend Function ObtenerOperador() As String
            Dim descripcionOperador As String = String.Empty

            Select Case Operador
                Case TipoOperador.Defecto, TipoOperador.Igual
                    descripcionOperador = Constantes.IGUAL

                Case TipoOperador.Diferente
                    descripcionOperador = Constantes.DIFERENTE

                Case TipoOperador.Mayor
                    descripcionOperador = Constantes.MAYOR

                Case TipoOperador.Menor
                    descripcionOperador = Constantes.MENOR

                Case TipoOperador.MayorIgual
                    descripcionOperador = Constantes.MAYOR_IGUAL

                Case TipoOperador.MenorIgual
                    descripcionOperador = Constantes.MENOR_IGUAL
            End Select

            Return descripcionOperador
        End Function
    End Class
End Namespace