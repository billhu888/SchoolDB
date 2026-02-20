using Microsoft.EntityFrameworkCore;
using SchoolDB.Models;

namespace SchoolDB.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<StudentSport> StudentSports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete loops
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Classes)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSport>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSports)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentSport>()
                .HasOne(ss => ss.Sport)
                .WithMany(sp => sp.StudentSports)
                .HasForeignKey(ss => ss.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Table naming
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Class>().ToTable("Classes");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<Sport>().ToTable("Sports");
            modelBuilder.Entity<StudentSport>().ToTable("StudentSports");
        }
    }
}
