 using Microsoft.EntityFrameworkCore;
using CustomerAssetTracker.Core.Data; // Pro ApplicationDbContext
using CustomerAssetTracker.Core.Abstractions; // Pro IGenericRepository
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace CustomerAssetTracker.Core.Repositories
{
    // Komentář: Obecná implementace repozitáře.
    // Obsahuje logiku pro práci s databází pro jakoukoli entitu.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context; // Odkaz na databázový kontext

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Aplikuje všechny include výrazy na dotaz.
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Aplikuje všechny include výrazy na dotaz.
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Najde entitu podle ID po aplikování includes.
            // Používáme FirstOrDefaultAsync místo FindAsync, protože FindAsync
            // nefunguje s .Include() v paměti (pokud už entita není sledována).
            return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
