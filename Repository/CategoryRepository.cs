using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// Repository xử lý Category (Danh mục sản phẩm)
    /// Người thực hiện: Marada
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
            TableName = "categories";
            TableId = "Id"; // nên dùng Id int để đồng bộ hệ thống
        }
    }
}