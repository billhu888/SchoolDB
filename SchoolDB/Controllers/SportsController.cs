using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class SportsController : Controller
    {
        private readonly SchoolContext _context;
        public SportsController(SchoolContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sports.Include(s => s.StudentSports).OrderBy(s => s.Name).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var sport = await _context.Sports
                .Include(s => s.StudentSports).ThenInclude(ss => ss.Student)
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null) return NotFound();
            return View(sport);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Season,Gender,Coach,MaxRosterSize")] Sport sport)
        {
            if (ModelState.IsValid) { _context.Add(sport); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
            return View(sport);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null) return NotFound();
            return View(sport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportId,Name,Description,Season,Gender,Coach,MaxRosterSize")] Sport sport)
        {
            if (id != sport.SportId) return NotFound();
            if (ModelState.IsValid)
            {
                try { _context.Update(sport); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sports.Any(e => e.SportId == sport.SportId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sport = await _context.Sports.FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null) return NotFound();
            return View(sport);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport != null) _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
