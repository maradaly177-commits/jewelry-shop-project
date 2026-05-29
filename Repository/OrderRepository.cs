using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Dapper;
using MySqlConnector;
using System.Data;

namespace Cosmetic_App.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        // =========================
        // CHECKOUT
        // =========================
        public async Task<int> Checkout(int userId, string address, string phone)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var transaction = await conn.BeginTransactionAsync();

            try
            {
                var cart = (await conn.QueryAsync<CartItemTemp>(@"
                    SELECT c.ProductId, c.Quantity, p.UnitPrice
                    FROM cart c
                    JOIN products p ON c.ProductId = p.Id
                    WHERE c.UserId = @UserId
                ", new { UserId = userId }, transaction)).ToList();

                if (!cart.Any())
                    throw new Exception("Cart empty");

                decimal total = cart.Sum(x => x.UnitPrice * x.Quantity);

                var orderId = await conn.ExecuteScalarAsync<int>(@"
                    INSERT INTO orders 
                    (UserId, OrderDate, TotalAmount, Status, ShippingAddress, Phone)
                    VALUES 
                    (@UserId, NOW(), @Total, 'PENDING', @Address, @Phone);
                    SELECT LAST_INSERT_ID();
                ", new
                {
                    UserId = userId,
                    Total = total,
                    Address = address,
                    Phone = phone
                }, transaction);

                foreach (var item in cart)
                {
                    await conn.ExecuteAsync(@"
                        INSERT INTO orderdetails 
                        (OrderId, ProductId, Quantity, UnitPrice)
                        VALUES 
                        (@OrderId, @ProductId, @Quantity, @UnitPrice)
                    ", new
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }, transaction);
                }

                await conn.ExecuteAsync(
                    "DELETE FROM cart WHERE UserId = @UserId",
                    new { UserId = userId },
                    transaction
                );

                await transaction.CommitAsync();
                return orderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // =========================
        // GET ORDERS (LIST)
        // =========================
        public async Task<IEnumerable<OrderDto>> GetOrdersByUser(int userId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var sql = @"
                SELECT Id, OrderDate, TotalAmount, Status
                FROM orders
                WHERE UserId = @userId
                ORDER BY OrderDate DESC";

            return await conn.QueryAsync<OrderDto>(sql, new { userId });
        }

        // =========================
        // GET ORDER DETAIL
        // =========================
        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var sql = @"
                SELECT 
                    od.OrderId,
                    p.ProductName,
                    od.Quantity,
                    od.UnitPrice AS Price
                FROM orderdetails od
                JOIN products p ON p.Id = od.ProductId
                WHERE od.OrderId = @orderId";

            return await conn.QueryAsync<OrderDetailDto>(sql, new { orderId });
        }

        // TEMP CART DTO
        private class CartItemTemp
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}