using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int ClassId { get; set; }
        public Class? Class { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;

        [StringLength(2)]
        public string? Grade { get; set; }

        [Display(Name = "Status")]
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Dropped, Completed

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
