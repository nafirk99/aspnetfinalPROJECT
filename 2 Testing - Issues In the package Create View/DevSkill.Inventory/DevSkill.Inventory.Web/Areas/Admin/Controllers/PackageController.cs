using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly InventoryDbContext _context;
        public PackageController(InventoryDbContext context)
        {
            _context = context;
        }


        // List all packages
        public async Task<IActionResult> Index()
        {
            // Use Include() to load the related Productas (assets) for each package
            var packages = await _context.Packages
                .Include(p => p.Productas)  // Include the Productas (assets)
                .ToListAsync();

            return View(packages);
        }


        // Add a new package (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var assets = await _context.Productsa.ToListAsync();
            var viewModel = new PackageViewModel
            {
                Assets = assets.Select(a => new AssetCheckboxViewModel
                {
                    AssetId = a.Id,
                    AssetName = a.Name,
                    Brand = a.Brand,
                    Price = a.Price,
                    IsSelected = false
                }).ToList()
            };
            return View(viewModel);
        }


        // Add a new package (POST)
        [HttpPost]
        public async Task<IActionResult> Create(PackageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new package and save it first to generate the ID
                var package = new Package
                {
                    PackageNumber = viewModel.PackageNumber,
                    CreatedAt = DateTime.Now,
                    Productas = new List<Producta>() // Initialize the Productas list
                };

                _context.Packages.Add(package);
                await _context.SaveChangesAsync();  // Save the package first to get the generated ID

                // Now handle the selected assets
                var selectedAssets = viewModel.Assets.Where(a => a.IsSelected).ToList();
                foreach (var asset in selectedAssets)
                {
                    var producta = await _context.Productsa.FindAsync(asset.AssetId);
                    if (producta != null)
                    {
                        producta.PackageId = package.Id;  // Assign asset to this package
                        package.Productas.Add(producta);  // Add asset to package's Productas collection

                        // Debugging: Log the asset and PackageId to see if it is being assigned
                        //Console.WriteLine($"Producta: {producta.Name}, PackageId: {producta.PackageId}");
                    }
                }

                // Save the updates for Producta and the package
                await _context.SaveChangesAsync();

                // Redirect back to the package index
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }








    }
}
