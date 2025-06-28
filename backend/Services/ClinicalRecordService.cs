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

        public ClinicalRecordService(MongoDbContext context)
        {
            _clinicalRecordsCollection = context.ClinicalRecords;
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
    }
}