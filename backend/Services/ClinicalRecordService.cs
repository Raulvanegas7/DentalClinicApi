using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using DentalClinicApi.Contexts;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class ClinicalRecordService
    {
        private readonly IMongoCollection<ClinicalRecord> _clinicalRecordsCollection;
        private readonly IMongoCollection<Patient> _patientsCollection;
        private readonly IMongoCollection<Appointment> _appointmentsCollection;
        private readonly IMongoCollection<Dentist> _dentistsCollection;
        private readonly IMongoCollection<Service> _servicesCollection;

        public ClinicalRecordService(MongoDbContext context)
        {
            _clinicalRecordsCollection = context.ClinicalRecords;
            _patientsCollection = context.Patients;
            _appointmentsCollection = context.Appointments;
            _dentistsCollection = context.Dentists;
            _servicesCollection = context.Services;
        }

        public async Task<List<ClinicalRecord>> GetAllClinicalRecordsAsync()
        {
            return await _clinicalRecordsCollection.Find(x => true).ToListAsync();
        }

        public async Task<List<ClinicalRecord>> GetByPatientIdAsync(string patientUserId)
        {
            return await _clinicalRecordsCollection.Find(x => x.PatientUserId == patientUserId).ToListAsync();
        }

        public async Task<ClinicalRecordDetailedDto> GetBytPatientDetailAsync(string patientUserId)
        {
           var filter = Builders<ClinicalRecord>.Filter.Eq(x => x.PatientUserId, patientUserId);
            var clinicalRecord = await _clinicalRecordsCollection.Find(filter).FirstOrDefaultAsync();
            if (clinicalRecord == null) return null!;

            var appointment = await _appointmentsCollection.Find(x => x.Id == clinicalRecord.AppointmentId).FirstOrDefaultAsync();
            var dentist = await _dentistsCollection.Find(x => x.UserId == clinicalRecord.DentistUserId).FirstOrDefaultAsync();
            var patient = await _patientsCollection.Find(x => x.UserId == clinicalRecord.PatientUserId).FirstOrDefaultAsync();
            var service = await _servicesCollection.Find(x => x.Id == appointment.ServiceId).FirstOrDefaultAsync();

            if (appointment == null || dentist == null || patient == null || service == null) return null!;

            return new ClinicalRecordDetailedDto
            {
                Id = clinicalRecord.Id,
                Diagnosis = clinicalRecord.Diagnosis,
                Treatment = clinicalRecord.Treatment,
                Notes = clinicalRecord.Notes,
                Appointment = new AppoinmentMiniDtoCr
                {
                    Id = appointment.Id,
                    Date = appointment.Date,
                    Status = appointment.Status,
                    Service = new ServiceDtoCr
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Price = service.Price
                    }
                },
                Patient = new PatientMiniDtoCr
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Email = patient.Email,
                    Phone = patient.Phone
                },
                Dentist = new DentistMiniDtoCr
                {
                    Id = dentist.Id,
                    Name = dentist.Name,
                    Specialty = dentist.Specialty
                }
            };
            
        }
        
        public async Task<ClinicalRecord?> GetByAppointmentIdAsync(string appointmentId)
        {
            return await _clinicalRecordsCollection.Find(x => x.AppointmentId == appointmentId).FirstOrDefaultAsync();
        }



        public async Task<ClinicalRecord> CreateClinicalRecordAsync(CreateClinicalRecordDto dto)
        {
            var appointment = await _appointmentsCollection.Find(x => x.Id == dto.AppointmentId).FirstOrDefaultAsync();
            if (appointment == null)
                throw new Exception("La cita no existe");

            var exists = await _clinicalRecordsCollection.Find(x => x.AppointmentId == dto.AppointmentId).AnyAsync();
            if (exists)
                throw new Exception("Ya existe una historia clínica para esta cita.");

            var newClinicalRecord = new ClinicalRecord
            {
                AppointmentId = dto.AppointmentId,
                PatientUserId = appointment.PatientUserId,
                DentistUserId = appointment.DentistUserId,
                ServiceId = appointment.ServiceId,
                Diagnosis = dto.Diagnosis,
                Treatment = dto.Treatment,
                Notes = dto.Notes
            };

            await _clinicalRecordsCollection.InsertOneAsync(newClinicalRecord);
            return newClinicalRecord;
        }

        public async Task<ClinicalRecordDetailedDto> GetClinicalWithDetailsAsync(string id)
        {
            var filter = Builders<ClinicalRecord>.Filter.Eq(x => x.Id, id);
            var clinicalRecord = await _clinicalRecordsCollection.Find(filter).FirstOrDefaultAsync();
            if (clinicalRecord == null) return null!;

            var appointment = await _appointmentsCollection.Find(x => x.Id == clinicalRecord.AppointmentId).FirstOrDefaultAsync();
            var dentist = await _dentistsCollection.Find(x => x.UserId == clinicalRecord.DentistUserId).FirstOrDefaultAsync();
            var patient = await _patientsCollection.Find(x => x.UserId == clinicalRecord.PatientUserId).FirstOrDefaultAsync();
            var service = await _servicesCollection.Find(x => x.Id == appointment.ServiceId).FirstOrDefaultAsync();

            if (appointment == null || dentist == null || patient == null || service == null) return null!;

            return new ClinicalRecordDetailedDto
            {
                Id = clinicalRecord.Id,
                Diagnosis = clinicalRecord.Diagnosis,
                Treatment = clinicalRecord.Treatment,
                Notes = clinicalRecord.Notes,
                Appointment = new AppoinmentMiniDtoCr
                {
                    Id = appointment.Id,
                    Date = appointment.Date,
                    Status = appointment.Status,
                    Service = new ServiceDtoCr
                    {
                        Id = service.Id,
                        Name = service.Name,
                        Price = service.Price
                    }
                },
                Patient = new PatientMiniDtoCr
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Email = patient.Email,
                    Phone = patient.Phone
                },
                Dentist = new DentistMiniDtoCr
                {
                    Id = dentist.Id,
                    Name = dentist.Name,
                    Specialty = dentist.Specialty
                }
            };


        }
    }
}