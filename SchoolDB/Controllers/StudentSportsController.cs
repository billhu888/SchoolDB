using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class StudentSportsController : Controller
    {
        private readonly SchoolContext _context;
        public StudentSportsController(SchoolContext context) { _context = context; }

        public async Task<IActionResult> Index(int? studentId, int? sportId)
        {
            var studentSports = _context.StudentSports
                .Include(ss => ss.Student)
                .Include(ss => ss.Sport)
                .AsQueryable();

            if (studentId.HasValue) studentSports = studentSports.Where(ss => ss.StudentId == studentId);
            if (sportId.HasValue) studentSports = studentSports.Where(ss => ss.SportId == sportId);

            ViewBag.Students = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName");
            ViewBag.Sports = new SelectList(_context.Sports.OrderBy(s => s.Name), "SportId", "Name");

            return View(await studentSports.OrderBy(ss => ss.Student!.LastName).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ss = await _context.StudentSports.Include(x => x.Student).Include(x => x.Sport).FirstOrDefaultAsync(m => m.StudentSportId == id);
            if (ss == null) return NotFound();
            return View(ss);
        }

        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName");
            ViewData["SportId"] = new SelectList(_context.Sports.OrderBy(s => s.Name), "SportId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SportId,JoinDate,Position,JerseyNumber,Status,Notes")] StudentSport studentSport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentSport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", studentSport.StudentId);
            ViewData["SportId"] = new SelectList(_context.Sports.OrderBy(s => s.Name), "SportId", "Name", studentSport.SportId);
            return View(studentSport);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ss = await _context.StudentSports.FindAsync(id);
            if (ss == null) return NotFound();
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", ss.StudentId);
            ViewData["SportId"] = new SelectList(_context.Sports.OrderBy(s => s.Name), "SportId", "Name", ss.SportId);
            return View(ss);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentSportId,StudentId,SportId,JoinDate,Position,JerseyNumber,Status,Notes")] StudentSport studentSport)
        {
            if (id != studentSport.StudentSportId) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(studentSport); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.StudentSports.Any(e => e.StudentSportId == studentSport.StudentSportId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students.OrderBy(s => s.LastName), "StudentId", "FullName", studentSport.StudentId);
            ViewData["SportId"] = new SelectList(_context.Sports.OrderBy(s => s.Name), "SportId", "Name", studentSport.SportId);
            return View(studentSport);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ss = await _context.StudentSports.Include(x => x.Student).Include(x => x.Sport).FirstOrDefaultAsync(m => m.StudentSportId == id);
            if (ss == null) return NotFound();
            return View(ss);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ss = await _context.StudentSports.FindAsync(id);
            if (ss != null) _context.StudentSports.Remove(ss);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
