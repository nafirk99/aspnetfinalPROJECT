using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly InventoryDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ItemController(InventoryDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index(string searchString)
        {
            // Storing the Current search query in the ViewData so taht we can use it in the view  
            ViewData["Filter"] = searchString;

            // Retrieve All the Products
            var items = _context.Items
                .Include(p => p.Category)
                .AsQueryable();

            // Filter Products Based On the search Query
            if (!string.IsNullOrEmpty(searchString))
            {
                items = _context.Items.Where(p => p.Name.Contains(searchString));
            }
            return View(items);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemDTO itemDTO)
        {

            if (itemDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image File Is Required");
            }

            if (!ModelState.IsValid)
            {
                return View(itemDTO);
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(itemDTO);
            }

            // Save image logic here (same as before)
            //Save The Image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(itemDTO.ImageFile!.FileName);

            string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                itemDTO.ImageFile.CopyTo(stream);
            }



            var item = new Item
            {
                Name = itemDTO.Name,
                Brand = itemDTO.Brand,
                Price = itemDTO.Price,
                Description = itemDTO.Description,
                ImageFileName = newFileName,    //-------For Image 
                CategoryId = itemDTO.CategoryId,     // Assign category
                SKU = itemDTO.SKU,
                CreatedBy = itemDTO.CreatedBy,
                TotalQuantity = itemDTO.TotalQuantity,
                ModelNumber = itemDTO.ModelNumber,
                AvailableQuantity = itemDTO.AvailableQuantity,
                StockPrice = itemDTO.StockPrice,
                CreatedAt = DateTime.Now
                //VendorId = productDTO.VendorId,           // Assign vendor
                //GroupId = productDTO.GroupId,            // Assign group
                //LocationId = productDTO.LocationId,    // Assign location  
                //ModelNumber = productDTO.ModelNumber,
            };

            _context.Items.Add(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var itemDTO = new ItemDTO
            {
                Name = item.Name,
                Brand = item.Brand,
                Price = item.Price,
                Description = item.Description,
                CategoryId = item.CategoryId,
                SKU = item.SKU,
                CreatedBy = item.CreatedBy,
                TotalQuantity = item.TotalQuantity,
                ModelNumber = item.ModelNumber,
                AvailableQuantity = item.AvailableQuantity,
                StockPrice = item.StockPrice
                //VendorId = product.VendorId,
                //GroupId = product.GroupId,
                //LocationId = product.LocationId
            };

            ViewData["ProductId"] = item.Id;
            ViewData["ImageFileName"] = item.ImageFileName;
            ViewData["CreatedAt"] = item.CreatedAt;

            ViewBag.Categories = _context.Categories.ToList();
            // New Code
            //ViewBag.Vendors = _context.Vendors.ToList();
            //ViewBag.Groups = _context.Groups.ToList();
            //ViewBag.Locations = _context.Locations.ToList();
            return View(itemDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, ItemDTO itemDTO)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                // New Code
                //ViewBag.Vendors = _context.Vendors.ToList();
                //ViewBag.Groups = _context.Groups.ToList();
                //ViewBag.Locations = _context.Locations.ToList();
                return View(itemDTO);
            }

            //-------------
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = item.Id;
                ViewData["ImageFileName"] = item.ImageFileName;
                ViewData["CreatedAt"] = item.CreatedAt;
                return View(itemDTO);
            }

            //-------------------
            // Update the image file if we have a new image file
            string newFileName = item.ImageFileName;
            if (itemDTO.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(itemDTO.ImageFile!.FileName);

                string imageFullPath = _environment.WebRootPath + "/Products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    itemDTO.ImageFile.CopyTo(stream);
                }


                //Delete Old Image
                string oldImageFullPath = _environment.WebRootPath + "/Products" + item.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            item.Name = itemDTO.Name;
            item.Brand = itemDTO.Brand;
            item.Price = itemDTO.Price;
            item.Description = itemDTO.Description;
            item.ImageFileName = newFileName;
            item.CategoryId = itemDTO.CategoryId;
            item.SKU = itemDTO.SKU;
            item.CreatedBy = itemDTO.CreatedBy;
            item.TotalQuantity = itemDTO.TotalQuantity;
            item.ModelNumber = itemDTO.ModelNumber;
            item.AvailableQuantity = itemDTO.AvailableQuantity;
            item.StockPrice = itemDTO.StockPrice;
            //item.VendorId = productDTO.VendorId;
            //item.GroupId = productDTO.GroupId;
            //product.LocationId = productDTO.LocationId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
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
            var itemsToDelete = _context.Items
                .Where(p => selectedProducts.Contains(p.Id))
                .ToList();

            // Remove selected products
            _context.Items.RemoveRange(itemsToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
