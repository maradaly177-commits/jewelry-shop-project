using Cosmetic_App.Common.DTO;

public interface IReviewService
{
    /// Người thực hiện: Vortey
    Task AddReview(CreateReviewDto dto);

    Task<IEnumerable<ReviewDto>>
        GetReviewsByProduct(int productId);

    Task<double>
        GetAverageRating(int productId);

    Task<int>
        GetReviewCount(int productId);

    Task<bool>
        CanReview(int userId, int productId);
}