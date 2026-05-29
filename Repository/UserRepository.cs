using Dapper;
using System.Data;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// Repository quản lý dữ liệu người dùng.
    /// Dự án: Cosmetic_App
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Lấy thông tin người dùng dựa trên Email để phục vụ đăng nhập.
        /// </summary>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            // Tên cột phải khớp chính xác với PasswordHash trong User.cs
            const string sql = @"SELECT 
                                    `Id`, 
                                    `Email`, 
                                    `PasswordHash`, 
                                    `FullName`, 
                                    `Role` 
                                 FROM `users` 
                                 WHERE `Email` = @Email 
                                 LIMIT 1;";

            // Thay thế đoạn try-catch cũ bằng cách sử dụng GetConnection()
            try
            {
                using var conn = GetConnection(); // Tạo kết nối mới mỗi khi cần
                return await conn.QueryFirstOrDefaultAsync<User>(
                    sql,
                    new { Email = email.Trim() }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] UserRepository.GetUserByEmailAsync: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Đăng ký người dùng mới.
        /// </summary>
        public async Task<bool> Register(User user)
        {
            // Sử dụng hàm InsertAsync từ BaseRepository
            var result = await InsertAsync(user);
            return result > 0;
        }
    }
}