using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Event
    {
        [Key]
        public int EV_ID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string EV_NAME;
    }
}
