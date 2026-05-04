using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class EntityDTO
    {
        [Key]
        public int EN_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string EN_NAME;
    }
}
