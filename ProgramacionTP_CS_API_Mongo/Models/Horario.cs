using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class Horario
    {
        [BsonElement("Id")]
        [JsonPropertyName("Id")]
        [BsonRepresentation(BsonType.Int64)]

        public int Id { get; set; } = 0;

        [BsonElement("Horario_pico")]
        [JsonPropertyName("Horario_pico")]
        [BsonRepresentation(BsonType.String)]

        public string Horario_pico { get; set; } = string.Empty;
    }
}
