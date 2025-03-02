using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("PermanentRolls")]
    public class PermanentRoll
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(dailyRoll))]
        public int idDailyRoll { get; set; }

        public virtual DailyRoll dailyRoll { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(student))]
        public int idStudent { get; set; }

        public virtual Students student { get; set; }

        [Required]
        public string rollState { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? modificationDate { get; set; }
    }
}