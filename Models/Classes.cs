using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("Classes")]
    public class Classes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClass { get; set; }
         
        [Required]
        public string ClassName { get; set; }

        public int? IdTeacher { get; set; }
        [ForeignKey("IdTeacher")]
        public Teachers Teacher { get; set; }

        public int IdCampus { get; set; }
        [ForeignKey("IdCampus")]
        public Campus Campus { get; set; }


        public string Period { get; set; }
        public int Year { get; set; }
        public int Credits { get; set; }

        public ICollection<Students> StudentsList { get; set; }

    }
}
