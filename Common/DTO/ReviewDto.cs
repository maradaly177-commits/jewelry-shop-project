namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO hiển thị đánh giá sản phẩm
    /// Người thực hiện: Vortey
    /// </summary>
    public class ReviewDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền...)
        /// </summary>
        public int? ProductVariantId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        /// <summary>
        /// Thời gian tạo (đồng bộ BaseEntity)
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Hiển thị size (13,14,15…)
        /// </summary>
        public string? Size { get; set; }
    }
}