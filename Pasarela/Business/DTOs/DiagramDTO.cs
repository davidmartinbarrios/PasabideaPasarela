using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Business.DTOs
{
    public class DiagramDTO
    {
        [Key]
        public string PROCEDIMIENTO;

        [Key]
        public int ORDEN_N1;

        [Key]
        public int ORDEN_N2;

        [Key]
        public int ORDEN_N3;

        [Key]
        public int ORDEN_N4;

        [Key]
        public int ORDEN_N5;

        [StringLength(maximumLength: 250)]
        public string CAT_DIAGRAMA;

        [StringLength(maximumLength: 250)]
        public string NOMBRE;

        [StringLength(maximumLength: 4000)]
        public string USERDEFINED;

        public int NIVEL;

        public string ARBOL;

        public string PLAZOTIPO1;

        public string PLAZOTIPO2;

        public string NIV_TRAMIT;

        public string BLOQUEO_EXP;

        public string UNION_RAMAS;

        public string TRAMIT_SIMUL;

        public string TRAM_OCULTO;

        public string IND_VALORVAR;

        public string VUELTA_ATRAS;

        public string NOMBRE_TRAM;
    }
}
