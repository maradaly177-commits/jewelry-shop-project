using Microsoft.AspNetCore.Mvc;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Common;
using Cosmetic_App.Repository.Interfaces;

namespace Cosmetic_App.Controllers
{
    /// <summary>
    /// Controller quản lý giỏ hàng
    /// Người thực hiện: Marada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        // =========================
        // ADD TO CART
        // =========================
        /// Người thực hiện: Vortey
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            await _cartRepo.AddToCart(request.UserId, request.ProductId, request.Quantity);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Thêm vào giỏ hàng thành công"
            });
        }

        // =========================
        // GET CART
        // =========================
        /// Người thực hiện: Vortey
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var data = await _cartRepo.GetCart(userId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data ?? new List<CartItemDto>() // ✅ FIX NULL
            });
        }
        // =========================
        // INCREASE
        // =========================
        /// Người thực hiện: Vortey
        [HttpPut("increase/{productId}")]
        public async Task<IActionResult> Increase(int productId, [FromQuery] int userId)
        {
            await _cartRepo.Increase(userId, productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OK"
            });
        }

        // =========================
        // DECREASE
        // =========================
        /// Người thực hiện: Vortey
        [HttpPut("decrease/{productId}")]
        public async Task<IActionResult> Decrease(int productId, [FromQuery] int userId)
        {
            await _cartRepo.Decrease(userId, productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OK"
            });
        }

        // =========================
        // REMOVE
        // =========================
        /// Người thực hiện: Vortey
        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> Remove(int productId, [FromQuery] int userId)
        {
            await _cartRepo.Remove(userId, productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OK"
            });
        }
    }
}