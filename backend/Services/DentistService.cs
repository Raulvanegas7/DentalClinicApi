using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
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

        public DentistService(MongoDbContext context)
        {
            _dentistsCollection = context.Dentists;
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

        public async Task<Dentist> CreateDentist(CreateDentistDto dto)
        {
            var newDentist = new Dentist
            {
                Name = dto.Name,
                Email = dto.Email,
                Specialty = dto.Specialty,
                Phone = dto.Phone
            };

            await _dentistsCollection.InsertOneAsync(newDentist);
            return newDentist;
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