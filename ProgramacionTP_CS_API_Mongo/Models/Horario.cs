using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class Horario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("hora")]
        [JsonPropertyName("hora")]
        [BsonRepresentation(BsonType.Int32)]
        public int Hora { get; set; } = 0;

        [BsonElement("horario_pico")]
        [JsonPropertyName("horario_pico")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Horario_pico { get; set; } = false;
    }
}