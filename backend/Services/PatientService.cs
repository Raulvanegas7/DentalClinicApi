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
        private readonly IMongoCollection<User> _usersCollection;
        private readonly JwtService _jwtService;

        public PatientService(MongoDbContext context, JwtService jwtService)
        {
            _patientsCollection = context.Patients;
            _usersCollection = context.Users;
            _jwtService = jwtService;
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

        public async Task RegisterPatientWithUserAsync(CreatePatientDto dto)
        {
            var existingUser = await _usersCollection.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingUser != null)
                throw new Exception("Usuario ya existe con este correo");

            var existingPatient = await _patientsCollection.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingPatient != null)
                throw new Exception("Ya existe paciente con este correo");

            var newUser = new User
            {
                Username = dto.Email,
                Email = dto.Email,
                Role = UserRole.Patient,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

            };

            await _usersCollection.InsertOneAsync(newUser);

            var newPatient = new Patient
            {
                Name = dto.Name,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                Phone = dto.Phone,
                UserId = newUser.Id
            };
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