using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("DailyAttendance")]
    public class DailyAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAttendance { get; set; }

        public int IdStudent { get; set; }
        [ForeignKey("IdStudent")]
        public Students Student { get; set; }

        public int IdClass { get; set; }
        [ForeignKey("IdClass")]
        public Classes Class { get; set; }

        public int IdCampus { get; set; }
        [ForeignKey("IdCampus")]
        public Campus Campus { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; } // Ejemplo: Presente, Ausente, Tarde
    }
}
