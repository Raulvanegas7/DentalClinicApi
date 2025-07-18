using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
            return await _servicesCollection.Find(x => true).ToListAsync();
        }

        public async Task<Service> GetOneById(string id)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            var result = await _servicesCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Service> CreateServiceAsync(CreateServiceDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("El nombre del servicio es obligatorio.");

            if (dto.Price <= 0)
                throw new Exception("El precio debe ser mayor que cero.");

            var exists = await _servicesCollection.Find(s => s.Name == dto.Name).AnyAsync();
            if (exists)
                throw new Exception("Ya existe un servicio con ese nombre.");

            var newService = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };
            await _servicesCollection.InsertOneAsync(newService);
            return newService;
        }

        public async Task UpdateService(string id, UpdateServiceDto dto)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            var updates = new List<UpdateDefinition<Service>>();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                updates.Add(Builders<Service>.Update.Set(x => x.Name, dto.Name));

            if (!string.IsNullOrWhiteSpace(dto.Description))
                updates.Add(Builders<Service>.Update.Set(x => x.Description, dto.Description));

            if (dto.Price.HasValue)
                updates.Add(Builders<Service>.Update.Set(x => x.Price, dto.Price.Value));

            var updateCombine = Builders<Service>.Update.Combine(updates);
            await _servicesCollection.UpdateOneAsync(filter, updateCombine);
        }

        public async Task DeleteService(string id)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, id);
            await _servicesCollection.DeleteOneAsync(filter);
        }
    }
}