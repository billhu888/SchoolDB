using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Student ID #")]
        public string StudentNumber { get; set; } = string.Empty;

        [StringLength(10)]
        public string? GradeLevel { get; set; }

        // FK to Department (major)
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<StudentSport> StudentSports { get; set; } = new List<StudentSport>();
    }
}
