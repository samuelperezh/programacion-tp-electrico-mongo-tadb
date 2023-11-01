using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class HorarioService
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioService(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task<IEnumerable<Horario>> GetAllAsync()
        {
            return await _horarioRepository
                .GetAllAsync();
        }

        public async Task<Horario> GetByIdAsync(int hora)
        {
            //Validamos que el horario exista con ese Id
            var unHorario = await _horarioRepository
                .GetByIdAsync(hora);

            if (unHorario.Hora == 0)
                throw new AppValidationException($"Horario no encontrado con el id {hora}");

            return unHorario;
        }
    }
}
