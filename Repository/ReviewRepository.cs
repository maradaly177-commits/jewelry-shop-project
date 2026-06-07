using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cosmetic_App.Repository
{
    // Người thực hiện: Vortey
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")!;
        }

        private MySqlConnection CreateConnection()
            => new MySqlConnection(_connectionString);

        // ================= ADD REVIEW =================
        public async Task AddReview(CreateReviewDto dto)
        {
            using var conn = CreateConnection();

            var sql = @"
                INSERT INTO reviews 
                    (ProductId, UserId, Rating, Comment, CreatedAt)
                VALUES 
                    (@ProductId, @UserId, @Rating, @Comment, NOW())";

            await conn.ExecuteAsync(sql, dto);
        }

        // ================= GET REVIEWS =================
        public async Task<IEnumerable<ReviewDto>> GetReviewsByProduct(int productId)
        {
            using var conn = CreateConnection();

            var sql = @"
                SELECT 
                    Id,
                    ProductId,
                    UserId,
                    Rating,
                    Comment,
                    CreatedAt
                FROM reviews
                WHERE ProductId = @productId
                ORDER BY CreatedAt DESC";

            return await conn.QueryAsync<ReviewDto>(sql, new { productId });
        }

        // ================= AVERAGE RATING =================
        public async Task<double> GetAverageRating(int productId)
        {
            using var conn = CreateConnection();

            var sql = @"
                SELECT IFNULL(AVG(Rating), 0)
                FROM reviews
                WHERE ProductId = @productId";

            return await conn.ExecuteScalarAsync<double>(sql, new { productId });
        }

        // ================= COUNT REVIEWS =================
        public async Task<int> GetReviewCount(int productId)
        {
            using var conn = CreateConnection();

            var sql = @"
                SELECT COUNT(*)
                FROM reviews
                WHERE ProductId = @productId";

            return await conn.ExecuteScalarAsync<int>(sql, new { productId });
        }

        // ================= CAN REVIEW =================
        public async Task<bool> CanReview(int userId, int productId)
        {
            using var conn = CreateConnection();

            var sql = @"
                SELECT COUNT(*)
                FROM orders o
                JOIN orderdetails od ON o.Id = od.OrderId
                WHERE o.UserId = @userId
                  AND od.ProductId = @productId
                  AND o.Status = 'DONE'";

            var count = await conn.ExecuteScalarAsync<int>(
                sql,
                new { userId, productId }
            );

            return count > 0;
        }
    }
}