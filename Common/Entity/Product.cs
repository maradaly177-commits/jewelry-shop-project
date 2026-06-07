using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể sản phẩm
    /// Người thực hiện: Marada
    /// </summary>
    [Table("products")]
    public class Product : BaseEntity
    {
        /// <summary>
        /// Mã sản phẩm (không dùng Id)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }

        public string? Image { get; set; }

        public string? Material { get; set; }

        public string? Gemstone { get; set; }

        /// <summary>
        /// ID danh mục
        /// </summary>
        public int CategoryId { get; set; }
    }
}