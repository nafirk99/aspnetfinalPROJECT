using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using DevSkill.Inventory.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly InventoryDbContext _context;

        public CategoriesController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public IActionResult Create(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }

            var category = new Category
            {
                Name = categoryDTO.Name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/{id}
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                Name = category.Name
            };

            return View(categoryDTO);
        }

        // POST: Categories/Edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, CategoryDTO categoryDTO)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }

            category.Name = categoryDTO.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/{id}
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
