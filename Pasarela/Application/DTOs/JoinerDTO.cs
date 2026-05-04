using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class JoinerDTO
    {
        [Key]
        public int ID_CONECTOR;

        [Required]
        public int NUM_SEQ_DESDE;

        [Required]
        public int NUM_SEQ_HASTA;

        [Required]
        public int DI_ID;
    }
}
