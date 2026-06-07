using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO dùng khi checkout (đặt hàng)
    /// Người thực hiện: Marada
    /// </summary>
    public class CheckoutRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Danh sách sản phẩm đặt hàng
        /// </summary>
        public List<CheckoutItemDto> Items { get; set; } = new();

        /// <summary>
        /// Ghi chú đơn hàng
        /// </summary>
        public string? Note { get; set; }
    }

    /// <summary>
    /// Chi tiết từng sản phẩm trong đơn hàng
    /// </summary>
    public class CheckoutItemDto
    {
        public int ProductId { get; set; }

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền…)
        /// </summary>
        public int? ProductVariantId { get; set; }

        public int Quantity { get; set; }
    }
}