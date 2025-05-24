using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerAssetTracker.Core.Data
{
    // neccessary class for creating migrations during design time
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Database is created in the solution root directory,
            // which is two levels up from the Core project directory.
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "CustomerAssetTracker.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
