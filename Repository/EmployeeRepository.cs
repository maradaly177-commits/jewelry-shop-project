using Cosmetic_App.Common.Entity;
using Cosmetic_App.Repository.Interfaces;
using Microsoft.Extensions.Configuration; // Phải thêm thư viện này

namespace Cosmetic_App.Repository
{
    /// <summary>
    /// Lớp thực thi các thao tác cơ sở dữ liệu cho thực thể Employee.
    /// Người thực hiện: Marada (MSSV: 5001467)
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Hàm khởi tạo nhận configuration và truyền xuống lớp cha BaseRepository.
        /// </summary>
        /// <param name="configuration">Cấu hình từ hệ thống</param>
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            // Bây giờ base(configuration) đã có đủ tham số nên sẽ hết lỗi
        }
    }
}