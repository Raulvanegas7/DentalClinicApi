using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Configurations;
using DentalClinicApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DentalClinicApi.Contexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<DataBaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            _database = client.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Patient> Patients =>
            _database.GetCollection<Patient>("Patients");
            
        public IMongoCollection<Dentist> Dentists =>
            _database.GetCollection<Dentist>("Dentists");

        public IMongoCollection<Service> Services =>
            _database.GetCollection<Service>("Services");

        public IMongoCollection<Appointment> Appointments =>
            _database.GetCollection<Appointment>("Appointments");

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");
    }
}