using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("set_group_access")]
    public class SetGroupAccess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int grp_mod_id { get; set; }

        [MaxLength(25)]
        public string grp_id { get; set; }

        [MaxLength(25)]
        public string mod_id { get; set; }

        public bool can_view { get; set; }

        public bool can_add { get; set; }

        public bool can_edit { get; set; }

        public bool can_delete { get; set; }
    }
}
