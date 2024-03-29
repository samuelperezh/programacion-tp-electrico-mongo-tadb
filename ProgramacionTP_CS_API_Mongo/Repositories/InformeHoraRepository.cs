using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class InformeHoraRepository : IInformeHoraRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeHoraRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<InformeHora>> GetAllInformeHoraAsync()
        {
            var conexion = contextoDB.CreateConnection();

            var horariosCollection = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);
            var utilizacionCargadoresCollection = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);
            var cargadoresCollection = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);
            var operacionAutobusesCollection = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
            var autobusesCollection = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var horarios = await horariosCollection.Find(new BsonDocument()).ToListAsync();

            List<InformeHora> informes = new List<InformeHora>();

            foreach (var horario in horarios)
            {
                var informe = new InformeHora();
                informe.Hora = horario.Hora;
                informe.Horario_pico = horario.Horario_pico;

                var cargadoresUsados = await utilizacionCargadoresCollection.CountDocumentsAsync(c => c.Hora == horario.Hora);
                var totalCargadores = await cargadoresCollection.CountDocumentsAsync(new BsonDocument());

                var autobusesOperacion = await operacionAutobusesCollection.CountDocumentsAsync(o => o.Hora == horario.Hora);
                var totalAutobuses = await autobusesCollection.CountDocumentsAsync(new BsonDocument());

                if (totalCargadores > 0)
                {
                    informe.Porcentaje_cargadores_utilizados = (float)cargadoresUsados / (float)totalCargadores * 100;
                }
                else
                {
                    informe.Porcentaje_cargadores_utilizados = 0; // Evitar la división por cero
                }

                if (totalAutobuses > 0)
                {
                    informe.Porcentaje_autobuses_operacion = (float)autobusesOperacion / (float)totalAutobuses * 100;
                }
                else
                {
                    informe.Porcentaje_autobuses_operacion = 0; // Evitar la división por cero
                }

                informes.Add(informe);
            }

            return informes;
        }

        public async Task<InformeHora> GetInformeHoraAsync(int hora)
        {
            var conexion = contextoDB.CreateConnection();

            var horariosCollection = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);
            var utilizacionCargadoresCollection = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);
            var cargadoresCollection = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);
            var operacionAutobusesCollection = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
            var autobusesCollection = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var horario = await horariosCollection.Find(h => h.Hora == hora).FirstOrDefaultAsync();

            var informe = new InformeHora();
            informe.Hora = horario.Hora;
            informe.Horario_pico = horario.Horario_pico;

            var cargadoresUsados = await utilizacionCargadoresCollection.CountDocumentsAsync(uc => uc.Hora == hora);
            var totalCargadores = await cargadoresCollection.CountDocumentsAsync(new BsonDocument());

            var autobusesOperacion = await operacionAutobusesCollection.CountDocumentsAsync(oa => oa.Hora == hora);
            var totalAutobuses = await autobusesCollection.CountDocumentsAsync(new BsonDocument());

            if (totalCargadores > 0)
            {
                informe.Porcentaje_cargadores_utilizados = (float)cargadoresUsados / totalCargadores * 100;
            }
            else
            {
                informe.Porcentaje_cargadores_utilizados = 0; // Evitar la división por cero
            }

            if (totalAutobuses > 0)
            {
                informe.Porcentaje_autobuses_operacion = (float)autobusesOperacion / totalAutobuses * 100;
            }
            else
            {
                informe.Porcentaje_autobuses_operacion = 0; // Evitar la división por cero
            }

            return informe;
        }
    }
}
