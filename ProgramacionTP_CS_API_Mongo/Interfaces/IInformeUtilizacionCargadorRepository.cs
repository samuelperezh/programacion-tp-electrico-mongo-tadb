using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IInformeUtilizacionCargadorRepository
    {
        public Task<IEnumerable<InformeUtilizacionCargador>> GetInformeUtilizacionAsync();
        public Task<InformeUtilizacionCargador> GetInformeUtilizacionByIdAsync(int hora);
    }
}