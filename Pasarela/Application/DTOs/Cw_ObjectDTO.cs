using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class Cw_ObjectDTO
    {
        [Key]
        public int GO_ID;

        [Required]
        public int OT_ID;

        [Required]
        public string GO_NAME;

        public string USERDEFINED;
    }
}
