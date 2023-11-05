using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
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
            var coleccionAutobuses = conexion.GetCollection<Autobus>(contextoDB.configuracionColecciones.ColeccionAutobuses);
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var lasUtilizaciones = await coleccionUtilizacionCargadores.Find(_ => true).ToListAsync();
            var losAutobuses = await coleccionAutobuses.Find(_ => true).ToListAsync();
            var losCargadores = await coleccionCargadores.Find(_ => true).ToListAsync();

            var lasUtilizacionesConNombreAutobus = lasUtilizaciones.Select(utilizacion => 
            {
                var autobus = losAutobuses.FirstOrDefault(a => a.Id == utilizacion.Autobus_id);
                var cargador = losCargadores.FirstOrDefault(c => c.Id == utilizacion.Cargador_id);
                utilizacion.Nombre_autobus = autobus?.Nombre_autobus ?? "";
                utilizacion.Nombre_cargador = cargador?.Nombre_cargador ?? "";
                return utilizacion;
            })
            .OrderBy(utilizacion => utilizacion.Autobus_id)
            .ThenBy(utilizacion => utilizacion.Hora)
            .ToList();

            return lasUtilizacionesConNombreAutobus;
        }

        public async Task<UtilizacionCargador> GetByUtilizationAsync(string cargador_id, string autobus_id, int hora)
        {
            UtilizacionCargador unaUtilizacionCargador = new UtilizacionCargador();
            
            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores); ;

            var resultado = await coleccionUtilizacionCargadores
                .Find(utilizacion => utilizacion.Cargador_id == cargador_id && utilizacion.Autobus_id == autobus_id && utilizacion.Hora == hora)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaUtilizacionCargador = resultado;

            return unaUtilizacionCargador;
        }

        public async Task<bool> CreateAsync(UtilizacionCargador unaUtilizacionCargador)
        {
            bool resultadoAccion = false;

            if (string.IsNullOrEmpty(unaUtilizacionCargador.Autobus_id) == false && string.IsNullOrEmpty(unaUtilizacionCargador.Cargador_id) == false)
            {
                var conexion = contextoDB.CreateConnection();
                var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);

                await coleccionUtilizacionCargadores.InsertOneAsync(unaUtilizacionCargador);

                unaUtilizacionCargador.Hora += 1;
                await coleccionUtilizacionCargadores.InsertOneAsync(unaUtilizacionCargador);

                var resultado = await GetByUtilizationAsync(unaUtilizacionCargador.Cargador_id, unaUtilizacionCargador.Autobus_id, unaUtilizacionCargador.Hora);

                if (resultado is not null)
                {
                    resultadoAccion = true;
                }
            }

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(UtilizacionCargador unaUtilizacionCargador)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);
            
            var resultado = await coleccionUtilizacionCargadores.ReplaceOneAsync(
                                utilizacion => utilizacion.Cargador_id == unaUtilizacionCargador.Cargador_id && utilizacion.Autobus_id == unaUtilizacionCargador.Autobus_id && utilizacion.Hora == unaUtilizacionCargador.Hora,
                                                unaUtilizacionCargador);
            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> DeleteAsync(UtilizacionCargador unaUtilizacionCargador)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>(contextoDB.configuracionColecciones.ColeccionUtilizacionCargadores);

            var resultado = await coleccionUtilizacionCargadores.DeleteOneAsync(
                                utilizacion => utilizacion.Cargador_id == unaUtilizacionCargador.Cargador_id && utilizacion.Autobus_id == unaUtilizacionCargador.Autobus_id && utilizacion.Hora == unaUtilizacionCargador.Hora);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}