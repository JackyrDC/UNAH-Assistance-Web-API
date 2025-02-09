using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("PermanentRolls")]
    public class PermanentRolls
    {
        [Key, Column(Order = 0)]
        public int IdDailyRoll { get; set; }

        [ForeignKey("IdDailyRoll")]
        public DailyRoll DailyRoll { get; set; }

        [Key, Column(Order = 1)]
        public int IdStudent { get; set; }

        [ForeignKey("IdStudent")]
        public Students Student { get; set; }

        [Required]
        public string RollState { get; set; }
    }
}
