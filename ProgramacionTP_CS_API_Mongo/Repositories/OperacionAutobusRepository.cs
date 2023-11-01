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
            .SortBy(operacion => operacion.Autobus_id)
            .ThenBy(operacion => operacion.Horario_id)
            .ToListAsync();
            ;
                return lasOperaciones;
            }
        }
        public async Task<OperacionAutobus> GetByOperationAsync(string? autobus_id, string? horario_id)
        {
            OperacionAutobus unaOperacionAutobus = new OperacionAutobus();

            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses); ;

            var resultado = await coleccionOperacionAutobuses
                .Find(operacion => operacion.Autobus_id == autobus_id && operacion.Horario_id == horario_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaOperacionAutobus = resultado;
           
            return unaOperacionAutobus;
        }

    public async Task<string> GetAutobusStateAsync(string horario_id, string autobus_id, BsonJavaScript bsonJavaScript)
    {
        string estado;
        var conexion = contextoDB.CreateConnection();
        var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses); ;

        var filter = Builders<OperacionAutobus>.Filter.And(
            Builders<OperacionAutobus>.Filter.Eq("horario_id", horario_id),
            Builders<OperacionAutobus>.Filter.Eq("autobus_id", autobus_id)
        );

        //Preguntarle al profesor 
        var projection = Builders<OperacionAutobus>.Projection.Expression<string>(
         new BsonJavaScript(
             "if (this.Horario_id == " + horario_id + " && this.Autobus_id == " + autobus_id + ") { 'Operando' } else { 'Parqueado' }"
         ));

        var result = await coleccionOperacionAutobuses.Find(filter)
            .Project(projection)
            .FirstOrDefaultAsync();

        if (result != null)
        {
            estado = result.ToString();
        }
        else
        {
            estado = "No encontrado";
        }

        return estado;
    }


public async Task<bool> CreateAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;
            
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

            await coleccionOperacionAutobuses.InsertOneAsync(unaOperacionAutobus);

            var resultado = await GetByOperationAsync(unaOperacionAutobus.Autobus_id, unaOperacionAutobus.Horario_id)
        if (resultado is not null)
            resultadoAccion = resultado;
            try
            {
                using (var conexion = contextoDB.CreateConnection())
                {
                    string procedimiento = "p_inserta_operacion_autobus";
                    var parametros = new
                    {
                        p_autobus_id = unaOperacionAutobus.Autobus_id,
                        p_horario_id = unaOperacionAutobus.Horario_id
                    };

                    var cantidad_filas = await conexion.ExecuteAsync(
                        procedimiento,
                        parametros,
                        commandType: CommandType.StoredProcedure);

                    if (cantidad_filas != 0)
                        resultadoAccion = true;
                }
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;

            try
            {
                using (var conexion = contextoDB.CreateConnection())
                {
                    string procedimiento = "p_actualiza_operacion_autobus";
                    var parametros = new
                    {
                        p_autobus_id = unaOperacionAutobus.Autobus_id,
                        p_horario_id = unaOperacionAutobus.Horario_id
                    };

                    var cantidad_filas = await conexion.ExecuteAsync(
                        procedimiento,
                        parametros,
                        commandType: CommandType.StoredProcedure);

                    if (cantidad_filas != 0)
                        resultadoAccion = true;
                }
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> DeleteAsync(OperacionAutobus unaOperacionAutobus)
        {
            bool resultadoAccion = false;

            try
            {
                using (var conexion = contextoDB.CreateConnection())
                {
                    string procedimiento = "p_elimina_operacion_autobus";
                    var parametros = new
                    {
                        p_autobus_id = unaOperacionAutobus.Autobus_id,
                        p_horario_id = unaOperacionAutobus.Horario_id
                    };

                    var cantidad_filas = await conexion.ExecuteAsync(
                        procedimiento,
                        parametros,
                        commandType: CommandType.StoredProcedure);

                    if (cantidad_filas != 0)
                        resultadoAccion = true;
                }
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }
    }
}
