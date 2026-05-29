using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Giao diện Repository cho Category (Danh mục).
    /// </summary>
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // Hiện tại các hàm CRUD cơ bản đã thừa kế từ IBaseRepository<Category>.
        // Nếu sau này bạn cần các hàm như: Lấy danh mục kèm số lượng sản phẩm, 
        // hoặc lọc danh mục theo trạng thái, bạn chỉ cần thêm vào đây.

        // Ví dụ:
        // Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync();
    }
}