using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Name required")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 character")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
