using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mentat.Repository.Models
{
    [BsonIgnoreExtraElements]
    public class Set
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("isPublic")]
        public bool IsPublic { get; set; }

        [BsonElement("cards")]
        public List<Card> Cards { get; set; }
    }
}

