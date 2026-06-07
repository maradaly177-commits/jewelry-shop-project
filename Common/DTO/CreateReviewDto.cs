using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.DTO
{
    /// <summary>
    /// DTO tạo đánh giá sản phẩm
    /// Người thực hiện: Vortey
    /// </summary>
    public class CreateReviewDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Nếu có size (nhẫn, dây chuyền...)
        /// </summary>
        public int? ProductVariantId { get; set; }

        /// <summary>
        /// Điểm đánh giá từ 1 đến 5
        /// </summary>
        [Range(1, 5)]
        public int Rating { get; set; }

        /// <summary>
        /// Nội dung đánh giá
        /// </summary>
        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}