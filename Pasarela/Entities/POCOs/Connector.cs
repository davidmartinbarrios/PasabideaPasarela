using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Connector
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
