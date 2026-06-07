using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO đại diện đơn hàng
    /// Người thực hiện: Marada
    /// </summary>
    public class OrderDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Mã đơn hàng (không dùng Id)
        /// </summary>
        public string OrderCode { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Trạng thái: Pending, Completed, Cancelled...
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Thông tin người nhận
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Danh sách sản phẩm trong đơn
        /// </summary>
        public List<OrderDetailDto> Items { get; set; } = new();

        /// <summary>
        /// Ngày tạo (từ BaseEntity)
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}