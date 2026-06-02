using Pasabidea.Core.Entities.XML;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Pasabidea.Core.Helpers
{

    public static class XmlHelper
    {
        public static XDocument Generar(Borrador borrador)
        {
            var p = borrador.Procedimiento;

            return new XDocument(
                new XElement("Borrador",
                    new XElement("ConfiguracionExterna"),

                    new XElement("Procedimiento",
                        new XAttribute("DescriptorProcedimientoBLT", p.DescriptorProcedimientoBLT),
                        new XAttribute("DescripcionProcedimientoBLTES", p.DescripcionProcedimientoBLTES),
                        new XAttribute("DescripcionProcedimientoBLTEU", p.DescripcionProcedimientoBLTEU),
                        new XAttribute("DescripcionVersionProcedimientoBLTES", p.DescripcionVersionProcedimientoBLTES),
                        new XAttribute("DescripcionVersionProcedimientoBLTEU", p.DescripcionVersionProcedimientoBLTEU),
                        new XAttribute("BloqueTramites", Bool(p.BloqueTramites)),
                        new XAttribute("Discrecional", Bool(p.Discrecional)),
                        new XAttribute("CodigoTipoOrigen", p.CodigoTipoOrigen),
                        new XAttribute("CodigoTipoSilencioAdtvo", p.CodigoTipoSilencioAdtvo),
                        new XAttribute("PublicaSede", Bool(p.PublicaSede)),
                        new XAttribute("CodigoModoTramitacion", p.CodigoModoTramitacion),
                        new XAttribute("AdmitePuestaManifiesto", Bool(p.AdmitePuestaManifiesto)),
                        new XAttribute("GeneraExpediente", Bool(p.GeneraExpediente)),
                        new XAttribute("IdentificadoPorPersona", Bool(p.IdentificadoPorPersona)),
                        new XAttribute("Securizado", Bool(p.Securizado)),

                        new XElement("DominioProcedimiento",
                            new XAttribute("DescriptorDominio", p.Dominio.DescriptorDominio),
                            new XAttribute("DescripcionDominioES", p.Dominio.DescripcionDominioES),
                            new XAttribute("DescripcionDominioEU", p.Dominio.DescripcionDominioEU)
                        ),

                        new XElement("PatronProcedimiento",
                            new XAttribute("DescriptorPatron", p.Patron.DescriptorPatron),
                            new XAttribute("DescripcionPatronES", p.Patron.DescripcionPatronES),
                            new XAttribute("DescripcionPatronEU", p.Patron.DescripcionPatronEU)
                        ),

                        new XElement("ProcesoProcedimiento",
                            new XAttribute("DescriptorProceso", p.Proceso.DescriptorProceso),
                            new XAttribute("DescripcionProcesoES", p.Proceso.DescripcionProcesoES),
                            new XAttribute("DescripcionProcesoEU", p.Proceso.DescripcionProcesoEU)
                        ),

                        new XElement("MapaProcesoProcedimiento",
                            new XAttribute("DescriptorMapaProceso", p.MapaProceso.DescriptorMapaProceso),
                            new XAttribute("DescripcionMapaProcesoES", p.MapaProceso.DescripcionMapaProcesoES),
                            new XAttribute("DescripcionMapaProcesoEU", p.MapaProceso.DescripcionMapaProcesoEU)
                        ),

                        new XElement("MacroProcesoProcedimiento",
                            new XAttribute("DescriptorMacroProceso", p.MacroProceso.DescriptorMacroProceso),
                            new XAttribute("DescripcionMacroProcesosES", p.MacroProceso.DescripcionMacroProcesosES),
                            new XAttribute("DescripcionMacroProcesosEU", p.MacroProceso.DescripcionMacroProcesosEU)
                        ),

                        new XElement("ElementosModeladosProcedimiento",
                            p.ElementosModelados.Select(e =>
                                new XElement("ElementoModelado",
                                    new XAttribute("IdentificadorElementoModelado", e.IdentificadorElementoModelado),
                                    new XAttribute("DescriptorTramite", e.DescriptorTramite),
                                    new XAttribute("DescripcionElementoModeladoES", e.DescripcionElementoModeladoES),
                                    new XAttribute("DescripcionElementoModeladoEU", e.DescripcionElementoModeladoEU),
                                    new XAttribute("GeneraTarea", Bool(e.GeneraTarea)),
                                    new XAttribute("Orden", e.Orden),
                                    new XAttribute("MarcaTareaNominativa", e.MarcaTareaNominativa)
                                )
                            )
                        )
                    )
                )
            );
        }

        private static string Bool(bool value) => value ? "TRUE" : "FALSE";
    }

}
