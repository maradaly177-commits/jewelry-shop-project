using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmetic_App.Service.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        // 🔥 SỬA Guid -> int + thêm ?
        Task<TEntity?> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        // 🔥 SỬA Guid -> int
        Task<int> DeleteAsync(int id);

        // 🔥 SỬA Guid -> int
        Task<int> UpdateAsync(int id, TEntity entity);

        Task<int> InsertAsync(TEntity entity);
    }
}