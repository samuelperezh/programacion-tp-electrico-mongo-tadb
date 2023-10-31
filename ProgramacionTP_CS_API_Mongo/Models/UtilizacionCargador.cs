using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class UtilizacionCargador
    {
        
        public int Cargador_id { get; set; } = 0;
        public int Autobus_id { get; set; } = 0;
        public int Horario_id { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otraUtilizacion = (UtilizacionCargador)obj;

            return Cargador_id == otraUtilizacion.Cargador_id
                && Autobus_id == otraUtilizacion.Autobus_id
                && Horario_id == otraUtilizacion.Horario_id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Cargador_id.GetHashCode();
                hash = hash * 5 + Autobus_id.GetHashCode();
                hash = hash * 5 + Horario_id.GetHashCode();

                return hash;
            }
        }
    }
}