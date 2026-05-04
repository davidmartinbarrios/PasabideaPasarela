using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Shape
    {
        [Key]
        public int NUM_SEQ;

        [Required]
        public int ANO_TABNR;

        [Required]
        public int ANO_ID;

        [Required]
        public int SH_Y;

        [Required]
        public int SH_X;

        [Required]
        public string OT_NAME;

        [Required]
        public int DI_ID;

    }
}
