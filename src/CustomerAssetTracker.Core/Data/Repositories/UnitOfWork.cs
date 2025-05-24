    using CustomerAssetTracker.Core.Data;
    using CustomerAssetTracker.Core.Abstractions;

    namespace CustomerAssetTracker.Core.Repositories
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly ApplicationDbContext _context;
            private IGenericRepository<Customer>? _customers;
            private IGenericRepository<Machine>? _machines;
            private IGenericRepository<License>? _licenses;
            private IGenericRepository<ServiceRecord>? _serviceRecords;

            public UnitOfWork(ApplicationDbContext context)
            {
                _context = context;
            }

            // Lazy initialization of repositories.
            // Repozitories are initialized only when they are accessed for the first time.
            public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);
            public IGenericRepository<Machine> Machines => _machines ??= new GenericRepository<Machine>(_context);
            public IGenericRepository<License> Licenses => _licenses ??= new GenericRepository<License>(_context);
            public IGenericRepository<ServiceRecord> ServiceRecords =>
                                                     _serviceRecords ??= new GenericRepository<ServiceRecord>(_context);

            public async Task<int> CompleteAsync()
            {
                return await _context.SaveChangesAsync();
            }

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
    