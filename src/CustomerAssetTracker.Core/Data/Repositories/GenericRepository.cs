 using Microsoft.EntityFrameworkCore;
    using CustomerAssetTracker.Core.Data; // Pro ApplicationDbContext
    using CustomerAssetTracker.Core.Abstractions; // Pro IGenericRepository
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
