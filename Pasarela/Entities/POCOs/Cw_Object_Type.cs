using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Cw_Object_Type
    {
        [Key]
        public int OT_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string OT_NAME;
    }
}
