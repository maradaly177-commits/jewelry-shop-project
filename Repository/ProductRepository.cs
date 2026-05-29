using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Cosmetic_App.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
            TableName = "products";
            TableId = "Id";
        }

        // ❌ KHÔNG override nữa
        //public async Task<Product?> GetByIdAsync(int id)
        //{
        //    using var conn = GetConnection();
        //    string sql = $"SELECT * FROM `{TableName}` WHERE `{TableId}` = @id";

        //    return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { id });
        //}

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            using var conn = GetConnection();

            string sql = $"SELECT * FROM `{TableName}` WHERE `ProductName` LIKE @Name";

            return await conn.QueryAsync<Product>(sql, new { Name = $"%{name}%" });
        }
    }
}