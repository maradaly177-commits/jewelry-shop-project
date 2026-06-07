using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể danh mục sản phẩm
    /// Người thực hiện: Marada
    /// </summary>
    [Table("categories")]
    public class Category : BaseEntity
    {
        /// <summary>
        /// Tên danh mục (Nhẫn, Dây chuyền…)
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả danh mục
        /// </summary>
        public string? Description { get; set; }
    }
}