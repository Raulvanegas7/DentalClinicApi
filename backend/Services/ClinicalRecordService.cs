using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using DentalClinicApi.Contexts;
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

        public async Task<List<ClinicalRecord>> GetByPatientIdAsync(string patientId)
        {
            return await _clinicalRecordsCollection.Find(x => x.PatientId == patientId).ToListAsync();
        }

        public async Task<ClinicalRecord?> GetByAppointmentIdAsync(string appointmentId)
        {
            return await _clinicalRecordsCollection.Find(x => x.AppointmentId == appointmentId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ClinicalRecord newClinicalRecord)
        {
            await _clinicalRecordsCollection.InsertOneAsync(newClinicalRecord);
        }

        public async Task<bool> PatientExists(string patientId)
        {
            return await _patientsCollection.Find(x => x.Id == patientId).AnyAsync();
        }

        public async Task<bool> AppointmentExists(string appointmentId)
        {
            return await _appointmentsCollection.Find(x => x.Id == appointmentId).AnyAsync();
        }

        public async Task<ClinicalRecordDetailedDto> GetClinicalWithDetailsAsync(string id)
        {
            var filter = Builders<ClinicalRecord>.Filter.Eq(x => x.Id,(id));
            var clinicalRecord = await _clinicalRecordsCollection.Find(filter).FirstOrDefaultAsync();
            if (clinicalRecord == null) return null;

            var appointment = await _appointmentsCollection.Find(x => x.Id == clinicalRecord.AppointmentId).FirstOrDefaultAsync();
            var dentist = await _dentistsCollection.Find(x => x.Id == clinicalRecord.DentistId).FirstOrDefaultAsync();
            var patient = await _patientsCollection.Find(x => x.Id == clinicalRecord.PatientId).FirstOrDefaultAsync();
            var service = await _servicesCollection.Find(x => x.Id == appointment.ServiceId).FirstOrDefaultAsync();

            if (appointment == null || dentist == null || patient == null || service == null) return null;

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