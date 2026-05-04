using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Process_Break
    {
        [Key]
        public int PR_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string PR_NAME;
    }
}
