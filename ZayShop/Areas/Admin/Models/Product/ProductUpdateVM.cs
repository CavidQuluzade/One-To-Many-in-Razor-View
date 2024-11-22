using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Product
{
    public class ProductUpdateVM
    {
        [Required(ErrorMessage = "Name required")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price required")]
        public decimal Price { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }

        [Required]
        [Display(Name = "Work Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
