using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// Repository xử lý Product
    /// Người thực hiện: Marada
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
            TableName = "products";
            TableId = "Id";
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo tên
        /// </summary>
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            using var conn = GetConnection();

            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<Product>();

            string sql = $@"
        SELECT * 
        FROM `{TableName}` 
        WHERE COALESCE(`ProductName`, '') LIKE @Name
        ORDER BY `Id` ASC
    ";

            return await conn.QueryAsync<Product>(
                sql,
                new { Name = $"%{name.Trim()}%" }
            );
        }
        //Url search category=jewelry
        public async Task<IEnumerable<Product>> GetAllProducts(string? category)
        {
            using var conn = GetConnection();

            var sql = "SELECT * FROM products WHERE 1=1";

            if (!string.IsNullOrEmpty(category))
            {
                var categoryKey = category.Trim().ToLower(); // ⭐ FIX QUAN TRỌNG

                if (categoryKey == "watch")
                {
                    sql += " AND CategoryId = 1";
                }
                else if (categoryKey == "jewelry")
                {
                    sql += " AND CategoryId = 2";
                }
            }

            return await conn.QueryAsync<Product>(sql);
        }
    }
}