using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Text_FieldDTO
    {
        [Required]
        public int ANO_TABNR;

        [Required]
        public int ANO_ID;

        [Required]
        public int TT_ATTRIBUTE;

        public string VALUE;
    }
}
