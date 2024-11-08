using Microsoft.AspNetCore.Mvc;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Data;
using ZayShop.Entities;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var model = new CategoryIndexVM
            {
                Categories = _context.Categories.ToList()
            };


            return View(model);


        }
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateVM categoryModel)
        {
            if (!ModelState.IsValid) return View();

            var category = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryModel.Name.ToLower());
            if (category is not null)
            {
                ModelState.AddModelError("Name", "This category already exists");
                return View();
            }

            category = new Category
            {
                CreatedAt = DateTime.Now,
                Name = categoryModel.Name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        //delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Category = _context.Categories.Find(id);
            if (Category is null) return NotFound();

            _context.Categories.Remove(Category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        //update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null) return NotFound();
            var categoryModel = new CategoryUpdateVM
            {
                Name = category.Name,
            };
            return View(categoryModel);
        }
        [HttpPost]
        public IActionResult Update(CategoryUpdateVM categoryModel, int id)
        {
            if (!ModelState.IsValid) return View();
            var category = _context.Categories.Find(id);
            if (category is null) return NotFound();
            var existCategory = _context.Categories.Any(c => c.Name.ToLower() == categoryModel.Name.ToLower() && c.Id != id);
            if (existCategory)
            {
                ModelState.AddModelError("Name", "Name is already exists");
                return View();
            }
            if (category.Name != categoryModel.Name)
                category.UpdatedAt = DateTime.Now;

            category.Name = categoryModel.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
