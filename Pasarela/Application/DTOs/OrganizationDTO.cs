using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class OrganizationDTO
    {
        [Key]
        public int OU_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string OU_NAME;

        public int OU_TYPE;
    }
}
