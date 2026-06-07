using Dapper;
using MySqlConnector;
using System.Data;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// BaseRepository dùng chung cho tất cả entity
    /// Thực hiện CRUD + phân trang
    /// Người thực hiện: Marada
    /// </summary>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly string _connectionString;

        /// <summary>
        /// Tên bảng (mặc định: tên class + s)
        /// </summary>
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name.ToLower() + "s";

        /// <summary>
        /// Tên cột khóa chính
        /// </summary>
        public virtual string TableId { get; protected set; } = "Id";

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost;Port=3306;Database=jewelryshopdb;Uid=root;Pwd=;";
        }

        /// <summary>
        /// Tạo connection mới (chuẩn async)
        /// </summary>
        protected IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // ================= GET ALL =================
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using var connection = GetConnection();

            var sql = $"SELECT * FROM `{TableName}`;";
            return await connection.QueryAsync<TEntity>(sql);
        }

        // ================= GET BY ID =================
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            using var connection = GetConnection();

            var sql = $"SELECT * FROM `{TableName}` WHERE `{TableId}` = @id;";
            return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { id });
        }

        // ================= INSERT =================
        public async Task<int> InsertAsync(TEntity entity)
        {
            using var connection = GetConnection();

            var properties = typeof(TEntity).GetProperties();

            var columnNames = string.Join(", ", properties.Select(p => $"`{p.Name}`"));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO `{TableName}` ({columnNames}) VALUES ({paramNames});";

            return await connection.ExecuteAsync(sql, entity);
        }

        // ================= UPDATE =================
        public async Task<int> UpdateAsync(int id, TEntity entity)
        {
            using var connection = GetConnection();

            var properties = typeof(TEntity).GetProperties();

            var setClause = string.Join(", ",
                properties
                .Where(p => p.Name != TableId)
                .Select(p => $"`{p.Name}` = @{p.Name}")
            );

            var sql = $"UPDATE `{TableName}` SET {setClause} WHERE `{TableId}` = @Id;";

            var parameters = new DynamicParameters(entity);
            parameters.Add("Id", id);

            return await connection.ExecuteAsync(sql, parameters);
        }

        // ================= DELETE =================
        public async Task<int> DeleteAsync(int id)
        {
            using var connection = GetConnection();

            var sql = $"DELETE FROM `{TableName}` WHERE `{TableId}` = @id;";
            return await connection.ExecuteAsync(sql, new { id });
        }

        // ================= PAGING + SEARCH =================
        /// <summary>
        /// Lấy danh sách có phân trang + tìm kiếm theo Name
        /// </summary>
        public async Task<(IEnumerable<TEntity> Data, int TotalCount)> GetPagedAsync(int page, int pageSize, string search)
        {
            using var connection = GetConnection();

            var offset = (page - 1) * pageSize;

            var sql = $@"
        SELECT * FROM `{TableName}`
        LIMIT @pageSize OFFSET @offset;

        SELECT COUNT(*) FROM `{TableName}`;
    ";

            using var multi = await connection.QueryMultipleAsync(sql, new
            {
                pageSize,
                offset
            });

            var data = await multi.ReadAsync<TEntity>();
            var total = await multi.ReadFirstAsync<int>();

            return (data, total);
        }
    }
}