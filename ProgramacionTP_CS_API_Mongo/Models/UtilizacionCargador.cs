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

        [BsonElement("codigo_cargador")]
        [JsonPropertyName("codigo_cargador")]
        [BsonRepresentation(BsonType.Int32)]
        public int Codigo_cargador { get; set; } = 0;

        [BsonElement("nombre_cargador")]
        [JsonPropertyName("nombre_cargador")]
        [BsonRepresentation(BsonType.String)]
        public string Nombre_cargador { get; set; } = string.Empty;

        [BsonElement("codigo_autobus")]
        [JsonPropertyName("codigo_autobus")]
        [BsonRepresentation(BsonType.Int32)]
        public int Codigo_autobus { get; set; } = 0;

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

            return Codigo_cargador.Equals(otraUtilizacion.Codigo_cargador)
                && Nombre_cargador.Equals(otraUtilizacion.Nombre_cargador)
                && Codigo_autobus.Equals(otraUtilizacion.Codigo_autobus)
                && Nombre_autobus.Equals(otraUtilizacion.Nombre_autobus)
                && Hora.Equals(otraUtilizacion.Hora);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Codigo_cargador.GetHashCode();
                hash = hash * 5 + (Nombre_cargador?.GetHashCode() ?? 0);
                hash = hash * 5 + Codigo_autobus.GetHashCode();
                hash = hash * 5 + (Nombre_autobus?.GetHashCode() ?? 0);
                hash = hash * 5 + Hora.GetHashCode();

                return hash;
            }
        }
    }
}