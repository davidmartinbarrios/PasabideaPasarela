using System.Collections.Generic;

namespace Lantik.Pasabidea.Core.Entities.DTOs
{
    public sealed class ProcedimientoDTO
    {
        public int RaizDiId { get; set; }
        public string RaizDiName { get; set; }

        public int DiId { get; set; }
        public int AnoId { get; set; }
        public int AnoTabnr { get; set; }

        public string DiName { get; set; }
        public string DiTitle { get; set; }

        public string TipoLogico { get; set; }
        public string DiTypeDesc { get; set; }

        public int Nivel { get; set; }
        public string RutaIds { get; set; }

        public List<ProcedimientoDTO> Hijos { get; set; } = new List<ProcedimientoDTO>();

        public bool EsGrupoProcedimiento => TipoLogico == "GRUPO_PROCEDIMIENTO";
        public bool EsProcedimiento => TipoLogico == "PROCEDIMIENTO";

        public override string ToString()
        {
            return DiName;
        }
    }
}