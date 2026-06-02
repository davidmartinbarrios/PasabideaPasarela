using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lantik.Pasabidea.Core.Data.Mugi;
using Lantik.Pasabidea.Core.Models.Mugi;

namespace Lantik.Pasabidea.Core.Business
{
    /// <summary>
    /// Capa de negocio para generar la estructura básica de un flujo MUGI.
    /// 
    /// No abre SqlConnection ni SqlTransaction.
    /// La conexión y la transacción quedan encapsuladas en Data/Mugi usando DbContext.
    /// </summary>
    public sealed class PasarelaMugiBusiness
    {
        public int GenerarFlujoBasico(
            //string mugiConnectionStringName,
            string flow,
            int version,
            IEnumerable<MugiTramiteDTO> tramites,
            string flowName = null,
            string comments = null,
            DateTime? fechaActivacion = null,
            bool sobrescribir = false,
            bool dryRun = false)
        {
            try
            {
                Trace.WriteLine("[PasarelaMugiBusiness] Inicio generación flujo MUGI");
                Trace.WriteLine("[PasarelaMugiBusiness] Flow: " + flow);
                Trace.WriteLine("[PasarelaMugiBusiness] Version: " + version);

                ValidarParametros(
                    //mugiConnectionStringName,
                    flow,
                    version,
                    tramites);

                var tramitesOrdenados = tramites
                    .OrderBy(t => t.Orden)
                    .ToList();

                flowName = string.IsNullOrWhiteSpace(flowName) ? flow : flowName.Trim();
                comments = comments ?? string.Empty;

                var fechaInicio = fechaActivacion ?? DateTime.Today;

                var wfFlow = new MugiWfFlow
                {
                    Flow = flow,
                    Version = version,
                    Active = "1",
                    FlowName = flowName,
                    Comments = comments,
                    Running = "1",
                    Start = fechaInicio.ToString("yyyyMMdd"),
                    StopOlderVersions = "0"
                };

                var wfFlowActions = MugiWfFlowActionBuilder.ConstruirAccionesBasicas(
                    flow,
                    version,
                    tramitesOrdenados);

                using (var data = new MugiWfFlowData())
                {
                    var rowsInserted = data.GrabarFlujo(
                        wfFlow,
                        wfFlowActions,
                        sobrescribir,
                        dryRun);

                    Trace.WriteLine("[PasarelaMugiBusiness] Insertadas wfFlowActions: " + rowsInserted);
                }

                Trace.WriteLine("[PasarelaMugiBusiness] Fin generación flujo MUGI OK");

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("[PasarelaMugiBusiness] Error generando flujo MUGI.");
                Trace.WriteLine(ex.ToString());

                return -1;
            }
        }

        private static void ValidarParametros(
            //string mugiConnectionStringName,
            string flow,
            int version,
            IEnumerable<MugiTramiteDTO> tramites)
        {
            //if (string.IsNullOrWhiteSpace(mugiConnectionStringName))
            //    throw new ArgumentException("El nombre de la cadena de conexión MUGI es obligatorio.", nameof(mugiConnectionStringName));

            if (string.IsNullOrWhiteSpace(flow))
                throw new ArgumentException("El código de Flow es obligatorio.", nameof(flow));

            if (version <= 0)
                throw new ArgumentException("La versión debe ser mayor que cero.", nameof(version));

            if (tramites == null)
                throw new ArgumentNullException(nameof(tramites));

            foreach (var tramite in tramites)
            {
                if (tramite == null)
                    throw new ArgumentException("La colección de trámites contiene un elemento nulo.", nameof(tramites));

                if (string.IsNullOrWhiteSpace(tramite.Codigo))
                    throw new ArgumentException("Todos los trámites deben tener código.", nameof(tramites));
            }
        }
    }
}
