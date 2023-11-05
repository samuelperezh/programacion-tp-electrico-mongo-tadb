using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class OperacionAutobusRepository : IOperacionAutobusRepository
    {
        private readonly MongoDbContext contextoDB;

        public OperacionAutobusRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<OperacionAutobus>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var lasOperaciones = await coleccionOperacionAutobuses.Find(_ => true).ToListAsync();
            var losAutobuses = await coleccionAutobuses.Find(_ => true).ToListAsync();

            var lasOperacionesConNombreAutobus = lasOperaciones.Select(operacion => 
            {
                var autobus = losAutobuses.FirstOrDefault(a => a.Id == operacion.Autobus_id);
                operacion.Nombre_autobus = autobus?.Nombre_autobus ?? "";
                return operacion;
            })
            .OrderBy(operacion => operacion.Autobus_id)
            .ThenBy(operacion => operacion.Hora)
            .ToList();

            return lasOperacionesConNombreAutobus;
        }

        public async Task<OperacionAutobus> GetByOperationAsync(string autobus_id, int hora)
        {
            OperacionAutobus unaOperacionAutobus = new OperacionAutobus();

            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses); ;

            var resultado = await coleccionOperacionAutobuses
                .Find(operacion => operacion.Autobus_id == autobus_id && operacion.Hora == hora)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaOperacionAutobus = resultado;
            
            return unaOperacionAutobus;
        }

        public async Task<string> GetAutobusStateAsync(int hora, string autobus_id)
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "hora", hora },
                    { "autobus_id", autobus_id }
                }),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", null },
                    { "count", new BsonDocument("$sum", 1) }
                })
            };

            var aggregationCursor = await coleccionOperacionAutobuses.Aggregate<BsonDocument>(pipeline)
                .ToListAsync();

            string estado = "No encontrado";

            if (aggregationCursor.Count > 0)
            {
                int count = aggregationCursor[0]["count"].AsInt32;
                if (count > 0)
                {
                    estado = "Operando";
                }
                else
                {
                    // Si count es 0, el autobús está parqueado.
                    estado = "Parqueado";
                }
            }

            return estado;
        }

        public async Task<bool> CreateAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;

            if (string.IsNullOrEmpty(unaOperacionAutobus.Autobus_id) == false)
            {
                var conexion = contextoDB.CreateConnection();
                var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

                await coleccionOperacionAutobuses.InsertOneAsync(unaOperacionAutobus);

                var resultado = await GetByOperationAsync(unaOperacionAutobus.Autobus_id, unaOperacionAutobus.Hora);

                if (resultado is not null)
                {
                    resultadoAccion = true;
                }
            }

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);
            
            var resultado = await coleccionOperacionAutobuses.ReplaceOneAsync(
                                operacion => operacion.Autobus_id == unaOperacionAutobus.Autobus_id && operacion.Hora == unaOperacionAutobus.Hora,
                                                unaOperacionAutobus);
            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> DeleteAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            var resultado = await coleccionOperacionAutobuses.DeleteOneAsync(
                                operacion => operacion.Autobus_id == unaOperacionAutobus.Autobus_id && operacion.Hora == unaOperacionAutobus.Hora);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}