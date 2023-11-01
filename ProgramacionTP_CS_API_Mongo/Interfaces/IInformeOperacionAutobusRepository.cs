using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IInformeOperacionAutobusRepository
    {
        public Task<IEnumerable<InformeOperacionAutobus>> GetInformeOperacionAsync();
        public Task<InformeOperacionAutobus> GetInformeOperacionByIdAsync(int hora);
    }
}