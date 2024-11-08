using Microsoft.AspNetCore.Mvc;
using ZayShop.Data;
using ZayShop.Models.Slider;

namespace ZayShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new SliderIndexVM
            {
                Sliders = _context.Sliders.ToList()
            };

            return View(model);
        }
    }
}