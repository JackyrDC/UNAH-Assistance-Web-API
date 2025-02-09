using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("Rolls")]
    public class Rolls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRoll { get; set; }

        [Required]
        public int IdTeacher { get; set; }

        [ForeignKey("IdTeacher")]
        public Teachers Teacher { get; set; }

        [Required]
        public int IdClass { get; set; }

        [ForeignKey("IdClass")]
        public Classes Class { get; set; }

        [Required]
        public DateTime RollDate { get; set; }

        public virtual ICollection<DailyRoll> DailyRolls { get; set; }
    }
}
