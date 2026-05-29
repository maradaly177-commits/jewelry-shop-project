using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể Danh mục sản phẩm trang sức (Nhẫn, Dây chuyền, Bông tai...)
    /// Người thực hiện: Marada (MSSV: 5001467)
    /// </summary>
    [Table("categories")] // Đảm bảo tên bảng khớp với MySQL
    public class Category : BaseEntity
    {
        [Key]
        // Đổi sang string để đồng bộ với BaseRepository đã sửa
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        // Có thể thêm mô tả cho danh mục (ví dụ: Trang sức cao cấp, Bộ sưu tập hè)
        public string? Description { get; set; }
    }
}