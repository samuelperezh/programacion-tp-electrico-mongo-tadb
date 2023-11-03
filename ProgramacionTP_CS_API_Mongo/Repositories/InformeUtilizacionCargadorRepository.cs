using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class InformeUtilizacionCargadorRepository : IInformeUtilizacionCargadorRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeUtilizacionCargadorRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<InformeUtilizacionCargador>> GetInformeUtilizacionAsync()
        {
            var informes = new List<InformeUtilizacionCargador>();

            var conexion = contextoDB.CreateConnection();

            var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);

            var horarios = await coleccionHorarios.Distinct(h => h.Hora, new BsonDocument()).ToListAsync();

            foreach (var hora in horarios)
            {
                var count = await coleccionUtilizacionCargadores.CountDocumentsAsync(uc => uc.Hora == hora);
                informes.Add(new InformeUtilizacionCargador { Hora = hora, Total_utilizacion_cargadores = (int)count });
            }

            return informes;
        }

        public async Task<InformeUtilizacionCargador> GetInformeUtilizacionByIdAsync(int hora)
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);

            var count = await coleccionUtilizacionCargadores.CountDocumentsAsync(uc => uc.Hora == hora);

            InformeUtilizacionCargador unInformeUtilizacionCargador = new InformeUtilizacionCargador();

            unInformeUtilizacionCargador.Hora = hora;
            unInformeUtilizacionCargador.Total_utilizacion_cargadores = (int)count;

            return unInformeUtilizacionCargador;
        }
    }
}
