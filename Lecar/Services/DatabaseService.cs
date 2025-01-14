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
            _database.CreateTableAsync<Illness>().Wait();
            _database.CreateTableAsync<Remedy>().Wait();
        }

        public Task<List<Patient>> GetPatientsAsync()
        {
            return _database.Table<Patient>().ToListAsync();
        }

        public Task AddPatientAsync(Patient patient)
        {
            return _database.InsertAsync(patient);
        }

        public Task DeletePatientAsync(Patient patient)
        {
            return _database.DeleteAsync(patient);
        }
        public Task<List<Illness>> GetIllnessesAsync()
        {
            return _database.Table<Illness>().ToListAsync();
        }
        public Task DeleteIllnessAsync(Illness illness)
        {
            return _database.DeleteAsync(illness);
        }
        public Task AddIllnessAsync(Illness illness)
        {
            return _database.InsertAsync(illness);
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
