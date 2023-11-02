using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IOperacionAutobusRepository
    {
        public Task<IEnumerable<OperacionAutobus>> GetAllAsync();
        public Task<OperacionAutobus> GetByOperationAsync(string autobus_id, int hora);
        public Task<string> GetAutobusStateAsync(int hora, string autobus_id);
        public Task<bool> CreateAsync(OperacionAutobus unaOperacionAutobus);
        public Task<bool> UpdateAsync(OperacionAutobus unaOperacionAutobus);
        public Task<bool> DeleteAsync(OperacionAutobus unaOperacionAutobus);
    }
}