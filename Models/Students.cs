using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    [Table("Students")]
    public class Students : Users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdStudent { get; set; }
        public string StudentName { get; set; }
        override public string Name { get => StudentName; set => StudentName = value; }
        public string StudentLastName { get; set; }
        override public string LastName { get => StudentLastName; set => StudentLastName = value; }
        public string StudentEmail { get; set; }
        override public string Email { get => StudentEmail; set => StudentEmail = value; }
        public string StudentPhone { get; set; }
        public string StudentAddress { get; set; }
        public string StudentGender { get; set; }
        public string StudentBirthDate { get; set; }
        public string StudentPhoto { get; set; }
        public bool StudentActive { get; set; }
        public int IdCampus { get; set; }
        [ForeignKey("IdCampus")]
        public Campus Campus { get; set; }

        [ForeignKey("IdUserState")]
        public virtual UserState userState { get; set; }
        public virtual int IdUserType { get; set; }
        [ForeignKey("IdUserType")]
        public virtual UserTypes userType { get; set; }

        public virtual ICollection<Classes> Classes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? modificationDate { get; set; }
    }
}