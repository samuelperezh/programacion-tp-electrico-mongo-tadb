using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_PostgreSQL_Dapper.Repositories
{
    public class UtilizacionCargadorRepository : IUtilizacionCargadorRepository
    {
        private readonly MongoDbContext contextoDB;

        public UtilizacionCargadorRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<UtilizacionCargador>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);

            var lasUtilizaciones = await coleccionUtilizacionCargadores
                .Find(_ => true)
                .SortBy(operacion => operacion.Codigo_autobus)
                .ThenBy(operacion => operacion.Hora)
                .ToListAsync();

            return lasUtilizaciones;
        }

        // public async Task<UtilizacionCargador> GetByUtilizationAsync(int cargador_id, int autobus_id, int horario_id)
        // {
        //     UtilizacionCargador unaUtilizacionCargador = new UtilizacionCargador();

        //     using (var conexion = contextoDB.CreateConnection())
        //     {
        //         DynamicParameters parametrosSentencia = new DynamicParameters();
        //         parametrosSentencia.Add("@cargador_id", cargador_id,
        //                                 DbType.Int32, ParameterDirection.Input);
        //         parametrosSentencia.Add("@autobus_id", autobus_id,
        //                                 DbType.Int32, ParameterDirection.Input);
        //         parametrosSentencia.Add("@horario_id", horario_id,
        //                                 DbType.Int32, ParameterDirection.Input);

        //         string sentenciaSQL = "SELECT cargador_id, autobus_id, horario_id " +
        //                               "FROM utilizacion_cargadores " +
        //                               "WHERE cargador_id = @cargador_id AND autobus_id= @autobus_id AND horario_id = @horario_id";

        //         var resultado = await conexion.QueryAsync<UtilizacionCargador>(sentenciaSQL,
        //                             parametrosSentencia);

        //         if (resultado.Count() > 0)
        //             unaUtilizacionCargador = resultado.First();
        //     }

        //     return unaUtilizacionCargador;
        // }

        // public async Task<bool> CreateAsync(UtilizacionCargador unaUtilizacionCargador)
        // {
        //     bool resultadoAccion = false;

        //     try
        //     {
        //         using (var conexion = contextoDB.CreateConnection())
        //         {
        //             string procedimiento = "p_inserta_utilizacion_cargador";
        //             var parametros = new
        //             {
        //                 p_cargador_id = unaUtilizacionCargador.Cargador_id,
        //                 p_autobus_id = unaUtilizacionCargador.Autobus_id,
        //                 p_horario_id = unaUtilizacionCargador.Horario_id
        //             };

        //             var cantidad_filas = await conexion.ExecuteAsync(
        //                 procedimiento,
        //                 parametros,
        //                 commandType: CommandType.StoredProcedure);

        //             var parametros2 = new
        //             {
        //                 p_cargador_id = unaUtilizacionCargador.Cargador_id,
        //                 p_autobus_id = unaUtilizacionCargador.Autobus_id,
        //                 p_horario_id = unaUtilizacionCargador.Horario_id+1
        //             };

        //             var cantidad_filas2 = await conexion.ExecuteAsync(
        //                 procedimiento,
        //                 parametros2,
        //                 commandType: CommandType.StoredProcedure);

        //             if (cantidad_filas != 0)
        //                 resultadoAccion = true;
        //         }
        //     }
        //     catch (NpgsqlException error)
        //     {
        //         throw new DbOperationException(error.Message);
        //     }

        //     return resultadoAccion;
        // }

        // public async Task<bool> UpdateAsync(UtilizacionCargador unaUtilizacionCargador)
        // {
        //     bool resultadoAccion = false;

        //     try
        //     {
        //         using (var conexion = contextoDB.CreateConnection())
        //         {
        //             string procedimiento = "p_actualiza_utilizacion_cargador";
        //             var parametros = new
        //             {
        //                 p_cargador_id = unaUtilizacionCargador.Cargador_id,
        //                 p_autobus_id = unaUtilizacionCargador.Autobus_id,
        //                 p_horario_id = unaUtilizacionCargador.Horario_id

        //             };

        //             var cantidad_filas = await conexion.ExecuteAsync(
        //                 procedimiento,
        //                 parametros,
        //                 commandType: CommandType.StoredProcedure);

        //             if (cantidad_filas != 0)
        //                 resultadoAccion = true;
        //         }
        //     }
        //     catch (NpgsqlException error)
        //     {
        //         throw new DbOperationException(error.Message);
        //     }

        //     return resultadoAccion;
        // }

        // public async Task<bool> DeleteAsync(UtilizacionCargador unaUtilizacionCargador)
        // {
        //     bool resultadoAccion = false;

        //     try
        //     {
        //         using (var conexion = contextoDB.CreateConnection())
        //         {
        //             string procedimiento = "p_elimina_utilizacion_cargador";
        //             var parametros = new
        //             {
        //                 p_cargador_id = unaUtilizacionCargador.Cargador_id,
        //                 p_autobus_id = unaUtilizacionCargador.Autobus_id,
        //                 p_horario_id = unaUtilizacionCargador.Horario_id
        //             };

        //             var cantidad_filas = await conexion.ExecuteAsync(
        //                 procedimiento,
        //                 parametros,
        //                 commandType: CommandType.StoredProcedure);

        //             if (cantidad_filas != 0)
        //                 resultadoAccion = true;
        //         }
        //     }
        //     catch (NpgsqlException error)
        //     {
        //         throw new DbOperationException(error.Message);
        //     }

        //     return resultadoAccion;
        // }
    }
}