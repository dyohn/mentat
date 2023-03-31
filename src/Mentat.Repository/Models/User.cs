using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentat.Repository.Models
{
    public class User
    {
        public User(string firstName, string lastName, string username, string email, string password, int role)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Role = role;
        }

        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set ;}

        [BsonElement("FirstName")]
        public string FirstName { get; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Email")]
        private string Email { get; set; }

        [BsonElement("Password")]
        private string Password { get; set; }

        [BsonElement("CreatedAt")]
        private DateTime CreatedAt { get; set; }

        [BsonElement("UpdatedAt")]
        private DateTime UpdatedAt { get; set; }

        [BsonElement("Role")]
        public int Role { get; set; }
    }
}
