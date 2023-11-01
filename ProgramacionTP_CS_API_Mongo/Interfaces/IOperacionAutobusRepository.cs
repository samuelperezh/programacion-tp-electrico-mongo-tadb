using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IOperacionAutobusRepository
    {
        public Task<IEnumerable<OperacionAutobus>> GetAllAsync();
        public Task<OperacionAutobus> GetByOperationAsync(int codigo_autobus, int horario_id);
        public Task<string> GetAutobusStateAsync(int horario_id, int codigo_autobus);
        public Task<bool> CreateAsync(OperacionAutobus unaOperacionAutobus);
        public Task<bool> UpdateAsync(OperacionAutobus unaOperacionAutobus);
        public Task<bool> DeleteAsync(OperacionAutobus unaOperacionAutobus);
    }
}