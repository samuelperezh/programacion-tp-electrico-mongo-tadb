using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class InformeUtilizacionCargadorService
    {
        private readonly IInformeUtilizacionCargadorRepository _informeUtilizacionCargadorRepository;

        public InformeUtilizacionCargadorService(IInformeUtilizacionCargadorRepository informeUtilizacionCargadorRepository)
        {
            _informeUtilizacionCargadorRepository = informeUtilizacionCargadorRepository;
        }

        public async Task<IEnumerable<InformeUtilizacionCargador>> GetInformeUtilizacionAsync()
        {
            return await _informeUtilizacionCargadorRepository
                .GetInformeUtilizacionAsync();
        }

        public async Task<InformeUtilizacionCargador> GetInformeUtilizacionByIdAsync(int hora)
        {
            // Validamos que el informe utilizacion cargador exista
            var unInformeUtilizacionCargador = await _informeUtilizacionCargadorRepository
                .GetInformeUtilizacionByIdAsync(hora);

            if (unInformeUtilizacionCargador.Hora != hora)
                throw new AppValidationException($"La hora {hora} no existe en la base de datos");

            return unInformeUtilizacionCargador;
        }
    }
}