using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IInformeHoraRepository
    {
        public Task<IEnumerable<InformeHora>> GetAllInformeHoraAsync();
        public Task<InformeHora> GetInformeHoraAsync(int hora);
    }
}