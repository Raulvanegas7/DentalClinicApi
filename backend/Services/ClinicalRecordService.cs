using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Contexts;
using DentalClinicApi.Models;
using MongoDB.Driver;

namespace DentalClinicApi.Services
{
    public class ClinicalRecordService
    {
        private readonly IMongoCollection<ClinicalRecord> _clinicalRecordsCollection;
        private readonly IMongoCollection<Patient> _patientsCollection;
        private readonly IMongoCollection<Appointment> _appointmentsCollection;

        public ClinicalRecordService(MongoDbContext context)
        {
            _clinicalRecordsCollection = context.ClinicalRecords;
            _patientsCollection = context.Patients;
            _appointmentsCollection = context.Appointments;
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
    }
}