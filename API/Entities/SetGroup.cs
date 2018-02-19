using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("set_group")]
    public class SetGroup
    {
        [Key]
        [MaxLength(25)]
        public string grp_id { get; set; }

        [MaxLength(50)]
        public string grp_name { get; set; }

        [MaxLength(255)]
        public string grp_desc { get; set; }

        public DateTime created_date { get; set; }
    }
}
