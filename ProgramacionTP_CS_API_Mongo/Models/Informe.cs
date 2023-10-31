using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class Informe
    {
        [JsonPropertyName("horarios")]
        public int Horarios { get; set; } = 0;

        [JsonPropertyName("autobuses")]
        public int Autobuses { get; set; } =0;

        [JsonPropertyName("cargadores")]
        public int Cargadores { get; set; } = 0;

        [JsonPropertyName("operacion_autobuses")]
        public float Operacion_autobuses { get; set; } = 0;

        [JsonPropertyName("utilizacion_cargadores")]
        public float Utilizacion_cargadores { get; set; } = 0;
    }
}