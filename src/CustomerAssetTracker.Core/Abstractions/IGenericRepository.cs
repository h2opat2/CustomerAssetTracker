using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CustomerAssetTracker.Core.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        // General CRUD operatons available for any entity of type <T>
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        // Komentář: Nová metoda pro získání entity podle ID s možností eager loading souvisejících entit.
        // 'includes' je pole výrazů, které specifikují, které navigační vlastnosti se mají načíst.
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        // Komentář: Nová metoda pro získání všech entit s možností eager loading souvisejících entit.
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    }
}
