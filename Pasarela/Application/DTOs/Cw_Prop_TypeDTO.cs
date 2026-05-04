using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_Prop_TypeDTO
    {
        [Key]
        public int PPT_UUID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string PPT_NAME;

        public int PPT_DATA_TYPE;
    }
}
