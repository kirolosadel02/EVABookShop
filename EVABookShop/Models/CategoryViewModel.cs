namespace EVABookShop.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; } // For internal use only
        public string CatName { get; set; }
        public int CatOrder { get; set; }
        public bool IsActive { get; set; }
    }
} 