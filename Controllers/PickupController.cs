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

        public IActionResult Index()
        {
            var pickups = _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)  // Include Bin navigation
                .ToList();

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
        public IActionResult Create(Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                pickup.PickupTime = DateTimeOffset.Now;

                string uniqueFileName = GetUploadedFileName(pickup);
                pickup.PhotoUrl = uniqueFileName;

                _context.Pickups.Add(pickup);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        private string GetUploadedFileName(Pickup pickup)
        {
            string uniqueFileName = null;

            if (pickup.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + pickup.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pickup.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Details(int id)
        {
            var pickup = _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)
                .FirstOrDefault(p => p.PickupId == id);

            if (pickup == null)
                return NotFound();

            return View(pickup);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pickup = _context.Pickups.Find(id);
            if (pickup == null)
                return NotFound();

            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                var existingPickup = _context.Pickups.AsNoTracking().FirstOrDefault(p => p.PickupId == pickup.PickupId);
                if (existingPickup == null)
                    return NotFound();

                // Preserve original PickupTime if you want
                pickup.PickupTime = existingPickup.PickupTime;

                
                // Handle photo update
                if (pickup.Photo != null)
                {
                    string uniqueFileName = GetUploadedFileName(pickup);
                    existingPickup.PhotoUrl = uniqueFileName;
                }

                _context.Update(pickup);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            PopulateCollectorsAndBins(pickup.CollectorId, pickup.BinId);
            return View(pickup);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var pickup = _context.Pickups
                .Include(p => p.Collector)
                .Include(p => p.Bin)
                .FirstOrDefault(p => p.PickupId == id);

            if (pickup == null)
                return NotFound();

            return View(pickup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pickup = _context.Pickups.Find(id);
            if (pickup == null)
                return NotFound();

            _context.Pickups.Remove(pickup);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCollectorsAndBins(int? selectedCollectorId = null, int? selectedBinId = null)
        {
            ViewData["Collectors"] = new SelectList(_context.Collectors, "CollectorId", "Name", selectedCollectorId);
            ViewData["Bins"] = new SelectList(_context.Bins, "BinId", "PlateID", selectedBinId);
        }

        
    }
}
