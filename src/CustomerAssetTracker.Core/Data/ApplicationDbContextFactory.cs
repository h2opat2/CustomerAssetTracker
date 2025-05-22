using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO; // Pro Path.Combine

namespace CustomerAssetTracker.Core.Data
{
    // Komentář: Tato třída slouží jako továrna pro vytváření instancí ApplicationDbContext
    // v době návrhu (design-time), např. pro generování migrací.
    // Je nutná, protože konstruktor ApplicationDbContext očekává DbContextOptions.
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Komentář: Metoda CreateDbContext je volána nástroji EF Core.
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Komentář: Zde konfigurujeme, jakou databázi chceme použít (SQLite)
            // a kde se má soubor databáze nacházet.
            // Používáme Path.Combine, aby cesta fungovala na různých OS.
            // Databáze se uloží do kořenové složky tvého řešení (Solution).
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "CustomerAssetTracker.db");
            // Alternativně, pokud chceš mít databázi přímo ve složce projektu Core:
            // var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "CustomerAssetTracker.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
