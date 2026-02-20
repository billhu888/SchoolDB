using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;
        public EnrollmentsController(SchoolContext context) { _context = context; }

        public async Task<IActionResult> Index(int? studentId, int? classId)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Class).ThenInclude(c => c!.Department)
                .AsQueryable();

            if (studentId.HasValue) enrollments = enrollments.Where(e => e.StudentId == studentId);
            if (classId.HasValue) enrollments = enrollments.Where(e => e.ClassId == classId);

            ViewBag.Students = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName");
            ViewBag.Classes = new SelectList(_context.Classes.OrderBy(c => c.ClassCode), "ClassId", "Name");
            ViewBag.SelectedStudent = studentId;
            ViewBag.SelectedClass = classId;

            return View(await enrollments.OrderBy(e => e.Student!.LastName).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Class).ThenInclude(c => c!.Department)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName");
            ViewData["ClassId"] = new SelectList(_context.Classes.OrderBy(c => c.ClassCode), "ClassId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,ClassId,EnrollmentDate,Grade,Status,Notes")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", enrollment.StudentId);
            ViewData["ClassId"] = new SelectList(_context.Classes.OrderBy(c => c.ClassCode), "ClassId", "Name", enrollment.ClassId);
            return View(enrollment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", enrollment.StudentId);
            ViewData["ClassId"] = new SelectList(_context.Classes.OrderBy(c => c.ClassCode), "ClassId", "Name", enrollment.ClassId);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,StudentId,ClassId,EnrollmentDate,Grade,Status,Notes")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(enrollment); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Enrollments.Any(e => e.EnrollmentId == enrollment.EnrollmentId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", enrollment.StudentId);
            ViewData["ClassId"] = new SelectList(_context.Classes.OrderBy(c => c.ClassCode), "ClassId", "Name", enrollment.ClassId);
            return View(enrollment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments
                .Include(e => e.Student).Include(e => e.Class)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null) _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
