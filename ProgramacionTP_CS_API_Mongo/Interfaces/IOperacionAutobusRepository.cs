using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IOperacionAutobusRepository
    {
        Task<IEnumerable<OperacionAutobus>> GetAllAsync();
        Task<OperacionAutobus> GetByOperationAsync(int codigo_autobus, int horario_id);
        Task<string> GetAutobusStateAsync(int horario_id, int codigo_autobus);
        Task<bool> CreateAsync(OperacionAutobus unaOperacionAutobus);
        Task<bool> UpdateAsync(OperacionAutobus unaOperacionAutobus);
        Task<bool> DeleteAsync(OperacionAutobus unaOperacionAutobus);

    }
}
