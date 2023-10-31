using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IHorarioRepository
    {
        public Task<IEnumerable<Horario>> GetAllAsync();
        public Task<Horario> GetByIdAsync(int horario_id);
    }
}
