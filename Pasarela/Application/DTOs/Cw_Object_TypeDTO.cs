using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_Object_TypeDTO
    {
        [Key]
        public int OT_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string OT_NAME;
    }
}
