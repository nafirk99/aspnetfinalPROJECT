using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructutre;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        private InventoryDbContext _context;

        public GroupController(InventoryDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var group = _context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(searchString)) 
            {
                group = group.Where(g => g.Name.Contains(searchString));
            }

            return View(group);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(groupDTO);
            }

            var group = new Group()
            {
                Name = groupDTO.Name
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Edit(int id)
        {
            var group = _context.Groups.Find(id);
            if(group == null)
            {
                return NotFound();
            }

            var groupDTO = new GroupDTO()
            {
                Name = group.Name
            };
            return View(groupDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, GroupDTO groupDTO)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(groupDTO);
            }

            group.Name = groupDTO.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
