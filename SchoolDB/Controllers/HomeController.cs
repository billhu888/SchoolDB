using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;

namespace SchoolDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.StudentCount = await _context.Students.CountAsync();
            ViewBag.DepartmentCount = await _context.Departments.CountAsync();
            ViewBag.ClassCount = await _context.Classes.CountAsync();
            ViewBag.SportCount = await _context.Sports.CountAsync();
            ViewBag.EnrollmentCount = await _context.Enrollments.CountAsync();
            ViewBag.StudentSportCount = await _context.StudentSports.CountAsync();
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}
