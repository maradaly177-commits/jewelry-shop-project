using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Service.Interfaces
{
    /// <summary>
    /// Chức năng: Giao diện định nghĩa các nghiệp vụ cho sản phẩm trang sức.
    /// </summary>
    // Người thực hiện: Marada
    public interface IProductService : IBaseService<Product>
    {
       
        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<IEnumerable<Product>> GetAllProducts(string? category);
    }
}