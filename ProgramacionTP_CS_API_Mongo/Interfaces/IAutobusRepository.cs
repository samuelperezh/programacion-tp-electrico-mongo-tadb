using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IAutobusRepository
    {
        Task<IEnumerable<Autobus>> GetAllAsync();
        Task<Autobus> GetByIdAsync(string autobus_id);
        Task<Autobus> GetByNameAsync(string nombre_autobus);
        Task<int> GetTotalAssociatedChargerUtilizationAsync(string id);
        Task<int> GetTotalAssociatedAutobusOperationAsync(string id);
        Task<bool> CreateAsync(Autobus unAutobus);
        Task<bool> UpdateAsync(Autobus unAutobus);
        Task<bool> DeleteAsync(Autobus unAutobus);
       
    }
}
