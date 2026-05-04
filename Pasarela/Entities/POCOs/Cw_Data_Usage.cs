using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class  Cw_Data_Usage
    {
        [Required]
        public int DM_DELETES;

        [Required]
        public int DM_INSERTS;
    }
}
