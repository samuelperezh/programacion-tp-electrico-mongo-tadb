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
        public async Task<InformeOperacionAutobus> GetAllAsync()
        {
            InformeOperacionAutobus unInformeOperacion = new();
            var conexion = contextoDB.CreateConnection();

            ////Total operaciones
            var coleccionOperaciones = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
            var totalOperaciones = await coleccionOperaciones
                .EstimatedDocumentCountAsync();

            unInformeOperacion.Total_operacion_autobuses = totalOperaciones;

            return unInformeOperacion;
        }
        public async Task<InformeOperacionAutobus> GetInformeOperacionByIdAsync(int hora)
        {
           
        }
    }
}
