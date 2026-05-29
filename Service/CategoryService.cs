using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    /// <summary>
    /// Chức năng: Triển khai các nghiệp vụ logic cho Danh mục sản phẩm (Category).
    /// Dự án: Jewelry Shop
    /// Người thực hiện: Marada (MSSV: 5001467) | Ngày tạo: 12/05/2026
    /// </summary>
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        /// <summary>
        /// Khởi tạo CategoryService.
        /// </summary>
        /// <param name="categoryRepository">Tiêm (Inject) Repository của Category để làm việc với Database</param>
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            // BaseService sẽ quản lý việc gọi các hàm CRUD cơ bản thông qua categoryRepository
        }

        // Lưu ý: Các hàm như GetAllAsync, GetByIdAsync, InsertAsync, v.v. 
        // đã được kế thừa từ BaseService<Category> nên không cần viết lại.
        // Bạn chỉ viết thêm các hàm đặc thù nếu có yêu cầu riêng từ thầy.
    }
}