using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Entities.POCOs
{
    public class  Cw_Prop_Type
    {
        [Key]
        public int PPT_UUID;

        [Required]
        [StringLength(maximumLength: 254)]
        public string PPT_NAME;

        public int PPT_DATA_TYPE;
    }
}
