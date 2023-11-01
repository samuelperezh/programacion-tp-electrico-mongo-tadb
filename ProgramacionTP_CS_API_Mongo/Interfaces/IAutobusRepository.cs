using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IAutobusRepository
    {
        Task<IEnumerable<Autobus>> GetAllAsync();
        Task<Autobus> GetByIdAsync(int codigo_autobus);
        Task<Autobus> GetByNameAsync(string nombre_autobus);
        Task<int> GetTotalAssociatedChargerUtilizationAsync(int codigo_autobus);
        Task<int> GetTotalAssociatedAutobusOperationAsync(int codigo_autobus);
        Task<bool> CreateAsync(Autobus unAutobus);
        Task<bool> UpdateAsync(Autobus unAutobus);
        Task<bool> DeleteAsync(Autobus unAutobus);
       
    }
}
