using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class AppointmentService
    {
        private readonly IMongoCollection<Appointment> _appointmentsCollection;

        public AppointmentService(MongoDbContext contex)
        {
            _appointmentsCollection = contex.Appointments;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _appointmentsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Appointment> GetOneBiId(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            var result = await _appointmentsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateAppointment(Appointment newAppointment)
        {
            await _appointmentsCollection.InsertOneAsync(newAppointment);
        }

        public async Task UpdateApp(string id, Appointment updateApp)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            await _appointmentsCollection.ReplaceOneAsync(filter, updateApp);
        }

        public async Task DeleteAppointment(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            await _appointmentsCollection.DeleteOneAsync(filter);
        }
    }
}