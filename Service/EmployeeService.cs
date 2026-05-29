// Sửa lại cho đúng đường dẫn thư mục mới tạo
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;
using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
        }
    }
}