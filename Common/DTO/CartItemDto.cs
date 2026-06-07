using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO hiển thị item trong giỏ hàng
    /// Người thực hiện: Marada
    /// </summary>
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền...)
        /// </summary>
        public int? ProductVariantId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        public string ProductName { get; set; } = "";

        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Giá * số lượng (FE không cần tự tính)
        /// </summary>
        public decimal TotalPrice => UnitPrice * Quantity;

        public string Image { get; set; } = "";

        /// <summary>
        /// Size hiển thị (13,14,15…)
        /// </summary>
        public string? Size { get; set; }
    }
}