using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class AutobusRepository : IAutobusRepository
    {
        private readonly MongoDbContext contextoDB;

        public AutobusRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<Autobus>> GetAllAsync()
        {
                var conexion = contextoDB.CreateConnection();
                var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

                var losAutobuses = await coleccionAutobuses
                    .Find(_ => true)
                    .SortBy(autobus => autobus.Nombre_autobus)
                    .ToListAsync();
            
                return losAutobuses;
        }

        public async Task<Autobus> GetByIdAsync(string autobus_id)
        {
            Autobus unAutobus = new Autobus();

            var conexion = contextoDB.CreateConnection();
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var resultado = await coleccionAutobuses
                    .Find(autobus => autobus.Id == autobus_id)
                    .FirstOrDefaultAsync();

            if (resultado is not null)
                    unAutobus = resultado;

            return unAutobus;
        }

        public async Task<Autobus> GetByNameAsync(string nombre_autobus)
        {
            Autobus unAutobus = new Autobus();

                var conexion = contextoDB.CreateConnection();
                var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

                var resultado = await coleccionAutobuses
                    .Find(autobus => autobus.Nombre_autobus == nombre_autobus)
                    .FirstOrDefaultAsync();

                if (resultado is not null)
                    unAutobus = resultado;

            return unAutobus;
        }

        public async Task<int> GetTotalAssociatedChargerUtilizationAsync(string autobus_id)
        {
            Autobus unAutobus = await GetByIdAsync(autobus_id);
            
            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>("utilizacion_cargadores");

            var builder = Builders<UtilizacionCargador>.Filter;
            var filtro = builder.And(
                builder.Eq(utilizacionCargador => utilizacionCargador.Autobus_id, unAutobus.Id),
                builder.Eq(utilizacionCargador => utilizacionCargador.Nombre_autobus, unAutobus.Nombre_autobus));

            var totalUtilizaciones = await coleccionUtilizacionCargadores
                .Find(filtro)
                .SortBy(utilizacionCargador => utilizacionCargador.Cargador_id)
                .ToListAsync();

            return totalUtilizaciones.Count();
        }

        public async Task<int> GetTotalAssociatedAutobusOperationAsync(string autobus_id)
        {
            Autobus unAutobus = await GetByIdAsync(autobus_id);
            
            var conexion = contextoDB.CreateConnection();
            var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>("operacion_autobuses");

            var builder = Builders<OperacionAutobus>.Filter;
            var filtro = builder.And(
                builder.Eq(operacionAutobuses => operacionAutobuses.Autobus_id, unAutobus.Id),
                builder.Eq(operacionAutobuses => operacionAutobuses.Nombre_autobus, unAutobus.Nombre_autobus));

            var totalUtilizaciones = await coleccionOperacionAutobuses
                .Find(filtro)
                .SortBy(operacionAutobuses => operacionAutobuses.Autobus_id)
                .ToListAsync();

            return totalUtilizaciones.Count();
        }
        public async Task<bool> CreateAsync(Autobus unAutobus)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            await coleccionAutobuses.InsertOneAsync(unAutobus);

            var resultado = await coleccionAutobuses
                .Find(autobus => autobus.Id == unAutobus.Id)
                .FirstOrDefaultAsync();

            if (resultado != null)
            {
                var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);

                // Buscar un horario en horario pico (cualquier criterio que indique horario pico)
                var horarioPico = await coleccionHorarios
                    .Find(horario => horario.Horario_pico == true)
                    .FirstOrDefaultAsync();

                if (horarioPico != null)
                {
                    // Registrar la asignación en una colección de asignaciones
                    var coleccionOperacionAutobuses = conexion.GetCollection<OperacionAutobus>(contextoDB.configuracionColecciones.ColeccionOperacionAutobuses);

                    var asignacion = new OperacionAutobus
                    {
                        Autobus_id = resultado.Id,  // Asigna el ID del autobús
                        Hora = horarioPico.Hora  // Asigna el ID del horario en horario pico
                    };

                    // Inserta el nuevo documento en la colección OperacionAutobus
                    await coleccionOperacionAutobuses.InsertOneAsync(asignacion);
                }
            }

            // Aquí siempre debes tener una sentencia de retorno
            return resultadoAccion;
        }

        //public async Task<bool> CreateAsync(Autobus unAutobus)
        //{
        //    bool resultadoAccion = false;

        //    var conexion = contextoDB.CreateConnection();
        //    var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

        //    await coleccionAutobuses.InsertOneAsync(unAutobus);

        //    var resultado = await coleccionAutobuses
        //            .Find(autobus => autobus.Autobus_id == unAutobus.Autobus_id)
        //            .FirstOrDefaultAsync();

        //    if (resultado is not null)
        //    {
        //        var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);

        //        // Buscar un horario en horario pico (cualquier criterio que indique horario pico)
        //        var horarioPico = await coleccionHorarios
        //            .Find(horario => horario.Horario_pico == true)
        //            .FirstOrDefaultAsync();

        //        if (horarioPico != null)
        //        {
        //            // Asignar el horario en horario pico al autobús
        //            resultado.Horario = horarioPico.Hora;
        //            await coleccionAutobuses.ReplaceOneAsync(autobus => autobus.Autobus_id == resultado.Autobus_id, resultado);
        //        }
        //    }

        //    if (resultado == null)
        //        resultadoAccion = true;

        //    return resultadoAccion;
        //}

        public async Task<bool> UpdateAsync(Autobus unAutobus)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var resultado = await coleccionAutobuses
                .ReplaceOneAsync(autobus => autobus.Id == unAutobus.Id, unAutobus);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> DeleteAsync(Autobus unAutobus)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);

            var resultado = await coleccionAutobuses
                .DeleteOneAsync(autobus => autobus.Id == unAutobus.Id);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;

        }
    }
}
