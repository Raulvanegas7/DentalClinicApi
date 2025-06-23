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
    public class ServiceService
    {
        private readonly IMongoCollection<Service> _servicesCollection;

        public ServiceService(MongoDbContext contex)
        {
            _servicesCollection = contex.Services;
        }

        public async Task<List<Service>> GetAllServices()
        {
            return await _servicesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Service> GetOneById(string id)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            var result = await _servicesCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateService(Service newService)
        {
            await _servicesCollection.InsertOneAsync(newService);
        }

        public async Task UpdateService(string id, Service updateService)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            await _servicesCollection.ReplaceOneAsync(filter, updateService);
        }

        public async Task DeleteService(string id)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            await _servicesCollection.DeleteOneAsync(filter);
        }
    }
}