using Microsoft.AspNetCore.Mvc;
using Cosmetic_App.Common.DTO;
using Cosmetic_App.Common;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Controllers
{
    /// <summary>
    /// Controller xử lý đánh giá sản phẩm
    /// Người thực hiện: Vortey
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        // =========================
        // CREATE REVIEW
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            if (dto == null || dto.ProductId <= 0 || dto.UserId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            await _service.AddReview(dto);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Thêm đánh giá thành công"
            });
        }

        // =========================
        // GET BY PRODUCT
        // =========================
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "ProductId không hợp lệ"
                });
            }

            var data = await _service.GetReviewsByProduct(productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = data
            });
        }

        // =========================
        // AVERAGE RATING
        // =========================
        [HttpGet("average/{productId}")]
        public async Task<IActionResult> GetAverage(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "ProductId không hợp lệ"
                });
            }

            var avg = await _service.GetAverageRating(productId);
            var count = await _service.GetReviewCount(productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new
                {
                    Average = avg,
                    Count = count
                }
            });
        }

        // =========================
        // CAN REVIEW
        // =========================
        [HttpGet("can-review")]
        public async Task<IActionResult> CanReview(int userId, int productId)
        {
            if (userId <= 0 || productId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu không hợp lệ"
                });
            }

            var result = await _service.CanReview(userId, productId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result
            });
        }
    }
}