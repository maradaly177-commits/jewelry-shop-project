using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// Repository xử lý Order (Checkout + History + Detail)
    /// Người thực hiện: Marada
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Missing connection string");
        }

        // =========================
        // CHECKOUT
        // =========================
        public async Task<int> Checkout(int userId, string address, string phone)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            try
            {
                // ================= GET CART =================
                var cart = (await conn.QueryAsync<CartItemTemp>(
                    @"
            SELECT c.ProductId, c.Quantity, p.UnitPrice
            FROM cart c
            LEFT JOIN products p ON p.Id = c.ProductId
            WHERE c.UserId = @UserId
            ",
                    new { UserId = userId },
                    transaction
                )).ToList();

                if (cart == null || cart.Count == 0)
                    throw new Exception("Cart is empty");

                // ================= TOTAL =================
                decimal total = cart.Sum(x => x.UnitPrice * x.Quantity);

                // ================= INSERT ORDER =================
                var orderId = conn.QuerySingle<int>(
                    @"
            INSERT INTO orders
            (UserId, OrderDate, TotalAmount, Status, ShippingAddress, Phone)
            VALUES
            (@UserId, NOW(), @Total, 'PENDING', @Address, @Phone);

            SELECT LAST_INSERT_ID();
            ",
                    new { UserId = userId, Total = total, Address = address, Phone = phone },
                    transaction
                );

                // ================= INSERT DETAILS =================
                foreach (var item in cart)
                {
                    conn.Execute(
                        @"
                INSERT INTO orderdetails
                (OrderId, ProductId, Quantity, UnitPrice)
                VALUES
                (@OrderId, @ProductId, @Quantity, @UnitPrice)
                ",
                        new
                        {
                            OrderId = orderId,
                            item.ProductId,
                            item.Quantity,
                            item.UnitPrice
                        },
                        transaction
                    );
                }

                // ================= CLEAR CART =================
                conn.Execute(
                    "DELETE FROM cart WHERE UserId = @UserId",
                    new { UserId = userId },
                    transaction
                );

                transaction.Commit();
                return orderId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        // =========================
        // ORDER LIST (SHOPEE STYLE)
        // =========================
        public async Task<IEnumerable<OrderPreviewDto>> GetOrderPreviewByUser(int userId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var sql = @"
                SELECT
                    o.Id,
                    o.TotalAmount,
                    o.Status,
                    o.OrderDate,

                    GROUP_CONCAT(p.ProductName SEPARATOR ', ') AS ProductNames,
                    GROUP_CONCAT(p.Image SEPARATOR ', ') AS Images,

                    MIN(p.Image) AS Thumbnail, 

                    COUNT(od.ProductId) AS ItemCount

                FROM orders o
                INNER JOIN orderdetails od ON od.OrderId = o.Id
                INNER JOIN products p ON p.Id = od.ProductId

                WHERE o.UserId = @userId

                GROUP BY o.Id, o.TotalAmount, o.Status, o.OrderDate
                ORDER BY o.OrderDate DESC";

            return await conn.QueryAsync<OrderPreviewDto>(sql, new { userId });
        }

        // =========================
        // ORDER DETAIL
        // =========================
        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetail(int orderId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var sql = @"
                SELECT
                    od.OrderId,
                    od.ProductId,
                    p.ProductName,
                    p.Image,
                    od.Quantity,
                    od.UnitPrice AS Price
                FROM orderdetails od
                INNER JOIN products p ON p.Id = od.ProductId
                WHERE od.OrderId = @OrderId";

            return await conn.QueryAsync<OrderDetailDto>(sql, new { OrderId = orderId });
        }

        // =========================
        // UPDATE STATUS
        // =========================
        public async Task UpdateStatus(int orderId, string status)
        {
            using var conn = new MySqlConnection(_connectionString);

            await conn.ExecuteAsync(
                @"
                UPDATE orders
                SET Status = @Status
                WHERE Id = @OrderId",
                new { OrderId = orderId, Status = status }
            );
        }

        // =========================
        // TEMP DTO
        // =========================
        private class CartItemTemp
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}