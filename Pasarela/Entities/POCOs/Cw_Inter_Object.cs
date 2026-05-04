using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Cw_Inter_Object
    {
        [Required]
        public int ANO_ID_BELOW;

        [Required]
        [StringLength(maximumLength: 254)]
        public string GO_NAME;
    }
}
