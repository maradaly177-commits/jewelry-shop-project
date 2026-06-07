using Dapper;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    // Người thực hiện: Marada
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            const string sql = @"
                SELECT 
                    Id,
                    Email,
                    PasswordHash,
                    FullName,
                    Role 
                FROM users
                WHERE Email = @Email
                LIMIT 1;
            ";

            using var conn = GetConnection();

            return await conn.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Email = email?.Trim() }
            );
        }

        public async Task<int> Register(User user)
        {
            return await InsertAsync(user);
        }
    }
}