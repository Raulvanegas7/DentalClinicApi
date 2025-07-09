using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Enums;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class DentistService
    {
        private readonly IMongoCollection<Dentist> _dentistsCollection;
        private readonly IMongoCollection<User> _usersCollection;

        private readonly JwtService _jwtService;
        public DentistService(MongoDbContext context, JwtService jwtService)
        {
            _dentistsCollection = context.Dentists;
            _usersCollection = context.Users;
            _jwtService = jwtService;
        }

        public async Task<List<Dentist>> GetAllDentists()
        {
            return await _dentistsCollection.Find(x => true).ToListAsync();
        }

        public async Task<Dentist> GetOneById(string id)
        {
            var filter = Builders<Dentist>.Filter.Eq(x => x.Id, id);
            var result = await _dentistsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> RegisterUserWithDentistAsync(CreateDentistDto dto)
        {
            var existingUser = await _usersCollection.Find(u => u.Email == dto.Email).FirstOrDefaultAsync();
            if (existingUser != null) return null;

            var existingDentist = await _dentistsCollection.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingDentist != null) return null;

            var newUser = new User
            {
                Username = dto.Email,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Dentist
            };

            await _usersCollection.InsertOneAsync(newUser);

            var newDentist = new Dentist
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                Email = dto.Email,
                Phone = dto.Phone,
                UserId = newUser.Id
            };

            await _dentistsCollection.InsertOneAsync(newDentist);

            return _jwtService.GenerateToken(newUser);
        }

        public async Task UpdateDentist(string id, Dentist updateDentist)
        {
            var filter = Builders<Dentist>.Filter.Eq(x => x.Id, id);
            await _dentistsCollection.ReplaceOneAsync(filter, updateDentist);
        }

        public async Task DeleteDentist(string id)
        {
            var filter = Builders<Dentist>.Filter.Eq(x => x.Id, id);
            await _dentistsCollection.DeleteOneAsync(filter);
        }
    }
}