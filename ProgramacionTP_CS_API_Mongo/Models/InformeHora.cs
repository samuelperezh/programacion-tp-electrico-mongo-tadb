namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class InformeHora
    {
        public int Hora { get; set; } = 0;
        public bool Horario_pico { get; set; } = false;
        public float Porcentaje_cargadores_utilizados { get; set; } = 0;
        public float Porcentaje_autobuses_operacion { get; set; } = 0;
    }
}