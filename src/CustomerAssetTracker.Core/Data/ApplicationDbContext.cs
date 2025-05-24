    using Microsoft.EntityFrameworkCore;

    namespace CustomerAssetTracker.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

                // Seedování zákazníků
                modelBuilder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Name = "ABC Company", Address = "123 Úzká, Praha 4", IsForeign = false },
                    new Customer { Id = 2, Name = "Global Corp", Address = "456 International Blvd, New York", IsForeign = true },
                    new Customer { Id = 3, Name = "KovoRobo", Address = "Hlavní 4, Pardubice", IsForeign = false },
                    new Customer { Id = 4, Name = "Metal2U s.r.o.", Address = "Dolní 2, Brno", IsForeign = false }
                );

                // Seedování strojů
                modelBuilder.Entity<Machine>().HasData(
                    new Machine
                    {
                        Id = 1,
                        Name = "CMM XYZ-1000",
                        SerialNumber = "CMM-SN-001",
                        Manufacturer = "Hexagon",
                        PurchaseDate = new DateTime(2022, 1, 15),
                        CustomerId = 1, // Reference to ABC Company
                        MachineType = Machine.MachineTypes.Cmm
                    },
                    new Machine
                    {
                        Id = 2,
                        Name = "Arm R-7",
                        SerialNumber = "ARM-SN-002",
                        Manufacturer = "FARO",
                        PurchaseDate = new DateTime(2023, 3, 10),
                        CustomerId = 1, // Reference to ABC Company
                        MachineType = Machine.MachineTypes.Arm
                    },
                    new Machine
                    {
                        Id = 3,
                        Name = "Laser Tracker LT-500",
                        SerialNumber = "LT-SN-003",
                        Manufacturer = "Leica",
                        PurchaseDate = new DateTime(2021, 6, 20),
                        CustomerId = 2, // Reference to Global Corp
                        MachineType = Machine.MachineTypes.LaserTracker
                    },
                    new Machine
                    {
                        Id = 4,
                        Name = "NimbleTrack",
                        SerialNumber = "SK1230054",
                        Manufacturer = "Scantech",
                        PurchaseDate = new DateTime(2024, 12, 10),
                        CustomerId = 3, // Reference to KovoRobo
                        MachineType = Machine.MachineTypes.HandHeldScanner
                    }
                );

                // Seedování licencí
                modelBuilder.Entity<License>().HasData(
                    new License
                    {
                        Id = 1,
                        Software = "PC-DMIS",
                        Version = "2023.1",
                        Vendor = "Hexagon",
                        MaintenanceContract = new DateTime(2025, 12, 31),
                        MachineId = 1, // Reference to CMM XYZ-1000
                        CustomerId = 1, // Reference to ABC Company
                        Type = License.LicenseType.Dongle
                    },
                    new License
                    {
                        Id = 2,
                        Software = "PolyWorks",
                        Version = "2024",
                        Vendor = "InnovMetric",
                        MaintenanceContract = new DateTime(2024, 11, 1),
                        MachineId = 2, // Reference to Arm R-7
                        CustomerId = 1, // Reference to ABC Company
                        Type = License.LicenseType.Floating
                    },
                    new License
                    {
                        Id = 3,
                        Software = "SpatialAnalyzer",
                        Version = "2023",
                        Vendor = "New River Kinematics",
                        MaintenanceContract = null,
                        MachineId = 3, // Reference to Laser Tracker LT-500
                        CustomerId = 2, // Reference to Global Corp
                        Type = License.LicenseType.Software
                    },
                    new License
                    {
                        Id = 4,
                        Software = "PolyWorks",
                        Version = "2024",
                        Vendor = "InnovMetric",
                        MaintenanceContract = new DateTime(2025, 12, 1), 
                        MachineId = 4, // Reference to NimbleTrack
                        CustomerId = 3, // Reference to KovoRobo
                        Type = License.LicenseType.Dongle
                    }
                    ,
                    new License
                    {
                        Id = 5,
                        Software = "IMK UP!",
                        Version = "3.4.3",
                        Vendor = "Topmes",
                        MaintenanceContract = null,
                        MachineId = null,
                        CustomerId = 2, // Reference to Global Corp
                        Type = License.LicenseType.Software
                    }
                );

                // Seeding service records
                modelBuilder.Entity<ServiceRecord>().HasData(
                    new ServiceRecord
                    {
                        Id = 1,
                        Date = new DateTime(2023, 5, 10),
                        Technician = "Jan Novák",
                        Text = "Pravidelná kalibrace a údržba. Vyměněny filtry.",
                        MachineId = 1 // Reference to CMM XYZ-1000
                    },
                    new ServiceRecord
                    {
                        Id = 2,
                        Date = new DateTime(2024, 1, 25),
                        Technician = "Petra Svobodová",
                        Text = "Oprava kloubu ramene. Testováno a funkční.",
                        MachineId = 2 // Reference to Arm R-7
                    }
                    ,
                    new ServiceRecord
                    {
                        Id = 3,
                        Date = new DateTime(2024, 5, 25),
                        Technician = "Jan Novák",
                        Text = "Periodická 1 roční kalibrace dle ISO 10360",
                        MachineId = 1 // Reference to CMM XYZ-1000
                    }
                );
        }
    }
}
    