using KutipWeb.Data;
using KutipWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KutipWeb.Controllers
{
    public class PickupController : Controller
    {
        private readonly KutipDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public PickupController(KutipDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var pickups = await _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)
                .ToListAsync();

            return View(pickups);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateCollectorsAndBins();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                pickup.PickupTime = DateTimeOffset.Now;

                string uniqueFileName = GetUploadedFileName(pickup);
                pickup.PhotoUrl = uniqueFileName;

                _context.Pickups.Add(pickup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pickup = await _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)
                .FirstOrDefaultAsync(p => p.PickupId == id);

            if (pickup == null)
                return NotFound();

            return View(pickup);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pickup = await _context.Pickups.FindAsync(id);
            if (pickup == null)
                return NotFound();

            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                var existingPickup = await _context.Pickups.AsNoTracking().FirstOrDefaultAsync(p => p.PickupId == pickup.PickupId);
                if (existingPickup == null)
                    return NotFound();

                // Preserve original PickupTime
                pickup.PickupTime = existingPickup.PickupTime;

                // Handle photo upload
                if (pickup.Photo != null)
                {
                    // Delete old file
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                    string oldFilePath = Path.Combine(uploadsFolder, existingPickup.PhotoUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    string uniqueFileName = GetUploadedFileName(pickup);
                    pickup.PhotoUrl = uniqueFileName;
                }
                else
                {
                    pickup.PhotoUrl = existingPickup.PhotoUrl; // Keep old photo if new not uploaded
                }

                _context.Update(pickup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var pickup = await _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)
                .FirstOrDefaultAsync(p => p.PickupId == id);

            if (pickup == null)
                return NotFound();

            return View(pickup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickup = await _context.Pickups.FindAsync(id);
            if (pickup == null)
                return NotFound();

            // Optionally delete photo file
            if (!string.IsNullOrEmpty(pickup.PhotoUrl))
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, pickup.PhotoUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Pickups.Remove(pickup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string GetUploadedFileName(Pickup pickup)
        {
            if (pickup.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(pickup.Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pickup.Photo.CopyTo(fileStream);
                }
                return uniqueFileName;
            }

            return "";
        }

        private void PopulateCollectorsAndBins(int? selectedCollectorId = null, int? selectedBinId = null)
        {
            ViewBag.Collectors = new SelectList(_context.Collectors, "CollectorId", "Name", selectedCollectorId);
            ViewBag.Bins = new SelectList(_context.Bins, "BinId", "PlateID", selectedBinId);
        }
    }
}
