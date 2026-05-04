using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Cw_Lookup
    {
        [Key]
        public int LU_ID;

        [Required]
        [StringLength(maximumLength: 5)]
        public string LU_ABBREVIATION;

        [Required]
        [StringLength(maximumLength: 254)]
        public string LU_NAME;
    }
}
