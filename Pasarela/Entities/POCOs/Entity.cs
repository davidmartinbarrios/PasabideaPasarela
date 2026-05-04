using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Entity
    {
        [Key]
        public int EN_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string EN_NAME;
    }
}
