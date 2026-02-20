using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class Sport
    {
        public int SportId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Sport Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(20)]
        public string? Season { get; set; } // Fall, Winter, Spring, Year-Round

        [StringLength(20)]
        public string? Gender { get; set; } // Men's, Women's, Co-Ed

        [StringLength(100)]
        public string? Coach { get; set; }

        [Display(Name = "Max Roster Size")]
        public int? MaxRosterSize { get; set; }

        // Navigation
        public ICollection<StudentSport> StudentSports { get; set; } = new List<StudentSport>();
    }
}
