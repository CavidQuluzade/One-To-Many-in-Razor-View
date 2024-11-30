using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayShop.Data;
using ZayShop.Models.Shop;

namespace ZayShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ShopIndexVM
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.ToList()
            };

            return View(model);

        }
        public IActionResult Detail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult FilterProduct(int categoryId)
        {
            var Product = _context.Products.Where(x => x.CategoryId == categoryId).ToList();
            if (categoryId == 0)
            {
                Product = _context.Products.ToList();
            }
            return PartialView("_ProductPartial", Product);
        }
    }
}
