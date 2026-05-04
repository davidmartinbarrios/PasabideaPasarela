Imports System.Xml.Serialization

Namespace Bizkaia.Pasarela
    <Serializable()>
    Public Enum TipoTramite
        <XmlEnum("AUTOMATICO")>
        Automatico

        <XmlEnum("MANUAL")>
        Manual

        <XmlEnum("RECEPCION_ACUSE_RECIBO")>
        RecepcionAcuseRecibo

        <XmlEnum("COMPROBACION_REQUISITOS")>
        ComprobacionRequisitos

        <XmlEnum("FIRMA_SELLO")>
        FirmaSello

        <XmlEnum("PLANIFICADO")>
        Planificado

        <XmlEnum("PROCESO_PLANIFICADO")>
        ProcesoPlanificado

        <XmlEnum("PORTA_FIRMAS")>
        PortaFirmas

        <XmlEnum("RECHAZO_FIRMAS")>
        RechazoFirmas

        <XmlEnum("ORDEN_NOTIFICACION")>
        OrdenNotificacion

        <XmlEnum("PROCESAR_NOTIFICACION")>
        ProcesarNotificacion

        <XmlEnum("ORDEN_NOTIFICACION_MANUAL")>
        OrdenNotificacionManual

        ' Trámites Ficticios
        <XmlEnum("UNION_RAMAS")>
        UnionRamas

        <XmlEnum("FICTICIO")>
        Ficticio

        ' Bloques de Tramitación Común
        <XmlEnum("NOTIFICACION")>
        Notificacion

        <XmlEnum("FIRMA")>
        Firma

        <XmlEnum("BKON")>
        BKON

        <XmlEnum("PUESTA_MANIFIESTO")>
        PuestaManifiesto

        ' No se deberían usar
        <XmlEnum("PORTA_FIRMAS_CONTROL-M")>
        PortaFirmasControlM

        <XmlEnum("AUTOMATICO_CONTROL-M")>
        AutomaticoControlM
    End Enum

    <Serializable()>
    Public Enum TipoAlerta
        <XmlEnum("1")>
        Tramite

        <XmlEnum("2")>
        Resolucion
    End Enum

    <Serializable()>
    Public Enum TipoDia
        <XmlEnum("")>
        Defecto

        <XmlEnum("1")>
        Naturales

        <XmlEnum("2")>
        Laborables

        <XmlEnum("3")>
        Habiles
    End Enum

    <Serializable()>
    Public Enum TipoPaso
        <XmlEnum("ACCION")>
        Accion

        <XmlEnum("INICIALIZACION_VARIABLES")>
        InicializacionVariables

        <XmlEnum("EVALUACION")>
        Evaluacion

        <XmlEnum("SALTO")>
        Salto

        <XmlEnum("PARADA")>
        Parada
    End Enum

    <Serializable()>
    Public Enum TipoOperador
        <XmlEnum("")>
        Defecto

        <XmlEnum("=")>
        Igual

        <XmlEnum("!=")>
        Diferente

        <XmlEnum(">")>
        Mayor

        <XmlEnum("<")>
        Menor

        <XmlEnum(">=")>
        MayorIgual

        <XmlEnum("<=")>
        MenorIgual
    End Enum

    <Serializable()>
    Public Enum TipoOperadorUnion
        <XmlEnum("")>
        None

        <XmlEnum("AND")>
        Y
    End Enum

    <Serializable()>
    Public Enum Origen_VariableValor
        <XmlEnum("")>
        None

        <XmlEnum("FORMULARIO_TRAMITE")>
        FormularioTramite

        <XmlEnum("GENERAL")>
        General
    End Enum

    <Serializable()>
    Public Enum TipoFirma
        <XmlEnum("")>
        None

        <XmlEnum("PREDEFINIDA")>
        Predefinida

        <XmlEnum("NIVELES")>
        Niveles

        <XmlEnum("CSVORGANICO")>
        CSVOrganico

        <XmlEnum("ADHOCK")>
        AdHock

        <XmlEnum("PROPIA")>
        Propia

        <XmlEnum("PROPIA+NIVELES")>
        Propia_Y_Niveles

        <XmlEnum("DEDUCIDA-EJECUCION")>
        Deducida_Ejecucion
    End Enum

    Friend Enum TipoTratamientoTramite
        Manual
        Automatico
    End Enum

    Friend Enum TipoTratamientoPasos
        TramiteARTEZ
        TramiteFicticio
        InicioTramitacion
    End Enum
End Namespace