using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentat.Repository.Models
{
    public class Card
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("question")]
        public string Question { get; set; }

        [BsonElement("answer")]
        public string Answer { get; set; }

        [BsonElement("isCustom")]
        public bool IsCustom { get; set; }

        [BsonElement("difficultyLevel")]
        public string DifficultyLevel { get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }
    }
}
