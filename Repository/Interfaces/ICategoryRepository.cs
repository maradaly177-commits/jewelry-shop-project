using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Repository xử lý dữ liệu cho Category (Danh mục sản phẩm)
    /// Kế thừa các chức năng CRUD từ IBaseRepository
    /// Người thực hiện: Marada
    /// </summary>
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // Hiện tại đã có sẵn:
        // - GetAllAsync
        // - GetByIdAsync
        // - InsertAsync
        // - UpdateAsync
        // - DeleteAsync

        // Có thể mở rộng:
        // Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync();
    }
}