using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasabidea.Core.Entities.XML
{
    public class Borrador
    {
        public ConfiguracionExterna ConfiguracionExterna { get; set; } = new ConfiguracionExterna();
        public Procedimiento Procedimiento { get; set; }
    }

    public class ConfiguracionExterna
    {
    }
    public class Procedimiento
    {
        public string DescriptorProcedimientoBLT { get; set; }
        public string DescripcionProcedimientoBLTES { get; set; }
        public string DescripcionProcedimientoBLTEU { get; set; }
        public string DescripcionVersionProcedimientoBLTES { get; set; }
        public string DescripcionVersionProcedimientoBLTEU { get; set; }

        public bool BloqueTramites { get; set; }
        public bool Discrecional { get; set; }
        public int CodigoTipoOrigen { get; set; }
        public string CodigoTipoSilencioAdtvo { get; set; }
        public bool PublicaSede { get; set; }
        public string CodigoModoTramitacion { get; set; }
        public bool AdmitePuestaManifiesto { get; set; }
        public bool GeneraExpediente { get; set; }
        public bool IdentificadoPorPersona { get; set; }
        public bool Securizado { get; set; }

        public DominioProcedimiento Dominio { get; set; }
        public PatronProcedimiento Patron { get; set; }
        public ProcesoProcedimiento Proceso { get; set; }
        public MapaProcesoProcedimiento MapaProceso { get; set; }
        public MacroProcesoProcedimiento MacroProceso { get; set; }

        public List<ElementoModelado> ElementosModelados { get; set; } = new List<ElementoModelado>();
    }

    public class DominioProcedimiento
    {
        public string DescriptorDominio { get; set; }
        public string DescripcionDominioES { get; set; }
        public string DescripcionDominioEU { get; set; }
    }

    public class PatronProcedimiento
    {
        public string DescriptorPatron { get; set; }
        public string DescripcionPatronES { get; set; }
        public string DescripcionPatronEU { get; set; }
    }

    public class ProcesoProcedimiento
    {
        public string DescriptorProceso { get; set; }
        public string DescripcionProcesoES { get; set; }
        public string DescripcionProcesoEU { get; set; }
    }

    public class MapaProcesoProcedimiento
    {
        public string DescriptorMapaProceso { get; set; }
        public string DescripcionMapaProcesoES { get; set; }
        public string DescripcionMapaProcesoEU { get; set; }
    }

    public class MacroProcesoProcedimiento
    {
        public string DescriptorMacroProceso { get; set; }
        public string DescripcionMacroProcesosES { get; set; }
        public string DescripcionMacroProcesosEU { get; set; }
    }

    public class ElementoModelado
    {
        public string IdentificadorElementoModelado { get; set; }
        public string DescriptorTramite { get; set; }
        public string DescripcionElementoModeladoES { get; set; }
        public string DescripcionElementoModeladoEU { get; set; }
        public bool GeneraTarea { get; set; }
        public int Orden { get; set; }
        public int MarcaTareaNominativa { get; set; }
    }
}
