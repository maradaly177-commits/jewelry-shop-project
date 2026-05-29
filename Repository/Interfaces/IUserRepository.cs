using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức giao tiếp dữ liệu cho thực thể User.
    /// Project: Cosmetic_App
    /// Author: Marada (Student ID: 5001467)
    /// Date: 2026-05-12
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Tìm kiếm người dùng trong hệ thống dựa trên Email.
        /// Sử dụng để kiểm tra đăng nhập hoặc xác thực email duy nhất.
        /// </summary>
        /// <param name="email">Địa chỉ email cần tìm</param>
        /// <returns>Đối tượng User nếu tìm thấy, ngược lại trả về null</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Thêm mới một người dùng vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="user">Thông tin người dùng mới</param>
        /// <returns>True nếu lưu thành công, ngược lại False</returns>
        Task<bool> Register(User user);
    }
} // Dấu đóng ngoặc phải nằm ở đây để bao bọc namespace