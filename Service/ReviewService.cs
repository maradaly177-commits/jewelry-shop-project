using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ liên quan đến Review sản phẩm
    /// Vai trò: trung gian giữa Controller và Repository
    /// Người thực hiện: Vortey
    /// </summary>
    public class ReviewService : IReviewService
    {
        /// <summary>
        /// Repository xử lý truy vấn dữ liệu Review
        /// </summary>
        private readonly IReviewRepository _repo;

        /// <summary>
        /// Constructor inject repository
        /// </summary>
        public ReviewService(IReviewRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Thêm đánh giá mới cho sản phẩm
        /// </summary>
        public Task AddReview(CreateReviewDto dto)
            => _repo.AddReview(dto);

        /// <summary>
        /// Lấy danh sách review theo sản phẩm
        /// </summary>
        public Task<IEnumerable<ReviewDto>> GetReviewsByProduct(int productId)
            => _repo.GetReviewsByProduct(productId);

        /// <summary>
        /// Tính điểm trung bình đánh giá của sản phẩm
        /// </summary>
        public Task<double> GetAverageRating(int productId)
            => _repo.GetAverageRating(productId);

        /// <summary>
        /// Đếm số lượng review của sản phẩm
        /// </summary>
        public Task<int> GetReviewCount(int productId)
            => _repo.GetReviewCount(productId);

        /// <summary>
        /// Kiểm tra người dùng có được phép review hay không
        /// (ví dụ: chỉ được review nếu đã mua sản phẩm)
        /// </summary>
        public Task<bool> CanReview(int userId, int productId)
            => _repo.CanReview(userId, productId);
    }
}