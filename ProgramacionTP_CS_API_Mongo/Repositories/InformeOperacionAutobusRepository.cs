using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class InformeOperacionAutobusRepository : IInformeOperacionAutobusRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeOperacionAutobusRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }
        
        public async Task<IEnumerable<InformeOperacionAutobus>> GetInformeOperacionAsync()
        {
            var informes = new List<InformeOperacionAutobus>();

            var conexion = contextoDB.CreateConnection();

            var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);
            var coleccionOperacionAutobuses = conexion.GetCollection<InformeOperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            var horarios = await coleccionHorarios.Distinct(h => h.Hora, new BsonDocument()).ToListAsync();

            foreach (var hora in horarios)
            {
                var count = await coleccionOperacionAutobuses.CountDocumentsAsync(uc => uc.Hora == hora);
                informes.Add(new InformeOperacionAutobus { Hora = hora, Total_operacion_autobuses = (int)count });
            }

            return informes;
        }

        public async Task<InformeOperacionAutobus> GetInformeOperacionByIdAsync(int hora)
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<InformeOperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            var count = await coleccionOperacionAutobuses.CountDocumentsAsync(uc => uc.Hora == hora);

            InformeOperacionAutobus unInformeOperacionAutobus = new InformeOperacionAutobus();

            unInformeOperacionAutobus.Hora = hora;
            unInformeOperacionAutobus.Total_operacion_autobuses = (int)count;

            return unInformeOperacionAutobus;
        }
    }
}