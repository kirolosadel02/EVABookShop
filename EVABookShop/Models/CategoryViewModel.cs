using System.ComponentModel.DataAnnotations;

namespace EVABookShop.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_]+$", ErrorMessage = "Category name can only contain letters, numbers, spaces, hyphens, and underscores.")]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }

        [Required(ErrorMessage = "Category order is required.")]
        [Range(1, 9999, ErrorMessage = "Category order must be between 1 and 9999.")]
        [Display(Name = "Category Order")]
        public int CatOrder { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}