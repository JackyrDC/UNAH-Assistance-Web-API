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
        public int idDailyRoll { get; set; }  // Cambiado a camelCase

        [ForeignKey("Roll")]
        public int idRoll { get; set; }  // Cambiado a camelCase

        public DateTime creationDate { get; set; }  // Cambiado a camelCase

        public virtual Roll roll { get; set; }  // Cambiado a camelCase

        public virtual ICollection<PermanentRoll> studentsList { get; set; }  // Cambiado a camelCase
    }
}
