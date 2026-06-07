using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Base entity chứa các thuộc tính chung cho toàn hệ thống
    /// Người thực hiện: Marada
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Khóa chính (Primary Key)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Ngày tạo dữ liệu
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Người tạo dữ liệu
        /// </summary>
        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật gần nhất
        /// </summary>
        public DateTimeOffset? ModifiedDate { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }
    }
}