using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IAutobusRepository
    {
        public Task<IEnumerable<Autobus>> GetAllAsync();
        public Task<Autobus> GetByIdAsync(string autobus_id);
        public Task<Autobus> GetByNameAsync(string nombre_autobus);
        public Task<int> GetTotalAssociatedChargerUtilizationAsync(string autobus_id);
        public Task<int> GetTotalAssociatedAutobusOperationAsync(string autobus_id);
        public Task<bool> CreateAsync(Autobus unAutobus);
        public Task<bool> UpdateAsync(Autobus unAutobus);
        public Task<bool> DeleteAsync(Autobus unAutobus);
    }
}