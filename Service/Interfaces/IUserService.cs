using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Service.Interfaces
{
    /// <summary>
    /// Chức năng: Giao diện nghiệp vụ cho người dùng
    /// Người tạo: Ly Marada | Ngày tạo: 12/05/2026
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        // Khai báo đúng tên hàm có đuôi Async
        Task<User?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(User user);
    }
}