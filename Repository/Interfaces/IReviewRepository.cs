using Cosmetic_App.Common.DTO;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Repository xử lý Review sản phẩm
    /// Người thực hiện: Vortey
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Thêm đánh giá sản phẩm
        /// </summary>
        Task AddReview(CreateReviewDto dto);

        /// <summary>
        /// Lấy danh sách review theo sản phẩm
        /// </summary>
        Task<IEnumerable<ReviewDto>> GetReviewsByProduct(int productId);

        /// <summary>
        /// Lấy điểm đánh giá trung bình
        /// </summary>
        Task<double> GetAverageRating(int productId);

        /// <summary>
        /// Đếm số review của sản phẩm
        /// </summary>
        Task<int> GetReviewCount(int productId);

        /// <summary>
        /// Kiểm tra user có thể review hay không
        /// </summary>
        Task<bool> CanReview(int userId, int productId);
    }
}