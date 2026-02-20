using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchString, string gradeLevel, int? departmentId)
        {
            var students = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Enrollments)
                .Include(s => s.StudentSports)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                students = students.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString) || s.StudentNumber.Contains(searchString));

            if (!string.IsNullOrEmpty(gradeLevel))
                students = students.Where(s => s.GradeLevel == gradeLevel);

            if (departmentId.HasValue)
                students = students.Where(s => s.DepartmentId == departmentId);

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.SearchString = searchString;
            ViewBag.GradeLevel = gradeLevel;
            ViewBag.SelectedDepartment = departmentId;

            return View(await students.OrderBy(s => s.LastName).ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Enrollments).ThenInclude(e => e.Class).ThenInclude(c => c!.Department)
                .Include(s => s.StudentSports).ThenInclude(ss => ss.Sport)
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null) return NotFound();
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,DateOfBirth,EnrollmentDate,StudentNumber,GradeLevel,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Email,DateOfBirth,EnrollmentDate,StudentNumber,GradeLevel,DepartmentId")] Student student)
        {
            if (id != student.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.StudentId == student.StudentId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = await _context.Students.Include(s => s.Department).FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null) _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
