    using CustomerAssetTracker.Core.Data; // Pro ApplicationDbContext
    using CustomerAssetTracker.Core.Interfaces; // Pro IUnitOfWork a IGenericRepository
    using System.Threading.Tasks;

    namespace CustomerAssetTracker.Core.Repositories
    {
        // Komentář: Implementace Unit of Work.
        // Spravuje instance repozitářů a zajišťuje uložení změn přes jeden DbContext.
        public class UnitOfWork : IUnitOfWork
        {
            private readonly ApplicationDbContext _context;

            // Komentář: Privátní backing fields pro repozitáře.
            private IGenericRepository<Customer>? _customers;
            private IGenericRepository<Machine>? _machines;
            private IGenericRepository<License>? _licenses;
            private IGenericRepository<ServiceRecord>? _serviceRecords;

            public UnitOfWork(ApplicationDbContext context)
            {
                _context = context;
            }

            // Komentář: Vlastnosti pro líné načítání repozitářů.
            // Repozitáře se vytvoří pouze při prvním přístupu.
            public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);
            public IGenericRepository<Machine> Machines => _machines ??= new GenericRepository<Machine>(_context);
            public IGenericRepository<License> Licenses => _licenses ??= new GenericRepository<License>(_context);
            public IGenericRepository<ServiceRecord> ServiceRecords => _serviceRecords ??= new GenericRepository<ServiceRecord>(_context);

            // Komentář: Uloží všechny sledované změny v DbContextu do databáze.
            public async Task<int> CompleteAsync()
            {
                return await _context.SaveChangesAsync();
            }

            // Komentář: Metoda Dispose pro uvolnění DbContextu.
            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
    