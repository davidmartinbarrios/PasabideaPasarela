using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Organization
    {
        [Key]
        public int OU_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string OU_NAME;

        public int OU_TYPE;
    }
}
