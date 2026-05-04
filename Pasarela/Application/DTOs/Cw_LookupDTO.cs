using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_LookupDTO
    {
        [Key]
        public int LU_ID;

        [Required]
        [StringLength(maximumLength: 5)]
        public string LU_ABBREVIATION;

        [Required]
        [StringLength(maximumLength: 254)]
        public string LU_NAME;
    }
}
