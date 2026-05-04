using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Attribute
    {
        [Key]
        public int AT_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string AT_NAME;
    }
}
