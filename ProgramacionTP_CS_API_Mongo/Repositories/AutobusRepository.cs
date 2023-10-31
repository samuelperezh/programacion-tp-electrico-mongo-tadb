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

    public async Task<int> GetTotalAssociatedChargerUtilizationAsync(int autobus_id)
    {
        var totalUtilizaciones = await GetTotalAssociatedChargerUtilizationAsync(autobus_id);

       return totalUtilizaciones;
    }

    public async Task<int> GetTotalAssociatedAutobusOperationAsync(int autobus_id)
    {
        var totalOperaciones = await GetTotalAssociatedAutobusOperationAsync(autobus_id);

            return totalOperaciones;
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
        if (resultado is not null)
            {
                var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);

                // Buscar un horario en horario pico (cualquier criterio que indique horario pico)
                var horarioPico = await coleccionHorarios
                    .Find(horario => horario.Horario_pico == true)
                    .FirstOrDefaultAsync();

                if (horarioPico != null)
                {
                    // Asignar el horario en horario pico al autobús
                    resultado.Horario = horarioPico.Id;
                    await coleccionAutobuses.ReplaceOneAsync(autobus => autobus.Id == resultado.Id, resultado);
                }
            }

            if (resultado == null)
                resultadoAccion = true;

            return resultadoAccion;
        }

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
