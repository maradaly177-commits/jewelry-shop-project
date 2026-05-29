using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }


        // 2. BẮT BUỘC: Phải triển khai thêm phương thức này từ IProductService
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _productRepository.GetProductsByName(name);
        }
    }
}