using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_Data_UsageDTO
    {
        [Required]
        public int DM_DELETES;

        [Required]
        public int DM_INSERTS;
    }
}
