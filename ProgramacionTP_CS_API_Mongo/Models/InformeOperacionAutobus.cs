using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class InformeOperacionAutobus
    {
        [JsonPropertyName("hora")]
        public int Hora { get; set; } = 0;

        [JsonPropertyName("total_operacion_autobuses")]
        public long Total_operacion_autobuses { get; set; } = 0;
    }
}