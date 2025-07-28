using System.ComponentModel.DataAnnotations;

namespace EVABookShop.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Book title must be between 2 and 200 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.,!?'""()]+$", ErrorMessage = "Book title can only contain letters, numbers, spaces, and common punctuation marks.")]
        public string Title { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Author name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-'.,]+$", ErrorMessage = "Author name can only contain letters, spaces, hyphens, apostrophes, and periods.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, 1000, ErrorMessage = "Price must be between $1 and $1000.")]
        public double Price { get; set; }

        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
    }
}
