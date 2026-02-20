using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class StudentSport
    {
        public int StudentSportId { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int SportId { get; set; }
        public Sport? Sport { get; set; }

        [Display(Name = "Join Date")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Today;

        [StringLength(50)]
        public string? Position { get; set; }

        [StringLength(10)]
        [Display(Name = "Jersey #")]
        public string? JerseyNumber { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Injured

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
