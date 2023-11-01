using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IUtilizacionCargadorRepository
    {
        public Task<IEnumerable<UtilizacionCargador>> GetAllAsync();
        public Task<UtilizacionCargador> GetByUtilizationAsync(int codigo_cargador, int codigo_autobus, int hora);
        public Task<bool> CreateAsync(UtilizacionCargador unaUtilizacionCargador);
        public Task<bool> UpdateAsync(UtilizacionCargador unaUtilizacionCargador);
        public Task<bool> DeleteAsync(UtilizacionCargador unaUtilizacionCargador);
    }
}