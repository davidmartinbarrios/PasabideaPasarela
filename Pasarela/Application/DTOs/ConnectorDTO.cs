using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class ConnectorDTO
    {
        [Key]
        public int CO_ID;

        [Required]
        public string CO_CONDITION;
        
        [Required]
        [StringLength(maximumLength: 8)]
        public string MODEL_NAME;
    }
}
