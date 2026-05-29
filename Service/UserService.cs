using BCrypt.Net;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        // --- PHẦN LOGIN ---
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _userRepository.GetUserByEmailAsync(email.Trim());

            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
                return null;

            try
            {
                // Kiểm tra BCrypt chuẩn nttrung: Chỗ này chưa đúng mã hóa(Sử dụng AI để phân tích cách mã hóa mật khẩu lúc đăng
                // nhập và đăng ký)
                bool isBcryptHash = user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$");

                if (isBcryptHash)
                {
                    return BCrypt.Net.BCrypt.Verify(password.Trim(), user.PasswordHash) ? user : null;
                }
                // Fallback cho dữ liệu cũ (plaintext)
                // so sánh password khi đăng nhập
                return user.PasswordHash.Trim() == password.Trim() ? user : null;
            }
            catch
            {
                return null;
            }
        }

        // --- PHẦN REGISTER ---
        public async Task<bool> RegisterAsync(User user)
        {
            // 1. Validate dữ liệu chặt chẽ
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash))
                return false;

            // 2. Chuẩn hóa Email
            user.Email = user.Email.Trim().ToLower();

            // 3. Kiểm tra tồn tại
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                return false; // Email đã được sử dụng

            // 4. Mã hóa mật khẩu (Luôn luôn mã hóa trước khi lưu)
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash.Trim());

            // 5. Metadata
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            user.CreatedBy = "System";

            // 6. Lưu vào DB
            return await _userRepository.Register(user);
        }
    }
}