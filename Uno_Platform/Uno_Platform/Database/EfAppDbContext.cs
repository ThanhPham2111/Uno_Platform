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
#if __ANDROID__
        // For Android, use local app data
        string personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        return Path.Combine(personalFolder, "cart.db");
#elif __IOS__
        // For iOS, use documents folder
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        return Path.Combine(documentsPath, "..", "Library", "cart.db");
#else
        // For other platforms (Windows, etc.)
        string localFolder = ApplicationData.Current.LocalFolder.Path;
        return Path.Combine(localFolder, "cart.db");
#endif
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

