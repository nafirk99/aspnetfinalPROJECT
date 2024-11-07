using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using DevSkill.Inventory.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsaController : Controller
    {
        private readonly InventoryDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsaController(InventoryDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Main Dashboard For EZO
        public IActionResult Dashboarda()
        {
            var dashboardData = new DashboardViewModel
            {
                ProductCount =  _context.Productsa.Count(),
                CategoryCount =  _context.Categories.Count(),
                VendorCount =  _context.Vendors.Count(),
                GroupCount =  _context.Groups.Count(),
                LocationCount =  _context.Locations.Count(),
                AllAssetPrice = _context.Productsa.Sum(p => p.Price),
                TotalQuantity = _context.Productsa.Sum(t => t.TotalQuantity),
                AvailableQuantity = _context.Productsa.Sum(a => a.AvailableQuantity)
            };

            //var p = _context.Productsa.Sum(a => a.TotalQuantity);

            return View(dashboardData);

            
        }

        
        
        //// GET: Products
        //public IActionResult Index()
        //{
        //    var products = _context.Productsa.Include(p => p.Category).ToList();
        //    return View(products);
        //}



        // New INdex Action
        public IActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            // Storing the Current search query in the ViewData so taht we can use it in the view  
            ViewData["Filter"] = searchString;

            // Retrieve All the Products
            var products = _context.Productsa
                .Include(p => p.Category)
                .Include(v => v.Vendor)
                .Include(g => g.Group)
                .Include(l => l.Location).AsQueryable();

            // Filter Products Based On the search Query
            if (!string.IsNullOrEmpty(searchString))
            {
                products = _context.Productsa.Where(p => p.Name.Contains(searchString));
            }

            int pageNumber = page ?? 1; // Default to page 1 if no page is specified
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);
            ViewBag.PageSize = pageSize;

            return View(pagedProducts);
        }

        public IActionResult AssetStockIndex(string searchString, int? page, int pageSize = 10)
        {
           

            // Storing the Current search query in the ViewData so taht we can use it in the view  
            ViewData["Filter"] = searchString;

            // Retrieve All the Products
            var products = _context.Productsa
                .Include(p => p.Category)
                .Include(v => v.Vendor)
                .Include(g => g.Group)
                .Include(l => l.Location).AsQueryable();

            // Filter Products Based On the search Query
            if (!string.IsNullOrEmpty(searchString))
            {
                products = _context.Productsa.Where(p => p.Name.Contains(searchString));
            }
            int pageNumber = page ?? 1; // Default to page 1 if no page is specified
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            ViewBag.PageSize = pageSize;
            return View(pagedProducts);
        }





        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            //New Code
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Groups = _context.Groups.ToList();
            ViewBag.Locations = _context.Locations.ToList();

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {

            if (productDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image File Is Required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();

                // New Code
                ViewBag.Vendors = _context.Vendors.ToList();
                ViewBag.Groups = _context.Groups.ToList();
                ViewBag.Locations = _context.Locations.ToList();
                return View(productDTO);
            }

            // Save image logic here (same as before)
            //Save The Image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);

            string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDTO.ImageFile.CopyTo(stream);
            }



            var product = new Producta
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Price = productDTO.Price,
                Description = productDTO.Description,
                AIN = productDTO.AIN,
                CreatedBy = productDTO.CreatedBy,
                VendorId = productDTO.VendorId,           // Assign vendor
                GroupId = productDTO.GroupId,            // Assign group
                CategoryId = productDTO.CategoryId,     // Assign category
                LocationId = productDTO.LocationId,    // Assign location  
                CreatedAt = DateTime.Now,
                ImageFileName = newFileName,    //-------For Image 
                TotalQuantity =productDTO.TotalQuantity,
                ModelNumber = productDTO.ModelNumber,
                AvailableQuantity = productDTO.AvailableQuantity,
                StockPrice = productDTO.StockPrice
            };

            _context.Productsa.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/{id}
        public IActionResult Edit(int id)
        {
            var product = _context.Productsa.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                AIN = product.AIN,
                CreatedBy = product.CreatedBy,
                VendorId = product.VendorId,
                GroupId = product.GroupId,
                CategoryId = product.CategoryId,
                LocationId = product.LocationId,
                TotalQuantity = product.TotalQuantity,
                ModelNumber = product.ModelNumber,
                AvailableQuantity= product.AvailableQuantity,
                StockPrice= product.StockPrice
            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreatedAt;

            ViewBag.Categories = _context.Categories.ToList();
            // New Code
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Groups = _context.Groups.ToList();
            ViewBag.Locations = _context.Locations.ToList();
            return View(productDTO);
        }

        // POST: Products/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, ProductDTO productDTO)
        {
            var product = _context.Productsa.Find(id);
            if (product == null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                // New Code
                ViewBag.Vendors = _context.Vendors.ToList();
                ViewBag.Groups = _context.Groups.ToList();
                ViewBag.Locations = _context.Locations.ToList();
                return View(productDTO);
            }

            //-------------
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreatedAt;
                return View(productDTO);
            }

            //-------------------
            // Update the image file if we have a new image file
            string newFileName = product.ImageFileName;
            if (productDTO.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);

                string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }


                //Delete Old Image
                string oldImageFullPath = _environment.WebRootPath + "/Products" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }





            product.Name = productDTO.Name;
            product.Brand = productDTO.Brand;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.AIN = productDTO.AIN;
            product.CreatedBy = productDTO.CreatedBy;
            product.VendorId = productDTO.VendorId;
            product.GroupId = productDTO.GroupId;
            product.CategoryId = productDTO.CategoryId;
            product.ImageFileName = newFileName;
            product.LocationId = productDTO.LocationId;
            product.TotalQuantity = productDTO.TotalQuantity;
            product.ModelNumber = productDTO.ModelNumber;
            product.AvailableQuantity = productDTO.AvailableQuantity;
            product.StockPrice = productDTO.StockPrice;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/{id}
        public IActionResult Delete(int id)
        {
            var product = _context.Productsa.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Productsa.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // Bulk Delete Action
        [HttpPost]
        public IActionResult BulkDelete(int[] selectedProducts)
        {
            if (selectedProducts == null || selectedProducts.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            // Retrieve the selected products
            var productsToDelete = _context.Productsa
                .Where(p => selectedProducts.Contains(p.Id))
                .ToList();

            // Remove selected products
            _context.Productsa.RemoveRange(productsToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        // Asset Stock EDIT-----------------------------------------------------------------
        public IActionResult AssetStockEdit(int id)
        {
            var product = _context.Productsa.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                AIN = product.AIN,
                CreatedBy = product.CreatedBy,
                VendorId = product.VendorId,
                GroupId = product.GroupId,
                CategoryId = product.CategoryId,
                LocationId = product.LocationId,
                TotalQuantity = product.TotalQuantity,
                ModelNumber = product.ModelNumber,
                AvailableQuantity = product.AvailableQuantity,
                StockPrice = product.StockPrice
            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreatedAt;

            ViewBag.Categories = _context.Categories.ToList();
            // New Code
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Groups = _context.Groups.ToList();
            ViewBag.Locations = _context.Locations.ToList();
            return View(productDTO);
        }

        // POST: Products/Edit/{id}
        [HttpPost]
        public IActionResult AssetStockEdit(int id, ProductDTO productDTO)
        {
            var product = _context.Productsa.Find(id);
            if (product == null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                // New Code
                ViewBag.Vendors = _context.Vendors.ToList();
                ViewBag.Groups = _context.Groups.ToList();
                ViewBag.Locations = _context.Locations.ToList();
                return View(productDTO);
            }

            //-------------
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreatedAt;
                return View(productDTO);
            }

            //-------------------
            // Update the image file if we have a new image file
            string newFileName = product.ImageFileName;
            if (productDTO.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);

                string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }


                //Delete Old Image
                string oldImageFullPath = _environment.WebRootPath + "/Products" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }
            product.Name = productDTO.Name;
            product.Brand = productDTO.Brand;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.AIN = productDTO.AIN;
            product.CreatedBy = productDTO.CreatedBy;
            product.VendorId = productDTO.VendorId;
            product.GroupId = productDTO.GroupId;
            product.CategoryId = productDTO.CategoryId;
            product.ImageFileName = newFileName;
            product.LocationId = productDTO.LocationId;
            product.TotalQuantity = productDTO.TotalQuantity;
            product.ModelNumber = productDTO.ModelNumber;
            product.AvailableQuantity = productDTO.AvailableQuantity;
            product.StockPrice = productDTO.StockPrice;

            _context.SaveChanges();
            return RedirectToAction(nameof(AssetStockIndex));
        }


        // Asset Stock CREATE-----------------------------------------------------------------
        public IActionResult AssetStockCreate()
        {
            ViewBag.Categories = _context.Categories.ToList();
            //New Code
            ViewBag.Vendors = _context.Vendors.ToList();
            ViewBag.Groups = _context.Groups.ToList();
            ViewBag.Locations = _context.Locations.ToList();

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public IActionResult AssetStockCreate(ProductDTO productDTO)
        {

            if (productDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image File Is Required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();

                // New Code
                ViewBag.Vendors = _context.Vendors.ToList();
                ViewBag.Groups = _context.Groups.ToList();
                ViewBag.Locations = _context.Locations.ToList();
                return View(productDTO);
            }

            // Save image logic here (same as before)
            //Save The Image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);

            string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDTO.ImageFile.CopyTo(stream);
            }



            var product = new Producta
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Price = productDTO.Price,
                Description = productDTO.Description,
                AIN = productDTO.AIN,
                CreatedBy = productDTO.CreatedBy,
                VendorId = productDTO.VendorId,           // Assign vendor
                GroupId = productDTO.GroupId,            // Assign group
                CategoryId = productDTO.CategoryId,     // Assign category
                LocationId = productDTO.LocationId,    // Assign location  
                CreatedAt = DateTime.Now,
                ImageFileName = newFileName,    //-------For Image 
                TotalQuantity = productDTO.TotalQuantity,
                ModelNumber = productDTO.ModelNumber,
                AvailableQuantity = productDTO.AvailableQuantity,
                StockPrice = productDTO.StockPrice
            };

            _context.Productsa.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(AssetStockIndex));
        }


        public IActionResult AssetReport()
        {
            // Retrieve asset data, you may apply filtering if needed
            var assets = _context.Productsa.ToList();

            return View(assets); // Pass the assets data to the view
        }


        public IActionResult ExportToExcel()
        {
            // Set the LicenseContext for EPPlus to avoid license exception
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Use this if your project is non-commercial

            // Fetch all assets/products
            var products = _context.Productsa.ToList();

            using (var package = new ExcelPackage())
            {
                // Create a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Asset Report");

                // Add column headers to the first row
                worksheet.Cells[1, 1].Value = "Asset#";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Total Quantity";
                worksheet.Cells[1, 5].Value = "Location";
                worksheet.Cells[1, 6].Value = "Model";
                worksheet.Cells[1, 7].Value = "Available Quantity";
                worksheet.Cells[1, 8].Value = "Stock Price";
                worksheet.Cells[1, 9].Value = "Creation Date";

                // Add asset data starting from the second row
                int row = 2;
                foreach (var product in products)
                {
                    worksheet.Cells[row, 1].Value = product.Id;
                    worksheet.Cells[row, 2].Value = product.Name;
                    worksheet.Cells[row, 3].Value = product.Description;
                    worksheet.Cells[row, 4].Value = product.TotalQuantity;
                    worksheet.Cells[row, 5].Value = product.Location?.Name;
                    worksheet.Cells[row, 6].Value = product.ModelNumber;
                    worksheet.Cells[row, 7].Value = product.AvailableQuantity;
                    worksheet.Cells[row, 8].Value = product.StockPrice;
                    worksheet.Cells[row, 9].Value = product.CreatedAt.ToString("yyyy-MM-dd");

                    row++;
                }

                // Set the content type and attachment header for download
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Asset_Report_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }


    }
}
