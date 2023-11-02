using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class OperacionAutobus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("autobus_id")]
        [JsonPropertyName("autobus_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Autobus_id { get; set; } = null;

        [BsonElement("nombre_autobus")]
        [JsonPropertyName("nombre_autobus")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_autobus { get; set; } = string.Empty;

        [BsonElement("hora")]
        [JsonPropertyName("hora")]
        [BsonRepresentation(BsonType.Int32)]
        public int Hora { get; set; } = 0;

            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var otraOperacionAutobus = (OperacionAutobus)obj;

                return Id == otraOperacionAutobus.Id
                    && Autobus_id == otraOperacionAutobus.Autobus_id
                    && Nombre_autobus.Equals(otraOperacionAutobus.Nombre_autobus)
                    && Hora.Equals(otraOperacionAutobus.Hora);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 3;
                    hash = hash * 5 + (Id?.GetHashCode() ?? 0);
                    hash = hash * 5 + (Autobus_id?.GetHashCode() ?? 0);
                    hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);
                    hash = hash * 5 + Hora.GetHashCode();

                    return hash;
                }
            }
        }
    }

