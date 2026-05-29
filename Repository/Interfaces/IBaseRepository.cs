using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmetic_App.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        // 🔥 SỬA GUID -> INT
        Task<TEntity?> GetByIdAsync(int id);

        Task<int> InsertAsync(TEntity entity);

        // 🔥 SỬA GUID -> INT
        Task<int> UpdateAsync(int id, TEntity entity);

        // 🔥 SỬA GUID -> INT
        Task<int> DeleteAsync(int id);
    }
}