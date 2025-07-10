using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DentalClinicApi.Models
{
    public class ClinicalRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string AppointmentId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string PatientId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string DentistId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceId { get; set; } = null;

        public string Diagnosis { get; set; } = null!;

        public string Treatment { get; set; } = null!;

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}