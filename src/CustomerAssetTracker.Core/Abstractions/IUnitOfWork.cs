
    namespace CustomerAssetTracker.Core.Abstractions
    {

        public interface IUnitOfWork : IDisposable
        {
            IGenericRepository<Customer> Customers { get; }
            IGenericRepository<Machine> Machines { get; }
            IGenericRepository<License> Licenses { get; }
            IGenericRepository<ServiceRecord> ServiceRecords { get; }

            Task<int> CompleteAsync(); // saves all changes to the database
        }
    }