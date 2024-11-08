using Microsoft.AspNetCore.Mvc;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Areas.Admin.Models.Slider;
using ZayShop.Data;
using ZayShop.Entities;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new SliderIndexVM
            {
                Sliders = _context.Sliders.ToList()
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SliderCreateVM sliderModel)
        {
            if (!ModelState.IsValid) return View();
            if (_context.Sliders.Count() == 3)
            {
                ModelState.AddModelError("Image", "Limit of sliders is three delete one to add new");
            }
            var slider = _context.Sliders.FirstOrDefault(s => s.Tittle.ToLower() == sliderModel.Title.ToLower());
            if (slider != null)
            {
                ModelState.AddModelError("Tittle", "This tittle already used");
                return View();
            }
            slider = _context.Sliders.FirstOrDefault(s => s.Order == sliderModel.Order);
            if (slider != null)
            {
                ModelState.AddModelError("Order", "You have a slider which order is same");
                return View();
            }
            slider = new Slider
            {
                Tittle = sliderModel.Title,
                SubTittle = sliderModel.SubTitle,
                ImgSrc = sliderModel.Image,
                Description = sliderModel.Description,
                Order = sliderModel.Order,
                CreatedAt = DateTime.Now
            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider == null) return NotFound();
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider == null) return NotFound();
            var sliderModel = new SliderUpdateVM
            {
                Title = slider.Tittle,
                SubTitle = slider.SubTittle,
                Image = slider.ImgSrc,
                Description = slider.Description,
                Order = slider.Order
            };
            return View(sliderModel);
        }
        [HttpPost]
        public IActionResult Update(int id, SliderUpdateVM sliderModel)
        {
            if (!ModelState.IsValid) return View();
            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            var existSlider = _context.Sliders.Any(s => s.Tittle.ToLower() == sliderModel.Title.ToLower() && s.Id != id);
            if (existSlider)
            {
                ModelState.AddModelError("Tittle", "Tittle is already exists");
                return View();
            }
            existSlider = _context.Sliders.Any(s => s.Order == sliderModel.Order && s.Id != id);
            if (slider.Tittle != sliderModel.Title)
                slider.UpdatedAt = DateTime.Now;

            slider.Tittle = sliderModel.Title;
            slider.SubTittle = sliderModel.SubTitle;
            slider.Order = sliderModel.Order;
            slider.Description = sliderModel.Description;
            slider.ImgSrc = sliderModel.Image;

            _context.Sliders.Update(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            var model = new SliderDetailVM
            {
                Title = slider.Tittle,
                SubTitle = slider.SubTittle,
                Order = slider.Order,
                Description = slider.Description,
                Image = slider.ImgSrc
            };
            return View(model);
        }
    }
}
