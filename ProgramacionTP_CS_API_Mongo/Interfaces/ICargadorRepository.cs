using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface ICargadorRepository
    {
        public Task<IEnumerable<Cargador>> GetAllAsync();
        public Task<Cargador> GetByIdAsync(string cargador_id);
        public Task<Cargador> GetByNameAsync(string nombre_cargador);
        public Task<int> GetTotalAssociatedChargerUtilizationAsync(string cargador_id);
        public Task<bool> CreateAsync(Cargador unCargador);
        public Task<bool> UpdateAsync(Cargador unCargador);
        public Task<bool> DeleteAsync(Cargador unCargador);
    }
}