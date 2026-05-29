using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_App.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
            // Cấu hình tên bảng và khóa chính khớp 100% với trong MySQL
            // Hãy thay đổi "categories" và "CategoryId" nếu trong DB của bạn tên khác
            TableName = "categories";
            TableId = "CategoryId";
        }

        // Hiện tại không cần ghi đè (override) các hàm CRUD vì BaseRepository đã chuẩn Guid
    }
}