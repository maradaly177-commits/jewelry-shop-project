using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Key]
        public int Id { get; set; }   // ✅ DÙNG INT (KHỚP DB)

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }

        public string? Image { get; set; }

        public string? Material { get; set; }
        public string? Gemstone { get; set; }
        public string? Size { get; set; }

        public int CategoryId { get; set; }  // ✅ cũng dùng int

        [NotMapped]
        public virtual Category? Category { get; set; }
    }
}