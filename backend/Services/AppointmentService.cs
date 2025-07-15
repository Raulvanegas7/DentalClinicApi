using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Enums;
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

        public async Task<List<Appointment>> GetAppointmentsByDentist(string dentistUserId)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.DentistId, dentistUserId);
            var result = await _appointmentsCollection.Find(filter).ToListAsync();
            return result;
        }
        public async Task<Appointment> CreateAppointment(CreateAppointmentDto dto)
        {
            var patient = await _patientsCollection.Find(x => x.UserId == dto.PatientUserId).FirstOrDefaultAsync();
            if (patient == null)
                throw new Exception("El paciente no existe");

            var dentist = await _dentistsCollection.Find(x => x.Id == dto.DentistProfileId).FirstOrDefaultAsync();
            if (dentist == null)
                throw new Exception("Dentista no existe");

            var service = await _servicesCollection.Find(x => x.Id == dto.ServiceId).FirstOrDefaultAsync();
            if (service == null)
                throw new Exception("El servicio no existe.");

            var newAppointment = new Appointment
            {
                PatientId = patient.UserId,
                DentistId = dentist.UserId,
                ServiceId = dto.ServiceId,
                Date = dto.Date,
                Notes = dto.Notes,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.UtcNow
            };

            await _appointmentsCollection.InsertOneAsync(newAppointment);
            return newAppointment;
        }


        public async Task PartialUpdateBasicAsync(string id, UpdateAppointmentDto dto)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);

            var updates = new List<UpdateDefinition<Appointment>>();

            if (dto.Date.HasValue)
                updates.Add(Builders<Appointment>.Update.Set(x => x.Date, dto.Date.Value));

            if (!string.IsNullOrWhiteSpace(dto.Notes))
                updates.Add(Builders<Appointment>.Update.Set(x => x.Notes, dto.Notes));

            if (dto.Status.HasValue)
                updates.Add(Builders<Appointment>.Update.Set(x => x.Status, dto.Status.Value));

            if (updates.Count > 0)
            {
                var combinedUpdate = Builders<Appointment>.Update.Combine(updates);
                await _appointmentsCollection.UpdateOneAsync(filter, combinedUpdate);
            }
        }

        public async Task MarkCompleteAsync(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            var update = Builders<Appointment>.Update
                .Set(x => x.Status, AppointmentStatus.Completed);

            await _appointmentsCollection.UpdateOneAsync(filter, update);
        }


        public async Task DeleteAppointment(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            await _appointmentsCollection.DeleteOneAsync(filter);
        }

        public async Task<List<AppointmentDetailDto>> GetDetailedAppointments()
        {
            var appointments = await _appointmentsCollection.Find(x => true).ToListAsync();

            var patients = await _patientsCollection.Find(x => true).ToListAsync();
            var dentists = await _dentistsCollection.Find(x => true).ToListAsync();
            var services = await _servicesCollection.Find(x => true).ToListAsync();

            var result = appointments.Select(app =>
            {
                var patient = patients.FirstOrDefault(p => p.UserId == app.PatientId);
                var dentist = dentists.FirstOrDefault(d => d.UserId == app.DentistId);
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
                        Name = service?.Name ?? "Desconocido",
                        Description = service?.Description ?? string.Empty
                    }
                };
            }).ToList();

            return result;
        }

        public async Task<bool> ServiceExists(string serviceId)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, serviceId);
            return await _servicesCollection.Find(filter).AnyAsync();
        }

        public async Task<bool> IsDentistAvailableAsync(string dentistProfileId, DateTime date)
        {
            var dentist = await _dentistsCollection.Find(x => x.Id == dentistProfileId).FirstOrDefaultAsync();
            if (dentist == null)
                throw new Exception("El odontólogo no existe.");

            var requestedStart = date;
            var requestedEnd = date.AddMinutes(30);

            var filter = Builders<Appointment>.Filter.And(
                Builders<Appointment>.Filter.Eq(x => x.DentistId, dentist.UserId),
                Builders<Appointment>.Filter.Ne(x => x.Status, AppointmentStatus.Completed),
                Builders<Appointment>.Filter.Or(
                    Builders<Appointment>.Filter.And(
                        Builders<Appointment>.Filter.Lt(x => x.Date, requestedEnd),
                        Builders<Appointment>.Filter.Gt(x => x.Date, requestedStart.AddMinutes(-30))
                    )
                )
            );

            var conflict = await _appointmentsCollection.Find(filter).FirstOrDefaultAsync();
            return conflict == null;
        }


        public async Task<bool> IsPatientAvailableAsync(string patientUserId, DateTime date)
        {
            var requestedStart = date;
            var requestedEnd = date.AddMinutes(30);

            var filter = Builders<Appointment>.Filter.And(
                Builders<Appointment>.Filter.Eq(x => x.PatientId, patientUserId),
                Builders<Appointment>.Filter.Ne(x => x.Status, AppointmentStatus.Completed),
                Builders<Appointment>.Filter.Or(
                    Builders<Appointment>.Filter.And(
                        Builders<Appointment>.Filter.Lt(x => x.Date, requestedEnd),
                        Builders<Appointment>.Filter.Gt(x => x.Date, requestedStart.AddMinutes(-30))
                    )
                )
            );

            var conflict = await _appointmentsCollection.Find(filter).FirstOrDefaultAsync();
            return conflict == null;
        }




    }
}