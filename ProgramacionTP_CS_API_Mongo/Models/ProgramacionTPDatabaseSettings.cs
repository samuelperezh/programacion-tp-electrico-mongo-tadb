namespace ProgramacionTP_CS_API_Mongo.Models
{
    public class ProgramacionTPDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ColeccionAutobuses { get; set; } = null!;
        public string ColeccionCargadores { get; set; } = null!;
        public string ColeccionHorarios { get; set; } = null!;
        public string ColeccionOperacionAutobuses { get; set; } = null!;
        public string ColeccionUtilizacionCargadores { get; set; } = null!;

        public ProgramacionTPDatabaseSettings(IConfiguration unaConfiguracion)
        {
            var configuracion = unaConfiguracion.GetSection("ProgramacionTPDatabaseSettings");

            DatabaseName = configuracion.GetSection("DatabaseName").Value!;
            ColeccionAutobuses = configuracion.GetSection("ColeccionAutobuses").Value!;
            ColeccionCargadores = configuracion.GetSection("ColeccionCargadores").Value!;
            ColeccionHorarios = configuracion.GetSection("ColeccionHorarios").Value!;
            ColeccionOperacionAutobuses = configuracion.GetSection("ColeccionOperacionAutobuses").Value!;
            ColeccionUtilizacionCargadores = configuracion.GetSection("ColeccionUtilizacionCargadores").Value!;
        }
    }
}