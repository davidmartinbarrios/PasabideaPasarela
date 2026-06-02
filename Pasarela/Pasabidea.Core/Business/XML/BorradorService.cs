using System;
using System.Linq;
using System.Xml.Linq;
using Lantik.Pasabidea.Core.Data.sqlRepository;
using Pasabidea.Core.Data.XML;
using Pasabidea.Core.Entities.XML;
using Pasabidea.Core.Helpers;


namespace Pasabidea.Core.Business.XML
{

    public class BorradorService
    {
        public class ProcedimientoService
        {
            private readonly ProcedimientoRepository _repository;
            private readonly XMLRepository _xmlrepository = new XMLRepository();
            public ProcedimientoService(ProcedimientoRepository repository)
            {
                _repository = repository;
            }

            public XDocument GenerarXml()
            {
                // 1. Obtener datos de BD
                var procedimiento = _repository.Get();

                // 2. Validar datos (muy importante en real)
                Validar(procedimiento);

                // 3. Construir objeto raíz
                var borrador = ConstruirBorrador(procedimiento);

                // 4. Generar XML
                var xml = XmlHelper.Generar(borrador);

                return xml;
            }

            public string GenerarXmlString()
            {
                return GenerarXml().ToString();
            }

            public void GuardarXml(string ruta)
            {
                var xml = GenerarXml();
                xml.Save(ruta);
            }

            private Borrador ConstruirBorrador(Procedimiento proc)
            {
                return new Borrador
                {
                    ConfiguracionExterna = new ConfiguracionExterna(),
                    Procedimiento = proc
                };
            }

            private void Validar(Procedimiento proc)
            {
                if (proc == null)
                    throw new Exception("El procedimiento es nulo");

                if (string.IsNullOrEmpty(proc.DescriptorProcedimientoBLT))
                    throw new Exception("DescriptorProcedimientoBLT es obligatorio");

                if (proc.ElementosModelados == null || !proc.ElementosModelados.Any())
                    throw new Exception("Debe existir al menos un ElementoModelado");
            }


            public int GrabarConfiguracionBorrador(string xml)
            {
                return _xmlrepository.GrabarConfiguracionBorrador(xml);
            }


        }

    }

}
