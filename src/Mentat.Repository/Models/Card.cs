using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentat.Repository.Models
{
    public class Card
    {
        [BsonElement("_id")]
        public Object? Id { get; set; }

        [BsonElement("subject")]
        public string? Subject { get; set; }

        [BsonElement("question")]
        public string? Question { get; set; }

        [BsonElement("answer")]
        public string? Answer { get; set; }

        [BsonElement("isCustom")]
        public bool IsCustom { get; set; }

        [BsonElement("difficulty_level")]
        public string? DifficultyLevel { get; set; }

        [BsonElement("notes")]
        public string? Notes { get; set; }
    }
}
