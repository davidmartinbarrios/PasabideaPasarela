using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class ErwinDiagramDTO
    {
        public override string ToString()
        {
            return DI_NAME;
        }

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
