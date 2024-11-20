using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorController : Controller
    {
        private readonly InventoryDbContext _context;

        public VendorController(InventoryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var vendors = _context.Vendors.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                //vendors = vendors.Where(x => x.Name == searchString);
                vendors = vendors.Where(v => v.Name.Contains(searchString));
            }
            return View(vendors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VendorDTO vendorDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(vendorDTO);
            }

            var vendor = new Vendor()
            {
                Name = vendorDTO.Name
            };

            _context.Vendors.Add(vendor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            var vendorDTO = new VendorDTO()
            {
                Name = vendor.Name
            };

            return View(vendorDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, VendorDTO vendorDTO)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(vendorDTO);
            }

            vendor.Name = vendorDTO.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }
            _context.Vendors.Remove(vendor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
