using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using MongoDB.Driver;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class HorarioRepository : IHorarioRepository
        {
            private readonly MongoDbContext contextoDB;

            public HorarioRepository(MongoDbContext unContexto)
            {
                contextoDB = unContexto;
            }
            public async Task<IEnumerable<Horario>> GetAllAsync()
            {
                var conexion = contextoDB.CreateConnection();
                var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);

                var losHorarios = await coleccionHorarios
                    .Find(_ => true)
                    .SortBy(horario => horario.Horario_pico)
                    .ToListAsync();

                return losHorarios;
            }

            public async Task<Horario> GetByIdAsync(int hora)
            {
                Horario unHorario = new();

                var conexion = contextoDB.CreateConnection();
                var coleccionHorarios = conexion.GetCollection<Horario>(contextoDB.configuracionColecciones.ColeccionHorarios);

                var resultado = await coleccionHorarios
                    .Find(Horario => Horario.Hora == hora)
                    .FirstOrDefaultAsync();

                if (resultado is not null)
                    unHorario = resultado;

                return unHorario;
            }
        }

    
}


