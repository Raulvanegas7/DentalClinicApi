using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Enums;
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
            return await _patientsCollection.Find(x => true).ToListAsync();
        }

        public async Task<Patient> GetOneById(string id)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);
            var result = await _patientsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task RegisterPatientAsync(CreatePatientDto dto)
        {
            var existingPatient = await _patientsCollection.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingPatient != null)
                throw new Exception("Ya existe paciente con este correo");

            var newPatient = new Patient
            {
                Name = dto.Name,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                Phone = dto.Phone
            };

            await _patientsCollection.InsertOneAsync(newPatient);
        }

        public async Task PartialUpdateAsync(string id, UpdatePatientDto dto)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);

            var updates = new List<UpdateDefinition<Patient>>();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                updates.Add(Builders<Patient>.Update.Set(x => x.Name, dto.Name));

            if (!string.IsNullOrWhiteSpace(dto.Email))
                updates.Add(Builders<Patient>.Update.Set(x => x.Email, dto.Email));

            if (!string.IsNullOrWhiteSpace(dto.Phone))
                updates.Add(Builders<Patient>.Update.Set(x => x.Phone, dto.Phone));

            if (!string.IsNullOrWhiteSpace(dto.Address))
                updates.Add(Builders<Patient>.Update.Set(x => x.Address, dto.Address));

            if (dto.BirthDate.HasValue)
                updates.Add(Builders<Patient>.Update.Set(x => x.BirthDate, dto.BirthDate.Value));

            if (updates.Count > 0)
            {
                var combinedUpdate = Builders<Patient>.Update.Combine(updates);
                await _patientsCollection.UpdateOneAsync(filter, combinedUpdate);
            }
        }


        public async Task DeletePatient(string id)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, id);
            await _patientsCollection.DeleteOneAsync(filter);
        }
    }
}