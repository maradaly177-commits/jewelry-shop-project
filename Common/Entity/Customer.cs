using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể khách hàng
    /// Người thực hiện: Marada
    /// </summary>
    [Table("customers")]
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
    }
}