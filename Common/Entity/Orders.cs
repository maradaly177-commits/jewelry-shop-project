using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể đơn hàng
    /// Người thực hiện: Marada
    /// </summary>
    [Table("orders")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// Mã đơn hàng (không dùng Id)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderCode { get; set; } = string.Empty;

        /// <summary>
        /// ID người dùng
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Tổng tiền đơn hàng
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public string? ShippingAddress { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? Phone { get; set; }
    }
}