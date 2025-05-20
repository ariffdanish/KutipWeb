using KutipWeb.Data;
using KutipWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutipWeb.Controllers
{
    public class CollectorController : Controller
    {
        private readonly KutipDbContext _context;

        public CollectorController(KutipDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var collectors = await _context.Collectors.ToListAsync();
            return View(collectors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Collector collector)
        {
            if (ModelState.IsValid)
            {
                _context.Collectors.Add(collector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collector);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var collector = await _context.Collectors.FindAsync(id);
            if (collector == null)
                return NotFound();

            return View(collector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Collector collector)
        {
            if (id != collector.CollectorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Collectors.Any(e => e.CollectorId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collector);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var collector = await _context.Collectors.FirstOrDefaultAsync(m => m.CollectorId == id);
            if (collector == null)
                return NotFound();

            return View(collector);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var collector = await _context.Collectors.FirstOrDefaultAsync(m => m.CollectorId == id);
            if (collector == null)
                return NotFound();

            return View(collector);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collector = await _context.Collectors.FindAsync(id);
            if (collector != null)
            {
                _context.Collectors.Remove(collector);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
