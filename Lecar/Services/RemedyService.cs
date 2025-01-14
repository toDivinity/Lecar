using Lecar.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecar.Services
{
    public class RemedyService
    {
        private readonly SQLiteAsyncConnection _database;

        public RemedyService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Remedy>().Wait();
        }

        public Task<List<Remedy>> GetRemediesAsync()
        {
            return _database.Table<Remedy>().ToListAsync();
        }

        public Task AddRemedyAsync(Remedy remedy)
        {
            return _database.InsertAsync(remedy);
        }

        public Task DeleteRemedyAsync(Remedy remedy)
        {
            return _database.DeleteAsync(remedy);
        }
    }
}
