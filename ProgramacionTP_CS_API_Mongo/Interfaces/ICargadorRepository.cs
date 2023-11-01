using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface ICargadorRepository
    {
        public Task<IEnumerable<Cargador>> GetAllAsync();
        public Task<Cargador> GetByIdAsync(int codigo_cargador);
        public Task<Cargador> GetByNameAsync(string nombre_cargador);
        public Task<int> GetTotalAssociatedChargerUtilizationAsync(int codigo_cargador);
        public Task<bool> CreateAsync(Cargador unCargador);
        public Task<bool> UpdateAsync(Cargador unCargador);
        public Task<bool> DeleteAsync(Cargador unCargador);
    }
}