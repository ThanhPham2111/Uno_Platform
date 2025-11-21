#if !__WASM__
using Microsoft.EntityFrameworkCore;
using System.IO;
using Uno_Platform.Models;
using Windows.Storage;

namespace Uno_Platform.Database;

public class EfAppDbContext : DbContext
{
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dbPath = GetDatabasePath();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

    private string GetDatabasePath()
    {
        string dbPath;
#if __ANDROID__
        // For Android, use local app data
        string personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        dbPath = Path.Combine(personalFolder, "cart.db");
#elif __IOS__
        // For iOS, use documents folder
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string libraryPath = Path.Combine(documentsPath, "..", "Library");
        dbPath = Path.Combine(libraryPath, "cart.db");
#else
        // For other platforms (Windows, etc.)
        string localFolder = ApplicationData.Current.LocalFolder.Path;
        dbPath = Path.Combine(localFolder, "cart.db");
#endif

        // Ensure the directory exists
        string? directory = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            System.Diagnostics.Debug.WriteLine($"Created database directory: {directory}");
        }

        System.Diagnostics.Debug.WriteLine($"Database path: {dbPath}");
        return dbPath;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure CartItem entity
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductName).IsRequired();
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Quantity).IsRequired();
        });
    }
}
#endif

