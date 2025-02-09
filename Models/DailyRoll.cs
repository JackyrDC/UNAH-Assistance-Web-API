using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("DailyRolls")]
    public class DailyRoll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDailyRoll { get; set; }

        [Required]
        public int IdRoll { get; set; }

        [ForeignKey("IdRoll")]
        public Rolls Roll { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public virtual ICollection<PermanentRolls> StudentsList { get; set; }
    }
}
