using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Entity chi tiết giỏ hàng
    /// Người thực hiện: Vortey
    /// </summary>
    [Table("cart_items")]
    public class CartItem : BaseEntity
    {
        /// <summary>
        /// ID giỏ hàng
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền...)
        /// </summary>
        public int? ProductVariantId { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
    }
}