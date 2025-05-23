    using System;
    using System.Threading.Tasks;

    namespace CustomerAssetTracker.Core.Abstractions
    {
        // Komentář: Rozhraní pro Unit of Work.
        // Poskytuje přístup k repozitářům a metodu pro uložení všech změn.
        // IDisposable je pro správné uvolnění zdrojů (např. DbContextu).
        public interface IUnitOfWork : IDisposable
        {
            // Komentář: Vlastnosti pro přístup ke konkrétním repozitářům.
            // Můžeš zde mít IGenericRepository<Customer> Customers { get; }
            // nebo si vytvořit specifická rozhraní jako ICustomerRepository.
            // Pro začátek použijeme obecný repozitář.
            IGenericRepository<Customer> Customers { get; }
            IGenericRepository<Machine> Machines { get; }
            IGenericRepository<License> Licenses { get; }
            IGenericRepository<ServiceRecord> ServiceRecords { get; }

            Task<int> CompleteAsync(); // Uložení všech změn do databáze
        }
    }