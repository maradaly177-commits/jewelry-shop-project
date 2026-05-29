using Microsoft.AspNetCore.Mvc;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Repository.Interfaces;

namespace Cosmetic_App.Controllers
{
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
        // 🔥 1. ADD TO CART
        // =========================
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            if (request == null || request.UserId <= 0 || request.ProductId <= 0)
                return BadRequest("Invalid data");

            await _cartRepo.AddToCart(request.UserId, request.ProductId, request.Quantity);

            return Ok(new
            {
                message = "Thêm vào giỏ thành công"
            });
        }

        // =========================
        // 🔥 2. GET CART
        // =========================
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var data = await _cartRepo.GetCart(userId);
            return Ok(data);
        }

        // =========================
        // 🔥 3. INCREASE
        // =========================
        [HttpPut("increase/{productId}")]
        public async Task<IActionResult> Increase(int productId)
        {
            int userId = 1; // ⚠️ sau này lấy từ JWT

            await _cartRepo.Increase(userId, productId);

            return Ok(new
            {
                message = "Tăng số lượng thành công"
            });
        }

        // =========================
        // 🔥 4. DECREASE
        // =========================
        [HttpPut("decrease/{productId}")]
        public async Task<IActionResult> Decrease(int productId)
        {
            int userId = 1;

            await _cartRepo.Decrease(userId, productId);

            return Ok(new
            {
                message = "Giảm số lượng thành công"
            });
        }

        // =========================
        // 🔥 5. REMOVE
        // =========================
        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            int userId = 1;

            await _cartRepo.Remove(userId, productId);

            return Ok(new
            {
                message = "Xóa sản phẩm thành công"
            });
        }
    }
}