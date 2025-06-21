using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patientsCollection;

        public PatientService(MongoDbContext context)
        {
            _patientsCollection = context.Patients;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _patientsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Patient> GetOneById(string id)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);
            var result = await _patientsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task CreatePatient(Patient newPatient)
        {
            await _patientsCollection.InsertOneAsync(newPatient);
        }

        public async Task UpdatePatient(string id, Patient updatedPatien)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);
            await _patientsCollection.ReplaceOneAsync(filter, updatedPatien);
        }

        public async Task DeletePatient(string id)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);
            await _patientsCollection.DeleteOneAsync(filter);
        }
    }
}