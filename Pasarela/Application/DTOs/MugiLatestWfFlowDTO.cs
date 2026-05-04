using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public class MugiLatestWfFlowDTO
    {
        [Required]
        [StringLength(maximumLength: 255)]
        public string BaseDatos;

        [Required]
        [StringLength(maximumLength: 255)]
        public string Flow;

        [StringLength(maximumLength: 50)]
        public string Version;

        public string Comments;
    }
}
