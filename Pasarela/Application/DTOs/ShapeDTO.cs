using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class ShapeDTO
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
