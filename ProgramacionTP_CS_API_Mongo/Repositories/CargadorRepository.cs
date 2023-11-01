using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class CargadorRepository : ICargadorRepository
    {
        private readonly MongoDbContext contextoDB;

        public CargadorRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<Cargador>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var losCargadores = await coleccionCargadores
                .Find(_ => true)
                .SortBy(cargador => cargador.Nombre_cargador)
                .ToListAsync();

            return losCargadores;
        }

        public async Task<Cargador> GetByIdAsync(int codigo_cargador)
        {
            Cargador unCargador = new Cargador();

            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var resultado = await coleccionCargadores
                    .Find(cargador => cargador.Codigo_cargador == codigo_cargador)
                    .FirstOrDefaultAsync();
            
            if (resultado is not null)
                unCargador = resultado;

            return unCargador;
        }

        public async Task<Cargador> GetByNameAsync(string nombre_cargador)
        {
            Cargador unCargador = new Cargador();

            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var resultado = await coleccionCargadores
                .Find(cargador => cargador.Nombre_cargador == nombre_cargador)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unCargador = resultado;

            return unCargador;
        }

        public async Task<int> GetTotalAssociatedChargerUtilizationAsync(int codigo_cargador)
        {
            Cargador unCargador = await GetByIdAsync(codigo_cargador);
            
            var conexion = contextoDB.CreateConnection();
            var coleccionUtilizacionCargadores = conexion.GetCollection<UtilizacionCargador>("utilizacion_cargadores");

            var builder = Builders<UtilizacionCargador>.Filter;
            var filtro = builder.And(
                builder.Eq(utilizacionCargador => utilizacionCargador.Codigo_cargador, unCargador.Codigo_cargador),
                builder.Eq(utilizacionCargador => utilizacionCargador.Nombre_cargador, unCargador.Nombre_cargador));

            var totalUtilizaciones = await coleccionUtilizacionCargadores
                .Find(filtro)
                .SortBy(utilizacionCargador => utilizacionCargador.Codigo_cargador)
                .ToListAsync();

            return totalUtilizaciones.Count();
        }

        public async Task<bool> CreateAsync(Cargador unCargador)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            await coleccionCargadores.InsertOneAsync(unCargador);

            var resultado = await GetByNameAsync(unCargador.Nombre_cargador);

            if (resultado is not null)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(Cargador unCargador)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var resultado = await coleccionCargadores.ReplaceOneAsync(cargador => cargador.Codigo_cargador == unCargador.Codigo_cargador, unCargador);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> DeleteAsync(Cargador unCargador)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionCargadores = conexion.GetCollection<Cargador>(contextoDB.configuracionColecciones.ColeccionCargadores);

            var resultado = await coleccionCargadores.DeleteOneAsync(cargador => cargador.Codigo_cargador == unCargador.Codigo_cargador);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}