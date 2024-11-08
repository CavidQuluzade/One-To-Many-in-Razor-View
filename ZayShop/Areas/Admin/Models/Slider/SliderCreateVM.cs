using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Tittle required")]
        [MinLength(3, ErrorMessage = "Tittle must contain at least 3 character")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "SubTittle required")]
        [MinLength(3, ErrorMessage = "SubTittle must contain at least 3 character")]
        [Display(Name = "SubTitle")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Description required")]
        [MinLength(3, ErrorMessage = "Description must contain at least 3 character")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Order required")]
        [Range(1,3, ErrorMessage = "Allowed order is 1-3")]
        [Display(Name = "Order")]
        public int Order { get; set; }
        [Required(ErrorMessage = "Image required")]
        [Display(Name = "Image")]
        public string Image { get; set; }


    }
}
