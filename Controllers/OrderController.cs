using Microsoft.AspNetCore.Mvc;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Service.Interfaces;
using System.Text.RegularExpressions;

namespace Cosmetic_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // =========================
        // CHECKOUT
        // =========================
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            if (req.UserId <= 0)
                return BadRequest(new { message = "User không hợp lệ" });

            if (string.IsNullOrWhiteSpace(req.ShippingAddress) || req.ShippingAddress.Length < 10)
                return BadRequest(new { message = "Địa chỉ giao hàng không hợp lệ" });

            if (!IsValidPhone(req.Phone))
                return BadRequest(new { message = "Số điện thoại không hợp lệ" });

            var orderId = await _orderService.Checkout(
                req.UserId,
                req.ShippingAddress,
                req.Phone
            );

            return Ok(new
            {
                message = "Đặt hàng thành công",
                orderId
            });
        }

        // =========================
        // ORDER LIST (SHOPEE: ĐƠN MUA)
        // =========================
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var data = await _orderService.GetOrdersByUser(userId);
            return Ok(data);
        }

        // =========================
        // ORDER DETAIL
        // =========================
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var data = await _orderService.GetOrderDetail(orderId);
            return Ok(data);
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