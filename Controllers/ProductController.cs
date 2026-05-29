using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// API tìm kiếm sản phẩm theo tên
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Tên tìm kiếm không được để trống");

            var result = await _productService.GetProductsByName(name);
            return Ok(result);
        }
    }
}