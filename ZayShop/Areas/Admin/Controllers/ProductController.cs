using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Areas.Admin.Models.Product;
using ZayShop.Data;
using ZayShop.Entities;
using ZayShop.Utilities.File;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public ProductController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ProductIndexVM
            {
                Products = _context.Products.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateVM
            {
                Categories = _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateVM productModel)
        {
            if (!ModelState.IsValid) return View();

            var product = _context.Products.FirstOrDefault(p => p.Name.ToLower() == productModel.Name.ToLower());
            if (product is not null)
            {
                ModelState.AddModelError("Name", "This product already exists");
                return View();
            }

            var category = _context.Categories.Find(productModel.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError("CategoryId", "This category doesn't exist");
                return View();
            }
            if (!_fileService.isImage(productModel.Image.ContentType))
                ModelState.AddModelError("Image", "Format of file must be image");
            if (!_fileService.isValidSize(productModel.Image.Length))
                ModelState.AddModelError("Image", "Size of image is to big. Size must be less than 100KB");

            var imageName = _fileService.Upload(productModel.Image, "assets/img");

            product = new Product
            {
                CreatedAt = DateTime.Now,
                Name = productModel.Name,
                ImageName = imageName,
                Price = productModel.Price,
                CategoryId = productModel.CategoryId
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        //delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            _fileService.Delete("assets/img", product.ImageName);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null) return NotFound();
            var productModel = new ProductUpdateVM
            {
                Name = product.Name,
                ImageName= product.ImageName,
                Price = product.Price,
                Categories = _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return View(productModel);
        }
        [HttpPost]
        public IActionResult Update(ProductUpdateVM productModel, int id)
        {
            if (!ModelState.IsValid) return View(productModel);
            var product = _context.Products.Find(id);
            if (product is null) return NotFound();
            var existProduct = _context.Products.Any(c => c.Name.ToLower() == productModel.Name.ToLower() && c.Id != id);
            if (existProduct)
            {
                ModelState.AddModelError("Name", "Name is already exists");
                return View();
            }
            if (product.Name != productModel.Name)
                product.UpdatedAt = DateTime.Now;

            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.CategoryId = productModel.CategoryId;
            if (productModel.Image is not null)
            {
                if (!_fileService.isImage(productModel.Image.ContentType))
                    ModelState.AddModelError("Image", "Format of file must be image");
                if (!_fileService.isValidSize(productModel.Image.Length))
                    ModelState.AddModelError("Image", "Size of image is to big. Size must be less than 100KB");
                _fileService.Delete("assets/img", product.ImageName);
                product.ImageName = _fileService.Upload(productModel.Image, "assets/img");
            }
            product.UpdatedAt = DateTime.Now;

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
