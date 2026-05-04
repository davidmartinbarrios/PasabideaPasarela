using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class AttributeDTO
    {
        [Key]
        public int AT_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string AT_NAME;
    }
}
