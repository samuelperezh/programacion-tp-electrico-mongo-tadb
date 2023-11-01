using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IAutobusRepository
    {
        public Task<IEnumerable<Autobus>> GetAllAsync();
        public Task<Autobus> GetByIdAsync(int codigo_autobus);
        public Task<Autobus> GetByNameAsync(string nombre_autobus);
        public Task<int> GetTotalAssociatedChargerUtilizationAsync(int codigo_autobus);
        public Task<int> GetTotalAssociatedAutobusOperationAsync(int codigo_autobus);
        public Task<bool> CreateAsync(Autobus unAutobus);
        public Task<bool> UpdateAsync(Autobus unAutobus);
        public Task<bool> DeleteAsync(Autobus unAutobus);
    }
}