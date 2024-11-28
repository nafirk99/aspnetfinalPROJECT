using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly InventoryDbContext _context;
        public LocationController(InventoryDbContext context)
        {
                _context = context;
        }
        public IActionResult Index(string searchString)
        {
            // Store the current search query in ViewData so it can be reused in the view
            ViewData["CurrentFilter"] = searchString;

            // Retrieve all Locations
            var locations = _context.Locations.AsQueryable();

            // Filter locations based on the search query
            if (!string.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(c => c.Name.Contains(searchString));
            }
            // Return the filtered or full category list to the view
            return View(locations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationDTO locationDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(locationDTO);
            }

            var location = new Location
            {
                Name = locationDTO.Name
            };

            _context.Locations.Add(location);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            var locationDTO = new LocationDTO
            {
                Name = location.Name
            };

            return View(locationDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, LocationDTO locationDTO)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(locationDTO);
            }

            location.Name = locationDTO.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
