using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Category
{
    public class CategoryUpdateVM
    {
        [Required(ErrorMessage = "Name required")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 character")]
        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}
