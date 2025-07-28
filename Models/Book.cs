//using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Book
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(50)]
        public string Title { get; set; }

        //[MaxLength(250)]
        public string Description { get; set; } // Allows null by default for reference types

        //[Required]
        //[MaxLength(50)]
        public string Author { get; set; }

        //[Required]
        //[Range(1, 1000)]
        //[Column("BookPrice")]
        public double Price { get; set; }

        // Foreign key to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

