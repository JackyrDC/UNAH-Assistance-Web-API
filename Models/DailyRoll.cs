using UNAH_Assistance_Web_API.Models;

public class DailyRoll
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idDailyRoll { get; set; }

    [ForeignKey("Roll")]
    public int idRoll { get; set; }

    public DateTime creationDate { get; set; }

    public virtual Roll roll { get; set; }

    public virtual ICollection<PermanentRoll> studentsList { get; set; }

    public bool IsDeleted { get; set; } = false;
}
