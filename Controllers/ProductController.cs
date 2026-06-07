using Cosmetic_App.Common;
using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    /// <summary>
    /// Controller quản lý sản phẩm
    /// Người thực hiện: Marada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
            : base(productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// API tìm kiếm sản phẩm theo tên
        /// </summary>
        /// Người thực hiện: Marada
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Tên tìm kiếm không được để trống"
                });
            }

            var result = await _productService.GetProductsByName(name);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result
            });
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetByCategory([FromQuery] string? category)
        {
            var result = await _productService.GetAllProducts(category);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = result
            });
        }

    }
}