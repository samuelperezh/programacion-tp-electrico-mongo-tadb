using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class OperacionAutobus
    {
        [BsonElement("codigo_autobus")]
        [JsonPropertyName("codigo_autobus")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonRepresentation(BsonType.Int32)]
        public int Codigo_autobus { get; set; } = 0;

        [BsonElement("nombre_autobus")]
        [JsonPropertyName("nombre_autobus")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_autobus { get; set; } = string.Empty;

        [BsonElement("horario_id")]
        [JsonPropertyName("horario_id")]
        [BsonRepresentation(BsonType.Int64)]

         public int Horario_id { get; set; } = 0;

            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var otraOperacionAutobus = (OperacionAutobus)obj;

                return Codigo_autobus == otraOperacionAutobus.Codigo_autobus
                    && Nombre_autobus == otraOperacionAutobus.Nombre_autobus
                    && Horario_id == otraOperacionAutobus.Horario_id;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 3;
                    hash = hash * 5 + Codigo_autobus.GetHashCode();
                    hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);
                    hash = hash * 5 + Horario_id.GetHashCode();

                    return hash;
                }
            }
        }
    }

