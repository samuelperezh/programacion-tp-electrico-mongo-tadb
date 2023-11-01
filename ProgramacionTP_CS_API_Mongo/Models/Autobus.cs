using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class Autobus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("codigo_autobus")]
        [JsonPropertyName("codigo_autobus")]
        [BsonRepresentation(BsonType.Int32)]
        public int Codigo_autobus { get; set; } = 0;

        [BsonElement("nombre_autobus")]
        [JsonPropertyName("nombre_autobus")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_autobus { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroAutobus = (Autobus)obj;

            return Id == otroAutobus.Id
                && Codigo_autobus.Equals(otroAutobus.Codigo_autobus)
                && Nombre_autobus.Equals(otroAutobus.Nombre_autobus);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (Id?.GetHashCode() ?? 0);
                hash = hash * 5 + Codigo_autobus.GetHashCode();
                hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);

                return hash;
            }
        }
    }
}
