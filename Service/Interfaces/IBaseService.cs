using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmetic_App.Service.Interfaces
{
    /// <summary>
    /// Interface tầng Service (business logic)
    /// Dùng chung cho tất cả entity
    /// Thực hiện CRUD + phân trang
    /// Người thực hiện: Marada
    /// </summary>
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Lấy dữ liệu theo ID
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

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

        /// <summary>
        /// Thêm mới dữ liệu
        /// Trả về entity vừa thêm
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật dữ liệu
        /// Trả về entity sau khi update
        /// </summary>
        Task<TEntity> UpdateAsync(int id, TEntity entity);

        /// <summary>
        /// Xóa dữ liệu
        /// Trả về true nếu thành công
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}