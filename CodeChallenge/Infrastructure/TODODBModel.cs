using CodeChallenge.Model;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CodeChallenge.Infrastructure
{
    public class TODODbModel
    {
        [BsonId]
        public int id { get; set; }
        [JsonPropertyName("Title")]
        [BsonElement("Title")]
        public string Title { get; set; }
        [JsonPropertyName("Description")]
        [BsonElement("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Priority")]
        [BsonElement("Priority")]
        public PriorityEnum Priority { get; set; }
    }
}
