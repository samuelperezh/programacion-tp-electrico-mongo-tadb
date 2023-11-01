using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class Cargador
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

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroCargador = (Cargador)obj;

            return Id == otroCargador.Id
                && Nombre_cargador.Equals(otroCargador.Nombre_cargador)
                && Codigo_cargador.Equals(otroCargador.Codigo_cargador);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (Id?.GetHashCode() ?? 0);
                hash = hash * 5 + (Nombre_cargador?.GetHashCode() ?? 0);
                hash = hash * 5 + Codigo_cargador.GetHashCode();

                return hash;
            }
        }
    }
}