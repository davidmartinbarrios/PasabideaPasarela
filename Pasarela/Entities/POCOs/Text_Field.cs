using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Text_Field
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
