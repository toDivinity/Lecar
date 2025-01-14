using SQLite;

namespace Lecar.Services
{
    public class DatabaseConnection
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseConnection(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
    }
}
