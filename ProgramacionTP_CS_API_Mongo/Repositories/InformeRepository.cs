
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo_Dapper.Repositories
{
    public class InformeRepository : IInformeRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<Informe> GetInformeAsync()
        {
            Informe unInforme = new Informe();
            var conexion = contextoDB.CreateConnection();
            {
                //Total Horarios
                var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);
                var totalHorarios = await coleccionHorarios
                    .EstimatedDocumentCountAsync();

                unInforme.Horarios = totalHorarios;

                //Total cargadores
                var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);
                var totalCargadores = await coleccionCargadores
                    .EstimatedDocumentCountAsync();

                unInforme.Cargadores = totalCargadores;

                //Total autobuses
                var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);
                var totalAutobuses = await coleccionAutobuses
                    .EstimatedDocumentCountAsync();

                unInforme.Autobuses = totalAutobuses;

                //Total operacion autobuses
                var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
                var totalOperacionAutobuses = await coleccionOperacionAutobuses
                    .EstimatedDocumentCountAsync();

                unInforme.Operacion_autobuses = totalOperacionAutobuses;

                //Total utilización cargadores
                var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);
                var totalUtilizacionCargadores = await coleccionUtilizacionCargadores
                    .EstimatedDocumentCountAsync();

                unInforme.Utilizacion_cargadores = totalUtilizacionCargadores;
            }
            return unInforme;
        }
    }
    
}
