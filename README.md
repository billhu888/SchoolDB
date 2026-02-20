# SchoolDB - ASP.NET MVC School Database

A full-featured school management system built with ASP.NET Core MVC 8, Entity Framework Core, and SQL Server.

## Features

- **Students** – Full CRUD with search/filter by name, grade level, and department
- **Departments** – Manage academic departments with head, office, description
- **Classes** – Manage courses with instructor, schedule, room, enrollment capacity
- **Enrollments** – Track which students are enrolled in which classes, with grade and status
- **Sports** – Manage sports teams with coach, season, gender, roster size
- **Student Athletes** – Track which students play which sports, with position and jersey number

## Getting Started

### Prerequisites
- Visual Studio 2022 (17.x or later)
- .NET 8 SDK
- SQL Server LocalDB (included with Visual Studio)

### Setup

1. **Open Solution**
   - Open `SchoolDB.sln` in Visual Studio

2. **Restore NuGet Packages**
   - Visual Studio does this automatically, or: `Tools → NuGet Package Manager → Restore`

3. **Run the Application**
   - Press **F5** or click the green Run button
   - The database is automatically created and seeded with sample data on first run

### Database

The app uses SQL Server LocalDB with EF Core Code-First. The connection string in `appsettings.json` points to:
```
Server=(localdb)\mssqllocaldb;Database=SchoolDB
```

The `DbInitializer` seeds the database with:
- 5 Departments (CS, Math, English, Biology, History)
- 10 Classes across departments
- 10 Students
- 17 Enrollments
- 8 Sports
- 11 Athlete records

### Project Structure

```
SchoolDB/
├── Controllers/
│   ├── HomeController.cs
│   ├── StudentsController.cs
│   ├── DepartmentsController.cs
│   ├── ClassesController.cs
│   ├── EnrollmentsController.cs
│   ├── SportsController.cs
│   └── StudentSportsController.cs
├── Models/
│   ├── Student.cs
│   ├── Department.cs
│   ├── Class.cs
│   ├── Enrollment.cs
│   ├── Sport.cs
│   └── StudentSport.cs
├── Data/
│   ├── SchoolContext.cs       ← EF DbContext
│   └── DbInitializer.cs      ← Seed data
├── Views/
│   ├── Home/
│   ├── Students/
│   ├── Departments/
│   ├── Classes/
│   ├── Enrollments/
│   ├── Sports/
│   └── StudentSports/
└── wwwroot/
    └── css/site.css
```

### Technology Stack
- ASP.NET Core MVC 8
- Entity Framework Core 8 (Code First)
- SQL Server LocalDB
- Bootstrap 5.3
- Font Awesome 6
