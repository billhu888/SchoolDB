using SchoolDB.Models;

namespace SchoolDB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            if (context.Students.Any()) return; // DB already seeded

            // Departments
            var departments = new Department[]
            {
                new Department { Name = "Computer Science", Code = "CS", DepartmentHead = "Dr. Alan Turing", OfficeLocation = "Tech Hall 101", Description = "Covers programming, algorithms, and software engineering." },
                new Department { Name = "Mathematics", Code = "MATH", DepartmentHead = "Dr. Emmy Noether", OfficeLocation = "Science Hall 202", Description = "Pure and applied mathematics." },
                new Department { Name = "English", Code = "ENG", DepartmentHead = "Dr. Jane Austen", OfficeLocation = "Humanities 305", Description = "Literature, writing, and communication." },
                new Department { Name = "Biology", Code = "BIO", DepartmentHead = "Dr. Charles Darwin", OfficeLocation = "Life Science 110", Description = "Study of living organisms." },
                new Department { Name = "History", Code = "HIST", DepartmentHead = "Dr. Howard Zinn", OfficeLocation = "Humanities 210", Description = "World and American history." },
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

            // Sports
            var sports = new Sport[]
            {
                new Sport { Name = "Basketball", Season = "Winter", Gender = "Men's", Coach = "Coach Mike", MaxRosterSize = 15 },
                new Sport { Name = "Basketball", Season = "Winter", Gender = "Women's", Coach = "Coach Lisa", MaxRosterSize = 15 },
                new Sport { Name = "Soccer", Season = "Fall", Gender = "Co-Ed", Coach = "Coach Rivera", MaxRosterSize = 22 },
                new Sport { Name = "Baseball", Season = "Spring", Gender = "Men's", Coach = "Coach Johnson", MaxRosterSize = 25 },
                new Sport { Name = "Track & Field", Season = "Spring", Gender = "Co-Ed", Coach = "Coach Williams", MaxRosterSize = 40 },
                new Sport { Name = "Swimming", Season = "Winter", Gender = "Co-Ed", Coach = "Coach Phelps", MaxRosterSize = 20 },
                new Sport { Name = "Volleyball", Season = "Fall", Gender = "Women's", Coach = "Coach Morgan", MaxRosterSize = 12 },
                new Sport { Name = "Tennis", Season = "Spring", Gender = "Co-Ed", Coach = "Coach Federer", MaxRosterSize = 10 },
            };
            context.Sports.AddRange(sports);
            context.SaveChanges();

            // Classes
            var classes = new Class[]
            {
                new Class { Name = "Intro to Programming", ClassCode = "CS101", CreditHours = 3, DepartmentId = departments[0].DepartmentId, Instructor = "Prof. Smith", Room = "TH-201", Schedule = "MWF 9:00 AM", Semester = "Fall 2024", MaxEnrollment = 30 },
                new Class { Name = "Data Structures", ClassCode = "CS201", CreditHours = 3, DepartmentId = departments[0].DepartmentId, Instructor = "Prof. Jones", Room = "TH-202", Schedule = "TTh 10:00 AM", Semester = "Fall 2024", MaxEnrollment = 25 },
                new Class { Name = "Algorithms", ClassCode = "CS301", CreditHours = 3, DepartmentId = departments[0].DepartmentId, Instructor = "Prof. Lee", Room = "TH-303", Schedule = "MWF 11:00 AM", Semester = "Fall 2024", MaxEnrollment = 20 },
                new Class { Name = "Calculus I", ClassCode = "MATH101", CreditHours = 4, DepartmentId = departments[1].DepartmentId, Instructor = "Prof. Newton", Room = "SH-101", Schedule = "MWF 8:00 AM", Semester = "Fall 2024", MaxEnrollment = 35 },
                new Class { Name = "Linear Algebra", ClassCode = "MATH201", CreditHours = 3, DepartmentId = departments[1].DepartmentId, Instructor = "Prof. Gauss", Room = "SH-102", Schedule = "TTh 1:00 PM", Semester = "Fall 2024", MaxEnrollment = 25 },
                new Class { Name = "English Composition", ClassCode = "ENG101", CreditHours = 3, DepartmentId = departments[2].DepartmentId, Instructor = "Prof. Hemingway", Room = "HU-201", Schedule = "MWF 10:00 AM", Semester = "Fall 2024", MaxEnrollment = 25 },
                new Class { Name = "American Literature", ClassCode = "ENG201", CreditHours = 3, DepartmentId = departments[2].DepartmentId, Instructor = "Prof. Fitzgerald", Room = "HU-202", Schedule = "TTh 2:00 PM", Semester = "Fall 2024", MaxEnrollment = 20 },
                new Class { Name = "General Biology", ClassCode = "BIO101", CreditHours = 4, DepartmentId = departments[3].DepartmentId, Instructor = "Prof. Watson", Room = "LS-101", Schedule = "MWF 9:00 AM", Semester = "Fall 2024", MaxEnrollment = 30 },
                new Class { Name = "World History", ClassCode = "HIST101", CreditHours = 3, DepartmentId = departments[4].DepartmentId, Instructor = "Prof. Toynbee", Room = "HU-301", Schedule = "TTh 11:00 AM", Semester = "Fall 2024", MaxEnrollment = 40 },
                new Class { Name = "US History", ClassCode = "HIST201", CreditHours = 3, DepartmentId = departments[4].DepartmentId, Instructor = "Prof. McCullough", Room = "HU-302", Schedule = "MWF 1:00 PM", Semester = "Fall 2024", MaxEnrollment = 35 },
            };
            context.Classes.AddRange(classes);
            context.SaveChanges();

            // Students
            var students = new Student[]
            {
                new Student { FirstName = "Emma", LastName = "Johnson", Email = "emma.johnson@school.edu", DateOfBirth = new DateTime(2003, 3, 15), EnrollmentDate = new DateTime(2022, 9, 1), StudentNumber = "STU001", GradeLevel = "Junior", DepartmentId = departments[0].DepartmentId },
                new Student { FirstName = "Liam", LastName = "Smith", Email = "liam.smith@school.edu", DateOfBirth = new DateTime(2002, 7, 22), EnrollmentDate = new DateTime(2021, 9, 1), StudentNumber = "STU002", GradeLevel = "Senior", DepartmentId = departments[0].DepartmentId },
                new Student { FirstName = "Olivia", LastName = "Williams", Email = "olivia.williams@school.edu", DateOfBirth = new DateTime(2003, 11, 5), EnrollmentDate = new DateTime(2022, 9, 1), StudentNumber = "STU003", GradeLevel = "Junior", DepartmentId = departments[1].DepartmentId },
                new Student { FirstName = "Noah", LastName = "Brown", Email = "noah.brown@school.edu", DateOfBirth = new DateTime(2004, 1, 30), EnrollmentDate = new DateTime(2023, 9, 1), StudentNumber = "STU004", GradeLevel = "Sophomore", DepartmentId = departments[2].DepartmentId },
                new Student { FirstName = "Ava", LastName = "Davis", Email = "ava.davis@school.edu", DateOfBirth = new DateTime(2003, 5, 12), EnrollmentDate = new DateTime(2022, 9, 1), StudentNumber = "STU005", GradeLevel = "Junior", DepartmentId = departments[3].DepartmentId },
                new Student { FirstName = "William", LastName = "Miller", Email = "william.miller@school.edu", DateOfBirth = new DateTime(2005, 8, 19), EnrollmentDate = new DateTime(2024, 9, 1), StudentNumber = "STU006", GradeLevel = "Freshman", DepartmentId = departments[4].DepartmentId },
                new Student { FirstName = "Sophia", LastName = "Wilson", Email = "sophia.wilson@school.edu", DateOfBirth = new DateTime(2002, 4, 3), EnrollmentDate = new DateTime(2021, 9, 1), StudentNumber = "STU007", GradeLevel = "Senior", DepartmentId = departments[0].DepartmentId },
                new Student { FirstName = "James", LastName = "Moore", Email = "james.moore@school.edu", DateOfBirth = new DateTime(2004, 9, 27), EnrollmentDate = new DateTime(2023, 9, 1), StudentNumber = "STU008", GradeLevel = "Sophomore", DepartmentId = departments[1].DepartmentId },
                new Student { FirstName = "Isabella", LastName = "Taylor", Email = "isabella.taylor@school.edu", DateOfBirth = new DateTime(2003, 6, 14), EnrollmentDate = new DateTime(2022, 9, 1), StudentNumber = "STU009", GradeLevel = "Junior", DepartmentId = departments[2].DepartmentId },
                new Student { FirstName = "Oliver", LastName = "Anderson", Email = "oliver.anderson@school.edu", DateOfBirth = new DateTime(2004, 12, 8), EnrollmentDate = new DateTime(2023, 9, 1), StudentNumber = "STU010", GradeLevel = "Sophomore", DepartmentId = departments[3].DepartmentId },
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // Enrollments
            var enrollments = new Enrollment[]
            {
                new Enrollment { StudentId = students[0].StudentId, ClassId = classes[0].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", Status = "Completed" },
                new Enrollment { StudentId = students[0].StudentId, ClassId = classes[1].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B+", Status = "Completed" },
                new Enrollment { StudentId = students[0].StudentId, ClassId = classes[3].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[1].StudentId, ClassId = classes[2].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A-", Status = "Completed" },
                new Enrollment { StudentId = students[1].StudentId, ClassId = classes[0].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[2].StudentId, ClassId = classes[3].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", Status = "Completed" },
                new Enrollment { StudentId = students[2].StudentId, ClassId = classes[4].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[3].StudentId, ClassId = classes[5].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[3].StudentId, ClassId = classes[8].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[4].StudentId, ClassId = classes[7].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B", Status = "Completed" },
                new Enrollment { StudentId = students[5].StudentId, ClassId = classes[8].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[5].StudentId, ClassId = classes[9].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[6].StudentId, ClassId = classes[0].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A+", Status = "Completed" },
                new Enrollment { StudentId = students[6].StudentId, ClassId = classes[2].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[7].StudentId, ClassId = classes[3].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[8].StudentId, ClassId = classes[6].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
                new Enrollment { StudentId = students[9].StudentId, ClassId = classes[7].ClassId, EnrollmentDate = new DateTime(2024, 9, 1), Status = "Active" },
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            // StudentSports
            var studentSports = new StudentSport[]
            {
                new StudentSport { StudentId = students[0].StudentId, SportId = sports[1].SportId, JoinDate = new DateTime(2022, 9, 1), Position = "Guard", JerseyNumber = "12", Status = "Active" },
                new StudentSport { StudentId = students[1].StudentId, SportId = sports[0].SportId, JoinDate = new DateTime(2021, 9, 1), Position = "Forward", JerseyNumber = "23", Status = "Active" },
                new StudentSport { StudentId = students[2].StudentId, SportId = sports[2].SportId, JoinDate = new DateTime(2022, 9, 1), Position = "Midfielder", JerseyNumber = "8", Status = "Active" },
                new StudentSport { StudentId = students[3].StudentId, SportId = sports[2].SportId, JoinDate = new DateTime(2023, 9, 1), Position = "Forward", JerseyNumber = "10", Status = "Active" },
                new StudentSport { StudentId = students[4].StudentId, SportId = sports[4].SportId, JoinDate = new DateTime(2022, 9, 1), Position = "Sprinter", Status = "Active" },
                new StudentSport { StudentId = students[5].StudentId, SportId = sports[5].SportId, JoinDate = new DateTime(2024, 9, 1), JerseyNumber = "5", Status = "Active" },
                new StudentSport { StudentId = students[6].StudentId, SportId = sports[1].SportId, JoinDate = new DateTime(2021, 9, 1), Position = "Center", JerseyNumber = "33", Status = "Active" },
                new StudentSport { StudentId = students[7].StudentId, SportId = sports[4].SportId, JoinDate = new DateTime(2023, 9, 1), Position = "Distance Runner", Status = "Active" },
                new StudentSport { StudentId = students[8].StudentId, SportId = sports[6].SportId, JoinDate = new DateTime(2022, 9, 1), Position = "Setter", JerseyNumber = "2", Status = "Active" },
                new StudentSport { StudentId = students[9].StudentId, SportId = sports[3].SportId, JoinDate = new DateTime(2023, 9, 1), Position = "Pitcher", JerseyNumber = "18", Status = "Active" },
                new StudentSport { StudentId = students[1].StudentId, SportId = sports[3].SportId, JoinDate = new DateTime(2022, 9, 1), Position = "Outfield", JerseyNumber = "7", Status = "Active" },
            };
            context.StudentSports.AddRange(studentSports);
            context.SaveChanges();
        }
    }
}
