using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Class Name")]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(20)]
        [Display(Name = "Class Code")]
        public string ClassCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Instructor { get; set; }

        [Range(1, 6)]
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; }

        [Display(Name = "Max Enrollment")]
        public int MaxEnrollment { get; set; } = 30;

        [StringLength(50)]
        public string? Schedule { get; set; }

        [StringLength(50)]
        public string? Room { get; set; }

        [StringLength(20)]
        public string? Semester { get; set; }

        // FK
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
