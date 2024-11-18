﻿using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
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
            //// Use Include() to load the related Productas (assets) for each package
            //var packages = await _context.Packages
            //    .Include(p => p.Productas)  // Include the Productas (assets)
            //    .ToListAsync();

            //return View(packages);

            // Use Include() to load the related Productas (assets) for each package
            var packages = await _context.Packages
                .Include(p => p.Productas)
                .Where(p => p.Productas.Any(a => a.AvailableQuantity > 0))  // Only show packages with available assets
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
                    IsSelected = false,
                    PackageId = a.PackageId  // Include PackageId here

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

                        // Explicitly attach the entity to the DbContext
                        _context.Attach(producta);


                        producta.PackageId = package.Id;  // Assign asset to this package
                        package.Productas.Add(producta);  // Add asset to package's Productas collection
                    }
                }

                // Save the updates for Producta and the package
                await _context.SaveChangesAsync();

                // Redirect back to the package index
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }



        // GET: Edit an existing package
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch the package along with its associated assets
            var package = await _context.Packages
                .Include(p => p.Productas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            // Fetch all available assets
            var allAssets = await _context.Productsa.ToListAsync();

            // Create the view model for the edit page
            var viewModel = new PackageViewModel
            {
                
                PackageNumber = package.PackageNumber,
                Assets = allAssets.Select(a => new AssetCheckboxViewModel
                {
                    AssetId = a.Id,
                    AssetName = a.Name,
                    Brand = a.Brand,
                    Price = a.Price,
                    IsSelected = package.Productas.Any(pa => pa.Id == a.Id), // Check if this asset is already assigned to the package
                    PackageId = a.PackageId
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Update the existing package
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PackageViewModel viewModel)
        {
            // Fetch the package to be updated
            var package = await _context.Packages
                .Include(p => p.Productas) // Load the associated assets
                .FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the package number
                package.PackageNumber = viewModel.PackageNumber;

                // Clear the current assets associated with the package
                package.Productas.Clear();

                // Now handle the selected assets
                var selectedAssets = viewModel.Assets.Where(a => a.IsSelected).ToList();
                foreach (var asset in selectedAssets)
                {
                    var producta = await _context.Productsa.FindAsync(asset.AssetId);
                    if (producta != null)
                    {
                        // Explicitly attach the entity to the DbContext
                        _context.Attach(producta);

                        producta.PackageId = package.Id;  // Assign asset to this package
                        package.Productas.Add(producta);  // Add asset to package's Productas collection
                    }
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Redirect back to the package index
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, reload the assets for the view model and return to the edit view
            var allAssets = await _context.Productsa.ToListAsync();
            viewModel.Assets = allAssets.Select(a => new AssetCheckboxViewModel
            {
                AssetId = a.Id,
                AssetName = a.Name,
                Brand = a.Brand,
                Price = a.Price,
                
                IsSelected = package.Productas.Any(pa => pa.Id == a.Id) // Check if this asset is already assigned to the package
            }).ToList();

            return View(viewModel);
        }


        //------------------------------------------------------------------------------------------------------
        // GET: Delete confirmation page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Fetch the package to delete along with its assets
            var package = await _context.Packages
                .Include(p => p.Productas)  // Include the associated assets (Productas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Delete the package
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Fetch the package to delete along with its assets
            var package = await _context.Packages
                .Include(p => p.Productas)  // Include the associated assets (Productas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            // Option 1: Remove the PackageId from Productas (set it to null)
            foreach (var producta in package.Productas)
            {
                producta.PackageId = null;  // Remove the relationship by setting PackageId to null
            }

            // Remove the package itself
            _context.Packages.Remove(package);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the package index page
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(List<int> selectedPackages)
        {
            if (selectedPackages == null || selectedPackages.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            // Fetch selected packages with their assets
            var packages = await _context.Packages
                .Where(p => selectedPackages.Contains(p.Id))
                .Include(p => p.Productas)
                .ToListAsync();

            // Calculate the total price
            var totalAmount = packages.Sum(p => p.Productas.Sum(a => a.Price * a.AvailableQuantity));

            // Prepare the CheckoutViewModel
            var checkoutViewModel = new CheckoutViewModel
            {
                SelectedPackageIds = selectedPackages,
                TotalAmount = totalAmount,
                Packages = packages

            };

            return View(checkoutViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmCheckout(CheckoutViewModel model)
        {
            if (model.SelectedPackageIds == null || model.SelectedPackageIds.Count == 0)
            {
                return RedirectToAction(nameof(Index)); // Redirect if no packages are selected
            }

            // Fetch selected packages with their assets
            var packages = await _context.Packages
                .Where(p => model.SelectedPackageIds.Contains(p.Id))
                .Include(p => p.Productas)
                .ToListAsync();

            // Deduct inventory for each package's assets and check for out-of-stock items
            foreach (var package in packages)
            {
                foreach (var asset in package.Productas)
                {
                    var newQuantity = asset.AvailableQuantity - 1;

                    // Check for stock availability
                    if (newQuantity < 0)
                    {
                        // Log the out-of-stock situation
                        Console.WriteLine($"Out of stock for Asset: {asset.Name} in Package: {package.PackageNumber}");
                        return BadRequest("One or more items in your package are out of stock.");
                    }

                    asset.AvailableQuantity = newQuantity;
                    _context.Entry(asset).State = EntityState.Modified; // Ensure tracking state
                }
            }

            // Save changes to update inventory in the database
            await _context.SaveChangesAsync();

            // Prepare a confirmation view model
            var confirmationViewModel = new ConfirmationViewModel
            {
                Packages = packages,
                TotalAmount = model.TotalAmount,
                PaymentStatus = "Success"
            };

            // Render the ConfirmCheckout view with the confirmationViewModel
            return View("ConfirmCheckout", confirmationViewModel);
        }





    }
}