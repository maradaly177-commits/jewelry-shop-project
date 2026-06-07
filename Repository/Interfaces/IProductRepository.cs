using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Repository xử lý dữ liệu Product
    /// Kế thừa IBaseRepository để dùng CRUD cơ bản
    /// Người thực hiện: Marada
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// Tìm kiếm sản phẩm theo tên
        /// </summary>
        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<IEnumerable<Product>> GetAllProducts(string? category);
    }
}