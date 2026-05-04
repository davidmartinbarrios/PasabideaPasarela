using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Cw_Object
    {
        [Key]
        public int GO_ID;

        [Required]
        public int OT_ID;

        [Required]
        public string GO_NAME;

        public string USERDEFINED;
    }
}
