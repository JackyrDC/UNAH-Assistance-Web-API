using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("Teachers")]
    public class Teachers : Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsUnique = true)]
        public int IdTeacher { get; set; } 
        public string employeeNumber { get; set; }
        public int IdCampus { get; set; }
        [ForeignKey("IdCampus")]
        public Campus Campus { get; set; }

        public int IdUserState { get; set; }
        [ForeignKey("IdUserState")]
        public  UserState userState { get; set; }

        public int IdUserType { get; set; }
        [ForeignKey("IdUserType")]
        public UserTypes userType { get; set; }

        public ICollection<Classes> Classes { get; set; }
    }
}
 