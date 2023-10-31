using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class OperacionAutobus
    {
        [BsonElement("Autobus_id")]
        [JsonPropertyName("Autobus_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Autobus_id { get; set; } = null;

        [BsonElement("Nombre_autobus")]
        [JsonPropertyName("Nombre_autobus")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_autobus { get; set; } = string.Empty;

        [BsonElement("Horario_id")]
        [JsonPropertyName("Horario_id")]
        [BsonRepresentation(BsonType.Int64)]

         public int Horario_id { get; set; } = 0;

            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var otraOperacionAutobus = (OperacionAutobus)obj;

                return Autobus_id == otraOperacionAutobus.Autobus_id
                    && Nombre_autobus == otraOperacionAutobus.Nombre_autobus
                    && Horario_id == otraOperacionAutobus.Horario_id;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 3;
                    hash = hash * 5 + (Autobus_id?.GetHashCode() ?? 0);
                    hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);
                    hash = hash * 5 + Horario_id.GetHashCode();

                    return hash;
                }
            }
        }
    }

