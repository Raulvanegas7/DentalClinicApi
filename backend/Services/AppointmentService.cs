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

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentsCollection.Find(x => true).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDentist(string dentistUserId)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.DentistUserId, dentistUserId);
            var result = await _appointmentsCollection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            var result = await _appointmentsCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            var patient = await _patientsCollection.Find(x => x.UserId == dto.PatientUserId).FirstOrDefaultAsync();
            if (patient == null)
                throw new Exception("El paciente no existe");

            var dentist = await _dentistsCollection.Find(x => x.UserId == dto.DentistUserId).FirstOrDefaultAsync();
            if (dentist == null)
                throw new Exception("Dentista no existe");

            var service = await _servicesCollection.Find(x => x.Id == dto.ServiceId).FirstOrDefaultAsync();
            if (service == null)
                throw new Exception("El servicio no existe.");

            if (dto.Date <= DateTime.UtcNow)
            {
                throw new Exception("No se puede agendar una cita en el pasado.");
            }

            var localHour = dto.Date.ToLocalTime().TimeOfDay;

            if (localHour < TimeSpan.FromHours(8) || localHour >= TimeSpan.FromHours(18))
            {
                throw new Exception("El horario permitido es de 8:00 AM a 6:00 PM hora Colombia.");
            }

            if (!await IsDentistAvailableAsync(dentist.UserId, dto.Date))
            {
                throw new Exception("El dentista ya tiene una cita en ese horario.");
            }

            if (!await IsPatientAvailableAsync(patient.UserId, dto.Date))
            {
                throw new Exception("El paciente ya tiene una cita en ese horario.");
            }

            var newAppointment = new Appointment
            {
                PatientUserId = patient.UserId,
                DentistUserId = dentist.UserId,
                ServiceId = dto.ServiceId,
                Date = dto.Date,
                Notes = dto.Notes,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.UtcNow
            };

            await _appointmentsCollection.InsertOneAsync(newAppointment);
            return newAppointment;
        }


        public async Task UpdateAppointmentAsync(string id, UpdateAppointmentDto dto)
        {
            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);

            var appointment = await _appointmentsCollection.Find(filter).FirstOrDefaultAsync();
            if (appointment == null)
                throw new Exception("La cita no existe.");

            var updates = new List<UpdateDefinition<Appointment>>();

            if (dto.Date.HasValue)
            {
                var newDate = dto.Date.Value;
                if (newDate <= DateTime.UtcNow)
                    throw new Exception("No se puede mover la cita al pasado.");

                var localHour = newDate.ToLocalTime().TimeOfDay;
                if (localHour < TimeSpan.FromHours(8) || localHour >= TimeSpan.FromHours(18))
                    throw new Exception("El horario permitido es de 8:00 AM a 6:00 PM hora Colombia.");

                if (!await IsDentistAvailableAsync(appointment.DentistUserId, newDate, appointment.Id))
                    throw new Exception("El odontólogo no está disponible en ese horario.");

                if (!await IsPatientAvailableAsync(appointment.PatientUserId, newDate, appointment.Id))
                    throw new Exception("El paciente no está disponible en ese horario.");

                updates.Add(Builders<Appointment>.Update.Set(x => x.Date, newDate));
            }

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
                var patient = patients.FirstOrDefault(p => p.UserId == app.PatientUserId);
                var dentist = dentists.FirstOrDefault(d => d.UserId == app.DentistUserId);
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

        public async Task MarkCompleteAsync(string id, string dentistUserId)
        {
            var appointment = await GetAppointmentByIdAsync(id);

            if (appointment == null)
                throw new Exception("La cita no existe.");

            if (appointment.DentistUserId != dentistUserId)
                throw new Exception("No tiene permiso para completar esta cita.");

            var filter = Builders<Appointment>.Filter.Eq(x => x.Id, id);
            var update = Builders<Appointment>.Update.Set(x => x.Status, AppointmentStatus.Completed);

            await _appointmentsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> IsDentistAvailableAsync(string dentistUserId, DateTime date, string? appointmentIdToExclude = null)
        {
            var start = date;
            var end = date.AddMinutes(30);

            var filter = Builders<Appointment>.Filter.And(
                Builders<Appointment>.Filter.Eq(x => x.DentistUserId, dentistUserId),
                Builders<Appointment>.Filter.Lt(x => x.Date, end),
                Builders<Appointment>.Filter.Gt(x => x.Date, start.AddMinutes(-30))
            );

            if (!string.IsNullOrEmpty(appointmentIdToExclude))
                filter &= Builders<Appointment>.Filter.Ne(x => x.Id, appointmentIdToExclude);

            return !await _appointmentsCollection.Find(filter).AnyAsync();
        }


        public async Task<bool> IsPatientAvailableAsync(string patientUserId, DateTime newStart, string? appointmentIdToExclude = null)
        {
            var newEnd = newStart.AddMinutes(30);

            var filter = Builders<Appointment>.Filter.And(
                Builders<Appointment>.Filter.Eq(x => x.PatientUserId, patientUserId),
                Builders<Appointment>.Filter.Lt(x => x.Date, newEnd),
                Builders<Appointment>.Filter.Gt(x => x.Date, newStart.AddMinutes(-30))
            );

            if (!string.IsNullOrEmpty(appointmentIdToExclude))
            {
                filter &= Builders<Appointment>.Filter.Ne(x => x.Id, appointmentIdToExclude);
            }

            return !await _appointmentsCollection.Find(filter).AnyAsync();
        }

    }
}

