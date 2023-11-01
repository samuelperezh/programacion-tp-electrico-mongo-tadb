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

            var lasOperaciones = await coleccionOperacionAutobuses
                .Find(_ => true)
                .SortBy(operacion => operacion.Codigo_autobus)
                .ThenBy(operacion => operacion.Hora)
                .ToListAsync();

            return lasOperaciones;
        }

        public async Task<OperacionAutobus> GetByOperationAsync(int codigo_autobus, int hora)
        {
            OperacionAutobus unaOperacionAutobus = new OperacionAutobus();

            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses); ;

            var resultado = await coleccionOperacionAutobuses
                .Find(operacion => operacion.Codigo_autobus == codigo_autobus && operacion.Hora == hora)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaOperacionAutobus = resultado;
            
            return unaOperacionAutobus;
        }

        public async Task<string> GetAutobusStateAsync(int hora, int codigo_autobus)
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "hora", hora },
                    { "codigo_autobus", codigo_autobus }
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

            if (unaOperacionAutobus.Codigo_autobus != 0)
            {
                var conexion = contextoDB.CreateConnection();
                var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

                await coleccionOperacionAutobuses.InsertOneAsync(unaOperacionAutobus);

                var resultado = await GetByOperationAsync(unaOperacionAutobus.Codigo_autobus, unaOperacionAutobus.Hora);

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
                                operacion => operacion.Codigo_autobus == unaOperacionAutobus.Codigo_autobus && operacion.Hora == unaOperacionAutobus.Hora,
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
                                operacion => operacion.Codigo_autobus == unaOperacionAutobus.Codigo_autobus && operacion.Hora == unaOperacionAutobus.Hora);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}