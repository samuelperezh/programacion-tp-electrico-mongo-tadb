using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class UtilizacionCargador
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("cargador_id")]
        [JsonPropertyName("cargador_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Cargador_id { get; set; } = null;

        [BsonElement("nombre_cargador")]
        [JsonPropertyName("nombre_cargador")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_cargador { get; set; } = string.Empty;

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

            var otraUtilizacion = (UtilizacionCargador)obj;

            return Cargador_id == otraUtilizacion.Cargador_id
                && Nombre_cargador.Equals(otraUtilizacion.Nombre_cargador)
                && Autobus_id == otraUtilizacion.Autobus_id
                && Nombre_autobus.Equals(otraUtilizacion.Nombre_autobus)
                && Hora.Equals(otraUtilizacion.Hora);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (Cargador_id?.GetHashCode() ?? 0);
                hash = hash * 5 + (Nombre_cargador?.GetHashCode() ?? 0);
                hash = hash * 5 + (Autobus_id?.GetHashCode() ?? 0);
                hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);
                hash = hash * 5 + Hora.GetHashCode();

                return hash;
            }
        }
    }
}