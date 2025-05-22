using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAssetTracker.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // General CRUD operatons available for any entity of type <T>
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
