using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Contexts;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class AppointmentService
    {
        private readonly IMongoCollection<Appointment> _appointmentsCollection;
        private readonly IMongoCollection<Patient> _patientsCollection;
        private readonly IMongoCollection<Dentist> _dentistsCollection;
        private readonly IMongoCollection<Service> _servicesCollection;

        public AppointmentService(MongoDbContext context)
        {
            _appointmentsCollection = context.Appointments;
            _patientsCollection = context.Patients;
            _dentistsCollection = context.Dentists;
            _servicesCollection = context.Services;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _appointmentsCollection.Find(x => true).ToListAsync();
        }

        public async Task<Appointment> GetOneById(string id)
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

        public async Task<List<AppointmentDetailDto>> GetDetailedAppointments()
        {
            var appointments = await _appointmentsCollection.Find(_ => true).ToListAsync();

            var patients = await _patientsCollection.Find(_ => true).ToListAsync();
            var dentists = await _dentistsCollection.Find(_ => true).ToListAsync();
            var services = await _servicesCollection.Find(_ => true).ToListAsync();

            var result = appointments.Select(app =>
            {
                var patient = patients.FirstOrDefault(p => p.Id == app.PatientId);
                var dentist = dentists.FirstOrDefault(d => d.Id == app.DentistId);
                var service = services.FirstOrDefault(s => s.Id == app.ServiceId);

                return new AppointmentDetailDto
                {
                    Id = app.Id,
                    Date = app.Date,
                    Notes = app.Notes,
                    Patient = new PatientMiniDto
                    {
                        Id = patient?.Id ?? string.Empty,
                        Name = patient?.Name ?? "Desconocido",
                        Email = patient?.Email ?? string.Empty
                    },
                    Dentist = new DentistMiniDto
                    {
                        Id = dentist?.Id ?? string.Empty,
                        Name = dentist?.Name ?? "Desconocido",
                        Specialty = dentist?.Specialty ?? string.Empty
                    },
                    Service = new ServiceMiniDto
                    {
                        Id = service?.Id ?? string.Empty,
                        Name = service?.Name ?? "Desconocido"
                    }
                };
            }).ToList();

            return result;
        }


        public async Task<bool> PatientExists(string patientId)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.Id, patientId);
            return await _patientsCollection.Find(filter).AnyAsync();
        }

        public async Task<bool> DentistExists(string dentistId)
        {
            var filter = Builders<Dentist>.Filter.Eq(x => x.Id, dentistId);
            return await _dentistsCollection.Find(filter).AnyAsync();
        }

        public async Task<bool> ServiceExists(string serviceId)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, serviceId);
            return await _servicesCollection.Find(filter).AnyAsync();
        }

    }
}