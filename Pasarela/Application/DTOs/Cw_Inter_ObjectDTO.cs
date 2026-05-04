using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_Inter_ObjectDTO
    {
        [Required]
        public int ANO_ID_BELOW;

        [Required]
        [StringLength(maximumLength: 254)]
        public string GO_NAME;
    }
}
