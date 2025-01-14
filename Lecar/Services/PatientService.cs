using Lecar.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecar.Services
{
    public class PatientService
    {
        private readonly SQLiteAsyncConnection _database;

        public PatientService(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Patient>().Wait();
        }

        public Task<List<Patient>> GetPatientsAsync()
        {
            return _database.Table<Patient>().ToListAsync();
        }

        public Task AddPatientAsync(Patient patient)
        {
            return _database.InsertAsync(patient);
        }

        public Task<int> UpdatePatientAsync(Patient patient)
        {
            return _database.UpdateAsync(patient);
        }

        public Task DeletePatientAsync(Patient patient)
        {
            return _database.DeleteAsync(patient);
        }
    }
}
