using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Cosmetic_App.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // =========================
        // ADD TO CART
        // =========================
        public async Task AddToCart(int userId, int productId, int quantity)
        {
            using var conn = GetConnection();

            var existing = await conn.QueryFirstOrDefaultAsync(
                @"SELECT * FROM cart 
                  WHERE UserId = @userId AND ProductId = @productId",
                new { userId, productId });

            if (existing != null)
            {
                await conn.ExecuteAsync(
                    @"UPDATE cart 
                      SET Quantity = Quantity + @quantity
                      WHERE UserId = @userId AND ProductId = @productId",
                    new { userId, productId, quantity });
            }
            else
            {
                await conn.ExecuteAsync(
                    @"INSERT INTO cart (UserId, ProductId, Quantity)
                      VALUES (@userId, @productId, @quantity)",
                    new { userId, productId, quantity });
            }
        }

        // =========================
        // GET CART (FIXED)
        // =========================
        public async Task<IEnumerable<CartItemDto>> GetCart(int userId)
        {
            using var conn = GetConnection();

            string sql = @"
                SELECT 
                    c.Id,
                    c.ProductId,
                    c.Quantity,
                    p.ProductName,
                    p.UnitPrice,
                    p.Image
                FROM cart c
                INNER JOIN products p ON c.ProductId = p.Id
                WHERE c.UserId = @userId
            ";

            return await conn.QueryAsync<CartItemDto>(sql, new { userId });
        }

        // =========================
        // INCREASE
        // =========================
        public async Task Increase(int userId, int productId)
        {
            using var conn = GetConnection();

            await conn.ExecuteAsync(
                @"UPDATE cart 
                  SET Quantity = Quantity + 1
                  WHERE UserId = @userId AND ProductId = @productId",
                new { userId, productId });
        }

        // =========================
        // DECREASE
        // =========================
        public async Task Decrease(int userId, int productId)
        {
            using var conn = GetConnection();

            await conn.ExecuteAsync(
                @"UPDATE cart 
                  SET Quantity = Quantity - 1
                  WHERE UserId = @userId 
                  AND ProductId = @productId
                  AND Quantity > 1",
                new { userId, productId });

            await conn.ExecuteAsync(
                @"DELETE FROM cart 
                  WHERE UserId = @userId 
                  AND ProductId = @productId
                  AND Quantity <= 0",
                new { userId, productId });
        }

        // =========================
        // REMOVE
        // =========================
        public async Task Remove(int userId, int productId)
        {
            using var conn = GetConnection();

            await conn.ExecuteAsync(
                @"DELETE FROM cart 
                  WHERE UserId = @userId AND ProductId = @productId",
                new { userId, productId });
        }
    }
}