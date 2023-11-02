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
                && Nombre_cargador.Equals(otroCargador.Nombre_cargador);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + (Id?.GetHashCode() ?? 0);
                hash = hash * 5 + (Nombre_cargador?.GetHashCode() ?? 0);

                return hash;
            }
        }
    }
}