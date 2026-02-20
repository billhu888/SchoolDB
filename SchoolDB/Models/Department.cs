using System.ComponentModel.DataAnnotations;

namespace SchoolDB.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string? Code { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Department Head")]
        [StringLength(100)]
        public string? DepartmentHead { get; set; }

        [Display(Name = "Office Location")]
        [StringLength(100)]
        public string? OfficeLocation { get; set; }

        // Navigation
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
