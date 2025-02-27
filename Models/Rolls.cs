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
        public int idRoll { get; set; }

        [Required]
        [ForeignKey(nameof(teacher))]
        public int idTeacher { get; set; }

        public virtual Teachers teacher { get; set; }

        [Required]
        [ForeignKey(nameof(classEntity))]
        public int idClass { get; set; }

        public virtual Classes classEntity { get; set; } 

        [Required]
        public DateTime rollDate { get; set; }

        public virtual ICollection<DailyRoll> dailyRolls { get; set; } = new List<DailyRoll>();
    }
}
