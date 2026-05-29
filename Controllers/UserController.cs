using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
        // cần viết model vào 1 file riêng không đặt ở controller
        #region Requests Models
        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class RegisterRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
        }
        #endregion
        /// <summary>
        /// chức năng đang nhập
        /// Created by Ly MARADA
        /// Created Date: xxx
        /// Modified 24/5/26  Ly bổ sung jwt và mã hõa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Trim email để tránh lỗi dư khoảng trắng khi người dùng nhập
            var email = request.Email?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Dữ liệu gửi lên không đầy đủ" });
            }

            var user = await _userService.LoginAsync(email, request.Password);

            if (user == null)
            {
                // Trả về 401 nếu sai pass hoặc không thấy email
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });
            }

            // SỬA TẠI ĐÂY: Phát sinh token và trả về object tùy biến thay vì return Ok(user);
            // Chuỗi token này giả lập (hoặc bạn gọi từ TokenService nếu có triển khai JWT)
           // Modified 24 / 5 / 26  Ly bổ sung jwt và mã hõa
            var fakeToken = $"JWT_TOKEN_SUCCESS_ROLE_{user.Role?.ToUpper()}_{user.Id}";

            return Ok(new
            {
                message = "Đăng nhập thành công!",
                token = fakeToken,
                data = new
                {
                    user.Id,
                    user.Email,
                    user.FullName,
                    user.Role
                }
            });
        }

    }
}