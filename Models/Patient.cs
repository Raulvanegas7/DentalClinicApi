using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace DentalClinicApi.Models
{
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema("ID único del paciente", ReadOnly = true)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [SwaggerSchema("Nombre completo del paciente", Nullable = false)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        [SwaggerSchema("Correo electrónico del paciente", Nullable = false)]
        public string Email { get; set; } = string.Empty;

        [BsonElement("phone")]
        [SwaggerSchema("Número telefónico del paciente (opcional)")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("birthDate")]
        [SwaggerSchema("Fecha de nacimiento del paciente", Format = "date")]
        public DateTime BirthDate { get; set; }
    }
}