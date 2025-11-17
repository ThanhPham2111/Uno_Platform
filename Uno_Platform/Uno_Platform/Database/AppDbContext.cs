#if !__WASM__
using SQLite;
using System.IO;
using Windows.Storage;
using Uno_Platform.Models;

namespace Uno_Platform.Database;

public class AppDbContext
{
    private SQLiteConnection? _connection;
    private const string DatabaseFileName = "unoplatform.db";

    public SQLiteConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                InitializeDatabase();
            }
            return _connection!;
        }
    }

    private void InitializeDatabase()
    {
        string dbPath = GetDatabasePath();
        _connection = new SQLiteConnection(dbPath);
        CreateTables();
    }

    private string GetDatabasePath()
    {
#if __ANDROID__
        // For Android, use local app data
        string personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        return Path.Combine(personalFolder, DatabaseFileName);
#else
        // For other platforms
        string localFolder = ApplicationData.Current.LocalFolder.Path;
        return Path.Combine(localFolder, DatabaseFileName);
#endif
    }

    private void CreateTables()
    {
        Connection.CreateTable<Product>();
    }

    public void CloseConnection()
    {
        _connection?.Close();
        _connection = null;
    }
}
#endif

