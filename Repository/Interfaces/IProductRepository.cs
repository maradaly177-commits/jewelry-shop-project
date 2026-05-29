using Cosmetic_App.Common.Entity;

namespace Cosmetic_App.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByName(string name);
    }
}