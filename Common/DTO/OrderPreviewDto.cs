namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO hiển thị danh sách đơn hàng (preview)
    /// Người thực hiện: Vortey
    /// </summary>
    public class OrderPreviewDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Mã đơn hàng (không dùng Id)
        /// </summary>
        public string OrderCode { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Tên các sản phẩm (ghép chuỗi)
        /// </summary>
        public string? ProductNames { get; set; }

        /// <summary>
        /// Ảnh đại diện (ảnh đầu tiên)
        /// </summary>
        public string? Images { get; set; }
        public string? Thumbnail { get; set; }

        public int ItemCount { get; set; }
    }
}