using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Process_BreakDTO
    {
        [Key]
        public int PR_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string PR_NAME;
    }
}
