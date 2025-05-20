using KutipWeb.Data;
using KutipWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutipWeb.Controllers
{
    public class BinController : Controller
    {
        private readonly KutipDbContext _context;

        public BinController(KutipDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bins = await _context.Bins.Include(b => b.Collectors).ToListAsync();
            return View(bins);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var bin = await _context.Bins
                .Include(b => b.Collectors)
                .FirstOrDefaultAsync(m => m.BinId == id);

            if (bin == null) return NotFound();

            return View(bin);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bin bin)
        {
            if (ModelState.IsValid)
            {
                bin.CreatedAt = DateTime.Now;
                bin.UpdatedAt = DateTime.Now;
                _context.Add(bin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bin);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bin = await _context.Bins.FindAsync(id);
            if (bin == null) return NotFound();

            return View(bin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bin bin)
        {
            if (id != bin.BinId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    bin.UpdatedAt = DateTime.Now;
                    _context.Update(bin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinExists(bin.BinId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bin);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bin = await _context.Bins
                .FirstOrDefaultAsync(m => m.BinId == id);

            if (bin == null) return NotFound();

            return View(bin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bin = await _context.Bins.FindAsync(id);
            if (bin != null)
            {
                _context.Bins.Remove(bin);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BinExists(int id)
        {
            return _context.Bins.Any(e => e.BinId == id);
        }
    }
}
