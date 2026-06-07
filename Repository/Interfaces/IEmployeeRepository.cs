using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    // Quan trọng nhất: Phải kế thừa IBaseRepository<Employee>
    // Người thực hiện: Marada
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        // Các hàm riêng của Employee nếu có...
    }
}