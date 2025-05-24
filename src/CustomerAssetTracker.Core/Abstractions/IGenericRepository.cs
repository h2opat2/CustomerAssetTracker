using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CustomerAssetTracker.Core.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        // General CRUD operatons available for any entity of type <T>
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        
        
    }
}
