using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class Joiner
    {
        [Key]
        public int ID_CONECTOR;

        [Required]
        public int NUM_SEQ_DESDE;

        [Required]
        public int NUM_SEQ_HASTA;

        [Required]
        public int DI_ID;
    }
}
