using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DentalClinicApi.Models
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("patientId")]
        public string PatientId { get; set; } = string.Empty;

        [BsonElement("dentistId")]
        public string DentistId { get; set; } = string.Empty;

        [BsonElement("serviceId")]
        public string ServiceId { get; set; } = string.Empty;

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; } = string.Empty;
    }
}