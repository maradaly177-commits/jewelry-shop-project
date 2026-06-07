using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    // Người thực hiện: Marada
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
            : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _productRepository.GetProductsByName(name);
        }
        public async Task<IEnumerable<Product>> GetAllProducts(string? category)
        {
            return await _productRepository.GetAllProducts(category);
        }
    }
}