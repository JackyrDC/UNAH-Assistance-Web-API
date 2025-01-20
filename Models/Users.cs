using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAH_Assistance_Web_API.Models
{
    public abstract class Users
    {

        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual int IdUserState { get; set; }
        [ForeignKey("IdUserState")]
        public virtual UserState userState { get; set; }
    }
}
