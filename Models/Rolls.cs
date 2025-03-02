using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("Rolls")]
    public class Roll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRoll { get; set; }

        [Required]
        [ForeignKey(nameof(Teacher))]
        public int IdTeacher { get; set; }
        public virtual Teachers Teacher { get; set; }

        [Required]
        [ForeignKey(nameof(ClassEntity))]
        public int IdClass { get; set; }
        public virtual Classes ClassEntity { get; set; }

        [Required]
        public DateTime RollDate { get; set; }

        public virtual ICollection<DailyRoll> DailyRolls { get; set; } = new HashSet<DailyRoll>();

        public bool IsDeleted { get; set; } = false;
        public DateTime? modificationDate { get; set; }
    }
}
