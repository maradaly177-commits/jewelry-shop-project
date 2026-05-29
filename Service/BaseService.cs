using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service.Interfaces;

namespace Cosmetic_App.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        // 🔥 SỬA Guid -> int
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            return await _baseRepository.InsertAsync(entity);
        }

        // 🔥 SỬA Guid -> int
        public virtual async Task<int> UpdateAsync(int id, TEntity entity)
        {
            return await _baseRepository.UpdateAsync(id, entity);
        }

        // 🔥 SỬA Guid -> int
        public virtual async Task<int> DeleteAsync(int id)
        {
            return await _baseRepository.DeleteAsync(id);
        }
    }
}