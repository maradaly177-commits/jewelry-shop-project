using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmetic_App.Repository.Interfaces
{
    /// <summary>
    /// Interface dùng chung cho tất cả Repository
    /// Thực hiện các thao tác CRUD + phân trang
    /// Người thực hiện: Marada
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy dữ liệu theo ID
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Thêm mới dữ liệu
        /// Trả về số dòng bị ảnh hưởng
        /// </summary>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật dữ liệu theo ID
        /// </summary>
        Task<int> UpdateAsync(int id, TEntity entity);

        /// <summary>
        /// Xóa dữ liệu theo ID
        /// </summary>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Lấy danh sách có phân trang + tìm kiếm
        /// </summary>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>
        /// Data: danh sách dữ liệu
        /// TotalCount: tổng số bản ghi
        /// </returns>
        Task<(IEnumerable<TEntity> Data, int TotalCount)> GetPagedAsync(int page, int pageSize, string search);
    }
}