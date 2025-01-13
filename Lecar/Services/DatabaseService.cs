using SQLite;
using Lecar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecar.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Patient>().Wait();
        }

        public Task<List<Patient>> GetPatientsAsync()
        {
            return _database.Table<Patient>().ToListAsync();
        }

        public Task<int> AddPatientAsync(Patient patient)
        {
            return _database.InsertAsync(patient);
        }

        public Task<int> UpdatePatientAsync(Patient patient)
        {
            return _database.UpdateAsync(patient);
        }

        public Task<int> DeletePatientAsync(Patient patient)
        {
            return _database.DeleteAsync(patient);
        }
    }
}
