using Dapper;
using MySqlConnector;
using System.Data;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly string _connectionString;

        // Tên bảng và cột (có thể override ở class con)
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name.ToLower() + "s";
        public virtual string TableId { get; protected set; } = "Id";

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost;Port=3306;Database=jewelryshopdb;Uid=root;Pwd=;";
        }

        // Tạo connection mới mỗi lần gọi (chuẩn async)
        protected IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // ================= GET ALL =================
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using var conn = GetConnection();
            var sql = $"SELECT * FROM `{TableName}`;";
            return await conn.QueryAsync<TEntity>(sql);
        }

        // ================= GET BY ID =================
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            using var conn = GetConnection();
            var sql = $"SELECT * FROM `{TableName}` WHERE `{TableId}` = @id;";
            return await conn.QueryFirstOrDefaultAsync<TEntity>(sql, new { id });
        }

        // ================= INSERT =================
        public async Task<int> InsertAsync(TEntity entity)
        {
            using var conn = GetConnection();

            var properties = typeof(TEntity).GetProperties();

            var columnNames = string.Join(", ", properties.Select(p => $"`{p.Name}`"));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO `{TableName}` ({columnNames}) VALUES ({paramNames});";

            return await conn.ExecuteAsync(sql, entity);
        }

        // ================= UPDATE =================
        public async Task<int> UpdateAsync(int id, TEntity entity)
        {
            using var conn = GetConnection();

            var properties = typeof(TEntity).GetProperties();

            var setClause = string.Join(", ",
                properties
                .Where(p => p.Name != TableId)
                .Select(p => $"`{p.Name}` = @{p.Name}")
            );

            var sql = $"UPDATE `{TableName}` SET {setClause} WHERE `{TableId}` = @Id;";

            var parameters = new DynamicParameters(entity);
            parameters.Add("Id", id);

            return await conn.ExecuteAsync(sql, parameters);
        }

        // ================= DELETE =================
        public async Task<int> DeleteAsync(int id)
        {
            using var conn = GetConnection();
            var sql = $"DELETE FROM `{TableName}` WHERE `{TableId}` = @id;";
            return await conn.ExecuteAsync(sql, new { id });
        }
    }
}