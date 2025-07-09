using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DentalClinicApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("role")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public UserRole Role { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}