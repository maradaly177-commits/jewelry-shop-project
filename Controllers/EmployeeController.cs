using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_App.Controllers
{
    /// Người thực hiện: Marada
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        // Sửa lỗi: Thay IEmployeeService bằng EmployeeService
        public EmployeeController(EmployeeService employeeService) : base(employeeService)
        {
        }
    }
}