using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức giao tiếp dữ liệu cho User
    /// Project: Cosmetic_App
    /// Người thực hiện: Marada (Student ID: 5001467)
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Tìm user theo email (dùng cho login / validate)
        /// </summary>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Đăng ký người dùng mới
        /// Trả về số dòng bị ảnh hưởng (theo chuẩn BaseRepository)
        /// </summary>
        Task<int> Register(User user);
    }
}