using Lecar.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecar.Services
{
    public class IllnessService
    {
        private readonly SQLiteAsyncConnection _database;

        public IllnessService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Illness>().Wait();
        }

        public Task<List<Illness>> GetIllnessesAsync()
        {
            return _database.Table<Illness>().ToListAsync();
        }

        public Task AddIllnessAsync(Illness illness)
        {
            return _database.InsertAsync(illness);
        }

        public Task DeleteIllnessAsync(Illness illness)
        {
            return _database.DeleteAsync(illness);
        }

    }
}
