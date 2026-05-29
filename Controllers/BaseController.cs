using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        protected readonly IBaseService<TEntity> _baseService;

        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        // 🔥 SỬA Guid -> int
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _baseService.DeleteAsync(id);
            return Ok(result);
        }

        // 🔥 SỬA Guid -> int
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            if (id <= 0) return BadRequest("ID không hợp lệ");

            var result = await _baseService.GetByIdAsync(id);

            if (result == null)
                return NotFound("Không tìm thấy dữ liệu");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _baseService.GetAllAsync();
            return Ok(data);
        }
    }
}