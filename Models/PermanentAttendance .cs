using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("PermanentAttendance")]
    public class PermanentAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPermanentAttendance { get; set; }
        public int IdStudent { get; set; }
        [ForeignKey("IdStudent")]
        public Students Student { get; set; }

        public int IdClass { get; set; }
        [ForeignKey("IdClass")]
        public Classes Class { get; set; }

        public int TotalPresent { get; set; } // Total de asistencias
        public int TotalAbsent { get; set; } // Total de ausencias
        public int TotalLate { get; set; }   // Total de llegadas tarde
    }
}
