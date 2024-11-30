using Microsoft.AspNetCore.Mvc;
using ZayShop.Data;

namespace ZayShop.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BrandViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
    }
}
