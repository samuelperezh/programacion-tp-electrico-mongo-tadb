using System.Text.Json.Serialization;

namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class InformeUtilizacionCargador
    {
        [JsonPropertyName("hora")]
        public int Hora { get; set; } = 0;

        [JsonPropertyName("total_utilizacion_cargadores")]
        public int Total_utilizacion_cargadores { get; set; } = 0;
    }
}