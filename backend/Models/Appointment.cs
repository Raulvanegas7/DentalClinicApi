using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DentalClinicApi.Models
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("patientUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PatientUserId { get; set; } = string.Empty;

        [BsonElement("dentistUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DentistUserId { get; set; } = string.Empty;

        [BsonElement("serviceId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceId { get; set; } = string.Empty;

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [BsonElement("notes")]
        public string Notes { get; set; } = string.Empty;
        [BsonElement("createdAt")]  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}