using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // ID de la cita asociada a este pago
        [BsonElement("appointmentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AppointmentId { get; set; }

        // Método de pago: efectivo, datáfono, tarjeta en línea, etc.
        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }

        // Estado del pago: Pending, Paid, Refunded
        [BsonElement("status")]
        public PaymentStatus Status { get; set; }

        // Monto pagado
        [BsonElement("amount")]
        public decimal Amount { get; set; }

        // Fecha en que se realizó el pago
        [BsonElement("paidAt")]
        public DateTime PaidAt { get; set; }

        // Nombre del paciente (opcional, puede usarse para búsquedas rápidas)
        [BsonElement("patientName")]
        public string PatientName { get; set; }

        // Observaciones del recepcionista si fue un pago presencial
        [BsonElement("notes")]
        public string? Notes { get; set; }
    }
}