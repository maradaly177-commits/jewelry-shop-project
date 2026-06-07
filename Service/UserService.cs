using BCrypt.Net;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    /// <summary>
    /// Service xử lý nghiệp vụ liên quan đến User
    /// Bao gồm: Login, Register, quản lý thông tin người dùng
    /// Người thực hiện: Marada
    /// </summary>
    public class UserService : BaseService<User>, IUserService
    {
        /// <summary>
        /// Repository xử lý truy vấn User
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor inject repository
        /// </summary>
        public UserService(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        // =========================
        // LOGIN
        // =========================
        /// <summary>
        /// Xử lý đăng nhập người dùng
        /// Kiểm tra email + mật khẩu (BCrypt)
        /// </summary>
        public async Task<User?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _userRepository.GetUserByEmailAsync(email.Trim());

            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
                return null;

            try
            {
                var inputPassword = password.Trim();
                var storedPassword = user.PasswordHash;

                // Nếu là BCrypt
                if (storedPassword.StartsWith("$2a$") || storedPassword.StartsWith("$2b$"))
                {
                    return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword)
                        ? user
                        : null;
                }

                // ✔ fallback plaintext (để user cũ login được)
                return storedPassword.Trim() == inputPassword ? user : null;
            }
            catch
            {
                return null;
            }
        }

        // =========================
        // REGISTER
        // =========================
        /// <summary>
        /// Đăng ký tài khoản mới
        /// Bao gồm: validate + hash password + lưu DB
        /// </summary>
        public async Task<bool> RegisterAsync(User user)
        {
            if (user == null ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.PasswordHash))
                return false;

            // Chuẩn hóa email
            user.Email = user.Email.Trim().ToLower();

            // Kiểm tra email tồn tại
            var existingUser =
                await _userRepository.GetUserByEmailAsync(user.Email);

            if (existingUser != null)
                return false;

            // Hash mật khẩu bằng BCrypt
            user.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(user.PasswordHash.Trim());

            // Metadata
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            user.CreatedBy = "System";

            // Lưu DB
            return await _userRepository.Register(user) > 0;
        }
    }
}