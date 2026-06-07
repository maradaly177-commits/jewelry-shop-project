using Microsoft.AspNetCore.Mvc;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Common;
using Cosmetic_App.Service.Interfaces;
using System.Text.RegularExpressions;

namespace Cosmetic_App.Controllers
{
    /// <summary>
    /// Controller xử lý đơn hàng
    /// Người thực hiện: Marada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            // ================= VALIDATE REQUEST =================
            if (req == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Request không hợp lệ"
                });
            }

            if (req.UserId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "UserId không hợp lệ"
                });
            }

            if (string.IsNullOrWhiteSpace(req.ShippingAddress))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Vui lòng nhập địa chỉ"
                });
            }

            if (req.ShippingAddress.Trim().Length < 10)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Địa chỉ quá ngắn (>= 10 ký tự)"
                });
            }

            if (string.IsNullOrWhiteSpace(req.Phone))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Vui lòng nhập số điện thoại"
                });
            }

            if (!IsValidPhone(req.Phone))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Số điện thoại không đúng định dạng"
                });
            }

            try
            {
                // ================= CALL SERVICE =================
                var orderId = await _orderService.Checkout(
                    req.UserId,
                    req.ShippingAddress.Trim(),
                    req.Phone.Trim()
                );

                // ================= SUCCESS RESPONSE =================
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Đặt hàng thành công",
                    Data = new { orderId }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // =========================
        // GET ORDERS BY USER
        // =========================
        /// Người thực hiện: Marada
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var data = await _orderService.GetOrderPreviewByUser(userId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data
            });
        }

        // =========================
        // ORDER DETAIL
        // =========================
        /// Người thực hiện: Vortey
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var data = await _orderService.GetOrderDetail(orderId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data
            });
        }

        // =========================
        // UPDATE STATUS
        // =========================
        /// Người thực hiện: Marada
        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromQuery] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Status không hợp lệ"
                });
            }

            await _orderService.UpdateStatus(orderId, status);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cập nhật trạng thái thành công"
            });
        }

        // =========================
        // PHONE VALIDATION
        // =========================
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, @"^(03|05|07|08|09)\d{8}$");
        }
    }
}