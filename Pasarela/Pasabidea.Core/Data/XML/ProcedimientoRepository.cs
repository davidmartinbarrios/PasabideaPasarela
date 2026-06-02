using Pasabidea.Core.Entities.XML;
using System.Collections.Generic;

namespace Pasabidea.Core.Data.XML
{
    public class ProcedimientoRepository
    {
        public Procedimiento Get()
        {
            return new Procedimiento
            {
                DescriptorProcedimientoBLT = "TEST-PA999910",
                DescripcionProcedimientoBLTES = "Testeo Pasarela Reducido",
                DescripcionProcedimientoBLTEU = "EU-Testeo Pasarela Reducido",
                DescripcionVersionProcedimientoBLTES = "Testeo Pasarela Reducido",
                DescripcionVersionProcedimientoBLTEU = "EU-Testeo Pasarela Reducido",

                BloqueTramites = false,
                Discrecional = false,
                CodigoTipoOrigen = 0,
                CodigoTipoSilencioAdtvo = "POSITIVO",
                PublicaSede = true,
                CodigoModoTramitacion = "HIDRA",
                AdmitePuestaManifiesto = true,
                GeneraExpediente = true,
                IdentificadoPorPersona = false,
                Securizado = false,

                Dominio = new DominioProcedimiento
                {
                    DescriptorDominio = "Ayudas",
                    DescripcionDominioES = "Ayudas",
                    DescripcionDominioEU = "Laguntzak"
                },

                Patron = new PatronProcedimiento
                {
                    DescriptorPatron = "DESCRIPTOR_PATRPROC_3004",
                    DescripcionPatronES = "JJGG",
                    DescripcionPatronEU = "EU-JJGG"
                },

                Proceso = new ProcesoProcedimiento
                {
                    DescriptorProceso = "PRCS_PROCDOMIAYUDSUBV_20221006163415",
                    DescripcionProcesoES = "Proceso del Dominio: Ayudas y subvenciones",
                    DescripcionProcesoEU = "Proceso del Dominio: Ayudas y subvenciones"
                },

                MapaProceso = new MapaProcesoProcedimiento
                {
                    DescriptorMapaProceso = "MPPRC_MAPADOMIAYUDSUBV_20221006163415",
                    DescripcionMapaProcesoES = "MapaProcesos del Dominio: Ayudas y subvenciones",
                    DescripcionMapaProcesoEU = "MapaProcesos del Dominio: Ayudas y subvenciones"
                },

                MacroProceso = new MacroProcesoProcedimiento
                {
                    DescriptorMacroProceso = "MCPRC_MACRDOMIAYUDSUBV_20221006163415",
                    DescripcionMacroProcesosES = "MacroProceso del Dominio: Ayudas y subvenciones",
                    DescripcionMacroProcesosEU = "MacroProceso del Dominio: Ayudas y subvenciones"
                },

                ElementosModelados = new List<ElementoModelado>
                {
                    new ElementoModelado
                    {
                        IdentificadorElementoModelado = "INICIAL",
                        DescriptorTramite = "TRAM_HIDRATareaAutomaticaVaciaSinDecision",
                        DescripcionElementoModeladoES = "Iniciar tramitación",
                        DescripcionElementoModeladoEU = "Tramitazioa hasi",
                        GeneraTarea = true,
                        Orden = 1,
                        MarcaTareaNominativa = 0
                    },
                    new ElementoModelado
                    {
                        IdentificadorElementoModelado = "FINAL",
                        DescriptorTramite = "TRAM_HIDRATareaAutomaticaVaciaSinDecision",
                        DescripcionElementoModeladoES = "Finalizar tramitación",
                        DescripcionElementoModeladoEU = "Izapidetzea amaitu",
                        GeneraTarea = true,
                        Orden = 2,
                        MarcaTareaNominativa = 0
                    },
                    new ElementoModelado
                    {
                        IdentificadorElementoModelado = "ANULACION",
                        DescriptorTramite = "TRAM_HIDRATareaAutomaticaVaciaSinDecision",
                        DescripcionElementoModeladoES = "Anular tramitación",
                        DescripcionElementoModeladoEU = "Tramitazioa ezeztatu",
                        GeneraTarea = true,
                        Orden = 3,
                        MarcaTareaNominativa = 0
                    },
                    new ElementoModelado
                    {
                        IdentificadorElementoModelado = "TRAMITE_1",
                        DescriptorTramite = "TRAM_HIDRATareaAutomaticaVaciaSinDecision",
                        DescripcionElementoModeladoES = "Actualización",
                        DescripcionElementoModeladoEU = "EU-Actualización",
                        GeneraTarea = true,
                        Orden = 4,
                        MarcaTareaNominativa = 0
                    }
                }
            };
        }
    }

}
