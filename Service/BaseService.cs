using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    /// <summary>
    /// BaseService xử lý logic nghiệp vụ chung cho tất cả entity
    /// Thực hiện CRUD + phân trang
    /// Người thực hiện: Marada
    /// </summary>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        /// <summary>
        /// Lấy dữ liệu theo ID
        /// </summary>
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Lấy danh sách có phân trang + tìm kiếm
        /// </summary>
        public virtual async Task<(IEnumerable<TEntity> Data, int TotalCount)> GetPagedAsync(int page, int pageSize, string search)
        {
            return await _baseRepository.GetPagedAsync(page, pageSize, search);
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// Trả về entity vừa thêm
        /// </summary>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _baseRepository.InsertAsync(entity);
            return entity;
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// Trả về entity sau khi update
        /// </summary>
        public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            await _baseRepository.UpdateAsync(id, entity);
            return entity;
        }

        /// <summary>
        /// Xóa dữ liệu
        /// Trả về true nếu thành công
        /// </summary>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var result = await _baseRepository.DeleteAsync(id);
            return result > 0;
        }
    }
}