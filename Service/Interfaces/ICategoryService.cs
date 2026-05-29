using Cosmetic_App.Common.Entity;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service.Interfaces
{
    /// <summary>
    /// Chức năng: Giao diện định nghĩa các nghiệp vụ logic cho Danh mục (Category)
    /// Dự án: Jewelry Shop
    /// Người thực hiện: Marada (MSSV: 5001467) | Ngày tạo: 12/05/2026
    /// </summary>
    public interface ICategoryService : IBaseService<Category>
    {
        // Các phương thức cơ bản (GetAll, GetById, Insert, Update, Delete) 
        // đã được kế thừa từ IBaseService<Category>.

        // Bạn có thể thêm các nghiệp vụ đặc thù cho trang sức tại đây nếu cần, ví dụ:
        // Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    }
}