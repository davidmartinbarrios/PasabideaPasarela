using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class EventDTO
    {
        [Key]
        public int EV_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string EV_NAME;
    }
}
