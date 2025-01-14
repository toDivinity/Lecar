using Lecar.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecar.Services
{
    public class SymptomService
    {
        private readonly SQLiteAsyncConnection _database;

        public SymptomService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Symptom>().Wait();
        }

        public Task<List<Symptom>> GetSymptomsAsync()
        {
            return _database.Table<Symptom>().ToListAsync();
        }

        public Task<int> AddSymptomAsync(Symptom symptom)
        {
            return _database.InsertAsync(symptom);
        }
    }
}
