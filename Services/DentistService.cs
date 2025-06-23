using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _dentistsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Dentist> GetOneById(string id)
        {
            var filter = Builders<Dentist>.Filter.Eq(x => x.Id, id);
            var result = await _dentistsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateDentist(Dentist newDentist)
        {
            await _dentistsCollection.InsertOneAsync(newDentist);
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