using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Category
    {
        // [Key]
        public int Id { get; set; }
        // [Required]
        // [MaxLength(50)]
        public string CatName { get; set; }
        // [Required]
        public int CatOrder { get; set; }
        // [NotMapped]
        public DateTime CreatedDate { get; set; }
        // [Column("isDeleted")]
        public bool MarkedAsDeleted { get; set; }
    }
} 