using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO đại diện chi tiết đơn hàng
    /// Người thực hiện: Vortey
    /// </summary>
    public class OrderDetailDto
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền...)
        /// </summary>
        public int? ProductVariantId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? Image { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        /// <summary>
        /// Giá tại thời điểm mua
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Tổng tiền (FE không cần tự tính)
        /// </summary>
        public decimal TotalPrice => Price * Quantity;

        /// <summary>
        /// Size hiển thị (13,14,15…)
        /// </summary>
        public string? Size { get; set; }
    }
}