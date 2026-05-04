using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class ErwinDiagram
    {
        [Key]
        public int DI_ID;

        [Required]
        [StringLength(maximumLength: 255)]
        public string DI_NAME;

        public int DI_TYPE;

        public int ANO_ID;

        public int ANO_TABNR;
    }
}
