using Cosmetic_App.Common;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    /// <summary>
    /// Controller quản lý user + auth
    /// Người thực hiện: Marada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        // =========================
        // LOGIN
        // =========================
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            var email = request.Email.Trim();

            var user = await _userService.LoginAsync(email, request.Password);

            if (user == null)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Sai tài khoản hoặc mật khẩu"
                });
            }

            // TODO: replace bằng JWT thật
            var fakeToken = $"JWT_{user.Role?.ToUpper()}_{user.Id}";

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = new
                {
                    Token = fakeToken,
                    User = new
                    {
                        user.Id,
                        user.Email,
                        user.FullName,
                        user.Role
                    }
                }
            });
        }
    }
}