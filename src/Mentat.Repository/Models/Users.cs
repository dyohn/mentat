using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentat.Repository.Models
{
    public class Users
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set ;}

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("CreatedAt")]
        public string CreatedAt { get; set; }

        [BsonElement("UpdatedAt")]
        public string UpdatedAt { get; set; }

        [BsonElement("Role")]
        public int Role { get; set; }
    }
}
