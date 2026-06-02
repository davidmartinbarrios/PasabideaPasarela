using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Lantik.Pasabidea.Core.Business;
using Lantik.Pasabidea.Core.Entities;
using Lantik.Pasabidea.Core.Models.Mugi;

namespace Pasabidea.Core.Business
{
    public static class MugiBusiness
    {
        //public static async Task GenerarAsync(string connectionStringMugi)
        public static void Generar()
        {
            var business = new PasarelaMugiBusiness();

            var tramites = new List<MugiTramiteDTO>
            {
                new MugiTramiteDTO { Codigo = "TRAMITE_1", Descripcion = "Trámite 1", Orden = 1, EsAutomatico = false },
                new MugiTramiteDTO { Codigo = "TRAMITE_2", Descripcion = "Trámite 2", Orden = 2, EsAutomatico = true },
                new MugiTramiteDTO { Codigo = "TRAMITE_3", Descripcion = "Trámite 3", Orden = 3, EsAutomatico = false },
                new MugiTramiteDTO { Codigo = "TRAMITE_4", Descripcion = "Trámite 4", Orden = 4, EsAutomatico = true }
            };

            var resultado = business.GenerarFlujoBasico(
                //mugiConnectionStringName: "BD_MUGI",
                flow: "TEST_PASABIDEA",
                version: 1,
                tramites: tramites,
                flowName: "TEST_PASABIDEA",
                comments: "Flujo generado desde Pasabidea.Core",
                fechaActivacion: DateTime.Today,
                sobrescribir: true,
                dryRun: false); //dryRun: true); 

            if (resultado == 0)
                Trace.WriteLine("Generación correcta.");
            else
                Trace.WriteLine("Error generando flujo.");

        }
    }
}
