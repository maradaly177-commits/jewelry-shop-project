using Cosmetic_App.Common;
using Cosmetic_App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BaseController<TEntity> : ControllerBase where TEntity : class
{
    protected readonly IBaseService<TEntity> _service;

    public BaseController(IBaseService<TEntity> service)
    {
        _service = service;
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "ID không hợp lệ",
                Data = null
            });
        }

        var data = await _service.GetByIdAsync(id);

        if (data == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Không tìm thấy",
                Data = null
            });
        }

        return Ok(new ApiResponse<TEntity>
        {
            Success = true,
            Message = "Lấy dữ liệu thành công",
            Data = data
        });
    }

    // GET ALL + PAGING
    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 1000, string search = "")
    {
        var result = await _service.GetPagedAsync(page, pageSize, search);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Lấy danh sách thành công",
            Data = new
            {
                Data = result.Data,
                TotalCount = result.TotalCount
            }
        });
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        var data = await _service.InsertAsync(entity);

        return Ok(new ApiResponse<TEntity>
        {
            Success = true,
            Message = "Thêm thành công",
            Data = data
        });
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
    {
        var data = await _service.UpdateAsync(id, entity);

        return Ok(new ApiResponse<TEntity>
        {
            Success = true,
            Message = "Cập nhật thành công",
            Data = data
        });
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);

        return Ok(new ApiResponse<bool>
        {
            Success = success,
            Message = success ? "Xóa thành công" : "Xóa thất bại",
            Data = success
        });
    }
}