using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SchoolContext _context;
        public ClassesController(SchoolContext context) { _context = context; }

        public async Task<IActionResult> Index(string searchString, int? departmentId)
        {
            var classes = _context.Classes.Include(c => c.Department).Include(c => c.Enrollments).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                classes = classes.Where(c => c.Name.Contains(searchString) || c.ClassCode.Contains(searchString));

            if (departmentId.HasValue)
                classes = classes.Where(c => c.DepartmentId == departmentId);

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.SearchString = searchString;
            ViewBag.SelectedDepartment = departmentId;

            return View(await classes.OrderBy(c => c.ClassCode).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var @class = await _context.Classes
                .Include(c => c.Department)
                .Include(c => c.Enrollments).ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null) return NotFound();
            return View(@class);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ClassCode,Description,Instructor,CreditHours,MaxEnrollment,Schedule,Room,Semester,DepartmentId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", @class.DepartmentId);
            return View(@class);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null) return NotFound();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", @class.DepartmentId);
            return View(@class);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Name,ClassCode,Description,Instructor,CreditHours,MaxEnrollment,Schedule,Room,Semester,DepartmentId")] Class @class)
        {
            if (id != @class.ClassId) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(@class); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Classes.Any(e => e.ClassId == @class.ClassId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", @class.DepartmentId);
            return View(@class);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var @class = await _context.Classes.Include(c => c.Department).FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null) return NotFound();
            return View(@class);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class != null) _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
